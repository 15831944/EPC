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

namespace Expenses.Areas.Schema.Controllers
{
    public class CBSInfoSchemaController : ExpensesFormController<S_EP_CBSInfoSchema>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            string CBSNodeInfoStr = dic.GetValue("CBSNodeInfo");
            if (!string.IsNullOrEmpty(CBSNodeInfoStr))
            {
                var detailDicList = JsonHelper.ToList(CBSNodeInfoStr);
                decimal cbsNodeInfoContractTotalValue = 0;
                foreach (var detailDic in detailDicList)
                {
                    decimal tmp = 0;
                    decimal.TryParse(detailDic.GetValue("ContractValue"), out tmp);
                    cbsNodeInfoContractTotalValue += tmp;
                }

                decimal contractValue = 0;
                decimal.TryParse(dic.GetValue("ContractValue"), out contractValue);
                if (cbsNodeInfoContractTotalValue > contractValue)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("分解合同金额不能大于{0}",  contractValue));
                }
            }
        }
        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            if (isNew)
            {
                var cbsInfoID = this.GetQueryString("CBSInfoID");
                if (String.IsNullOrEmpty(cbsInfoID))
                {
                    throw new Formula.Exceptions.BusinessValidationException("");
                }
                var sql = String.Format(@"select ID as CBSNodeID,Name,Code,ParentID as CBSParentID,RelateID as CBSRelateID,
NodeType,ContractValue,TaxRate,SortIndex,
ContractTaxValue,ContractClearValue,CostValue,ChargerUser,ChargerUserName,DefineID as CBSDefineID,
ChargerDept,ChargerDeptName,FullID as CBSNodeFullID,'{1}' as ModifyState  from S_EP_CBSNode
where CBSInfoID='{0}' and NodeType!='Subject' and ParentID is not null and ParentID <>''", cbsInfoID, ModifyState.Normal.ToString());
                var nodeDt = this.SQLDB.ExecuteDataTable(sql);
                dt.Rows[0]["CBSNodeInfo"] = JsonHelper.ToJson(nodeDt);
            }
        }

        protected override void OnFlowEnd(S_EP_CBSInfoSchema data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }

        public JsonResult CreateEmptyNode(string ParentNode, string CBSInfoID)
        {
            var cbsInfo = this.GetDataDicByID("S_EP_CBSInfo", CBSInfoID);
            if (cbsInfo == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有获取到核算项目信息，无法新增节点");
            }
            var cbsNodeDefineDt = this.InfrasDB.ExecuteDataTable(String.Format(@"select ID as NodeDefineID,NodeType,CBSType,ParentID 
from S_EP_DefineCBSNode where DefineID='{0}' and IsDynamic='{1}'", cbsInfo.GetValue("CBSDefineInfoID"), "true"));
            var result = new Dictionary<string, object>();
            if (!String.IsNullOrEmpty(ParentNode))
            {
                var parent = JsonHelper.ToObject(ParentNode);
                result.SetValue("CBSNodeID", FormulaHelper.CreateGuid());
                var node = cbsNodeDefineDt.AsEnumerable().FirstOrDefault(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value &&
                    c["ParentID"].ToString() == parent.GetValue("CBSDefineID"));
                if (node == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("【" + parent.GetValue("Name") + "】下不允许增加新节点");
                }
                result.SetValue("NodeType", node["NodeType"]);
                result.SetValue("Name", "新增节点");
                result.SetValue("CBSParentID", parent.GetValue("CBSNodeID"));
                result.SetValue("ChargerUser", parent.GetValue("ChargerUser"));
                result.SetValue("ChargerUserName", parent.GetValue("ChargerUserName"));
                result.SetValue("ChargerDept", parent.GetValue("ChargerDept"));
                result.SetValue("ChargerDeptName", parent.GetValue("ChargerDeptName"));
                result.SetValue("CBSNodeFullID", parent.GetValue("CBSNodeFullID") + "." + result.GetValue("CBSNodeID"));
                result.SetValue("CBSDefineID", node["NodeDefineID"]);
                result.SetValue("ModifyState", ModifyState.Added.ToString());
            }
            else
            {
                var rootNodeDt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode where CBSInfoID='{0}' and (ParentID is null or ParentID='')",
                     cbsInfo.GetValue("ID")));
                if (rootNodeDt.Rows.Count == 0 || rootNodeDt.Rows[0]["DefineID"] == null || rootNodeDt.Rows[0]["DefineID"] == DBNull.Value)
                {
                    throw new Formula.Exceptions.BusinessValidationException("");
                }
                var nodeDefineID = rootNodeDt.Rows[0]["DefineID"].ToString();
                var node = cbsNodeDefineDt.AsEnumerable().FirstOrDefault(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value &&
                      c["ParentID"].ToString() == nodeDefineID);
                if (node == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("【不允许增加新节点");
                }
                result.SetValue("CBSNodeID", FormulaHelper.CreateGuid());
                result.SetValue("NodeType", node["NodeType"]);
                result.SetValue("Name", "新增节点");
                result.SetValue("ChargerUser", rootNodeDt.Rows[0]["ChargerUser"]);
                result.SetValue("ChargerUserName", rootNodeDt.Rows[0]["ChargerUserName"]);
                result.SetValue("ChargerDept", rootNodeDt.Rows[0]["ChargerDept"]);
                result.SetValue("ChargerDeptName", rootNodeDt.Rows[0]["ChargerDeptName"]);
                result.SetValue("CBSParentID", rootNodeDt.Rows[0]["ID"]);
                result.SetValue("CBSNodeFullID", rootNodeDt.Rows[0]["FullID"].ToString() + "." + result.GetValue("CBSNodeID"));
                result.SetValue("CBSDefineID", node["NodeDefineID"]);
                result.SetValue("ModifyState", ModifyState.Added.ToString());
            }
            return Json(result);
        }

        public JsonResult GetNodeType(string ParentDefineID, string CBSInfoID)
        {
            var cbsInfo = this.GetDataDicByID("S_EP_CBSInfo", CBSInfoID);
            if (cbsInfo == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有获取到核算项目信息");
            }
            var cbsNodeDefineDt = this.SQLDB.ExecuteDataTable(String.Format(@"select ID as NodeDefineID,Name,NodeType,CBSType,ParentID 
from S_EP_DefineCBSNode where DefineID='{0}' and IsDynamic='{1}'", cbsInfo.GetValue("CBSDefineInfoID"), "true"));
            if (!String.IsNullOrEmpty(ParentDefineID))
            {
                var list = cbsNodeDefineDt.AsEnumerable().Where(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value
                     && c["ParentID"].ToString() == ParentDefineID).Select(c => new { value = c["NodeType"], text = c["Name"], DefineID = c["NodeDefineID"] }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rootNodeDt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode where CBSInfoID='{0}' and (ParentID is null or ParentID='')",
                    cbsInfo.GetValue("ID")));
                if (rootNodeDt.Rows.Count == 0 || rootNodeDt.Rows[0]["DefineID"] == null || rootNodeDt.Rows[0]["DefineID"] == DBNull.Value)
                {
                    throw new Formula.Exceptions.BusinessValidationException("");
                }
                var nodeDefineID = rootNodeDt.Rows[0]["DefineID"].ToString();
                var list = cbsNodeDefineDt.AsEnumerable().Where(c => c["ParentID"] != null && c["ParentID"] != DBNull.Value
                     && c["ParentID"].ToString() == nodeDefineID).Select(c => new { value = c["NodeType"], text = c["Name"], DefineID = c["NodeDefineID"] }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpgradeSchema(string CBSInfoID)
        {
            var sql = "select ID from S_EP_CBSInfoSchema where FlowPhase <> '{0}' and CBSInfoID='{1}'";
            var dt = this.SQLDB.ExecuteDataTable(String.Format(sql, "End", CBSInfoID));
            if (dt.Rows.Count > 0)
            {
                return Json(new { ID = dt.Rows[0]["ID"] });
            }
            else
            {

                return Json(new { ID = "" });
            }
        }

    }
}
