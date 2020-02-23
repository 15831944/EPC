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
using Config;
using Config.Logic;
using MvcAdapter;
using Market.Logic;
using Market.Logic.Domain;


namespace Market.Areas.Basic.Controllers
{
    public class ReceiptController : MarketController<S_C_Receipt>
    {
        public override JsonResult GetList(QueryBuilder qb)
        {
            string sql = @"select * from S_C_Receipt";
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            string sumSql = "select isnull(Sum(Amount),0) as SumAmount from (" + sql + qb.GetWhereString() + ") SumDataInfo";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt != null && sumDt.Rows.Count > 0)
            {
                data.sumData.SetValue("Amount", Convert.ToDecimal(sumDt.Rows[0]["SumAmount"]).ToString("c"));
            }
            return Json(data);
        }

        protected override void AfterGetMode(Dictionary<string, object> entityDic, S_C_Receipt entity, bool isNew)
        {
            var receiptID = entityDic.GetValue("ID");
            if (isNew)
            {
                entityDic.SetValue("Operator", this.CurrentUserInfo.UserName);
                entityDic.SetValue("OperatorID", this.CurrentUserInfo.UserID);
                entityDic.SetValue("ArrivedDate", DateTime.Now);
                entityDic.SetValue("ReceiptType", "转账");
            }
            else
            {
                var contract = this.GetEntityByID<S_C_ManageContract>(entityDic.GetValue("ContractInfoID"));
                entityDic.SetValue("ContractValue", contract.ContractRMBAmount);
                //var receiptID = entityDic.GetValue("ID");
                var contractReceiptValue = Convert.ToDecimal(contract.S_C_Receipt.Where(d => d.ID != receiptID).Sum(d => d.Amount));
                entityDic.SetValue("ContractReceiptValue", contractReceiptValue);
                var remainValue = Convert.ToDecimal(contract.ContractRMBAmount) - contractReceiptValue;
                if (remainValue < 0) remainValue = 0;
                entityDic.SetValue("RemainReceiptValue", remainValue);

                entityDic.SetValue("CustomerFullID", contract.CustomerFullID);
                entityDic.SetValue("CustomerFullIDName", contract.CustomerFullIDName);
                entityDic.SetValue("PartyA", contract.PartyA);
                entityDic.SetValue("PartyAName", contract.PartyAName);
                
            }
            var registerID =entityDic.GetValue("RegisterID");
            if(string.IsNullOrEmpty(registerID))
                registerID = GetQueryString("RegisterID");
            var register = this.GetEntityByID<S_C_ReceiptRegister>(registerID);
            if (register != null)
            {
                entityDic.SetValue("ReceiptRegisterValue", register.ReceiptValue);
                var confirmValue = 0m;
                if (this.entities.Set<S_C_Receipt>().Count(d => d.ID != receiptID && d.RegisterID == register.ID) > 0)
                    confirmValue = Convert.ToDecimal(this.entities.Set<S_C_Receipt>().Where(d => d.ID != receiptID && d.RegisterID == register.ID).Sum(d => d.Amount));
                entityDic.SetValue("ConfirmValue", confirmValue);
                var remainValue = Convert.ToDecimal(register.ReceiptValue) - confirmValue;
                entityDic.SetValue("RemainValue", remainValue);
                if (isNew)
                {
                    entityDic.SetValue("RegisterID", register.ID);
                    entityDic.SetValue("PayerUnit", register.CustomerInfo);
                    entityDic.SetValue("PayerUnitName", register.CustomerInfoName);
                    entityDic.SetValue("ArrivedDate", register.ReceiptDate);
                }
            }
            var invoiceID = GetQueryString("InvoiceID");
            if (!string.IsNullOrEmpty(invoiceID))
            {
                var invoice = this.GetEntityByID<S_C_Invoice>(invoiceID);
                if (invoice != null)
                {
                    var contract = invoice.S_C_ManageContract;
                    entityDic.SetValue("ContractValue", contract.ContractRMBAmount);
                    //var receiptID = entityDic.GetValue("ID");
                    var contractReceiptValue = Convert.ToDecimal(contract.S_C_Receipt.Where(d => d.ID != receiptID).Sum(d => d.Amount));
                    entityDic.SetValue("ContractReceiptValue", contractReceiptValue);
                    var remainValue = Convert.ToDecimal(contract.ContractRMBAmount) - contractReceiptValue;
                    if (remainValue < 0) remainValue = 0;
                    entityDic.SetValue("RemainReceiptValue", remainValue);

                    entityDic.SetValue("ContractInfoID", contract.ID);
                    entityDic.SetValue("ContractName", contract.Name);
                    entityDic.SetValue("CustomerID", contract.PartyA);
                    entityDic.SetValue("CustomerName", contract.PartyAName);

                    var invoiceList = new List<Dictionary<string,object>>();
                    var invoiceDic = invoice.ToDic();
                    var RelationValue = invoice.S_C_InvoiceReceiptRelation.Sum(a => a.RelationValue);
                    invoiceDic.SetValue("SumRelationValue", RelationValue);
                    invoiceList.Add(invoiceDic);
                    entityDic.SetValue("InvoiceList", JsonHelper.ToJson<List<Dictionary<string, object>>>(invoiceList));
                    entityDic.SetValue("Amount", (invoice.Amount - RelationValue));
                    entityDic.SetValue("InvoiceID", invoiceID);

                    entityDic.SetValue("CustomerFullID", contract.CustomerFullID);
                    entityDic.SetValue("CustomerFullIDName", contract.CustomerFullIDName);
                    entityDic.SetValue("PartyA", contract.PartyA);
                    entityDic.SetValue("PartyAName", contract.PartyAName);
                }
            }
        }

