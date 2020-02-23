using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using System.Text;


namespace Expenses.Areas.Cost.Controllers
{
    public class SubContractTaskController : ExpensesFormController<S_EP_SubContractTask>
    {
    
        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "CostUnitDetail")
            {
                var taxRate = String.IsNullOrEmpty(dic.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(dic.GetValue("TaxRate"));
                var contractValue = String.IsNullOrEmpty(dic.GetValue("SubContractValue")) ? 0m : Convert.ToDecimal(dic.GetValue("SubContractValue"));
                var taxValue = contractValue / (1 + taxRate) * taxRate;
                var clearValue = contractValue - taxValue;
                detail.SetValue("TaxValue", taxValue.ToString());
                detail.SetValue("ClearValue", clearValue.ToString());
            }
        }

        protected override void OnFlowEnd(S_EP_SubContractTask data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult RemoveTask(string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var idList = list.Select(d => d.GetValue("ID")).ToList();
            var ids = String.Join(",", idList);

            var sql = String.Format(@"select S_EP_SubContractProgressConfirm.ID,S_EP_SubContractTask.Name from S_EP_SubContractProgressConfirm
left join S_EP_SubContractTask_CostUnitDetail on 
S_EP_SubContractProgressConfirm.TaskDetailID=S_EP_SubContractTask_CostUnitDetail.ID
left join S_EP_SubContractTask on S_EP_SubContractTask.ID=S_EP_SubContractTaskID
where S_EP_SubContractTask.ID in ('{0}')", ids.Replace(",", "','"));
            var dt = this.SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("任务【{0}】已经进行了进度确认，无法删除", dt.Rows[0]["Name"]));
            }

            Action action = () =>
            {
                var sqlCommand = new StringBuilder();
                foreach (var item in list)
                {
                    sqlCommand.AppendLine(String.Format("delete from S_EP_SubContractTask where ID='{0}';", item.GetValue("ID")));
                }
                this.SQLDB.ExecuteNonQuery(sqlCommand.ToString());
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult GetCostUnitInfo(string NodeID)
        {
            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode WHERE ID='{0}'", NodeID));
            if (dt.Rows.Count == 0)
            {
                return Json("");
            }
            var nodeFullID = dt.Rows[0]["FullID"].ToString();
            var sql = @"select S_EP_CostUnit.ID as UnitID,
isnull(SubContractValue,0) as SubContractValue,
SubContractCostValue,S_EP_CBSNode.* from S_EP_CostUnit
left join S_EP_CBSNode on CBSNodeID=S_EP_CBSNode.ID
left join (select Sum(SubContractValue) as SubContractValue,CostUnitID from dbo.S_EP_SubContractTask_CostUnitDetail
group by CostUnitID) SubContractTaskDetailInfo
on SubContractTaskDetailInfo.CostUnitID=S_EP_CostUnit.ID
where FullID like '{0}%' ";
            var result = this.SQLDB.ExecuteDataTable(String.Format(sql, nodeFullID));
            return Json(result);
        }

    }
}
