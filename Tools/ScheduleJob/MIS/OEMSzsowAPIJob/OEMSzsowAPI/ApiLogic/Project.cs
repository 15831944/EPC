using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OEMSzsowAPI.Common;
using OEMSzsowAPI.Model;
using Config;
using Config.Logic;
using OEMSzsowAPI.Helper;
using Formula.Helper;
using System.Data;
using Formula;
namespace OEMSzsowAPI.ApiLogic
{
    public class Project : BaseApi
    {

        SQLHelper _BusinessSQLHelper;
        public SQLHelper BusinessSQLHelper
        {
            get
            {
                if (_BusinessSQLHelper == null)
                    _BusinessSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
                return _BusinessSQLHelper;
            }
        }
        
        public Project(S_OEM_TaskList task)
            : base(task)
        { }

        public override void SaveLogic(string businessID)
        {
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/edit";
            //设置参数
            #region 设置参数
            var dt = this.BusinessSQLHelper.ExecuteDataTable("select * from S_I_ProjectInfo where id='" + businessID + "'");
            var projectInfo = new Dictionary<string, object>();
            if (dt.Rows.Count > 0)
                projectInfo = DataHelper.DataRowToDic(dt.Rows[0]);
            else
                throw new Exception("未找到指定ID的业务数据");
            var wbsDt = this.BusinessSQLHelper.ExecuteDataTable("select * from S_W_WBS where ProjectInfoID='" + businessID + "'");
            var rbsDt = this.BusinessSQLHelper.ExecuteDataTable("select * from S_W_RBS where ProjectInfoID='" + businessID + "'");
            var param = new ProjectRequestData();
            param.id = projectInfo.GetValue("ID");
            param.code = projectInfo.GetValue("Code");
            param.name = projectInfo.GetValue("Name");
            param.constructionUnit = projectInfo.GetValue("CustomerName");
            param.enable = true;
            var state = projectInfo.GetValue("State");
            var enableStates = new string[] { "Finish", "Pause", "Terminate" };//完工、暂停、终止
            if (enableStates.Contains(state))
                param.enable = false;
            //项目经理
            #region 项目经理
            var pmList = new List<string>();
            var pmRows = rbsDt.Select("RoleCode='" + ProjectRole.ProjectManager.ToString() + "'");
            foreach (DataRow row in pmRows)
            {
                if (row["UserID"] == null || row["UserID"] == DBNull.Value)
                    continue;
                var userId = row["UserID"].ToString();
                if (string.IsNullOrEmpty(userId))
                    continue;
                if (!pmList.Contains(userId))
                    pmList.Add(userId);
            }
            param.owerUserId = string.Join(",", pmList.ToArray());

            #endregion
            //阶段
            #region 阶段
            param.phases = new List<phase>();
            var phaseRows = wbsDt.Select("WBSType='" + WBSNodeType.Phase.ToString() + "'");
            foreach (DataRow row in phaseRows)
            {
                var item = new phase();
                param.phases.Add(item);
                var phaseWBSID = row["ID"].ToString();
                item.PhaseName = row["Name"].ToString();
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
                            var roleUserRows = rbsDt.Select("WBSID='" + majorRow["ID"].ToString() + "'");
                            var majorName = majorRow["Name"].ToString();
                            setProjectUser(roleUserRows, item.phaseUsers, majorName);
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
                                var roleUserRows = rbsDt.Select("WBSID='" + majorRow["ID"].ToString() + "'");
                                var majorName = majorRow["Name"].ToString();
                                setProjectUser(roleUserRows, subProject.users, majorName);
                            }
                        }
                    }
                }
            }

            #endregion
            #endregion
            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<ProjectRequestData>(param);
            var response = HttpHelper.Post(api, param);
            this.Task.Response = response;
        }

        public void setProjectUser(DataRow[] roleUserRows, List<ProjectUser> projectUsers, string majorName)
        {
            string[] roles = new string[] { ProjectRole.Designer.ToString(), ProjectRole.Collactor.ToString(), ProjectRole.Auditor.ToString()
            , ProjectRole.Approver.ToString(), ProjectRole.Mapper.ToString(),ProjectRole.MajorPrinciple.ToString()};
            foreach (DataRow roleUser in roleUserRows)
            {
                var userID = roleUser["UserID"].ToString();
                var roleCode = roleUser["RoleCode"].ToString();
                if (!roles.Contains(roleCode)) continue;
                var userItem = projectUsers.FirstOrDefault(a => a.UserID == userID && a.SpecialtyName == majorName );
                if (userItem == null)
                {
                    userItem = new ProjectUser()
                    {
                        UserID = userID,
                        PM = false,
                        Master = false,
                        Proof = false,
                        Audit = false,
                        Approval = false,
                        Drafting = false,
                        SpecialtyName = majorName
                    };
                    projectUsers.Add(userItem);
                }
                if (roleCode == ProjectRole.MajorPrinciple.ToString())
                    userItem.Master = true;
                else if (roleCode == ProjectRole.Collactor.ToString())
                    userItem.Proof = true;
                else if (roleCode == ProjectRole.Auditor.ToString())
                    userItem.Audit = true;
                else if (roleCode == ProjectRole.Approver.ToString())
                    userItem.Approval = true;
                else if (roleCode == ProjectRole.Mapper.ToString())
                    userItem.Drafting = true;
            }
        }

        public override void RemoveLogic(string businessID)
        {
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/delete";
            //设置参数
            var param = new DeleteRequestData();
            param.id = businessID;
            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<DeleteRequestData>(param);
            var response = HttpHelper.Post(api, param);
            this.Task.Response = response;
        }

        public List<string> GetIDs()
        {
            var api = this.BaseServerUrl + "project/getdata";
            var rtn = new List<string>();
            try
            {
                StartSync();
                //请求
                this.Task.RequestUrl = api;
                var response = HttpHelper.Get(api);
                this.Task.Response = response;
                var dataStr = AnalysisResponse();
                ComplateSync();
                if (dataStr.StartsWith("["))
                {
                    var list = JsonHelper.ToObject<List<Dictionary<string, object>>>(dataStr);
                    rtn = list.Select(a => a.GetValue("ID")).ToList();
                }
            }
            catch (Exception e)
            {
                LogWriter.Error(e, string.Format("TaskID：{0}，错误：{1}", Task.ID, e.Message));
                ErrorSync(e.Message);
            }
            return rtn;
        }
    }
    public class DeleteRequestData
    {
        public string id { get; set; }
    }
    public class ProjectRequestData
    {
        public string id { get; set; }//项目id
        public string code { get; set; }//项目编号
        public string name { get; set; }//项目名称
        public string owerUserId { get; set; }//项目经理
        public string constructionUnit { get; set; }//建设单位
        public List<phase> phases { get; set; }//阶段
        public bool enable { get; set; }//是否停用
    }
    public class phase
    {
        public string PhaseName { get; set; }//阶段名称
        public List<ProjectUser> phaseUsers { get; set; }//专业、用户
        public List<subProject> subProjects { get; set; }//子项
    }
    public class ProjectUser
    {
        public string UserID { get; set; }//用户id
        public string SpecialtyName { get; set; }//专业名称
        public bool PM { get; set; }//项目经理
        public bool Master { get; set; }//设计
        public bool Proof { get; set; }//校核
        public bool Audit { get; set; }//审核
        public bool Approval { get; set; }//审定
        public bool Drafting { get; set; }//制图
    }
    public class subProject
    {
        public string id { get; set; }//子项WBSID名称
        public string Name { get; set; }//子项名称
        public string Code { get; set; }//子项编号
        public List<ProjectUser> users { get; set; }//专业、用户
    }
}