        protected override void BeforeSave(S_C_Receipt entity, bool isNew)
        {
            if (!isNew)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_Receipt", entity.ID);
            }
            string planData = this.Request["PlanReceiptData"];
            string invoiceData = this.Request["InvoiceData"];
            if (!String.IsNullOrEmpty(planData))
            {
                var sumReceiptValue = 0M;
                entity.ClearPlanReceipt();

                var list = JsonHelper.ToList(planData);
                foreach (var plan in list)
                {
                    if (String.IsNullOrEmpty(plan.GetValue("RelationValue"))) throw new Formula.Exceptions.BusinessException("计划收款未填写关联金额，操作失败");
                    var relateValue = Convert.ToDecimal(plan.GetValue("RelationValue"));
                    sumReceiptValue += relateValue;
                    entity.RelateToPlanReceipt(plan.GetValue("PlanID"), relateValue);
                }
                if (sumReceiptValue > entity.Amount)
                    throw new Formula.Exceptions.BusinessException("关联的计划收款总金额不能大于本次收款金额");
            }

            if (!String.IsNullOrEmpty(invoiceData))
            {
                var list = JsonHelper.ToList(invoiceData);
                entity.ClearInvoice();
                var sumInvoiceValue = 0M;
                foreach (var invoice in list)
                {
                    if (String.IsNullOrEmpty(invoice.GetValue("RelationValue"))) throw new Formula.Exceptions.BusinessException("发票未填写关联金额，操作失败");
                    var relateValue = Convert.ToDecimal(invoice.GetValue("RelationValue"));
                    sumInvoiceValue += relateValue;
                    entity.RelateToInvoice(invoice.GetValue("InvoiceID"), relateValue);
                }
                if (sumInvoiceValue > entity.Amount)
                    throw new Formula.Exceptions.BusinessException("关联的发票总金额不能大于本次收款金额");
            }

            SaveDeptRelation(entity);
            SaveProjectRelation(entity);

            if (!String.IsNullOrEmpty(entity.CustomerID))
            {
                var customerInfo = this.GetEntity<S_F_Customer>(entity.CustomerID);
                if (customerInfo != null)
                {
                    entity.CustomerFullID = customerInfo.FullID;
                    entity.CustomerName = customerInfo.Name;
                }
            }

