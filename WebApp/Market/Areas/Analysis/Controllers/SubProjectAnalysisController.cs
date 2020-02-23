using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Config;
using Config.Logic;
using Market.Logic;
using Market.Logic.Domain;
using System.Collections;

namespace Market.Areas.Analysis.Controllers
{
    public class SubProjectAnalysisController : MarketController
    {
        public ActionResult ProjectAnalysisList()
        {
            var tab = new Tab();
            //var levelCategory = CategoryFactory.GetCategory("Market.CustomerLevel", "项目等级", "CustomerLevel");
            //levelCategory.SetDefaultItem();
            //tab.Categories.Add(levelCategory);

            var deptCategory = CategoryFactory.GetCategory("Market.ManDept", "生产部门", "ChargerDept");
            deptCategory.SetDefaultItem();
            tab.Categories.Add(deptCategory);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"  SELECT * FROM   (SELECT S_I_Project.*,S_SP_SupplierContract.*,'0'AS DirectCost,'0' AS DirectCostProbability,
               Isnull(ProjectContractValue, 0) AS ProjectContractValue,
               Isnull(ProjectReceiptValue, 0)  AS ProjectReceiptValue,
               Isnull(ProjectInvoiceValue, 0) AS ProjectInvoiceValue,
               Isnull(ProjectBadDebtValue, 0)AS ProjectBadDebtValue,
               CASE
                 WHEN Isnull(ProjectContractValue, 0) = 0 THEN 0
                 ELSE Round(Isnull(ProjectReceiptValue, 0) / Isnull(ProjectContractValue, 0) * 100, 2)
               END                                                                                               AS ReceiptRate,
               Isnull(ProjectContractValue, 0) - Isnull(ProjectReceiptValue, 0) - Isnull(ProjectBadDebtValue, 0) AS RemainValue,
               Isnull(ProjectInvoiceValue, 0) - Isnull(ProjectReceiptValue, 0) - Isnull(ProjectBadDebtValue, 0)  AS ReceivableValue
        FROM   S_I_Project
               LEFT JOIN (SELECT Sum(ReceiptValue)     AS ProjectContractValue,
                                 Sum(FactReceiptValue) AS ProjectReceiptValue,
                                 Sum(FactInvoiceValue) AS ProjectInvoiceValue,
                                 Sum(BadDebtValue)     AS ProjectBadDebtValue,
                                 ProjectInfoID
                          FROM   S_C_ReceiptObject
                          WHERE  ProjectInfoID IS NOT NULL
                          GROUP  BY ProjectInfoID) ProjectValueInfo
                      ON S_I_Project.ID = ProjectValueInfo.ProjectInfoID
                      left join (SELECT sum(ContractValue)as FBContractValue,sum(SumPaymentValue) 
as FBSumPaymentValue,Projectinfo FROM S_SP_SupplierContract where state='sign'
group by Projectinfo) S_SP_SupplierContract on S_I_Project.ID=S_SP_SupplierContract.Projectinfo) TableInfo
 ";
            var data = this.SqlHelper.ExecuteGridData(sql, qb);

            string sumSql = @"select isnull(Sum(ProjectContractValue),0) as ProjectContractValue,
 isnull(Sum(ProjectInvoiceValue),0) as ProjectInvoiceValue,
 isnull(Sum(ProjectReceiptValue),0) as ProjectReceiptValue,
 isnull(Sum(ProjectBadDebtValue),0) as ProjectBadDebtValue,
 isnull(Sum(RemainValue),0) as RemainValue,
 isnull(Sum(ReceivableValue),0) as ReceivableValue          
            from (" + sql + qb.GetWhereString() + ") sumTableInfo ";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt.Rows.Count > 0)
            {
                var sumRow = sumDt.Rows[0];
                data.sumData.SetValue("ProjectContractValue", Convert.ToDecimal(sumRow["ProjectContractValue"]).ToString("c"));
                data.sumData.SetValue("ProjectInvoiceValue", Convert.ToDecimal(sumRow["ProjectInvoiceValue"]).ToString("c"));
                data.sumData.SetValue("ProjectReceiptValue", Convert.ToDecimal(sumRow["ProjectReceiptValue"]).ToString("c"));
                data.sumData.SetValue("ProjectBadDebtValue", Convert.ToDecimal(sumRow["ProjectBadDebtValue"]).ToString("c"));
                data.sumData.SetValue("RemainValue", Convert.ToDecimal(sumRow["RemainValue"]).ToString("c"));
                data.sumData.SetValue("ReceivableValue", Convert.ToDecimal(sumRow["ReceivableValue"]).ToString("c"));
            }
            return Json(data);
        }
    }
}
