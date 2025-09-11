using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Windows;


namespace Development
{
    
    class MESSendPCB
      
    {


        private static MyLogger logger = new MyLogger("MESSendPCB");

        //Notify
        protected NotifyEvenMES notifyEvenMES;
        // TCP server:
        protected TcpListener listener;
        protected TcpClient mesClients;
        protected bool isRunning = false;
        public bool isAccept = false;
        public string IpServer;
        public string PortServer;
        //
        protected bool isReceiver = false;
        protected string DataReceiver { get; set; }

        public MESSendPCB(string ip, int port)
        {
            try
            {
                this.LoadNotifyEvenMES();
                this.listener = new TcpListener(IPAddress.Parse(ip), port);
            }
            catch (Exception ex)
            {
                logger.Create("BaseRepositoryMES :" + ex.Message, LogLevel.Error);
            }
        }
        private void LoadNotifyEvenMES()
        {
            this.notifyEvenMES = SystemsManager.Instance.NotifyEvenMES;
        }
        public bool CheckConnect( out string IP ,out string Port)
        {
            if(isAccept)
            {
                IP = IpServer.ToString();
                Port = PortServer.ToString();
                return true;
                
            }
            IP = "No Connect";
            Port = "No Port";
            return false;
        }
        public async Task Start()
        {
            if (this.isRunning)
            {
                //this.notifyEvenMES.NotifyToUI("Notify : MES Is Opend!!!");
                return;
            }
            try
            {
                this.listener.Start();
                this.isRunning = true;
                //this.notifyEvenMES.NotifyToUI("Notify : Listen MES...!!!");
                while (isRunning)
                {
                    TcpClient mesClient = await listener.AcceptTcpClientAsync();
                    if (mesClients == null)
                    {
                        mesClients = mesClient;

                        //this.notifyEvenMES.NotifyToUI("Notify : Server Connected To MES!!!");
                        this.notifyEvenMES.NotifyMESConnect(true);
                        // Lấy địa chỉ IP của client
                        string clientIP = ((IPEndPoint)mesClient.Client.RemoteEndPoint).Address.ToString();
                        int clientPort = ((IPEndPoint)mesClient.Client.RemoteEndPoint).Port;
                        IpServer = clientIP;
                        PortServer = clientPort.ToString();
                        //this.notifyEvenMES.GetInformationFromClientConnect(clientIP, clientPort);
                        this.isAccept = true;
                        //this.notifyEvenMES.NotifyToUI($"Notify : Client connected from {clientIP}:{clientPort}");
                        await Task.Run(() => HandleMesClient(mesClients));
                    }
                    else
                    {
                        //this.notifyEvenMES.NotifyToUI("Notify : MES Is Disconnect!!!");
                        this.notifyEvenMES.NotifyMESConnect(false);
                        this.isAccept = false;
                        mesClient.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //this.notifyEvenMES.NotifyToUI("Notify : MES Connect Faild " + ex.Message);
                logger.Create("MES Connect Faild " + ex.Message, LogLevel.Error);
                //this.notifyEvenMES.NotifyMESConnect(false);
                this.isAccept = false;
            }
        }
        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                listener.Stop();
                mesClients.Close();
                mesClients = null;
                //this.notifyEvenMES.NotifyToUI("Notify : Server Is Closed!!!");
                //this.notifyEvenMES.NotifyMESConnect(false);
                this.isAccept = false;
            }
        }
        public bool CheckMESConnection()
        {
            return mesClients != null && mesClients.Connected;
        }
        public async Task HandleMesClient(TcpClient mesClient)
        {
            NetworkStream stream = mesClient.GetStream();
            byte[] buffer = new byte[21000];
            while (true)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        //this.notifyEvenMES.NotifyToUI("Notify : Connect From MES Is Closed!!!");
                        //this.notifyEvenMES.NotifyMESConnect(false);
                        this.isAccept = false;
                        mesClient.Close();
                        await ReconnectToMES();
                        break;
                    }
                    string requestData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string responseData = requestData;
                    string equip = UiManager.appSetting.LotinData.WorkGroup;
                    equip = equip.PadRight(9, ' ');
                    if (!responseData.Contains(equip + "M001"))
                    {
                        this.DataReceiver = responseData;
                        this.isReceiver = true;
                    }