            //判定是否是认领收款，如果是认领收款则要做金额限制校验
            if (!String.IsNullOrEmpty(entity.RegisterID))
            {
                var receiptRegister = this.GetEntityByID<S_C_ReceiptRegister>(entity.RegisterID);
                if (receiptRegister == null) throw new Formula.Exceptions.BusinessException("未能找到对应的到款登记记录，无法认领到款");
                var receiptID = entity.ID;
                var confirmValue =0m; 
                var exceptList = this.entities.Set<S_C_Receipt>().Where(d => d.RegisterID == receiptRegister.ID && d.ID != receiptID).ToList();
                if (exceptList.Count > 0)
                    confirmValue = exceptList.Sum(d => d.Amount);
                //var sumConfirmValue = (confirmValue.HasValue ? confirmValue.Value : 0m) + (String.IsNullOrEmpty(dic.GetValue("ReceiptValue")) ? 0m :
                //Convert.ToDecimal(dic.GetValue("ReceiptValue")));
                var sumConfirmValue = confirmValue + entity.Amount;
                if (sumConfirmValue > receiptRegister.ReceiptValue)
                    throw new Formula.Exceptions.BusinessException("认领金额总和不能大于收款登记总金额【" + receiptRegister.ReceiptValue + "】");
            }
            if (isNew)
            {
                if (!String.IsNullOrEmpty(entity.RegisterID))
                {
                    entity.State = "Create";
                    //dic.SetValue("State", "Create");
                }
            }

            //如果是承兑汇票的到款项,则不能这里修改
            if (entities.Set<S_C_AcceptanceBill>().Any(a => a.ReceiptIDs.Contains(entity.ID)))
            {
                throw new Formula.Exceptions.BusinessException("到款项对应为承兑汇票的子项内容,不能修改");
            }

