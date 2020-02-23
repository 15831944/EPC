using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using OEMSzsowAPI.Helper;
using Formula.Helper;
using OEMSzsowAPI.Model;
using Formula;
using Config;
using System.Data;
using OEMSzsowAPI.Common;
using System.Configuration;

namespace OEMSzsowAPI.ApiLogic
{
    public class Task : BaseApi
    {
        string OEMCollactorSerial = ConfigurationManager.AppSettings["OEMCollactorSerial"];//OEM校对环节编号
        string OEMAuditorSerial = ConfigurationManager.AppSettings["OEMAuditorSerial"];//OEM审核环节编号
        string OEMApproverSerial = ConfigurationManager.AppSettings["OEMApproverSerial"];//OEM审定环节编号
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
        
        public Task(S_OEM_TaskList task)
            : base(task)
        { }

        public override void SaveLogic(string businessID)
        {
            var dt = this.BusinessSQLHelper.ExecuteDataTable(@"select t.*,wbs.Name as SubProjectWBSName from S_W_TaskWork t
left join S_W_WBS wbs on  CHARINDEX(wbs.ID,t.WBSFullID,1)>0 and wbs.WBSType='SubProject'
where t.ProjectInfoID='" + businessID + "' ");
            if (dt.Rows.Count <= 0)
                throw new Exception("未找到指定ID的业务数据");
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/edit";
            //设置参数
            var param = new ProjectTaskRequestData();
            param.projectId = businessID;
            param.tasks = new List<task>();
            foreach (DataRow row in dt.Rows)
            {
                var item = new task();
                item.id = row["ID"].ToString();
                item.name = row["Name"].ToString();
                item.userId = row["DesignerUserID"].ToString();
                if (row["SubProjectWBSName"] != null && row["SubProjectWBSName"] != DBNull.Value)
                    item.subProjectName = row["SubProjectWBSName"].ToString();
                item.phaseName = GlobalData.PhaseEnum.GetValue(row["PhaseValue"].ToString());
                item.specialtyName = GlobalData.MajorEnum.GetValue(row["MajorValue"].ToString());
                param.tasks.Add(item);
            }

            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<ProjectTaskRequestData>(param);
            var response = HttpHelper.Post(api, param);
            this.Task.Response = response;
        }

        public override void GetDataLogic(string businessID)
        {
            if (string.IsNullOrEmpty(businessID))
                return;
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/getdata?projectId=" + businessID;
            //请求
            this.Task.RequestUrl = api;
            var response = HttpHelper.Get(api);
            this.Task.Response = response;
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

        public override void AfterGetData(Dictionary<string, object> data)
        {
            if (this.Task.BusinessType != BusinessType.GetData.ToString())
                return;
            var projectInfoID = this.Task.BusinessID;
            var wbsDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_W_WBS where ProjectInfoID='" + projectInfoID + "'");
            var productDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_E_Product where ProjectInfoID='" + projectInfoID + "'");
            var versionDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_E_ProductVersion where ProjectInfoID='" + projectInfoID + "'");
            var auditDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from T_EXE_Audit where ProjectInfoID='" + projectInfoID + "'");
            var auditAdviceDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from T_EXE_Audit_AdviceDetail where T_EXE_AuditID in (select id from T_EXE_Audit where ProjectInfoID='" + projectInfoID + "') ");
            var mistakeDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_AE_Mistake where ProjectInfoID='" + projectInfoID + "'");
            var taskDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_W_TaskWork where ProjectInfoID='" + projectInfoID + "'");
            StringBuilder sb = new StringBuilder();
            StringBuilder basesb = new StringBuilder();
            var Tasks = JsonHelper.ToList(data.GetValue("Tasks"));//任务集合
            var Files = JsonHelper.ToList(data.GetValue("Files"));//原图集合，发送校审后有数据
            //var Versions = JsonHelper.ToList(data.GetValue("Versions"));//原图版本数据，根据日期获取最新版，
            var Proofs = JsonHelper.ToList(data.GetValue("Proofs"));//校审记录 校审通过后md5字段才有数据
            var Comments = JsonHelper.ToList(data.GetValue("Comments"));//批注意见
            var errorList = new List<Dictionary<string, string>>();
            foreach (var task in Tasks)
            {
                var taskID = task.GetValue("TaskID");
                var taskRow = taskDt.Select("ID='" + taskID + "'").FirstOrDefault();
                if (taskRow == null)
                {
                    //删除
                    basesb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        Task.OEMCode, BusinessType.Remove.ToString(), Task.BusinessCode, taskID, Task.CreateTime.ToString());
                    basesb.AppendLine();
                }
            }
            foreach (var file in Files)
            {
                var fileID = file.GetValue("ID");
                var wbsID = string.Empty;
                var proof = Proofs.Where(a => a.GetValue("FileId") == fileID)
                    .OrderByDescending(a => Convert.ToInt64(a.GetValue("CreateDate"))).FirstOrDefault();
                if (proof == null)
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileId", fileID }, { "Name", file.GetValue("Name") }, { "Msg", "proof为null" } });
                    continue;
                }
                #region 获得WBSID
                string phaseName = file.GetValue("PhaseName"), subProjectName = file.GetValue("SubProjectName"),
                    majorName = file.GetValue("SpecialtyName"), taskName = file.GetValue("TaskName");
                var wbsRow = GetWBS(wbsDt, phaseName,subProjectName,majorName,taskName);
                if (wbsRow != null)
                    wbsID = wbsRow["ID"].ToString();
                if (string.IsNullOrEmpty(wbsID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileId", fileID }, { "Name", file.GetValue("Name") }, { "Msg", "WBSID为null" } });
                    continue;
                }
                var rootWbs = wbsDt.Select("WBSType='" + WBSNodeType.Project.ToString() + "'").FirstOrDefault();
                if (rootWbs == null)
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileId", fileID }, { "Name", file.GetValue("Name") }, { "Msg", "WBSRoot为null" } });
                    continue;
                }
                #endregion
                var description = file.GetValue("Description");
                if (string.IsNullOrEmpty(description))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileId", fileID }, { "Name", file.GetValue("Name") }, { "Msg", "Description为null，未开启校审识别图框" } });
                    continue;
                }
                var drawings = JsonHelper.ToList(description);//图框信息
                if (drawings.Count == 0)
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileId", fileID }, { "Name", file.GetValue("Name") }, { "Msg", "Description为[]，未检测到图框信息" } });
                    continue;
                }
                var state = proof.GetValue("State");
                //循环图框
                foreach (var drawing in drawings)
                {
                    var code = drawing.GetValue("DrawingNo");
                    if (string.IsNullOrEmpty(code))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileId", fileID }, { "DrawingNo", code }, { "Msg", "DrawingNo为null" } });
                        continue;
                    }
                    var name = drawing.GetValue("DrawingName");
                    var size = drawing.GetValue("Size");
                    var designerName = drawing.GetValue("Designer");//图框输入值
                    #region 生成业务记录S_E_Product、S_E_ProductVersion
                    #region S_E_Product
                    var productRow = productDt.Select("Code='" + code + "' and Name='" + name + "' and WBSID ='" + wbsID + "'").FirstOrDefault();
                    var auditId = string.Empty;
                        var productId = string.Empty;
                    if (productRow == null)
                    {
                        productRow = productDt.NewRow();
                        productDt.Rows.Add(productRow);
                        auditId = GuidHelper.CreateGuid();
                        productId = GuidHelper.CreateGuid();
                        productRow["ID"] = productId;
                        productRow["ProjectInfoID"] = projectInfoID;
                        productRow["WBSID"] = wbsID;
                        productRow["WBSFullID"] = wbsRow["FullID"].ToString();
                        productRow["AuditID"] = auditId;
                        productRow["CreateUser"] = file.GetValue("LockUserName");
                        productRow["CreateUserID"] = file.GetValue("LockUserID");
                        productRow["CreateDate"] = DateTime.Now;
                        productRow["ModifyUser"] = productRow["CreateUser"];
                        productRow["ModifyUserID"] = productRow["CreateUserID"];
                        productRow["ModifyDate"] = productRow["CreateDate"];
                        productRow["FileType"] = "图纸";
                        productRow["Version"] = 1;
                        productRow["State"] = "Create";
                        productRow["SignState"] = "False";
                        productRow["CoSignState"] = "NoSign";
                        productRow["ArchiveState"] = "False";
                        productRow["PrintState"] = "UnPrint";
                    }
                    else
                    {
                        productId = productRow["ID"].ToString();
                        auditId = productRow["AuditID"].ToString();
                        productRow["ModifyUser"] = file.GetValue("LockUserID");
                        productRow["ModifyUserID"] = file.GetValue("LockUserName");
                        productRow["ModifyDate"] = DateTime.Now;
                    }
                    productRow["SwfFile"] = fileID;//此模式下SwfFile、ShotSnap无用，所以记录FileID和MD5，在GetAreaData接口根据这两个值绑定Mainfile 和 Pdffile
                    productRow["ShotSnap"] = proof.GetValue("Md5");//proof的Md5 是在校审完成后才会赋值
                    productRow["Name"] = name;
                    productRow["Code"] = code;
                    productRow["FileSize"] = size;
                    productRow["SubmitDate"] = DataHelper.FormatTime(proof.GetValue("CreateDate"));
                    /*Proofs字段说明
                     * Step1State - Step6State：step1 = 校对，2 = 审核，3 = 审定
                         值：0 = 还未送到当前节点，1 = 未读，2 = 校对中/审核中/审定中，3 = 退回，4 = 完成
                        State：0 = 未完成，1 = 完成，待最后确认（和存储相关，可忽略），2 = 完成             */
                    if (state == "0")
                        productRow["AuditState"] = "Flow";
                    else
                    {
                        productRow["AuditState"] = "Pass";
                        productRow["AuditPassDate"] = DateTime.Now;
                    }
                    if (GlobalData.MajorEnum.Any(a => a.Value == majorName))
                        productRow["MajorValue"] = GlobalData.MajorEnum.FirstOrDefault(a => a.Value == majorName).Key;
                    if (GlobalData.PhaseEnum.Any(a => a.Value == phaseName))
                        productRow["PhaseValue"] = GlobalData.PhaseEnum.FirstOrDefault(a => a.Value == phaseName).Key;
                    productRow["SubProjectInfo"] = subProjectName;
                    //TaskName
                    if (wbsRow["WBSType"].ToString() == WBSNodeType.Work.ToString())
                    {
                        productRow["PackageName"] =taskName;
                        productRow["PackageCode"] = wbsRow["Code"].ToString();
                    }
                    //设校审人员
                    productRow["Designer"] = file.GetValue("LockUserID");
                    productRow["DesignerName"] = file.GetValue("LockUserName");
                    var proofUsers = JsonHelper.ToList(proof.GetValue("ProofUsers"));
                    foreach (var proofUser in proofUsers)
                    {
                        var userid = proofUser.GetValue("UserId") ;
                        var userRow = GlobalData.UserDt.Select("ID='" + userid + "'").FirstOrDefault();
                        if (userRow == null)
                            continue;
                        var userName = userRow["Name"].ToString();
                        var proofSerial = proofUser.GetValue("Serial");
                        var auditType = string.Empty;
                        if (proofSerial == OEMCollactorSerial)
                            auditType = "Collactor";
                        else if (proofSerial == OEMAuditorSerial)
                            auditType = "Auditor";
                        else if (proofSerial == OEMApproverSerial)
                            auditType = "Approver";
                        if (!string.IsNullOrEmpty(auditType))
                        {
                            productRow[auditType] = userid;
                            productRow[auditType + "Name"] = userName;
                        }
                    }

