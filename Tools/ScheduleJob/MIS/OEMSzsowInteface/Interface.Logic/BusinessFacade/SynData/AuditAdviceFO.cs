using Config.Logic;
using Formula.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class AuditAdviceFO : SynDataFO
    {
        //校审意见库
        public void CreateAdviceQueue()
        {
            var sourceSql = @"select * from S_AE_AuditAdvice
where Major is not null and Catalog is not null and  Level='System'";
            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_AE_AuditAdvice>("select * from S_AE_AuditAdvice  ");
            var catalogData = this.SQLHelperInterface.ExecuteList<S_AE_AuditAdviceCatalog>("select * from S_AE_AuditAdviceCatalog  ");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and ModifyDate >='" + startDate + "' and ModifyDate <='" + endDate + "'";
            }

            #region Remove
            var removeSb = new StringBuilder();
            var removeUrl = this.BaseServerUrl + "Article/DeleteArticleContent";
            var alldtSql = @"select ID from S_AE_AuditAdvice
where Major is not null and Catalog is not null and  Level='System'";
            var alldt = this.SQLHelpeProject.ExecuteDataTable(alldtSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_AE_AuditAdvice", item.ID, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());
            #endregion

            #region Add/Edit

            var addUrl = this.BaseServerUrl + "Article/CreateArticleContent";
            var editUrl = this.BaseServerUrl + "Article/UpdateArticleContent";
            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProject.ExecuteDataTable(sourceSql);

            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);
                //设置参数
                var param = new AuditAdviceRequestData();
                param.id = dic.GetValue("ID");
                var catalog = catalogData.FirstOrDefault(a => a.MajorCode == dic.GetValue("Major") && a.Name == dic.GetValue("Catalog"));
                if (catalog == null)
                {
                    Formula.LogWriter.Info(string.Format("同步校审意见错误：专业分类未同步：{0}，数据ID：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), param.id));
                    continue;
                }
                param.articleID = catalog.ID;
                param.content = dic.GetValue("Advice");
                var url = editUrl;
                var exist = synData.FirstOrDefault(a => a.State == DataState.Normal.ToString() && a.ID == param.id);
                if (exist == null)
                    url = addUrl;

                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_AE_AuditAdvice", param.id, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<AuditAdviceRequestData>(param).Replace("'", "''"), url);
                saveSb.AppendLine(queueSql);
            }

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
            #endregion
        }

        //校审意见库
        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            var sql = string.Empty; 
            if (queueData.SynType == SynType.Save.ToString())
            {
                var synData = JsonHelper.ToObject<AuditAdviceRequestData>(queueData.RequestData);
                sql = @"if exists(select 1 from S_AE_AuditAdvice where ID='{0}')
                                update S_AE_AuditAdvice set Name='{1}', SynTime='{4}',SynData='{5}' where ID='{0}'
                            else
                                insert into S_AE_AuditAdvice(ID,Name,State,SynID,SynTime,SynData) values ('{0}','{1}','{2}','{3}','{4}','{5}')";
                sql = string.Format(sql, queueData.RelateID, synData.content.Replace("'", "''"), DataState.Normal.ToString(), ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"));
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_AE_AuditAdvice set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