            entity.Save();
        }

        private void SaveDeptRelation(S_C_Receipt entity)
        {
            //需要兼容只有收款计划的情况
            string deptData = this.Request["DeptReceiptData"];
            string planData = this.Request["PlanReceiptData"];
            var deleteDeptList = entity.S_C_Receipt_DeptRelation.ToList();
            if (!String.IsNullOrEmpty(deptData))
            {
                #region 各部门分配金额合计必须等于本次收款金额
                
                var list = JsonHelper.ToList(deptData);
                var sumDeptValue = 0M;
                foreach (var item in list)
                {
                    var dept = deleteDeptList.FirstOrDefault(a => a.Dept == item.GetValue("Dept"));
                    if (dept == null)
                    {
                        dept = new S_C_Receipt_DeptRelation();
                        dept = FormulaHelper.UpdateEntity(dept, item);
                        dept.ID = FormulaHelper.CreateGuid();
                        dept.S_C_Receipt_ID = entity.ID;
                        dept.ContractInfoID = entity.ContractInfoID;
                        entity.S_C_Receipt_DeptRelation.Add(dept);
                    }
                    else
                    {
                        dept = FormulaHelper.UpdateEntity(dept, item);
                        deleteDeptList.Remove(dept);
                    }
                    dept.ModifyDate = DateTime.Now;
                    dept.ModifyUserID = CurrentUserInfo.UserID;
                    dept.ModifyUser = CurrentUserInfo.UserName;
                    sumDeptValue += (dept.RelationValue ?? 0);
                }
                if (sumDeptValue != entity.Amount)
                    throw new Formula.Exceptions.BusinessException("各部门分配金额合计必须等于本次收款金额");
                #endregion
            }
            else
            {
                #region 没有部门列表，默认将按合同的部门分解子表比例分配金额
                var contract = this.entities.Set<S_C_ManageContract>().FirstOrDefault(a => a.ID == entity.ContractInfoID);
                var conDepts = contract.S_C_ManageContract_DeptRelation.OrderBy(a => a.SortIndex).ToList();
                var conValue = Convert.ToDecimal(contract.ContractRMBAmount);
                if(conValue!=0)
                {
                    var sumValue = 0m;
                    foreach (var conDept in conDepts)
                    {
                        var dept = deleteDeptList.FirstOrDefault(a => a.Dept == conDept.Dept);
                        if (dept == null)
                        {
                            dept = new S_C_Receipt_DeptRelation();
                            dept.ID = FormulaHelper.CreateGuid();
                            dept.Dept = conDept.Dept;
                            dept.DeptName = conDept.DeptName;
                            dept.S_C_Receipt_ID = entity.ID;
                            dept.ContractInfoID = entity.ContractInfoID;
                            entity.S_C_Receipt_DeptRelation.Add(dept);
                        }
                        else
                            deleteDeptList.Remove(dept);

                        var amount = 0m;
                        if (conDept != conDepts.LastOrDefault())
                        {
                            var scale = Math.Round((Convert.ToDecimal(conDept.DeptValue) + Convert.ToDecimal(conDept.SumSupplementaryValue)) / conValue, 2);
                            amount = entity.Amount * scale;
                            sumValue += amount;
                        }
                        else
                            amount = entity.Amount - sumValue;

                        dept.ModifyDate = DateTime.Now;
                        dept.ModifyUserID = CurrentUserInfo.UserID;
                        dept.ModifyUser = CurrentUserInfo.UserName;
                        dept.RelationValue = amount;
                    }
                }
                #endregion
            }
            foreach (var item in deleteDeptList)
            {
                this.entities.Set<S_C_Receipt_DeptRelation>().Remove(item);
            }
        }

        private void SaveProjectRelation(S_C_Receipt entity)
        {
            //需要兼容只有收款计划的情况
            string projectData = this.Request["ProjectReceiptData"];
            var deleteProjectList = entity.S_C_Receipt_ProjectRelation.ToList();
            if (!String.IsNullOrEmpty(projectData))
            {
                #region 各项目分配金额合计必须等于本次收款金额

                var list = JsonHelper.ToList(projectData);
                if (list.Count != 0)
                {
                    var sumProjectValue = 0M;
                    foreach (var item in list)
                    {
                        var project = deleteProjectList.FirstOrDefault(a => a.ProjectID == item.GetValue("ProjectID"));
                        if (project == null)
                        {
                            project = new S_C_Receipt_ProjectRelation();
                            project = FormulaHelper.UpdateEntity(project, item);
                            project.ID = FormulaHelper.CreateGuid();
                            project.S_C_Receipt_ID = entity.ID;
                            project.ContractInfoID = entity.ContractInfoID;
                            entity.S_C_Receipt_ProjectRelation.Add(project);
                        }
                        else
                        {
                            project = FormulaHelper.UpdateEntity(project, item);
                            deleteProjectList.Remove(project);
                        }
                        project.ModifyDate = DateTime.Now;
                        project.ModifyUserID = CurrentUserInfo.UserID;
                        project.ModifyUser = CurrentUserInfo.UserName;
                        sumProjectValue += (project.RelationValue ?? 0);
                    }
                    if (sumProjectValue != entity.Amount)
                        throw new Formula.Exceptions.BusinessException("各项目分配金额合计必须等于本次收款金额");
                }
                #endregion
            }
            else
            {
                #region 没有项目列表，默认将按合同的关联项目子表比例分配金额
                var contract = this.entities.Set<S_C_ManageContract>().FirstOrDefault(a => a.ID == entity.ContractInfoID);
                var conProjects = contract.S_C_ManageContract_ProjectRelation.OrderBy(a => a.SortIndex).ToList();
                var conValue = Convert.ToDecimal(contract.ContractRMBAmount);
                if (conValue != 0)
                {
                    var sumValue = 0m;
                    foreach (var conProject in conProjects)
                    {
                        var project = deleteProjectList.FirstOrDefault(a => a.ProjectID == conProject.ProjectID);
                        if (project == null)
                        {
                            project = new S_C_Receipt_ProjectRelation();
                            project.ID = FormulaHelper.CreateGuid();
                            project.ProjectID = conProject.ProjectID;
                            project.ProjectName = conProject.ProjectName;
                            project.S_C_Receipt_ID = entity.ID;
                            project.ContractInfoID = entity.ContractInfoID;
                            entity.S_C_Receipt_ProjectRelation.Add(project);
                        }
                        else
                            deleteProjectList.Remove(project);

                        var amount = 0m;
                        if (conProject != conProjects.LastOrDefault())
                        {
                            var scale = Math.Round(Convert.ToDecimal(conProject.ProjectValue) / conValue, 2);
                            amount = entity.Amount * scale;
                            sumValue += amount;
                        }
                        else
                            amount = entity.Amount - sumValue;

                        project.ModifyDate = DateTime.Now;
                        project.ModifyUserID = CurrentUserInfo.UserID;
                        project.ModifyUser = CurrentUserInfo.UserName;
                        project.RelationValue = amount;
                    }
                }

                #endregion
            }
            foreach (var item in deleteProjectList)
            {
                this.entities.Set<S_C_Receipt_ProjectRelation>().Remove(item);
            }
        }

        protected override void AfterSave(S_C_Receipt entity, bool isNew)
        {
            //如果是通过到款认领功能进行拆分登记，则需要汇总到款登记上的实际认领金额数据
            if (!String.IsNullOrEmpty(entity.RegisterID))
            {
                var receiptRegister = this.GetEntityByID<S_C_ReceiptRegister>(entity.RegisterID);
                receiptRegister.SumConfirmValue();
            }
            this.entities.SaveChanges();
            //更新合同部门的收款
            entity.SumContractDeptRelation();//合同项目表没有收款汇总所以这里不需要汇总项目的总收款
        }

        protected override void BeforeDelete(List<S_C_Receipt> entityList)
        {
            foreach (var entity in entityList)
            {
                Expenses.Logic.BusinessFacade.DataInterfaceFo.ValidateDataSyn("S_C_Receipt", entity.ID);
            }
        }
        public override JsonResult Delete()
        {
            if (!String.IsNullOrEmpty(Request["ListIDs"]))
            {
                var ids = Request["ListIDs"].Split(',');
                foreach (var Id in ids)
                {
                    var entity = this.GetEntityByID(Id);

                    //如果是承兑汇票的到款项,则不能在这里删除 
                    if (entities.Set<S_C_AcceptanceBill>().Any(a => a.ReceiptIDs.Contains(Id)))
                    {
                        throw new Formula.Exceptions.BusinessException("所选到款项对应为承兑汇票的子项内容,不能删除");
                    }

                    if (entity.CreateUserID != this.CurrentUserInfo.UserID && !String.IsNullOrEmpty(entity.RegisterID))
                        throw new Formula.Exceptions.BusinessException("不能删除别人认领的收款信息");
                    entity.Delete();
                    this.entities.SaveChanges();
                    if (!String.IsNullOrEmpty(entity.RegisterID))
                    {
                        var receiptRegister = this.entities.Set<S_C_ReceiptRegister>().Find(entity.RegisterID);
                        if (receiptRegister != null)
                        {
                            receiptRegister.SumConfirmValue();
                        }
                    }
                    entity.SumContractDeptRelation();//合同项目表没有收款汇总所以这里不需要汇总项目的总收款
                }
                this.entities.SaveChanges();
            }
            return Json("");
        }

        public JsonResult ValidateModify(string ID)
        {
            var receipt = this.GetEntityByID(ID);
            if (receipt == null) throw new Formula.Exceptions.BusinessException("未能找到指定的收款记录，无法修订");
            if (!String.IsNullOrEmpty(receipt.RegisterID) && this.entities.Set<S_C_ReceiptRegister>().Count(d => d.ID == receipt.RegisterID) > 0
                && receipt.State == "Normal")
            {
                throw new Formula.Exceptions.BusinessException("财务已经确认的认领记录不能编辑");
            }
            return Json("");
        }

        public JsonResult RemovePlanRelation(string ReceiptID,string DeleteList)
        {
            var receipt = this.GetEntityByID(ReceiptID);
            if (receipt != null)
            {
                var list = JsonHelper.ToList(DeleteList);
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item.GetValue("ID"))) continue;
                    var plan = this.GetEntity<S_C_PlanReceipt>(item.GetValue("PlanID"));
                    if (plan == null) continue;
                    receipt.RemovePlanRelation(plan);
                }
                this.entities.SaveChanges();
            }
            return Json("");
        }

        public JsonResult GetPlanRelationList(string ReceiptID)
        {
            string sql = @"select * from dbo.S_C_ReceiptPlanRelation
left join dbo.S_C_PlanReceipt on S_C_PlanReceipt.ID=S_C_ReceiptPlanRelation.PlanID
left join (select Sum(RelationValue) as SumRelationValue,PlanID
from S_C_ReceiptPlanRelation where ReceiptID !='{0}'
group by PlanID) SumRelationInfo on  SumRelationInfo.PlanID = S_C_ReceiptPlanRelation.ID
where ReceiptID='{0}' order by S_C_PlanReceipt.CreateDate";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ReceiptID));
            return Json(dt);
        }

        public JsonResult GetInvoiceRelationList(string ReceiptID)
        {
            string sql = @"select * from S_C_InvoiceReceiptRelation
left join dbo.S_C_Invoice on S_C_Invoice.ID=S_C_InvoiceReceiptRelation.InvoiceID
left join (select Sum(RelationValue) as SumRelationValue,InvoiceID
from S_C_InvoiceReceiptRelation where ReceiptID !='{0}'
group by InvoiceID) SumRelationInfo on  SumRelationInfo.InvoiceID = S_C_Invoice.ID
where ReceiptID='{0}' ";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ReceiptID));
            return Json(dt);
        }

        public JsonResult GetDeptRelationList(string ReceiptID)
        {
            string sql = @"select S_C_Receipt_DeptRelation.*,Scale,DeptValue,SumRelationValue from S_C_Receipt_DeptRelation
left join (select Sum(RelationValue) as SumRelationValue,Dept,ContractInfoID
from S_C_Receipt_DeptRelation where S_C_Receipt_ID !='{0}' 
group by Dept,ContractInfoID) SumRelationInfo on  SumRelationInfo.Dept = S_C_Receipt_DeptRelation.Dept 
and SumRelationInfo.ContractInfoID = S_C_Receipt_DeptRelation.ContractInfoID
left join S_C_ManageContract_DeptRelation on S_C_ManageContract_DeptRelation.Dept = S_C_Receipt_DeptRelation.Dept
and S_C_ManageContract_DeptRelation.S_C_ManageContractID = S_C_Receipt_DeptRelation.ContractInfoID
where S_C_Receipt_ID='{0}' order by S_C_ManageContract_DeptRelation.SortIndex";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ReceiptID));
            return Json(dt);
        }

        public JsonResult GetProjectRelationList(string ReceiptID)
        {
            string sql = @"select S_C_Receipt_ProjectRelation.*,ProjectCode,ChargerDept,ChargerDeptName,Scale,ProjectValue,SumRelationValue from S_C_Receipt_ProjectRelation
left join (select Sum(RelationValue) as SumRelationValue,ProjectID,ContractInfoID
from S_C_Receipt_ProjectRelation where S_C_Receipt_ID !='{0}' 
group by ProjectID,ContractInfoID) SumRelationInfo on  SumRelationInfo.ProjectID = S_C_Receipt_ProjectRelation.ProjectID 
and SumRelationInfo.ContractInfoID = S_C_Receipt_ProjectRelation.ContractInfoID
left join S_C_ManageContract_ProjectRelation on S_C_ManageContract_ProjectRelation.ProjectID = S_C_Receipt_ProjectRelation.ProjectID
and S_C_ManageContract_ProjectRelation.S_C_ManageContractID = S_C_Receipt_ProjectRelation.ContractInfoID
where S_C_Receipt_ID='{0}'  order by S_C_ManageContract_ProjectRelation.SortIndex";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ReceiptID));
            return Json(dt);
        }
        
        public JsonResult GetWriteOffList(QueryBuilder qb)
        {
            string sql = @"select * from (select S_C_Receipt.*,PlanRelationValue,InvoiceRelationValue,
isnull(Amount,0)-isnull(PlanRelationValue,0) as RemainPlanValue,
isnull(Amount,0)-isnull(InvoiceRelationValue,0) as RemainInvoiceValue,
case when (isnull(Amount,0)-isnull(PlanRelationValue,0))=0 and 
(isnull(Amount,0)-isnull(InvoiceRelationValue,0))=0 then 'Yes' else 'No' end as WriteOffState
from S_C_Receipt left join (select Sum(RelationValue) as PlanRelationValue,ReceiptID from S_C_ReceiptPlanRelation
group by ReceiptID) PlanRelation on PlanRelation.ReceiptID=S_C_Receipt.ID
left join (select Sum(RelationValue) as InvoiceRelationValue,ReceiptID from S_C_InvoiceReceiptRelation
group by ReceiptID) InvoiceRelation on  InvoiceRelation.ReceiptID = S_C_Receipt.ID) TableInfo  ";
            var data = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public JsonResult GetInvoiceList(QueryBuilder qb, string ContractInfoID)
        {
            var State = InvoiceState.Invalid.ToString();
            string sql = @" select S_C_Invoice.*,isnull(SumRelationValue,0) as SumRelationValue,
Amount-isnull(SumRelationValue,0) as RemainRelationValue from S_C_Invoice
left join (select Sum(RelationValue) as SumRelationValue,InvoiceID from dbo.S_C_InvoiceReceiptRelation
group by InvoiceID) RelationInfo
on RelationInfo.InvoiceID = S_C_Invoice.ID where S_C_Invoice.ContractInfoID='{0}' and (State <> '" + State + "' or State is null )";
            string whereStr = qb.GetWhereString(false);
            if (!String.IsNullOrEmpty(whereStr)) sql += whereStr;
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }

        public JsonResult GetReceiptObjectList(string ContractInfoID)
        {
            string sql = @"select * from dbo.S_C_ReceiptObject where ContractInfoID ='{0}'   order by Code ";
            var dt = this.SqlHelper.ExecuteDataTable(String.Format(sql, ContractInfoID));
            return Json(dt);
        }

        public JsonResult SetWriteOff(string ReceiptData, string ReceiptObjectData, string InvoiceData)
        {
            var receiptData = JsonHelper.ToObject(ReceiptData);
            var receipt = this.GetEntityByID(receiptData.GetValue("ID"));
            if (receipt == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + receiptData.GetValue("ID") + "】的收款对象，无法进行对账操作");
            var receiptObjectList = JsonHelper.ToList(ReceiptObjectData);
            var invoiceList = JsonHelper.ToList(InvoiceData);
            foreach (var item in invoiceList)
            {
                var invoice = this.GetEntityByID<S_C_Invoice>(item.GetValue("ID"));
                if (invoice == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的发票信息，无法进行对账操作");
                receipt.RelateToInvoice(invoice, 0, true);
            }
            foreach (var item in receiptObjectList)
            {
                var receiptObject = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(item.GetValue("ID"));
                if (receiptObject == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + item.GetValue("ID") + "】的收款项信息，无法进行对账操作");
                var plan = receiptObject.S_C_PlanReceipt.FirstOrDefault(d => d.State == PlanReceiptState.UnReceipt.ToString());
                if (plan == null) throw new Formula.Exceptions.BusinessException("收款项不存在未收款的计划内容，无法进行对账操作");
                receipt.RelateToPlanReceipt(plan, 0, true);
            }
            this.entities.SaveChanges();
            return Json("");
        }

        public JsonResult GetContract(string receiptId)
        {
            var recepit = entities.Set<S_C_Receipt>().Find(receiptId);
            if(recepit != null && recepit.S_C_ManageContract != null)
                return Json(recepit.S_C_ManageContract);
            return Json("");
        }
    }
}