                    this.notifyEvenMES.NotifyMESResult($"MES RECEIVED :"+responseData);
                }
                catch (Exception ex)
                {
                    //this.notifyEvenMES.NotifyToUI("Notify : Error Access MES : " + ex.Message);
                    //this.notifyEvenMES.NotifyMESConnect(false);
                    logger.Create($@"MESSendPCD ERROR :{ex}", LogLevel.Error);
                    this.isAccept = false;
                    mesClient.Close();
                    isRunning = false;
                    mesClients = null;
                    break;
                }
            }
        }
        public async Task SendToMes(byte[] txBuf)
        {

            if (!isRunning)
            {
                //this.notifyEvenMES.NotifyToUI("Notify : Server Is Closed or MES is Closed : ");
                //this.notifyEvenMES.NotifyMESConnect(false);
                this.isAccept = false;
                return;
            }
            NetworkStream stream = mesClients.GetStream();
            await stream.WriteAsync(txBuf, 0, txBuf.Length);
            byte[] buffer = new byte[1024];
        }
        private async Task ReconnectToMES()
        {
            isRunning = false;
            mesClients = null;
            await Start();
        }
        public async Task<MesCheck> SendParam(MesCheck entity, string CH)
        {
            this.isReceiver = false;
            try
            {
                if (!this.CheckMESConnection())
                {
                    logger.Create(" -> TCP connection not ready -> discard sending SendReady!", LogLevel.Warning);
                    return null;
                }
                var packet = new List<byte>();
                //HEADER
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.EquipmentId));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.Status));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.DIV));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                // BODY
                List<object> DATA = new List<object>();
                DATA.Add(entity.FormatP005);
                var formatP005Object = new { DATA };
                string formatP005 = JsonConvert.SerializeObject(formatP005Object);
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(formatP005));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                //Check SUM
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.CheckSum));

                var txBuf = packet.ToArray();
                logger.Create($@"MES.SEND PCB{CH}:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);
             
               
                await this.SendToMes(txBuf);
              
                await WaitMESReturnData();
                if (this.isReceiver && !string.IsNullOrEmpty(this.DataReceiver))
                {
                    this.isReceiver = false;
                    logger.Create($@"MES.RECEIVER PCB{CH}:" + this.DataReceiver, LogLevel.Information);
                   
                    return FilterData(this.DataReceiver, entity, CH);
                }
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{CH}]: No Response from MES");
                logger.Create($@"Notify [MES{CH}]: No Response from MES",LogLevel.Warning);
            }
            catch (Exception ex)
            {
                logger.Create($@"Send{CH} : " + ex.Message, LogLevel.Error);
            }
            return null;
        }
        public async Task<MesCheck> SendPCB(MesCheck entity, string CH)
        {
            this.isReceiver = false;
            try
            {
                if (!this.CheckMESConnection())
                {
                    logger.Create(" -> TCP connection not ready -> discard sending SendReady!", LogLevel.Warning);
                    return null;
                }
                var packet = new List<byte>();
                //HEADER
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.EquipmentId));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.Status));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.DIV));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                // BODY
                if (entity.SelectSendMes)
                {
                    List<object> DATA = new List<object>();
                    DATA.Add(entity.FormatP230);
                    var formatP250Object = new { DATA };
                    string formatP250 = JsonConvert.SerializeObject(formatP250Object);
                    packet.AddRange(ASCIIEncoding.ASCII.GetBytes(formatP250));
                }
                else
                {
                    List<object> DATA = new List<object>();
                    DATA.Add(entity.FormatP240);
                    var formatP260Object = new { DATA };
                    string formatP260 = JsonConvert.SerializeObject(formatP260Object);
                    packet.AddRange(ASCIIEncoding.ASCII.GetBytes(formatP260));
                }


                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                //Check SUM
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.CheckSum));

                var txBuf = packet.ToArray();
                logger.Create($@"MES.SEND PCB{CH}:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{CH}]: SEND TO MES ->" + ASCIIEncoding.ASCII.GetString(txBuf));
                await this.SendToMes(txBuf);
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{CH}]: Wait MES Receiver...");
                await WaitMESReturnData();
                if (this.isReceiver && !string.IsNullOrEmpty(this.DataReceiver))
                {
                    this.isReceiver = false;
                    logger.Create($@"MES.RECEIVER PCB{CH}:" + this.DataReceiver, LogLevel.Information);
                    return FilterData(this.DataReceiver, entity, CH);
                }
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{CH}]: No Response from MES");
                logger.Create($@"Notify [MES{CH}]: No Response from MES",LogLevel.Warning);
            }
            catch (Exception ex)
            {
                logger.Create($@"Send{CH} : " + ex.Message, LogLevel.Error);
            }
            return null;
        }
        public async Task<bool> SendReady(MesCheck entity, string CH)
        {
            this.isReceiver = false;
            try
            {
                if (!this.CheckMESConnection())
                {
                    logger.Create(" -> TCP connection not ready -> discard sending SendReady!", LogLevel.Warning);
                    return false;
                }
                var packet = new List<byte>();
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.EquipmentId));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("M001"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("READYOK                                          "));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));

                var txBuf = packet.ToArray();
                logger.Create($@"MES.SEND Ready{CH}:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{CH}]: SEND TO MES ->" + ASCIIEncoding.ASCII.GetString(txBuf));
                await this.SendToMes(txBuf);
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{CH}]: Wait MES Receiver...");
                await WaitMESReturnData();
                if (this.isReceiver && !string.IsNullOrEmpty(this.DataReceiver))
                {
                    this.isReceiver = false;
                    logger.Create($@"MES.RECEIVER Ready{CH}:" + this.DataReceiver, LogLevel.Information);
                    var mesData = FilterData(this.DataReceiver, entity, CH);
                    if (mesData != null) return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Create($@"SendReady{CH} : " + ex.Message, LogLevel.Error);
            }
            return false;
        }
        public async Task<MesCheck> SendLogin(MesCheck entity, string ch)
        {
            this.isReceiver = false;
            try
            {
                if (!this.CheckMESConnection())
                {
                    logger.Create(" -> TCP connection not ready -> discard sending Send Login!", LogLevel.Warning);
                    return null;
                }
                var packet = new List<byte>();
                // HEAD
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.EquipmentId));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("P002"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.DIV));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.MESCheckLogIn.Type));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("^"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.MESCheckLogIn.User));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("^"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.MESCheckLogIn.Password));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
             
                //Check SUM
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.CheckSum));
                var txBuf = packet.ToArray();
                logger.Create($@"MES.SEND Login [MES{ch}]:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{ch}]: SEND TO MES ->" + ASCIIEncoding.ASCII.GetString(txBuf));
                await this.SendToMes(txBuf);
                //this.notifyEvenMES.NotifyToUI($@"Notify [MES{ch}]: Wait MES Receiver...");
                await WaitMESReturnData();
                if (this.isReceiver && !string.IsNullOrEmpty(this.DataReceiver))
                {
                    this.isReceiver = false;
                    logger.Create($@"MES.RECEIVER Login [MES{ch}]:" + this.DataReceiver, LogLevel.Information);
                    return FilterData(this.DataReceiver, entity, ch);
                }
            }
            catch (Exception ex)
            {
                logger.Create($@"SendReady {ch}: " + ex.Message, LogLevel.Error);
            }
            return null;
        }
        public async Task<MesCheck> SendConfig(MesCheck entity, string configBefor, string configAffter, string CH)
        {
            this.isReceiver = false;
            try
            {
                if (!this.CheckMESConnection())
                {
                    logger.Create(" -> TCP connection not ready -> discard sending SendReady!",LogLevel.Warning);
                    return null;
                }
                var packet = new List<byte>();
                // HEAD
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.EquipmentId));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("P000"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.DIV));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(configBefor));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes("^"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(configAffter));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(";"));
                packet.AddRange(ASCIIEncoding.ASCII.GetBytes(entity.CheckSum));
                var txBuf = packet.ToArray();
                logger.Create($"MES.SEND Config MES{CH}:" + ASCIIEncoding.ASCII.GetString(txBuf), LogLevel.Information);
                //this.notifyEvenMES.NotifyToUI($"Notify [MES{CH}]: SEND TO MES ->" + ASCIIEncoding.ASCII.GetString(txBuf));
                await this.SendToMes(txBuf);
                //this.notifyEvenMES.NotifyToUI($"Notify [MES{CH}]: Wait MES Receiver...");
                await WaitMESReturnData();
                if (this.isReceiver && !string.IsNullOrEmpty(this.DataReceiver))
                {
                    this.isReceiver = false;
                    logger.Create("MES.RECEIVER Ready:" + this.DataReceiver, LogLevel.Information);
                    return FilterData(this.DataReceiver, entity, "");
                }
            }
            catch (Exception ex)
            {
                logger.Create("SendReady : " + ex.Message, LogLevel.Error);
            }
            return null;
        }
        private async Task WaitMESReturnData()
        {
            int counterDelayReceiver = 0;
            await Task.Run(async () => {
                while (!this.isReceiver)
                {
                    if (counterDelayReceiver > 10000)
                    {
                        break;
                    }
                    await Task.Delay(10); // Đợi 10 giây
                    counterDelayReceiver++;
                }
            });
        }
        private MesCheck FilterData(string data, MesCheck mesOld, string CH)
        {
            try
            {
                
                MesCheck newMESCheck = new MesCheck();
                int idex = 0;
                // EQUIPMENT ID
                string equipmentId = data.Substring(idex, 9);
                if (mesOld.EquipmentId != equipmentId)
                {
                    logger.Create($@"EquipmentId Is Diffrent {CH}: Old('" + mesOld.EquipmentId + "') , New('" + equipmentId + "')", LogLevel.Information);
                    return null;
                }
                logger.Create($@"MES.RECEIVER EquipmentId {CH}:" + equipmentId, LogLevel.Information);
                newMESCheck.EquipmentId = equipmentId;
                idex += 9;
                // STATUS
                string status = data.Substring(idex, 4);
                if (status == "M002")
                {
                    logger.Create($@"MES.RECEIVER Status {CH}:" + status, LogLevel.Information);
                    newMESCheck.Status = status;
                    return newMESCheck;
                }
                logger.Create($@"MES.RECEIVER Status {CH}:" + status, LogLevel.Information);
                newMESCheck.Status = status;

                idex += 4;
                // LOT NO
                string div = data.Substring(idex, 14);
                //if (mesOld.LotNo != lotNo)
                //{
                //    logger.Create($@"LotNo Is Diffrent {CH}: Old('" + mesOld.LotNo + "') , New('" + lotNo + "')", LogLevel.Warning);
                //    return null;
                //}
                logger.Create($@"MES.RECEIVER LotNo {CH}:" + div, LogLevel.Information);
                newMESCheck.DIV = div;
                idex += 14;
                // Config
                //BODY
                idex += 1;
                StringBuilder stringBuilder = new StringBuilder(data);
                stringBuilder.Remove(0, idex);
                var checkResult = stringBuilder[0];
                if (checkResult == '{' || checkResult == '[') // Format
                {
                    var body = stringBuilder.ToString().Split(';');
                    if (body.Length != 2) return null;
                    if (newMESCheck.Status == "P231")
                    {
                        List<FormatP231> DATA = new List<FormatP231>();
                        var formatP231Object = new { DATA };
                        var j = JsonConvert.DeserializeAnonymousType(body[0], formatP231Object);
                        if (j == null || j.DATA.Count <= 0) return null;
                        mesOld.FormatP231 = j.DATA[0];
                    }
                    else if (newMESCheck.Status == "P241")
                    {
                        List<FormatP241> DATA = new List<FormatP241>();
                        var formatP241Object = new { DATA };
                        var j = JsonConvert.DeserializeAnonymousType(body[0], formatP241Object);
                        if (j == null || j.DATA.Count <= 0) return null;
                        mesOld.FormatP241 = j.DATA[0];
                    }
                    else if (newMESCheck.Status == "P008")
                    {
                        List<FormatP006> DATA = new List<FormatP006>();
                        var formatP006Object = new { DATA };
                        var j = JsonConvert.DeserializeAnonymousType(body[0], formatP006Object);
                        if (j == null || j.DATA.Count <= 0) return null;
                        mesOld.FormatP006 = j.DATA[0];
                    }
                    mesOld.CheckSum = body[1];
                }
                //else
                //{
                //    var body = stringBuilder.ToString().Split('^');
                //    if (body.Length != 2) return null;
                //    if (body.Length >= 2)
                //    {
                //        var x = body[1].Split(';');
                //        mesOld.MES_Result = body[0] + "^" + x[0];
                //        mesOld.CheckSum = x[1];
                //    }
                //}
                else
                {
                    var body = stringBuilder.ToString().Split('^');
                    if (body.Length != 2) return null;
                    if (body.Length >= 2)
                    {
                        var x = body[1].Split(';');
                        string OKNG = body[0].Substring(0, 2);
                        mesOld.MES_Result = OKNG;
                        mesOld.MES_MSG = OKNG + "^" + x[0];
                        //mesOld.MES_Result = body[0] + "^" + x[0];
                        mesOld.CheckSum = x[1];
                    }
                }
                return mesOld;
                //var body = stringBuilder.ToString().Split('^');
                //if (body.Length <= 1) return null;
                //newMESCheck.CheckSum = body[body.Length - 1];
                //for (int i = 0; i < body.Length - 1; i++)
                //{
                //    newMESCheck.PCB_Result.Add(body[i]);
                //}
                //if (body.Length == 2)
                //{
                //    var x = body[1].Split(';');
                //    newMESCheck.MES_Result = body[0] + "^" + x[0];
                //    newMESCheck.CheckSum = x[1];
                //}
                //else if (body.Length >= 3)
                //{
                //    var x = body[2].Split(';');
                //    newMESCheck.MES_Result = body[0] + "^" + body[1] + "^" + x[0];
                //    newMESCheck.CheckSum = x[1];
                //}
                //else
                //{
                //    return null;
                //}
                //return newMESCheck;
            }
            catch (Exception ex)
            {
                logger.Create($@"FilterData {CH} ""data"" {data}: " + ex.Message, LogLevel.Error);
            }
            return null;
        }

    }
}
