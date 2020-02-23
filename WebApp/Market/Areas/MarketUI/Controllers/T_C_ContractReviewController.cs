using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Logic.Domain;
using Workflow.Logic.Domain;
using Config.Logic;
using Config;
using System.Data;

namespace Market.Areas.MarketUI.Controllers
{
    public class ContractReviewController : MarketFormContorllor<T_C_ContractReview>
    {
        public override bool ExecTaskExec(S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing, string nextExecUserIDs,
            string nextExecUserNames, string nextExecUserIDsGroup, string nextExecRoleIDs, string nextExecOrgIDs,
            string execComment)
        {
            var rtn = base.ExecTaskExec(taskExec, routing, nextExecUserIDs, nextExecUserNames, nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);

            var dt = this.MarketSQLDB.ExecuteDataTable("SELECT * FROM T_C_ContractReview WHERE ID='" + taskExec.S_WF_InsFlow.FormInstanceID + "'");
            if (dt.Rows.Count > 0)
            {
                var dic = Formula.FormulaHelper.DataRowToDic(dt.Rows[0]);
                if (!String.IsNullOrEmpty(dic.GetValue("ContractID")))
                {
                    this.MarketSQLDB.ExecuteNonQuery("Update S_C_ManageContract set FlowPhase='" + dic.GetValue("FlowPhase") + "' where ID='" + dic.GetValue("ContractID") + "'");
                    if (rtn)
                    {
                        this.MarketSQLDB.ExecuteNonQuery(String.Format("Update S_C_ManageContract set ContractState='{0}' where ID='{1}'", "WaitSign", dic.GetValue("ContractID")));
                    }
                }
            }
            return rtn;
        }

        protected override void BeforeDelete(string[] Ids)
        {
            var sql = "";
            var dt = this.MarketSQLDB.ExecuteDataTable("SELECT * FROM T_C_ContractReview WHERE ID in('" + string.Join("','", Ids) + "')");
            foreach (DataRow row in dt.Rows)
            {
                if (row["FlowPhase"].ToString() == "End")
                    throw new Formula.Exceptions.BusinessException("已通过的合同评审不能删除！");
                sql += "update S_C_ManageContract set FlowPhase='Start' where ID='" + row["ContractID"].ToString() + "'";
            }
            if (!string.IsNullOrEmpty(sql))
                this.MarketSQLDB.ExecuteNonQuery(sql);
        }
    }
}
