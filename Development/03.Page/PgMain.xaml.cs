using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using ITM_Semiconductor;


namespace Development
{
    public partial class PgMain : Page, IObserverChangeBits,IObTester
    {
        private MyLogger logger = new MyLogger("PgMain");
        private DispatcherTimer timer;
        private LotInData lotInData;
        private readonly CompositeViewModel compositeViewModel;

        private bool isUpdate = false;
        private List<short> ZR_ListShortDevicePLC_10600_ = new List<short>();
        private List<short> D_ListShortDevicePLC_0_ = new List<short>();
        private List<int> D_ListDoubleDevicePLC_0_ = new List<int>();
        private List<bool> M_ListBitPLC_0_ = new List<bool>();
        private List<bool> L_ListBitPLC_10000_ = new List<bool>();
        private bool hasClearedError = false;
        private string strModelName = "";


        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool isLooping = false;

        // Define Color Displays Status Button Start/Stop
        private Brush LAMP_OFF = Brushes.LightGray;
        private Brush LAMP_GREEN = Brushes.Green;
        private Brush LAMP_RED = Brushes.Red;
        private Brush LAMP_YELLOW = Brushes.Yellow;
        private Brush LAMP_BROWN = Brushes.Brown;
        private Brush LAMP_BLUE = Brushes.Blue;
        private Brush LAMP_ORANGE = Brushes.Orange;

        public PgMain()
        {
            // ON Binding addlog
            compositeViewModel = new CompositeViewModel
            {
                LogEntries = new ObservableCollection<logEntry>()
            };
            DataContext = compositeViewModel;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            InitializeErrorCodes();
            InitializeComponent();


            // Event Page Main
            this.Loaded += PgMain_Loaded;
            this.Unloaded += PgMain_Unloaded;
            this.btStart.Click += BtStart_Click;
            this.btStop.Click += BtStop_Click;
            this.btReset.Click += BtReset_Click;
            this.btHome.Click += BtHome_Click;
            this.btLotIn.Click += BtLotIn_Click;
            this.btLotOut.Click += BtLotOut_Click;
            this.btLotEnd.Click += BtLotEnd_Click;

            this.btClr.Click += BtClr_Click;
            this.btClrA.Click += BtClrA_Click;
            this.btClrNG.Click += BtClrNG_Click;

        }

        

        private void PgMain_Unloaded(object sender, RoutedEventArgs e)
        {
            this.isUpdate = false;
            //this.RemoveDevicePLC();
            this.RemoveDataVision();
            this.StopSound();


        }
        private void PgMain_Loaded(object sender, RoutedEventArgs e)
        {
            this.isUpdate = true;
            this.StartTimer();
            this.LoadLotData();
            //this.RegisterDevicePLC();

            this.RegisterDataTester();
            this.ThreadUpDatePLC();
        }
       
        private void ThreadUpDatePLC()
        {
            new Thread(new ThreadStart(this.ReadPLC))
            {
                IsBackground = true
            }.Start();
        }
        private void ReadPLC()
        {
            while (this.isUpdate)
            {
                bool flag = UiManager.Instance.PLC.device.isOpen();
                if (flag)
                {
                    UiManager.Instance.PLC.device.ReadMultiBits(DeviceCode.M, 0, 400, out this.M_ListBitPLC_0_);
                    UiManager.Instance.PLC.device.ReadMultiBits(DeviceCode.L, 10000, 20, out this.L_ListBitPLC_10000_);
                    UiManager.Instance.PLC.device.ReadMultiWord(DeviceCode.ZR, 10600, 200, out this.ZR_ListShortDevicePLC_10600_);
                    UiManager.Instance.PLC.device.ReadMultiWord(DeviceCode.D, 0, 400, out this.D_ListShortDevicePLC_0_);
                    //UiManager.Instance.PLC.device.ReadMultiDoubleWord(DeviceCode.D, 0, 100, out this.D_ListDoubleDevicePLC_0_);

                    #region D_ListShortDevicePLC_0_->D_ListDoubleDevicePLC_0_
                    D_ListDoubleDevicePLC_0_.Clear();
                    for (int i = 0; i < D_ListShortDevicePLC_0_.Count; i += 2)
                    {
                        short lowWord = D_ListShortDevicePLC_0_[i];
                        short highWord = D_ListShortDevicePLC_0_[i + 1];

                        // ghép 2 word thành 1 int (32-bit)
                        int value = ((ushort)lowWord << 16) | (ushort)highWord;

                        D_ListDoubleDevicePLC_0_.Add(value);
                    }
                    #endregion

                    #region ZR10620-ZR10629->ASCII
                    var subWords = ZR_ListShortDevicePLC_10600_.Skip(20).Take(10).ToList(); // Lấy 10 word từ ZR10620 -> ZR10629
                    List<byte> bytes = new List<byte>(); // Ghép thành byte[]
                    foreach (short word in subWords)
                    {
                        // Thường là LowByte trước, HighByte sau
                        byte low = (byte)(word & 0xFF);
                        byte high = (byte)((word >> 8) & 0xFF);
                        bytes.Add(low);
                        bytes.Add(high);
                    }
                    strModelName = Encoding.ASCII.GetString(bytes.ToArray()); // Chuyển sang chuỗi ASCII (tối đa 20 ký tự)
                    strModelName = strModelName.TrimEnd('\0');  // Cắt bỏ ký tự rỗng
                    #endregion

                    this.UpdateError();
                    this.UpdateUI_Devices();
                    this.UpdateUI_Lamp();
                }

                Thread.Sleep(20);
            }
        }
       
