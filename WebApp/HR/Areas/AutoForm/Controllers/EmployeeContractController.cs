using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HR.Logic.Domain;
using Config.Logic;

namespace HR.Areas.AutoForm.Controllers
{
    public class EmployeeContractController : HRFormContorllor<T_EmployeeAcademicDegree>
    {
        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            string employeeID = dic.GetValue("EmployeeID");
            var employee = BusinessEntities.Set<T_Employee>().Find(employeeID);
            if (employee != null)
            {
                var lastContract = BusinessEntities.Set<T_EmployeeContract>().Where(c => c.EmployeeID == employee.ID).OrderBy("ContractStartDate", false).FirstOrDefault();
                if (lastContract != null)
                {
                    employee.ContractType = lastContract.ContractCategory;
                    employee.DeterminePostsDate = lastContract.PostDate;
                }
            }
            this.BusinessEntities.SaveChanges();
        }

        public override JsonResult Delete()
        {
            flowService.Delete(Request["ID"], Request["TaskExecID"], Request["ListIDs"]);
            this.BusinessEntities.SaveChanges();
            string employeeID = GetQueryString("EmployeeID");
            var employee = BusinessEntities.Set<T_Employee>().Find(employeeID);
            if (employee != null)
            {
                var lastContract = BusinessEntities.Set<T_EmployeeContract>().Where(c => c.EmployeeID == employee.ID).OrderBy("ContractStartDate", false).FirstOrDefault();
                if (lastContract != null)
                {
                    employee.ContractType = lastContract.ContractCategory;
                    employee.DeterminePostsDate = lastContract.PostDate;
                }
                else
                {
                    employee.ContractType = "";
                    employee.DeterminePostsDate = null;
                }
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
