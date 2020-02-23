using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Config.Logic;
using Formula;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierController : MarketFormContorllor<S_SP_Supplier>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_SP_Supplier", dic.GetValue("ID"));
            }
            string updateSql = @" update S_SP_Payment set SupplierName='{0}' where Supplier='{1}';
update S_SP_Invoice set SupplierName='{0}' where Supplier='{1}';
update S_SP_SupplierContract set SupplierName='{0}'  where Supplier='{1}';
update S_SP_PaymentPlan set SupplierName='{0}'  where Supplier='{1}';
";
            updateSql = String.Format(updateSql, dic.GetValue("Name"), dic.GetValue("ID"));
            this.MarketSQLDB.ExecuteNonQuery(updateSql);
        }

        public JsonResult GetCredentialInfo(string suppierID)
        {
            var credentialList = this.BusinessEntities.Set<S_SP_Supplier_CredentialInfo>().Where(d => d.S_SP_SupplierID == suppierID).Select(d => new
            {
                CertificateName = d.CertificateName,
                CertificateCode = d.CertificateCode,
                Achievement = d.Achievement,
                ExpiryDate = d.ExpiryDate,
                Scope = d.Scope,
                Attachment = d.Attachment,
                AttachmentName = d.AttachmentName
            }).ToList();
            return Json(credentialList);
        }

        public JsonResult GetCoopProjectInfo(string suppierID)
        {
            var coopProjectList = this.BusinessEntities.Set<S_SP_Supplier_CoopProjectInfo>().Where(d => d.S_SP_SupplierID == suppierID).Select(d => new
            {
                Project = d.Project,
                ProjectName = d.ProjectName,
                ChargerUser = d.ChargerUser,
                ChargerUserName = d.ChargerUserName,
                DesignDept = d.DesignDept,
                DesignDeptName = d.DesignDeptName,
                ProductName = d.ProductName,
                Evaluate = d.Evaluate
            }).ToList();
            return Json(coopProjectList);
        }
        
        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_SP_Supplier", Id);
                var supplier = this.GetEntityByID(Id);
                if (supplier == null) throw new Formula.Exceptions.BusinessException("未能找到指定的分包商");
                if (this.BusinessEntities.Set<S_SP_SupplierContract>().Count(d => d.Supplier == Id) > 0)
                {
                    throw new Formula.Exceptions.BusinessException("已经有分包合同的合作供应商不能删除");
                }
            }
        }

        public JsonResult GetContractByProjectID(string ProjectInfoID)
        {
            var list = this.BusinessEntities.Set<S_C_ManageContract_ReceiptObj>().Where(d => d.ProjectInfo == ProjectInfoID).ToList();
            return Json(list);
        }

        public JsonResult GetContractInfoByProjectID(string ProjectInfoID)
        {
            var contractList = this.BusinessEntities.Set<S_C_ManageContract_ProjectRelation>().Where(d => d.ProjectID == ProjectInfoID).ToList();
            if (contractList.Count == 0) return Json("");
            var contractIDs = contractList.Select(d => d.S_C_ManageContractID).ToList();
            var list = this.BusinessEntities.Set<S_C_ManageContract>().Where(d => contractIDs.Contains(d.ID)).ToList();
            return Json(list);
        }

        public JsonResult GetBankInfo(string ID)
        {
            var result = new List<Dictionary<string, object>>();
            var supplier = this.BusinessEntities.Set<S_SP_Supplier>().Find(ID);
            if (supplier != null)
            {
                foreach (var item in supplier.S_SP_Supplier_BankInfo.ToList())
                {
                    var dic = new Dictionary<string, object>();
                    dic.SetValue("value", item.BankAccount);
                    dic.SetValue("text", item.BankAccountName + "-" + item.BankAccount);
                    dic.SetValue("bankName", item.BankName);
                    result.Add(dic);
                }
            }
            return Json(result);
        }
    }
}
