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
using Config;
using Newtonsoft.Json;
using Formula.ImportExport;
using Base.Logic.Domain;

namespace Expenses.Areas.Cost.Controllers
{
    public class WorkHourUserLogController : ExpensesFormController<S_EP_WorkHourUserLog>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var workHour = 0m;
            if (!decimal.TryParse(dic.GetValue("WorkHour"), out workHour))
            {
                throw new Formula.Exceptions.BusinessValidationException("请填写工时！");
            }

        }
        protected override void AfterSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            var sql = string.Format(@"update S_EP_WorkHourUser set WorkHour={1} where ID='{0}' ",
                dic.GetValue("WorkHourUserID"), Convert.ToDecimal(dic.GetValue("WorkHour")));
            SQLDB.ExecuteNonQuery(sql);
        }

    }
}
