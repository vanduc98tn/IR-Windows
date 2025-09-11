using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    public class FormatP231 :FormatMes
    {
        public string JIG_ID { get; set; }
        public string PARAM_RESULT { get; set; }
        public string PARAM_MSG { get; set; }
        //public ResultPCB PCB { get; set; }

        public List<ResultPCB> PCB { get; set; }
        public FormatP231() 
        { 
            PCB = new List<ResultPCB>();
        } 

    }
}
