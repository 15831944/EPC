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
    public class MilestoneFO : SynDataFO
    {
        public void CreateMileStoneQueue()
        {
            var milestoneType = "'Major','Cooperation'";
            var sourceSql = @"select m.*,wbs.Name WBSName,wbs.WBSType from S_P_MileStone m left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
 left join S_W_WBS wbs with(nolock) on m.WBSID = wbs.ID 
where MileStoneType in(" + milestoneType + ") ";
            var synProjectMode = ConfigurationManager.AppSettings["SynProjectMode"] != null ? ConfigurationManager.AppSettings["SynProjectMode"].ToString() : string.Empty;
            if (!string.IsNullOrEmpty(synProjectMode))
                sourceSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_P_MileStone>("select * from S_P_MileStone where type in (" + milestoneType + ")");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sourceSql += " and m.ModifyDate >='" + startDate + "' and m.ModifyDate <='" + endDate + "'";
            }

            #region Remove
            var removeSb = new StringBuilder();
            var removeUrl = this.BaseServerUrl + "{0}/delete";
            var alldtSql = @"select m.ID from S_P_MileStone m left join S_I_ProjectInfo p on p.ID = m.ProjectInfoID 
where MileStoneType in(" + milestoneType + ") ";
            if (!string.IsNullOrEmpty(synProjectMode))
                alldtSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";
            var alldt = this.SQLHelpeProject.ExecuteDataTable(alldtSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var synType = item.Type == "Major" ? "Plan" : "Exchange";
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_P_MileStone", item.ID, item.Type
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), string.Format(removeUrl, synType));
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());
            #endregion

            #region Edit

            var saveUrl = this.BaseServerUrl + "{0}/editinfo";
            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProject.ExecuteDataTable(sourceSql);

            foreach (DataRow row in sourceDt.Rows)
            {
                var dic = DataHelper.DataRowToDic(row);
                var mileStoneType = dic.GetValue("MileStoneType");
                var synType = mileStoneType == "Major" ? "Plan" : "Exchange";
                //设置参数
                if (synType == "Plan")
                {
                    var param = new PlanRequestData();
                    param.projectId = dic.GetValue("ProjectInfoID");
                    param.id = dic.GetValue("ID");
                    param.name = Convert.ToDateTime(dic.GetValue("PlanFinishDate")).ToString("yyyy.MM.dd") + " " + dic.GetValue("Name");
                    var wbstype = dic.GetValue("WBSType");
                    if (wbstype == WBSNodeType.SubProject.ToString())
                        param.subProjectName = dic.GetValue("WBSName");
                    param.phaseCode = row["PhaseValue"].ToString();
                    param.specialtyCode = row["MajorValue"].ToString();
                    var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_P_MileStone", param.id, mileStoneType
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<PlanRequestData>(param).Replace("'", "''"), string.Format(saveUrl, synType));
                    saveSb.AppendLine(queueSql);
                }
                else
                {
                    var param = new ExchangeRequestData();
                    param.projectId = dic.GetValue("ProjectInfoID");
                    param.id = dic.GetValue("ID");
                    param.name = Convert.ToDateTime(dic.GetValue("PlanFinishDate")).ToString("yyyy.MM.dd") + " " + dic.GetValue("Name");
                    var wbstype = dic.GetValue("WBSType");
                    if (wbstype == WBSNodeType.SubProject.ToString())
                        param.subProjectName = dic.GetValue("WBSName");
                    param.phaseCode = row["PhaseValue"].ToString();
                    param.specialtyCode = row["MajorValue"].ToString();
                    param.userId = dic.GetValue("CreateUserID");
                    var synUser = GlobalData.UserList.FirstOrDefault(a => a.ID == param.userId);
                    if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                        param.userId = synUser.SynID;
                    param.RecSpecialties = new List<string>();
                    foreach (var _i in dic.GetValue("OutMajorValue").Split(','))
                        param.RecSpecialties.Add(_i);
                    var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_P_MileStone", param.id, mileStoneType
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<ExchangeRequestData>(param).Replace("'", "''"), string.Format(saveUrl, synType));
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
                sql = @"if exists(select 1 from S_P_MileStone where ID='{0}')
                                update S_P_MileStone set Type = '{1}', Name='{2}', SynTime='{5}',SynData='{6}' where ID='{0}'
                            else
	                            insert into S_P_MileStone(ID,Type,Name,State,SynID,SynTime,SynData,ProjectInfoID) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                if (queueData.RelateType.ToLower() == "major")
                {
                    var synData = JsonHelper.ToObject<PlanRequestData>(queueData.RequestData);
                    sql = string.Format(sql, queueData.RelateID, queueData.RelateType, synData.name.Replace("'", "''"), DataState.Normal.ToString(), ""
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"), synData.projectId);
                }
                else
                {
                    var synData = JsonHelper.ToObject<ExchangeRequestData>(queueData.RequestData);
                    sql = string.Format(sql, queueData.RelateID, queueData.RelateType, synData.name.Replace("'", "''"), DataState.Normal.ToString(), ""
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"), synData.projectId);
                }
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_P_MileStone set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
