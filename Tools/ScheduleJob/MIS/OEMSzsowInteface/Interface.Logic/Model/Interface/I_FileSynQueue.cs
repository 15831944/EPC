using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class I_FileSynQueue
    {
        public long ID { get; set; }
        public string SynType { get; set; }
        public string BusinessCode { get; set; }
        public string RelateTable { get; set; }
        public string RelateID { get; set; }
        public string RelateType { get; set; }
        public string MD5Code { get; set; }
        public string FileName { get; set; }
        public string FsFileID { get; set; }
        public DateTime? CreateTime { get; set; }
        public string SynState { get; set; }
        public DateTime? SynTime { get; set; }
        public string RequestUrl { get; set; }
        public string RequestData { get; set; }
        public string Response { get; set; }
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 生成下载文件队列
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="fileName"></param>
        /// <param name="md5Code"></param>
        /// <param name="relateTable"></param>
        /// <param name="relateID"></param>
        /// <param name="relateType"></param>
        public static void CreateDownloadQueue(StringBuilder sb,string fileName,string md5Code, string relateTable, string relateID, string relateType,string RequestData="")
        {
            string fileTaskSql = @" if exists(select 1 from I_FileSynQueue where SynType='{0}' and RelateID='{2}' and SynState ='{5}')
    update I_FileSynQueue set CreateTime = '{4}', MD5Code='{6}',  FileName='{7}' where SynType='{0}' and RelateID='{2}' and SynState ='{5}'
else
    insert into I_FileSynQueue(SynType,RelateTable,RelateID,RelateType,CreateTime,SynState,MD5Code,FileName,RequestData) 
                                        values( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            fileTaskSql = string.Format(fileTaskSql, Interface.Logic.SynType.Download.ToString(), relateTable, relateID, relateType
                , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Interface.Logic.SynState.New.ToString(), md5Code, fileName.Replace("'", "''"), RequestData.Replace("'", "''"));
            sb.AppendLine(fileTaskSql);
        }
        /// <summary>
        /// 生成上传文件对垒
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="fsFileID"></param>
        /// <param name="sourceDate"></param>
        /// <param name="relateTable"></param>
        /// <param name="relateID"></param>
        /// <param name="relateType"></param>
        public static void CreateUploadQueue(StringBuilder sb, string fsFileID, string sourceDate, string relateTable, string relateID, string relateType)
        {
            string fileTaskSql = @" 
if not exists(select 1 from I_DataSynQueue where SynType='Save' and RelateID='{2}' and SynState ='{5}' and RelateTable='{1}')
begin
    if exists(select 1 from I_FileSynQueue where SynType='{0}' and RelateID='{2}' and SynState ='{5}')
        update I_FileSynQueue set CreateTime = '{4}', FsFileID='{6}',  RequestData='{7}' where SynType='{0}' and RelateID='{2}' and SynState ='{5}'
    else
        insert into I_FileSynQueue(SynType,RelateTable,RelateID,RelateType,CreateTime,SynState,FsFileID,RequestData) 
                                        values( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')
end";
            fileTaskSql = string.Format(fileTaskSql, Interface.Logic.SynType.Upload.ToString(), relateTable, relateID, relateType
                , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Interface.Logic.SynState.New.ToString(), fsFileID, sourceDate.Replace("'", "''"));
            sb.AppendLine(fileTaskSql);
        }

    }
}
