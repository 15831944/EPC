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
    public class ProjectFO : SynDataFO
    {
        public void CreateProjectQueue()
        {
            //URL地址
            var saveUrl = this.BaseServerUrl + "project/edit";
            var removeUrl = this.BaseServerUrl + "project/delete";

            var sourceSql = "select * from S_I_ProjectInfo where 1=1  ";
            
            var synProjectMode = ConfigurationManager.AppSettings["SynProjectMode"] != null ? ConfigurationManager.AppSettings["SynProjectMode"].ToString() : string.Empty;
            var whereSql = string.Empty;
            if (!string.IsNullOrEmpty(synProjectMode))
                whereSql = "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";

            //取最近的同步记录时间，到当前时间的差异数据
            var synData = this.SQLHelperInterface.ExecuteList<S_I_ProjectInfo>("select * from S_I_ProjectInfo");
            var lastSynTime = synData.Max(a => a.SynTime);
            if (lastSynTime != null)
            {
                //判断同步增量数据 还是 所有数据
                var startDate = Convert.ToDateTime(lastSynTime).ToString();
                var endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                whereSql += " and ModifyDate >='" + startDate + "' and ModifyDate <='" + endDate + "'";

            }
            sourceSql += whereSql;

            var saveSb = new StringBuilder();
            var sourceDt = this.SQLHelpeProject.ExecuteDataTable(sourceSql);
            var wbsDt = this.SQLHelpeProject.ExecuteDataTable("select * from S_W_WBS with(nolock) where ProjectInfoID in (select ID from S_I_ProjectInfo where 1=1 " + whereSql + ")");
            var rbsDt = this.SQLHelpeProject.ExecuteDataTable("select * from S_W_RBS with(nolock) where ProjectInfoID in (select ID from S_I_ProjectInfo where 1=1 " + whereSql + ")");

            #region 角色编号对应关系、图框属性对应

            var roleCodeDic = new Dictionary<string, string>();//角色编号对应关系
            var attrDic = new Dictionary<string, string>();//图框属性对应关系
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key.StartsWith("FrameAttr_"))
                    attrDic.SetValue(key.Replace("FrameAttr_", ""), ConfigurationManager.AppSettings[key].ToString());
                else if (key.StartsWith("RoleCode_"))
                    roleCodeDic.SetValue(key.Replace("RoleCode_", ""), ConfigurationManager.AppSettings[key].ToString());
            }
            #endregion

            var removeSb = new StringBuilder();

            foreach (DataRow prjRow in sourceDt.Rows)
            {
                var projectDic = DataHelper.DataRowToDic(prjRow);

                //设置参数
                var param = new ProjectRequestData();
                param.id = projectDic.GetValue("ID");
                param.name = projectDic.GetValue("Name");
                param.code = projectDic.GetValue("Code");
                param.constructionUnit = projectDic.GetValue("CustomerName");
                param.enable = true;
                param.standard = true;
                param.isdeleted = false;
                var state = projectDic.GetValue("State");
                var enableStates = new string[] { "Finish", "Pause", "Terminate" };//完工、暂停、终止
                if (enableStates.Contains(state))
                    param.enable = false;
                //项目经理
                #region 项目经理
                var pmList = new List<string>();
                var pmRows = rbsDt.Select("ProjectInfoID='" + param.id + "' and RoleCode='" + ProjectRole.ProjectManager.ToString() + "'");
                foreach (DataRow row in pmRows)
                {
                    if (row["UserID"] == null || row["UserID"] == DBNull.Value)
                        continue;
                    var userId = row["UserID"].ToString();
                    if (string.IsNullOrEmpty(userId))
                        continue;
                    var synUser = GlobalData.UserList.FirstOrDefault(a => a.ID == userId);
                    if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                        userId = synUser.SynID;
                    if (!pmList.Contains(userId))
                        pmList.Add(userId);
                }
                param.owerUserId = string.Join(",", pmList.ToArray());

                #endregion
                
                //阶段
                #region 阶段
                param.phases = new List<phase>();
                var phaseRows = wbsDt.Select("ProjectInfoID='" + param.id + "' and WBSType='" + WBSNodeType.Phase.ToString() + "'");
                foreach (DataRow row in phaseRows)
                {
                    var item = new phase();
                    param.phases.Add(item);
                    var phaseWBSID = row["ID"].ToString();
                    item.PhaseCode = row["WBSValue"].ToString();
                    item.phaseUsers = new List<ProjectUser>();
                    item.subProjects = new List<subProject>();
                    var children = wbsDt.Select("ParentID='" + phaseWBSID + "'");
                    if (children.FirstOrDefault() != null)
                    {
                        if (children.FirstOrDefault()["WBSType"].ToString() == WBSNodeType.Major.ToString())
                        {
                            //阶段-专业
                            foreach (var majorRow in children)
                            {
                                //处理专业节点及所有以上人员
                                //项目经理通过owerUserId处理
                                var wbsids = majorRow["FullID"].ToString().Replace(".", "','");
                                var roleUserRows = rbsDt.Select("WBSID in ('" + wbsids + "') and RoleCode <>'" + ProjectRole.ProjectManager.ToString() + "'");
                                var majorCode = majorRow["WBSValue"].ToString();
                                setProjectUser(roleUserRows, item.phaseUsers, majorCode, roleCodeDic);
                            }
                        }
                        else
                        {
                            //阶段-子项-专业
                            foreach (var subProjectRow in children)
                            {
                                var subProject = new subProject();
                                item.subProjects.Add(subProject);
                                subProject.id = subProjectRow["ID"].ToString();
                                subProject.Name = subProjectRow["Name"].ToString();
                                if (subProjectRow["Code"] != null && subProjectRow["Code"] != DBNull.Value)
                                    subProject.Code = subProjectRow["Code"].ToString();
                                else
                                    subProject.Code = subProject.Name;
                                subProject.users = new List<ProjectUser>();
                                var spChildren = wbsDt.Select("ParentID='" + subProjectRow["ID"].ToString() + "' and WBSType='" + WBSNodeType.Major.ToString() + "'");
                                foreach (var majorRow in spChildren)
                                {
                                    //处理专业节点及所有以上人员
                                    //项目经理通过owerUserId处理
                                    var wbsids = majorRow["FullID"].ToString().Replace(".", "','");
                                    var roleUserRows = rbsDt.Select("WBSID in ('" + wbsids + "') ");
                                    var majorCode = majorRow["WBSValue"].ToString();
                                    setProjectUser(roleUserRows, subProject.users, majorCode, roleCodeDic);
                                }
                            }
                        }
                    }
                }

                #endregion
                //图框扩展属性 结构比较怪异，需要传递树层数据
                #region 图框扩展属性
                param.drawInfo = new List<drawInfo>();
                foreach (var field in attrDic.Keys)
                {
                    var code = attrDic.GetValue(field);
                    var name = projectDic.GetValue(field);
                    if (string.IsNullOrEmpty(name)) continue;
                    var droot = new drawInfo();
                    droot.ProjectId = param.id;
                    droot.ID = GuidHelper.CreateGuid();
                    droot.Name = code;
                    droot.Code = code;
                    droot.Description = string.Empty;
                    param.drawInfo.Add(droot);
                    foreach (var item in name.Split(','))
                    {
                        var dvalue = new drawInfo();
                        dvalue.ProjectId = param.id;
                        dvalue.ID = GuidHelper.CreateGuid();
                        dvalue.Name = item;
                        dvalue.Description = droot.ID;
                        dvalue.Code = string.Empty;
                        param.drawInfo.Add(dvalue);
                    }
                }
                #endregion

                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Save.ToString(), "S_I_ProjectInfo", param.id, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<ProjectRequestData>(param).Replace("'", "''"), saveUrl);
                saveSb.AppendLine(queueSql);

            }

            var idSql = "select ID from S_I_ProjectInfo where 1=1";
            if (!string.IsNullOrEmpty(synProjectMode))
                idSql += "and ModeCode in ('" + synProjectMode.Replace(",", "','") + "') ";
            var alldt = this.SQLHelpeProject.ExecuteDataTable(idSql);
            var allIDs = new List<string>();
            foreach (DataRow item in alldt.Rows)
                allIDs.Add(item["ID"].ToString());
            var removeList = synData.Where(a => !allIDs.Contains(a.ID) && a.State == DataState.Normal.ToString()).ToList();
            foreach (var item in removeList)
            {
                var queueSql = string.Format(this.SaveQueueSqlTmp, SynType.Remove.ToString(), "S_I_ProjectInfo", item.ID, ""
                    , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), JsonHelper.ToJson<DeleteRequestData>(new DeleteRequestData() { id = item.ID }), removeUrl);
                removeSb.AppendLine(queueSql);
            }

            if (removeSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(removeSb.ToString());

            if (saveSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(saveSb.ToString());
        }

        private void setProjectUser(DataRow[] roleUserRows, List<ProjectUser> projectUsers, string majorCode, Dictionary<string, string> roleCodeDic)
        {
            foreach (DataRow roleUser in roleUserRows)
            {
                var userID = roleUser["UserID"].ToString();
                var synUser = GlobalData.UserList.FirstOrDefault(a => a.ID == userID);
                if (synUser != null && !string.IsNullOrEmpty(synUser.SynID))
                    userID = synUser.SynID;
                var roleCode = roleUser["RoleCode"].ToString();
                if (!roleCodeDic.ContainsKey(roleCode)) continue;
                var paramRoleCode = roleCodeDic.GetValue(roleCode);

                var userItem = projectUsers.FirstOrDefault(a => a.UserID == userID && a.SpecialtyCode == majorCode);
                if (userItem == null)
                {
                    userItem = new ProjectUser()
                    {
                        UserID = userID,
                        SpecialtyCode = majorCode
                    };
                    projectUsers.Add(userItem);
                }
                userItem.RoleCode += ("," + paramRoleCode);
                userItem.RoleCode = userItem.RoleCode.TrimStart(',');
            }
        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            var sql = string.Empty;
            if (queueData.SynType == SynType.Save.ToString())
            {
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synData = JsonHelper.ToObject<ProjectRequestData>(queueData.RequestData);
                sql = @"if exists(select 1 from S_I_ProjectInfo where ID='{0}')
                                update S_I_ProjectInfo set Code = '{1}', Name='{2}', SynTime='{5}',SynData='{6}' where ID='{0}' 
                            else
	                            insert into S_I_ProjectInfo(ID,Code,Name,State,SynID,SynTime,SynData) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                sql = string.Format(sql, queueData.RelateID, synData.code, synData.name.Replace("'", "''"), DataState.Normal.ToString(), "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), queueData.RequestData.Replace("'", "''"));
            }
            else if (queueData.SynType == SynType.Remove.ToString())
            {
                sql = "update S_I_ProjectInfo set State='" + DataState.Deleted.ToString() + "' where id='" + queueData.RelateID + "'";
            }
            interfaceSb.AppendLine(sql);
        }
    }
}
