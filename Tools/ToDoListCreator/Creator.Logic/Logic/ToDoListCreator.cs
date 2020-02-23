using Common;
using Creator.Logic.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Creator.Logic
{
    public class ToDoListCreator
    {
        public DataTable _toListDefine;

        public DataTable _modeList;

        public DataTable _projectInfoList;

        public SQLHelper ProjectDB;

        public SQLHelper ProjectBaseConfigDB;

        public ToDoListCreator()
        {
            Init();
        }

        void Init()
        {
            this.ProjectBaseConfigDB = SQLHelper.CreateSqlHelper("ProjectBaseConfig", System.Configuration.ConfigurationManager.ConnectionStrings["ProjectBaseConfig"].ConnectionString);

            this.ProjectDB = SQLHelper.CreateSqlHelper("Project", System.Configuration.ConfigurationManager.ConnectionStrings["Project"].ConnectionString);

            _toListDefine = this.ProjectBaseConfigDB.ExecuteDataTable("select * from S_T_ToDoListDefineNode where Type='Task'");

            _modeList = this.ProjectBaseConfigDB.ExecuteDataTable("select * from S_T_ProjectMode");
            _projectInfoList = this.ProjectDB.ExecuteDataTable("select * from S_I_ProjectInfo");
        }

        public void CreatorToDoList()
        {
            foreach (DataRow projectInfo in this._projectInfoList.Rows)
            {
                try
                {


                    var defineNodes = this.GetDefineInfoByModeCode(projectInfo["ModeCode"].ToString());
                    if (defineNodes == null || defineNodes.Count == 0)
                        continue;
                    foreach (var defineNode in defineNodes)
                    {
                        if (String.IsNullOrEmpty(defineNode["TriggeringCondition"].ToString()))
                            continue;
                        var sql = defineNode["TriggeringCondition"].ToString();
                        sql = TextRegHelper.ReplaceString(sql, projectInfo);
                        var dt = this.ProjectDB.ExecuteDataTable(sql);
                        if (!dt.Columns.Contains("ExecData")) continue;
                        foreach (DataRow sourceRow in dt.Rows)
                        {
                            var execData = sourceRow["ExecData"] == null || sourceRow["ExecData"] == DBNull.Value ? "" : sourceRow["ExecData"].ToString().Trim();
                            var countSQL = "select * from S_T_ToDoList where DefineNodeID='{0}' and ProjectInfoID='{1}' and ExecData='{2}' and State='Create'";
                            var countDt = this.ProjectDB.ExecuteDataTable(String.Format(countSQL, defineNode["ID"], projectInfo["ID"], execData));
                            if (countDt.Rows.Count > 0)
                            {
                                string updateSQL = "update S_T_ToDoList set LinkUrl='{0}',ExeAction='{1}' where ID='{2}'";
                                this.ProjectDB.ExecuteNonQuery(String.Format(updateSQL,
                                    defineNode["LinkUrl"] == null || defineNode["LinkUrl"] == DBNull.Value ? "" : TextRegHelper.ReplaceString(defineNode["LinkUrl"].ToString(), sourceRow),
                                    defineNode["ExeAction"], countDt.Rows[0]["ID"]));
                            }
                            else
                            {
                                #region 创建待办事项
                                var dic = new Dictionary<string, object>();
                                dic.SetValue("Name", defineNode["Name"].ToString());
                                dic.SetValue("EngineeringInfoID", projectInfo["EngineeringInfoID"].ToString());
                                dic.SetValue("ProjectInfoID", projectInfo["ID"].ToString());
                                dic.SetValue("ExeAction", defineNode["ExeAction"].ToString().Trim());
                                dic.SetValue("State", "Create");
                                var execUserList = getUser(defineNode, projectInfo, sourceRow);
                                var execUserIDs = execUserList.Select(c => c.GetValue("UserID")).ToList();
                                var execUserName = execUserList.Select(c => c.GetValue("UserName")).ToList();
                                if (execUserIDs.Count == 0)
                                    continue;
                                dic.SetValue("ExecUserID", String.Join(",", execUserIDs));
                                dic.SetValue("ExecUserName", String.Join(",", execUserName));
                                dic.SetValue("CreateDate", DateTime.Now);
                                var url = defineNode["LinkUrl"] == null || defineNode["LinkUrl"] == DBNull.Value ? "" : TextRegHelper.ReplaceString(defineNode["LinkUrl"].ToString(), sourceRow);
                                dic.SetValue("LinkUrl", TextRegHelper.ReplaceString(url, projectInfo));
                                dic.SetValue("OpenWidth", "80%");
                                dic.SetValue("OpenHeight", "80%");
                                dic.SetValue("DefineNodeID", defineNode["ID"].ToString());
                                dic.SetValue("DefineNodeName", defineNode["Name"].ToString());
                                dic.SetValue("DefineID", defineNode["DefineID"].ToString());
                                dic.SetValue("ExecData", sourceRow["ExecData"]);
                                dic.SetValue("FinishData", "");
                                dic.InsertDB(ProjectDB, "S_T_ToDoList");
                                #endregion
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    LogWriter.Error(exp);
                }
            }
        }

        List<Dictionary<string, object>> getUser(DataRow defineRow, DataRow projectInfo, DataRow dataSourceRow)
        {
            var result = new List<Dictionary<string, object>>();
            var userIDs = string.Empty;
            if (defineRow["UserIDs"] != null && defineRow["UserIDs"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["UserIDs"].ToString()))
            {
                userIDs = defineRow["UserIDs"].ToString();
            }
            else if (!String.IsNullOrEmpty(defineRow["UserIDsFromField"].ToString()))
            {
                if (projectInfo.Table.Columns.Contains(defineRow["UserIDsFromField"].ToString()))
                {
                    if (projectInfo[defineRow["UserIDsFromField"].ToString()] != null &&
                        projectInfo[defineRow["UserIDsFromField"].ToString()] != DBNull.Value &&
                        !String.IsNullOrEmpty(projectInfo[defineRow["UserIDsFromField"].ToString()].ToString()))
                    {
                        userIDs = projectInfo[defineRow["UserIDsFromField"].ToString()].ToString();
                    }
                }
                else if (dataSourceRow.Table.Columns.Contains(defineRow["UserIDsFromField"].ToString()))
                {
                    if (dataSourceRow[defineRow["UserIDsFromField"].ToString()] != null &&
                       dataSourceRow[defineRow["UserIDsFromField"].ToString()] != DBNull.Value &&
                       !String.IsNullOrEmpty(dataSourceRow[defineRow["UserIDsFromField"].ToString()].ToString()))
                    {
                        userIDs = dataSourceRow[defineRow["UserIDsFromField"].ToString()].ToString();
                    }
                }
            }
            else if (defineRow["RoleIDs"] != null && defineRow["RoleIDs"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["RoleIDs"].ToString()))
            {
                var orgID = "";
                if (defineRow["OrgIDs"] != null && defineRow["OrgIDs"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["OrgIDs"].ToString()))
                {
                    orgID = defineRow["OrgIDs"].ToString();
                }
                if (String.IsNullOrEmpty(orgID) && defineRow["OrgIDFromField"] != null && defineRow["OrgIDFromField"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["OrgIDFromField"].ToString()))
                {
                    if (projectInfo.Table.Columns.Contains(defineRow["OrgIDFromField"].ToString()) && projectInfo[defineRow["OrgIDFromField"].ToString()] != null && projectInfo[defineRow["OrgIDFromField"].ToString()] != DBNull.Value)
                    {
                        orgID = projectInfo[defineRow["OrgIDFromField"].ToString()].ToString();
                    }
                }
                userIDs = UserService.GetUserIDsInRoles(defineRow["RoleIDs"].ToString(), orgID);
                var prjRoleUser = PrjRoleExt.GetRoleUserIDs(defineRow["RoleIDs"].ToString(), orgID);
                if (!string.IsNullOrEmpty(prjRoleUser))
                    userIDs = (prjRoleUser + "," + userIDs).Trim(',');
            }
            else if (defineRow["RoleIDsFromField"] != null && defineRow["RoleIDsFromField"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["RoleIDsFromField"].ToString()))
            {
                var roleID = "";
                if (projectInfo.Table.Columns.Contains(defineRow["RoleIDsFromField"].ToString()) && projectInfo[defineRow["RoleIDsFromField"].ToString()] != null && projectInfo[defineRow["RoleIDsFromField"].ToString()] != DBNull.Value)
                {
                    roleID = projectInfo[defineRow["RoleIDsFromField"].ToString()].ToString();
                }
                else if (dataSourceRow.Table.Columns.Contains(defineRow["RoleIDsFromField"].ToString()))
                {
                    roleID = dataSourceRow[defineRow["RoleIDsFromField"].ToString()].ToString();
                }
                if (!String.IsNullOrEmpty(roleID))
                {
                    var orgID = "";
                    if (defineRow["OrgIDs"] != null && defineRow["OrgIDs"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["OrgIDs"].ToString()))
                    {
                        orgID = defineRow["OrgIDs"].ToString();
                    }
                    if (String.IsNullOrEmpty(orgID) && defineRow["OrgIDFromField"] != null && defineRow["OrgIDFromField"] != DBNull.Value && !String.IsNullOrEmpty(defineRow["OrgIDFromField"].ToString()))
                    {
                        if (projectInfo.Table.Columns.Contains(defineRow["OrgIDFromField"].ToString()) && projectInfo[defineRow["OrgIDFromField"].ToString()] != null && projectInfo[defineRow["OrgIDFromField"].ToString()] != DBNull.Value)
                        {
                            orgID = projectInfo[defineRow["OrgIDFromField"].ToString()].ToString();
                        }
                        else if (dataSourceRow.Table.Columns.Contains(defineRow["OrgIDFromField"].ToString()))
                        {
                            orgID = dataSourceRow[defineRow["OrgIDFromField"].ToString()].ToString();
                        }
                    }
                    userIDs = UserService.GetUserIDsInRoles(defineRow["RoleIDs"].ToString(), orgID);
                }
            }

            if (!String.IsNullOrEmpty(userIDs))
            {
                var userNames = UserService.GetUserNames(userIDs).Split(',');
                if (userIDs.Split(',').Length == userNames.Length)
                {
                    for (int i = 0; i < userIDs.Split(',').Length; i++)
                    {
                        var dic = new Dictionary<string, object>();
                        dic.SetValue("UserID", userIDs.Split(',')[i]);
                        dic.SetValue("UserName", userNames[i]);
                        result.Add(dic);
                    }
                }
            }
            return result;
        }

        DataRow GetMode(string code)
        {
            return this._modeList.AsEnumerable().FirstOrDefault(c => c["ModeCode"].ToString() == code);
        }

        List<DataRow> GetDefineInfoByModeCode(string ModeCode)
        {
            var mode = GetMode(ModeCode);
            if (mode == null) return null;
            return GetDefineInfo(mode["ID"].ToString());
        }

        List<DataRow> GetDefineInfo(string DefineID)
        {
            var list = _toListDefine.AsEnumerable().Where(c => c["ModeID"].ToString() == DefineID).OrderBy(c => c["FullID"].ToString()).ToList();
            return list;
        }
    }
}
