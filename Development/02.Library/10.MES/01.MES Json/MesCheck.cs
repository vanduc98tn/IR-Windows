using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class MesCheck
    {
        public MESCheckLogIn MESCheckLogIn { get; set; }
        public bool SelectSendMes { get; set; }
        public string EquipmentId { get; set; }
        public string Status { get; set; }
        public string DIV { get; set; }
        public string CheckSum { get; set; }
        public string MES_Result { get; set; }
        public string MES_MSG { get; set; }


        public FormatP005 FormatP005 { get; set; }
        public FormatP006 FormatP006 { get; set; }
        public FormatP230 FormatP230 { get; set; }
        public FormatP231 FormatP231 { get; set; }
        public FormatP240 FormatP240 { get; set; }
        public FormatP241 FormatP241 { get; set; }

        public MesCheck()
        {
            FormatP005 = new FormatP005();
            FormatP006 = new FormatP006();
            FormatP230 = new FormatP230();
            FormatP231 = new FormatP231();
            FormatP240 = new FormatP240();
            FormatP241 = new FormatP241();

            MESCheckLogIn = new MESCheckLogIn();
        }
    }
}