                    string productSql = SQLHelper.CreateUpdateSql("S_E_Product", productRow);
                    sb.AppendLine(productSql);
                    #endregion
                    #region S_E_ProductVersion

                    //version
                    var versionRow = versionDt.Select("ProductID='" + productId + "'").FirstOrDefault();
                    if (versionRow == null)
                    {
                        versionRow = versionDt.NewRow();
                        versionDt.Rows.Add(versionRow);
                        versionRow["ID"] = GuidHelper.CreateGuid();
                        versionRow["ProductID"] = productId;
                    }
                    foreach (DataColumn col in versionDt.Columns)
                    {
                        if (col.ColumnName == "ID") continue;
                        if (productDt.Columns.Contains(col.ColumnName))
                            versionRow[col] = productRow[col.ColumnName];
                    }
                    string versionSql = SQLHelper.CreateUpdateSql("S_E_ProductVersion", versionRow);
                    sb.AppendLine(versionSql);
                    #endregion
                    #endregion
                    #region 生成校审记录T_EXE_Audit、T_EXE_Audit_AdviceDetail、S_AE_Mistake
                    /*Proofs字段说明
             * Step1State - Step6State：step1 = 校对，2 = 审核，3 = 审定
                 值：0 = 还未送到当前节点，1 = 未读，2 = 校对中/审核中/审定中，3 = 退回，4 = 完成
                State：0 = 未完成，1 = 完成，待最后确认（和存储相关，可忽略），2 = 完成             */
                    #region T_EXE_Audit
                    var auditRow = auditDt.Select("ID='" + auditId + "'").FirstOrDefault();
                    if (auditRow == null)
                    {
                        auditRow = auditDt.NewRow();
                        auditDt.Rows.Add(auditRow);
                        auditRow["ID"] = auditId;
                        auditRow["ProjectInfoID"] = projectInfoID;
                        auditRow["WBSID"] = wbsID;
                        auditRow["ProjectInfoName"] = rootWbs["Name"].ToString();
                        auditRow["ProjectInfoCode"] = rootWbs["Code"].ToString();
                        auditRow["SubProjectName"] = subProjectName;
                        auditRow["PhaseCode"] = productRow["PhaseValue"];
                        auditRow["MajorCode"] = productRow["MajorValue"];
                        auditRow["CreateUser"] = productRow["CreateUser"];
                        auditRow["CreateUserID"] = productRow["CreateUserID"];
                        auditRow["CreateDate"] = DataHelper.FormatTime(proof.GetValue("CreateDate"));
                        auditRow["ModifyDate"] = auditRow["CreateDate"];
                    }
                    else
                        auditRow["ModifyDate"] = DateTime.Now;
                    auditRow["FlowPhase"] = state == "0" ? "Processing" : "End";
                    auditRow["SerialNumber"] = code;
                    //设置校审人员
                    auditRow["Designer"] = productRow["Designer"];
                    auditRow["DesignerName"] = productRow["DesignerName"];
                    auditRow["Collactor"] = productRow["Collactor"];
                    auditRow["CollactorName"] = productRow["CollactorName"];
                    auditRow["Auditor"] = productRow["Auditor"];
                    auditRow["AuditorName"] = productRow["AuditorName"];
                    auditRow["Approver"] = productRow["Approver"];
                    auditRow["ApproverName"] = productRow["ApproverName"];

