using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base.Logic;
using Config;
using Formula;
using MvcAdapter;

namespace Project.Areas.ProjectGroup.Controllers
{
    public class AuditProductAnalyzeController : ProjectController
    {
        //
        // GET: /ProjectGroup/AuditProductAnalyze/

        //public ActionResult Index()
        //{
        //    var a = this.CurrentUserInfo.UserID;
        //    return View();
        //}
        public ActionResult UserProductList()
        {
            var tab = new Formula.Tab();
            //var category = CategoryFactory.GetCategory("System.ManDept", "部门", "DeptName", true);
            //category.Multi = true;
            //tab.Categories.Add(category);

            var category = CategoryFactory.GetYearCategory("Year");
            category.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(category);

            category = CategoryFactory.GetMonthCategory("Month");
            category.SetDefaultItem(DateTime.Now.Month.ToString());
            tab.Categories.Add(category);

            category = CategoryFactory.GetQuarterCategory("Quarter");
            category.SetDefaultItem((Math.Ceiling(Convert.ToDouble(DateTime.Now.Month)/3)).ToString());
            tab.Categories.Add(category);

            category = CategoryFactory.GetCategory("Project.ProductFileType", "成果类型", "FileType", true);
            category.SetDefaultItem("图纸");
            category.Multi = false;
            tab.Categories.Add(category);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
        //个人成果展示
        public JsonResult GetUserProductList(QueryBuilder qb)
        {
            var sql = @"
select ID, Code,Name,DeptName,A0,A1,A2,A3,A4,Other,ToA1 from EPM_Base..S_A_User
right join (
select  Designer,sum(case when charindex('A0',FileSize,1)>0 then 1 else 0 end) A0,
	sum(case when charindex('A1',FileSize,1)>0 then 1 else 0 end) A1,
	sum(case when charindex('A2',FileSize,1)>0 then 1 else 0 end) A2,
	sum(case when charindex('A3',FileSize,1)>0 then 1 else 0 end) A3,
	sum(case when charindex('A4',FileSize,1)>0 then 1 else 0 end) A4,
	sum(case when FileSize = null or FileSize='' then 1 else 0 end) Other,
	SUM(isnull(ToA1,0)) ToA1
    from (
	select ID,name,code,WBSID,ProjectInfoID,
		Designer,DesignerName,AuditState,AuditPassDate,FileType,ToA1,FileSize from S_E_Product
		where AuditState='Pass' {0}
		) product  group by Designer
		) projects on projects.designer=ID";

            //var misQb = new QueryBuilder();
            var subWhereStr = "";
            //var deptStr = "";
            foreach (var condition in qb.Items.Where(a => a.Field == "Year" || a.Field == "Month" || a.Field == "Quarter" || a.Field == "FileType"))
            {
                if (condition.Field == "FileType")
                    subWhereStr += "and " + condition.Field + " in ('" + condition.Value + "')";
                //else if (condition.Field == "DeptName")
                //{
                //    string[] str = condition.Value.ToString().Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //    string conditionValue = string.Join("','", str);
                //    deptStr = "where DeptID in ('" + conditionValue + "')";
                //}
                else
                    subWhereStr += "and datepart(" + condition.Field + ",AuditPassDate) in (" + condition.Value + ")";
            }
            qb.Items.RemoveWhere(a => a.Field == "FileType");
            qb.Items.RemoveWhere(a => a.Field == "Year");
            qb.Items.RemoveWhere(a => a.Field == "Month");
            qb.Items.RemoveWhere(a => a.Field == "Quarter");
            //string whereStr = misQb.GetWhereString(false);

            var baseDB = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            //sql = String.Format(sql,subWhereStr,deptStr);
            sql = String.Format(sql, subWhereStr);
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }
        //部门成果统计
        public ActionResult DeptProductList()
        {
            var tab = new Formula.Tab();

            var category = CategoryFactory.GetYearCategory("Year");
            category.SetDefaultItem(DateTime.Now.Year.ToString());
            tab.Categories.Add(category);

            category = CategoryFactory.GetMonthCategory("Month");
            category.SetDefaultItem(DateTime.Now.Month.ToString());
            tab.Categories.Add(category);

            category = CategoryFactory.GetQuarterCategory("Quarter");
            category.SetDefaultItem((Math.Ceiling(Convert.ToDouble(DateTime.Now.Month) / 3)).ToString());
            tab.Categories.Add(category);

            category = CategoryFactory.GetCategory("Project.ProductFileType", "成果类型", "FileType", true);
            category.SetDefaultItem("图纸");
            category.Multi = false;
            tab.Categories.Add(category);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
        public JsonResult GetDeptProductList(QueryBuilder qb)
        {
            var sql = @"
select deptInfo.DeptID ID,DeptName,sum(case when charindex('A0',FileSize,1)>0 then 1 else 0 end) A0,
	sum(case when charindex('A1',FileSize,1)>0 then 1 else 0 end) A1,
	sum(case when charindex('A2',FileSize,1)>0 then 1 else 0 end) A2,
	sum(case when charindex('A3',FileSize,1)>0 then 1 else 0 end) A3,
	sum(case when charindex('A4',FileSize,1)>0 then 1 else 0 end) A4,
	sum(case when FileSize = null or FileSize='' then 1 else 0 end) Other,
	SUM(isnull(ToA1,0)) ToA1
    from (
		select suser.DeptID, DeptName,Designer,DesignerName,AuditState,AuditPassDate,FileType,ToA1,FileSize from EPM_Base..S_A_User suser
		right join
		(select ID,name,code,WBSID,ProjectInfoID,
		Designer,DesignerName,AuditState,AuditPassDate,FileType,ToA1,FileSize from S_E_Product
		where AuditState='Pass' {0}) Product on Product.Designer=suser.ID
) deptInfo group by deptInfo.DeptID,DeptName";
            var subWhereStr = "";
            foreach (var condition in qb.Items.Where(a => a.Field == "Year" || a.Field == "Month" || a.Field == "Quarter" || a.Field == "FileType"))
            {
                if (condition.Field == "FileType")
                    subWhereStr += "and " + condition.Field + " in ('" + condition.Value + "')";
             
                else
                    subWhereStr += "and datepart(" + condition.Field + ",AuditPassDate) in (" + condition.Value + ")";
            }
            
            qb.Items.RemoveWhere(a => a.Field == "FileType");
            qb.Items.RemoveWhere(a => a.Field == "Year");
            qb.Items.RemoveWhere(a => a.Field == "Month");
            qb.Items.RemoveWhere(a => a.Field == "Quarter");
            ViewBag.selectStr = subWhereStr;
            var baseDB = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            sql = String.Format(sql, subWhereStr);
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }

       

    }
}