        private void UpdateError()
        {
            Application.Current.Dispatcher.Invoke(delegate ()
            {
                bool flag = UiManager.Instance.PLC.device.isOpen();
                if (flag)
                {
                    bool flag2 = this.D_ListShortDevicePLC_0_.Count >= 1;
                    if (flag2)
                    {
                        this.AddError(this.D_ListShortDevicePLC_0_[200]);
                        this.AddError(this.D_ListShortDevicePLC_0_[201]);
                        this.AddError(this.D_ListShortDevicePLC_0_[202]);
                        this.AddError(this.D_ListShortDevicePLC_0_[203]);
                        this.AddError(this.D_ListShortDevicePLC_0_[204]);
                        this.AddError(this.D_ListShortDevicePLC_0_[205]);
                        this.AddError(this.D_ListShortDevicePLC_0_[206]);
                        this.AddError(this.D_ListShortDevicePLC_0_[207]);
                        this.AddError(this.D_ListShortDevicePLC_0_[208]);
                        this.AddError(this.D_ListShortDevicePLC_0_[209]);

                        bool flag3 = this.D_ListShortDevicePLC_0_[200] == 0 && !this.hasClearedError;
                        if (flag3)
                        {
                            this.ClearError();
                            this.hasClearedError = true;
                        }
                        else
                        {
                            bool flag4 = this.D_ListShortDevicePLC_0_[200] != 0;
                            if (flag4)
                            {
                                this.hasClearedError = false;
                            }
                        }
                        bool flag5 = this.M_ListBitPLC_0_[7];
                        if (flag5)
                        {
                            this.ClearError();
                        }
                    }
                }
            });
        }


        private void BtClrNG_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want to Clear Couter Data ?")) return;

