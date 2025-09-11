using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Development
{
    class MESService
    {
        private MESSendPCB MESSend;
        private SemaphoreSlim modbusSemaphore = new SemaphoreSlim(1, 1);
        public bool isAccept { get; set; }
        //public string ReceivedLog;


        public MESService(TCPSetting tcpSetting)
        {
            this.MESSend = new MESSendPCB(tcpSetting.Ip, tcpSetting.Port);
        }

       
        //public string ReceviedLog()
        //{
        //    return ReceivedLog;
        //}

        public async Task<MesCheck> SendConfig(MesCheck entity , string DeviceID ,string Recipe)
        {

            await modbusSemaphore.WaitAsync();
            try
            {
                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                if (entity.DIV.Length != 14)
                {
                    entity.DIV = entity.DIV.PadRight(14, ' ');
                }
                if (entity.CheckSum.Length != 14)
                {
                    entity.CheckSum = entity.CheckSum.PadRight(14, ' ');
                }
                return await this.MESSend.SendConfig(entity, DeviceID, Recipe, "");
            }
            finally
            {
                modbusSemaphore.Release();
            }
                
        }
        public async Task<MesCheck> SendLogIn(MesCheck entity)
        {
            await modbusSemaphore.WaitAsync();
            try
            {
                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                if (entity.DIV.Length != 14)
                {
                    entity.DIV = entity.DIV.PadRight(14, ' ');
                }
                return await this.MESSend.SendLogin(entity, "");
            }
            finally
            {
                modbusSemaphore.Release();
            }

        }
        public async Task<MesCheck> SendParam(MesCheck entity, string CH)
        {
            await modbusSemaphore.WaitAsync();
            try
            {

                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                if (entity.DIV.Length != 14)
                {
                    entity.DIV = entity.DIV.PadRight(14, ' ');
                }
                if (entity.CheckSum.Length != 14)
                {
                    entity.CheckSum = entity.CheckSum.PadRight(14, ' ');
                }
                return await this.MESSend.SendParam(entity, CH);
            }
            finally
            {
                modbusSemaphore.Release();
            }
        }
        public async Task<bool> SendReady(MesCheck entity, string CH)
        {
            await modbusSemaphore.WaitAsync();
            try
            {
                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                return await this.MESSend.SendReady(entity, CH);
            }
            finally
            {
                modbusSemaphore.Release();
            }
        }
        public async Task<MesCheck> SendPCB(MesCheck entity, string CH)
        {
            await modbusSemaphore.WaitAsync();
            try
            {

                if (entity.EquipmentId.Length != 9)
                {
                    entity.EquipmentId = entity.EquipmentId.PadRight(9, ' ');
                }
                if (entity.DIV.Length != 14)
                {
                    entity.DIV = entity.DIV.PadRight(14, ' ');
                }
                if (entity.CheckSum.Length != 14)
                {
                    entity.CheckSum = entity.CheckSum.PadRight(14, ' ');
                }
                return await this.MESSend.SendPCB(entity, CH);
            }
            finally
            {
                modbusSemaphore.Release();
            }
        }
        public async Task Start()
        {
            await this.MESSend.Start();
            this.isAccept = this.MESSend.isAccept;
        }
        public void Stop()
        {
            this.MESSend.Stop();
        }
        public bool CheckConnect(out string Ipconnect, out string PortConnect)
        {
           bool Connect = MESSend.CheckConnect(out string IP,out string PORT);
           if(Connect)
           {
                Ipconnect = IP;
                PortConnect = PORT;
                return true;
           }    
           else
           {
                Ipconnect = "No IP";
                PortConnect = "No Port";
                return false;
           }   
        }
    }
}
