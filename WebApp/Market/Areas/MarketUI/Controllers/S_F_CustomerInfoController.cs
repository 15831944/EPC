using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula.Helper;
using Config.Logic;
using Newtonsoft.Json;
using Formula.ImportExport;
using Formula;
using Config;
using System.Data;


namespace Market.Areas.MarketUI.Controllers
{
    public class CustomerInfoController : MarketFormContorllor<S_F_Customer>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_F_Customer", dic.GetValue("ID"));

            string bCode = dic.GetValue("BusinessCode");
            string id = dic["ID"];
            if (!string.IsNullOrEmpty(bCode)
                && BusinessEntities.Set<S_F_Customer>().Any(a => a.BusinessCode == bCode && a.ID != id))
            {
                throw new Formula.Exceptions.BusinessException("工商代码【" + dic.GetValue("BusinessCode") + "】已存在");
            }

            var entity = this.GetEntityByID(dic["ID"]);
            if (entity == null)
                entity = new S_F_Customer();

            if (String.IsNullOrEmpty(dic.GetValue("Parent")))
            {
                dic.SetValue("FullID", dic.GetValue("ID"));
            }
            else
            {
                var parent = this.GetEntityByID(dic.GetValue("Parent"));
                if (parent == null)
                {
                    dic.SetValue("Parent", "");
                    dic.SetValue("FullID", dic.GetValue("ID"));
                }
                else
                {
                    var fullID = parent.FullID + "." + dic.GetValue("ID");
                    dic.SetValue("FullID", fullID);
                }
            }
            this.UpdateEntity(entity, dic);
            entity.SychCustomerProperties();
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_F_Customer", Id);
                var entity = this.GetEntityByID(Id);
                if (entity != null)
                {
                    if (this.BusinessEntities.Set<S_F_Customer>().Count(a => !string.IsNullOrEmpty(a.FullID) && a.FullID.StartsWith(entity.FullID) && a.ID != entity.ID) > 0)
                        throw new Formula.Exceptions.BusinessException("客户【" + entity.Name + "】存在子公司或子机构，无法进行删除");
                    if (this.BusinessEntities.Set<S_C_ManageContract>().FirstOrDefault(a => a.PartyA == entity.ID) != null)
                        throw new Formula.Exceptions.BusinessException("客户【" + entity.Name + "】已经有合同，无法进行删除");
                }
            }
        }


        #region Excel导入

        public JsonResult ValidateData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var excelData = JsonConvert.DeserializeObject<ExcelData>(tempdata["data"]);
            var errors = excelData.Vaildate(e =>
            {
                if (e.FieldName == "CustomerName" && string.IsNullOrWhiteSpace(e.Value))
                {
                    e.IsValid = false;
                    e.ErrorText = string.Format("客户名称不能为空", e.Value);
                }
            });
            return Json(errors);
        }

        public JsonResult SaveExcelData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(tempdata["data"]);
            var currentUser = FormulaHelper.GetUserInfo();
            foreach (var item in list)
            {
                var customerName = item.GetValue("Name");
                var customer = this.BusinessEntities.Set<S_F_Customer>().FirstOrDefault(d => d.Name == customerName);
                if (customer == null)
                {
                    customer = new S_F_Customer();
                    customer.ID = FormulaHelper.CreateGuid();
                    this.UpdateEntity<S_F_Customer>(customer, item);
                    customer.CreateUser = this.CurrentUserInfo.UserName;
                    customer.CreateUserID = this.CurrentUserInfo.UserID;
                    customer.CreateDate = DateTime.Now;
                    this.BusinessEntities.Set<S_F_Customer>().Add(customer);
                }
                this.BusinessEntities.SaveChanges();
            }
            return Json("Success");
        }

        public JsonResult ValidateLinkManData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var excelData = JsonConvert.DeserializeObject<ExcelData>(tempdata["data"]);
            var errors = excelData.Vaildate(e =>
            {
                if (e.FieldName == "LinkmanName" && string.IsNullOrWhiteSpace(e.Value))
                {
                    e.IsValid = false;
                    e.ErrorText = string.Format("联系人姓名不能为空", e.Value);
                }
                if (e.FieldName == "CustomerName" && string.IsNullOrWhiteSpace(e.Value))
                {
                    e.IsValid = false;
                    e.ErrorText = string.Format("客户名称不能为空", e.Value);
                }
            });
            return Json(errors);
        }

        public JsonResult SaveLinkManExcelData()
        {
            var reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string data = reader.ReadToEnd();
            var tempdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(tempdata["data"]);
            var currentUser = FormulaHelper.GetUserInfo();
            foreach (var item in list)
            {
                var customerName = item.GetValue("CustomerName");
                var customer = this.BusinessEntities.Set<S_F_Customer>().FirstOrDefault(d => d.Name == customerName);
                if (customer == null)
                {
                    customer = new S_F_Customer();
                    customer.ID = FormulaHelper.CreateGuid();
                    customer.Name = customerName;
                    customer.CustomerLevel = item.GetValue("CustomerLevel");
                    customer.Country = "中国";
                    customer.Province = item.GetValue("Province");
                    customer.City = item.GetValue("City");
                    customer.Area = item.GetValue("Area");
                    customer.CreateUser = this.CurrentUserInfo.UserName;
                    customer.CreateUserID = this.CurrentUserInfo.UserID;
                    customer.CreateDate = DateTime.Now;
                    this.BusinessEntities.Set<S_F_Customer>().Add(customer);
                }
                var linkmanName = item.GetValue("LinkmanName");
                var linkman = customer.S_F_Customer_LinkMan.FirstOrDefault(d => d.LinkmanName == linkmanName);
                if (linkman == null)
                {
                    linkman = new S_F_Customer_LinkMan();
                    linkman = this.UpdateEntity<S_F_Customer_LinkMan>(linkman, item);
                    linkman.ID = FormulaHelper.CreateGuid();
                    customer.S_F_Customer_LinkMan.Add(linkman);
                }
                this.BusinessEntities.SaveChanges();
            }
            return Json("Success");
        }

        #endregion

        //员工登录智能提示
        public JsonResult SmartReminder(string key)
        {
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper("Market");
            string sqlCommand = "select ID,Name,CreateUser,CreateDate from S_F_Customer where Name like '%" + key + "%'";
            DataTable data = sqlHelp.ExecuteDataTable(sqlCommand);

            return Json(data);
        }
    }
}
