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
    /// Interaction logic for PgTeachingMenu03.xaml
    /// </summary>
    public partial class PgTeachingMenu03 : Page
    {
        public PgTeachingMenu03()
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



            this.btLoadAx412Pos0.Click += BtLoadAx412Pos0_Click;
            this.btLoadAx412Pos1.Click += BtLoadAx412Pos1_Click;
            this.btLoadAx412Pos2.Click += BtLoadAx412Pos2_Click;
            this.btLoadAx412Pos3.Click += BtLoadAx412Pos3_Click;
            this.btLoadAx412Pos4.Click += BtLoadAx412Pos4_Click;
            this.btLoadAx412Pos5.Click += BtLoadAx412Pos5_Click;
            this.btLoadAx412Pos6.Click += BtLoadAx412Pos6_Click;
            this.btLoadAx412Pos7.Click += BtLoadAx412Pos7_Click;
            this.btLoadAx412Pos8.Click += BtLoadAx412Pos8_Click;
            this.btLoadAx412Pos9.Click += BtLoadAx412Pos9_Click;
            this.btLoadAx412Pos10.Click += BtLoadAx412Pos10_Click;

            this.btLoadAx413Pos0.Click += BtLoadAx413Pos0_Click;
            this.btLoadAx413Pos1.Click += BtLoadAx413Pos1_Click;
            this.btLoadAx413Pos2.Click += BtLoadAx413Pos2_Click;
            this.btLoadAx413Pos3.Click += BtLoadAx413Pos3_Click;
            this.btLoadAx413Pos4.Click += BtLoadAx413Pos4_Click;
            this.btLoadAx413Pos5.Click += BtLoadAx413Pos5_Click;
            this.btLoadAx413Pos6.Click += BtLoadAx413Pos6_Click;
            this.btLoadAx413Pos7.Click += BtLoadAx413Pos7_Click;
            this.btLoadAx413Pos8.Click += BtLoadAx413Pos8_Click;
            this.btLoadAx413Pos9.Click += BtLoadAx413Pos9_Click;
            this.btLoadAx413Pos10.Click += BtLoadAx413Pos10_Click;

            this.btLoadAx414Pos0.Click += BtLoadAx414Pos0_Click;
            this.btLoadAx414Pos1.Click += BtLoadAx414Pos1_Click;
            this.btLoadAx414Pos2.Click += BtLoadAx414Pos2_Click;
            this.btLoadAx414Pos3.Click += BtLoadAx414Pos3_Click;
            this.btLoadAx414Pos4.Click += BtLoadAx414Pos4_Click;
            this.btLoadAx414Pos5.Click += BtLoadAx414Pos5_Click;
            this.btLoadAx414Pos6.Click += BtLoadAx414Pos6_Click;
            this.btLoadAx414Pos7.Click += BtLoadAx414Pos7_Click;
            this.btLoadAx414Pos8.Click += BtLoadAx414Pos8_Click;
            this.btLoadAx414Pos9.Click += BtLoadAx414Pos9_Click;
            this.btLoadAx414Pos10.Click += BtLoadAx414Pos10_Click;

            this.btLoadAx2712Pos0.Click += BtLoadAx2712Pos0_Click;
            this.btLoadAx2712Pos1.Click += BtLoadAx2712Pos1_Click;
            this.btLoadAx2712Pos2.Click += BtLoadAx2712Pos2_Click;
            this.btLoadAx2712Pos3.Click += BtLoadAx2712Pos3_Click;
            this.btLoadAx2712Pos4.Click += BtLoadAx2712Pos4_Click;
            this.btLoadAx2712Pos5.Click += BtLoadAx2712Pos5_Click;
           
        }
        private void BtLoadAx2712Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6398, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, false);

        }
        private void BtLoadAx2712Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6398, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, false);

        }
        private void BtLoadAx2712Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6398, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, false);

        }
        private void BtLoadAx2712Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6398, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, false);

        }
        private void BtLoadAx2712Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6398, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, false);

        }
        private void BtLoadAx2712Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6398, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6299, false);

        }






        private void BtLoadAx414Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }
        private void BtLoadAx414Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22798, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21499, false);

        }



        private void BtLoadAx413Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }
        private void BtLoadAx413Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22598, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21399, false);

        }



        private void BtLoadAx412Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

        }
        private void BtLoadAx412Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22398, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21299, false);

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
