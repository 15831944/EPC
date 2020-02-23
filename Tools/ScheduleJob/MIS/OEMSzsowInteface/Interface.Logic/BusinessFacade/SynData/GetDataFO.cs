using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;
using Config;
using Formula;

namespace Interface.Logic
{
    public class GetDataFO : SynDataFO
    {
        public void CreateGetQueue()
        {
            StringBuilder interfaceSb = new StringBuilder();

            //先创建成果、再创建校审单等，此顺序不能调整，校审结束后会删除拆分图，再次新增；必须在校审单之前执行删除拆分图，再次新增的逻辑；
            CreateGetQueue("S_E_Product", "Product", interfaceSb);//获取成果（拆分图、无附件）
            CreateGetQueue("S_E_Product", "Plan", interfaceSb);//获取发图计划里程碑成果子表（拆分图）、同时绑定成果的附件
            CreateGetQueue("I_FlowAuditProduct", "", interfaceSb);//获取校审成果（原图）、同时更新拆分图校审信息
            CreateGetQueue("I_FlowExchangeProduct", "", interfaceSb);//获取提资成果（原图）、同时如果是计划内提资里程碑文件子表，更新提资计划里程碑成果子表
            CreateGetQueue("I_FlowSignProduct", "", interfaceSb);//获取会签成果（原图）、同时更新拆分图会签信息
            CreateGetQueue("I_FlowChangeProduct", "", interfaceSb);//获取设计变更成果（原图）

            DeleteGetLog(interfaceSb);
            if (interfaceSb.Length > 0)
                this.SQLHelperInterface.ExecuteNonQuery(interfaceSb.ToString());
        }

        public void CreateGetQueue(string RelateTable, string RelateType, StringBuilder interfaceSb)
        {
            var getUrl = this.BaseServerUrl;
            switch (RelateTable)
            {
                case "S_E_Product":
                    if (RelateType == "Product") getUrl += "product/getallsplitinfo";//成果（拆分图，无附件，发起校审时可获取、校审通过时再次获取）
                    else if (RelateType == "Plan") getUrl += "product/getdata";//绑定成果附件、同时增加里程碑子表数据
                    break;
                case "I_FlowAuditProduct": getUrl += "flow/getallaudit"; break;//同时更改成功的校审信息
                case "I_FlowExchangeProduct": getUrl += "flow/getallexchange"; break;//同时增加里程碑子表数据
                case "I_FlowSignProduct": getUrl += "flow/getallsign"; break;//同时更改成果的会签信息
                case "I_FlowChangeProduct": getUrl += "flow/getallchange"; break;
                default:
                    break;
            }

            var getSql = @"select top 1 SynTime from I_GetDataLog where RelateTable ='" + RelateTable + "' and RelateType='" + RelateType + "' order by SynTime desc";
            var lastGetSynTime = this.SQLHelperInterface.ExecuteScalar(getSql);
            var queryStamp = DateTime.Now.ToString("yyyyMM");//首次取当月数据
            if (lastGetSynTime != null && lastGetSynTime != DBNull.Value)
                queryStamp = Convert.ToDateTime(lastGetSynTime).ToString("yyyyMMddHHmmssfff");
            getUrl += "?stamp=" + queryStamp;
            string GetQueueSqlTmp = @"
if exists(select 1 from I_DataSynQueue where SynType='{0}' and RelateTable='{1}' and RelateType='{3}' and SynState ='" + SynState.New.ToString() + @"')
    update I_DataSynQueue set CreateTime = '{4}', RequestData='{5}',  RequestUrl='{6}' where SynType='{0}' and RelateTable='{1}' and RelateType='{3}' and SynState ='" + SynState.New.ToString() + @"'
else
    insert into I_DataSynQueue(SynType,RelateTable,RelateID,RelateType,CreateTime,RequestData,RequestUrl,SynState) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','" + SynState.New.ToString() + "')";

            var queueSql = string.Format(GetQueueSqlTmp, SynType.GetData.ToString(), RelateTable, "", RelateType
                     , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "", getUrl);
            interfaceSb.AppendLine(queueSql);
        }
        
        //删除7天以前的 获取日志记录
        public void DeleteGetLog(StringBuilder interfaceSb, string relatetable = null)
        {
            var date = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "delete from I_GetDataLog where SynTime<'" + date + "'";
            if (!string.IsNullOrEmpty(relatetable))
                sql += " and RelateTable ='" + relatetable + "'";
            interfaceSb.AppendLine(sql);
        }

        public override void ExecuteQueueLogic(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            switch (queueData.RelateTable)
            {
                case "S_E_Product": ExecuteProduct(queueData, responseDataStr, interfaceSb); break;
                case "I_FlowAuditProduct":
                case "I_FlowChangeProduct": ExecuteAudit(queueData, responseDataStr, interfaceSb); break;
                case "I_FlowExchangeProduct": ExecuteExchange(queueData, responseDataStr, interfaceSb); break;
                case "I_FlowSignProduct": ExecuteAuditSign(queueData, responseDataStr, interfaceSb); break;
                default:
                    break;
            }
        }
        
