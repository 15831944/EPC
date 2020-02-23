using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_AE_AuditAdviceCatalog
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string MajorCode { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }

    public class AuditAdviceCatalogRequestData
    {
        public string id { get; set; }
        public string title { get; set; }//分类名称
        public string specialtyCode { get; set; }//专业编号
    }
}
