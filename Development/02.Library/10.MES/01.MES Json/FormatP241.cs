using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    public class FormatP241 :FormatMes
    {
        public string JIG_ID { get; set; }
        public string TIME_RESULT { get; set; }
        public string TIME_MSG { get; set; }
       
        public List<PCB> PCB { get; set; }
        public FormatP241()
        {
            PCB = new List<PCB>();
        }
    }
}
