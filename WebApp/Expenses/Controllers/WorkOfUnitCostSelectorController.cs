using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Base.Logic.Domain;
using Config;
using Config.Logic;
using MvcAdapter;
using Formula.Helper;
using Formula;
using Expenses.Logic;

namespace Expenses.Controllers
{
    public class WorkOfUnitCostSelectorController : ExpensesFormController
    {
        public ActionResult WorkSelector()
        {
            var dataTable = EnumBaseHelper.GetEnumTable(typeof(Expenses.Logic.CBSDefineType));
            ViewBag.DefineCBSDefineType = "";
            var dicList = FormulaHelper.DataTableToListDic(dataTable);
            var resList = dicList.Where(a => !new string[] { CBSDefineType.Company.ToString(), CBSDefineType.Org.ToString() }.Contains(a.GetValue("value")));
            if (resList.Count() > 0)
            {
                ViewBag.DefaultCBSDefineType = resList.FirstOrDefault().GetValue("value");
                ViewBag.CBSDefineType = JsonHelper.ToJson(resList);
            }

            return View();
        }

        public JsonResult GetCostUnitList(QueryBuilder qb)
        {
            string WorkHourType = GetQueryString("CBSDefineType");
            var sql = @"select S_EP_DefineCBSInfo.Type as WorkHourType,
                        S_EP_CostUnit.ID as ProjectID,
                        S_EP_CostUnit.Name as ProjectName,
                        S_EP_CostUnit.Code as ProjectCode,
                        S_EP_CBSNode.ChargerUser as ProjectChargerUser,
                        S_EP_CBSNode.ChargerUserName as ProjectChargerUserName,
                        S_EP_CBSNode.ChargerDept as ProjectDept,
                        S_EP_CBSNode.ChargerDeptName as ProjectDeptName,
                        S_EP_CBSInfo.CreateDate
                 from S_EP_CostUnit 
                 left join S_EP_CBSNode on S_EP_CBSNode.ID = S_EP_CostUnit.CBSNodeID
                 left join {1}.dbo.S_EP_DefineCBSNode on S_EP_DefineCBSNode.ID = S_EP_CBSNode.DefineID
                 left join {1}.dbo.S_EP_DefineCBSInfo on S_EP_DefineCBSInfo.ID = S_EP_DefineCBSNode.DefineID
                 left join S_EP_CBSInfo on S_EP_CBSInfo.ID = S_EP_CostUnit.CBSInfoID
                 where S_EP_DefineCBSInfo.Type = '{0}'";
            var data = this.MarketSQLDB.ExecuteGridData(string.Format(sql, WorkHourType,
                SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig).DbName), qb);
            return Json(data);
        }

        public JsonResult GetUserDefaultList(QueryBuilder qb)
        {
            string sql = @"select top 10 * from (
select distinct ProjectID,ProjectName,ProjectCode,ProjectDept,ProjectDeptName,
ProjectChargerUser,ProjectChargerUserName,SubProjectName,SubProjectCode
,MajorName,MajorCode,TaskWorkCode,TaskWorkName,WorkHourType,Max(WorkHourDate) as MaxWorkHourDate
from S_W_UserWorkHour where UserID='" + this.CurrentUserInfo.UserID + @"'
group by  ProjectID,ProjectName,ProjectCode,ProjectChargerUserName,SubProjectName,SubProjectCode
,MajorName,MajorCode,TaskWorkCode,TaskWorkName,WorkHourType,
ProjectDept,ProjectDeptName,
ProjectChargerUser) tableInfo";
            qb.SortField = "MaxWorkHourDate";
            qb.PageSize = 0;
            var hrDB = SQLHelper.CreateSqlHelper(ConnEnum.HR);
            var data = hrDB.ExecuteGridData(sql, qb);
            return Json(data);
        }
    }
}
