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
    /// Interaction logic for PgTeachingMenu06.xaml
    /// </summary>
    public partial class PgTeachingMenu06 : Page
    {
        public PgTeachingMenu06()
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




            this.btLoadAx62Pos0.Click += BtLoadAx62Pos0_Click;
            this.btLoadAx62Pos1.Click += BtLoadAx62Pos1_Click;
            this.btLoadAx62Pos2.Click += BtLoadAx62Pos2_Click;
            this.btLoadAx62Pos3.Click += BtLoadAx62Pos3_Click;
            this.btLoadAx62Pos4.Click += BtLoadAx62Pos4_Click;
            this.btLoadAx62Pos5.Click += BtLoadAx62Pos5_Click;
            this.btLoadAx62Pos6.Click += BtLoadAx62Pos6_Click;
            this.btLoadAx62Pos7.Click += BtLoadAx62Pos7_Click;
            this.btLoadAx62Pos8.Click += BtLoadAx62Pos8_Click;
            this.btLoadAx62Pos9.Click += BtLoadAx62Pos9_Click;
            this.btLoadAx62Pos10.Click += BtLoadAx62Pos10_Click;
            this.btLoadAx62Pos11.Click += BtLoadAx62Pos11_Click;


            this.btLoadAx63Pos0.Click += BtLoadAx63Pos0_Click;
            this.btLoadAx63Pos1.Click += BtLoadAx63Pos1_Click;
            this.btLoadAx63Pos2.Click += BtLoadAx63Pos2_Click;
            this.btLoadAx63Pos3.Click += BtLoadAx63Pos3_Click;
            this.btLoadAx63Pos4.Click += BtLoadAx63Pos4_Click;
            this.btLoadAx63Pos5.Click += BtLoadAx63Pos5_Click;
            this.btLoadAx63Pos6.Click += BtLoadAx63Pos6_Click;
            this.btLoadAx63Pos7.Click += BtLoadAx63Pos7_Click;
            this.btLoadAx63Pos8.Click += BtLoadAx63Pos8_Click;
            this.btLoadAx63Pos9.Click += BtLoadAx63Pos9_Click;
            this.btLoadAx63Pos10.Click += BtLoadAx63Pos10_Click;
            this.btLoadAx63Pos11.Click += BtLoadAx63Pos11_Click;



            this.btLoadAx64Pos0.Click += BtLoadAx64Pos0_Click;
            this.btLoadAx64Pos1.Click += BtLoadAx64Pos1_Click;
            this.btLoadAx64Pos2.Click += BtLoadAx64Pos2_Click;
            this.btLoadAx64Pos3.Click += BtLoadAx64Pos3_Click;
            this.btLoadAx64Pos4.Click += BtLoadAx64Pos4_Click;
            this.btLoadAx64Pos5.Click += BtLoadAx64Pos5_Click;
            this.btLoadAx64Pos6.Click += BtLoadAx64Pos6_Click;
            this.btLoadAx64Pos7.Click += BtLoadAx64Pos7_Click;
            this.btLoadAx64Pos8.Click += BtLoadAx64Pos8_Click;
            this.btLoadAx64Pos9.Click += BtLoadAx64Pos9_Click;
            this.btLoadAx64Pos10.Click += BtLoadAx64Pos10_Click;


            this.btLoadAx638Pos0.Click += BtLoadAx638Pos0_Click;
            this.btLoadAx638Pos1.Click += BtLoadAx638Pos1_Click;
            this.btLoadAx638Pos2.Click += BtLoadAx638Pos2_Click;
            this.btLoadAx638Pos3.Click += BtLoadAx638Pos3_Click;
            this.btLoadAx638Pos4.Click += BtLoadAx638Pos4_Click;
            this.btLoadAx638Pos5.Click += BtLoadAx638Pos5_Click;
            
        }
        private void BtLoadAx638Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6798, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, false);

        }
        private void BtLoadAx638Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6798, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, false);

        }
        private void BtLoadAx638Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6798, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, false);

        }
        private void BtLoadAx638Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6798, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, false);

        }
        private void BtLoadAx638Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6798, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, false);

        }
        private void BtLoadAx638Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6798, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6499, false);

        }

        private void BtLoadAx64Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx64Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24798, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24499, false);

        }
        private void BtLoadAx63Pos11_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 11")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 11);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx63Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24598, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24399, false);

        }
        private void BtLoadAx62Pos11_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 11")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 11);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

        }
        private void BtLoadAx62Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 24398, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 24299, false);

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
