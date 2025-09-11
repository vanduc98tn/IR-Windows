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
    /// Interaction logic for PgTeachingMenu02.xaml
    /// </summary>
    public partial class PgTeachingMenu02 : Page
    {
        public PgTeachingMenu02()
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


            this.btLoadAx49Pos0.Click += BtLoadAx49Pos0_Click;
            this.btLoadAx49Pos1.Click += BtLoadAx49Pos1_Click;
            this.btLoadAx49Pos2.Click += BtLoadAx49Pos2_Click;
            this.btLoadAx49Pos3.Click += BtLoadAx49Pos3_Click;
            this.btLoadAx49Pos4.Click += BtLoadAx49Pos4_Click;
            this.btLoadAx49Pos5.Click += BtLoadAx49Pos5_Click;
            this.btLoadAx49Pos6.Click += BtLoadAx49Pos6_Click;
            this.btLoadAx49Pos7.Click += BtLoadAx49Pos7_Click;
            this.btLoadAx49Pos8.Click += BtLoadAx49Pos8_Click;
            this.btLoadAx49Pos9.Click += BtLoadAx49Pos9_Click;
            this.btLoadAx49Pos10.Click += BtLoadAx49Pos10_Click;

            this.btLoadAx410Pos0.Click += BtLoadAx410Pos0_Click;
            this.btLoadAx410Pos1.Click += BtLoadAx410Pos1_Click;
            this.btLoadAx410Pos2.Click += BtLoadAx410Pos2_Click;
            this.btLoadAx410Pos3.Click += BtLoadAx410Pos3_Click;
            this.btLoadAx410Pos4.Click += BtLoadAx410Pos4_Click;
            this.btLoadAx410Pos5.Click += BtLoadAx410Pos5_Click;
            this.btLoadAx410Pos6.Click += BtLoadAx410Pos6_Click;
            this.btLoadAx410Pos7.Click += BtLoadAx410Pos7_Click;
            this.btLoadAx410Pos8.Click += BtLoadAx410Pos8_Click;
            this.btLoadAx410Pos9.Click += BtLoadAx410Pos9_Click;
            this.btLoadAx410Pos10.Click += BtLoadAx410Pos10_Click;


            this.btLoadAx411Pos0.Click += BtLoadAx411Pos0_Click;
            this.btLoadAx411Pos1.Click += BtLoadAx411Pos1_Click;
            this.btLoadAx411Pos2.Click += BtLoadAx411Pos2_Click;
            this.btLoadAx411Pos3.Click += BtLoadAx411Pos3_Click;
            this.btLoadAx411Pos4.Click += BtLoadAx411Pos4_Click;
            this.btLoadAx411Pos5.Click += BtLoadAx411Pos5_Click;
            this.btLoadAx411Pos6.Click += BtLoadAx411Pos6_Click;
            this.btLoadAx411Pos7.Click += BtLoadAx411Pos7_Click;
            this.btLoadAx411Pos8.Click += BtLoadAx411Pos8_Click;
            this.btLoadAx411Pos9.Click += BtLoadAx411Pos9_Click;
            this.btLoadAx411Pos10.Click += BtLoadAx411Pos10_Click;


            this.btLoadAx216Pos0.Click += BtLoadAx216Pos0_Click;
            this.btLoadAx216Pos1.Click += BtLoadAx216Pos1_Click;
            this.btLoadAx216Pos2.Click += BtLoadAx216Pos2_Click;
            this.btLoadAx216Pos3.Click += BtLoadAx216Pos3_Click;
            this.btLoadAx216Pos4.Click += BtLoadAx216Pos4_Click;
            this.btLoadAx216Pos5.Click += BtLoadAx216Pos5_Click;
           
        }
        private void BtLoadAx216Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6198, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, false);

        }
        private void BtLoadAx216Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6198, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, false);

        }
        private void BtLoadAx216Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6198, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, false);

        }
        private void BtLoadAx216Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6198, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, false);

        }
        private void BtLoadAx216Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6198, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, false);

        }
        private void BtLoadAx216Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 6198, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 6199, false);

        }




        private void BtLoadAx411Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }
        private void BtLoadAx411Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 22198, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21199, false);

        }


        private void BtLoadAx410Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }
        private void BtLoadAx410Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21998, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 21099, false);

        }



        private void BtLoadAx49Pos10_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 10")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 10);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos9_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 09")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 9);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos8_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 08")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 8);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos7_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 07")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 7);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos6_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 06")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 6);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos5_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 05")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 5);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos4_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 04")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 4);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos3_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 03")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 3);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos2_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 02")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 2);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos1_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 01")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 1);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

        }
        private void BtLoadAx49Pos0_Click(object sender, RoutedEventArgs e)
        {

            WndComfirm comfirmYesNo = new WndComfirm();
            if (!comfirmYesNo.DoComfirmYesNo($"Confrim Save Data Pos 00")) return;
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.ZR, 21798, 0);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, true);
            Thread.Sleep(10);
            UiManager.Instance.PLC.device.WriteBit(DeviceCode.L, 20999, false);

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
