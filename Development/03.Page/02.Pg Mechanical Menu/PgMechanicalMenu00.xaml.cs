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
    /// Interaction logic for PgMechanicalMenu00.xaml
    /// </summary>
    public partial class PgMechanicalMenu00 : Page
    {
        public PgMechanicalMenu00()
        {
            InitializeComponent();
            this.Loaded += PgMechanicalMenu00_Loaded;

            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.btSave.Click += BtSave_Click;
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSetting();
        }

        private void PgMechanicalMenu00_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateUI();
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

        private void UpdateUI()
        {
            this.tbIPMES.Text = UiManager.appSetting.MESSettings.Ip.ToString();
            this.tbPortMES.Text = UiManager.appSetting.MESSettings.Port.ToString();
            this.tbEquipment.Text = UiManager.appSetting.MESSettings.EquimentID.ToString();

        }
        private void SaveSetting()
        {
            UiManager.appSetting.MESSettings.Ip = this.tbIPMES.Text;
            UiManager.appSetting.MESSettings.Port = Convert.ToInt32(this.tbPortMES.Text);
            UiManager.appSetting.MESSettings.EquimentID = this.tbEquipment.Text;

            UiManager.SaveAppSetting();
            UpdateLogs($"Setting Ip : {UiManager.appSetting.MESSettings.Ip}");
            UpdateLogs($"Setting Port : {UiManager.appSetting.MESSettings.Port}");
            UpdateLogs($"Setting Equipment : {UiManager.appSetting.MESSettings.EquimentID}");
            UpdateLogs($"Save Setting Complete !");


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
