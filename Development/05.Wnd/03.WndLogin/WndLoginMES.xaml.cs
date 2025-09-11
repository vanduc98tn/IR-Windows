using KeyPad;
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
using System.Windows.Shapes;

namespace Development
{
    /// <summary>
    /// Interaction logic for WndLoginMES.xaml
    /// </summary>
    public partial class WndLoginMES : Window
    {
        private int isLogonSuccess = 0;
        private bool selectLoginMes = false;
        private bool OnKeyBoard = false;
        public WndLoginMES()
        {
            InitializeComponent();
            this.Loaded += WndLoginMES_Loaded;
            this.btOperator.Click += BtOperator_Click;
            this.btManager.Click += BtManager_Click;
            this.btAutoteam.Click += BtAutoteam_Click;
            this.btcancel.Click += Btcancel_Click;
            this.btSignin.Click += BtSignin_Click;

            this.txtPassword.PreviewTouchDown += TxtPassword_PreviewTouchDown;
            this.tbxCodeUserName.PreviewTouchDown += Txt_PreviewTouchDown;

            this.txtPassword.PreviewMouseDown += TxtPassword_PreviewMouseDown;
            this.tbxCodeUserName.PreviewMouseDown += TbxCodeUserName_PreviewMouseDown;

            this.btSwitchOnKeyBoard.Click += BtSwitchOnKeyBoard_Click;
            this.btSwitchOnKeyBoard.TouchDown += BtSwitchOnKeyBoard_TouchDown;
        }

        private void BtSwitchOnKeyBoard_TouchDown(object sender, TouchEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                this.OnKeyBoard = toggle.IsChecked == true ? true : false;
            }
        }

       

        private void BtSwitchOnKeyBoard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                this.OnKeyBoard = toggle.IsChecked == true ? true : false;
            }
        }

        private void TbxCodeUserName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OnKeyBoard)
            {
                TextBox txt = sender as TextBox;
                VirtualKeyboard keyboardWindow = new VirtualKeyboard();
                if (keyboardWindow.ShowDialog() == true)
                {
                    txt.Text = keyboardWindow.Result;
                }
            }
        }

        private void TxtPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OnKeyBoard)
            {
                PasswordBox txt = sender as PasswordBox;
                VirtualKeyboard keyboardWindow = new VirtualKeyboard();
                if (keyboardWindow.ShowDialog() == true)
                {
                    txt.Password = keyboardWindow.Result;
                }
            }
        }

        private void TxtPassword_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (OnKeyBoard)
            {
                PasswordBox txt = sender as PasswordBox;
                VirtualKeyboard keyboardWindow = new VirtualKeyboard();
                if (keyboardWindow.ShowDialog() == true)
                {
                    txt.Password = keyboardWindow.Result;
                }
            }

        }

        private void Txt_PreviewTouchDown(object sender, TouchEventArgs e)
        {
          if (OnKeyBoard)
            {
                TextBox txt = sender as TextBox;
                VirtualKeyboard keyboardWindow = new VirtualKeyboard();
                if (keyboardWindow.ShowDialog() == true)
                {
                    txt.Text = keyboardWindow.Result;
                }
            }
        }

        private void BtSignin_Click(object sender, RoutedEventArgs e)
        {
            this.Login();
        }

        private void Btcancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WndLoginMES_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateUi();
        }
        private void UpdateUi()
        {
            this.tbxCodeUserName.IsEnabled = false;
            this.txtPassword.IsEnabled = false;
        }
        private void BtAutoteam_Click(object sender, RoutedEventArgs e)
        {
            this.selectLoginMes = true;

            this.tbxCodeUserName.IsEnabled = true;
            this.txtPassword.IsEnabled = true;
            this.tbxCodeUserName.Text = "Nhập Mã Nhân viên : Auto Team";



            this.textId.Text = UiManager.managerSetting.loginApp.UseNameADM;
        }

        private void BtManager_Click(object sender, RoutedEventArgs e)
        {
            this.selectLoginMes = false;

            this.tbxCodeUserName.IsEnabled = true;
            this.txtPassword.IsEnabled = true;
            this.tbxCodeUserName.Text = "Nhập Mã Nhân viên : Manager";
            

            this.textId.Text = UiManager.managerSetting.loginApp.UseNameEN;
        }

        private void BtOperator_Click(object sender, RoutedEventArgs e)
        {
            this.selectLoginMes = false;

            this.tbxCodeUserName.IsEnabled = true;
            this.txtPassword.IsEnabled = true;
            this.tbxCodeUserName.Text = "Nhập Mã Nhân viên : Operator";
           

            this.textId.Text = UiManager.managerSetting.loginApp.UseNameOPE;
        }
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Login();
            }
        }
        private void textId_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.textId.Focus();

        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Image_Mouseup(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        public int DoCheck()
        {

            this.WindowState = WindowState.Normal;
            this.Topmost = true;
            this.ShowDialog();
            return isLogonSuccess;
        }
        private void tbxCodeUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxCodeUserName.Text = "";
        }
        private void Login()
        {
            if(selectLoginMes)
            {
                if(this.tbxCodeUserName.Text == "Nhập Mã Nhân viên : Auto Team")
                {
                    this.tbxCodeUserName.Text = "";
                }    
                string codeUser = tbxCodeUserName.Text;
                if (String.IsNullOrEmpty(codeUser))
                {
                    MessageBox.Show("Please re-confirm the employee code!");
                }
                else
                {
                    string usename = this.textId.Text;
                    UserManager.createUserLog(UserActions.LOGON_BUTTON_ENTER);
                    isLogonSuccess = UserManager.LogOn(usename, this.txtPassword.Password);
                    if (isLogonSuccess == 0)
                    {
                        MessageBox.Show("Wrong Password!");
                    }
                    else
                    {
                        UiManager.UserNameLoginMesOP_ME = UiManager.managerSetting.loginApp.LabelMesNameME;
                        UiManager.CodeUserLoginMesOP_ME = tbxCodeUserName.Text;
                        this.Close();
                        selectLoginMes = false;
                    }
                }
                
            }
            else
            {
                if (this.tbxCodeUserName.Text == "Nhập Mã Nhân viên : Operator" || this.tbxCodeUserName.Text == "Nhập Mã Nhân viên : Manager")
                {
                    this.tbxCodeUserName.Text = "";
                }
                string codeUser = tbxCodeUserName.Text;
                if (String.IsNullOrEmpty(codeUser))
                {
                    MessageBox.Show("Please re-confirm the employee code!");
                }
                else
                {
                    /// Sử dụng Check Mes Tại đây 
                    MessageBox.Show("Comming Soon.......");
                }    
             
              

            }
        }
    }
}
