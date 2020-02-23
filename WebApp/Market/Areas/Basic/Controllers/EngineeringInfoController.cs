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
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;

namespace Market.Areas.Basic.Controllers
{
    public class EngineeringInfoController : MarketController<S_I_Engineering>
    {
        public JsonResult GetTaskNoticeList(string EngineeringInfoID)
        {
            string sql = "select * from dbo.T_CP_TaskNotice where EngineeringID='" + EngineeringInfoID + "'";
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.Project).ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult GetContractInfo(string EngineeringInfoID)
        {
            string sql = @"select case when SumReceiptValue=null or SumReceiptValue=0 then 0 
else SumReceiptValue/ContractRMBAmount*100 end ReceiptRate,* from S_C_ManageContract where EngineeringInfo='" + EngineeringInfoID + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult GetClue(string EngineeringInfoID)
        {
            var list = this.entities.Set<S_P_MarketClue>().Where(d => d.EngineeringInfo == EngineeringInfoID).ToList();
            return Json(list);
        }

        public JsonResult ValidateData(string ListIDs)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (var ID in ListIDs.Split(','))
            {
                //Expenses.Logic.CBSInfoFO.ValidateDeleteRelateData(Id);
                var entity = this.GetEntityByID(ID);
                if (entity == null) throw new Formula.Exceptions.BusinessException("指定的工程未找到，无法记性删除操作");
                if (this.entities.Set<S_I_Project>().Count(d => d.EngineeringInfo == entity.ID) > 0)
                    throw new Formula.Exceptions.BusinessException("工程【" + entity.Name + "】已经下达过任务单，不能删除");
                if (this.entities.Set<S_C_ManageContract>().Count(d => d.EngineeringInfo == entity.ID) > 0)
                {
                    var eEty = new Dictionary<string, object>();
                    eEty.SetValue("Name",entity.Name);
                    list.Add(eEty);
                }
            }
            return Json(list);
        }

        protected override void BeforeDelete(List<S_I_Engineering> entityList)
        {
            foreach (var item in entityList)
            {
                var sql = string.Format(@"select ID from S_I_Project where EngineeringInfo='{0}' ", item.ID);
                var dt = this.SqlHelper.ExecuteDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_I_Project", dr["ID"].ToString());
                }

                item.Delete(false);
            }
        }
    }
}
