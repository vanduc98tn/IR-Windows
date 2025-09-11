using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Development
{
    public class ByteMESService : IObserverMES
    {
        private SemaphoreSlim modbusSemaphore = new SemaphoreSlim(1, 1);
        private ByteMesRepository ByteMESSend;
        public bool isAccept { get; set; }
        public string InformationClient { get; set; }
        //Notify
        private NotifyEvenMES notifyEvenMES;
        public ByteMESService(TCPSetting tcpSetting)
        {
            this.LoadNotifyEvenMES();
            this.ByteMESSend = new ByteMesRepository(tcpSetting.Ip, tcpSetting.Port);
        }
        public async Task<ByteMESCheck> SendPCB(ByteMESCheck entity, string CH)
        {
            await modbusSemaphore.WaitAsync();
            try
            {
                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                if (entity.LotNo.Length != 10)
                {
                    entity.LotNo = entity.LotNo.PadRight(10, ' ');
                }
                if (entity.Config.Length != 40)
                {
                    entity.Config = entity.Config.PadRight(40, ' ');
                }
                if (entity.CheckSum.Length != 10)
                {
                    entity.CheckSum = entity.CheckSum.PadRight(10, ' ');
                }
                return await this.ByteMESSend.Send(entity, CH);
            }
            finally
            {
                modbusSemaphore.Release();
            }
        }
        public async Task<bool> SendReady(ByteMESCheck entity, string CH)
        {
            await modbusSemaphore.WaitAsync();
            try
            {
                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                return await this.ByteMESSend.SendReady(entity, CH);
            }
            finally
            {
                modbusSemaphore.Release();
            }
        }
        public async Task<ByteMESCheck> SendLogIn(ByteMESCheck entity)
        {
            await modbusSemaphore.WaitAsync();
            try
            {
                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                return await this.ByteMESSend.SendLogin(entity, "");
            }
            finally
            {
                modbusSemaphore.Release();
            }

        }
        public async Task Start()
        {
            await this.ByteMESSend.Start();
            this.isAccept = this.ByteMESSend.isAccept;
        }
        public void Stop()
        {
            this.ByteMESSend.Stop();
        }
        public bool CheckConnection()
        {
            return this.ByteMESSend.CheckMESConnection();
        }
        private void LoadNotifyEvenMES()
        {
            this.notifyEvenMES = SystemsManager.Instance.NotifyEvenMES;
            this.notifyEvenMES.Attach(this);
        }
        public void CheckConnectionMES(bool connected)
        {
            this.isAccept = connected;
        }
        public void FollowingDataMES(string MESResult)
        {

        }
        public void GetInformationFromClientConnect(string clientIP, int clientPort)
        {
            this.InformationClient = clientIP + "-" + clientPort;
        }
        public void UpdateNotifyToUI(string Notify)
        {

        }
    }
}
