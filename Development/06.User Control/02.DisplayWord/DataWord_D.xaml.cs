using ITM_Semiconductor;
using KeyPad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for DataWord_D.xaml
    /// </summary>
    public partial class DataWord_D : UserControl, IObserverChangeWords
    {
        private CancellationTokenSource monitorCancellation;
        private MyLogger logger = new MyLogger("DataWord_D");
        //public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
        //    "Background", typeof(Brush), typeof(DataWord_D), new PropertyMetadata(Brushes.White));
        //public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        //    "Foreground", typeof(Brush), typeof(DataWord_D), new PropertyMetadata(Brushes.White));
        public static readonly DependencyProperty DeviceProperty = DependencyProperty.Register(
            "Device", typeof(object), typeof(DataWord_D), new PropertyMetadata(null));
        public static readonly DependencyProperty NoOfDisplayProperty =
            DependencyProperty.Register("NoOfDisplay", typeof(int), typeof(DataWord_D), new PropertyMetadata(8));
        public static readonly DependencyProperty NoOfDecimalDigitsProperty =
            DependencyProperty.Register("NoOfDecimalDigits", typeof(int), typeof(DataWord_D), new PropertyMetadata(0));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
        "IsReadOnly", typeof(bool), typeof(DataWord_D), new PropertyMetadata(false));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text", typeof(string), typeof(DataWord_D), new PropertyMetadata("0"));

        //public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
        //    "FontSize", typeof(int), typeof(DataWord_D), new PropertyMetadata(20));

        public static readonly DependencyProperty IsShowInWindowProperty = DependencyProperty.Register(
            "IsShowInWindow", typeof(bool), typeof(DataWord_D), new PropertyMetadata(false));

        public static readonly DependencyProperty IsTabItemProperty = DependencyProperty.Register(
            "IsTabItem", typeof(bool), typeof(DataWord_D), new PropertyMetadata(false));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public bool IsTabItem
        {
            get { return (bool)GetValue(IsTabItemProperty); }
            set { SetValue(IsTabItemProperty, value); }
        }

        public bool IsShowInWindow
        {
            get { return (bool)GetValue(IsShowInWindowProperty); }
            set { SetValue(IsShowInWindowProperty, value); }
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        //public int FontSize
        //{
        //    get { return (int)GetValue(FontSizeProperty); }
        //    set { SetValue(FontSizeProperty, value); }
        //}
        public int NoOfDisplay
        {
            get { return (int)GetValue(NoOfDisplayProperty); }
            set { SetValue(NoOfDisplayProperty, value); }
        }
        public int NoOfDecimalDigits
        {
            get { return (int)GetValue(NoOfDecimalDigitsProperty); }
            set { SetValue(NoOfDecimalDigitsProperty, value); }
        }
        //public Brush Background
        //{
        //    get { return (Brush)GetValue(BackgroundProperty); }
        //    set { SetValue(BackgroundProperty, value); }
        //}
        //public Brush Foreground
        //{
        //    get { return (Brush)GetValue(ForegroundProperty); }
        //    set { SetValue(ForegroundProperty, value); }
        //}
        public object Device
        {
            get { return GetValue(DeviceProperty); }
            set { SetValue(DeviceProperty, value); }
        }
        public string Value
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    string oldValue = _content;
                    _content = value;
                    OnPropertyChanged(nameof(Value));
                    OnContentChanged(oldValue, _content);
                    ContentChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private string _content;
        public event EventHandler ContentChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isShowKeypad;
        private bool isInTabItem;
        private NotifyPLCWord notifyPLCWord = new NotifyPLCWord();
        public DataWord_D()
        {
            InitializeComponent();
            this.isShowKeypad = false;
            this.Loaded += TxtReadOnlyWord_Loaded;
            this.Unloaded += TxtReadOnlyWord_Unloaded;
        }

        private void TxtReadOnlyWord_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    // Bỏ qua các hành động trong chế độ Design
                    return;
                }
                if (this.isInTabItem) return;
                this.Unregister();
                this.monitorCancellation?.Cancel();
                if (this.isShowKeypad) return;
                this.RemoveDevice();
            }
            catch (Exception ex)
            {
                this.logger.Create("TxtReadOnlyWord_Unloaded: " + ex.Message, LogLevel.Error);
            }
        }

        private void TxtReadOnlyWord_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.isInTabItem) return;
                this.Initial();
                this.RemoveDevice();
                this.LoadNotifyPLCWord();
                this.RegisterNotify();
                this.monitorCancellation = new CancellationTokenSource();
                this.AddDevice();
                this.isInTabItem = this.IsTabItem;
            }
            catch (Exception ex)
            {
                this.logger.Create("TxtReadOnlyWord_Loaded: " + ex.Message, LogLevel.Error);
            }
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DataWord_D;
            control?.OnContentChanged();
        }

        protected virtual void OnContentChanged()
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void AddDevice()
        {
            if (this.Device == null) return;
            var address = ushort.Parse(this.Device.ToString());
            UiManager.Instance.PLC.AddAddressDeviceWord_D(address);
        }
        private void RemoveDevice()
        {
            if (this.Device == null) return;
            var address = ushort.Parse(this.Device.ToString());
            UiManager.Instance.PLC.RemoveAddressDeviceWord_D(address);
        }

        private void LoadNotifyPLCWord()
        {
            this.notifyPLCWord = SystemsManager.Instance.NotifyPLCWord;
        }
        private void RegisterNotify()
        {
            this.notifyPLCWord.Attach(this);
        }
        private void Unregister()
        {
            this.notifyPLCWord.Detach(this);
        }
        private void Initial()
        {
            var value = this.convertValue(Convert.ToDouble(0), NoOfDisplay, NoOfDecimalDigits);
            txt.Text = value;
        }
        public string convertValue(double value, int totalDigitsBeforeDecimal, int totalDigitsAfterDecimal)
        {
            string formatString = "";
            try
            {
                string numberString = value.ToString();
                string numberStringSource = value.ToString();
                numberString = numberString.Replace('-', ' ');
                numberString = numberString.Trim();

                // Thêm '0' nếu thiếu phần nguyên (ví dụ: ".150" → "0.150")
                if (numberString.StartsWith("."))
                {
                    numberString = "0" + numberString;
                }

                // Bỏ dấu chấm thập phân để đếm ký tự
                numberString = numberString.Replace(".", "");

                formatString = numberString.PadLeft(totalDigitsBeforeDecimal + totalDigitsAfterDecimal, '0');

                // Tìm vị trí của dấu thập phân và chèn dấu thập phân
                int decimalPointIndex = formatString.Length - totalDigitsAfterDecimal;
                if (decimalPointIndex >= 0 && decimalPointIndex < formatString.Length)
                {
                    formatString = formatString.Insert(decimalPointIndex, ".");
                }
                if (numberStringSource.Contains("-"))
                {
                    formatString = "-" + formatString;
                }
            }
            catch (Exception ex)
            {
                this.logger.Create("convertValue: " + ex.Message, LogLevel.Error);
            }
            //return this.FillString(formatString);
            return formatString;
        }
        private string FillString(string input)
        {
            int indexOfMinus = input.IndexOf('-');
            if (indexOfMinus != -1)
            {
                return input.Substring(0, indexOfMinus + 1) + input.Substring(indexOfMinus + 1).TrimStart('0');
            }
            else
            {
                return input;
            }
        }
        private void txt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.IsReadOnly) return;
            this.isShowKeypad = true;
            string NumberBefore = txt.Text;

            Keypad keyboardWindow = new Keypad(false);
            if (keyboardWindow.ShowDialog() == true)
            {
                txt.Text = keyboardWindow.Result;

            }
            var number = txt.Text;
            string x = "1";
            string y = x.PadRight(this.NoOfDecimalDigits + 1, '0');
            //var numberData = number.TrimStart('0');

            var numberData = number; ;

            Text = txt.Text;
            if (string.IsNullOrEmpty(numberData)) return;
            int value = (int)(Convert.ToDouble(numberData) * Convert.ToInt16(y));
            var address = ushort.Parse(this.Device.ToString());
            if (UiManager.Instance.PLC.device.WriteWord(DeviceCode.D, address, value))
            {
                this.EventLog(address + " Changed value : ", $"{NumberBefore} > {value.ToString()}");
            }
                
         
            this.isShowKeypad = false;



            
        }
        private void EventLog(string message, string type)
        {
            try
            {
                DbWrite.createEventLog($"Word D :{message}", type);
            }
            catch (Exception ex)
            {
                logger.Create("AlarmLog: " + ex.Message, LogLevel.Error);
            }
        }
        public void NotifyChangeWord(string key, short value)
        {
            Dispatcher.Invoke(() =>
            {
                if (this.Device == null) return;
                if (key != this.Device.ToString()) return;
                var data = this.convertValue(Convert.ToDouble(value), NoOfDisplay, NoOfDecimalDigits);
                if (!isShowKeypad)
                {
                    txt.Text = data;
                    Text = data;
                    Value = data;
                }
            });
            
        }

        private void txt_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            
            TextBox txt = sender as TextBox;

            if (txt.IsReadOnly) return;
            this.isShowKeypad = true;


            Keypad keyboardWindow = new Keypad(true);
            if (keyboardWindow.ShowDialog() == true)
            {
                txt.Text = keyboardWindow.Result;

            }
            var number = txt.Text;
            string x = "1";
            string y = x.PadRight(this.NoOfDecimalDigits + 1, '0');
            //var numberData = number.TrimStart('0');

            var numberData = number; ;

            Text = txt.Text;
            if (string.IsNullOrEmpty(numberData)) return;
            int value = (int)(Convert.ToDouble(numberData) * Convert.ToInt16(y));
            var address = ushort.Parse(this.Device.ToString());
            UiManager.Instance.PLC.device.WriteWord(DeviceCode.D, address, value);
            this.EventLog(address + " Changed value ", value.ToString());
            this.isShowKeypad = false;

        }
    }
}