            this.addLog("Click Clear Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 54, true);
                this.addLog(string.Format("Write Bit Clear L : {0} = true", 54));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 54, false);
                this.addLog(string.Format("Write Bit Clear L : {0} = False", 54));
            }
            else
            {
                this.addLog("Click Clear False : PLC Disconnect");
                this.logger.Create("Click Clear False : PLC Disconnect", LogLevel.Error);
            }
        }
        private void BtClrA_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want to Clear Couter Data ?")) return;

            this.addLog("Click Clear Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 52, true);
                this.addLog(string.Format("Write Bit Clear L : {0} = true", 52));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 52, false);
                this.addLog(string.Format("Write Bit Clear L : {0} = False", 52));
            }
            else
            {
                this.addLog("Click Clear False : PLC Disconnect");
                this.logger.Create("Click Clear False : PLC Disconnect", LogLevel.Error);
            }
        }
        private void BtClr_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want to Clear Couter Data ?")) return;

            this.addLog("Click Clear Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 50, true);
                this.addLog(string.Format("Write Bit Clear L : {0} = true", 50));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 50, false);
                this.addLog(string.Format("Write Bit Clear L : {0} = False", 50));
            }
            else
            {
                this.addLog("Click Clear False : PLC Disconnect");
                this.logger.Create("Click Clear False : PLC Disconnect", LogLevel.Error);
            }
        }
        private void BtLotEnd_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want to Stop Input Machine ?")) return;

            this.addLog("Click Stop Input Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6, true);
                this.addLog(string.Format("Write Bit Stop Input L : {0} = true", 6));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6, false);
                this.addLog(string.Format("Write Bit Stop Input L : {0} = False", 6));
            }
            else
            {
                this.addLog("Click Stop Input False : PLC Disconnect");
                this.logger.Create("Click Stop Input False : PLC Disconnect", LogLevel.Error);
            }
        }
        private void BtHome_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want to Home Machine ?")) return;

            this.addLog("Click Home Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 5, true);
                this.addLog(string.Format("Write Bit Home L : {0} = true", 5));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 5, false);
                this.addLog(string.Format("Write Bit Home L : {0} = False", 5));
            }
            else
            {
                this.addLog("Click Home False : PLC Disconnect");
                this.logger.Create("Click Home False : PLC Disconnect", LogLevel.Error);
            }

        }
        private void BtReset_Click(object sender, RoutedEventArgs e)
        {
            this.addLog("Click Reset Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 7, true);
                this.addLog(string.Format("Write Bit Reset L : {0} = true", 7));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 7, false);
                this.addLog(string.Format("Write Bit Reset L : {0} = False", 7));
            }
            else
            {
                this.addLog("Click Reset False : PLC Disconnect");
                this.logger.Create("Click Reset False : PLC Disconnect", LogLevel.Error);
            }
            this.ClearError();
            this.ResetResultTester();
        }
        private void BtStop_Click(object sender, RoutedEventArgs e)
        {
            this.addLog("Click Stop Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 3, true);
                this.addLog(string.Format("Write Bit Stop L : {0} = true", 3));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 3, false);
                this.addLog(string.Format("Write Bit Stop L : {0} = False", 3));
            }
            else
            {
                this.addLog("Click Stop False : PLC Disconnect");
                this.logger.Create("Click Stop False : PLC Disconnect", LogLevel.Error);
            }
        }
        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            this.addLog("Click Start Machine");
            bool flag = UiManager.Instance.PLC.device.isOpen();
            if (flag)
            {
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 2, true);
                this.addLog(string.Format("Write Bit Start L : {0} = true", 2));
                Thread.Sleep(20);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 2, false);
                this.addLog(string.Format("Write Bit Start L : {0} = False", 2));
            }
            else
            {
                this.addLog("Click Start False : PLC Disconnect");
                this.logger.Create("Click Start False : PLC Disconnect", LogLevel.Error);
            }
        }
        
        #region Program Tester
        public void ResetResultTester()
        {
            base.Dispatcher.Invoke(delegate ()
            {
                this.lbResultTool01.Content = "---";
                this.lbResultTool01.Background = Brushes.Orange;
                this.lbResultTool02.Content = "---";
                this.lbResultTool02.Background = Brushes.Orange;
                this.lbResultTool03.Content = "---";
                this.lbResultTool03.Background = Brushes.Orange;
                this.lbResultTool04.Content = "---";
                this.lbResultTool04.Background = Brushes.Orange;
                this.lbResultTool05.Content = "---";
                this.lbResultTool05.Background = Brushes.Orange;
                this.lbResultTool06.Content = "---";
                this.lbResultTool06.Background = Brushes.Orange;
                this.lbResultTool07.Content = "---";
                this.lbResultTool07.Background = Brushes.Orange;
                this.lbResultTool08.Content = "---";
                this.lbResultTool08.Background = Brushes.Orange;
                this.lbResultTool09.Content = "---";
                this.lbResultTool09.Background = Brushes.Orange;
                this.lbResultTool10.Content = "---";
                this.lbResultTool10.Background = Brushes.Orange;
                this.lbResultTool11.Content = "---";
                this.lbResultTool11.Background = Brushes.Orange;
                this.lbResultTool12.Content = "---";
                this.lbResultTool12.Background = Brushes.Orange;
                this.addLog("Reset Result Tool Tester Complete !");
            });
        }
        #endregion


        #region NotifyResultTester
        private void RemoveDataVision()
        {
            SystemsManager.Instance.NotifyEvenTester.Detach(this);
        }
        private void RegisterDataTester()
        {
            SystemsManager.Instance.NotifyEvenTester.Attach(this);
        }
        public void FollowDataResultTester(int ResultCH1, int ResultCH2, int ResultCH3, int ResultCH4, int ResultCH5, int ResultCH6, int ResultCH7, int ResultCH8, int ResultCH9, int ResultCH10, int ResultCH11, int ResultCH12)
        {
            base.Dispatcher.Invoke(delegate ()
            {
                bool flag = ResultCH1 == 0;
                if (flag)
                {
                    this.lbResultTool01.Content = "0:OK";
                    this.lbResultTool01.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool01.Content = string.Format("{0}:NG", ResultCH1);
                    this.lbResultTool01.Background = Brushes.Red;
                }
                bool flag2 = ResultCH2 == 0;
                if (flag2)
                {
                    this.lbResultTool02.Content = "0:OK";
                    this.lbResultTool02.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool02.Content = string.Format("{0}:NG", ResultCH2);
                    this.lbResultTool02.Background = Brushes.Red;
                }
                bool flag3 = ResultCH3 == 0;
                if (flag3)
                {
                    this.lbResultTool03.Content = "0:OK";
                    this.lbResultTool03.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool03.Content = string.Format("{0}:NG", ResultCH3);
                    this.lbResultTool03.Background = Brushes.Red;
                }
                bool flag4 = ResultCH4 == 0;
                if (flag4)
                {
                    this.lbResultTool04.Content = "0:OK";
                    this.lbResultTool04.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool04.Content = string.Format("{0}:NG", ResultCH4);
                    this.lbResultTool04.Background = Brushes.Red;
                }
                bool flag5 = ResultCH5 == 0;
                if (flag5)
                {
                    this.lbResultTool05.Content = "0:OK";
                    this.lbResultTool05.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool05.Content = string.Format("{0}:NG", ResultCH5);
                    this.lbResultTool05.Background = Brushes.Red;
                }
                bool flag6 = ResultCH6 == 0;
                if (flag6)
                {
                    this.lbResultTool06.Content = "0:OK";
                    this.lbResultTool06.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool06.Content = string.Format("{0}:NG", ResultCH6);
                    this.lbResultTool06.Background = Brushes.Red;
                }
                bool flag7 = ResultCH7 == 0;
                if (flag7)
                {
                    this.lbResultTool07.Content = "0:OK";
                    this.lbResultTool07.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool07.Content = string.Format("{0}:NG", ResultCH7);
                    this.lbResultTool07.Background = Brushes.Red;
                }
                bool flag8 = ResultCH8 == 0;
                if (flag8)
                {
                    this.lbResultTool08.Content = "0:OK";
                    this.lbResultTool08.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool08.Content = string.Format("{0}:NG", ResultCH8);
                    this.lbResultTool08.Background = Brushes.Red;
                }
                bool flag9 = ResultCH9 == 0;
                if (flag9)
                {
                    this.lbResultTool09.Content = "0:OK";
                    this.lbResultTool09.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool09.Content = string.Format("{0}:NG", ResultCH9);
                    this.lbResultTool09.Background = Brushes.Red;
                }
                bool flag10 = ResultCH10 == 0;
                if (flag10)
                {
                    this.lbResultTool10.Content = "0:OK";
                    this.lbResultTool10.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool10.Content = string.Format("{0}:NG", ResultCH10);
                    this.lbResultTool10.Background = Brushes.Red;
                }
                bool flag11 = ResultCH11 == 0;
                if (flag11)
                {
                    this.lbResultTool11.Content = "0:OK";
                    this.lbResultTool11.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool11.Content = string.Format("{0}:NG", ResultCH11);
                    this.lbResultTool11.Background = Brushes.Red;
                }
                bool flag12 = ResultCH12 == 0;
                if (flag12)
                {
                    this.lbResultTool12.Content = "0:OK";
                    this.lbResultTool12.Background = Brushes.Green;
                }
                else
                {
                    this.lbResultTool12.Content = string.Format("{0}:NG", ResultCH12);
                    this.lbResultTool12.Background = Brushes.Red;
                }
            });
        }

        public void FollowDataTester(string Messenger)
        {
            addLog(Messenger);
        }
        #endregion


        #region NotifyChangeBit
        private void RemoveDevicePLC()
        {
            UiManager.Instance.PLC.RemoveBitAddress("M", 100);
            SystemsManager.Instance.NotifyPLCBits.Detach(this);

        }
        private void RegisterDevicePLC()
        {
            SystemsManager.Instance.NotifyPLCBits.Attach(this);
            this.AddDevicePLC();
        }
        private void AddDevicePLC()
        {
            UiManager.Instance.PLC.AddBitAddress("M", 100);
        }
        public void NotifyChangeBits(string key, bool status)
        {

            //if (key == "M" + 100 && status)
            //{
            //    a++;
            //    UiManager.Instance.PLC.device.WriteBit(DeviceCode.M, 100, false);
            //    MessageBox.Show($"M 100 online {a}");
            //}
            //if (key == "M" + 3010 && status)
            //{
            //    MessageBox.Show("M 3010 online");
            //}
            //if (key == "M" + 5010 && status)
            //{
            //    MessageBox.Show("M 5010 online");
            //}
        }
        #endregion


        #region Update UI 
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateUI_TimeTick();


        }
        private void UpdateUI_TimeTick()
        {
            /// Update Ui PLC
            if (UiManager.Instance.PLC.device.isOpen())
            {
                this.lbPlcConnect.Content = "Connect";
                this.lbPlcConnect.Background = Brushes.Green;
            }
            else
            {
                this.lbPlcConnect.Content = "Disconnect";
                this.lbPlcConnect.Background = Brushes.Red;
            }

            // Update Ui Tester 
            if (UiManager.Instance.TesterCOM.isOpen())
            {
                this.lbTesterConnect.Content = "Com Open";
                this.lbTesterConnect.Background = Brushes.Green;
            }
            else
            {
                this.lbTesterConnect.Content = "No Connect";
                this.lbTesterConnect.Background = Brushes.Red;
            }
        }
        private void UpdateUI_Lamp()
        {
            try
            {


                if (UiManager.Instance.PLC.device.isOpen())
                {

                    if (L_ListBitPLC_10000_.Count >= 1)
                    {

                        Dispatcher.Invoke(() =>
                        {
                            this.btStart.Background = L_ListBitPLC_10000_[2] ? LAMP_GREEN : LAMP_OFF;
                            this.btStop.Background = L_ListBitPLC_10000_[3] ? LAMP_RED : LAMP_OFF;
                            this.btReset.Background = L_ListBitPLC_10000_[7] ? LAMP_YELLOW : LAMP_OFF;
                            this.btHome.Background = L_ListBitPLC_10000_[5] ? LAMP_ORANGE : LAMP_OFF;
                            this.btLotEnd.Background = L_ListBitPLC_10000_[6] ? LAMP_BROWN : LAMP_OFF;
                        });
                    }


                }

            }
            catch (Exception ex)
            {
                //this.logger.Create("Update Status PLC Error: " + ex.Message, LogLevel.Error);
            }


        }
        private void UpdateUI_Devices()
        {
            try
            {
                if (UiManager.Instance.PLC.device.isOpen())
                {

                    if (D_ListDoubleDevicePLC_0_.Count >= 1)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            this.lblModelNo.Content = ZR_ListShortDevicePLC_10600_[10].ToString();
                            this.lblModelName.Content = strModelName.ToString();


                            this.lbCoutter1.Content = D_ListDoubleDevicePLC_0_[20].ToString();
                            this.lbCoutter2.Content = D_ListDoubleDevicePLC_0_[22].ToString();
                            this.lbCoutter3.Content = D_ListDoubleDevicePLC_0_[24].ToString();
                            this.lbCoutter4.Content = D_ListDoubleDevicePLC_0_[26].ToString();

                            this.lbCoutterA1.Content = D_ListDoubleDevicePLC_0_[30].ToString();
                            this.lbCoutterA2.Content = D_ListDoubleDevicePLC_0_[32].ToString();
                            this.lbCoutterA3.Content = D_ListDoubleDevicePLC_0_[34].ToString();

                            this.lbCoutterNG1.Content = D_ListDoubleDevicePLC_0_[60].ToString();
                            this.lbCoutterNG2.Content = D_ListDoubleDevicePLC_0_[62].ToString();


                            this.lbTimer1.Content = (D_ListDoubleDevicePLC_0_[50] / 1000.0).ToString();


                        });
                    }

                }

            }
            catch (Exception ex)
            {
                //this.logger.Create("Update Status PLC Error: " + ex.Message, LogLevel.Error);
            }


        }
        #endregion


        #region Update LOTDATA
        private void BtLotOut_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want to LotOut ?")) return;
            this.LotOut();
        }
        private void BtLotIn_Click(object sender, RoutedEventArgs e)
        {
            /// Create Wnd Login
            var setting = UiManager.managerSetting.assignSystem;
            if (setting.LoginApp == true && setting.LoginMes == true)
            {
                var Result = ManagerLogin.DoCheck();
                if (Result == 0)
                {
                    return;
                }
            }
            UserManager.isLogOn = 0;


            //
            WndLotin wndLot = new WndLotin();
            var newLot = wndLot.DoSettings(Window.GetWindow(this), this.lotInData);
            if (newLot == null)
            {
                return;
            }
            else
            {

                /// Thực Hiện Check Lotin Param tại đây 
                lotInData = newLot;
                UpdateInformationLOTIN(lotInData);
            }
        }
        private void LotOut()
        {
            lotInData = UiManager.appSetting.LotinData;

            lotInData.OKCount = 0;
            lotInData.NGCount = 0;
            lotInData.TotalCount = 0;
            lotInData.Yield = 0;
            lotInData.LotQty = 0;
            lotInData.DeviceId = "";
            lotInData.LotId = "";

            //this.lbLotID.Content = lotInData.LotId?.ToString();
            //this.lbConfig.Content = lotInData.DeviceId?.ToString();
            //this.lbQty.Content = lotInData.LotQty.ToString();
            //this.lbOkCount.Content = lotInData.OKCount.ToString();
            //this.lbNgCount.Content = lotInData.NGCount.ToString();
            //this.lbTotalCount.Content = lotInData.TotalCount.ToString();
            //this.lbYield.Content = lotInData.Yield.ToString();
            UiManager.appSetting.LotinData = lotInData;
            UiManager.SaveAppSetting();
        }
        private void UpdateOK_NG(int OK, int NG)
        {
            Dispatcher.Invoke(() =>
            {

                lotInData.OKCount = lotInData.OKCount + OK;
                lotInData.NGCount = lotInData.NGCount + NG;
                lotInData.TotalCount = lotInData.TotalCount + OK + NG;
                lotInData.Yield = Math.Round((double)lotInData.OKCount / lotInData.TotalCount * 100, 2);

                //this.lbQty.Content = lotInData.LotQty.ToString();
                //this.lbOkCount.Content = lotInData.OKCount.ToString();
                //this.lbNgCount.Content = lotInData.NGCount.ToString();
                //this.lbTotalCount.Content = lotInData.TotalCount.ToString();
                //this.lbYield.Content = lotInData.Yield.ToString();
            });
        }
        private void UpdateInformationLOTIN(LotInData lotin)
        {

            lotin.OKCount = 0;
            lotin.NGCount = 0;
            lotin.TotalCount = 0;
            lotin.Yield = 0;

            this.lbLotID.Content = lotin.LotId?.ToString();
            this.lbConfig.Content = lotin.DeviceId?.ToString();

            //this.lbQty.Content = lotin.LotQty.ToString();
            //this.lbOkCount.Content = lotin.OKCount.ToString();
            //this.lbNgCount.Content = lotin.NGCount.ToString();
            //this.lbTotalCount.Content = lotin.TotalCount.ToString();
            //this.lbYield.Content = lotin.Yield.ToString();
            UiManager.appSetting.LotinData = lotin;
            UiManager.SaveAppSetting();
        }
        private void LoadLotData()
        {
            lotInData = UiManager.appSetting.LotinData;

            //this.lbLotID.Content = lotInData.LotId?.ToString();
            //this.lbConfig.Content = lotInData.DeviceId?.ToString();
            //this.lbQty.Content = lotInData.LotQty.ToString();
            //this.lbOkCount.Content = lotInData.OKCount.ToString();
            //this.lbNgCount.Content = lotInData.NGCount.ToString();
            //this.lbTotalCount.Content = lotInData.TotalCount.ToString();
            //this.lbYield.Content = lotInData.Yield.ToString();
        }
        #endregion


        #region ALARM 
        private void Playsound()
        {
            try
            {
                string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sound", "Minion.m4a");
                bool flag = File.Exists(text);
                if (flag)
                {
                    this.mediaPlayer.Open(new Uri(text));
                    this.mediaPlayer.Play();
                    this.isLooping = true;
                }
            }
            catch (Exception)
            {
            }
        }
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            bool flag = this.isLooping;
            if (flag)
            {
                this.mediaPlayer.Position = TimeSpan.Zero;
                this.mediaPlayer.Play();
            }
        }
        private void StopSound()
        {
            this.mediaPlayer.Stop();
        }


        #region Add Error MES
        private void AddErrorMES(string Messenger, string Solution)
        {

            WndAlarmMES ShowAlarm = new WndAlarmMES();
            ShowAlarm.Messenger(Messenger, Solution);
            addLog($"Allarm{Messenger} {Solution}");

        }
        #endregion
        #region ALARM LOG


        private List<int> errorCodes;
        List<DateTime> timeerror = new List<DateTime>();
        private void InitializeErrorCodes()
        {
            errorCodes = new List<int>();
            timeerror = new List<DateTime>();

        }
        private void AddError(short errorCode)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (errorCode == 0)
                {
                    return;
                }

                if (errorCodes.Contains(errorCode))
                {
                    return;
                }

                else if (errorCodes.Count >= 31)
                {
                    errorCodes.Add(1);
                    return;
                }

                //Thêm lỗi vào SQL
                if (errorCode <= 100)
                {
                    string mes = AlarmInfo.getMessage(errorCode);
                    string sol = AlarmList.GetSolution(errorCode);
                    var alarm = new AlarmInfo(errorCode, mes, sol);
                    DbWrite.createAlarm(alarm);
                }
                else
                {
                    string mes = AlarmList.GetMes(errorCode);
                    string sol = AlarmList.GetSolution(errorCode);
                    var alarm = new AlarmInfo(errorCode, mes, sol);
                    DbWrite.createAlarm(alarm);
                }
                errorCodes.Add(errorCode);
                timeerror.Add(DateTime.Now);

                //GirdInfor.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
                //GirdInfor.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

                for (int i = 0; i < errorCodes.Count; i++)

                {
                    int code = errorCodes[i];
                    switch (i)
                    {
                        case 0: this.DisplayAlarm(1, code); break;
                        case 1: this.DisplayAlarm(2, code); break;
                        case 2: this.DisplayAlarm(3, code); break;
                        case 3: this.DisplayAlarm(4, code); break;
                        case 4: this.DisplayAlarm(5, code); break;
                        case 5: this.DisplayAlarm(6, code); break;
                        case 6: this.DisplayAlarm(7, code); break;
                        case 7: this.DisplayAlarm(8, code); break;
                        case 8: this.DisplayAlarm(9, code); break;
                        case 9: this.DisplayAlarm(10, code); break;
                        case 10: this.DisplayAlarm(11, code); break;
                        case 11: this.DisplayAlarm(12, code); break;
                        case 12: this.DisplayAlarm(13, code); break;
                        case 13: this.DisplayAlarm(14, code); break;
                        case 14: this.DisplayAlarm(15, code); break;
                        case 15: this.DisplayAlarm(16, code); break;
                        case 16: this.DisplayAlarm(17, code); break;
                        case 17: this.DisplayAlarm(18, code); break;
                        case 18: this.DisplayAlarm(19, code); break;
                        case 19: this.DisplayAlarm(20, code); break;
                        case 20: this.DisplayAlarm(21, code); break;
                        case 21: this.DisplayAlarm(22, code); break;
                        case 22: this.DisplayAlarm(23, code); break;
                        case 23: this.DisplayAlarm(24, code); break;
                        case 24: this.DisplayAlarm(25, code); break;
                        case 25: this.DisplayAlarm(26, code); break;
                        case 26: this.DisplayAlarm(27, code); break;
                        case 27: this.DisplayAlarm(28, code); break;
                        case 28: this.DisplayAlarm(29, code); break;
                        case 29: this.DisplayAlarm(30, code); break;

                        default:
                            break;
                    }
                }
                if (!isAlarmWindowOpen)
                {
                    this.ShowAlarm();
                }

                this.Number_Alarm();
                this.Playsound();
            });


        }
        private void Number_Alarm()
        {
            int NumberAlarm = errorCodes.Count;
            this.CbShow.Content = NumberAlarm > 0 ? $"Errors : {NumberAlarm}" : "Not Show";
        }
        private void AlarmCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isAlarmWindowOpen = true;
        }
        private void AlarmCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isAlarmWindowOpen = false;
        }
        private void WriteLog_Checked(object sender, RoutedEventArgs e)
        {
            isWriteLog = true;
        }
        private void WriteLog_Unchecked(object sender, RoutedEventArgs e)
        {
            isWriteLog = false;
        }
        private bool isAlarmWindowOpen = false;
        private bool isWriteLog = false;
        private void ShowAlarm()
        {
            WndAlarm wndAlarm = new WndAlarm();
            wndAlarm.UpdateErrorList(errorCodes);
            wndAlarm.UpdateTimeList(timeerror);
            if (!isAlarmWindowOpen)
            {
                wndAlarm.Show();
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (errorCodes.Count >= 1)
            {
                WndAlarm wndAlarm = new WndAlarm();
                wndAlarm.UpdateErrorList(errorCodes);
                wndAlarm.UpdateTimeList(timeerror);
                wndAlarm.Show();
            }

        }
        public void ClearError()
        {
            timeerror.Clear();
            errorCodes.Clear();
            Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 1; i <= 30; i++)
                {
                    var label = (Label)FindName("lbMes" + i);
                    label.Content = "";
                    label.Background = Brushes.Black;
                }
            }));

            WndAlarm wndAlarm = new WndAlarm();
            wndAlarm.UpdateErrorList(errorCodes);
            wndAlarm.UpdateTimeList(timeerror);
            this.Number_Alarm();

            GirdInfor.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            GirdInfor.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);

            if (WndAlarmMES.Instance != null)
            {
                WndAlarmMES.Instance.CloseAlarm();
            }
            this.StopSound();

        }
        private void DisplayAlarm(int index, int code)
        {
            try
            {
                if (code <= 100)
                {
                    Label label = (Label)FindName($"lbMes{index}");
                    if (label != null)
                    {
                        string mes = AlarmInfo.getMessage(code);
                        this.Dispatcher.Invoke(() =>
                        {
                            DateTime currentTime = DateTime.Now;
                            string currentTimeString = currentTime.ToString();
                            string newContent = currentTime.ToString() + " : " + mes;

                            label.Content = newContent;
                            label.Background = Brushes.Red;
                            //label.FontWeight = FontWeights.ExtraBold;
                            //label.Foreground = Brushes.Black;
                        });
                    }
                }
                else
                {
                    Label label = (Label)FindName($"lbMes{index}");
                    if (label != null)
                    {
                        string mes = AlarmList.GetMes(code);
                        this.Dispatcher.Invoke(() =>
                        {
                            string currentTime = DateTime.Now.ToString("HH:mm:ss");
                            string currentTimeString = currentTime.ToString();
                            string newContent = currentTime.ToString() + " : " + mes;

                            label.Content = newContent;
                            label.Background = Brushes.Red;
                            //label.FontWeight = FontWeights.ExtraBold;
                            //label.Foreground = Brushes.Black;
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Create($"DisplayAlarm PgMain: {ex.Message}", LogLevel.Error);
            }
        }
        #endregion
        #region AddLog
        public Boolean uiLogEnable { get; set; } = true;
        private String lastLog = "";
        private int gLogIndex;
        private bool autoScrollMode = true;
        private void addLog(String log)
        {
            try
            {
                if (log != null && !log.Equals(lastLog))
                {
                    lastLog = log;

                    if (isWriteLog)
                    {
                        logger.Create("addLog:" + log, LogLevel.Information);
                    }


                    // UI log:
                    if (true)
                    {
                        logEntry x = new logEntry()
                        {
                            logIndex = gLogIndex++,
                            logTime = DateTime.Now.ToString("HH:mm:ss.ff"),
                            logMessage = log,
                        };
                        this.Dispatcher.Invoke(() =>
                        {
                            compositeViewModel.LogEntries.Add(x);

                            // Nếu số lượng log vượt quá 1000
                            if (compositeViewModel.LogEntries.Count > 300)
                            {
                                // Giữ lại 50 dòng gần nhất
                                var recentLogs = compositeViewModel.LogEntries.Skip(compositeViewModel.LogEntries.Count - 100).ToList();
                                compositeViewModel.LogEntries.Clear();
                                foreach (var item in recentLogs)
                                    compositeViewModel.LogEntries.Add(item);
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("addLog error:" + ex.Message,LogLevel.Error);
            }
        }
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                if (e.Source.GetType().Equals(typeof(ScrollViewer)))
                {
                    ScrollViewer sv = (ScrollViewer)e.Source;

                    if (sv != null)
                    {
                        // User scroll event : set or unset autoscroll mode
                        if (e.ExtentHeightChange == 0)
                        {   // Content unchanged : user scroll event
                            if (sv.VerticalOffset == sv.ScrollableHeight)
                            {   // Scroll bar is in bottom -> Set autoscroll mode
                                autoScrollMode = true;
                            }
                            else
                            {   // Scroll bar isn't in bottom -> Unset autoscroll mode
                                autoScrollMode = false;
                            }
                        }

                        // Content scroll event : autoscroll eventually
                        if (autoScrollMode && e.ExtentHeightChange != 0)
                        {   // Content changed and autoscroll mode set -> Autoscroll
                            sv.ScrollToVerticalOffset(sv.ExtentHeight);
                        }
                    }
                }
            }
            catch
            {
            }
        }
        #endregion
        #region Show Error Machine
        #region StatusMachine
        private void AddStatus(int code)
        {
            if (code == 0)
            {
                // Nếu là số 0, không làm gì cả và thoát khỏi phương thức
                return;
            }
            string mes = StatusMachine.GetMes(code);
            Dispatcher.Invoke(() =>
            {
                this.lblStatust.Content = mes;
            });
        }
        #endregion
    }
    public class CompositeViewModel
    {
        public ObservableCollection<logEntry> LogEntries { get; set; } = new ObservableCollection<logEntry>();
    }
    public class logEntry : PropertyChangedBase
    {
        public int logIndex { get; set; }
        public String logTime { get; set; }
        public string logMessage { get; set; }
    }
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        private static MyLogger Logger = new MyLogger("LogEntry");
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
                }));
            }
            catch (Exception ex)
            {
                Logger.Create(String.Format("Binding Property Of Logger Error: " + ex.Message) ,LogLevel.Error);
            }
        }
    }
    #endregion
    #endregion
}
