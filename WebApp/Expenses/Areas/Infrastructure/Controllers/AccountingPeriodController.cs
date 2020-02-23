using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using System.Text;

namespace Expenses.Areas.Infrastructure.Controllers
{
    public class AccountingPeriodController : InstrasController
    {
        public JsonResult GetTreeList()
        {
            string sql = @"select distinct Convert(nvarchar,[Year]) as Name,'Root' as ParentID,'Child' as ID from (
select distinct [Year] from dbo.S_EP_DefineMonthPeriod
union all select distinct [Year] from dbo.S_EP_DefineMonthPeriod) tableInfo";
            var dt = this.SQLDB.ExecuteDataTable(sql);
            var root = dt.NewRow();
            root["ID"] = "Root";
            root["Name"] = "会计期间";
            root["ParentID"] = "";
            dt.Rows.Add(root);
            return Json(dt);
        }

        public JsonResult GetMonthList(string Year)
        {
            var sql = String.Format("select * from S_EP_DefineMonthPeriod where Year='{0}'", Year);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult GetQuarterList(string Year)
        {
            var sql = String.Format("select * from S_EP_DefineQuarterPeriod where Year='{0}'", Year);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult AddPeriod(string Year)
        {
            if (String.IsNullOrEmpty(Year))
                throw new Formula.Exceptions.BusinessValidationException("新建会计期间必须指定年份");
            if (!System.Text.RegularExpressions.Regex.IsMatch(Year, "^[1-9]\\d*$"))
                throw new Formula.Exceptions.BusinessValidationException("年份必须是整形数字");
            string sql = String.Format(@"select Count(*) from (
select distinct [Year] from dbo.S_EP_DefineMonthPeriod
union all select distinct [Year] from dbo.S_EP_DefineMonthPeriod)
tableInfo where Year={0}", Year);
            var obj = Convert.ToInt32(this.SQLDB.ExecuteScalar(sql));
            if (obj > 0)
                throw new Formula.Exceptions.BusinessValidationException(String.Format("已经存在【{0}】年的会计期间，不能重复添加", Year));
            for (int i = 1; i <= 12; i++)
            {
                var startDate = new DateTime(Convert.ToInt32(Year), i, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var dic = new Dictionary<string, object>();
                dic.SetValue("Year", Year);
                dic.SetValue("Month", i);
                dic.SetValue("StartDate", startDate);
                dic.SetValue("EndDate", endDate);
                dic.InsertDB(this.SQLDB, "S_EP_DefineMonthPeriod");
            }

            for (int i = 1; i <= 4; i++)
            {
                var month = (i - 1) * 3 + 1;
                var startDate = new DateTime(Convert.ToInt32(Year), month, 1);
                var endDate = startDate.AddMonths(3).AddDays(-1);
                var dic = new Dictionary<string, object>();
                dic.SetValue("Year", Year);
                dic.SetValue("Quarter", i);
                dic.SetValue("StartDate", startDate);
                dic.SetValue("EndDate", endDate);
                dic.InsertDB(this.SQLDB, "S_EP_DefineQuarterPeriod");
            }
            return Json("");
        }

        public JsonResult RemovePeriod(string Year)
        {
            if (String.IsNullOrEmpty(Year))
                throw new Formula.Exceptions.BusinessValidationException("删除会计期间必须指定年份");
            this.SQLDB.ExecuteNonQuery(String.Format("delete from S_EP_DefineQuarterPeriod where Year={0}", Year));
            this.SQLDB.ExecuteNonQuery(String.Format("delete from S_EP_DefineMonthPeriod where Year={0}", Year));
            return Json("");
        }

        public JsonResult ChangePeriod(string Year, string Day)
        {
            if (String.IsNullOrEmpty(Year))
                throw new Formula.Exceptions.BusinessValidationException("必须指定年份");
            if (!System.Text.RegularExpressions.Regex.IsMatch(Year, "^[1-9]\\d*$"))
                throw new Formula.Exceptions.BusinessValidationException("年份必须是整形数字");
            if (String.IsNullOrEmpty(Day))
                throw new Formula.Exceptions.BusinessValidationException("必须指定开始日期");

            var date = Convert.ToDateTime(Day);
            var sqlCommand = new StringBuilder();
            var monthDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from dbo.S_EP_DefineMonthPeriod where Year={0}", Year));
            for (int i = 0; i < monthDt.Rows.Count; i++)
            {
                var startDate = date.AddMonths(i);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                sqlCommand.AppendLine(String.Format("update S_EP_DefineMonthPeriod set StartDate='{0}',EndDate='{1}' where ID='{2}'", startDate, endDate,
                    monthDt.Rows[i]["ID"]));
            }


            var quarterDt = this.SQLDB.ExecuteDataTable(String.Format(@"select * from dbo.S_EP_DefineQuarterPeriod where Year={0}", Year));
            for (int i = 0; i < quarterDt.Rows.Count; i++)
            {
                var startDate = date.AddMonths(i * 3);
                var endDate = startDate.AddMonths(3).AddDays(-1);
                sqlCommand.AppendLine(String.Format("update S_EP_DefineQuarterPeriod set StartDate='{0}',EndDate='{1}' where ID='{2}'", startDate, endDate,
                  quarterDt.Rows[i]["ID"]));
            }

            this.ExecuteAction(() =>
            {
                this.SQLDB.ExecuteNonQuery(sqlCommand.ToString());
            });
            return Json("");
        }
    }
}
