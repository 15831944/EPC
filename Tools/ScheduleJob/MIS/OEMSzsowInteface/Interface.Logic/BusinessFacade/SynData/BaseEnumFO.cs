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
    public class BaseEnumFO : SynDataFO
    {
        //校审问题类型（质量问题分类）
        public void CreateBaseMistakeTypeQueue()
        {
            //URL地址
            var saveUrl = this.BaseServerUrl + "quality/add";
            var removeUrl = this.BaseServerUrl + "quality/delete";
            var enumKey = "Project.MistakeLevel";

            var sourceSql = @"select d.ModifyTime,i.* from S_M_EnumDef d
                                            left join S_M_EnumItem i on i.EnumDefID = d.id
                                            where d.code='" + enumKey + "' ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_A_BaseEnum>("select * from S_A_BaseEnum where type='" + enumKey + "'");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and ModifyTime >='" + startDate + "' and ModifyTime <='" + endDate + "'";
            }

            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeBase.ExecuteDataTable(sourceSql);
            var removeSb = new StringBuilder();

            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);

                //设置参数
                var param = new BaseEnumRequestData();
                param.id = dic.GetValue("ID");
                var exist = synData.FirstOrDefault(a => a.ID == param.id);
                if (exist != null && !string.IsNullOrEmpty(exist.SynID))
                    param.id = exist.SynID;
                param.name = dic.GetValue("Name");
                param.code = dic.GetValue("Code");
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_A_BaseEnum", param.id, enumKey
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<BaseEnumRequestData>(param).Replace("'", "''"), saveUrl);
                saveSb.AppendLine(queueSql);
            }

            var alldt = this.SQLHelpeBase.ExecuteDataTable(@"select i.ID from S_M_EnumDef d
                                            left join S_M_EnumItem i on i.EnumDefID = d.id
                                            where d.code='" + enumKey + "' ");
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var delID = item.ID;
                var exist = synData.FirstOrDefault(a => a.ID == delID);
                if (exist != null && !string.IsNullOrEmpty(exist.SynID))
                    delID = exist.SynID;
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_A_BaseEnum", delID, enumKey
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = delID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
        }

        //阶段专业枚举
        public void CreateBaseMajorPhaseQueue()
        {
            //URL地址
            var saveUrl = this.BaseServerUrl + "{0}/add";
            var removeUrl = this.BaseServerUrl + "{0}/delete";
            var enumKey = "'Major','Phase'";

            var sourceSql = @"select * from S_D_WBSAttrDefine where type in (" + enumKey + ")";

            //根据配置的同步时间间隔同步数据
            var synData = this.SQLHelperInterface.ExecuteList<S_A_BaseEnum>("select * from S_A_BaseEnum where type in (" + enumKey + ")");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                var interval = ConfigurationManager.AppSettings["BaseMajorPhaseSynInterval"];
                var intervalValue = 0d;
                if (!string.IsNullOrEmpty(interval))
                    intervalValue = Convert.ToDouble(interval);
                if (DateTime.Now < lastSynTime.Value.AddMinutes(intervalValue))
                    return;
            }

            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProjectConfig.ExecuteDataTable(sourceSql);
            var removeSb = new StringBuilder();

            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);

                //设置参数
                var param = new BaseEnumRequestData();
                param.id = dic.GetValue("ID");
                var exist = synData.FirstOrDefault(a => a.ID == param.id);
                if (exist != null && !string.IsNullOrEmpty(exist.SynID))
                    param.id = exist.SynID;
                param.name = dic.GetValue("Name");
                param.code = dic.GetValue("Code");
                var queueSql = string.Format(SaveQueueSqlTmp, SynType.Save.ToString(), "S_A_BaseEnum", param.id, dic.GetValue("Type")
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<BaseEnumRequestData>(param).Replace("'", "''"), string.Format(saveUrl, dic.GetValue("Type") == "Phase" ? "Phase" : "specialty"));
                saveSb.AppendLine(queueSql);
            }

            var alldt = this.SQLHelpeProjectConfig.ExecuteDataTable(@"select ID from S_D_WBSAttrDefine where type in (" + enumKey + ")");
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var delID = item.ID;
                var exist = synData.FirstOrDefault(a => a.ID == delID);
                if (exist != null && !string.IsNullOrEmpty(exist.SynID))
                    delID = exist.SynID;
                var queueSql = string.Format(SaveQueueSqlTmp, SynType.Remove.ToString(), "S_A_BaseEnum", delID, item.Type
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = delID }), string.Format(removeUrl, item.Type == "Phase" ? "Phase" : "specialty"));
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            var sql = string.Empty;
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synID = responseData.GetValue("ID");
                var synData = JsonHelper.ToObject<BaseEnumRequestData>(queueData.RequestData);
                if (synID == synData.id) synID = string.Empty;
                sql = @"if exists(select 1 from S_A_BaseEnum where ID='{0}' or SynID='{0}')
                                update S_A_BaseEnum set Code = '{1}', Name='{2}', SynTime='{6}',SynData='{7}' where ID='{0}' or SynID='{0}'
                            else
	                            insert into S_A_BaseEnum(ID,Code,Name,State,Type,SynID,SynTime,SynData) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                sql = string.Format(sql, queueData.RelateID, synData.code, synData.name.Replace("'", "''"), DataState.Normal.ToString(), queueData.RelateType,
                    synID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"));
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_A_BaseEnum set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
