using Config.Logic;
using Formula.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class AuditAdviceCatalogFO : SynDataFO
    {
        //校审意见库分类
        public void CreateCatalogQueue()
        {
            var sourceSql = @"select distinct Major,Catalog from S_AE_AuditAdvice
where Major is not null and Catalog is not null and  Level='System'";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_AE_AuditAdviceCatalog>("select * from S_AE_AuditAdviceCatalog where State='Normal' ");
            //数据库中所有专业、分类
            var alldt = this.SQLHelpeProject.ExecuteDataTable(sourceSql).AsEnumerable();

            #region Remove
            var removeSb = new StringBuilder();
            var removeUrl = this.BaseServerUrl + "Article/DeleteArticle";
            var removeList = new List<S_AE_AuditAdviceCatalog>();
            foreach (var item in synData)
            {
                var exist = alldt.FirstOrDefault(a => a["Major"].ToString() == item.MajorCode && a["Catalog"].ToString() == item.Name);
                if (exist == null) removeList.Add(item);
            }
            foreach (var item in removeList)
            {
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_AE_AuditAdviceCatalog", item.ID, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());
            #endregion

            #region Add
            var saveSb = new StringBuilder();
            var saveUrl = this.BaseServerUrl + "Article/CreateArticle";
            foreach (var item in alldt)
            {
                var major = item["Major"].ToString();
                var catalog = item["Catalog"].ToString();
                if (!synData.Exists(a => a.MajorCode == major && a.Name == catalog))
                {
                    var param = new AuditAdviceCatalogRequestData();
                    param.id = GuidHelper.CreateGuid();
                    param.title = catalog;//分类
                    param.specialtyCode = major;//专业
                    var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_AE_AuditAdviceCatalog", param.id, ""
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<AuditAdviceCatalogRequestData>(param).Replace("'", "''"), saveUrl);
                    saveSb.AppendLine(queueSql);
                }
            }
            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());

            #endregion

        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            var sql = string.Empty;
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synID = responseData.GetValue("ID");
                var synData = JsonHelper.ToObject<AuditAdviceCatalogRequestData>(queueData.RequestData);
                if (synID == synData.id) synID = string.Empty;
                sql = @"insert into S_AE_AuditAdviceCatalog(ID,MajorCode,Name,State,SynID,SynTime,SynData) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                sql = string.Format(sql, queueData.RelateID, synData.specialtyCode, synData.title.Replace("'", "''"), DataState.Normal.ToString(),
                    synID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"));
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_AE_AuditAdviceCatalog set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
