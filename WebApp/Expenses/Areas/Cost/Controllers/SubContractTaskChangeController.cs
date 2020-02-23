using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
using Base.Logic.Domain;


namespace Expenses.Areas.Cost.Controllers
{
    public class SubContractTaskChangeController : ExpensesFormController<S_EP_SubContractTaskChange>
    {
        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            string tmplCode = Request["TmplCode"];
            var formInfo = baseEntities.Set<S_UI_Form>().SingleOrDefault(c => c.Code == tmplCode);
            if (formInfo == null) throw new Exception("没有找到编号为【" + tmplCode + "】的表单定义");
            var items = JsonHelper.ToList(formInfo.Items).Where(c => c.GetValue("ItemType") == "SubTable").ToList();
            if (isNew)
            {
                string contractID = this.GetQueryString("TaskID");
                foreach (var item in items)
                {
                    var tableName = formInfo.TableName + "_" + item.GetValue("Code");
                    var contractTableName = "S_EP_SubContractTask" + "_" + item.GetValue("Code");
                    string sql = "SELECT count(0) as TableCount FROM sysobjects WHERE name='{0}'";
                    var obj = Convert.ToInt32(this.SQLDB.ExecuteScalar(String.Format(sql, contractTableName)));
                    if (obj > 0)
                    {
                        sql = "Select * from {0} where S_EP_SubContractTaskID='{1}'";
                        var subTable = this.SQLDB.ExecuteDataTable(String.Format(sql, contractTableName, contractID));
                        var subTableData = new List<Dictionary<string, object>>();
                        foreach (DataRow subItem in subTable.Rows)
                        {
                            var dic = Formula.FormulaHelper.DataRowToDic(subItem);
                            dic.SetValue("ID", "");
                            dic.SetValue("OrlID", subItem["ID"]);
                            dic.SetValue("SubTaskDetailID", subItem["ID"]);
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

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            if (isNew)
            {
                if (String.IsNullOrEmpty(dic.GetValue("SubContractTaskID")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("必须选择一个委外任务才能进行变更，请确认您是否关联了委外任务，因为委外任务ID为空值");
                }
                var lastVersionData = this.GetDataDicByID("S_EP_SubContractTask", dic.GetValue("SubContractTaskID"), Config.ConnEnum.Market, true);
                dic.SetValue("LastVersionData", JsonHelper.ToJson(lastVersionData));
            }
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, S_UI_Form formInfo)
        {
            if (subTableName == "CostUnitDetail")
            {
                var detailID = detail.GetValue("SubTaskDetailID");
                string sql = String.Format(@"select isnull(sum(CurrentValue),0) from S_EP_SubContractProgressConfirm where TaskDetailID='{0}' and FlowPhase<>'{1}'", detailID, "Start");
                var obj = this.SQLDB.ExecuteScalar(sql);
                var contractValue = String.IsNullOrEmpty(detail.GetValue("SubContractValue")) ? 0m : Convert.ToDecimal(detail.GetValue("SubContractValue"));
                if (obj != null && obj != DBNull.Value)
                {
                    if (contractValue < Convert.ToDecimal(obj))
                    {
                        throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】的分包金额不能小于【{1}】", detail.GetValue("CostUnitName"), obj));
                    }
                }
                var taxRate = String.IsNullOrEmpty(dic.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(dic.GetValue("TaxRate"));
                var taxValue = contractValue / (1 + taxRate) * taxRate;
                var clearValue = contractValue - taxValue;
                detail.SetValue("TaxValue", taxValue.ToString());
                detail.SetValue("ClearValue", clearValue.ToString());
            }
        }

        protected override void OnFlowEnd(S_EP_SubContractTaskChange data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult ValidateStart(string ID)
        {
            var dic = this.GetDataDicByID("S_EP_SubContractTask", ID);
            if (dic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的分包任务，无法变更");
            if (dic.GetValue("FlowPhase") != "End")
            {
                throw new Formula.Exceptions.BusinessValidationException("任务尚未审批完成，无法变更");
            }
            string sql = String.Format("select ID from S_EP_SubContractTaskChange where SubContractTaskID='{0}' and FlowPhase='Start'", ID);
            var dt = this.SQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                return Json(new { ID = dt.Rows[0]["ID"].ToString() });
            }
            else
            {
                sql = String.Format("select count(ID) from S_EP_SubContractTaskChange where SubContractTaskID='{0}' and FlowPhase!='End'", ID);
                var obj = this.SQLDB.ExecuteScalar(sql);
                if (Convert.ToInt32(obj) > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("尚有未审批完的变更申请记录，无法重复变更");
                }
            }
            return Json("");
        }

        public JsonResult ValidateRemove(string ListData, string TaskID)
        {
            var list = JsonHelper.ToList(ListData);
            foreach (var item in list)
            {
                string detailID = item.GetValue("SubTaskDetailID");
                if (String.IsNullOrEmpty(detailID))
                    continue;
                var taskDetail = this.GetDataDicByID("S_EP_SubContractTask_CostUnitDetail", detailID, Config.ConnEnum.Market, true);
                if (taskDetail == null) continue;
                var sql = String.Format(@"select count(ID) from S_EP_SubContractProgressConfirm where TaskDetailID='{0}'", detailID);
                var obj = Convert.ToInt32(this.SQLDB.ExecuteScalar(sql));
                if (obj > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】已经确认了成本，不能删除", taskDetail.GetValue("CostUnitName")));
                }
            }
            return Json("");
        }
    }
}
