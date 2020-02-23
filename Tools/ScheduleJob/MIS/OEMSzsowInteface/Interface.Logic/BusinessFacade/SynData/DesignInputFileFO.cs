using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;

namespace Interface.Logic
{
    public class DesignInputFileFO : SynDataFO
    {
        public void CreateDesignInputFileQueue()
        {
            var sourceSql = @"select doc.* from S_D_InputDocument doc
left join S_D_Input m  on m.ID = doc.InputID
left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where 1=1";

            var synProjectMode = ConfigurationManager.AppSettings["SynProjectMode"] != null ? ConfigurationManager.AppSettings["SynProjectMode"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(synProjectMode))
                sourceSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_D_InputDocument>("select * from S_D_InputDocument where 1=1 ");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and doc.CreateDate >='" + startDate + "' and doc.CreateDate <='" + endDate + "'";
            }

            #region Remove
            var removeSb = new StringBuilder();
            var removeUrl = this.BaseServerUrl + "file/delete";
            var alldtSql = @"select doc.ID from S_D_InputDocument doc
left join S_D_Input m  on m.ID = doc.InputID
left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where 1=1";
            if (!string.IsNullOrEmpty(synProjectMode))
                alldtSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";
            var alldt = this.SQLHelpeProject.ExecuteDataTable(alldtSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_D_InputDocument", item.ID, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());
            #endregion

            //Add 新增日志在FileInterface中生成，需要先上传文件
        }

        public override bool ValidateRequeset(I_DataSynQueue queueData)
        {
            if (queueData.SynType == SynType.Remove.ToString())
                return true;
            //判断文件夹是否存在，不存在文件夹不同步数据
            var synData = JsonHelper.ToObject<InputDocumentRequestData>(queueData.RequestData);
            var folder = this.SQLHelperInterface.ExecuteObject<S_D_Input>("select * from S_D_Input where id='" + synData.folderId + "'");
            return folder != null;
        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synData = JsonHelper.ToObject<InputDocumentRequestData>(queueData.RequestData);
                var sql = @"if exists(select 1 from S_D_InputDocument where ID='{0}')
                                update S_D_InputDocument set Name='{1}', SynTime='{4}',SynData='{5}' where ID='{0}'
                            else
                                insert into S_D_InputDocument(ID,Name,State,SynID,SynTime,SynData) values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                sql = string.Format(sql, queueData.RelateID, synData.fileName.Replace("'", "''"), DataState.Normal.ToString(), ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"));
                interfaceSb.AppendLine(sql);
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                var sql = "update S_D_InputDocument set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
                interfaceSb.AppendLine(sql);
            }
        }
    }
}
