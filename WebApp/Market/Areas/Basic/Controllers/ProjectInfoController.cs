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
    public class ProjectInfoController : MarketController<S_I_Project>
    {
        public ActionResult Index()
        {
            string projectInfoID = this.GetQueryString("ID");
            var projectInfo = this.GetEntityByID(projectInfoID);
            if (projectInfo == null) throw new Formula.Exceptions.BusinessException("没有找到指定的项目信息");
            var sql = "select top 1 * from T_CP_TaskNotice with(nolock) where MarketProjectID='" + projectInfo.ID + "' and FlowPhase='End' order by CreateDate desc";
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.Project).ExecuteDataTable(sql);
            Dictionary<string,object> obj = new Dictionary<string,object>();
            if (dt.Rows.Count > 0)
                FormulaHelper.DataRowToDic(dt.Rows[0], obj);
            ViewBag.TaskNoticeID = obj.GetValue("ID");
            ViewBag.TaskNoticeTmplCode = obj.GetValue("TmplCode");
            return View();
        }

        protected override void BeforeDelete(List<S_I_Project> entityList)
        {
            foreach (var item in entityList)
                item.Delete();
        }

        public JsonResult GetpushTaskForMPList(string ID)
        {
            string sql = "select * from dbo.T_CP_TaskNotice where MarketID='" + ID + "'";
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.Project).ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult ValidateData(string ID)
        {
            return Json("");
        }

        public JsonResult ValidationInfo(string ID)
        {
            var project = this.GetEntityByID(ID);
            if (project == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【】的项目信息，无法进行任务单下达操作");
            project.ValidateTaskPush();
            return Json("");
        }

        public JsonResult GetInvoiceList(QueryBuilder qb, string ProjectInfoID)
        {
            string sql = @"select * from (
select Relation.*,S_C_ManageContract_ReceiptObj.ProjectInfo,S_C_ManageContract_ReceiptObj.ProjectInfoName,ContractCode,ContractName,
PayerUnit,S_C_Invoice.ID,InvoiceCode,InvoiceType,InvoiceDate from  (
select Sum(RelationValue) as InvoiceValue,ReceiptObjectID,S_C_InvoiceID
from S_C_Invoice_ReceiptObject with(nolock) group by ReceiptObjectID,S_C_InvoiceID) Relation
left join S_C_Invoice with(nolock) on Relation.S_C_InvoiceID = S_C_Invoice.ID
left join S_C_ManageContract_ReceiptObj with(nolock) on S_C_ManageContract_ReceiptObj.ID = Relation.ReceiptObjectID) tableInfo where ProjectInfo='" + ProjectInfoID + "'";
            qb.PageSize = 0;
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public JsonResult GetReceiptList(QueryBuilder qb, string ProjectInfoID)
        {
            string sql = @"
SELECT *,S_C_Receipt.Code as ReceiptCode FROM  S_C_Receipt 
inner join (select Sum(RelationValue) as ReceiptValue,S_C_Receipt_ID 
from S_C_Receipt_ProjectRelation where ProjectID ='{0}'
group by S_C_Receipt_ID) ProjectReceiptInfo
on S_C_Receipt.ID = ProjectReceiptInfo.S_C_Receipt_ID";
            qb.PageSize = 0;
            sql = string.Format(sql, ProjectInfoID);
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }

        //重写GetList方法
        public override JsonResult GetList(QueryBuilder qb)
        {
            this.FillQueryBuilderFilter(qb, true);
            var qbItem_date = qb.Items.FirstOrDefault(a=>a.Field=="Date");
            if(qbItem_date!=null)
            {
                qb.Add("CreateDate", QueryMethod.GreaterThanOrEqual, GetQueryString("Date"));
                qb.Items.Remove(qbItem_date);
            }

            string sql = @" select * from (SELECT pinfo.ID,pinfo.Name,pinfo.Code,pinfo.ProjectScale,pinfo.ProjectClass
,pinfo.ProjectType,pinfo.Industry,pinfo.Customer,pinfo.CustomerName,pinfo.ProjectLevel
,pinfo.CustomerLevel,pinfo.Phase,pinfo.ChargerDeptName,pinfo.ChargerDept,pinfo.ChargerUserName
,pinfo.ChargerUser,pinfo.MarketDeptName,pinfo.MarketDept,pinfo.BusinessChargerUserName
,pinfo.BusinessChargerUser,pinfo.OtherDeptName,pinfo.OtherDept,pinfo.Country,isnull(pinfo.City,'') City
,pinfo.BuildAddress,pinfo.State,pinfo.Remark,pinfo.Attachments,pinfo.MakertClueID
,pinfo.CreateDate,pinfo.TasKNoticeID,isnull(pinfo.Province,'') Province FROM S_I_Project pinfo with(nolock)
left join S_F_Customer c with(nolock) on pinfo.customer = c.ID) aa";
            GridData grid = this.SqlHelper.ExecuteGridData(sql, qb);

            return Json(grid);

        }
    }
}
