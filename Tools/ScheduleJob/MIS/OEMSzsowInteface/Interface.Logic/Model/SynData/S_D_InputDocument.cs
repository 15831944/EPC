using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class S_D_InputDocument
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string SynID { get; set; }
        public DateTime? SynTime { get; set; }
        public string SynData { get; set; }
    }
    public class InputDocumentRequestData
    {
        public string fileId { get; set; }
        public string fileName { get; set; }
        public long fileSize { get; set; }
        public string folderId { get; set; }
        public string fileExtension { get; set; }
        public string fileMd5 { get; set; }
        public string userId { get; set; }
    }
}
