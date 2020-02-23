using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdapter;
using DocSystem.Logic.Domain;
using Formula.DynConditionObject;
using Formula;
using Formula.Helper;
using Config.Logic;
using Config;

namespace DocSystem.Areas.ApplyInfo.Controllers
{
    public class MyApplyController : DocConstController<T_Borrow>
    {
        //
        // GET: /ApplyInfo/MyApply/

        public override JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"select * from (
select ID,ApplyDate,Name,Type,'Borrow' as ApplyType,Purpose,CreateUserID,CreateUserName,CreateDate,PassDate,FlowID,BorrowState as State from dbo.T_Borrow
union select ID,ApplyDate,Name,Type,'DownLoad' as ApplyType,Purpose,CreateUserID,CreateUserName,CreateDate,PassDate,FlowID,DownloadState as State 
from dbo.T_Download)A where 1=1 and CreateUserID='" + FormulaHelper.GetUserInfo().UserID + "'";

            var dt = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteDataTable(sql, qb);
            return Json(dt);
        }
        public JsonResult getCarlist()
        {
            string type = this.Request["Type"];
            string applyID = this.Request["ApplyID"];
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(applyID))
            {
                string sql = string.Empty;
                if (type.ToLower() == "borrow")
                    sql = " select * from T_Borrow_FileInfo where T_BorrowID='" + applyID + "'";
                else if (type.ToLower() == "download")
                    sql = " select * from T_Download_FileInfo where T_DownloadID='" + applyID + "'";
                var dt = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteDataTable(sql);
                return Json(dt);
            }
            else
            {
                return Json("");
            }
        }
    }
}
