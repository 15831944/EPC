using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;
using Config;
namespace Market.Areas.Basic.Controllers
{
    public class CommonController : MarketController
    {

        SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base");
        /// <summary>
        /// 获取省份枚举
        /// </summary>
        /// <param name="country">国家</param>
        /// <returns></returns>
        public JsonResult GetProvinceEnum(string country)
        {
            string sql = string.Format("SELECT * FROM S_M_EnumItem WHERE EnumDefID IN (SELECT ID FROM S_M_EnumDef Where Code='System.Province') AND Category='{0}' Order by SortIndex", country);
            DataTable dt = sqlHelper.ExecuteDataTable(sql);
            List<object> list = new List<object>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new { text = dt.Rows[i]["Name"], value = dt.Rows[i]["Code"] });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取城市枚举
        /// </summary>
        /// <param name="province">省份</param>
        /// <returns></returns>
        public JsonResult GetCityEnum(string province)
        {
            string sql = string.Format("SELECT * FROM S_M_EnumItem WHERE EnumDefID IN (SELECT ID FROM S_M_EnumDef Where Code='System.City') AND SubCategory='{0}'  Order by SortIndex", province);
            DataTable dt = sqlHelper.ExecuteDataTable(sql);
            List<object> list = new List<object>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new { text = dt.Rows[i]["Name"], value = dt.Rows[i]["Code"] });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 年份枚举
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYearEnum()
        {
            List<object> list = new List<object>();
            for (int i = 0; i < 8; i++)
            {
                var year = DateTime.Now.Year - i;
                list.Add(new { value = year, text = year });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 年月枚举,201401
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYearMonth()
        {
            List<YearMonth> list = new List<YearMonth>();
            int beginYear = DateTime.Now.Year - 1;
            for (int year = beginYear; year <= DateTime.Now.Year + 4; year++)
            {
                for (int month = 12; month >= 1; month--)
                {
                    var yearMonth = year.ToString() + month.ToString().PadLeft(2, '0');
                    list.Add(new YearMonth { value = yearMonth, text = yearMonth });
                }
            }
            string key = Request["key"];
            return Json(list.Where(p => p.value.Contains(key)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDeptEnum()
        {
            string sql = @"select ID as value,case when ID='{0}' then '全公司' else Name end as text from dbo.S_A_Org
where ParentID = '{0}' or ParentID is null or ParentID='' order by SortIndex";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var dt = db.ExecuteDataTable(String.Format(sql, Config.Constant.OrgRootID));
            return Json(dt);
        }

        public struct YearMonth
        {
            public string value;
            public string text;
        }

        public JsonResult ValideLinkManPhone(string id, string phone)
        {
            bool result = false;
            string msg = string.Empty;
            //客户
            //分包单位
            string sql = @"select * from (
  select id,LinkTelphone,Name,'合作单位' Type from S_SP_Supplier
  union 
  select id,LinkTelphone,Name,'客户' Type from S_F_Customer
  ) tmp where LinkTelphone='" + phone + "' and id!='" + id + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count <= 0)
                result = true;
            else
                msg += "联系电话不能重复！" + dt.Rows[0]["Type"] + "【" + dt.Rows[0]["Name"] + "】的联系人已经使用此号码";
            return Json(new { result, msg });
        }

        public JsonResult GetContractPaymentDetail(string ContractInfoID)
        {
            var sql = @"select payer.CustomerID as value,payer.CustomerIDName as text,customer.*
from S_C_ManageContract_PaymentDetail payer
left join S_F_Customer customer on payer.CustomerID = customer.ID
where S_C_ManageContractID='" + ContractInfoID + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
    }
}
