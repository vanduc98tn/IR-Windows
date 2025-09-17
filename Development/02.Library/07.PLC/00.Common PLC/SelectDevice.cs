using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Development
{
    public class SelectDevice
    {
        private object iLock = new object();
        public Device device;
        private MyLogger logger = new MyLogger("Select Device");

        private object obj = new object();
        private CancellationTokenSource monitorCancellation;

        //Notify.
        private NotifyPLCBits notifyPLCBits;
        private NotifyPLCWord notifyPLCWord;
        private NotifyPLCDWord notifyPLCDWord;
        private NotifyPLCWord_ZR notifyPLCWord_ZR;
        private NotifyPLCDWord_ZR notifyPLCDWord_ZR;

        public List<ushort> AddressDeviceWord_D = new List<ushort>();
        public Dictionary<string, short> monitorDeviceWord_D = new Dictionary<string, short>();

        public List<ushort> AddressDeviceDWord_D = new List<ushort>();
        public Dictionary<string, int> monitorDeviceDWord_D = new Dictionary<string, int>();

        public List<ushort> AddressDeviceWord_ZR = new List<ushort>();
        public Dictionary<string, short> monitorDeviceWord_ZR = new Dictionary<string, short>();

        public List<ushort> AddressDeviceDWord_ZR = new List<ushort>();
        public Dictionary<string, int> monitorDeviceDWord_ZR = new Dictionary<string, int>();

        public List<DeviceItem> AddressDeviceBits_M = new List<DeviceItem>();
        public Dictionary<string, bool> monitorDeviceBits_M = new Dictionary<string, bool>();

        public List<DeviceItem> AddressDeviceBits_X = new List<DeviceItem>();
        public Dictionary<string, bool> monitorDeviceBits_X = new Dictionary<string, bool>();

        public List<DeviceItem> AddressDeviceBits_Y = new List<DeviceItem>();
        public Dictionary<string, bool> monitorDeviceBits_Y = new Dictionary<string, bool>();

        public List<DeviceItem> AddressDeviceBits_L = new List<DeviceItem>();
        public Dictionary<string, bool> monitorDeviceBits_L = new Dictionary<string, bool>();

        public List<DeviceItem> AddressDeviceBits_K = new List<DeviceItem>();
        public Dictionary<string, bool> monitorDeviceBits_K = new Dictionary<string, bool>();

        Dictionary<string, bool> previousStateBits_M = new Dictionary<string, bool>();
        Dictionary<string, bool> previousStateBits_X = new Dictionary<string, bool>();
        Dictionary<string, bool> previousStateBits_Y = new Dictionary<string, bool>();
        Dictionary<string, bool> previousStateBits_L = new Dictionary<string, bool>();
        Dictionary<string, bool> previousStateBits_K = new Dictionary<string, bool>();
        Dictionary<string, short> previousStateWord_D = new Dictionary<string, short>();
        Dictionary<string, int> previousStateDWord_D = new Dictionary<string, int>();
        Dictionary<string, short> previousStateWord_ZR = new Dictionary<string, short>();
        Dictionary<string, int> previousStateDWord_ZR = new Dictionary<string, int>();

        private int limitDeviceBits;
        private int limitDeviceWord;


        public SelectDevice (SaveDevice device, SettingDevice settingDevice)
        {
            this.LoadNotifyPLC();
            if (SaveDevice.Mitsubishi_MC_Protocol_Binary_TCP == device)
            {
                this.limitDeviceWord = 900;
                this.limitDeviceBits = 2000;
                this.device = new ServiceTCPMCProtocolBinary(settingDevice.MC_TCP_Binary);
            }
            if (SaveDevice.Mitsubishi_RS422_SC09 == device)
            {
                this.limitDeviceBits = 30;
                this.limitDeviceWord = 30;
                this.device = new ServiceRs422Fx(settingDevice.sc09Setting);
            }
            if (SaveDevice.LS_XGTServer_TCP == device)
            {
                this.limitDeviceBits = 1000;
                this.limitDeviceWord = 50;
                this.device = new ServiceXGTServerTCP(settingDevice.LSXGTServerTCPSetting);
            }
            if (SaveDevice.LS_XGTServer_COM == device)
            {
                this.limitDeviceBits = 300;
                this.limitDeviceWord = 120;
                this.device = new ServiceLSXGTServerCOM(settingDevice.XGTServerCOMSetting);
            }
        }

        #region USER CONTROL
        private void LoadNotifyPLC()
        {
            this.notifyPLCBits = SystemsManager.Instance.NotifyPLCBits;
            this.notifyPLCWord = SystemsManager.Instance.NotifyPLCWord;
            this.notifyPLCDWord = SystemsManager.Instance.NotifyPLCDWord;
            this.notifyPLCWord_ZR = SystemsManager.Instance.NotifyPLCWord_ZR;
            this.notifyPLCDWord_ZR = SystemsManager.Instance.NotifyPLCDWord_ZR;
        }
        public void MonitorDevice()
        {
            try
            {
                this.monitorCancellation = new CancellationTokenSource();
                this.AddressDeviceWord_D = new List<ushort>();
                this.AddressDeviceDWord_D = new List<ushort>();
                this.AddressDeviceBits_M = new List<DeviceItem>();
                this.AddressDeviceBits_X = new List<DeviceItem>();
                this.AddressDeviceBits_Y = new List<DeviceItem>();
                this.AddressDeviceBits_L = new List<DeviceItem>();

                while (!this.monitorCancellation.Token.IsCancellationRequested)
                {
                    if (device.isOpen())
                    {
                        if (!SystemsManager.Instance.isWriteDevice)
                        {
                            Task.Run(() => { MonitorDeviceBits_M(); });


                            if (UiManager.appSetting.selectDevice == SaveDevice.Mitsubishi_MC_Protocol_Binary_TCP)
                            {
                                Task.Run(() => { MonitorDeviceBits_X(); });
                                Task.Run(() => { MonitorDeviceBits_Y(); });
                                Task.Run(() => { MonitorDeviceBits_L(); });

                                Task.Run(() => { MonitorDeviceWord_ZR(); });
                                Task.Run(() => { MonitorDeviceDWord_ZR(); });
                            }
                            if (UiManager.appSetting.selectDevice == SaveDevice.LS_XGTServer_TCP)
                            {
                                Task.Run(() => { MonitorDeviceBits_L(); });
                                Task.Run(() => { MonitorDeviceBits_K(); });
                                Task.Run(() => { MonitorDeviceWord_ZR(); });
                                Task.Run(() => { MonitorDeviceDWord_ZR(); });
                            }

                            if (UiManager.appSetting.selectDevice == SaveDevice.LS_XGTServer_COM)
                            {
                                Task.Run(() => { MonitorDeviceBits_L(); });
                                Task.Run(() => { MonitorDeviceWord_ZR(); });
                                Task.Run(() => { MonitorDeviceDWord_ZR(); });
                            }
                            Task.Run(() => { MonitorDeviceWord(); });
                            Task.Run(() => { MonitorDeviceDWord(); });
                        }

                    }

                    Thread.Sleep(10);

                }
            }
            catch (Exception ex)
            {
                logger.Create("MonitorDevice: " + ex.Message, LogLevel.Error);

            }
            
        }
        public void NotUseDevice()
        {
            this.monitorCancellation?.Cancel();
            this.device.Close();
        }

        #region Word_D
        private void MonitorDeviceWord()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceDWord_D và monitorDeviceDWord_D vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceWord_D.ToList();
                    var monitorDeviceClone = new Dictionary<string, short>(this.monitorDeviceWord_D);
                    this.previousStateWord_D = new Dictionary<string, short>(this.monitorDeviceWord_D);
                    
                    // Nếu DeviceLoadDWord được thiết lập là true, reset previousStateDWord
                    if (SystemsManager.Instance.DeviceLoadWord)
                    {
                        this.previousStateWord_D.Clear();
                        foreach (var x in this.monitorDeviceWord_D)
                        {
                            this.previousStateWord_D.Add(x.Key, 0);
                        }
                        SystemsManager.Instance.DeviceLoadWord = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadWord)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x);
                        ushort addressMax = addressDeviceClone.Max(x => x);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 1) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceWord, thực hiện đọc
                        if (lengthLm <= this.limitDeviceWord)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<short> values = new List<short>();
                            if (this.device.ReadMultiWord(DeviceCode.D, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values[i];
                                        this.monitorDeviceWord_D[addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của dữ liệu
                            CheckChangedWord(previousStateWord_D, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x >= addressMin && x <= addressMax);
                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceWord, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x >= addressMin && x <= (addressMin + this.limitDeviceWord - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x);
                            ushort subAddressMax = subAddresses.Max(x => x);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<short> values1 = new List<short>();
                            if (this.device.ReadMultiWord(DeviceCode.D, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values1[i];
                                        this.monitorDeviceWord_D[addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x >= subAddressMin && x <= subAddressMax);

                            // Kiểm tra sự thay đổi của dữ liệu sau khi cập nhật
                            CheckChangedWord(previousStateWord_D, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceDWord: " + ex.Message, LogLevel.Error);
                }
            }

        }
        public void AddAddressDeviceWord_D(ushort addressDevice)
        {
            SystemsManager.Instance.DeviceLoadWord = true;
            if (this.AddressDeviceWord_D == null)
            {
                this.AddressDeviceWord_D = new List<ushort>();
                this.monitorDeviceWord_D = new Dictionary<string, short>();
            }
            if (this.AddressDeviceWord_D.Contains(addressDevice)) return;
            this.previousStateWord_D = new Dictionary<string, short>();
            this.AddressDeviceWord_D.Add(addressDevice);
            if (this.monitorDeviceWord_D.Any(x => x.Key == addressDevice.ToString())) return;
            this.monitorDeviceWord_D.Add(addressDevice.ToString(), 0);
            foreach (var x in this.monitorDeviceWord_D)
            {
                this.previousStateWord_D.Add(x.Key, 0);
            }
        }
        public void RemoveAddressDeviceWord_D(ushort addressDevice)
        {
            if (this.AddressDeviceWord_D == null)
            {
                this.AddressDeviceWord_D = new List<ushort>();
                this.monitorDeviceWord_D = new Dictionary<string, short>();
            }
            if (!this.AddressDeviceWord_D.Contains(addressDevice)) return;
            this.AddressDeviceWord_D.Remove(addressDevice);
            this.monitorDeviceWord_D.Remove(addressDevice.ToString());
        }
        #endregion

        #region DWord_D
        private void MonitorDeviceDWord()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceDWord_D và monitorDeviceDWord_D vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceDWord_D.ToList();
                    var monitorDeviceClone = new Dictionary<string, int>(this.monitorDeviceDWord_D);
                    this.previousStateDWord_D = new Dictionary<string, int>(this.monitorDeviceDWord_D);

                    // Nếu DeviceLoadDWord được thiết lập là true, reset previousStateDWord
                    if (SystemsManager.Instance.DeviceLoadDWord)
                    {
                        this.previousStateDWord_D.Clear();
                        foreach (var x in this.monitorDeviceDWord_D)
                        {
                            this.previousStateDWord_D.Add(x.Key, 0);
                        }
                        SystemsManager.Instance.DeviceLoadDWord = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadDWord)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x);
                        ushort addressMax = addressDeviceClone.Max(x => x);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 2) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceWord, thực hiện đọc
                        if (lengthLm <= this.limitDeviceWord)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<int> values = new List<int>();
                            if (this.device.ReadMultiDoubleWord(DeviceCode.D, addressMin, lengthLm , out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values[i];
                                        this.monitorDeviceDWord_D[addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của dữ liệu
                            CheckChangedDWord(previousStateDWord_D, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x >= addressMin && x <= addressMax);
                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceWord, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x >= addressMin && x <= (addressMin + this.limitDeviceWord - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x);
                            ushort subAddressMax = subAddresses.Max(x => x);
                            int subLength = (subAddressMax + 2) - subAddressMin;

                            List<int> values1 = new List<int>();
                            if (this.device.ReadMultiDoubleWord(DeviceCode.D, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values1[i];
                                        this.monitorDeviceDWord_D[addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x >= subAddressMin && x <= subAddressMax);

                            // Kiểm tra sự thay đổi của dữ liệu sau khi cập nhật
                            CheckChangedDWord(previousStateDWord_D, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceDWord: " + ex.Message, LogLevel.Error);
                }
            }
           
        }
        public void AddAddressDeviceDWord_D(ushort addressDevice)
        {
            try
            {
                SystemsManager.Instance.DeviceLoadDWord = true;
                if (this.AddressDeviceDWord_D == null)
                {
                    this.AddressDeviceDWord_D = new List<ushort>();
                    this.monitorDeviceDWord_D = new Dictionary<string, int>();
                }
                if (this.AddressDeviceDWord_D.Contains(addressDevice)) return;
                this.previousStateDWord_D = new Dictionary<string, int>();

                this.AddressDeviceDWord_D.Add(addressDevice);
                if (this.monitorDeviceDWord_D.Any(x => x.Key == addressDevice.ToString())) return;
                this.monitorDeviceDWord_D.Add(addressDevice.ToString(), 0);
                foreach (var x in this.monitorDeviceDWord_D)
                {
                    this.previousStateDWord_D.Add(x.Key, 0);
                }
            }
            catch (Exception ex)
            {
                logger.Create("AddAddressDeviceDWord_D: " + ex.Message, LogLevel.Error);
            }
        }
        public void RemoveAddressDeviceDWord_D(ushort addressDevice)
        {
            if (this.AddressDeviceDWord_D == null)
            {
                this.AddressDeviceDWord_D = new List<ushort>();
                this.monitorDeviceDWord_D = new Dictionary<string, int>();
            }
            if (!this.AddressDeviceDWord_D.Contains(addressDevice)) return;
            this.AddressDeviceDWord_D.Remove(addressDevice);
            this.monitorDeviceDWord_D.Remove(addressDevice.ToString());
        }
        #endregion

        #region Word_ZR
        private void MonitorDeviceWord_ZR()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceDWord_D và monitorDeviceDWord_D vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceWord_ZR.ToList();
                    var monitorDeviceClone = new Dictionary<string, short>(this.monitorDeviceWord_ZR);
                    this.previousStateWord_ZR = new Dictionary<string, short>(this.monitorDeviceWord_ZR);

                    // Nếu DeviceLoadDWord được thiết lập là true, reset previousStateDWord
                    if (SystemsManager.Instance.DeviceLoadWord_ZR)
                    {
                        this.previousStateWord_ZR.Clear();
                        foreach (var x in this.monitorDeviceWord_ZR)
                        {
                            this.previousStateWord_ZR.Add(x.Key, 0);
                        }
                        SystemsManager.Instance.DeviceLoadWord_ZR = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadWord_ZR)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x);
                        ushort addressMax = addressDeviceClone.Max(x => x);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 1) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceWord, thực hiện đọc
                        if (lengthLm <= this.limitDeviceWord)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<short> values = new List<short>();
                            if (this.device.ReadMultiWord(DeviceCode.ZR, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values[i];
                                        this.monitorDeviceWord_ZR[addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của dữ liệu
                            CheckChangedWord_ZR(previousStateWord_ZR, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x >= addressMin && x <= addressMax);
                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceWord, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x >= addressMin && x <= (addressMin + this.limitDeviceWord - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x);
                            ushort subAddressMax = subAddresses.Max(x => x);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<short> values1 = new List<short>();
                            if (this.device.ReadMultiWord(DeviceCode.ZR, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values1[i];
                                        this.monitorDeviceWord_ZR[addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x >= subAddressMin && x <= subAddressMax);

                            // Kiểm tra sự thay đổi của dữ liệu sau khi cập nhật
                            CheckChangedWord(previousStateWord_ZR, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceDWord: " + ex.Message, LogLevel.Error);
                }
            }

        }

        public void AddAddressDeviceWord_ZR(ushort addressDevice)
        {
            SystemsManager.Instance.DeviceLoadWord_ZR = true;
            if (this.AddressDeviceWord_ZR == null)
            {
                this.AddressDeviceWord_ZR = new List<ushort>();
                this.monitorDeviceWord_ZR = new Dictionary<string, short>();
            }
            if (this.AddressDeviceWord_ZR.Contains(addressDevice)) return;
            this.previousStateWord_ZR = new Dictionary<string, short>();
            this.AddressDeviceWord_ZR.Add(addressDevice);
            if (this.monitorDeviceWord_ZR.Any(x => x.Key == addressDevice.ToString())) return;
            this.monitorDeviceWord_ZR.Add(addressDevice.ToString(), 0);
            foreach (var x in this.monitorDeviceWord_ZR)
            {
                this.previousStateWord_ZR.Add(x.Key, 0);
            }
        }
        public void RemoveAddressDeviceWord_ZR(ushort addressDevice)
        {
            if (this.AddressDeviceWord_ZR == null)
            {
                this.AddressDeviceWord_ZR = new List<ushort>();
                this.monitorDeviceWord_ZR = new Dictionary<string, short>();
            }
            if (!this.AddressDeviceWord_ZR.Contains(addressDevice)) return;
            this.AddressDeviceWord_ZR.Remove(addressDevice);
            this.monitorDeviceWord_ZR.Remove(addressDevice.ToString());
        }
        #endregion

        #region DWord_ZR
        private void MonitorDeviceDWord_ZR()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceDWord_D và monitorDeviceDWord_D vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceDWord_ZR.ToList();
                    var monitorDeviceClone = new Dictionary<string, int>(this.monitorDeviceDWord_ZR);
                    this.previousStateDWord_ZR = new Dictionary<string, int>(this.monitorDeviceDWord_ZR);

                    // Nếu DeviceLoadDWord được thiết lập là true, reset previousStateDWord
                    if (SystemsManager.Instance.DeviceLoadDWord_ZR)
                    {
                        this.previousStateDWord_ZR.Clear();
                        foreach (var x in this.monitorDeviceDWord_ZR)
                        {
                            this.previousStateDWord_ZR.Add(x.Key, 0);
                        }
                        SystemsManager.Instance.DeviceLoadDWord_ZR = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadDWord_ZR)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x);
                        ushort addressMax = addressDeviceClone.Max(x => x);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 2) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceWord, thực hiện đọc
                        if (lengthLm <= this.limitDeviceWord)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<int> values = new List<int>();
                            if (this.device.ReadMultiDoubleWord(DeviceCode.ZR, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values[i];
                                        this.monitorDeviceDWord_ZR[addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của dữ liệu
                            CheckChangedDWord_ZR(previousStateDWord_ZR, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x >= addressMin && x <= addressMax);
                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceWord, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x >= addressMin && x <= (addressMin + this.limitDeviceWord - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x);
                            ushort subAddressMax = subAddresses.Max(x => x);
                            int subLength = (subAddressMax + 2) - subAddressMin;

                            List<int> values1 = new List<int>();
                            if (this.device.ReadMultiDoubleWord(DeviceCode.ZR, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey(addressNew.ToString()))
                                    {
                                        monitorDeviceClone[addressNew.ToString()] = values1[i];
                                        this.monitorDeviceDWord_ZR[addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x >= subAddressMin && x <= subAddressMax);

                            // Kiểm tra sự thay đổi của dữ liệu sau khi cập nhật
                            CheckChangedDWord_ZR(previousStateDWord_ZR, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceDWord: " + ex.Message, LogLevel.Error);
                }
            }

        }
        public void AddAddressDeviceDWord_ZR(ushort addressDevice)
        {
            try
            {
                SystemsManager.Instance.DeviceLoadDWord_ZR = true;
                if (this.AddressDeviceDWord_ZR == null)
                {
                    this.AddressDeviceDWord_ZR = new List<ushort>();
                    this.monitorDeviceDWord_ZR = new Dictionary<string, int>();
                }
                if (this.AddressDeviceDWord_ZR.Contains(addressDevice)) return;
                this.previousStateDWord_ZR = new Dictionary<string, int>();

                this.AddressDeviceDWord_ZR.Add(addressDevice);
                if (this.monitorDeviceDWord_ZR.Any(x => x.Key == addressDevice.ToString())) return;
                this.monitorDeviceDWord_ZR.Add(addressDevice.ToString(), 0);
                foreach (var x in this.monitorDeviceDWord_ZR)
                {
                    this.previousStateDWord_ZR.Add(x.Key, 0);
                }
            }
            catch (Exception ex)
            {
                logger.Create("AddAddressDeviceDWord_ZR: " + ex.Message, LogLevel.Error);
            }
        }
        public void RemoveAddressDeviceDWord_ZR(ushort addressDevice)
        {
            if (this.AddressDeviceDWord_ZR == null)
            {
                this.AddressDeviceDWord_ZR = new List<ushort>();
                this.monitorDeviceDWord_ZR = new Dictionary<string, int>();
            }
            if (!this.AddressDeviceDWord_ZR.Contains(addressDevice)) return;
            this.AddressDeviceDWord_ZR.Remove(addressDevice);
            this.monitorDeviceDWord_ZR.Remove(addressDevice.ToString());
        }
        #endregion


        #region Bit M

        private void MonitorDeviceBits_M()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceBits_M và monitorDeviceBits_M vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceBits_M.ToList();
                    var monitorDeviceClone = new Dictionary<string, bool>(this.monitorDeviceBits_M);
                    this.previousStateBits_M = new Dictionary<string, bool>(this.monitorDeviceBits_M);

                    // Nếu DeviceLoadBit_M được thiết lập là true, reset previousStateBits_M
                    if (SystemsManager.Instance.DeviceLoadBit_M)
                    {
                        this.previousStateBits_M.Clear();
                        foreach (var x in this.monitorDeviceBits_M)
                        {
                            this.previousStateBits_M.Add(x.Key, false);
                        }
                        SystemsManager.Instance.DeviceLoadBit_M = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadBit_M)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x.address);
                        ushort addressMax = addressDeviceClone.Max(x => x.address);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 1) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceBits, thực hiện đọc
                        if (lengthLm <= this.limitDeviceBits)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<bool> values = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.M, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey("M" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["M" + addressNew.ToString()] = values[i];
                                        this.monitorDeviceBits_M["M" + addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của bits
                            CheckChangedBits(previousStateBits_M, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x.address >= addressMin && x.address <= addressMax);

                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceBits, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x.address >= addressMin && x.address <= (addressMin + this.limitDeviceBits - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x.address);
                            ushort subAddressMax = subAddresses.Max(x => x.address);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<bool> values1 = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.M, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey("M" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["M" + addressNew.ToString()] = values1[i];
                                        this.monitorDeviceBits_M["M" + addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x.address >= subAddressMin && x.address <= subAddressMax);

                            // Kiểm tra sự thay đổi của bits sau khi cập nhật
                            CheckChangedBits(previousStateBits_M, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        //break;
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceBits_M: " + ex.Message, LogLevel.Error);
                }
            }
              
        }
        private void AddAddressDeviceBits_M(string deviceType, ushort addressDevice)
        {
            SystemsManager.Instance.DeviceLoadBit_M = true;
            DeviceItem item = new DeviceItem();
            item.type = deviceType;
            item.address = addressDevice;
            if (this.AddressDeviceBits_M == null)
            {
                this.AddressDeviceBits_M = new List<DeviceItem>();
                this.monitorDeviceBits_M = new Dictionary<string, bool>();
            }
            if (this.AddressDeviceBits_M.Any(x => x.address == addressDevice)) return;
            this.previousStateBits_M = new Dictionary<string, bool>();
            this.AddressDeviceBits_M.Add(item);
            if (this.monitorDeviceBits_M.Any(x => x.Key == deviceType + addressDevice.ToString())) return;
            this.monitorDeviceBits_M.Add(item.type + item.address, false);
            this.ClearPreviousStateBits(this.previousStateBits_M, this.monitorDeviceBits_M);
        }
        private void RemoveAddressDeviceBits_M(string deviceType, ushort addressDevice)
        {
            DeviceItem item = new DeviceItem();
            item.type = deviceType;
            item.address = addressDevice;
            if (this.AddressDeviceBits_M == null)
            {
                this.AddressDeviceBits_M = new List<DeviceItem>();
                this.monitorDeviceBits_M = new Dictionary<string, bool>();
            }
            //if (!this.AddressDeviceBits_M.Any(x => x.address == addressDevice)) return;
            int indexToRemove = AddressDeviceBits_M.FindIndex(x => x.address == addressDevice);
            if (indexToRemove != -1)
            {
                AddressDeviceBits_M.RemoveAt(indexToRemove);
            }
            this.AddressDeviceBits_M.Remove(item);
            //if (item.type + item.address == "M3529")
            //{
            //    string x = "1";
            //}
            this.monitorDeviceBits_M.Remove(item.type + item.address);

        }
        #endregion


        #region Bit X
        private void MonitorDeviceBits_X()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceBits_M và monitorDeviceBits_M vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceBits_X.ToList();
                    var monitorDeviceClone = new Dictionary<string, bool>(this.monitorDeviceBits_X);
                    this.previousStateBits_X = new Dictionary<string, bool>(this.monitorDeviceBits_X);

                    // Nếu DeviceLoadBit_M được thiết lập là true, reset previousStateBits_M
                    if (SystemsManager.Instance.DeviceLoadBit_X)
                    {
                        this.previousStateBits_X.Clear();
                        foreach (var x in this.monitorDeviceBits_X)
                        {
                            this.previousStateBits_X.Add(x.Key, false);
                        }
                        SystemsManager.Instance.DeviceLoadBit_X = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadBit_X)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x.address);
                        ushort addressMax = addressDeviceClone.Max(x => x.address);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 1) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceBits, thực hiện đọc
                        if (lengthLm <= this.limitDeviceBits)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<bool> values = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.X, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey("X" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["X" + addressNew.ToString()] = values[i];
                                        this.monitorDeviceBits_X["X" + addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của bits
                            CheckChangedBits(previousStateBits_X, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x.address >= addressMin && x.address <= addressMax);

                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceBits, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x.address >= addressMin && x.address <= (addressMin + this.limitDeviceBits - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x.address);
                            ushort subAddressMax = subAddresses.Max(x => x.address);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<bool> values1 = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.X, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey("X" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["X" + addressNew.ToString()] = values1[i];
                                        this.monitorDeviceBits_X["X" + addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x.address >= subAddressMin && x.address <= subAddressMax);

                            // Kiểm tra sự thay đổi của bits sau khi cập nhật
                            CheckChangedBits(previousStateBits_X, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        //break;
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceBits_M: " + ex.Message, LogLevel.Error);
                }
            }
          
        }
        private void AddAddressDeviceBits_X(string deviceType, ushort addressDevice)
        {
            DeviceItem item = new DeviceItem();
            SystemsManager.Instance.DeviceLoadBit_X = true;
            item.type = deviceType;
            item.address = addressDevice;
            if (this.AddressDeviceBits_X == null)
            {
                this.AddressDeviceBits_X = new List<DeviceItem>();
                this.monitorDeviceBits_X = new Dictionary<string, bool>();
            }
            if (this.AddressDeviceBits_X.Any(x => x.address == addressDevice)) return;
            this.previousStateBits_X = new Dictionary<string, bool>();
            this.AddressDeviceBits_X.Add(item);
            if (this.monitorDeviceBits_X.Any(x => x.Key == deviceType + addressDevice.ToString())) return;
            this.monitorDeviceBits_X.Add(item.type + item.address, false);
            this.ClearPreviousStateBits(this.previousStateBits_X, this.monitorDeviceBits_X);
        }
        private void RemoveAddressDeviceBits_X(string deviceType, ushort addressDevice)
        {
            DeviceItem item = new DeviceItem();
            item.type = deviceType;
            item.address = addressDevice;
            if (this.AddressDeviceBits_X == null)
            {
                this.AddressDeviceBits_X = new List<DeviceItem>();
                this.monitorDeviceBits_X = new Dictionary<string, bool>();
            }
            if (!this.AddressDeviceBits_X.Any(x => x.address == addressDevice)) return;
            int indexToRemove = AddressDeviceBits_X.FindIndex(x => x.address == addressDevice);
            if (indexToRemove != -1)
            {
                AddressDeviceBits_X.RemoveAt(indexToRemove);
            }
            this.monitorDeviceBits_X.Remove(item.type + item.address);
        }
        #endregion


        #region Bit Y
        private void MonitorDeviceBits_Y()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceBits_M và monitorDeviceBits_M vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceBits_Y.ToList();
                    var monitorDeviceClone = new Dictionary<string, bool>(this.monitorDeviceBits_Y);
                    this.previousStateBits_Y = new Dictionary<string, bool>(this.monitorDeviceBits_Y);

                    // Nếu DeviceLoadBit_M được thiết lập là true, reset previousStateBits_M
                    if (SystemsManager.Instance.DeviceLoadBit_Y)
                    {
                        this.previousStateBits_Y.Clear();
                        foreach (var x in this.monitorDeviceBits_Y)
                        {
                            this.previousStateBits_Y.Add(x.Key, false);
                        }
                        SystemsManager.Instance.DeviceLoadBit_Y = false;
                    }

                    // Vòng lặp chính để duyệt qua địa chỉ thiết bị
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadBit_Y)
                    {
                        // Lọc địa chỉ min và max trong danh sách còn lại
                        ushort addressMin = addressDeviceClone.Min(x => x.address);
                        ushort addressMax = addressDeviceClone.Max(x => x.address);

                        // Tính độ dài đoạn cần đọc
                        int lengthLm = (addressMax + 1) - addressMin;

                        // Nếu độ dài đoạn nhỏ hơn hoặc bằng limitDeviceBits, thực hiện đọc
                        if (lengthLm <= this.limitDeviceBits)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<bool> values = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.Y, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey("Y" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["Y" + addressNew.ToString()] = values[i];
                                        this.monitorDeviceBits_Y["Y" + addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                            // Kiểm tra sự thay đổi của bits
                            CheckChangedBits(previousStateBits_Y, monitorDeviceClone);
                            // Loại bỏ các phần tử đã xử lý
                            addressDeviceClone.RemoveAll(x => x.address >= addressMin && x.address <= addressMax);

                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceBits, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x.address >= addressMin && x.address <= (addressMin + this.limitDeviceBits - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x.address);
                            ushort subAddressMax = subAddresses.Max(x => x.address);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<bool> values1 = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.Y, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey("Y" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["Y" + addressNew.ToString()] = values1[i];
                                        this.monitorDeviceBits_Y["Y" + addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            // Loại bỏ các phần tử đã xử lý khỏi danh sách
                            addressDeviceClone.RemoveAll(x => x.address >= subAddressMin && x.address <= subAddressMax);

                            // Kiểm tra sự thay đổi của bits sau khi cập nhật
                            CheckChangedBits(previousStateBits_Y, monitorDeviceClone);
                        }

                        // Sau khi xử lý xong, nếu còn địa chỉ, tiếp tục
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        //break;
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceBits_M: " + ex.Message, LogLevel.Error);
                }
            }
           
        }
        private void AddAddressDeviceBits_Y(string deviceType, ushort addressDevice)
        {
            SystemsManager.Instance.DeviceLoadBit_Y = true;
            DeviceItem item = new DeviceItem();
            item.type = deviceType;
            item.address = addressDevice;
            if (this.AddressDeviceBits_Y == null)
            {
                this.AddressDeviceBits_Y = new List<DeviceItem>();
                this.monitorDeviceBits_Y = new Dictionary<string, bool>();
            }
            if (this.AddressDeviceBits_Y.Any(x => x.address == addressDevice)) return;
            this.previousStateBits_Y = new Dictionary<string, bool>();
            this.AddressDeviceBits_Y.Add(item);
            if (this.monitorDeviceBits_Y.Any(x => x.Key == deviceType + addressDevice.ToString())) return;
            this.monitorDeviceBits_Y.Add(item.type + item.address, false);
            this.ClearPreviousStateBits(this.previousStateBits_Y, this.monitorDeviceBits_Y);
        }
        private void RemoveAddressDeviceBits_Y(string deviceType, ushort addressDevice)
        {
            DeviceItem item = new DeviceItem();
            item.type = deviceType;
            item.address = addressDevice;
            if (this.AddressDeviceBits_Y == null)
            {
                this.AddressDeviceBits_Y = new List<DeviceItem>();
                this.monitorDeviceBits_Y = new Dictionary<string, bool>();
            }
            if (!this.AddressDeviceBits_Y.Any(x => x.address == addressDevice)) return;
            int indexToRemove = AddressDeviceBits_Y.FindIndex(x => x.address == addressDevice);
            if (indexToRemove != -1)
            {
                AddressDeviceBits_Y.RemoveAt(indexToRemove);
            }
            this.monitorDeviceBits_Y.Remove(item.type + item.address);
        }
        #endregion


        #region Bit L
        private void MonitorDeviceBits_L()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceBits_M và monitorDeviceBits_M vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceBits_L.ToList();
                    var monitorDeviceClone = new Dictionary<string, bool>(this.monitorDeviceBits_L);
                    this.previousStateBits_L = new Dictionary<string, bool>(this.monitorDeviceBits_L);

                    // Nếu DeviceLoadBit_M được thiết lập là true, reset previousStateBits_M
                    if (SystemsManager.Instance.DeviceLoadBit_L)
                    {
                        this.previousStateBits_L.Clear();
                        foreach (var x in this.monitorDeviceBits_L)
                        {
                            this.previousStateBits_L.Add(x.Key, false);
                        }
                        SystemsManager.Instance.DeviceLoadBit_L = false;
                    }

                  
                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadBit_L)
                    {
                       
                        ushort addressMin = addressDeviceClone.Min(x => x.address);
                        ushort addressMax = addressDeviceClone.Max(x => x.address);

                       
                        int lengthLm = (addressMax + 1) - addressMin;

                      
                        if (lengthLm <= this.limitDeviceBits)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<bool> values = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.L, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey("L" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["L" + addressNew.ToString()] = values[i];
                                        this.monitorDeviceBits_L["L" + addressNew.ToString()] = values[i];
                                    }
                                }
                            }

                           
                            CheckChangedBits(previousStateBits_L, monitorDeviceClone);
                          
                            addressDeviceClone.RemoveAll(x => x.address >= addressMin && x.address <= addressMax);

                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceBits, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x.address >= addressMin && x.address <= (addressMin + this.limitDeviceBits - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x.address);
                            ushort subAddressMax = subAddresses.Max(x => x.address);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<bool> values1 = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.L, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey("L" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["L" + addressNew.ToString()] = values1[i];
                                        this.monitorDeviceBits_L["L" + addressNew.ToString()] = values1[i];
                                    }
                                }
                            }

                            
                            addressDeviceClone.RemoveAll(x => x.address >= subAddressMin && x.address <= subAddressMax);

                            
                            CheckChangedBits(previousStateBits_L, monitorDeviceClone);
                        }

                        
                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceBits_M: " + ex.Message, LogLevel.Error);
                }
            }
           
        }
        private void AddAddressDeviceBits_L(string deviceType, ushort addressDevice)
        {
            try
            {
                SystemsManager.Instance.DeviceLoadBit_L = true;
                DeviceItem item = new DeviceItem();
                item.type = deviceType;
                item.address = addressDevice;
                if (this.AddressDeviceBits_L == null)
                {
                    this.AddressDeviceBits_L = new List<DeviceItem>();
                    this.monitorDeviceBits_L = new Dictionary<string, bool>();
                }
                if (this.AddressDeviceBits_L.Any(x => x.address == addressDevice)) return;
                this.previousStateBits_L = new Dictionary<string, bool>();
                this.AddressDeviceBits_L.Add(item);
                if (this.monitorDeviceBits_L.Any(x => x.Key == deviceType + addressDevice.ToString())) return;
                this.monitorDeviceBits_L.Add(item.type + item.address, false);
                this.ClearPreviousStateBits(this.previousStateBits_L, this.monitorDeviceBits_L);
            }
            catch (Exception ex)
            {
                logger.Create("ERROR  AddAddressDeviceBits_L:" + ex.Message, LogLevel.Error);

            }
            
        }
        private void RemoveAddressDeviceBits_L(string deviceType, ushort addressDevice)
        {
            try
            {
                DeviceItem item = new DeviceItem();
                item.type = deviceType;
                item.address = addressDevice;
                if (this.AddressDeviceBits_L == null)
                {
                    this.AddressDeviceBits_L = new List<DeviceItem>();
                    this.monitorDeviceBits_L = new Dictionary<string, bool>();
                }
                if (!this.AddressDeviceBits_L.Any(x => x.address == addressDevice)) return;
                int indexToRemove = AddressDeviceBits_L.FindIndex(x => x.address == addressDevice);
                if (indexToRemove != -1)
                {
                    AddressDeviceBits_L.RemoveAt(indexToRemove);
                }
                this.monitorDeviceBits_L.Remove(item.type + item.address);
            }
            catch (Exception ex)
            {

                logger.Create("ERROR  RemoveAddressDeviceBits_L:" + ex.Message, LogLevel.Error);
            }
           
        }
        #endregion

        #region Bit K PLC LS
        private void MonitorDeviceBits_K()
        {
            lock (iLock)
            {
                try
                {
                    // Sao chép collection AddressDeviceBits_M và monitorDeviceBits_M vào các danh sách tạm
                    var addressDeviceClone = this.AddressDeviceBits_K.ToList();
                    var monitorDeviceClone = new Dictionary<string, bool>(this.monitorDeviceBits_K);
                    this.previousStateBits_K = new Dictionary<string, bool>(this.monitorDeviceBits_K);

                    // Nếu DeviceLoadBit_M được thiết lập là true, reset previousStateBits_M
                    if (SystemsManager.Instance.DeviceLoadBit_K)
                    {
                        this.previousStateBits_K.Clear();
                        foreach (var x in this.monitorDeviceBits_K)
                        {
                            this.previousStateBits_K.Add(x.Key, false);
                        }
                        SystemsManager.Instance.DeviceLoadBit_K = false;
                    }


                    while (addressDeviceClone.Count > 0 && !SystemsManager.Instance.DeviceLoadBit_K)
                    {

                        ushort addressMin = addressDeviceClone.Min(x => x.address);
                        ushort addressMax = addressDeviceClone.Max(x => x.address);


                        int lengthLm = (addressMax + 1) - addressMin;


                        if (lengthLm <= this.limitDeviceBits)
                        {
                            if (lengthLm == 0)
                            {
                                lengthLm = 1;
                            }

                            List<bool> values = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.K_LS, addressMin, lengthLm, out values))
                            {
                                for (int i = 0; i < values.Count; i++)
                                {
                                    int addressNew = addressMin + i;
                                    if (monitorDeviceClone.ContainsKey("K_LS" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["K_LS" + addressNew.ToString()] = values[i];
                                        this.monitorDeviceBits_K["K_LS" + addressNew.ToString()] = values[i];
                                    }
                                }
                            }


                            CheckChangedBits(previousStateBits_K, monitorDeviceClone);

                            addressDeviceClone.RemoveAll(x => x.address >= addressMin && x.address <= addressMax);

                        }
                        else
                        {
                            // Trường hợp độ dài đoạn lớn hơn limitDeviceBits, chia nhỏ ra
                            var subAddresses = addressDeviceClone
                                .Where(x => x.address >= addressMin && x.address <= (addressMin + this.limitDeviceBits - 1))
                                .ToList();

                            ushort subAddressMin = subAddresses.Min(x => x.address);
                            ushort subAddressMax = subAddresses.Max(x => x.address);
                            int subLength = (subAddressMax + 1) - subAddressMin;

                            List<bool> values1 = new List<bool>();
                            if (this.device.ReadMultiBits(DeviceCode.K_LS, subAddressMin, subLength, out values1))
                            {
                                for (int i = 0; i < values1.Count; i++)
                                {
                                    int addressNew = subAddressMin + i;
                                    if (monitorDeviceClone.ContainsKey("K_LS" + addressNew.ToString()))
                                    {
                                        monitorDeviceClone["K_LS" + addressNew.ToString()] = values1[i];
                                        this.monitorDeviceBits_K["K_LS" + addressNew.ToString()] = values1[i];
                                    }
                                }
                            }


                            addressDeviceClone.RemoveAll(x => x.address >= subAddressMin && x.address <= subAddressMax);


                            CheckChangedBits(previousStateBits_K, monitorDeviceClone);
                        }


                        if (addressDeviceClone.Count > 0)
                        {
                            continue;
                        }
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    logger.Create("MonitorDeviceBits_K: " + ex.Message, LogLevel.Error);
                }
            }

        }
        private void AddAddressDeviceBits_K(string deviceType, ushort addressDevice)
        {
            try
            {
                SystemsManager.Instance.DeviceLoadBit_K = true;
                DeviceItem item = new DeviceItem();
                item.type = deviceType;
                item.address = addressDevice;
                if (this.AddressDeviceBits_K == null)
                {
                    this.AddressDeviceBits_K = new List<DeviceItem>();
                    this.monitorDeviceBits_K = new Dictionary<string, bool>();
                }
                if (this.AddressDeviceBits_K.Any(x => x.address == addressDevice)) return;
                this.previousStateBits_K = new Dictionary<string, bool>();
                this.AddressDeviceBits_K.Add(item);
                if (this.monitorDeviceBits_K.Any(x => x.Key == deviceType + addressDevice.ToString())) return;
                this.monitorDeviceBits_K.Add(item.type + item.address, false);
                this.ClearPreviousStateBits(this.previousStateBits_K, this.monitorDeviceBits_K);
            }
            catch (Exception ex)
            {
                logger.Create("ERROR  AddAddressDeviceBits_L:" + ex.Message, LogLevel.Error);

            }

        }
        private void RemoveAddressDeviceBits_K(string deviceType, ushort addressDevice)
        {
            try
            {
                DeviceItem item = new DeviceItem();
                item.type = deviceType;
                item.address = addressDevice;
                if (this.AddressDeviceBits_K == null)
                {
                    this.AddressDeviceBits_K = new List<DeviceItem>();
                    this.monitorDeviceBits_K = new Dictionary<string, bool>();
                }
                if (!this.AddressDeviceBits_K.Any(x => x.address == addressDevice)) return;
                int indexToRemove = AddressDeviceBits_K.FindIndex(x => x.address == addressDevice);
                if (indexToRemove != -1)
                {
                    AddressDeviceBits_K.RemoveAt(indexToRemove);
                }
                this.monitorDeviceBits_K.Remove(item.type + item.address);
            }
            catch (Exception ex)
            {

                logger.Create("ERROR  RemoveAddressDeviceBits_K:" + ex.Message, LogLevel.Error);
            }

        }
        #endregion


        #region Check Changed Bit
        private void CheckChangedBits(Dictionary<string, bool> previousState, Dictionary<string, bool> currentState)
        {
            try
            {
                List<string> changedKeys = GetChangedKeysBits(previousState, currentState);
                if (changedKeys.Count > 0)
                {
                    foreach (var key in changedKeys)
                    {
                        NotifyStatusChangedBits(key, currentState[key]);
                    }
                }
                //Cập nhật previousState
                previousState.Clear();
                foreach (var kvp in currentState)
                {
                    previousState[kvp.Key] = kvp.Value;
                }
            }
            catch (Exception ex)
            {
                logger.Create("CheckChangedBits:" + ex.Message, LogLevel.Error);
            }
        }
        private List<string> GetChangedKeysBits(Dictionary<string, bool> previousState, Dictionary<string, bool> currentState)
        {
            List<string> changedKeys = new List<string>();
            try
            {
                foreach (var kvp in currentState)
                {
                    if (previousState.TryGetValue(kvp.Key, out var prevValue) && kvp.Value != prevValue)
                    {
                        changedKeys.Add(kvp.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("GetChangedKeysBits: " + ex.Message, LogLevel.Error);
            }
            return changedKeys;
        }

        private void ClearPreviousStateBits(Dictionary<string, bool> previousState, Dictionary<string, bool> OrgState)
        {
            foreach (var x in OrgState)
            {
                previousState.Add(x.Key, false);
            }
        }
        public void AddBitAddress(string deviceName, ushort address)
        {
            try
            {
                if (deviceName == DeviceCode.M.ToString())
                {
                    this.AddAddressDeviceBits_M(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.X.ToString())
                {
                    this.AddAddressDeviceBits_X(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.Y.ToString())
                {
                    this.AddAddressDeviceBits_Y(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.L.ToString())
                {
                    this.AddAddressDeviceBits_L(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.K_LS.ToString())
                {
                    this.AddAddressDeviceBits_K(deviceName.ToString(), address);
                }
            }
            catch (Exception ex)
            {

                logger.Create($"AddBítAddress Error : {ex}",LogLevel.Error);
            }
           
        }
        public void RemoveBitAddress(string deviceName, ushort address)
        {
            try
            {
                if (deviceName == DeviceCode.M.ToString())
                {
                    this.RemoveAddressDeviceBits_M(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.X.ToString())
                {
                    this.RemoveAddressDeviceBits_X(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.Y.ToString())
                {
                    this.RemoveAddressDeviceBits_Y(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.L.ToString())
                {
                    this.RemoveAddressDeviceBits_L(deviceName.ToString(), address);
                }
                else if (deviceName == DeviceCode.K_LS.ToString())
                {
                    this.RemoveAddressDeviceBits_K(deviceName.ToString(), address);
                }
            }
            catch (Exception ex)
            {

                logger.Create($"RemoveBitAddress Error : {ex}", LogLevel.Error);
            }
           
        }
        private void NotifyStatusChangedBits(string key, bool status)
        {
            try
            {
                this.notifyPLCBits.NotifyChangeBits(key, status);
            }
            catch (Exception ex)
            {
                logger.Create("NotifyStatusChangedBits: " + ex.Message, LogLevel.Error);
            }
        }
        #endregion

        #region Check Changed Word_D
        private void CheckChangedWord(Dictionary<string, short> previousState, Dictionary<string, short> currentState)
        {
            List<string> changedKeys = GetChangedKeysWord(previousState, currentState);
            if (changedKeys.Count > 0)
            {
                foreach (var key in changedKeys)
                {
                    NotifyStatusChangedWord(key, currentState[key]);
                }
            }
            // Cập nhật previousState
            previousState.Clear();
            foreach (var kvp in currentState)
            {
                previousState[kvp.Key] = kvp.Value;
            }
        }
        private List<string> GetChangedKeysWord(Dictionary<string, short> previousState, Dictionary<string, short> currentState)
        {
            List<string> changedKeys = new List<string>();

            foreach (var kvp in currentState)
            {
                if (previousState.TryGetValue(kvp.Key, out var prevValue) && kvp.Value != prevValue || kvp.Value == 0)
                {
                    changedKeys.Add(kvp.Key);
                }
            }
            return changedKeys;
        }
        private void NotifyStatusChangedWord(string key, short value)
        {
            this.notifyPLCWord.NotifyChangeWord(key, value);
        }
        #endregion

        #region Check Changed DWord_D
        private void CheckChangedDWord(Dictionary<string, int> previousState, Dictionary<string, int> currentState)
        {
            List<string> changedKeys = GetChangedKeysDWord(previousState, currentState);
            if (changedKeys.Count > 0)
            {
                foreach (var key in changedKeys)
                {
                    NotifyStatusChangedDWord(key, currentState[key]);
                }
            }
            // Cập nhật previousState
            previousState.Clear();
            foreach (var kvp in currentState)
            {
                previousState[kvp.Key] = kvp.Value;
            }
        }
        private List<string> GetChangedKeysDWord(Dictionary<string, int> previousState, Dictionary<string, int> currentState)
        {
            List<string> changedKeys = new List<string>();

            foreach (var kvp in currentState)
            {
                if (previousState.TryGetValue(kvp.Key, out var prevValue) && kvp.Value != prevValue || kvp.Value == 0)
                {
                    changedKeys.Add(kvp.Key);
                }
            }
            return changedKeys;
        }
        private void NotifyStatusChangedDWord(string key, int value)
        {
            this.notifyPLCDWord.NotifyChangeDWord(key, value);
        }
        #endregion

        #region Check Changed Word_ZR
        private void CheckChangedWord_ZR(Dictionary<string, short> previousState, Dictionary<string, short> currentState)
        {
            List<string> changedKeys = GetChangedKeysWord_ZR(previousState, currentState);
            if (changedKeys.Count > 0)
            {
                foreach (var key in changedKeys)
                {
                    NotifyStatusChangedWord_ZR(key, currentState[key]);
                }
            }
            // Cập nhật previousState
            previousState.Clear();
            foreach (var kvp in currentState)
            {
                previousState[kvp.Key] = kvp.Value;
            }
        }
        private List<string> GetChangedKeysWord_ZR(Dictionary<string, short> previousState, Dictionary<string, short> currentState)
        {
            List<string> changedKeys = new List<string>();

            foreach (var kvp in currentState)
            {
                if (previousState.TryGetValue(kvp.Key, out var prevValue) && kvp.Value != prevValue || kvp.Value == 0)
                {
                    changedKeys.Add(kvp.Key);
                }
            }
            return changedKeys;
        }
        private void NotifyStatusChangedWord_ZR(string key, short value)
        {
            this.notifyPLCWord_ZR.NotifyChangeWord_ZR(key, value);
        }
        #endregion


        #region Check Changed DWork_ZR
        private void CheckChangedDWord_ZR(Dictionary<string, int> previousState, Dictionary<string, int> currentState)
        {
            List<string> changedKeys = GetChangedKeysDWord_ZR(previousState, currentState);
            if (changedKeys.Count > 0)
            {
                foreach (var key in changedKeys)
                {
                    NotifyStatusChangedDWord_ZR(key, currentState[key]);
                }
            }
            // Cập nhật previousState
            previousState.Clear();
            foreach (var kvp in currentState)
            {
                previousState[kvp.Key] = kvp.Value;
            }
        }
        private List<string> GetChangedKeysDWord_ZR(Dictionary<string, int> previousState, Dictionary<string, int> currentState)
        {
            List<string> changedKeys = new List<string>();

            foreach (var kvp in currentState)
            {
                if (previousState.TryGetValue(kvp.Key, out var prevValue) && kvp.Value != prevValue || kvp.Value == 0)
                {
                    changedKeys.Add(kvp.Key);
                }
            }
            return changedKeys;
        }
        private void NotifyStatusChangedDWord_ZR(string key, int value)
        {
            this.notifyPLCDWord_ZR.NotifyChangeDWord_ZR(key, value);
        }
        #endregion

        #endregion
    }
    public class DeviceItem
    {
        public string type { get; set; }
        public ushort address { get; set; }
    }
}
