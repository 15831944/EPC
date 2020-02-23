using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class I_GetDataLog
    {
        public long ID { get; set; }
        public string RelateTable { get; set; }
        public string RelateType { get; set; }
        public string RequestUrl { get; set; }
        public DateTime? SynTime { get; set; }
        public string ErrorMsg { get; set; }
    }
}