                    string auditSql = SQLHelper.CreateUpdateSql("T_EXE_Audit", auditRow);
                    sb.AppendLine(auditSql);
                    #endregion
                    //校审意见
                    var comments = Comments.Where(a => a.GetValue("FileID") == fileID && a.GetValue("DrawingNo") == code).ToList();
                    foreach (var comment in comments)
                    {
                        #region T_EXE_Audit_AdviceDetail
                        var adviceID = comment.GetValue("ID");
                        var isConfirm = false;
                        if (comment.GetValue("IsConfirm") == "true")
                            isConfirm = true;
                        var adviceRow = auditAdviceDt.Select("ID='" + adviceID + "'").FirstOrDefault();
                        if (adviceRow == null)
                        {
                            adviceRow = auditAdviceDt.NewRow();
                            auditAdviceDt.Rows.Add(adviceRow);
                            adviceRow["ID"] = adviceID;
                            adviceRow["T_EXE_AuditID"] = auditId;
                            adviceRow["ProductCode"] = code;
                            var mistakeDate = DataHelper.FormatTime(comment.GetValue("CreateDate"));
                            adviceRow["CreateDate"] = mistakeDate;
                            adviceRow["MistakeYear"] = mistakeDate.Year;
                            adviceRow["MistakeMonth"] = mistakeDate.Month;
                            adviceRow["MistakeSeason"] = (mistakeDate.Month + 2) / 3;
                        }
                        /*Comments字段说明
                        Type：0 = 普通批注，1 = 校对批注，2 = 审核批注，3 = 审定批注
                        CommentType：1 = 严重，2 = 一般，4 = 提醒 */
                        adviceRow["MsitakeContent"] = comment.GetValue("Content");
                        adviceRow["MistakeType"] = "CommentType" + comment.GetValue("CommentType");
                        var step = AuditType.Design.ToString();
                        switch (comment.GetValue("Type"))
                        {
                            case "1": step = AuditType.Collact.ToString(); break;
                            case "2": step = AuditType.Audit.ToString(); break;
                            case "3": step = AuditType.Approve.ToString(); break;
                            case "0":
                            default: step = AuditType.Design.ToString(); break;
                        }
                        adviceRow["Step"] = step;
                        adviceRow["SubmitUser"] = comment.GetValue("CriticUserID");
                        adviceRow["SubmitUserName"] = comment.GetValue("CriticUserName");
                        var  rpyStr = string.Empty;
                        var replys = JsonHelper.ToList(comment.GetValue("ReplyContent"));
                        foreach (var reply in replys)
                            rpyStr += string.Format("{0}({1})：{2}\r\n",reply.GetValue("author"),DataHelper.FormatTime( reply.GetValue("time")),reply.GetValue("content"));
                        adviceRow["ResponseContent"] = rpyStr;
                        string adviceSql = SQLHelper.CreateUpdateSql("T_EXE_Audit_AdviceDetail", adviceRow);
                        sb.AppendLine(adviceSql);


                         #endregion
                        #region S_AE_Mistake
                        
                        var  mistakeRow =  mistakeDt.Select("ID='" + adviceID + "'").FirstOrDefault();
                        if (mistakeRow == null)
                        {
                            mistakeRow = mistakeDt.NewRow();
                            mistakeDt.Rows.Add(mistakeRow);
                            mistakeRow["ID"] = adviceID;
                            mistakeRow["AuditID"] = auditId;
                            mistakeRow["ProjectInfoID"] = projectInfoID;
                            mistakeRow["AuditActivityType"] = adviceRow["Step"];
                            mistakeRow["MistakeLevel"] = adviceRow["MistakeType"];
                            mistakeRow["MajorCode"] = auditRow["MajorCode"];
                            mistakeRow["MajorName"] = majorName;
                            mistakeRow["DesignerID"] = auditRow["Designer"];
                            mistakeRow["Designer"] = auditRow["DesignerName"];
                            mistakeRow["CreateDate"] = adviceRow["CreateDate"];
                            mistakeRow["MistakeYear"] = adviceRow["MistakeYear"];
                            mistakeRow["MistakeMonth"] = adviceRow["MistakeMonth"];
                            mistakeRow["MistakeSeason"] = adviceRow["MistakeSeason"];
                            mistakeRow["CreateUserID"] = adviceRow["SubmitUser"];
                            mistakeRow["CreateUser"] = adviceRow["SubmitUserName"];
                        }
                        mistakeRow["MistakeContent"] = adviceRow["MsitakeContent"];
                        mistakeRow["Measure"] = adviceRow["ResponseContent"];
                        mistakeRow["DrawingNO"] = adviceRow["ProductCode"];
                        mistakeRow["DeptID"] = rootWbs["WBSDeptID"];
                        mistakeRow["DeptName"] = rootWbs["WBSDeptName"];
                        mistakeRow["State"] = isConfirm ? "Finish" : "Create";
                        string mistakeSql = SQLHelper.CreateUpdateSql("S_AE_Mistake", mistakeRow);
                        sb.AppendLine(mistakeSql);
                        #endregion
                    }     
                    #endregion
                }
            }
            if (errorList.Count > 0)
                this.Task.ErrorMsg = JsonHelper.ToJson(errorList);
            if (sb.Length > 0)
                this.BusinessSQLHelper.ExecuteNonQuery(sb.ToString());
            if (basesb.Length > 0)
                this.BaseSQLHelper.ExecuteNonQuery(basesb.ToString());
        }

        public static void test()
        {
            var prjid = "a8530114-ce01-46f0-bccc-a0253c4dbe47";//"a93d00ee-c52a-4852-8f0b-75cc23521278";
            var api = "http://www.szsow.com/api/goodway/Task/getdata?projectId=a8530114-ce01-46f0-bccc-a0253c4dbe47";
            var response = HttpHelper.Get(api);
            var rtn = JsonHelper.ToObject<Dictionary<string, object>>(response);
            var appendData = JsonHelper.ToObject<Dictionary<string, object>>(rtn.GetValue("AppendData"));
            new Task(new S_OEM_TaskList() { BusinessID = prjid, BusinessType = BusinessType.GetData.ToString() }).AfterGetData(appendData);
        }
                
        private DataRow GetWBS(DataTable wbsDt, string phaseName, string subProjectName, string majorName,string taskName)
        {
            var phaseRow = wbsDt.Select("WBSType='Phase' and Name='" + phaseName + "'").FirstOrDefault();
            if (phaseRow == null) return null;
            if (!string.IsNullOrEmpty(subProjectName))
            {
                var subProjectRow = wbsDt.Select("WBSType='SubProject' and Name='" + subProjectName + "' and FullID like '" + phaseRow["FullID"] + "%'").FirstOrDefault();
                if (subProjectRow == null) return null;
                var majorRow = wbsDt.Select("WBSType='Major' and Name='" + majorName + "' and FullID like '" + subProjectRow["FullID"] + "%'").FirstOrDefault();
                if (majorRow == null) return null;
                if (!string.IsNullOrEmpty(taskName))
                    return wbsDt.Select(" Name='" + taskName + "' and FullID like '" + majorRow["FullID"] + "%'").FirstOrDefault();
                else
                    return majorRow;
            }
            else
            {
                var majorRow = wbsDt.Select("WBSType='Major' and Name='" + majorName + "' and FullID like '" + phaseRow["FullID"] + "%'").FirstOrDefault();
                if (majorRow == null) return null;
                if (!string.IsNullOrEmpty(taskName))
                    return wbsDt.Select(" Name='" + taskName + "' and FullID like '" + majorRow["FullID"] + "%'").FirstOrDefault();
                else
                    return majorRow;
            }
        }
    }
    public class ProjectTaskRequestData
    {
        public string projectId { get; set; }//项目id
        public List<task> tasks { get; set; }//设计任务列表
    }
    public class task
    {
        public string id { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public string phaseName { get; set; }//阶段名称
        public string subProjectName { get; set; }//子项名称
        public string specialtyName { get; set; }//专业名称
    }
}
