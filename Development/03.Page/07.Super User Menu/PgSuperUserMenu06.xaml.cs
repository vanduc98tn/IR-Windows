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
    /// Interaction logic for PgSuperUserMenu06.xaml
    /// </summary>
    public partial class PgSuperUserMenu06 : Page
    {
        public PgSuperUserMenu06()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.btSetting5.Click += BtSetting5_Click;
            this.btSetting6.Click += BtSetting6_Click;
            this.btSetting7.Click += BtSetting7_Click;
            this.btSetting8.Click += BtSetting8_Click;
            this.btSetting9.Click += BtSetting9_Click;

        }
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_02);
        }
        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_01);
        }
        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_03);
        }
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_04);
        }
        private void BtSetting5_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_05);
        }
        private void BtSetting6_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_06);
        }
        private void BtSetting7_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_07);
        }
        private void BtSetting8_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_08);
        }
        private void BtSetting9_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_09);
        }
    }
}
