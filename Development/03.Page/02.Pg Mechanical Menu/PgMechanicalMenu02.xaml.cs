﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for PgMechanicalMenu02.xaml
    /// </summary>
    public partial class PgMechanicalMenu02 : Page
    {
        private SettingDevice settingDevice;
        public PgMechanicalMenu02()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.btSetting.Click += BtSetting_Click;
            this.btSave.Click += BtSave_Click;

            this.btOpen.Click += BtOpen_Click;
            this.btClose.Click += BtClose_Click;

            this.Loaded += PgMechanicalMenu02_Loaded;
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            if(UiManager.Instance.TesterCOM.isOpen())
            {
                UiManager.Instance.DisconnectTester();
                UpdateUiButton();
            }    
        }

        private void BtOpen_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.Instance.TesterCOM.isOpen())
            {
                UiManager.Instance.ConnectTester();
                UpdateUiButton();
            }
        }
        private void UpdateUiButton()
        {
            if (UiManager.Instance.TesterCOM.isOpen())
            {
                UpdateLogs("Connect Tester Complete");
                btClose.Background = Brushes.White;
                btOpen.Background = Brushes.LightGreen;
            }
            else
            {
                UpdateLogs("Disconnect Tester");
                btClose.Background = Brushes.OrangeRed;
                btOpen.Background = Brushes.White;
            }
        }
        private void PgMechanicalMenu02_Loaded(object sender, RoutedEventArgs e)
        {
            settingDevice = UiManager.appSetting.settingDevice;
            UpdateUiButton();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting?")) return;
            UiManager.appSetting.settingDevice.COMTestter = settingDevice.COMTestter;
            UpdateLogs($"Save Setting Com Tester Complete");
            UiManager.SaveAppSetting();
        }

        private void BtSetting_Click(object sender, RoutedEventArgs e)
        {
            WndComSetting wndMC = new WndComSetting();
            var settingNew = wndMC.DoSettings(Window.GetWindow(this), this.settingDevice.COMTestter);
            if (settingNew != null)
            {
                this.settingDevice.COMTestter = settingNew;
                UpdateLogs($"Device Seting PortName :{settingNew.portName.ToString()}");
                UpdateLogs($"Device Seting Parity :{settingNew.parity}");
                UpdateLogs($"Device Seting Databis :{settingNew.dataBits.ToString()}");
                UpdateLogs($"Device Seting Stopbit :{settingNew.stopBits.ToString()}");
                UpdateLogs($"Device Seting Handshake :{settingNew.Handshake.ToString()}");
                UpdateLogs($"Click Button Save to Complete");
            }
        }
        private void UpdateLogs(string notify)
        {
            this.Dispatcher.Invoke(() => {
                this.txtLogs.Text += "\r\n" + notify;
                this.txtLogs.ScrollToEnd();
            });
        }


        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU_02);
        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU_01);

        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU_00);

        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU_PLC);

        }
    }
}
