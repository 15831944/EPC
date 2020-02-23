using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using Market.Logic;
using Market.Logic.Domain;
using Formula;
using Formula.Exceptions;
using Base.Logic.Domain;

namespace Market.Areas.MarketUI.Controllers
{
    public class PerformanceBondController : MarketFormContorllor<S_B_PerformanceBond>
    {

        public override JsonResult Delete()
        {
            var listIDs = Request["ListIDs"];
            if (String.IsNullOrEmpty(listIDs))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择要删除的数据！");
            }
            var ids = listIDs.Split(',');
            var sql = string.Format(@"select * from S_B_PerformanceBond where ID in ('{0}')", string.Join("','", ids));
            var dt = MarketSQLDB.ExecuteDataTable(sql);
            var list = FormulaHelper.DataTableToListDic(dt);
            var flag = list.Exists(a => a.GetValue("FlowPhase") == "Processing" || a.GetValue("FlowPhase") == "End");
            if (flag)
            {
                throw new BusinessException("【流程中】或【已结束】的履约保证金不能删除，请确认！");
            }
            sql = string.Format(@"delete from S_B_PerformanceBond where ID in ('{0}')", string.Join("','", ids));
            MarketSQLDB.ExecuteNonQuery(sql);
            return Json("");
        }

        public JsonResult SendBack()
        {
            var sendBackDate = DateTime.Now;
            if (!DateTime.TryParse(GetQueryString("SendBackDate"), out sendBackDate))
            {
                throw new Formula.Exceptions.BusinessValidationException("请选择退回日期！");
            }

            var listData = JsonHelper.ToList(GetQueryString("ListData"));
            if (listData.Count == 0)
            {
                throw new BusinessException("请选择要操作的行！");
            }

            var idList = listData.Select(a => a.GetValue("ID"));
            string sql = string.Format("select * from S_B_PerformanceBond where ID in ('{0}') ", string.Join("','", idList));
            var dt = MarketSQLDB.ExecuteDataTable(sql);
            var list = FormulaHelper.DataTableToListDic(dt);
            if (list.Exists(d => d.GetValue("FlowPhase") != "End"))
            {
                throw new BusinessException("只有流程【已结束】的单才能退回！");
            }
            if (list.Exists(d => d.GetValue("IsSendBack") != "0"))
            {
                throw new BusinessException("只有【未退回】的单才能退回！");
            }

            sql = string.Format(@"update S_B_PerformanceBond set IsSendBack='1',SendBackUser='{1}',SendBackUserName='{2}',SendBackDate='{3}' 
                where ID in ('{0}') and IsSendBack='0' ", string.Join("','", idList), CurrentUserInfo.UserID, CurrentUserInfo.UserName, sendBackDate.ToString("yyyy-MM-dd"));
            MarketSQLDB.ExecuteNonQuery(sql);
            return Json("");
        }


    }
}
