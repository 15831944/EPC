using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Logic.Domain;
using Workflow.Logic.Domain;
using Config.Logic;
using Config;
using Formula.Helper;
using Base.Logic.Model.UI.Form;
using Base.Logic.Domain;
using System.Data;
using Formula;

namespace Market.Areas.MarketUI.Controllers
{
    public class ContractChangeController : MarketFormContorllor<T_C_ContractChange>
    {
        private string[] noNeedSynch = new string[] { "ID", "CreateDate", "ModifyDate", "CreateUserID", "CreateUser", 
            "ModifyUserID", "ModifyUser","OrgID","CompanyID","FlowPhase","FlowInfo","StepName" };


        protected override void AfterGetData(System.Data.DataTable dt, bool isNew, string upperVersionID)
        {
            string tmplCode = Request["TmplCode"];
            var formInfo = baseEntities.Set<S_UI_Form>().SingleOrDefault(c => c.Code == tmplCode);
            if (formInfo == null) throw new Exception("没有找到编号为【" + tmplCode + "】的表单定义");
            var items = JsonHelper.ToList(formInfo.Items).Where(c => c.GetValue("ItemType") == "SubTable").ToList();
            if (isNew)
            {
                string contractID = this.GetQueryString("ContractID");               
                foreach (var item in items)
                {
                    var tableName = formInfo.TableName + "_" + item.GetValue("Code");
                    var contractTableName = "S_C_ManageContract" + "_" + item.GetValue("Code");
                    string sql = "SELECT count(0) as TableCount FROM sysobjects WHERE name='{0}'";
                    var obj = Convert.ToInt32(this.MarketSQLDB.ExecuteScalar(String.Format(sql, contractTableName)));
                    if (obj > 0)
                    {
                        sql = "Select * from {0} where S_C_ManageContractID='{1}'";
                        var subTable = this.MarketSQLDB.ExecuteDataTable(String.Format(sql, contractTableName, contractID));
                        var subTableData = new List<Dictionary<string, object>>();
                        foreach (DataRow subItem in subTable.Rows)
                        {
                            var dic = Formula.FormulaHelper.DataRowToDic(subItem);
                            dic.SetValue("ID", "");
                            dic.SetValue("OrlID", subItem["ID"]);
                            subTableData.Add(dic);
                        }
                        var json = JsonHelper.ToJson(subTableData);
                        SetDtValue(dt, item.GetValue("Code"), json);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    var dic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                    var lastVersionData = JsonHelper.ToJson(dic);
                    SetDtValue(dt, "LastVersionData", lastVersionData);
                }              
            }
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "ReceiptObj")
            {
                var sumReceiptObjValue = 0m;
                foreach (var item in detailList)
                {
                    sumReceiptObjValue += String.IsNullOrEmpty(item.GetValue("ReceiptValue")) ? 0m : Convert.ToDecimal(item.GetValue("ReceiptValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ContractRMBAmount"));
                if (sumReceiptObjValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("收款项金额总计不能大于合同结算金额");
                }
            }
            else if (subTableName == "ContractSplit")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("SplitValue")) ? 0m : Convert.ToDecimal(item.GetValue("SplitValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ContractRMBAmount"));
                if (sumValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("合同分解金额总计不能大于结算人民币金额");
                }
            }
            else if (subTableName == "ProjectRelation")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("ProjectValue")) ? 0m : Convert.ToDecimal(item.GetValue("ProjectValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ContractRMBAmount"));
                if (sumValue > contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("关联项目金额总计不能大于结算人民币金额");
                }
            }
            else if (subTableName == "DeptRelation")
            {
                var sumValue = 0m;
                foreach (var item in detailList)
                {
                    sumValue += String.IsNullOrEmpty(item.GetValue("DeptValue")) ? 0m : Convert.ToDecimal(item.GetValue("DeptValue"));
                }
                var contractRMBValue = String.IsNullOrEmpty(dic.GetValue("ThisContractRMBAmount")) ? 0m : Convert.ToDecimal(dic.GetValue("ThisContractRMBAmount"));
                if (sumValue != contractRMBValue)
                {
                    throw new Formula.Exceptions.BusinessException("部门分解总额总计必须等于折合人民币金额");
                }
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "ReceiptObj")
            {
                var entity = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(detail.GetValue("ID"));

                var receiptValue = String.IsNullOrEmpty(detail.GetValue("ReceiptValue")) ? 0m : Convert.ToDecimal(detail.GetValue("ReceiptValue"));
                //entity.ReceiptValue = receiptValue;
                if (entity != null)
                {
                    var sumInvoice = entity.S_C_Invoice_ReceiptObject.Sum(a => a.RelationValue);
                    var sumReceipt = entity.S_C_PlanReceipt.Sum(a => a.FactReceiptValue);
                    if (receiptValue < sumInvoice || receiptValue < sumReceipt)
                        throw new Formula.Exceptions.BusinessException("收款项金额不能小于已开票或已收款金额");
                }
            }
        }

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            string sql = "select count(0) from S_C_ManageContract  where SerialNumber ='{0}' and ID!='{1}'";
            var obj = this.MarketSQLDB.ExecuteScalar(String.Format(sql, dic.GetValue("SerialNumber"), dic.GetValue("ContractID")));
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("合同编号【" + dic.GetValue("SerialNumber") + "】已经存在，不能重复，请重新编号");
            }
            string contractID = dic.GetValue("ContractID");
            var oldContract = BusinessEntities.Set<S_C_ManageContract>().Find(contractID);
            string oldCustomerIDs = oldContract.CustomerFullID ?? "";
            string oldCustomerIDNames = oldContract.CustomerFullIDName ?? "";

            string customerIDs = dic.GetValue("CustomerFullID") ?? "";

            var oldCustomerIDArr = oldCustomerIDs.Split(',').ToList();
            var oldCustomerNameArr = oldCustomerIDNames.Split(',').ToList();
            for (int i = 0; i < oldCustomerIDArr.Count; i++)
            {
                string oldCustomerID = oldCustomerIDArr[i];
                string customerName = oldCustomerID;
                if (i < oldCustomerNameArr.Count)
                {
                    customerName = oldCustomerNameArr[i];
                }

                if (BusinessEntities.Set<S_C_Invoice>().Any(a => a.PayerUnitID == oldCustomerID && a.ContractInfoID == contractID) && !customerIDs.Contains(oldCustomerID))
                {
                    throw new Formula.Exceptions.BusinessException("付款方【" + customerName + "】已经有开票登记信息,不能");
                }
                else if (BusinessEntities.Set<T_C_InvoiceApply>().Any(a => a.PayerUnit == oldCustomerID && a.Contract == contractID) && !customerIDs.Contains(oldCustomerID))
                {
                    throw new Formula.Exceptions.BusinessException("付款方【" + customerName + "】已经有开票申请信息,不能删除");
                }
                else if (BusinessEntities.Set<S_C_Receipt>().Any(a => a.CustomerID == oldCustomerID && a.ContractInfoID == contractID) && !customerIDs.Contains(oldCustomerID))
                {
                    throw new Formula.Exceptions.BusinessException("付款方【" + customerName + "】已经有到款信息,不能删除");
                }
            }
        }

        protected override void OnFlowEnd(T_C_ContractChange entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            string tmplCode = Request["TmplCode"];
            var formInfo = baseEntities.Set<S_UI_Form>().SingleOrDefault(c => c.Code == tmplCode);
            if (formInfo == null) throw new Exception("没找到编号为【" + tmplCode + "】的表单定义");
            var sqlContract = @"select * from S_C_ManageContract where ID = '{0}'";
            var sqlChange = @"select * from T_C_ContractChange where ID = '{0}'";
            var contarct = MarketSQLDB.ExecuteDataTable(string.Format(sqlContract, entity.ContractID));
            var change = MarketSQLDB.ExecuteDataTable(string.Format(sqlChange, entity.ID));
            var subTableFields = JsonHelper.ToList(formInfo.Items).Where(c => c.GetValue("ItemType") == "SubTable").ToList();
            foreach (var item in subTableFields)
            {
                var contractTableName = "S_C_ManageContract" + "_" + item.GetValue("Code");
                var subTableName = formInfo.TableName + "_" + item.GetValue("Code");
                string subSql = "SELECT count(0) as TableCount FROM sysobjects WHERE name='{0}'";
                var obj = Convert.ToInt32(this.MarketSQLDB.ExecuteScalar(String.Format(subSql, contractTableName)));
                if (obj <= 0)
                {
                    continue;
                }
                if (item.GetValue("Code") == "ReceiptObj")
                {
                    #region 单独更新收款项（特殊逻辑）
                    subSql = "select * from {0} where {1}='{2}'";
                    var data = this.MarketSQLDB.ExecuteDataTable(String.Format(subSql, subTableName, formInfo.TableName + "ID", entity.ID));
                    var orlIds = string.Empty;
                    foreach (DataRow subItem in data.Rows)
                    {
                        var dic = Formula.FormulaHelper.DataRowToDic(subItem);
                        var receiptObj = this.GetEntityByID<S_C_ManageContract_ReceiptObj>(dic.GetValue("OrlID"));
                        if (receiptObj != null)
                        {
                            FormulaHelper.UpdateEntity<S_C_ManageContract_ReceiptObj>(receiptObj, dic, false);
                            receiptObj.ResetPlan();
                        }
                        else
                        {
                            receiptObj = new S_C_ManageContract_ReceiptObj();
                            FormulaHelper.UpdateEntity<S_C_ManageContract_ReceiptObj>(receiptObj, dic, false);
                            var contract = this.GetEntityByID<S_C_ManageContract>(entity.ContractID);
                            if (contract == null) throw new Formula.Exceptions.BusinessValidationException("不存在ID为【" + entity.ContractID + "】的合同，任务执行失败");
                            receiptObj.ID = FormulaHelper.CreateGuid();
                            receiptObj.S_C_ManageContractID = contract.ID;
                            receiptObj.S_C_ManageContract = contract;
                            contract.S_C_ManageContract_ReceiptObj.Add(receiptObj);
                            receiptObj.ResetPlan();                            
                        }
                        receiptObj.SyncSupplementary();
                        orlIds += dic.GetValue("OrlID") + ",";
                    }
                    orlIds = orlIds.TrimEnd(',');
                    var removeObjList = this.BusinessEntities.Set<S_C_ManageContract_ReceiptObj>().Where(c => c.S_C_ManageContractID == entity.ContractID
                        && !orlIds.Contains(c.ID)).ToList();
                    foreach (var removeObj in removeObjList)
                    {
                        removeObj.Delete();
                    }
                    this.BusinessEntities.SaveChanges();
                    #endregion
                }
                else
                {
                    subSql = "delete from {0} where {1}='{2}'";
                    this.MarketSQLDB.ExecuteNonQuery(String.Format(subSql, contractTableName, "S_C_ManageContract" + "ID", entity.ContractID));
                    subSql = "select * from {0} where {1}='{2}'";
                    var data = this.MarketSQLDB.ExecuteDataTable(String.Format(subSql, subTableName, formInfo.TableName + "ID", entity.ID));
                    foreach (DataRow subItem in data.Rows)
                    {
                        var dic = Formula.FormulaHelper.DataRowToDic(subItem);
                        dic.SetValue("ID", FormulaHelper.CreateGuid());
                        dic.SetValue("S_C_ManageContractID", entity.ContractID);
                        dic.InsertDB(this.MarketSQLDB, contractTableName);
                    }
                }
            }
            var synchList = new List<string>();
            foreach (var column in change.Columns)
                if (!noNeedSynch.Contains(column.ToString()) && contarct.Columns.Contains(column.ToString()))
                    synchList.Add(column.ToString());
            var sql = @"update S_C_ManageContract set {0} where ID = '{1}'";
            var setStr = " ";
            foreach (var column in synchList)
            {
                setStr += String.Format("{0} = '{1}',", column, change.Rows[0][column]);
            }
            sql = string.Format(sql, setStr.TrimEnd(','), entity.ContractID);
            MarketSQLDB.ExecuteNonQuery(sql);

            #region 自动同步核算项目
            var dt =MarketSQLDB.ExecuteDataTable(string.Format(sqlContract, entity.ContractID));
            if (dt.Rows.Count > 0)
            {
                var contractDic = FormulaHelper.DataRowToDic(dt.Rows[0]);
                Expenses.Logic.CBSInfoFO.SynCBSInfo(contractDic, Expenses.Logic.SetCBSOpportunity.Contract);
            }
            #endregion
        }

        public JsonResult RemoveReceiptObj(string ReceiptData)
        {
            var list = JsonHelper.ToList(ReceiptData);
            foreach (var item in list)
            {
                var FactInvoiceValue = String.IsNullOrEmpty(item.GetValue("FactInvoiceValue")) ? 0m : Convert.ToDecimal(item.GetValue("FactInvoiceValue"));
                var FactReceiptValue = String.IsNullOrEmpty(item.GetValue("FactReceiptValue")) ? 0m : Convert.ToDecimal(item.GetValue("FactReceiptValue"));
                var FactBadValue = String.IsNullOrEmpty(item.GetValue("FactBadValue")) ? 0m : Convert.ToDecimal(item.GetValue("FactBadValue"));
                if (FactInvoiceValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + item.GetValue("Name") + "】已经有开票信息，无法进行删除");
                if (FactReceiptValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + item.GetValue("Name") + "】已经有收款信息，无法进行删除");
                if (FactBadValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + item.GetValue("Name") + "】已经有坏账金额，无法进行删除");
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult GetProjectInfoListByEngineeringID(string EngineeringInfoID)
        {
            var list = this.BusinessEntities.Set<S_I_Project>().Where(d => d.EngineeringInfo == EngineeringInfoID).ToList();
            return Json(list);
        }

    }
}
