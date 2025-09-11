using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for PgAssignMenu.xaml
    /// </summary>
    public partial class PgAssignMenu : Page
    {
        public PgAssignMenu()
        {
            InitializeComponent();
            this.Loaded += PgAssignMenu_Loaded;
            this.Unloaded += PgAssignMenu_Unloaded;
            this.btSwitchLogin.Click += BtSwitchLogin_Click;
            this.btSwitchLoginApp.Click += BtSwitchLoginApp_Click;
            this.btSwitchLoginAppInital.Click += BtSwitchLoginAppInital_Click;
            this.btSwitchAutoCheckUpdate.Click += BtSwitchAutoCheckUpdate_Click;

            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
        }

        private void BtSwitchAutoCheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                var setting = UiManager.managerSetting.assignSystem;
                setting.AutoCheckUpdate = toggle.IsChecked == true ? true : false;
                this.UpdateUI();
                UiManager.SaveManagerSetting();
            }
        }

        private void BtSwitchLoginAppInital_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                var setting = UiManager.managerSetting.assignSystem;
                setting.LoginApp = toggle.IsChecked == true ? true : false;
                this.UpdateUI();
                UiManager.SaveManagerSetting();
            }
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_ASSIGN_MENU);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_ASSIGN_ALARM_SETTING);
        }

       

       

        private void BtSwitchLoginApp_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                var setting = UiManager.managerSetting.assignSystem;
                setting.LoginMes = toggle.IsChecked == true ? true : false;
                this.UpdateUI();
                UiManager.SaveManagerSetting();
            }
        }

        private void BtSwitchLogin_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                var settingFTP = UiManager.managerSetting.settingFTPUpdate;
                settingFTP.UseLogin = toggle.IsChecked == true ? "Y" : "N";

                this.UpdateUI();
                UiManager.SaveManagerSetting();
            }
        }

        private void PgAssignMenu_Unloaded(object sender, RoutedEventArgs e)
        {
            UiManager.managerSetting.assignSystem.NameMachine = this.tbxNameMachine.Text;

            var SettingFTP = UiManager.managerSetting.settingFTPUpdate;
            SettingFTP.Url = this.tbxUrlFTP.Text;
            SettingFTP.UserName = this.tbxUseNameFTP.Text;
            SettingFTP.PassWord = this.tbxPassWordFTP.Text;
            SettingFTP.NameAppOpen = this.tbxNameAppFTP.Text;


            UiManager.managerSetting.assignSystem.NameMachine = this.tbxNameMachine.Text;
            UiManager.managerSetting.loginApp.LabelMesNameME = this.tbxMESME.Text;
            UiManager.managerSetting.loginApp.LabelMesNameOPE = this.tbxMESOP.Text;
            UiManager.SaveManagerSetting();
        }

        private void PgAssignMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateUI();
        }
        private void UpdateUI()
        {
            var SettingFTP = UiManager.managerSetting.settingFTPUpdate;
            var Setting = UiManager.managerSetting.assignSystem;
            this.tbxUrlFTP.Text = SettingFTP.Url.ToString();
            this.tbxUseNameFTP.Text = SettingFTP.UserName.ToString();
            this.tbxPassWordFTP.Text = SettingFTP.PassWord.ToString();
            this.tbxNameAppFTP.Text = SettingFTP.NameAppOpen.ToString();

            this.tbxNameMachine.Text = UiManager.managerSetting.assignSystem.NameMachine;
            this.tbxMESME.Text = UiManager.managerSetting.loginApp.LabelMesNameME;
            this.tbxMESOP.Text = UiManager.managerSetting.loginApp.LabelMesNameOPE;



            if (SettingFTP.UseLogin == "Y")
            {
               this.btSwitchLogin.IsChecked = true;
               this.tbxUseNameFTP.IsEnabled = true;
               this.tbxPassWordFTP.IsEnabled = true;
            }    
            else
            {
                this.btSwitchLogin.IsChecked = false;
                this.tbxUseNameFTP.IsEnabled = false;
                this.tbxPassWordFTP.IsEnabled = false;
            }    
            if(Setting.LoginMes)
            {
                this.btSwitchLoginApp.IsChecked = true;
            }
            else
            {
                this.btSwitchLoginApp.IsChecked = false;
            }

            if (Setting.LoginApp)
            {
                this.btSwitchLoginAppInital.IsChecked = true;
            }
            else
            {
                this.btSwitchLoginAppInital.IsChecked = false;
            }
            if (Setting.AutoCheckUpdate)
            {
                this.btSwitchAutoCheckUpdate.IsChecked = true;
            }
            else
            {
                this.btSwitchAutoCheckUpdate.IsChecked = false;
            }
           
        }
    }
}
