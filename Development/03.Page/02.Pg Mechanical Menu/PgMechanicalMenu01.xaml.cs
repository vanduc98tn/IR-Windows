using System;
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
    /// Interaction logic for PgMechanicalMenu01.xaml
    /// </summary>
    public partial class PgMechanicalMenu01 : Page
    {
        public PgMechanicalMenu01()
        {
            InitializeComponent();
            this.Loaded += PgMechanicalMenu01_Loaded;
            this.btSave.Click += BtSave_Click;

            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
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

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            this.SaveSetting();
        }

        private void PgMechanicalMenu01_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateUI();
        }
        private void UpdateUI()
        {
            this.tbIpTCPVision.Text = UiManager.appSetting.settingDevice.settingTCPTranferVision.Ip.ToString();
            this.tbPortTCPVision.Text = UiManager.appSetting.settingDevice.settingTCPTranferVision.Port.ToString();
        }
        private void SaveSetting()
        {
            UiManager.appSetting.settingDevice.settingTCPTranferVision.Ip = this.tbIpTCPVision.Text;
            UiManager.appSetting.settingDevice.settingTCPTranferVision.Port = Convert.ToInt32(this.tbPortTCPVision.Text);
            UiManager.SaveAppSetting();

            UpdateLogs($"Setting IP : {UiManager.appSetting.settingDevice.settingTCPTranferVision.Ip}");
            UpdateLogs($"Setting PORT : {UiManager.appSetting.settingDevice.settingTCPTranferVision.Port}");
            UpdateLogs("Save Complete !");
        }
        private void UpdateLogs(string notify)
        {
            this.Dispatcher.Invoke(() => {
                this.txtLogs.Text += "\r\n" + notify;
                this.txtLogs.ScrollToEnd();
            });
        }
    }
}
