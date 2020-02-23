using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

using Base.Logic.BusinessFacade;
using Formula;
using Formula.Helper;
using Config;
using Config.Logic;
using DocSystem;
using DocSystem.Logic;
using DocSystem.Logic.Domain;

namespace DocSystem.Areas.MaterialManage.Controllers
{
    public class IdentifyManageController : DocConstController<S_I_IdentifyInfo>
    {
        private string[] noNeedSynch = new string[] { "ID", "CreateDate", "ModifyDate", "CreateUserID", "CreateUser",
            "ModifyUserID", "ModifyUser","OrgID","CompanyID","FlowPhase","FlowInfo","StepName",
            "SpaceID","SpaceName","NodeID","ConfigID" ,"State"};

        public JsonResult GetCommonInfo()
        {
            var result = new Dictionary<string, string>();
            var sql = "select top 1 * from S_I_IdentifyInfo order by CreateDate desc";
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                result.Add("LastTime", Convert.ToDateTime(dt.Rows[0]["CreateDate"]).ToShortDateString());

            sql = "select top 1 * from S_I_IdentifyRule order by CreateDate desc";
            var ruleDT = this.SqlHelper.ExecuteDataTable(sql);
            if (ruleDT.Rows.Count > 0)
                result.Add("Rule", ruleDT.Rows[0]["DayRule"].ToString());

            return Json(result);
        }

        public JsonResult GatherPhysicNode()
        {
            var state = IdentifyState.Finish.ToString();
            var sql = "select * from S_I_IdentifyInfo where [State] <> '" + state + "'";
            var identityInfoDT = this.SqlHelper.ExecuteDataTable(sql);
            var toInsertDT = this.SqlHelper.ExecuteDataTable("select * from S_I_IdentifyInfo where 1=2");

            var timeRuleStr = "";
            var rule = this.entities.Set<S_I_IdentifyRule>().OrderByDescending(a => a.CreateDate).FirstOrDefault();
            if (rule == null)
                throw new Formula.Exceptions.BusinessException("未设置鉴定提醒规则");
            timeRuleStr = "where DATEDIFF(day,GETDATE(),KeepEndDate) is not null and DATEDIFF(day,GETDATE(),KeepEndDate)<=" + rule.DayRule;

            StringBuilder updateSql = new StringBuilder();
            var spaceList = FormulaHelper.GetEntities<DocConfigEntities>().Set<S_DOC_Space>().ToList();
            foreach (var space in spaceList)
            {
                var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                var nodeConfigIDs = space.S_DOC_Node.Where(a => a.IsPhysicalBox == "True").Select(a => a.ID);
                var nodeSql = @"select * from(
select case when ArchiveDate is not null and KeepYear is not null 
then DATEADD(YEAR,convert(int,KeepYear),ArchiveDate) else '9999-12-31' end as KeepEndDate,
* from S_NodeInfo where ConfigID in ('" + string.Join("','", nodeConfigIDs) + "'))a " + timeRuleStr;
                var nodeDT = instanceDB.ExecuteDataTable(nodeSql);
                var updateCols = new List<string>();
                foreach (var col in identityInfoDT.Columns)
                {
                    if (!noNeedSynch.Contains(col.ToString()) && nodeDT.Columns.Contains(col.ToString()))
                        updateCols.Add(col.ToString());
                }

                foreach (DataRow row in nodeDT.Rows)
                {
                    var identityInfo = identityInfoDT.AsEnumerable().FirstOrDefault(a =>
                        a["NodeID"].ToString() == row["ID"].ToString());
                    if (identityInfo != null)
                    {
                        var updateCol = "";
                        foreach (var item in updateCols)
                            updateCol += "," + item + "='" + row[item] + "'";
                        updateSql.AppendLine(string.Format("update S_I_IdentifyInfo set KeepYearToToday = '{1}'{2} where ID = '{0}'",
                            identityInfo["ID"].ToString(), GetKeepYearToToday(Convert.ToDateTime(row["ArchiveDate"])), updateCol));
                    }
                    else
                    {
                        var configID = row["ConfigID"].ToString();
                        var nodeConfig = space.S_DOC_Node.FirstOrDefault(a => a.ID == configID);
                        if (nodeConfig != null)
                            addNewRowToDT(toInsertDT, row, updateCols, space, nodeConfig);
                    }
                }
            }
            this.SqlHelper.BulkInsertToDB(toInsertDT, "S_I_IdentifyInfo");
            if (!string.IsNullOrEmpty(updateSql.ToString()))
                this.SqlHelper.ExecuteNonQuery(updateSql.ToString());
            return Json(new { });
        }

        private void addNewRowToDT(DataTable toInsertDT, DataRow nodeRow, List<string> updateCols,
            S_DOC_Space space, S_DOC_Node nodeConfig)
        {
            var newRow = toInsertDT.NewRow();
            newRow["ID"] = FormulaHelper.CreateGuid();
            newRow["CreateDate"] = DateTime.Now;
            newRow["ModifyDate"] = DateTime.Now;
            newRow["CreateUserID"] = this.CurrentUserInfo.UserID;
            newRow["CreateUser"] = this.CurrentUserInfo.UserName;
            newRow["ModifyUserID"] = this.CurrentUserInfo.UserID;
            newRow["ModifyUser"] = this.CurrentUserInfo.UserName;
            newRow["OrgID"] = this.CurrentUserInfo.UserOrgID;
            newRow["CompanyID"] = this.CurrentUserInfo.UserCompanyID;
            newRow["State"] = IdentifyState.Create.ToString();
            newRow["SpaceID"] = space.ID;
            newRow["SpaceName"] = space.Name;
            newRow["NodeID"] = nodeRow["ID"].ToString();
            newRow["ConfigID"] = nodeConfig.ID;
            newRow["DataType"] = nodeConfig.Name;
            newRow["KeepYearToToday"] = GetKeepYearToToday(Convert.ToDateTime(nodeRow["ArchiveDate"]));

            foreach (var item in updateCols)
            {
                newRow[item] = nodeRow[item];
            }

            toInsertDT.Rows.Add(newRow);
        }

        private string GetKeepYearToToday(DateTime date)
        {
            var today = DateTime.Now;
            int Months = (today.Year - date.Year) * 12 + (today.Month - date.Month);
            int Year = Months / 12;
            int Month = Months % 12;
            return Year + "年" + (Month == 0 ? "" : (Month + "月"));
        }

        public JsonResult SetRule(string rule, string ruleName)
        {
            var identifyRule = new S_I_IdentifyRule
            {
                ID = FormulaHelper.CreateGuid(),
                DayRule = rule,
                DayRuleName = ruleName
            };
            EntityCreateLogic<S_I_IdentifyRule>(identifyRule);
            this.entities.Set<S_I_IdentifyRule>().Add(identifyRule);
            this.entities.SaveChanges();
            return Json(new { });
        }
    }
}
