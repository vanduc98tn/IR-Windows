using KeyPad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Development
{
    /// <summary>
    /// Interaction logic for PgModel.xaml
    /// </summary>
    public partial class PgModel : Page
    {

        MyLogger logger = new MyLogger("PG_TeachingMenu");
        string NameModelCurrent, NameModel01, NameModel02, NameModel03, NameModel04, NameModel05, NameModel06, NameModel07, NameModel08, NameModel09, NameModel10;
        private bool isUpdate = false;

        public PgModel()
        {
            InitializeComponent();

            this.Loaded += PgModel_Loaded;
            this.Unloaded += PgModel_Unloaded;

            this.txbModel1.PreviewMouseDown += TxbModel1_PreviewMouseDown;
            this.txbModel2.PreviewMouseDown += TxbModel2_PreviewMouseDown;
            this.txbModel3.PreviewMouseDown += TxbModel3_PreviewMouseDown;
            this.txbModel4.PreviewMouseDown += TxbModel4_PreviewMouseDown;
            this.txbModel5.PreviewMouseDown += TxbModel5_PreviewMouseDown;
            //this.txbModel6.PreviewMouseDown += TxbModel6_PreviewMouseDown;
            //this.txbModel7.PreviewMouseDown += TxbModel7_PreviewMouseDown;
            //this.txbModel8.PreviewMouseDown += TxbModel8_PreviewMouseDown;
            //this.txbModel9.PreviewMouseDown += TxbModel9_PreviewMouseDown;
            //this.txbModel10.PreviewMouseDown += TxbModel10_PreviewMouseDown;
        }




        private void TxbModel10_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10790, result);
            }
        }
        private void TxbModel9_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10780, result);
            }
        }
        private void TxbModel8_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10770, result);
            }
        }
        private void TxbModel7_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10760, result);
            }
        }
        private void TxbModel6_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10750, result);
            }
        }
        private void TxbModel5_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10740, result);
            }
        }
        private void TxbModel4_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10730, result);
            }
        }
        private void TxbModel3_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10720, result);
            }
        }
        private void TxbModel2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10710, result);
            }
        }
        private void TxbModel1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
            {
                string result = keyboardWindow.Result;

                // nếu dài hơn 20 ký tự thì cắt còn 20
                if (result.Length > 20)
                    result = result.Substring(0, 20);

                UiManager.Instance.PLC.device.WriteString(DeviceCode.ZR, 10700, result);
            }
        }



        private void PgModel_Unloaded(object sender, RoutedEventArgs e)
        {
            this.isUpdate = false;
            //this.RemoveDevicePLC();
        }

        private void PgModel_Loaded(object sender, RoutedEventArgs e)
        {
            this.isUpdate = true;
            //this.RegisterDevicePLC();

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

                    UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_MODEL_RUNNING_NAME, 10, out NameModelCurrent);

                    UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_01, 10, out NameModel01);
                    UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_02, 10, out NameModel02);
                    UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_03, 10, out NameModel03);
                    UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_04, 10, out NameModel04);
                    UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_05, 10, out NameModel05);
                    //UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_06, 10, out NameModel06);
                    //UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_07, 10, out NameModel07);
                    //UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_08, 10, out NameModel08);
                    //UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_09, 10, out NameModel09);
                    //UiManager.Instance.PLC.device.ReadASCIIString(DeviceCode.ZR, PLCStore.R_NAME_MODEL_10, 10, out NameModel10);

                }

                this.UpdateUI();
            }
            Thread.Sleep(20);
        }
        private void UpdateUI()
        {

            if (UiManager.Instance.PLC.device.isOpen())
            {
                Dispatcher.Invoke(new Action(() =>
                {

                    this.lblModel.Content = NameModelCurrent;

                    this.txbModel1.Text = NameModel01;
                    this.txbModel2.Text = NameModel02;
                    this.txbModel3.Text = NameModel03;
                    this.txbModel4.Text = NameModel04;
                    this.txbModel5.Text = NameModel05;
                    //this.txbModel6.Text = NameModel06;
                    //this.txbModel7.Text = NameModel07;
                    //this.txbModel8.Text = NameModel08;
                    //this.txbModel9.Text = NameModel09;
                    //this.txbModel10.Text = NameModel10;
                    
                }));
            }

        }
    }
}
