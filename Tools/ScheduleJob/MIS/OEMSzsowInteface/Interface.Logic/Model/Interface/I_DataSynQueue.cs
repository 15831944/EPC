using Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class I_DataSynQueue
    {
        public long ID { get; set; }
        public string SynType { get; set; }
        public string RelateTable { get; set; }
        public string RelateID { get; set; }
        public string RelateType { get; set; }
        public DateTime? CreateTime { get; set; }
        public string SynState { get; set; }
        public DateTime? SynTime { get; set; }
        public string RequestUrl { get; set; }
        public string RequestData { get; set; }
        public string Response { get; set; }
        public string ErrorMsg { get; set; }
        
    }
}
