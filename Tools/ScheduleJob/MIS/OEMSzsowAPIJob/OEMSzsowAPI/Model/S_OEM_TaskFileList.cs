using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OEMSzsowAPI.Model
{
    public class S_OEM_TaskFileList
    {
        public long ID { get; set; }
        public string OEMCode { get; set; }
        public string BusinessCode { get; set; }
        public string BusinessID { get; set; }
        public string MD5Code { get; set; }
        public string FileName { get; set; }
        public string FsFileID { get; set; }
        public DateTime? CreateTime { get; set; }
        public string SyncState { get; set; }
        public DateTime? SyncTime { get; set; }
        public string RequestUrl { get; set; }
        public string RequestData { get; set; }
        public string Response { get; set; }
        public string ErrorMsg { get; set; }
    }
}
