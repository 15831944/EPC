using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;


namespace Market.Areas.Basic.Controllers
{
    public class PlanReceiptController : MarketController<S_C_PlanReceipt>
    {
        public override ActionResult List()
        {
            var tab = new Tab();
            var planStateCategory = CategoryFactory.GetCategory(typeof(PlanReceiptState), "State");
            planStateCategory.SetDefaultItem(PlanReceiptState.UnReceipt.ToString());
            tab.Categories.Add(planStateCategory);

            var yearCategory = CategoryFactory.GetYearCategory("BelongYear", 10, 2);
            yearCategory.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(yearCategory);

            var quarterCategory = CategoryFactory.GetQuarterCategory("BelongQuarter");
            quarterCategory.SetDefaultItem(MarketHelper.GetQuarter(DateTime.Now).ToString());
            tab.Categories.Add(quarterCategory);

            var monthCategory = CategoryFactory.GetMonthCategory("BelongMonth");
            tab.Categories.Add(monthCategory);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            ViewBag.DefaultState = PlanReceiptState.UnReceipt.ToString();
            ViewBag.DefaultYear = DateTime.Now.Year.ToString();
            ViewBag.DefaultQuarter = MarketHelper.GetQuarter(DateTime.Now).ToString();
            ViewBag.DefaultMonth = DateTime.Now.Month.ToString();
            return View();
        }

        public override JsonResult GetList(QueryBuilder qb)
        {
            var projectSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            DateTime currentMonthFirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string sql = @"
select * from (
select S_C_PlanReceipt.*,ReceiptObj.FactInvoiceValue,SerialNumber,ReceiptObj.MileStonePlan,ReceiptObj.MileStoneFact,
case when PlanReceiptDate< '" + currentMonthFirstDay.ToShortDateString() + "' and S_C_PlanReceipt.State='" + PlanReceiptState.UnReceipt + @"' then 'T' else 'F' end as IsDelay 
from S_C_PlanReceipt
left join (
select S_C_ManageContract_ReceiptObj.*,S_P_MileStone.PlanFinishDate MileStonePlan,S_P_MileStone.FactFinishDate MileStoneFact
from S_C_ManageContract_ReceiptObj
left join " + projectSQLHelper.DbName + @"..S_P_MileStone
on S_C_ManageContract_ReceiptObj.MileStoneID = S_P_MileStone.ID
)ReceiptObj
on S_C_PlanReceipt.ReceiptObjectID=ReceiptObj.ID
left join S_C_ManageContract 
on S_C_ManageContract.ID=S_C_PlanReceipt.ContractInfoID
) planDataInfo ";
            //this.FillQueryBuilderFromQueryTabData(qb);
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            string sumSql = @"select Sum(PlanReceiptValue) as PlanReceiptValue,Sum(FactReceiptValue) as FactReceiptValue,
Sum(BadDebtValue) as BadDebtValue  from (" + sql + qb.GetWhereString() + ") PlanData ";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt != null && sumDt.Rows.Count > 0)
            {
                data.sumData.SetValue("PlanReceiptValue", sumDt.Rows[0]["PlanReceiptValue"]);
                data.sumData.SetValue("FactReceiptValue", sumDt.Rows[0]["FactReceiptValue"]);
                data.sumData.SetValue("BadDebtValue", sumDt.Rows[0]["BadDebtValue"]);
            }
            return Json(data);
        }

        public void SavePlanReceipt(string planReceiptData)
        {
            var planDic = JsonHelper.ToObject(planReceiptData);
            var planReceipt = this.GetEntityByID(planDic.GetValue("ID"));
            if (planReceipt == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + planDic.GetValue("ID") + "】的计划收款对象，修改计划收款失败");
            this.UpdateEntity<S_C_PlanReceipt>(planReceipt, planDic);
            planReceipt.Save();
            this.entities.SaveChanges();
        }

        public void DelayPlan(string PlanReceiptData, string NewPlanDate)
        {
            var list = JsonHelper.ToList(PlanReceiptData);
            if (String.IsNullOrEmpty(NewPlanDate.Trim('\"'))) throw new Formula.Exceptions.BusinessException("必须指定延迟日期");
            foreach (var item in list)
            {
                var plan = this.GetEntityByID(item.GetValue("ID"));
                if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的计划收款信息，无法延迟操作");
                var date = Convert.ToDateTime(NewPlanDate.Trim('\"'));
                plan.Delay(date);
            }
            this.entities.SaveChanges();
        }

        public void SplitPlanReceipt(string PlanReceiptID, string SplitData)
        {
            var planReceipt = this.GetEntityByID(PlanReceiptID);
            if (planReceipt == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + PlanReceiptID + "】的计划收款信息，无法拆分操作");
            var data = JsonHelper.ToObject(SplitData);
            var splitValue = data.GetValue("SplitValue");
            if (String.IsNullOrEmpty(splitValue)) throw new Formula.Exceptions.BusinessException("必须指定拆分金额才能对计划收款进行拆分");
            DateTime? newPlanDate = null;
            if (!String.IsNullOrEmpty(data.GetValue("NewPlanDate")))
                newPlanDate = Convert.ToDateTime(data.GetValue("NewPlanDate"));
            var newPlan = planReceipt.Split(Convert.ToDecimal(splitValue), newPlanDate);
            newPlan.Name = data.GetValue("Name");
            this.entities.SaveChanges();
        }

        public void CombinePlanReceipt(string PlanReceiptData)
        {
            var list = JsonHelper.ToList(PlanReceiptData);
            var planReceiptList = new List<S_C_PlanReceipt>();
            foreach (var item in list)
            {
                var plan = this.GetEntityByID(item.GetValue("ID"));
                if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的计划收款信息，无法拆分操作");
                planReceiptList.Add(plan);
            }
            var targetPlan = planReceiptList.OrderBy(d => d.ID).ThenBy(d => d.PlanReceiptDate).FirstOrDefault();
            targetPlan.Combine(planReceiptList);
            entities.SaveChanges();
        }

        public void ValidateOperation(string PlanReceiptData)
        {
            var list = JsonHelper.ToList(PlanReceiptData);
            foreach (var item in list)
            {
                var planReceipt = this.GetEntityByID(item.GetValue("ID"));
                if (planReceipt == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的计划收款信息，无法延迟操作");
                if (planReceipt.State != PlanReceiptState.UnReceipt.ToString())
                    throw new Formula.Exceptions.BusinessException("【" + planReceipt.Name + "】不是未到款状态，无法进行操作");
            }
        }

        public void SetBadDebt(string PlanReceiptData)
        {
            var list = JsonHelper.ToList(PlanReceiptData);
            foreach (var item in list)
            {
                var plan = this.GetEntityByID(item.GetValue("ID"));
                if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的计划收款信息");
                plan.BadDebt();
            }
            this.entities.SaveChanges();

        }

        public void RevertBadDebt(string PlanReceiptData)
        {
            var list = JsonHelper.ToList(PlanReceiptData);
            foreach (var item in list)
            {
                var plan = this.GetEntityByID(item.GetValue("ID"));
                if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的计划收款信息");
                plan.RevertBadDebt();
            }
            this.entities.SaveChanges();
        }

    }
}
