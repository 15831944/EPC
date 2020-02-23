using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula.Helper;
using Config.Logic;
using Project.Logic.Domain;
using Project.Logic;
using System.Data;
using Formula;

namespace Market.Areas.MarketUI.Controllers
{
    public class EngineeringInfoController : MarketFormContorllor<S_I_Engineering>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (dic.ContainsKey("SerialNumber") && string.IsNullOrEmpty(dic.GetValue("Code")))
                dic.SetValue("Code", dic.GetValue("SerialNumber"));
            var projectEntities = Formula.FormulaHelper.GetEntities<ProjectEntities>();
            var id = dic.GetValue("ID");
            var group = projectEntities.Set<S_I_ProjectGroup>().FirstOrDefault(d => d.RelateID == id);
            bool buildEPS = false;
            if (group == null)
            {
                group = new S_I_ProjectGroup();
                group.ID = Formula.FormulaHelper.CreateGuid();
                buildEPS = true;
            }
            group.Name = dic.GetValue("Name");
            group.Code = dic.GetValue("Code");
            group.City = dic.GetValue("City");
            group.Country = dic.GetValue("Country");
            group.Province = dic.GetValue("Province");
            group.ProjectClass = dic.GetValue("Class");
            group.Investment = String.IsNullOrEmpty(dic.GetValue("Investment")) ? 0M : Convert.ToDecimal(dic.GetValue("Investment"));
            group.Proportion = dic.GetValue("Proportion");
            group.PhaseContent = dic.GetValue("PhaseContent");
            group.Address = dic.GetValue("Address");
            group.DeptID = dic.GetValue("MainDept");
            group.DeptName = dic.GetValue("MainDeptName");
            group.RelateID = dic.GetValue("ID");
            group.ChargeUser = dic.GetValue("ChargerUser");
            group.ChargeUserName = dic.GetValue("ChargerUserName");
            group.EngineeringSpaceCode = ProjectMode.Standard.ToString();
            group.CreateDate = DateTime.Now;
            if (buildEPS)
            {
                var fo = Formula.FormulaHelper.CreateFO<EPSFO>();
                fo.BuildEngineering(group);
            }
            projectEntities.SaveChanges();
        }

        protected override void AfterSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {

            #region 自动同步核算项目
            var newDic = new Dictionary<string, object>();
            foreach (var item in dic.Keys)
                newDic.SetValue(item, dic.GetValue(item));
            Expenses.Logic.CBSInfoFO.SynCBSInfo(newDic, Expenses.Logic.SetCBSOpportunity.Engineering);
            #endregion
        }
    }
}
