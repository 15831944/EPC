using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Text;
using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using OfficeAuto.Logic;
using OfficeAuto.Logic.Domain;
using Base.Logic.Domain;

namespace OfficeAuto.Areas.AutoForm.Controllers
{
    public class LoanApplyController : OfficeAutoFormContorllor<T_F_LoanApply>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            if (!isNew)
            {
                var sql = string.Format(@"select SynID from {0} where ID='{1}' ", "S_EP_UserLoan", dic.GetValue("ID"));
                var db = SQLHelper.CreateSqlHelper(ConnEnum.DataInterface);
                var obj = db.ExecuteScalar(sql);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    throw new Formula.Exceptions.BusinessValidationException("数据已同步，不允许操作！");
                }
            }
        }
        protected override void OnFlowEnd(T_F_LoanApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity != null)
                entity.Push();
            this.BusinessEntities.SaveChanges();
        }
        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                var sql = string.Format(@"select SynID from {0} where ID='{1}' ", "S_FC_UserLoanAccount", Id);
                var db = SQLHelper.CreateSqlHelper(ConnEnum.DataInterface);
                var obj = db.ExecuteScalar(sql);
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    throw new Formula.Exceptions.BusinessValidationException("数据已同步，不允许操作！");
                }
            }
        }

    }
}
