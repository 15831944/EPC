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

namespace OEMSzsowAPI.ApiLogic
{
    public class Plan : BaseApi
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
        public Plan(S_OEM_TaskList task)
            : base(task)
        { }

        public override void SaveLogic(string businessID)
        {
            var dt = this.BusinessSQLHelper.ExecuteDataTable(@"select m.*,wbs.Name WBSName,wbs.WBSType from S_P_MileStone  m
            left join S_W_WBS wbs on m.WBSID = wbs.ID 
            where m.ProjectInfoID='" + businessID + "' and m.MileStoneType = 'Major'");
            if (dt.Rows.Count <= 0)
                throw new Exception("未找到指定ID的业务数据");
            var api = this.BaseServerUrl + this.Task.BusinessCode + "/edit";
            //设置参数
            var param = new ProjectPlanRequestData();
            param.projectId = businessID;
            param.plans = new List<plan>();
            foreach (DataRow row  in dt.Rows)
            {
                var item = new plan();
                item.id = row["ID"].ToString();
                item.name = Convert.ToDateTime(row["PlanFinishDate"]).ToString("yyyy.MM.dd") +row["Name"].ToString();
                if (row["WBSType"] != null && row["WBSType"] != DBNull.Value)
                {
                    if (row["WBSType"].ToString() == WBSNodeType.SubProject.ToString())
                        item.subProjectName = row["WBSName"].ToString();
                }

                item.phaseName = GlobalData.PhaseEnum.GetValue(row["PhaseValue"].ToString());
                item.specialtyName = GlobalData.MajorEnum.GetValue(row["MajorValue"].ToString());
                param.plans.Add(item);
            }

            //请求
            this.Task.RequestUrl = api;
            this.Task.RequestData = JsonHelper.ToJson<ProjectPlanRequestData>(param);
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
        
        public override void AfterGetList(List<Dictionary<string, object>> dataList)
        {
            //base.AfterGetList(dataList);
            if (this.Task.BusinessType != BusinessType.GetData.ToString())
                return;
            var projectInfoID = this.Task.BusinessID;
            var mileStoneDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_P_MileStone where ProjectInfoID='" + projectInfoID + "'");
            var detailDt = this.BusinessSQLHelper.ExecuteDataTable(@"select * from S_P_MileStone_ProductDetail where ProjectInfoID='" + projectInfoID + "'");

            StringBuilder sb = new StringBuilder();
            StringBuilder filesb = new StringBuilder();
            var errorList = new List<Dictionary<string, string>>();
            foreach (var item in dataList)
            {
                var plan = JsonHelper.ToObject(item.GetValue("plan"));
                var files = JsonHelper.ToList(item.GetValue("files"));
                var mileStoneID = plan.GetValue("ID");
                var msRow = mileStoneDt.Select("ID='" + mileStoneID + "'").FirstOrDefault();
                if (msRow == null)
                {
                    //errorList.Add(new Dictionary<string, string>() { { "ID", mileStoneID }, { "Name", plan.GetValue("Name") }, { "Msg", "S_P_MileStone为null" } });
                    //删除
                    filesb.AppendFormat("insert into S_OEM_TaskList(OEMCode,BusinessType,BusinessCode,BusinessID,CreateTime) values( '{0}','{1}','{2}','{3}','{4}')",
                        Task.OEMCode, BusinessType.Remove.ToString(), Task.BusinessCode, mileStoneID, Task.CreateTime.ToString());
                    filesb.AppendLine();
                    continue;
                }
                foreach (var file in files)
                {
                    var detailID = file.GetValue("ID");
                    if (string.IsNullOrEmpty(detailID))
                    {
                        errorList.Add(new Dictionary<string, string>() { { "ID", mileStoneID }, { "Name", plan.GetValue("Name") }, { "Msg", "Plot.ID为null" } });
                        continue;
                    }
                    var md5Code = file.GetValue("MD5");
                    var fileName = file.GetValue("Name");
                    var userID = file.GetValue("LockUserID");
                    var userRow = GlobalData.UserDt.Select("ID='" + userID + "'").FirstOrDefault();
                    var userName = file.GetValue("LockUserName");
                    if (userRow != null)
                        userName = userRow["Name"].ToString();
                    var date = DataHelper.FormatTime(file.GetValue("CreateDate"));
                    //生成业务记录S_P_MileStone_ProductDetail
                    var detailRow = detailDt.Select("ID='" + detailID + "'").FirstOrDefault();
                    if (detailRow == null)
                    {
                        detailRow = detailDt.NewRow();
                        detailDt.Rows.Add(detailRow);
                        detailRow["ID"] = detailID;
                        detailRow["S_P_MileStoneID"] = mileStoneID;
                        detailRow["ProjectInfoID"] = projectInfoID;
                        detailRow["CreateUser"] = userName;
                        detailRow["CreateUserID"] = userID;
                        detailRow["CreateDate"] = date;
                    }
                    detailRow["ModifyUser"] = userName;
                    detailRow["ModifyUserID"] = userID;
                    detailRow["ModifyDate"] = date;
                    detailRow["Name"] = fileName;

                    if (detailRow["MD5Code"] == null || detailRow["MD5Code"] == DBNull.Value || detailRow["MD5Code"].ToString() != md5Code)
                    {
                        detailRow["MD5Code"] = md5Code;
                        #region 生成待同步文件队列

                        if (!string.IsNullOrEmpty(md5Code) && !string.IsNullOrEmpty(fileName))
                        {
                            string fileTaskSql = @" insert into S_OEM_TaskFileList(OEMCode,BusinessCode,BusinessID,CreateTime,MD5Code,FileName) 
                                        values( '{0}','{1}','{2}','{3}','{4}','{5}')";
                            fileTaskSql = string.Format(fileTaskSql, Task.OEMCode, Task.BusinessCode, detailID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), md5Code, fileName.Replace("'", "''"));
                            filesb.AppendLine(fileTaskSql);
                        }
                        #endregion
                    }
                    string detailSql = SQLHelper.CreateUpdateSql("S_P_MileStone_ProductDetail", detailRow);
                    sb.AppendLine(detailSql);


                }
            }
            if (errorList.Count > 0)
                this.Task.ErrorMsg = JsonHelper.ToJson(errorList);
            if (sb.Length > 0)
                this.BusinessSQLHelper.ExecuteNonQuery(sb.ToString());
            if (filesb.Length > 0)
                this.BaseSQLHelper.ExecuteNonQuery(filesb.ToString());
        }

        public static void test()
        {
            var prjid = "a95f00fb-11c2-4564-96bd-dec56280fbd0";// "a93d00ee-c52a-4852-8f0b-75cc23521278";
            var api = "http://www.szsow.com/api/goodway/plan/getdata?projectId=" + prjid;
            //var api = "http://61.142.69.246:8080/webapi/goodway/plan/getdata?projectId=a96100cc-b214-46d9-9c4b-582b7b52d5e9";
            var response = HttpHelper.Get(api);
            var rtn = JsonHelper.ToObject<Dictionary<string, object>>(response);
            var appendData = JsonHelper.ToList(rtn.GetValue("AppendData"));
            new Plan(new S_OEM_TaskList() { BusinessID = prjid, BusinessType = BusinessType.GetData.ToString() }).AfterGetList(appendData);
        }
    }
    public class ProjectPlanRequestData
    {
        public string projectId  { get; set; }//项目id
        public List<plan> plans { get; set; }//发图计划列表
    }
    public class plan
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phaseName { get; set; }//阶段名称
        public string subProjectName { get; set; }//子项wbsname
        public string specialtyName { get; set; }//专业名称
    }
}
