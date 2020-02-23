using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
namespace Expenses.Areas.Build.Controllers
{
    public class CBSInfoBuilderController : ExpensesFormController<S_EP_CBSInfoBuilder>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {

        }

        protected override void OnFlowEnd(S_EP_CBSInfoBuilder data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult ValidateBuild(string ID, string BuildBasis)
        {
            //校验一个对象不允许重复建立账套信息
            if (String.IsNullOrEmpty(ID))
            {
                throw new Formula.Exceptions.BusinessValidationException("校验业务ID为空，无法启动立项");
            }

            var sql = "select count(ID) from S_EP_CBSInfoBuilder where 1=1 ";
            switch (BuildBasis)
            {
                default:
                case "None":
                    return Json("");
                case "Contract":
                    sql += " and ContractInfo='{0}'";
                    break;
                case "Engineering":
                    sql += " and EngineeringInfoID='{0}'";
                    break;
                case "Project":
                    sql += " and ProjectInfoID='{0}'";
                    break;
                case "Clue":
                    sql += " and ClueInfo='{0}'";
                    break;               
            }
            var obj = this.SQLDB.ExecuteScalar(String.Format(sql, ID));
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("您选择的记录已经立项，不能重复立项");
            }
            return Json("");
        }
    }
}
