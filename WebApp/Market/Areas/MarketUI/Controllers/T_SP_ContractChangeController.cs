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
using Formula.Exceptions;

namespace Market.Areas.MarketUI.Controllers
{
    public class SPContractChangeController : MarketFormContorllor<T_SP_ContractChange>
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
                    var contractTableName = "S_SP_SupplierContract" + "_" + item.GetValue("Code");
                    string sql = "SELECT count(0) as TableCount FROM sysobjects WHERE name='{0}'";
                    var obj = Convert.ToInt32(this.MarketSQLDB.ExecuteScalar(String.Format(sql, contractTableName)));
                    if (obj > 0)
                    {
                        sql = "Select * from {0} where S_SP_SupplierContractID='{1}'";
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

        protected override void BeforeSave(Dictionary<string, string> dic, S_UI_Form formInfo, bool isNew)
        {
            string sql = "select count(0) from S_SP_SupplierContract  where SerialNumber ='{0}' and ID!='{1}'";
            var obj = this.MarketSQLDB.ExecuteScalar(String.Format(sql, dic.GetValue("SerialNumber"), dic.GetValue("ContractID")));
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("合同编号【" + dic.GetValue("SerialNumber") + "】已经存在，不能重复，请重新编号");
            }
            #region 供应商修改时，要校验是否有发票和付款
            string contractID = dic.GetValue("ContractID");
            var oldContract = BusinessEntities.Set<S_SP_SupplierContract>().Find(contractID);
            string oldSupplierID = oldContract.Supplier ?? "";
            string oldSupplierName = oldContract.SupplierName ?? "";
            if (BusinessEntities.Set<S_SP_Invoice>().Any(a => a.Supplier == oldSupplierID && a.SupplierContract == contractID) && oldSupplierID != dic.GetValue("Supplier"))
            {
                throw new Formula.Exceptions.BusinessException("供应商【" + oldSupplierName + "】已经有发票登记信息,不能修改供应商");
            }
            else if (BusinessEntities.Set<T_SP_PaymentApply>().Any(a => a.Supplier == oldSupplierID && a.Contract == contractID)
                && oldSupplierID != dic.GetValue("Supplier"))
            {
                throw new Formula.Exceptions.BusinessException("付款方【" + oldSupplierName + "】已经有付款申请信息,不能修改供应商");
            }
            else if (BusinessEntities.Set<S_SP_Payment>().Any(a => a.Supplier == oldSupplierID && a.Contract == contractID)
                && oldSupplierID != dic.GetValue("Supplier"))
            {
                throw new Formula.Exceptions.BusinessException("付款方【" + oldSupplierName + "】已经有付款信息,不能修改供应商");
            }
            #endregion
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "ContractSplit")
            {
                var splitValue = 0m;
                decimal.TryParse(detail.GetValue("SplitValue"), out splitValue);
                if (splitValue <= 0)
                {
                    throw new BusinessValidationException("【委外合同金额】必须大于0！");
                }

                var sql = string.Format("select top 1 * from S_EP_SupplierContractConfirm where SubContractDetailID='{0}' order by ID desc", detail.GetValue("OrlID"));
                var dt = MarketSQLDB.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    var supplierContractConfirmInfo = FormulaHelper.DataRowToDic(dt.Rows[0]);
                    var totalConfirmValue = 0m;
                    decimal.TryParse(supplierContractConfirmInfo.GetValue("TotalValue"), out totalConfirmValue);
                    if (splitValue <= totalConfirmValue)
                    {
                        throw new BusinessValidationException(string.Format("【{0}】的委外金额不能小于【{1}】", detail.GetValue("Name"), totalConfirmValue));
                    }
                    double totalProgress = 0;
                    double.TryParse(supplierContractConfirmInfo.GetValue("TotalProgress"), out totalProgress);
                    if (totalProgress >= 1 && splitValue != totalConfirmValue)
                    {
                        throw new BusinessValidationException("委外合同已经确认至100%，【委外合同金额】不能变更！");
                    }
                }

            }
        }


        protected override void OnFlowEnd(T_SP_ContractChange entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            string tmplCode = Request["TmplCode"];
            var formInfo = baseEntities.Set<S_UI_Form>().SingleOrDefault(c => c.Code == tmplCode);
            if (formInfo == null) throw new Exception("没找到编号为【" + tmplCode + "】的表单定义");
            var sqlContract = @"select * from S_SP_SupplierContract where ID = '{0}'";
            var sqlChange = @"select * from T_SP_ContractChange where ID = '{0}'";
            var contract = MarketSQLDB.ExecuteDataTable(string.Format(sqlContract, entity.ContractID));
            var change = MarketSQLDB.ExecuteDataTable(string.Format(sqlChange, entity.ID));
            var subTableFields = JsonHelper.ToList(formInfo.Items).Where(c => c.GetValue("ItemType") == "SubTable").ToList();
            foreach (var item in subTableFields)
            {
                var contractTableName = "S_SP_SupplierContract" + "_" + item.GetValue("Code");
                var subTableName = formInfo.TableName + "_" + item.GetValue("Code");
                string subSql = "SELECT count(0) as TableCount FROM sysobjects WHERE name='{0}'";
                var obj = Convert.ToInt32(this.MarketSQLDB.ExecuteScalar(String.Format(subSql, contractTableName)));
                if (obj <= 0)
                {
                    continue;
                }
                subSql = "delete from {0} where {1}='{2}'";
                this.MarketSQLDB.ExecuteNonQuery(String.Format(subSql, contractTableName, "S_SP_SupplierContract" + "ID", entity.ContractID));
                subSql = "select * from {0} where {1}='{2}'";
                var data = this.MarketSQLDB.ExecuteDataTable(String.Format(subSql, subTableName, formInfo.TableName + "ID", entity.ID));
                foreach (DataRow subItem in data.Rows)
                {
                    var dic = Formula.FormulaHelper.DataRowToDic(subItem);
                    if (subItem["OrlID"] == null || subItem["OrlID"] == DBNull.Value || String.IsNullOrEmpty(subItem["OrlID"].ToString()))
                    {
                        //表示是新增
                        dic.SetValue("ID", FormulaHelper.CreateGuid());
                    }
                    else
                    {
                        dic.SetValue("ID", subItem["OrlID"]);
                    }
                    dic.SetValue("S_SP_SupplierContractID", entity.ContractID);
                    dic.InsertDB(this.MarketSQLDB, contractTableName, dic.GetValue("ID"));
                }
            }
            var synchList = new List<string>();
            foreach (var column in change.Columns)
                if (!noNeedSynch.Contains(column.ToString()) && contract.Columns.Contains(column.ToString()))
                    synchList.Add(column.ToString());
            var sql = @"update S_SP_SupplierContract set {0} where ID = '{1}'";
            var setStr = " ";
            foreach (var column in synchList)
            {
                if (column == "SerialNumber")
                {
                    setStr += String.Format("{0} = '{1}',", "SerialNumber", change.Rows[0]["ContractCode"]);
                    continue;
                }
                setStr += String.Format("{0} = '{1}',", column, change.Rows[0][column]);
            }

            sql = string.Format(sql, setStr.TrimEnd(','), entity.ContractID);
            MarketSQLDB.ExecuteNonQuery(sql);
        }

        public JsonResult ValidateDeleteDetail(string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            foreach (var item in list)
            {
                var obj = this.MarketSQLDB.ExecuteScalar(String.Format(@"SELECT COUNT(ID) FROM S_EP_SupplierContractConfirm 
WHERE SubContractDetailID='{0}'", item.GetValue("OrlID")));
                if (Convert.ToInt32(obj) > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(
                        String.Format(String.Format(@"【{0}】已经确认过进度，不能删除", item.GetValue("Name"))));
                }
            }
            return Json("");
        }
    }
}
