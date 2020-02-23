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
    public class DrawMoneyController : ExpensesFormController<S_EP_DrawMoney>
    {


        public JsonResult Deletes()
        {
            var listData = JsonHelper.ToList(GetQueryString("ListData"));
            if (listData.Count == 0)
            {
                throw new BusinessException("请选择要操作的行！");
            }

            var idList = listData.Select(a => a.GetValue("ID"));
            string sql = string.Format("select * from S_EP_DrawMoney where ID in ('{0}') ", string.Join("','", idList));
            var dt = SQLDB.ExecuteDataTable(sql);
            var list = FormulaHelper.DataTableToListDic(dt);
            if (list.Exists(d => d.GetValue("FlowPhase") != "Start"))
            {
                throw new BusinessException("【流程中】或【已结束】的领款单不能删除！");
            }

            sql = string.Format("delete from S_EP_DrawMoney  where ID in ('{0}') ", string.Join("','", idList));
            SQLDB.ExecuteNonQuery(sql);
            return Json("");
        }

    }
}
