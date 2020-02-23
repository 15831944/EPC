using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Logic.Domain;
using Workflow.Logic.Domain;
using Config.Logic;
using Config;
using Formula.Helper;
using Base.Logic.Model.UI.Form;
using Base.Logic.Domain;
using System.Data;
using Formula;
using Formula.Exceptions;

namespace Market.Areas.MarketUI.Controllers
{
    public class GuaranteeLetterTerminateController : MarketFormContorllor<T_F_GuaranteeLetterTerminate>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var sql = string.Format(@"select * from T_F_GuaranteeLetterApply where ID='{0}' ", dic.GetValue("GuaranteeLetterID"));
            var dt = MarketSQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                throw new BusinessException("保函申请单不存在！");
            }
            if (!(dt.Rows[0]["FlowPhase"] != DBNull.Value && dt.Rows[0]["FlowPhase"].ToString() == "End"))
            {
                throw new BusinessException("只有流程【已结束】的单才能解除！");
            }

            if (isNew)
            {
                sql = string.Format(@"select * from T_F_GuaranteeLetterTerminate where GuaranteeLetterID='{0}' ", dic.GetValue("GuaranteeLetterID"));
                dt = MarketSQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    throw new BusinessException("保函已解除，无需重复解除！");
                }
            }

        }

        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var sql = string.Format(@"update T_F_GuaranteeLetterApply set Status='Terminate',Terminator='{1}',TerminatorName='{2}',TerminateDate=getdate() 
                where ID = '{0}' and FlowPhase='End' ", dic.GetValue("GuaranteeLetterID"), dic.GetValue("Terminator"), dic.GetValue("TerminatorName"));
            MarketSQLDB.ExecuteNonQuery(sql);
        }

        public JsonResult GetTerminateList(string guaranteeLetterID)
        {
            string sql = String.Format(@"select 'Terminate' as Status,* from T_F_GuaranteeLetterTerminate where GuaranteeLetterID='{0}' ", guaranteeLetterID);
            var data = this.MarketSQLDB.ExecuteDataTable(sql);
            return Json(data);
        }

    }
}
