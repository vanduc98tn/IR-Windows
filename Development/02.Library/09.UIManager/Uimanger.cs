using ITM_Semiconductor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Development
{
    public enum PAGE_ID
    {
        // Page Main
        PAGE_MAIN = 0,

        // Page Menu
        PAGE_MENU,

        PAGE_TEACHING_MENU_01,
        PAGE_TEACHING_MENU_02,
        PAGE_TEACHING_MENU_03,
        PAGE_TEACHING_MENU_04,
        PAGE_TEACHING_MENU_05,
        PAGE_TEACHING_MENU_06,
        PAGE_TEACHING_MENU_07,
        PAGE_TEACHING_MENU_08,
        PAGE_TEACHING_MENU_09,


        PAGE_MECHANICAL_MENU_PLC,
        PAGE_MECHANICAL_MENU_00,
        PAGE_MECHANICAL_MENU_01,
        PAGE_MECHANICAL_MENU_02,
        PAGE_MECHANICAL_MENU_03,

        PAGE_SYSTEM_MENU_01,
        PAGE_SYSTEM_MENU_02,

        PAGE_MANUAL_OPERATION_01,
        PAGE_MANUAL_OPERATION_02,
        PAGE_MANUAL_OPERATION_03,
        PAGE_MANUAL_OPERATION_04,
        PAGE_MANUAL_OPERATION_05,
        PAGE_MANUAL_OPERATION_06,

        PAGE_STATUS_MENU,

        PAGE_MODEL,

        PAGE_SUPER_USER_MENU_01,
        PAGE_SUPER_USER_MENU_02,
        PAGE_SUPER_USER_MENU_03,
        PAGE_SUPER_USER_MENU_04,
        PAGE_SUPER_USER_MENU_05,
        PAGE_SUPER_USER_MENU_06,
        PAGE_SUPER_USER_MENU_07,
        PAGE_SUPER_USER_MENU_08,
        PAGE_SUPER_USER_MENU_09,

        PAGE_ASSIGN_MENU,
        PAGE_ASSIGN_ALARM_SETTING,

        // Page I/O
        PAGE_IO_INPUT,
        PAGE_IO_OUTPUT,

        // Page Alarm 
        PAGE_ALARM,

    }


    class UiManager
    {
        private static UiManager instance = new UiManager();
        public static UiManager Instance => instance;
        public MainWindow wndMain;
        public static WndCheckUpdate WndCheckver;
        private static MyLogger logger = new MyLogger("UiManager");
        public static Hashtable pageTable = new Hashtable();
        public static AppSetting appSetting = new AppSetting();
        public static ManagerSetting managerSetting = new ManagerSetting();



        public SelectDevice PLC;

       

        public TesterCOM TesterCOM;
        private byte[] receivedDataOld = null;
        private List<bool> M_ListBitPLC20000_20900 = new List<bool>();
        protected NotifyEvenTester notifyEvenTester;
        private bool isUpdate = false;

        #region Sử dụng Use Đăng Nhập MES
        public static string UserNameLoginMesOP_ME { get; set; }
        public static string CodeUserLoginMesOP_ME { get; set; }
        #endregion

        public void Startup()
        {
            logger.Create("Startup:",LogLevel.Information);
            try
            {
                // Create Database if not existed
                Dba.createDatabaseIfNotExisted();

                // Create Database imageAlarm if not existed
                SQLimageAlarm.createDatabaseIfNotExisted();

                // Initialize Page in Project
                initPageTable();

                // Load Settings
                LoadAppSetting();

                // Load ManagerSetting
                LoadManagerSetting();

                // Load Excel file for alarms
                AlarmList.LoadAlarm();

                // Load Excel file for status
                StatusMachine.LoadStatus();

                // Load MainWindow
                wndMain = new MainWindow();

                // Create Main window:
                wndMain.frmMainContent.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                wndMain.Show();

                // Creatr Wnd CheckUpdate
               if( managerSetting.assignSystem.AutoCheckUpdate)
                {
                    WndCheckver = new WndCheckUpdate();
                    WndCheckver.Show();
                }    
               

                // LoadPage
                this.SwitchPage(PAGE_ID.PAGE_MAIN);

               

                // Connect PLC 
                this.ConncetPLC();

                //Open Server MES Vision
                //ConnectMesVision();

                //Connect Tester COM
                this.ConnectTester();

                this.LoadNotifyTester();




                logger.Create("Uimanager Program Start Up",LogLevel.Information);
            }
            catch (Exception ex)
            {
                logger.Create("Startup error:" + ex.Message,LogLevel.Error);
            }



        }


        private static void initPageTable()
        {

            pageTable.Add(PAGE_ID.PAGE_MAIN, new PgMain());
            pageTable.Add(PAGE_ID.PAGE_ALARM, new PgAlarm());
            pageTable.Add(PAGE_ID.PAGE_IO_INPUT, new PgInput());
            pageTable.Add(PAGE_ID.PAGE_IO_OUTPUT, new PgOutput());
            pageTable.Add(PAGE_ID.PAGE_MENU, new PgMenu());

            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_01, new PgTeachingMenu01());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_02, new PgTeachingMenu02());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_03, new PgTeachingMenu03());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_04, new PgTeachingMenu04());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_05, new PgTeachingMenu05());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_06, new PgTeachingMenu06());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_07, new PgTeachingMenu07());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_08, new PgTeachingMenu08());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_09, new PgTeachingMenu09());

            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU_PLC, new PgMechanicalMenuPLC());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU_00, new PgMechanicalMenu00());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU_01, new PgMechanicalMenu01());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU_02, new PgMechanicalMenu02());

            pageTable.Add(PAGE_ID.PAGE_SYSTEM_MENU_01, new PgSystemMenu01());
            pageTable.Add(PAGE_ID.PAGE_SYSTEM_MENU_02, new PgSystemMenu02());

            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION_01, new PgManual01());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION_02, new PgManual02());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION_03, new PgManual03());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION_04, new PgManual04());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION_05, new PgManual05());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION_06, new PgManual06());

            pageTable.Add(PAGE_ID.PAGE_STATUS_MENU, new PgStatusMenu());

            pageTable.Add(PAGE_ID.PAGE_MODEL, new PgModel());

            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_01, new PgSuperUserMenu01());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_02, new PgSuperUserMenu02());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_03, new PgSuperUserMenu03());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_04, new PgSuperUserMenu04());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_05, new PgSuperUserMenu05());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_06, new PgSuperUserMenu06());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_07, new PgSuperUserMenu07());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_08, new PgSuperUserMenu08());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_09, new PgSuperUserMenu09());

            pageTable.Add(PAGE_ID.PAGE_ASSIGN_MENU, new PgAssignMenu());
            pageTable.Add(PAGE_ID.PAGE_ASSIGN_ALARM_SETTING, new PgAssignAlarmSetting());

        }
        public void SwitchPage(PAGE_ID pgID)     // ham de thay dd
        {
            if (pageTable.ContainsKey(pgID))
            {
                var pg = (System.Windows.Controls.Page)pageTable[pgID];
                wndMain.UpdateMainContent(pg);
                wndMain.btMenu.ClearValue(Button.BackgroundProperty);
                wndMain.btMain.ClearValue(Button.BackgroundProperty);
                wndMain.btIO.ClearValue(Button.BackgroundProperty);
                wndMain.btLastJam.ClearValue(Button.BackgroundProperty);

                if (pgID == PAGE_ID.PAGE_MAIN)
                {
                    wndMain.btMain.Background = Brushes.Orange;
                }
                if ((pgID >= PAGE_ID.PAGE_MENU) & (pgID <= PAGE_ID.PAGE_ASSIGN_MENU))
                {
                    wndMain.btMenu.Background = Brushes.Orange;
                }
                if ((pgID >= PAGE_ID.PAGE_IO_INPUT) & (pgID <= PAGE_ID.PAGE_IO_OUTPUT))
                {
                    wndMain.btIO.Background = Brushes.Orange;
                }
                if (pgID == PAGE_ID.PAGE_ALARM)
                {
                    wndMain.btLastJam.Background = Brushes.Orange;
                }

            }
        }
        public static void SaveAppSetting()            ///  LUU THONG SO SETTING
        {
            try
            {
                if (appSetting == null)
                {
                    appSetting = new AppSetting();
                }
                string str = appSetting.TOJSON();
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSetting.SETTING_FILE_NAME);   // duong dan den file exe de mo ung dung
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path))
                {
                    streamWriter.WriteLine(str);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Create("SaveAppSetting" + ex.Message,LogLevel.Error);
            }

        }
        public static void LoadAppSetting()           // LOAD DU LIEU SETTING
        {

            String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), AppSetting.SETTING_FILE_NAME);
            if (File.Exists(filePath))
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    appSetting = AppSetting.FromJSON(file.ReadToEnd());
                }
            }
            else
            {
                appSetting = new AppSetting();
            }
        }

        public static void SaveManagerSetting()            ///  LUU THONG SO SETTING
        {
            try
            {
                if (managerSetting == null)
                {
                    managerSetting = new ManagerSetting();
                }
                string str = managerSetting.TOJSON();
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ManagerSetting.SETTING_FILE_NAME);  
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path))
                {
                    streamWriter.WriteLine(str);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Create("SaveAppSetting" + ex.Message, LogLevel.Error);
            }

        }
        public static void LoadManagerSetting()           // LOAD DU LIEU SETTING
        {

            String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), ManagerSetting.SETTING_FILE_NAME);
            if (File.Exists(filePath))
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    managerSetting = ManagerSetting.FromJSON(file.ReadToEnd());
                }
            }
            else
            {
                managerSetting = new ManagerSetting();
            }
        }


        #region Conncet PLC
        public void ConncetPLC()
        {
            PLC = new SelectDevice(UiManager.appSetting.selectDevice, UiManager.appSetting.settingDevice);
            PLC.device.Open();
            Task.Run(() => { this.PLC.MonitorDevice(); });

        }
        public void DisconncetPLC()
        {
            if(PLC != null)
            {
                PLC.device.Close();
            }    
           
        }
        #endregion

   


        #region Tester COM
        private void LoadNotifyTester()
        {
            this.notifyEvenTester = SystemsManager.Instance.NotifyEvenTester;
        }
        public void ConnectTester()
        {
            TesterCOM = new TesterCOM(UiManager.appSetting.settingDevice.COMTestter);
            TesterCOM.Open();
            TesterCOM.DataReceived += TesterCOM_DataReceived;
            this.isUpdate = true;
            ThreadUpDatePLC();
        }
        public void DisconnectTester()
        {
            if(TesterCOM.isOpen())
            {
                TesterCOM.Close();
                isUpdate = false;
            }    
        }
        private string BytesToHexString(IEnumerable<byte> data)
        {
            return string.Join(" ", data.Select(b => $"0x{b:X2}"));
        }
        private void TesterCOM_DataReceived(object sender, List<byte> data)
        {
           
            byte[] receivedData = data.ToArray();
        

            //if (receivedDataOld != null && receivedData.SequenceEqual(receivedDataOld))
            //{
            //    return;
            //}    
            receivedDataOld = receivedData;

          



            byte[] OPEN01OUT02OUT = new byte[] { 0x02, 0x32, 0x32, 0x03 ,0x0D , 0x0A };
            byte[] OPEN01IN02OUT = new byte[] { 0x02, 0x32, 0x33, 0x03, 0x0D, 0x0A };
            byte[] OPEN01OUT02IN = new byte[] { 0x02, 0x32, 0x34, 0x03, 0x0D, 0x0A };



            //재시도 전송 Open (0x33)
            byte[] OpenAgain = new byte[] { 0x02, 0x33, 0x33, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03, 0x0D, 0x0A };


            //검사결과 전송 (0x34)
            byte[] SendResult = new byte[] { 0x02, 0x34, 0x34, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03, 0x0D, 0x0A };

            #region Send Lotin
            // Tester Send Resquest LOTID > PC  // Nhận dc Lotin Auto tự gửi lại . có ghi log r anh
            //Tester send Request LOTIN
            byte[] Lotin = new byte[] { 0x02, 0x39, 0x39, 0x03, 0x0D, 0x0A };

            string hexString = BitConverter.ToString(receivedData);
            notifyEvenTester.NotifyDataTester($"Data Received : {hexString}");

            if (Lotin[0] == receivedData[0] && Lotin[1] == receivedData[1] && Lotin[2] == receivedData[2] && Lotin[3] == receivedData[3])
            {
                string hexData = BytesToHexString(receivedData);
                logger.Create01($"Data Recceived :{hexData} : Request Lotin", LogLevel.Information);
                this.SentLotinToTester();
            }
            #endregion


            #region Open or Close
            // Tester OPEN  or CLOSE ----------  TYPE#01 OUT-TYPE#02 OUT
            if (OPEN01OUT02OUT[0] == receivedData[0] && OPEN01OUT02OUT[1] == receivedData[1] && OPEN01OUT02OUT[2] == receivedData[2] && OPEN01OUT02OUT[3] == receivedData[3])
            {
                if (OPEN01OUT02OUT.Length != receivedData.Length)
                {
                    return;
                }
                string hexData = BytesToHexString(receivedData);
                logger.Create01($"Data Recceived :{hexData} :  TYPE#01 OUT-TYPE#02 OUT", LogLevel.Information);
                this.SendType01Out02OutTester();
               
            }

            // Tester OPEN  or CLOSE ----------  TYPE#01 IN-TYPE#02 OUT
            if (OPEN01IN02OUT[0] == receivedData[0] && OPEN01IN02OUT[1] == receivedData[1] && OPEN01IN02OUT[2] == receivedData[2] && OPEN01IN02OUT[3] == receivedData[3])
            {
                if (OPEN01IN02OUT.Length != receivedData.Length)
                {
                    return;
                }
                string hexData = BytesToHexString(receivedData);
                logger.Create01($"Data Recceived :{hexData} :  TYPE#01 IN-TYPE#02 OUT", LogLevel.Information);
                this.SendType01In02OutTester();
               

            }

            // Tester OPEN  or CLOSE ----------  TYPE#01 OUT-TYPE#02 IN
            if (OPEN01OUT02IN[0] == receivedData[0] && OPEN01OUT02IN[1] == receivedData[1] && OPEN01OUT02IN[2] == receivedData[2] && OPEN01OUT02IN[3] == receivedData[3])
            {
                if (OPEN01OUT02IN.Length != receivedData.Length)
                {
                    return;
                }
                string hexData = BytesToHexString(receivedData);
                logger.Create01($"Data Recceived :{hexData} :  TYPE#01 OUT-TYPE#02 IN", LogLevel.Information);
                this.SendType01Out02InTester();
               

            }
            #endregion


            #region Open Again 
            // Tester OPEN AGAIN
            if (OpenAgain[0] == receivedData[0] && OpenAgain[1] == receivedData[1] && OpenAgain[2] == receivedData[2] && OpenAgain[9] == receivedData[9])
            {
                bool CH1_2 = false;
                bool CH3_4 = false;
                bool CH5_6 = false;
                bool CH7_8 = false;
                bool CH9_10 = false;
                bool CH11_12 = false;
               
                if (OpenAgain.Length != receivedData.Length)
                {
                    return;
                }
                // CH1_2
                if (OpenAgain[3] == receivedData[3])
                {
                     CH1_2 = false;
                }
                else 
                {
                    CH1_2 = true;
                }

                // CH3_4
                if (OpenAgain[4] == receivedData[4])
                {
                    CH3_4 = false;
                }
                else
                {
                    CH3_4 = true;
                }


                // CH5_6
                if (OpenAgain[5] == receivedData[5])
                {
                    CH5_6 = false;
                }
                else
                {
                    CH5_6 = true;
                }

                // CH7_8
                if (OpenAgain[6] == receivedData[6])
                {
                    CH7_8 = false;
                }
                else
                {
                    CH7_8 = true;
                }

                // CH9_10
                if (OpenAgain[7] == receivedData[7])
                {
                    CH9_10 = false;
                }
                else
                {
                    CH9_10 = true;
                }

                // CH11_12
                if (OpenAgain[8] == receivedData[8])
                {
                    CH11_12 = false;
                }
                else
                {
                    CH11_12 = true;
                }
                string hexData = BytesToHexString(receivedData);
                logger.Create01($"Data Recceived :{hexData} :  Tester OPEN AGAIN", LogLevel.Information);
                this.OpenAgainTester( CH1_2,CH3_4,CH5_6,CH7_8,CH9_10,CH11_12);
               

            }
            #endregion


            #region Result 
            // Tester OPEN AGAIN
            if (SendResult[0] == receivedData[0] && SendResult[1] == receivedData[1] && SendResult[2] == receivedData[2] && SendResult[15] == receivedData[15])
            {
                if (SendResult.Length != receivedData.Length)
                {
                    return;
                }
                this.ResultTester(receivedData[3],
                                 receivedData[4],
                                 receivedData[5], 
                                 receivedData[6], 
                                 receivedData[7],
                                 receivedData[8], 
                                 receivedData[9],
                                 receivedData[10], 
                                 receivedData[11], 
                                 receivedData[12],
                                 receivedData[13], 
                                 receivedData[14]);
                string hexData = BytesToHexString(receivedData);
                logger.Create01($"Data Recceived :{hexData} :  Result ", LogLevel.Information);
            }
            #endregion

        }
        // Auto Send Lotin
        private void SentLotinToTester()
        {
           
            string LotID = appSetting.LotinData.LotId;
            logger.Create01($"Send Lotin :{LotID}", LogLevel.Information);
            notifyEvenTester.NotifyDataTester($"Send Lotin to Tester :{LotID}");
            TesterCOM.SendLotin(LotID);
        }
        private void SendType01Out02OutTester() 
        {
            if (PLC.device.isOpen())
            {
                PLC.device.WriteBit(DeviceCode.M, 20460, true);
            }
            else
            {
                logger.Create01("Error Send Type 01Out 02Out Tester :PLC Disconnect", LogLevel.Error);
            }
            notifyEvenTester.NotifyDataTester($"SendType01Out02OutTester : PC > PLC 20460 = TRUE");
            logger.Create01($"PC > PLC 20460 = TRUE ", LogLevel.Information);
            //TesterCOM.Send01OutAnd02OutTester(); /// nhận dcm20442 se gửi dong này
        }
        private void SendType01In02OutTester()
        {
            if (PLC.device.isOpen())
            {
                PLC.device.WriteBit(DeviceCode.M, 20461, true);
            }
            else
            {
                logger.Create01("Error Send Type 01In 02Out Tester :PLC Disconnect", LogLevel.Error);
            }
            notifyEvenTester.NotifyDataTester($"SendType01In02OutTester : PC > PLC 20461 = TRUE");
            logger.Create01($"PC > PLC 20461 = TRUE ", LogLevel.Information);
            // TesterCOM.Send01InAnd02OutTester();
        }
        private void SendType01Out02InTester()
        {
            if (PLC.device.isOpen())
            {
                PLC.device.WriteBit(DeviceCode.M, 20462, true);
            }
            else
            {
                logger.Create01("Error Send Type 01Out 02In Tester :PLC Disconnect", LogLevel.Error);
            }
            notifyEvenTester.NotifyDataTester($"SendType01Out02InTester : PC > PLC 20462 = TRUE");
            logger.Create01($"PC > PLC 20462 = TRUE ", LogLevel.Information);
            // TesterCOM.Send01OutAnd02InTester();
        }
        private void OpenAgainTester( bool CH1_2, bool CH3_4, bool CH5_6, bool CH7_8, bool CH9_10, bool CH11_12)
        {
           
            if (PLC.device.isOpen())
            {
                if (PLC.device.WriteBit(DeviceCode.M, 20471, CH1_2))
                {
                    if (CH1_2)
                    {
                        
                        logger.Create01($"OpenAgainTester CH1_2 : Send Bit 20471 = TRUE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH1_2: Send Bit 20471 = TRUE");
                    }
                    else
                    {
                        logger.Create01($"OpenAgainTester : Send Bit 20471 = FALSE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH1_2: Send Bit 20471 = FALSE ");
                    }
                   
                }

                if (PLC.device.WriteBit(DeviceCode.M, 20472, CH3_4))
                {
                    if (CH3_4)
                    {
                        logger.Create01($"OpenAgainTester CH3_4 : Send Bit 20472 = TRUE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH3_4: Send Bit 20472 = TRUE");

                    }
                    else
                    {
                        logger.Create01($"OpenAgainTester CH3_4 : Send Bit 20472 = FALSE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH3_4: Send Bit 20472 = FALSE");
                    }
                   
                }


                if (PLC.device.WriteBit(DeviceCode.M, 20473, CH5_6))
                {
                    if (CH5_6)
                    {
                        logger.Create01($"OpenAgainTester CH5_6: Send Bit 20473 = TRUE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH5_6: Send Bit 20473 = TRUE");
                    }
                    else
                    {
                        logger.Create01($"OpenAgainTester CH5_6: Send Bit 20473 = FALSE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH5_6: Send Bit 20473 = FALSE");
                    }
                    
                }

                if (PLC.device.WriteBit(DeviceCode.M, 20474, CH7_8))
                {
                    if (CH7_8)
                    {
                        logger.Create01($"OpenAgainTester CH7_8: Send Bit 20474 = TRUE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH7_8: Send Bit 20474 = TRUE");
                    }
                    else
                    {
                        logger.Create01($"OpenAgainTester CH7_8: Send Bit 20474 = FALSE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH7_8: Send Bit 20474 = FALSE ");
                    }
                   
                }

                if (PLC.device.WriteBit(DeviceCode.M, 20475, CH9_10))
                {
                    if (CH9_10)
                    {
                        logger.Create01($"OpenAgainTester CH9_10: Send Bit 20475 = TRUE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH9_10: Send Bit 20475 = FALSE ");
                    }
                    else
                    {
                        logger.Create01($"OpenAgainTester CH9_10: Send Bit 20475 = FALSE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH9_10: Send Bit 20475 = FALSE ");
                    }
                   
                }

                if (PLC.device.WriteBit(DeviceCode.M, 20476, CH11_12))
                {
                    if (CH11_12)
                    {
                        logger.Create01($"OpenAgainTester CH11_12: Send Bit 20476 = TRUE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH11_12: Send Bit 20476 = TRUE ");
                    }
                    else
                    {
                        logger.Create01($"OpenAgainTester CH11_12: Send Bit 20476 = FALSE ", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("OpenAgainTester CH11_12: Send Bit 20476 = FALSE ");
                    }
                    
                }

                
                PLC.device.WriteBit(DeviceCode.M, 20470, true);
                logger.Create01($"OpenAgainTester : Send Bit 20470 = TRUE ", LogLevel.Information);
                notifyEvenTester.NotifyDataTester($" OpenAgainTester : Send Bit 20470 = TRUE");

                
                
            }
            else
            {
                logger.Create01("OpenAgainTester : Send  PLC Error : PLC check connect", LogLevel.Warning);
            }
           
            // TesterCOM.SendCheckAgain();


        }
        private void ResultTester(byte CH1_EN, byte CH2_EN, byte CH3_EN, byte CH4_EN, byte CH5_EN, byte CH6_EN, byte CH7_EN, byte CH8_EN, byte CH9_EN, byte CH10_EN, byte CH11_EN, byte CH12_EN)
        {
            int CodeErrorCH1 = 0;
            int CodeErrorCH2 = 0;
            int CodeErrorCH3 = 0;
            int CodeErrorCH4 = 0;
            int CodeErrorCH5 = 0;
            int CodeErrorCH6 = 0;
            int CodeErrorCH7 = 0;
            int CodeErrorCH8 = 0;
            int CodeErrorCH9 = 0;
            int CodeErrorCH10 = 0;
            int CodeErrorCH11 = 0;
            int CodeErrorCH12 = 0;
            


            byte ResultOK = 0X30;
            byte Error01 = 0x31;
            byte Error02 = 0x32;
            byte Error03 = 0x33;
            byte Error04 = 0x34;
            byte Error05 = 0x35;
            byte Error06 = 0x36;
            byte Error07 = 0x37;
            byte Error08 = 0x38;
            byte Error09 = 0x39;
           

            if (CH1_EN == ResultOK)
            {
                if(PLC.device.isOpen())
                {
                    if(!PLC.device.WriteBit(DeviceCode.M, 20481, true))
                    {
                        logger.Create01("ResultTester CH1_EN :Send Bit M20481 : Error .Check Connect PLC", LogLevel.Error);
                    }    
                    else
                    {
                        logger.Create01("ResultTester CH1_EN:  Send Bit M20481 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH1_EN: Send Bit M20481 = True");
                    }    
                }
                else
                {
                    logger.Create01("Send Bit M20481 : Error .Check Connect PLC", LogLevel.Error);
                }


            }
            else
            {
                if (CH1_EN == Error01)
                {
                    CodeErrorCH1 = 1;
                }
                if (CH1_EN == Error02)
                {
                    CodeErrorCH1 = 2;
                }
                if (CH1_EN == Error03)
                {
                    CodeErrorCH1 = 3;
                }
                if (CH1_EN == Error04)
                {
                    CodeErrorCH1 = 4;

                }
                if (CH1_EN == Error05)
                {
                    CodeErrorCH1 = 5;
                }
                if (CH1_EN == Error06)
                {
                    CodeErrorCH1 = 6;
                }
                if (CH1_EN == Error07)
                {
                    CodeErrorCH1 = 7;
                }
                if (CH1_EN == Error08)
                {
                    CodeErrorCH1 = 8;
                }
                if (CH1_EN == Error09)
                {
                    CodeErrorCH1 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20481, false))
                    {
                        logger.Create01("ResultTester CH1_EN :Send Bit M20481 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH1_EN:  Send Bit M20481 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH1_EN: Send Bit M20481 = False");
                    }    
                }
                else
                {
                    logger.Create01("Send Bit M20481 : Error .Check Connect PLC", LogLevel.Error);
                }
            }


            if (CH2_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20482, true))
                    {
                        logger.Create01("ResultTester CH2_EN :Send Bit M20482 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH2_EN:  Send Bit M20482 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH2_EN: Send Bit M20482 = True");
                    }    
                }
                else
                {
                    logger.Create01("Send Bit M20482 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH2_EN == Error01)
                {
                    CodeErrorCH2 = 1;
                }
                if (CH2_EN == Error02)
                {
                    CodeErrorCH2 = 2;
                }
                if (CH2_EN == Error03)
                {
                    CodeErrorCH2 = 3;
                }
                if (CH2_EN == Error04)
                {
                    CodeErrorCH2 = 4;

                }
                if (CH2_EN == Error05)
                {
                    CodeErrorCH2 = 5;
                }
                if (CH2_EN == Error06)
                {
                    CodeErrorCH2 = 6;
                }
                if (CH2_EN == Error07)
                {
                    CodeErrorCH2 = 7;
                }
                if (CH2_EN == Error08)
                {
                    CodeErrorCH2 = 8;
                }
                if (CH2_EN == Error09)
                {
                    CodeErrorCH2 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20482, false))
                    {
                        logger.Create01("ResultTester CH2_EN :Send Bit M20482 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH2_EN:  Send Bit M20482 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH2_EN: Send Bit M20482 = False");
                    }
                }
                else
                {
                    logger.Create01("ResultTester CH3_EN :Send Bit M20482 : Error .Check Connect PLC", LogLevel.Error);
                }

            }



            if (CH3_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20483, true))
                    {
                        logger.Create01("ResultTester CH3_EN :Send Bit M20483 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH3_EN:  Send Bit M20483 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH3_EN: Send Bit M20483 = True");
                    }
                }
                else
                {
                    logger.Create01("ResultTester CH3_EN :Send Bit M20483 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH3_EN == Error01)
                {
                    CodeErrorCH3 = 1;
                }
                if (CH3_EN == Error02)
                {
                    CodeErrorCH3 = 2;
                }
                if (CH3_EN == Error03)
                {
                    CodeErrorCH3 = 3;
                }
                if (CH3_EN == Error04)
                {
                    CodeErrorCH3 = 4;

                }
                if (CH3_EN == Error05)
                {
                    CodeErrorCH3 = 5;
                }
                if (CH3_EN == Error06)
                {
                    CodeErrorCH3 = 6;
                }
                if (CH3_EN == Error07)
                {
                    CodeErrorCH3 = 7;
                }
                if (CH3_EN == Error08)
                {
                    CodeErrorCH3 = 8;
                }
                if (CH3_EN == Error09)
                {
                    CodeErrorCH3 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20483, false))
                    {
                        logger.Create01("Send Bit M20483 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH3_EN:  Send Bit M20483 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH3_EN: Send Bit M20483 = False");
                    }

                }
                else
                {
                    logger.Create01("Send Bit M20483 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH4_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20484, true))
                    {
                        logger.Create01("Send Bit M20484 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH4_EN:  Send Bit M20484 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH4_EN: Send Bit M20484 = True");
                    }    
                }
                else
                {
                    logger.Create01("Send Bit M20484 : Error .Check Connect PLC", LogLevel.Error);
                }    
            }
            else
            {
                if (CH4_EN == Error01)
                {
                    CodeErrorCH4 = 1;
                }
                if (CH4_EN == Error02)
                {
                    CodeErrorCH4 = 2;
                }
                if (CH4_EN == Error03)
                {
                    CodeErrorCH4 = 3;
                }
                if (CH4_EN == Error04)
                {
                    CodeErrorCH4 = 4;

                }
                if (CH4_EN == Error05)
                {
                    CodeErrorCH4 = 5;
                }
                if (CH4_EN == Error06)
                {
                    CodeErrorCH4 = 6;
                }
                if (CH4_EN == Error07)
                {
                    CodeErrorCH4 = 7;
                }
                if (CH4_EN == Error08)
                {
                    CodeErrorCH4 = 8;
                }
                if (CH4_EN == Error09)
                {
                    CodeErrorCH4 = 9;
                }
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20484, false))
                    {
                        logger.Create01("Send Bit M20484 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH4_EN:  Send Bit M20484 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH5_EN: Send Bit M20484 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20484 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH5_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20485, true))
                    {
                        logger.Create01("Send Bit M20485 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH5_EN:  Send Bit M20485 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH5_EN: Send Bit M20485 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20485 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH5_EN == Error01)
                {
                    CodeErrorCH5 = 1;
                }
                if (CH5_EN == Error02)
                {
                    CodeErrorCH5 = 2;
                }
                if (CH5_EN == Error03)
                {
                    CodeErrorCH5 = 3;
                }
                if (CH5_EN == Error04)
                {
                    CodeErrorCH5 = 4;

                }
                if (CH5_EN == Error05)
                {
                    CodeErrorCH5 = 5;
                }
                if (CH5_EN == Error06)
                {
                    CodeErrorCH5 = 6;
                }
                if (CH5_EN == Error07)
                {
                    CodeErrorCH5 = 7;
                }
                if (CH5_EN == Error08)
                {
                    CodeErrorCH5 = 8;
                }
                if (CH5_EN == Error09)
                {
                    CodeErrorCH5 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20485, false))
                    {
                        logger.Create01("Send Bit M20485 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH5_EN:  Send Bit M20485 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH5_EN: Send Bit M20484 = False");
                    }
                }
                else
                {
                    logger.Create("Send Bit M20485 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH6_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20486, true))
                    {
                        logger.Create01("Send Bit M20486 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH6_EN:  Send Bit M20486 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH6_EN: Send Bit M20486 = True");
                    }
                }
                else
                {
                    logger.Create("Send Bit M20486 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH6_EN == Error01)
                {
                    CodeErrorCH6 = 1;
                }
                if (CH6_EN == Error02)
                {
                    CodeErrorCH6 = 2;
                }
                if (CH6_EN == Error03)
                {
                    CodeErrorCH6 = 3;
                }
                if (CH6_EN == Error04)
                {
                    CodeErrorCH6 = 4;

                }
                if (CH6_EN == Error05)
                {
                    CodeErrorCH6 = 5;
                }
                if (CH6_EN == Error06)
                {
                    CodeErrorCH6 = 6;
                }
                if (CH6_EN == Error07)
                {
                    CodeErrorCH6 = 7;
                }
                if (CH6_EN == Error08)
                {
                    CodeErrorCH6 = 8;
                }
                if (CH6_EN == Error09)
                {
                    CodeErrorCH6 = 9;
                }
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20486, false))
                    {
                        logger.Create01("Send Bit M20486 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH6_EN:  Send Bit M20486 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH6_EN: Send Bit M20486 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20486 : Error .Check Connect PLC", LogLevel.Error);
                }
            }


            if (CH7_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20487, true))
                    {
                        logger.Create01("Send Bit M20487 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH7_EN:  Send Bit M20487 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH7_EN: Send Bit M20487 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20487 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH7_EN == Error01)
                {
                    CodeErrorCH7 = 1;
                }
                if (CH7_EN == Error02)
                {
                    CodeErrorCH7 = 2;
                }
                if (CH7_EN == Error03)
                {
                    CodeErrorCH7 = 3;
                }
                if (CH7_EN == Error04)
                {
                    CodeErrorCH7 = 4;

                }
                if (CH7_EN == Error05)
                {
                    CodeErrorCH7 = 5;
                }
                if (CH7_EN == Error06)
                {
                    CodeErrorCH7 = 6;
                }
                if (CH7_EN == Error07)
                {
                    CodeErrorCH7 = 7;
                }
                if (CH7_EN == Error08)
                {
                    CodeErrorCH7 = 8;
                }
                if (CH7_EN == Error09)
                {
                    CodeErrorCH7 = 9;
                }
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20487, false))
                    {
                        logger.Create01("Send Bit M20487 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH7_EN:  Send Bit M20487 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH7_EN: Send Bit M20487 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20487 : Error .Check Connect PLC", LogLevel.Error);
                }
            }


            if (CH8_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20488, true))
                    {
                        logger.Create01("Send Bit M20488 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH8_EN:  Send Bit M20488 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH8_EN: Send Bit M20488 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20488 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH8_EN == Error01)
                {
                    CodeErrorCH8 = 1;
                }
                if (CH8_EN == Error02)
                {
                    CodeErrorCH8 = 2;
                }
                if (CH8_EN == Error03)
                {
                    CodeErrorCH8 = 3;
                }
                if (CH8_EN == Error04)
                {
                    CodeErrorCH8 = 4;

                }
                if (CH8_EN == Error05)
                {
                    CodeErrorCH8 = 5;
                }
                if (CH8_EN == Error06)
                {
                    CodeErrorCH8 = 6;
                }
                if (CH8_EN == Error07)
                {
                    CodeErrorCH8 = 7;
                }
                if (CH8_EN == Error08)
                {
                    CodeErrorCH8 = 8;
                }
                if (CH8_EN == Error09)
                {
                    CodeErrorCH8 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20488, false))
                    {
                        logger.Create01("Send Bit M20488 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH8_EN:  Send Bit M20488 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH8_EN: Send Bit M20488 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20488 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH9_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20489, true))
                    {
                        logger.Create01("Send Bit M20489 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH9_EN:  Send Bit M20489 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH9_EN: Send Bit M20489 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20489 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH9_EN == Error01)
                {
                    CodeErrorCH9 = 1;
                }
                if (CH9_EN == Error02)
                {
                    CodeErrorCH9 = 2;
                }
                if (CH9_EN == Error03)
                {
                    CodeErrorCH9 = 3;
                }
                if (CH9_EN == Error04)
                {
                    CodeErrorCH9 = 4;

                }
                if (CH9_EN == Error05)
                {
                    CodeErrorCH9 = 5;
                }
                if (CH9_EN == Error06)
                {
                    CodeErrorCH9 = 6;
                }
                if (CH9_EN == Error07)
                {
                    CodeErrorCH9 = 7;
                }
                if (CH9_EN == Error08)
                {
                    CodeErrorCH9 = 8;
                }
                if (CH9_EN == Error09)
                {
                    CodeErrorCH9 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20489, false))
                    {
                        logger.Create01("Send Bit M20489 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH9_EN:  Send Bit M20489 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH9_EN: Send Bit M20489 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20489 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH10_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20490, true))
                    {
                        logger.Create01("Send Bit M20490 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH10_EN: Send Bit M20490 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH10_EN: Send Bit M20490 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20490 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH10_EN == Error01)
                {
                    CodeErrorCH10 = 1;
                }
                if (CH10_EN == Error02)
                {
                    CodeErrorCH10 = 2;
                }
                if (CH10_EN == Error03)
                {
                    CodeErrorCH10 = 3;
                }
                if (CH10_EN == Error04)
                {
                    CodeErrorCH10 = 4;

                }
                if (CH10_EN == Error05)
                {
                    CodeErrorCH10 = 5;
                }
                if (CH10_EN == Error06)
                {
                    CodeErrorCH10 = 6;
                }
                if (CH10_EN == Error07)
                {
                    CodeErrorCH10 = 7;
                }
                if (CH10_EN == Error08)
                {
                    CodeErrorCH10 = 8;
                }
                if (CH10_EN == Error09)
                {
                    CodeErrorCH10 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20490, false))
                    {
                        logger.Create01("Send Bit M20490 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH10_EN: Send Bit M20490 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH10_EN: Send Bit M20490 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20490 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH11_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20491, true))
                    {
                        logger.Create01("Send Bit M20491 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH11_EN: Send Bit M20491 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH11_EN: Send Bit M20491 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20491 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH11_EN == Error01)
                {
                    CodeErrorCH11 = 1;
                }
                if (CH11_EN == Error02)
                {
                    CodeErrorCH11 = 2;
                }
                if (CH11_EN == Error03)
                {
                    CodeErrorCH11 = 3;
                }
                if (CH11_EN == Error04)
                {
                    CodeErrorCH11 = 4;

                }
                if (CH11_EN == Error05)
                {
                    CodeErrorCH11 = 5;
                }
                if (CH11_EN == Error06)
                {
                    CodeErrorCH11 = 6;
                }
                if (CH11_EN == Error07)
                {
                    CodeErrorCH11 = 7;
                }
                if (CH11_EN == Error08)
                {
                    CodeErrorCH11 = 8;
                }
                if (CH11_EN == Error09)
                {
                    CodeErrorCH11 = 9;
                }

                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20491, false))
                    {
                        logger.Create01("Send Bit M20491 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH11_EN: Send Bit M20491 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH11_EN: Send Bit M20491 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20491 : Error .Check Connect PLC", LogLevel.Error);
                }
            }



            if (CH12_EN == ResultOK)
            {
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20492, true))
                    {
                        logger.Create01("Send Bit M20492 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH12_EN: Send Bit M20492 = True", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH12_EN: Send Bit M20492 = True");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20492 : Error .Check Connect PLC", LogLevel.Error);
                }
            }
            else
            {
                if (CH12_EN == Error01)
                {
                    CodeErrorCH12 = 1;
                }
                if (CH12_EN == Error02)
                {
                    CodeErrorCH12 = 2;
                }
                if (CH12_EN == Error03)
                {
                    CodeErrorCH12 = 3;
                }
                if (CH12_EN == Error04)
                {
                    CodeErrorCH12 = 4;

                }
                if (CH12_EN == Error05)
                {
                    CodeErrorCH12 = 5;
                }
                if (CH12_EN == Error06)
                {
                    CodeErrorCH12 = 6;
                }
                if (CH12_EN == Error07)
                {
                    CodeErrorCH12 = 7;
                }
                if (CH12_EN == Error08)
                {
                    CodeErrorCH12 = 8;
                }
                if (CH12_EN == Error09)
                {
                    CodeErrorCH12 = 9;
                }
                if (PLC.device.isOpen())
                {
                    if (!PLC.device.WriteBit(DeviceCode.M, 20492, false))
                    {
                        logger.Create01("Send Bit M20492 : Error .Check Connect PLC", LogLevel.Error);
                    }
                    else
                    {
                        logger.Create01("ResultTester CH12_EN: Send Bit M20492 = False", LogLevel.Information);
                        notifyEvenTester.NotifyDataTester("ResultTester CH12_EN: Send Bit M20492 = False");
                    }
                }
                else
                {
                    logger.Create01("Send Bit M20492 : Error .Check Connect PLC", LogLevel.Error);
                }
            }


            if(!PLC.device.WriteBit(DeviceCode.M, 20480, true))
            {
                logger.Create01("Send Bit M20480 : Error .Check Connect PLC", LogLevel.Error);
            }
            else
            {
                logger.Create01("ResultTester : Send Bit M20480 = True", LogLevel.Information);
                notifyEvenTester.NotifyDataTester("ResultTester : Send Bit M20480 = True");
                
            }
            notifyEvenTester.NotifyResultTester(CodeErrorCH1, CodeErrorCH2, CodeErrorCH3, CodeErrorCH4, CodeErrorCH5, CodeErrorCH6, CodeErrorCH7, CodeErrorCH8
                                                ,CodeErrorCH9, CodeErrorCH10, CodeErrorCH11, CodeErrorCH12);
            //TesterCOM.SendResult();



            logger.Create01("SendResult : PC > TESTER Send OK", LogLevel.Information);
            notifyEvenTester.NotifyDataTester("SendResult :PC > TESTER Send OK");
            TesterCOM.SendResult();
            notifyEvenTester.NotifyDataTester("ResultTester : END TESTER ----------");
            logger.Create01("ResultTester : END TESTER --------------------------------------------------------------------", LogLevel.Information);

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
                    UiManager.Instance.PLC.device.ReadMultiBits(DeviceCode.M, 20000, 900, out this.M_ListBitPLC20000_20900);
                    if(M_ListBitPLC20000_20900.Count >= 1)
                    {
                        SendResultTester();
                    }    
                   
                }

                Task.Delay(20);
            }
        }
        private void SendResultTester()
        {

            
            if (M_ListBitPLC20000_20900[440])
            {
                notifyEvenTester.NotifyDataTester("SendIRSS: STATRT TESTER ------------");
                notifyEvenTester.NotifyDataTester("SendIRSS: Send Bit M20440 = False");
                logger.Create01("SendIRSS: STATRT TESTER----------------------------------------------------------------------", LogLevel.Information);
                logger.Create01("SendIRSS: Send Bit M20440 = False", LogLevel.Information);
                UiManager.Instance.PLC.device.WriteBit(DeviceCode.M, 20440, false);
                bool CH1_EN = M_ListBitPLC20000_20900[261];
                bool CH2_EN = M_ListBitPLC20000_20900[262];
                bool CH3_EN = M_ListBitPLC20000_20900[263];
                bool CH4_EN = M_ListBitPLC20000_20900[264];
                bool CH5_EN = M_ListBitPLC20000_20900[265];
                bool CH6_EN = M_ListBitPLC20000_20900[266];
                bool CH7_EN = M_ListBitPLC20000_20900[267];
                bool CH8_EN = M_ListBitPLC20000_20900[268];
                bool CH9_EN = M_ListBitPLC20000_20900[269];
                bool CH10_EN = M_ListBitPLC20000_20900[270];
                bool CH11_EN = M_ListBitPLC20000_20900[271];
                bool CH12_EN = M_ListBitPLC20000_20900[272];


                string message =
                $"CH1_EN:  {CH1_EN}\n" +
                $"CH2_EN:  {CH2_EN}\n" +
                $"CH3_EN:  {CH3_EN}\n" +
                $"CH4_EN:  {CH4_EN}\n" +
                $"CH5_EN:  {CH5_EN}\n" +
                $"CH6_EN:  {CH6_EN}\n" +
                $"CH7_EN:  {CH7_EN}\n" +
                $"CH8_EN:  {CH8_EN}\n" +
                $"CH9_EN:  {CH9_EN}\n" +
                $"CH10_EN: {CH10_EN}\n" +
                $"CH11_EN: {CH11_EN}\n" +
                $"CH12_EN: {CH12_EN}";

                notifyEvenTester.NotifyDataTester($"SendIRSS: READ_BIT_PLC :\n{message}");
                logger.Create01($"SendIRSS: READ_BIT_PLC :\n{message}", LogLevel.Information);


                notifyEvenTester.NotifyDataTester($"SendIRSS: PC > TESTER ");
                logger.Create01($"SendIRSS: PC > TESTER", LogLevel.Information);
                TesterCOM.SendIRSS(CH1_EN, CH2_EN, CH3_EN, CH4_EN, CH5_EN, CH6_EN, CH7_EN, CH8_EN, CH9_EN, CH10_EN, CH11_EN, CH12_EN);
            }

            if (M_ListBitPLC20000_20900[442])
            {
                if(PLC.device.WriteBit(DeviceCode.M, 20442, false))
                {
                    notifyEvenTester.NotifyDataTester("Send01OutAnd02OutTester: Send Bit M20442 = False");
                    logger.Create01("Send01OutAnd02OutTester: Send Bit M20442 = False", LogLevel.Information);
                }
                else
                {
                    logger.Create01("Send01OutAnd02OutTester: Send Bit M20442 :Error PLC Disconnect", LogLevel.Error);
                    notifyEvenTester.NotifyDataTester("Send01OutAnd02OutTester: Send Bit M20442 :Error PLC Disconnect");
                }
                logger.Create01("Send01OutAnd02OutTester : PC > TESTER Send OK", LogLevel.Information);
                notifyEvenTester.NotifyDataTester("Send01OutAnd02OutTester :PC > TESTER Send OK");
                TesterCOM.Send01OutAnd02OutTester();
            }

            if (M_ListBitPLC20000_20900[443])
            {
                if(PLC.device.WriteBit(DeviceCode.M, 20443, false))
                {
                    notifyEvenTester.NotifyDataTester("Send01InAnd02OutTester: Send Bit M20443 = False");
                    logger.Create01("Send01InAnd02OutTester: Send Bit M20443 = False", LogLevel.Information);
                }
                else
                {
                    logger.Create01("Send01InAnd02OutTester: Send Bit M20443 :Error PLC Disconnect", LogLevel.Error);
                    notifyEvenTester.NotifyDataTester("Send01InAnd02OutTester: Send Bit M20443 :Error PLC Disconnect");
                }
                logger.Create01("Send01InAnd02OutTester : PC > TESTER Send OK", LogLevel.Information);
                notifyEvenTester.NotifyDataTester("Send01InAnd02OutTester :PC > TESTER Send OK");
                TesterCOM.Send01InAnd02OutTester();
            }
            if (M_ListBitPLC20000_20900[444])
            {
                if(PLC.device.WriteBit(DeviceCode.M, 20444, false))
                {
                    notifyEvenTester.NotifyDataTester("Send01OutAnd02InTester: Send Bit M20444 = False");
                    logger.Create01("Send01OutAnd02InTester: Send Bit M20444 = False", LogLevel.Information);
                }
                else
                {
                    logger.Create01("Send01OutAnd02InTester: Send Bit M20444 :Error PLC Disconnect", LogLevel.Error);
                    notifyEvenTester.NotifyDataTester("Send01OutAnd02InTester: Send Bit M20444 :Error PLC Disconnect");
                }
                logger.Create01("Send01OutAnd02InTester : PC > TESTER Send OK", LogLevel.Information);
                notifyEvenTester.NotifyDataTester("Send01OutAnd02InTester :PC > TESTER Send OK");
                TesterCOM.Send01OutAnd02InTester();
            }

            if (M_ListBitPLC20000_20900[446])
            {
                if(PLC.device.WriteBit(DeviceCode.M, 20446, false))
                {
                    notifyEvenTester.NotifyDataTester("SendCheckAgain: Send Bit M20446 = False");
                    logger.Create01("SendCheckAgain: Send Bit M20446 = False", LogLevel.Information);
                }
                else
                {
                    logger.Create01("SendCheckAgain: Send Bit M20446 :Error PLC Disconnect", LogLevel.Error);
                    notifyEvenTester.NotifyDataTester("SendCheckAgain: Send Bit M20446 :Error PLC Disconnect");
                }
                logger.Create01("SendCheckAgain : PC > TESTER Send OK", LogLevel.Information);
                notifyEvenTester.NotifyDataTester("SendCheckAgain :PC > TESTER Send OK");
                TesterCOM.SendCheckAgain();
            }
            if (M_ListBitPLC20000_20900[448])
            {
                //if(PLC.device.WriteBit(DeviceCode.M, 20448, false))
                //{
                //    notifyEvenTester.NotifyDataTester("SendResult: Send Bit M20448 = False");
                //    logger.Create01("SendResult: Send Bit M20448 = False", LogLevel.Information);

                //}
                //else
                //{
                //    logger.Create01("SendResult: Send Bit M20448 :Error PLC Disconnect", LogLevel.Error);
                //    notifyEvenTester.NotifyDataTester("SendResult: Send Bit M20448 :Error PLC Disconnect");
                //}
                //logger.Create01("SendResult : PC > TESTER Send OK", LogLevel.Information);
                //notifyEvenTester.NotifyDataTester("SendResult :PC > TESTER Send OK");
                //TesterCOM.SendResult();
                //notifyEvenTester.NotifyDataTester("ResultTester : END TESTER ----------");
                //logger.Create01("ResultTester : END TESTER --------------------------------------------------------------------", LogLevel.Information);

            }
            
        }
        #endregion

    }
}
