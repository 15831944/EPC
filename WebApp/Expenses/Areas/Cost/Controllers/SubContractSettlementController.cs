using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using MvcAdapter;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using Formula.Exceptions;
using Base.Logic.Domain;
using Workflow.Logic.Domain;

namespace Expenses.Areas.Cost.Controllers
{
    public class SubContractSettlementController : ExpensesFormController<S_EP_SubContractSettlement>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dic.GetValue("SettlementDate")));
        }
        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                var diffValue = Convert.ToDecimal(detail.GetValue("DiffValue"));
                var sql = string.Format(@"select isnull(sum(CostChangeValue),0) as CostChangeValueTotal 
from (select * from S_EP_SubContractSettlement_Detail where SubContract='{0}' ) Detail
left join S_EP_SubContractSettlement Settlement on Detail.S_EP_SubContractSettlementID=Settlement.ID
where Settlement.FlowPhase='End' ", detail.GetValue("SubContract"));
                var costChangeTotal = Convert.ToDecimal(SQLDB.ExecuteScalar(sql));
                if (costChangeTotal == diffValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("合同【{0}】开票和成本无差异，无需结算！", dic.GetValue("SubContractName")));
                }
                var costChangeValue = diffValue - costChangeTotal;
                detail.SetValue("CostChangeValue", Math.Round(costChangeValue, 2).ToString());

            }

        }
        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "Detail")
            {
                var sql = string.Format(@"select * from (select * from S_EP_SubContractSettlement where FlowPhase<>'End' and ID<>'{0}' ) Settlement 
inner join S_EP_SubContractSettlement_Detail Detail on Settlement.ID= Detail.S_EP_SubContractSettlementID ", dic.GetValue("ID"));
                var dtUnEnd = SQLDB.ExecuteDataTable(sql);
                if (dtUnEnd.Rows.Count > 0)
                {
                    var dicUnEnd = FormulaHelper.DataTableToListDic(dtUnEnd);
                    var subContract = string.Empty;
                    foreach (var item in detailList)
                    {
                        if (dicUnEnd.Exists(c => c.GetValue("SubContract") == item.GetValue("SubContract")))
                        {
                            throw new Formula.Exceptions.BusinessValidationException(string.Format("合同【{0}】在结算流程中，无法结算！", item.GetValue("SubContractName")));
                        }

                    }

                }


            }

        }

        protected override void OnFlowEnd(S_EP_SubContractSettlement data, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }


        public JsonResult SettlementRevert(string id)
        {
            var sql = string.Format(@"select top 1 * from S_EP_SubContractSettlement order by CreateDate desc ", "");
            var dt = SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count ==0)
            {
                throw new Formula.Exceptions.BusinessValidationException("无结算单，请确认！");
            }
            var dicLast = FormulaHelper.DataRowToDic(dt.Rows[0]);
            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dicLast.GetValue("SettlementDate")));

            if (dicLast.GetValue("FlowPhase") == "Start")
            {
                sql = string.Format(@"delete from S_EP_SubContractSettlement where ID='{0}' ", id);
                SQLDB.ExecuteNonQuery(sql);
                return Json("");
            }
            if (dicLast.GetValue("FlowPhase") == "Processing")
            {
                throw new Formula.Exceptions.BusinessValidationException("此结算单在【流程中】，无法撤销！");
            }



            sql = string.Format(@"select * from S_EP_SubContractSettlement_Detail where S_EP_SubContractSettlementID='{0}' ", id);
            dt = SQLDB.ExecuteDataTable(sql);
            var settlementDetail = FormulaHelper.DataTableToListDic(dt);
            
            var cbsInfoIDs = new List<string>();
            var revertID = FormulaHelper.CreateGuid();
            dicLast.SetValue("RevertReason", string.Empty);
            var sqlUpdate = dicLast.CreateInsertSql(SQLDB, "S_EP_SubContractSettlementRevert", revertID);
            sqlUpdate += string.Format(@"delete from S_EP_SubContractSettlement where ID='{0}' ", id);
            foreach (var item in settlementDetail)
            {
                cbsInfoIDs.Add(item.GetValue("CBSInfoID"));
                item.SetValue("S_EP_SubContractSettlementRevertID", revertID);
                sqlUpdate += item.CreateInsertSql(SQLDB, "S_EP_SubContractSettlementRevert_Detail", FormulaHelper.CreateGuid());
                sqlUpdate += string.Format(@"delete from S_EP_CostInfo where RelateID='{0}' ", item.GetValue("ID"));

            }
            SQLDB.ExecuteNonQuery(sqlUpdate);

            foreach (var cbsInfoID in cbsInfoIDs.Distinct())
            {
                var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", cbsInfoID);
                if (cbsInfoDic == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法撤销！");
                }
                var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SummaryCostValue();
            }


            return Json("");
        }

    }
}
