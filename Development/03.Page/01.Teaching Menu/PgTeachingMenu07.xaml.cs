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
    /// Interaction logic for PgTeachingMenu07.xaml
    /// </summary>
    public partial class PgTeachingMenu07 : Page
    {
        public PgTeachingMenu07()
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
            this.btSetting10.Click += BtSetting10_Click;




            this.btLoadAx68Pos0.Click += BtLoadAx68Pos0_Click;
            this.btLoadAx68Pos1.Click += BtLoadAx68Pos1_Click;
            this.btLoadAx68Pos2.Click += BtLoadAx68Pos2_Click;

            //this.btLoadAx68Pos3.Click += BtLoadAx68Pos3_Click;
            //this.btLoadAx68Pos4.Click += BtLoadAx68Pos4_Click;
            //this.btLoadAx68Pos5.Click += BtLoadAx68Pos5_Click;
            //this.btLoadAx68Pos6.Click += BtLoadAx68Pos6_Click;
            //this.btLoadAx68Pos7.Click += BtLoadAx68Pos7_Click;
            //this.btLoadAx68Pos8.Click += BtLoadAx68Pos8_Click;
            //this.btLoadAx68Pos9.Click += BtLoadAx68Pos9_Click;
            //this.btLoadAx68Pos10.Click += BtLoadAx68Pos10_Click;


            this.btLoadAx69Pos0.Click += BtLoadAx69Pos0_Click;
            this.btLoadAx69Pos1.Click += BtLoadAx69Pos1_Click;
            this.btLoadAx69Pos2.Click += BtLoadAx69Pos2_Click;

            this.btLoadAx610Pos0.Click += BtLoadAx610Pos0_Click;
            this.btLoadAx610Pos1.Click += BtLoadAx610Pos1_Click;
            this.btLoadAx610Pos2.Click += BtLoadAx610Pos2_Click;
        }

        private void BtLoadAx610Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25998, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 25099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 25099, false);

        }
        private void BtLoadAx610Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25998, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 25099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 25099, false);

        }
        private void BtLoadAx610Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25998, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 25099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 25099, false);

        }








        private void BtLoadAx69Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25798, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24999, false);

        }
        private void BtLoadAx69Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25798, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24999, false);

        }
        private void BtLoadAx69Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25798, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24999, false);

        }








        private void BtLoadAx68Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }
        private void BtLoadAx68Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 25598, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24899, false);

        }












        private void BtSetting10_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_09);
        }
        private void BtSetting9_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_08);
        }
        private void BtSetting8_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_07);
        }
        private void BtSetting7_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_06);
        }
        private void BtSetting6_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_05);
        }
        private void BtSetting5_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_04);
        }
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_03);
        }
        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_02);
        }
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_01);
        }
        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION_01);
        }
    }
}
