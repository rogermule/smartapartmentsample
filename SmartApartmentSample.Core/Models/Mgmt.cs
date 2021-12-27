using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentSample.Core.Models
{
    public class Mgmt
    {
        public int mgmtID { get; set; }
        public string name { get; set; }
        public string market { get; set; }
        public string state { get; set; }
    }

    public class MgmtHolder
    {
        public Mgmt mgmt { get; set; }
    }
}