        private void ExecuteProduct(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            if (queueData.SynType != SynType.GetData.ToString())
                return;
            if (queueData.RelateType == "Product")
            {
                #region 获取拆分成果（校审时、校审后、无附件）
                StringBuilder projectsb = new StringBuilder();
                var errorList = new List<Dictionary<string, string>>();
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synTime = DataHelper.FormatTime(responseData.GetValue("Stamp"));
                var files = JsonHelper.ToList(responseData.GetValue("Files"));
                var productDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_E_Product where 1!=1");
                var versionDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_E_ProductVersion where 1!=1");
                var wbsDtDic = new Dictionary<string, DataTable>();
                //生成 S_E_Product数据   
                //循环返回列表（原图集合）
                foreach (var file in files)
                {
                    var fileID = file.GetValue("FileId");//原图ID
                    var fileName = file.GetValue("FileName"); //原图名称
                    var framesJson = file.GetValue("Frames");
                    var projectInfoID = file.GetValue("ProjectId");
                    //刚发起校审、与校审完成时都回返回数据，发起校审时，可能没有图框、校审中也可能变图号，所以每次都把原图下的所有拆分图都删除，再重新增加
                    projectsb.AppendLine("delete from S_E_Product where SwfFile='" + fileID + "'");

                    #region 图框校验，没有图框则跳过

                    if (string.IsNullOrEmpty(framesJson))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "Frames为null，未开启校审识别图框" } });
                        continue;
                    }
                    var frames = JsonHelper.ToList(framesJson);
                    if (frames.Count == 0)
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "Frames为[]，未检测到图框信息" } });
                        continue;
                    }
                    if (string.IsNullOrEmpty(projectInfoID))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "ProjectId为null" } });
                        continue;
                    }
                    #endregion

                    #region 获得WBSID
                    var wbsDt = new DataTable();
                    if (wbsDtDic.ContainsKey(projectInfoID))
                        wbsDt = wbsDtDic[projectInfoID];
                    else
                    {
                        wbsDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_W_WBS with(nolock) where ProjectInfoID='" + projectInfoID + "'");
                        wbsDtDic[projectInfoID] = wbsDt;
                    }
                    var wbsID = string.Empty;
                    string phaseCode = file.GetValue("PhaseCode"), subProjectName = file.GetValue("SubProjectName"),
                        majorCode = file.GetValue("SpecialtyCode"), taskID = file.GetValue("TaskPackageId");
                    var wbsRow = GetWBS(wbsDt, phaseCode, subProjectName, majorCode, taskID);
                    if (wbsRow != null)
                        wbsID = wbsRow["ID"].ToString();
                    if (string.IsNullOrEmpty(wbsID))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "WBSID为null" } });
                        continue;
                    }
                    var rootWbs = wbsDt.Select("WBSType='" + WBSNodeType.Project.ToString() + "'").FirstOrDefault();
                    if (rootWbs == null)
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "WBSRoot为null" } });
                        continue;
                    }
                    #endregion

                    //循环返回文件列表
                    foreach (var frame in frames)
                    {
                        var code = frame.GetValue("DrawingNo");
                        var name = frame.GetValue("DrawingName");
                        var size = frame.GetValue("Size");
                        var designerName = frame.GetValue("Designer");//图框输入值
                        #region 生成业务记录S_E_Product、S_E_ProductVersion
                        #region S_E_Product
                        var productRow = productDt.NewRow();
                        var productId = GuidHelper.CreateGuid();
                        productRow["ID"] = productId;
                        productRow["ProjectInfoID"] = projectInfoID;
                        productRow["WBSID"] = wbsID;
                        productRow["WBSFullID"] = wbsRow["FullID"].ToString();
                        productRow["AuditID"] = "";
                        var modifyUserID = file.GetValue("LastUpdateUserId");
                        if (!string.IsNullOrEmpty(modifyUserID))
                        {
                            var userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == modifyUserID || a.ID == modifyUserID);
                            if (userRow != null)
                            {
                                productRow["CreateUserID"] = userRow.ID;
                                productRow["CreateUser"] = userRow.Name;
                            }
                            userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == modifyUserID || a.ID == modifyUserID);
                            if (userRow != null)
                            {
                                productRow["ModifyUserID"] = userRow.ID;
                                productRow["ModifyUser"] = userRow.Name;
                            }
                        }
                        productRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("CreateDate"));
                        productRow["ModifyDate"] = DataHelper.FormatTime(file.GetValue("LastUpdateTime"));
                        productRow["FileType"] = "图纸";
                        productRow["Version"] = 1;
                        productRow["State"] = "Create";
                        productRow["SignState"] = "False";
                        productRow["CoSignState"] = "NoSign";
                        productRow["ArchiveState"] = "False";
                        productRow["PrintState"] = "UnPrint";
                        productRow["SwfFile"] = fileID;//此模式下SwfFile、ShotSnap无用，所以记录FileID和MD5，在GetAreaData接口根据这两个值绑定Mainfile、Pdffile
                        // productRow["ShotSnap"] = file.GetValue("Md5");//proof的Md5 是在校审完成后才会赋值
                        productRow["Name"] = name;
                        productRow["Code"] = code;
                        productRow["FileSize"] = size;
                        productRow["SubmitDate"] = DataHelper.FormatTime(file.GetValue("SendTime"));
                        productRow["AuditState"] = "Flow";
                        productRow["MajorValue"] = majorCode;
                        productRow["PhaseValue"] = phaseCode;
                        productRow["SubProjectInfo"] = subProjectName;
                        //TaskName
                        if (wbsRow["WBSType"].ToString() == WBSNodeType.Work.ToString())
                        {
                            productRow["PackageName"] = wbsRow["Name"].ToString();
                            productRow["PackageCode"] = wbsRow["Code"].ToString();
                        }

                        string productSql = SQLHelper.CreateUpdateSql("S_E_Product", productRow);
                        projectsb.AppendLine(productSql);
                        #endregion

                        #region S_E_ProductVersion

                        //version
                        var versionRow = versionDt.NewRow();
                        versionDt.Rows.Add(versionRow);
                        versionRow["ID"] = GuidHelper.CreateGuid();
                        versionRow["ProductID"] = productId;

                        foreach (DataColumn col in versionDt.Columns)
                        {
                            if (col.ColumnName == "ID") continue;
                            if (productDt.Columns.Contains(col.ColumnName))
                                versionRow[col] = productRow[col.ColumnName];
                        }
                        string versionSql = SQLHelper.CreateUpdateSql("S_E_ProductVersion", versionRow);
                        projectsb.AppendLine(versionSql);
                        #endregion
                        #endregion
                    }
                }

                if (projectsb.Length > 0)
                    this.SQLHelpeProject.ExecuteNonQuery(projectsb.ToString());

                var errorStr = string.Empty;
                if (errorList.Count > 0)
                    errorStr = JsonHelper.ToJson(errorList);
                var getDataLogSql = @"insert into I_GetDataLog(RelateTable,RelateType,RequestUrl,SynTime,ErrorMsg) values ('{0}','{1}','{2}','{3}','{4}')";
                getDataLogSql = string.Format(getDataLogSql, "S_E_Product", queueData.RelateType, queueData.RequestUrl,
                    synTime.ToString("yyyy-MM-dd HH:mm:ss"), errorStr.Replace("'", "''"));

                interfaceSb.AppendLine(getDataLogSql);

                #endregion
            }
            else if (queueData.RelateType == "Plan")
            {
                #region 获取发图计划下成果，如果是校审过的，则更新校审的附件
                StringBuilder projectsb = new StringBuilder();
                var errorList = new List<Dictionary<string, string>>();
                var responseData = JsonHelper.ToObject(responseDataStr);
                var synTime = DataHelper.FormatTime(responseData.GetValue("Stamp"));
                var files = JsonHelper.ToList(responseData.GetValue("Files"));
                var productDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_P_MileStone_ProductDetail where 1!=1");
                //生成 S_P_MileStone_ProductDetail数据   
                //循环返回列表
                foreach (var file in files)
                {
                    var id = file.GetValue("FileId");//文件记录ID
                    var mileStoneID = file.GetValue("PlanId");
                    var projectInfoID = file.GetValue("ProjectId");
                    var fileName = file.GetValue("FileName"); //原图名称
                    var sourceFileId = file.GetValue("SourceFileId");//原图ID
                    var signatured = file.GetValue("Signatured");//是否签名，如果签名则md5 为签名的pdf文件
                    var ext = file.GetValue("Extension");
                    var md5Code = file.GetValue("Md5");
                    var drawingNo = string.Empty;
                    var attachInfoJson = file.GetValue("AttachInfo");//图框识别信息
                    if (string.IsNullOrEmpty(attachInfoJson))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "AttachInfo为null" } });
                    }
                    else
                        drawingNo = JsonHelper.ToObject(attachInfoJson).GetValue("图号");//拆分图图号

                    if (string.IsNullOrEmpty(projectInfoID))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "ProjectId为null" } });
                        continue;
                    }

                    #region 生成表数据

                    var productRow = productDt.NewRow();
                    productRow["ID"] = id;
                    productRow["S_P_MileStoneID"] = mileStoneID;
                    productRow["ProjectInfoID"] = projectInfoID;
                    productRow["Name"] = fileName;
                    productRow["Code"] = drawingNo;
                    productRow["MD5Code"] = md5Code;
                    var modifyUserID = file.GetValue("LastUpdateUserId");
                    if (!string.IsNullOrEmpty(modifyUserID))
                    {
                        var userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == modifyUserID || a.ID == modifyUserID);
                        if (userRow != null)
                        {
                             productRow["CreateUserID"]  = userRow.ID;
                            productRow["CreateUser"] = userRow.Name;
                        }
                        userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == modifyUserID || a.ID == modifyUserID);
                        if (userRow != null)
                        {
                            productRow["ModifyUserID"] =  userRow.ID;
                            productRow["ModifyUser"] = userRow.Name;
                        }
                    }
                    productRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("CreateDate"));
                    productRow["ModifyDate"] = DataHelper.FormatTime(file.GetValue("LastUpdateTime"));

                    string productSql = SQLHelper.CreateUpdateSql("S_P_MileStone_ProductDetail", productRow);
                    projectsb.AppendLine(productSql);
                    #endregion

                    #region 生成待同步文件队列
                    var productData = new Dictionary<string, string>();
                    productData.SetValue("Signatured", signatured);
                    productData.SetValue("SourceFileId", sourceFileId);
                    productData.SetValue("DrawingNo", drawingNo);
                    productData.SetValue("Extension", ext);
                    I_FileSynQueue.CreateDownloadQueue(interfaceSb, fileName, md5Code, "S_P_MileStone_ProductDetail", id, queueData.RelateType, JsonHelper.ToJson(productData));

                    #endregion
                }

                if (projectsb.Length > 0)
                    this.SQLHelpeProject.ExecuteNonQuery(projectsb.ToString());

                var errorStr = string.Empty;
                if (errorList.Count > 0)
                    errorStr = JsonHelper.ToJson(errorList);
                var getDataLogSql = @"insert into I_GetDataLog(RelateTable,RelateType,RequestUrl,SynTime,ErrorMsg) values ('{0}','{1}','{2}','{3}','{4}')";
                getDataLogSql = string.Format(getDataLogSql, "S_E_Product", queueData.RelateType, queueData.RequestUrl,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), errorStr.Replace("'", "''"));

                interfaceSb.AppendLine(getDataLogSql);

                #endregion
            }
        }

        private DataRow GetWBS(DataTable wbsDt, string phaseCode, string subProjectName, string majorCode, string taskID)
        {
            var phaseRow = wbsDt.Select("WBSType='Phase' and WBSValue='" + phaseCode + "'").FirstOrDefault();
            if (phaseRow == null) return null;
            if (!string.IsNullOrEmpty(subProjectName))
            {
                var subProjectRow = wbsDt.Select("WBSType='SubProject' and Name='" + subProjectName + "' and FullID like '" + phaseRow["FullID"] + "%'").FirstOrDefault();
                if (subProjectRow == null) return null;
                var majorRow = wbsDt.Select("WBSType='Major' and WBSValue='" + majorCode + "' and FullID like '" + subProjectRow["FullID"] + "%'").FirstOrDefault();
                if (majorRow == null) return null;
                if (!string.IsNullOrEmpty(taskID))
                    return wbsDt.Select(" ID='" + taskID + "' and FullID like '" + majorRow["FullID"] + "%'").FirstOrDefault();
                else
                    return majorRow;
            }
            else
            {
                var majorRow = wbsDt.Select("WBSType='Major' and WBSValue='" + majorCode + "' and FullID like '" + phaseRow["FullID"] + "%'").FirstOrDefault();
                if (majorRow == null) return null;
                if (!string.IsNullOrEmpty(taskID))
                    return wbsDt.Select(" ID='" + taskID + "' and FullID like '" + majorRow["FullID"] + "%'").FirstOrDefault();
                else
                    return majorRow;
            }
        }

        private void ExecuteAudit(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            if (queueData.SynType != SynType.GetData.ToString())
                return;
            #region 获取校审的原图，同时更新拆分图的校审信息
            StringBuilder projectsb = new StringBuilder();
            var errorList = new List<Dictionary<string, string>>();
            var responseData = JsonHelper.ToObject(responseDataStr);
            var synTime = DataHelper.FormatTime(responseData.GetValue("Stamp"));
            var files = JsonHelper.ToList(responseData.GetValue("Files"));
            var productDt = this.SQLHelperInterface.ExecuteDataTable(@"select * from " + queueData.RelateTable + " where 1!=1");
            var mistakeDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_AE_Mistake where 1!=1");
            var auditDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from T_EXE_Audit where 1!=1");
            var auditAdviceDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from T_EXE_Audit_AdviceDetail where 1!=1");
            var wbsDtDic = new Dictionary<string, DataTable>();
            //循环返回列表（原图集合）
            foreach (var file in files)
            {
                var id = file.GetValue("Id");//文件记录ID
                var fileName = file.GetValue("Name"); //原图名称
                var sourceFileId = file.GetValue("SourceFileId");//原图ID
                var md5Code = file.GetValue("Md5");
                var flowJson = file.GetValue("Proof");//审批记录Json
                var commentJson = file.GetValue("Comments");//批注json
                var projectInfoID = file.GetValue("ProjectId");
                string phaseCode = file.GetValue("PhaseCode"), subProjectName = file.GetValue("SubProjectName"),
                    majorCode = file.GetValue("SpecialtyCode"), taskID = file.GetValue("TaskID");

                if (string.IsNullOrEmpty(projectInfoID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "ProjectId为null" } });
                    continue;
                }

                #region 获得WBSID
                var wbsDt = new DataTable();
                if (wbsDtDic.ContainsKey(projectInfoID))
                    wbsDt = wbsDtDic[projectInfoID];
                else
                {
                    wbsDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_W_WBS with(nolock) where ProjectInfoID='" + projectInfoID + "'");
                    wbsDtDic[projectInfoID] = wbsDt;
                }
                var wbsID = string.Empty;
                var wbsRow = GetWBS(wbsDt, phaseCode, subProjectName, majorCode, taskID);
                if (wbsRow != null)
                    wbsID = wbsRow["ID"].ToString();
                if (string.IsNullOrEmpty(wbsID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "WBSID为null" } });
                    continue;
                }
                #endregion

                #region 处理明细Json：替换人员ID、增加人员名称，格式化时间
                var flowList = JsonHelper.ToList(flowJson);
                foreach (var item in flowList)
                {
                    var userId = item.GetValue("UserId");
                    var userName = string.Empty;
                    var _user = GlobalData.UserList.FirstOrDefault(a => a.SynID == userId || a.ID == userId);
                    if (_user != null)
                    {
                        userId = _user.ID;
                        userName = _user.Name;
                    }
                    item.SetValue("UserId", userId);
                    item.SetValue("UserName", userName);
                    item["OperationTime"] = DataHelper.FormatTime(item.GetValue("OperationTime"));
                }
                var commentList = JsonHelper.ToList(commentJson);
                foreach (var item in commentList)
                {
                    var userId = item.GetValue("CriticUserId");
                    var userName = string.Empty;
                    var _user = GlobalData.UserList.FirstOrDefault(a => a.SynID == userId || a.ID == userId);
                    if (_user != null)
                    {
                        userId = _user.ID;
                        userName = _user.Name;
                    }
                    item.SetValue("CriticUserId", userId);
                    item.SetValue("CriticUserName", userName);
                    item["CreateDate"] = DataHelper.FormatTime(item.GetValue("CreateDate"));
                }
                #endregion

                #region 生成表数据

                var productRow = productDt.NewRow();
                productRow["ID"] = id;
                productRow["Name"] = fileName;
                productRow["FileName"] = fileName;
                productRow["FileMD5"] = md5Code;
                productRow["WBSID"] = wbsID;
                productRow["ProjectInfoID"] = projectInfoID;
                productRow["SubProjectName"] = subProjectName;
                productRow["PhaseCode"] = phaseCode;
                productRow["MajorCode"] = majorCode;
                var createUserID = file.GetValue("LockUserId");
                var userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == createUserID || a.ID == createUserID);
                if (userRow != null)
                {
                    createUserID = userRow.ID;
                    productRow["CreateUserName"] = userRow.Name;
                }
                productRow["CreateUser"] = createUserID;
                productRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("ComplateTime"));

                productRow["FlowDetail"] = JsonHelper.ToJson(flowList);
                productRow["CommentDetail"] = JsonHelper.ToJson(commentList);
                productRow["SynDate"] = DateTime.Now;
                string productSql = SQLHelper.CreateUpdateSql(queueData.RelateTable, productRow);
                interfaceSb.AppendLine(productSql);
                #endregion

                #region 生成待同步文件队列

                I_FileSynQueue.CreateDownloadQueue(interfaceSb, fileName, md5Code, queueData.RelateTable, id, queueData.RelateType);

                #endregion
                
                #region 更新成果校审信息
                var AuditSignUserList = new List<Dictionary<string, string>>();
                string AuditSignUser = "", Designer = productRow["CreateUser"].ToString(), DesignerName = productRow["CreateUserName"].ToString()
                    , Collactor = "", CollactorName = "", CollactorDate = "", Auditor = "", AuditorName = "", AuditorDate = "", Approver = "", ApproverName = "", ApproverDate = "";
                foreach (var item in flowList)
                {
                    var proofSerial = item.GetValue("Serial");
                    var userId = item.GetValue("UserId");
                    var userName = item.GetValue("UserName");
                    var auditType = string.Empty;
                    if (proofSerial == OEMCollactorSerial)
                    {
                        auditType = "Collactor";
                        Collactor = userId;
                        CollactorName = userName;
                        CollactorDate = item.GetValue("OperationTime").Replace("T", "");
                    }
                    else if (proofSerial == OEMAuditorSerial)
                    {
                        auditType = "Auditor";
                        Auditor = userId;
                        AuditorName = userName;
                        AuditorDate = item.GetValue("OperationTime").Replace("T", "");
                    }
                    else if (proofSerial == OEMApproverSerial)
                    {
                        auditType = "Approver";
                        Approver = userId;
                        ApproverName = userName;
                        ApproverDate = item.GetValue("OperationTime").Replace("T", "");
                    }
                    AuditSignUserList.Add(new Dictionary<string, string>() { { "UserID", userId }, { "UserName", userName }, { "ActivityKey", auditType }, { "SignDate", item.GetValue("OperationTime") } });
                }
                AuditSignUser = JsonHelper.ToJson(AuditSignUserList);
                var AuditPassDate = productRow["CreateDate"].ToString();
                var AuditState = "Pass";
                var updateSql = @"update S_E_Product set AuditPassDate='{1}',AuditState='{2}',AuditSignUser='{3}',
                                                    Designer='{4}',DesignerName='{5}',Collactor='{6}',CollactorName='{7}',Auditor='{8}',
                                                    AuditorName='{9}',Approver='{10}',ApproverName='{11}' where SwfFile='{0}' 
                                                    update S_E_ProductVersion set AuditPassDate='{1}',AuditState='{2}',AuditSignUser='{3}',
                                                    Designer='{4}',DesignerName='{5}',Collactor='{6}',CollactorName='{7}',Auditor='{8}',
                                                    AuditorName='{9}',Approver='{10}',ApproverName='{11}' where SwfFile='{0}' ";
                updateSql = string.Format(updateSql, sourceFileId, AuditPassDate, AuditState, AuditSignUser, Designer, DesignerName, Collactor, CollactorName, Auditor, AuditorName, Approver, ApproverName);
                projectsb.AppendLine(updateSql);

                #endregion

                #region AutoCreateForm 生成校审单
                var autoCreateForm = false;
                if (ConfigurationManager.AppSettings["AutoCreateForm"] != null && ConfigurationManager.AppSettings["AutoCreateForm"].ToString().ToLower() == "true")
                    autoCreateForm = true;
                var auditId = string.Empty;
                if (autoCreateForm)
                {
                    var rootWbs = wbsDt.Select("WBSType='" + WBSNodeType.Project.ToString() + "'").FirstOrDefault();
                    var auditRow = auditDt.NewRow();
                    auditId = GuidHelper.CreateGuid();
                    auditRow["ID"] = auditId;
                    auditRow["ProjectInfoID"] = projectInfoID;
                    auditRow["WBSID"] = wbsID;
                    auditRow["ProjectInfoName"] = rootWbs["Name"].ToString();
                    auditRow["ProjectInfoCode"] = rootWbs["Code"].ToString();
                    auditRow["SubProjectName"] = subProjectName;
                    auditRow["PhaseCode"] = productRow["PhaseCode"];
                    auditRow["MajorCode"] = productRow["MajorCode"];
                    auditRow["CreateUser"] = productRow["CreateUserName"];
                    auditRow["CreateUserID"] = productRow["CreateUser"];
                    auditRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("ComplateTime"));
                    auditRow["ModifyDate"] = auditRow["CreateDate"];
                    auditRow["FlowPhase"] = "End";
                    //auditRow["SerialNumber"] = code;
                    //设置校审人员
                    auditRow["Designer"] = Designer;
                    auditRow["DesignerName"] = DesignerName;
                    auditRow["Collactor"] = Collactor;
                    auditRow["CollactorName"] = CollactorName;
                    auditRow["Auditor"] = Auditor;
                    auditRow["AuditorName"] = AuditorName;
                    auditRow["Approver"] = Approver;
                    auditRow["ApproverName"] = ApproverName;
                    //签字设置
                    auditRow["CollactorSign"] = SetSignFieldJson(Collactor, CollactorName, CollactorDate);
                    auditRow["AuditorSign"] = SetSignFieldJson(Auditor, AuditorName, AuditorDate);
                    auditRow["ApproverSign"] = SetSignFieldJson(Approver, ApproverName, ApproverDate);

                    string auditSQL = SQLHelper.CreateUpdateSql("T_EXE_Audit", auditRow);
                    projectsb.AppendLine(auditSQL);

                    var updateProductSql = @"update S_E_Product set AuditID='{1}' where SwfFile='{0}' 
                                                    update S_E_ProductVersion set AuditID='{1}' where SwfFile='{0}' ";
                    updateProductSql = string.Format(updateProductSql, sourceFileId, auditId);
                    projectsb.AppendLine(updateProductSql);

                }

                #endregion

                #region 更新S_AE_Mistake、T_EXE_Audit_AdviceDetail
                foreach (var item in commentList)
                {
                    // CommentType：0 = 普通批注，1 = 校对批注，2 = 审核批注，3 = 审定批注
                    var commentType = item.GetValue("CommentType");
                    var qualityCode = item.GetValue("QualityCode");
                    if (string.IsNullOrEmpty(qualityCode))
                        qualityCode = "OtherForNull";
                    var mistakeDate = Convert.ToDateTime(item.GetValue("CreateDate"));

                    var step = AuditType.Design.ToString();
                    switch (commentType)
                    {
                        case "1": step = AuditType.Collact.ToString(); break;
                        case "2": step = AuditType.Audit.ToString(); break;
                        case "3": step = AuditType.Approve.ToString(); break;
                    }

                    #region S_AE_Mistake
                    
                    var mistakeRow = mistakeDt.NewRow();
                    mistakeRow["ProjectInfoID"] = projectInfoID;
                    mistakeRow["ID"] = item.GetValue("Id");
                    mistakeRow["MistakeContent"] = item.GetValue("Content");
                    mistakeRow["AuditActivityType"] = step;
                    mistakeRow["MistakeLevel"] = qualityCode;
                    mistakeRow["MajorCode"] = majorCode;
                    mistakeRow["DesignerID"] = Designer;
                    mistakeRow["Designer"] = DesignerName;
                    mistakeRow["CreateUserID"] = item.GetValue("CriticUserId");
                    mistakeRow["CreateUser"] = item.GetValue("CriticUserName");
                    mistakeRow["CreateDate"] = mistakeDate;
                    mistakeRow["MistakeYear"] = mistakeDate.Year;
                    mistakeRow["MistakeMonth"] = mistakeDate.Month;
                    mistakeRow["MistakeSeason"] = (mistakeDate.Month + 2) / 3;
                    mistakeRow["Measure"] = "";
                    mistakeRow["DrawingNO"] = item.GetValue("DrawingNo");
                    string misSql = SQLHelper.CreateUpdateSql("S_AE_Mistake", mistakeRow);
                    projectsb.AppendLine(misSql);

                    #endregion

                    if (autoCreateForm)
                    {
                        #region T_EXE_Audit_AdviceDetail

                        var adviceRow = auditAdviceDt.NewRow();
                        adviceRow["ID"] = item.GetValue("Id");
                        adviceRow["T_EXE_AuditID"] = auditId;
                        adviceRow["ProductCode"] = item.GetValue("DrawingNo");
                        adviceRow["CreateDate"] = mistakeDate;
                        adviceRow["MistakeYear"] = mistakeDate.Year;
                        adviceRow["MistakeMonth"] = mistakeDate.Month;
                        adviceRow["MistakeSeason"] = (mistakeDate.Month + 2) / 3;
                        adviceRow["MsitakeContent"] = item.GetValue("Content");
                        adviceRow["MistakeType"] = qualityCode;
                        adviceRow["Step"] = step;
                        adviceRow["SubmitUser"] = item.GetValue("CriticUserID");
                        adviceRow["SubmitUserName"] = item.GetValue("CriticUserName");
                        string adviceSql = SQLHelper.CreateUpdateSql("T_EXE_Audit_AdviceDetail", adviceRow);
                        projectsb.AppendLine(adviceSql);
                        #endregion
                    }
                }
                #endregion
            }

            if (projectsb.Length > 0)
                this.SQLHelpeProject.ExecuteNonQuery(projectsb.ToString());

            var errorStr = string.Empty;
            if (errorList.Count > 0)
                errorStr = JsonHelper.ToJson(errorList);
            var getDataLogSql = @"insert into I_GetDataLog(RelateTable,RelateType,RequestUrl,SynTime,ErrorMsg) values ('{0}','{1}','{2}','{3}','{4}')";
            getDataLogSql = string.Format(getDataLogSql, queueData.RelateTable, queueData.RelateType, queueData.RequestUrl,
                synTime.ToString("yyyy-MM-dd HH:mm:ss"), errorStr.Replace("'", "''"));

            interfaceSb.AppendLine(getDataLogSql);

            #endregion
        }

        private string SetSignFieldJson(string userIds, string userNames, string dates)
        {
            var list = new List<Dictionary<string, string>>();
            for (int i = 0; i < userIds.Split(',').Length; i++)
            {
                var userid = userIds.Split(',')[i];
                var userName = userNames.Split(',')[i];
                var date = dates.Split(',')[i];
                var dic = new Dictionary<string, string>();
                dic.SetValue("TaskUserID", userid);
                dic.SetValue("TaskUserName", userName);
                dic.SetValue("ExecUserID", userid);
                dic.SetValue("ExecUserName", userName);
                dic.SetValue("SignTime", date.Replace('/', '-'));
                dic.SetValue("SignComment", string.Empty);
                list.Add(dic);
            }
            return JsonHelper.ToJson(list);
        }

        private void ExecuteExchange(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            if (queueData.SynType != SynType.GetData.ToString())
                return;
            #region 获取提资的原图，同时计划内成果更新提资计划关联子表
            StringBuilder projectsb = new StringBuilder();
            var errorList = new List<Dictionary<string, string>>();
            var responseData = JsonHelper.ToObject(responseDataStr);
            var synTime = DataHelper.FormatTime(responseData.GetValue("Stamp"));
            var files = JsonHelper.ToList(responseData.GetValue("Files"));
            var wbsDtDic = new Dictionary<string, DataTable>();
            var ids = files.Select(a => a.GetValue("Id"));
            var msIds = files.Where(a => !string.IsNullOrEmpty(a.GetValue("ExchangeId"))).Select(a => a.GetValue("ExchangeId"));
            var productDt = this.SQLHelperInterface.ExecuteDataTable(@"select* from I_FlowExchangeProduct where ID in ('" + string.Join("','", ids) + "')");
            var mDetailDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_P_MileStone_ProductDetail where S_P_MileStoneID in ('" + string.Join("','", msIds) + "')");
            var mDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_P_MileStone where ID in ('" + string.Join("','", msIds) + "')");

            //循环返回列表（原图集合）
            foreach (var file in files)
            {
                var id = file.GetValue("Id");//文件记录ID
                var fileName = file.GetValue("Name"); //原图名称
                var sourceFileId = file.GetValue("SourceFileId");//原图ID
                var mileStoneID = file.GetValue("ExchangeId");
                var md5Code = file.GetValue("Md5");
                var inMajorStr = file.GetValue("ReceiveSpecialtyCodes");//接收专业数组
                var receiveJson = file.GetValue("Receive");//接收情况json
                var flowJson = file.GetValue("Proof");//审批记录Json
                var projectInfoID = file.GetValue("ProjectId");
                string phaseCode = file.GetValue("PhaseCode"), subProjectName = file.GetValue("SubProjectName"),
                    majorCode = file.GetValue("SendSpecialtyCode"), taskID = file.GetValue("TaskID");
                var inMajorAry = new List<string>();
                if (!string.IsNullOrEmpty(inMajorStr))
                    inMajorAry = JsonHelper.ToObject<List<string>>(inMajorStr);
                var inMajorCodes = string.Join(",", inMajorAry);

                if (!string.IsNullOrEmpty(mileStoneID))
                {
                    var milstone = mDt.Select("ID='" + mileStoneID + "'").AsEnumerable().FirstOrDefault();
                    if (milstone != null)
                    {
                        inMajorCodes = milstone["OutMajorValue"].ToString();
                        inMajorAry = inMajorCodes.Split(',').ToList();
                    }
                }

                if (string.IsNullOrEmpty(projectInfoID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "ProjectId为null" } });
                    continue;
                }

                #region 获得WBSID
                var wbsDt = new DataTable();
                if (wbsDtDic.ContainsKey(projectInfoID))
                    wbsDt = wbsDtDic[projectInfoID];
                else
                {
                    wbsDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_W_WBS with(nolock) where ProjectInfoID='" + projectInfoID + "'");
                    wbsDtDic[projectInfoID] = wbsDt;
                }
                var wbsID = string.Empty;
                var wbsRow = GetWBS(wbsDt, phaseCode, subProjectName, majorCode, taskID);
                if (wbsRow != null)
                    wbsID = wbsRow["ID"].ToString();
                if (string.IsNullOrEmpty(wbsID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "WBSID为null" } });
                    continue;
                }
                #endregion

                #region 处理明细Json：替换人员ID、增加人员名称，格式化时间
                var flowList = JsonHelper.ToList(flowJson);
                foreach (var item in flowList)
                {
                    var userId = item.GetValue("UserId");
                    var userName = string.Empty;
                    var _user = GlobalData.UserList.FirstOrDefault(a => a.SynID == userId || a.ID == userId);
                    if (_user != null)
                    {
                        userId = _user.ID;
                        userName = _user.Name;
                    }
                    item.SetValue("UserId", userId);
                    item.SetValue("UserName", userName);
                    if (!string.IsNullOrEmpty(item.GetValue("OperationTime")))
                        item["OperationTime"] = DataHelper.FormatTime(item.GetValue("OperationTime"));
                }
                var receiveList = JsonHelper.ToList(receiveJson);
                foreach (var item in receiveList)
                {
                    var userId = item.GetValue("UserId");
                    var userName = string.Empty;
                    var _user = GlobalData.UserList.FirstOrDefault(a => a.SynID == userId || a.ID == userId);
                    if (_user != null)
                    {
                        userId = _user.ID;
                        userName = _user.Name;
                    }
                    item.SetValue("UserId", userId);
                    item.SetValue("UserName", userName);
                    item["OperationTime"] = DataHelper.FormatTime(item.GetValue("OperationTime"));
                }
                //把增量的接收情况，组合到同一Json大字段
                //var existRow = productDt.Select("ID='" + id + "'").FirstOrDefault();
                //if (existRow != null)
                //{
                //    var existList = JsonHelper.ToList(existRow["ReceiveDetail"].ToString());
                //    existList.AddRange(receiveList);
                //    receiveList = existList;
                //}
                #endregion

                #region 生成I_FlowExchangeProduct表数据

                var productRow = productDt.NewRow();
                productRow["ID"] = id;
                productRow["Name"] = fileName;
                productRow["FileName"] = fileName;
                productRow["FileMD5"] = md5Code;
                productRow["WBSID"] = wbsID;
                productRow["MileStoneID"] = mileStoneID;
                productRow["ProjectInfoID"] = projectInfoID;
                productRow["SubProjectName"] = subProjectName;
                productRow["PhaseCode"] = phaseCode;
                productRow["OutMajor"] = majorCode;
                productRow["InMajors"] = inMajorCodes;
                var createUserID = file.GetValue("SendUserId");
                var userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == createUserID || a.ID == createUserID);
                if (userRow != null)
                {
                    createUserID = userRow.ID;
                    productRow["CreateUserName"] = userRow.Name;
                }
                productRow["CreateUser"] = createUserID;
                productRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("ComplateTime"));

                productRow["FlowDetail"] = JsonHelper.ToJson(flowList);
                productRow["ReceiveDetail"] = JsonHelper.ToJson(receiveList);
                productRow["SynDate"] = DateTime.Now;
                string productSql = SQLHelper.CreateUpdateSql(queueData.RelateTable, productRow);
                interfaceSb.AppendLine(productSql);
                #endregion

                #region 生成待同步文件队列

                I_FileSynQueue.CreateDownloadQueue(interfaceSb, fileName, md5Code, queueData.RelateTable, id, queueData.RelateType);

                #endregion

                if (!string.IsNullOrEmpty(mileStoneID))
                {
                    #region 生成S_P_MileStone_ProductDetail表数据
                    var detailRow = mDetailDt.NewRow();
                    if(mDetailDt.Select("ID='"+id+"'").Length>0)
                        detailRow = mDetailDt.Select("ID='"+id+"'")[0];
                    else
                        mDetailDt.Rows.Add(detailRow);
                    detailRow["ID"] = id;
                    detailRow["S_P_MileStoneID"] = mileStoneID;
                    detailRow["ProjectInfoID"] = projectInfoID;
                    detailRow["Name"] = fileName;
                    detailRow["Code"] = fileName;
                    detailRow["MD5Code"] = md5Code;
                    detailRow["CreateUserID"] = productRow["CreateUser"];
                    detailRow["CreateUser"] = productRow["CreateUserName"];
                    detailRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("SendTime"));
                    detailRow["ModifyUserID"] = detailRow["CreateUserID"];
                    detailRow["ModifyUser"] = detailRow["CreateUser"];
                    detailRow["ModifyDate"] = detailRow["CreateDate"];
                    var mReceiveDetail = new List<Dictionary<string, object>>();
                    foreach (var item in receiveList)
                    {
                        //Status：0：未接收（已发送），1：已接收，2：已拒绝
                        var dic = mReceiveDetail.FirstOrDefault(a => a.GetValue("ReceiveUserName") == item.GetValue("UserName")
                            && a.GetValue("ReceiveSpecialtyName") == item.GetValue("SpecialtyCode"));
                        if (dic == null)
                        {
                            dic = new Dictionary<string, object>();
                            mReceiveDetail.Add(dic);
                        }
                        dic.SetValue("ReceiveSpecialtyName", item.GetValue("SpecialtyCode"));//接收专业
                        var _status = item.GetValue("Status");
                        dic.SetValue("Status", _status == "0" ? "未接收" : _status == "1" ? "已接收" : "已拒绝");//接收状态
                        if (_status != "0")
                        {
                            dic.SetValue("ReceiveUserName", item.GetValue("UserName"));//接收人
                            dic.SetValue("ReceiveDate", item.GetValue("OperationTime"));//接收时间
                            dic.SetValue("Comment", item.GetValue("Remark"));//备注
                        }
                    }
                    detailRow["ReceiveInfo"] = JsonHelper.ToJson(mReceiveDetail);

                    string detailSql = SQLHelper.CreateUpdateSql("S_P_MileStone_ProductDetail", detailRow);
                    projectsb.AppendLine(detailSql);
                    #endregion

                    #region 设置里程碑的状态
                    //所有专业人员都接收成果以后才算完成，每次有新的成果提资，这个提资都重置状态
                    var mileStoneState = "Finish";//Finish
                    var finishDateStr = "Null";//FactFinishDate
                    if (inMajorAry.Count == 0)
                        mileStoneState = "Create";
                    else
                    {
                        //循环所有成果判定是否全部接收
                        var productDetails = mDetailDt.Select("S_P_MileStoneID='" + mileStoneID + "'");
                        foreach (var productDetail in productDetails)
                        {
                            if (productDetail["ReceiveInfo"] == DBNull.Value || productDetail["ReceiveInfo"] == null || string.IsNullOrEmpty(productDetail["ReceiveInfo"].ToString()))
                            {
                                mileStoneState = "Create";
                                break;
                            }
                            var oReceiveDetail = JsonHelper.ToList(productDetail["ReceiveInfo"].ToString());
                            foreach (var major in inMajorAry)
                            {
                                var receives = oReceiveDetail.Where(a => a.GetValue("ReceiveSpecialtyName") == major).ToList();
                                if (receives.Count == 0 || receives.Any(a => a.GetValue("Status") != "已接收"))
                                {
                                    mileStoneState = "Create";
                                    break;
                                }
                            }
                            if (mileStoneState == "Create")
                                break;
                        }

                        if (mileStoneState == "Finish")
                        {
                            //当前肯定是最晚接收的成果
                            finishDateStr = mReceiveDetail.Where(a => !string.IsNullOrEmpty(a.GetValue("ReceiveDate"))).Max(a => Convert.ToDateTime(a.GetValue("ReceiveDate"))).ToString();
                            finishDateStr = "'" + finishDateStr + "'";
                        }
                    }
                    var mileStoneSQL = @"if exists(select 1 from s_p_milestone where ID='{0}')
                                update s_p_milestone set State = '{1}', FactFinishDate={2} where ID='{0}' ";
                    projectsb.AppendLine(string.Format(mileStoneSQL, mileStoneID, mileStoneState, finishDateStr));
                    #endregion
                }
            }

            if (projectsb.Length > 0)
                this.SQLHelpeProject.ExecuteNonQuery(projectsb.ToString());

            var errorStr = string.Empty;
            if (errorList.Count > 0)
                errorStr = JsonHelper.ToJson(errorList);
            var getDataLogSql = @"insert into I_GetDataLog(RelateTable,RelateType,RequestUrl,SynTime,ErrorMsg) values ('{0}','{1}','{2}','{3}','{4}')";
            getDataLogSql = string.Format(getDataLogSql, queueData.RelateTable, queueData.RelateType, queueData.RequestUrl,
                synTime.ToString("yyyy-MM-dd HH:mm:ss"), errorStr.Replace("'", "''"));

            interfaceSb.AppendLine(getDataLogSql);
            #endregion
        }

        private void ExecuteAuditSign(I_DataSynQueue queueData, string responseDataStr, StringBuilder interfaceSb)
        {
            if (queueData.SynType != SynType.GetData.ToString())
                return;
            #region 获取会签的原图，同时更新拆分图的会签信息
            StringBuilder projectsb = new StringBuilder();
            var errorList = new List<Dictionary<string, string>>();
            var responseData = JsonHelper.ToObject(responseDataStr);
            var synTime = DataHelper.FormatTime(responseData.GetValue("Stamp"));
            var files = JsonHelper.ToList(responseData.GetValue("Files"));
            var productDt = this.SQLHelperInterface.ExecuteDataTable(@"select * from " + queueData.RelateTable + " where 1!=1");
            var wbsDtDic = new Dictionary<string, DataTable>();
            //循环返回列表（原图集合）
            foreach (var file in files)
            {
                var id = file.GetValue("Id");//文件记录ID
                var fileName = file.GetValue("Name"); //原图名称
                var sourceFileId = file.GetValue("SourceFileId");//原图ID
                var md5Code = file.GetValue("Md5");
                var flowJson = file.GetValue("Proof");//审批记录Json
                var projectInfoID = file.GetValue("ProjectId");
                string phaseCode = file.GetValue("PhaseCode"), subProjectName = file.GetValue("SubProjectName"),
                    majorCode = file.GetValue("SendSpecialtyCode"), taskID = file.GetValue("TaskID");
                var approveMajorStr = file.GetValue("ReceiveSpecialtyCodes");
                var approveMajorCodes = string.Empty;
                if (!string.IsNullOrEmpty(approveMajorStr))
                {
                    var ary = JsonHelper.ToObject<List<string>>(approveMajorStr);
                    approveMajorCodes = string.Join(",", ary);
                }

                if (string.IsNullOrEmpty(projectInfoID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "ProjectId为null" } });
                    continue;
                }

                #region 获得WBSID
                var wbsDt = new DataTable();
                if (wbsDtDic.ContainsKey(projectInfoID))
                    wbsDt = wbsDtDic[projectInfoID];
                else
                {
                    wbsDt = this.SQLHelpeProject.ExecuteDataTable(@"select * from S_W_WBS with(nolock) where ProjectInfoID='" + projectInfoID + "'");
                    wbsDtDic[projectInfoID] = wbsDt;
                }
                var wbsID = string.Empty;
                var wbsRow = GetWBS(wbsDt, phaseCode, subProjectName, majorCode, taskID);
                if (wbsRow != null)
                    wbsID = wbsRow["ID"].ToString();
                if (string.IsNullOrEmpty(wbsID))
                {
                    errorList.Add(new Dictionary<string, string>() { { "FileName", fileName }, { "Msg", "WBSID为null" } });
                    continue;
                }
                #endregion

                #region 处理明细Json：替换人员ID、增加人员名称，格式化时间
                var flowList = JsonHelper.ToList(flowJson);
                foreach (var item in flowList)
                {
                    var userId = item.GetValue("UserId");
                    var userName = string.Empty;
                    var _user = GlobalData.UserList.FirstOrDefault(a => a.SynID == userId || a.ID == userId);
                    if (_user != null)
                    {
                        userId = _user.ID;
                        userName = _user.Name;
                    }
                    item.SetValue("UserId", userId);
                    item.SetValue("UserName", userName);
                    item["OperationTime"] = DataHelper.FormatTime(item.GetValue("OperationTime"));
                }
                #endregion

                #region 生成表数据

                var productRow = productDt.NewRow();
                productRow["ID"] = id;
                productRow["Name"] = fileName;
                productRow["FileName"] = fileName;
                productRow["FileMD5"] = md5Code;
                productRow["WBSID"] = wbsID;
                productRow["ProjectInfoID"] = projectInfoID;
                productRow["SubProjectName"] = subProjectName;
                productRow["PhaseCode"] = phaseCode;
                productRow["MajorCode"] = majorCode;
                productRow["ApproveMajors"] = approveMajorCodes;
                var createUserID = file.GetValue("UserId");
                var userRow = GlobalData.UserList.FirstOrDefault(a => a.SynID == createUserID || a.ID == createUserID);
                if (userRow != null)
                {
                    createUserID = userRow.ID;
                    productRow["CreateUserName"] = userRow.Name;
                }
                productRow["CreateUser"] = createUserID;
                productRow["CreateDate"] = DataHelper.FormatTime(file.GetValue("ComplateTime"));

                productRow["FlowDetail"] = JsonHelper.ToJson(flowList);
                productRow["SynDate"] = DateTime.Now;
                string productSql = SQLHelper.CreateUpdateSql(queueData.RelateTable, productRow);
                interfaceSb.AppendLine(productSql);
                #endregion

                #region 生成待同步文件队列

                I_FileSynQueue.CreateDownloadQueue(interfaceSb, fileName, md5Code, queueData.RelateTable, id, queueData.RelateType);

                #endregion

                #region 更新成果校审信息
                var AuditSignUserList = new List<Dictionary<string, string>>();
                string CoSignUser = "", Designer = productRow["CreateUser"].ToString(), DesignerName = productRow["CreateUserName"].ToString()
                    , Collactor = "", CollactorName = "", Auditor = "", AuditorName = "", Approver = "", ApproverName = "";
                foreach (var item in flowList)
                {
                    var proofSerial = item.GetValue("Serial");
                    var userId = item.GetValue("UserId");
                    var userName = item.GetValue("UserName");
                    var auditType = string.Empty;
                    if (proofSerial == OEMCollactorSerial)
                    {
                        auditType = "Collactor";
                        Collactor = userId;
                        CollactorName = userName;
                    }
                    else if (proofSerial == OEMAuditorSerial)
                    {
                        auditType = "Auditor";
                        Auditor = userId;
                        AuditorName = userName;
                    }
                    else if (proofSerial == OEMApproverSerial)
                    {
                        auditType = "Approver";
                        Approver = userId;
                        ApproverName = userName;
                    }
                    AuditSignUserList.Add(new Dictionary<string, string>() { { "UserID", userId }, { "UserName", userName }, 
                    { "MajorValue", item.GetValue("SpecialtyCode") }, { "SignDate", item.GetValue("OperationTime") } });
                }
                CoSignUser = JsonHelper.ToJson(AuditSignUserList);
                var CoSignState = "SignComplete";
                var updateSql = @"update S_E_Product set CoSignState='{2}',CoSignUser='{3}',
                                                    Designer='{4}',DesignerName='{5}',Collactor='{6}',CollactorName='{7}',Auditor='{8}',
                                                    AuditorName='{9}',Approver='{10}',ApproverName='{11}' where SwfFile='{0}' 
                                                    update S_E_ProductVersion set CoSignState='{2}',CoSignUser='{3}',
                                                    Designer='{4}',DesignerName='{5}',Collactor='{6}',CollactorName='{7}',Auditor='{8}',
                                                    AuditorName='{9}',Approver='{10}',ApproverName='{11}' where SwfFile='{0}' {1}";
                updateSql = string.Format(updateSql, sourceFileId, "", CoSignState, CoSignUser, Designer, DesignerName, Collactor, CollactorName, Auditor, AuditorName, Approver, ApproverName);
                projectsb.AppendLine(updateSql);

                #endregion

            }

            if (projectsb.Length > 0)
                this.SQLHelpeProject.ExecuteNonQuery(projectsb.ToString());

            var errorStr = string.Empty;
            if (errorList.Count > 0)
                errorStr = JsonHelper.ToJson(errorList);
            var getDataLogSql = @"insert into I_GetDataLog(RelateTable,RelateType,RequestUrl,SynTime,ErrorMsg) values ('{0}','{1}','{2}','{3}','{4}')";
            getDataLogSql = string.Format(getDataLogSql, queueData.RelateTable, queueData.RelateType, queueData.RequestUrl,
                synTime.ToString("yyyy-MM-dd HH:mm:ss"), errorStr.Replace("'", "''"));

            interfaceSb.AppendLine(getDataLogSql);

            #endregion
        }


    }
}
