﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
     public class AssignSystem
    {
        public string NameMachine { get; set; }
        public bool LoginMes { get; set; }
        public bool LoginApp { get; set; }
        public bool AutoCheckUpdate { get; set; }
      

        public AssignSystem()
        {
            this.NameMachine = "Auto Team";
            this.LoginMes = false;
            this.LoginApp = false;
           
            this.AutoCheckUpdate = true;
        }
    }
}
