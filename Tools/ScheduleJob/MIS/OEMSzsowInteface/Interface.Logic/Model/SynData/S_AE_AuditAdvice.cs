using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_AE_AuditAdvice
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }
    public class AuditAdviceRequestData
    {
        public string id { get; set; }
        public string articleID { get; set; }//分类id
        public string content { get; set; }//意见内容
    }
}
