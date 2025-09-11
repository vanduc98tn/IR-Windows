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
    /// Interaction logic for PgManual01.xaml
    /// </summary>
    public partial class PgManual01 : Page
    {
        public PgManual01()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.btSetting5.Click += BtSetting5_Click;
            this.btSetting6.Click += BtSetting6_Click;

            this.btTeaching.Click += BtTeaching_Click;
        }

        private void BtTeaching_Click(object sender, RoutedEventArgs e)
        {
            if (UserManager.IsLogOn() == 3)
            {
                UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_01);
            }    
            else
            {
                WndMessenger wndMessenger = new WndMessenger();
                wndMessenger.MessengerShow("Use Login AutoTeam To Page Teaching");
            }    
        }
        private void BtSetting6_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_06);
        }

        private void BtSetting5_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_05);
        }
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_04);
        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_03);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_02);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_01);
        }
    }
}
