using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    public class FormatP240 :FormatMes
    {
        public string JIG_ID { get; set; }
        public string DEFLUX_START { get; set; }
        public string DEFLUX_END { get; set; }
        public string TIME_CALC { get; set; }
        public string V_RINSE_TANK1 { get; set; }
        public string V_RINSE_TANK2 { get; set; }
        public string V_RINSE_TANK3 { get; set; }
        public string V_DRYER { get; set; }
        public string V_CLEANING_TOP { get; set; }
        public string V_CLEANING_BOT { get; set; }
        public string V_CMC_IS_TOP { get; set; }
        public string V_CMC_IS_BOT { get; set; }
        public string V_RINSING1_TOP { get; set; }
        public string V_RINSING1_BOT { get; set; }
        public string V_RINSING2_TOP { get; set; }
        public string V_RINSING2_BOT { get; set; }
        public string V_RINSING3_TOP { get; set; }
        public string V_RINSING3_BOT { get; set; }
        public string V_FINAL_SPARY_TOP { get; set; }
        public string V_FINAL_SPARY_BOT { get; set; }
        public string V_AIR_KNIFE_TOP { get; set; }
        public string V_AIR_KNIFE_BOT { get; set; }
        public string V_CON_MSR { get; set; }
        public string V_CON_SPEED { get; set; }
        public List<PCB> PCB { get; set; }
    }
}
