using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_A_BaseEnum
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }
    public class BaseEnumRequestData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }
}
