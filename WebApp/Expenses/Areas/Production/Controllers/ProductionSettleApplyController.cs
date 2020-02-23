using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using Expenses.Logic.Domain;
using Config;

namespace Expenses.Areas.Production.Controllers
{
    public class ProductionSettleApplyController : ExpensesFormController<S_EP_ProductionSettleApply>
    {
        public JsonResult GetWBSUserInfo(string ListData,string CBSNodeID)
        {
            var list = JsonHelper.ToList(ListData);
            var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", CBSNodeID);
            var wbsIds = cbsNodeDic.GetValue("RelateID");
            var sql = String.Format(@"select distinct WBSID,UserID,UserName,RoleCode,RoleName From S_W_RBS with(nolock)
inner join S_W_WBS  with(nolock) on S_W_RBS.WBSID=S_W_WBS.ID
where S_W_WBS.FullID like '%{0}%'", wbsIds);
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            var dt = db.ExecuteDataTable(sql);
            var userList = dt.AsEnumerable().Select(c => new
            {
                UserID = c["UserID"].ToString(),
                UserName = c["UserName"].ToString(),
                WBSID = c["WBSID"].ToString()
            }).Distinct().ToList();
            var result = new List<Dictionary<string, object>>();
            foreach (var user in userList)
            {
                var userInfo = new Dictionary<string, object>();
                userInfo.SetValue("UserInfo", user.UserID);
                userInfo.SetValue("UserInfoName", user.UserName);
                var roleCodeList = dt.AsEnumerable().Where(c => c["RoleCode"] != DBNull.Value && c["UserID"].ToString() == user.UserID &&
                    c["WBSID"].ToString() == user.WBSID).Select(c => c["RoleCode"].ToString()).ToList();
                var roleNameList = dt.AsEnumerable().Where(c => c["RoleName"] != DBNull.Value && c["UserID"].ToString() == user.UserID &&
                  c["WBSID"].ToString() == user.WBSID).Select(c => c["RoleName"].ToString()).ToList();
                userInfo.SetValue("RoleCode", String.Join(",", roleCodeList));
                userInfo.SetValue("RoleName", String.Join(",", roleNameList));
                var node = list.FirstOrDefault(c => c.GetValue("RelateID") == user.WBSID);
                if (node != null)
                {
                    userInfo.SetValue("ParentCode", node.GetValue("Code"));
                    userInfo.SetValue("ParentNode", node.GetValue("CBSNode"));
                    userInfo.SetValue("ParentID", node.GetValue("CBSNodeID"));
                }
                result.Add(userInfo);
            }
            return Json(result);
        }

        public JsonResult ValidateSettle(string NodeID)
        {
            var nodeDic = this.GetDataDicByID("S_EP_CBSNode", NodeID);
            if (nodeDic == null) throw new Formula.Exceptions.BusinessValidationException("未能找到指定的节点信息，无法启动产值确认");

            var obj = this.SQLDB.ExecuteScalar(String.Format("select count(*) from S_EP_ProductionUnit where CBSNodeID='{0}'", NodeID));
            if (Convert.ToInt32(obj) == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("您选中的节点【{0}】不是产值节点，无法确认产值", nodeDic.GetValue("Name")));
            }

            var dt = this.SQLDB.ExecuteDataTable(String.Format("select ID from S_EP_ProductionSettleApply where CBSNode='{0}' and FlowPhase='Start'", NodeID));
            if (dt.Rows.Count > 0)
            {
                return Json(new { ApplyFormID = dt.Rows[0]["ID"].ToString() });
            }
            obj = this.SQLDB.ExecuteScalar(String.Format("select count(ID) from S_EP_ProductionSettleApply where CBSNode='{0}' and FlowPhase<>'End'", NodeID));
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("所选节点存在未审批完成的产值确认单，无法再次确认");
            }

            string sql = String.Format(@"select Count(*) from S_EP_ProductionBatchSettleApply_Detail
inner join S_EP_ProductionBatchSettleApply on S_EP_ProductionBatchSettleApplyID=S_EP_ProductionBatchSettleApply.ID
where CBSNodeID='{0}' and S_EP_ProductionBatchSettleApply.FlowPhase<>'End'", NodeID);
            obj = this.SQLDB.ExecuteScalar(sql);
            if (Convert.ToInt32(obj) > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("所选节点存在未审批完成的产值确认单，无法再次确认");
            }
            return Json("");
        }

        protected override void BeforeSaveDetail(Dictionary<string, string> dic, string subTableName, Dictionary<string, string> detail, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "DetailInfo")
            {
                var detailJson = JsonHelper.ToJson(detail);
                detail.SetValue("Data", detailJson);
            }
            if (subTableName == "UserInfo")
            {
                var detailJson = JsonHelper.ToJson(detail);
                detail.SetValue("Data", detailJson);
            }
        }

        protected override void BeforeSaveSubTable(Dictionary<string, string> dic, string subTableName, List<Dictionary<string, string>> detailList, Base.Logic.Domain.S_UI_Form formInfo)
        {
            if (subTableName == "DetailInfo" && detailList.Count > 0)
            {
                var sumSettleValue = detailList.Where(c => !String.IsNullOrEmpty(c.GetValue("SettleValue"))).Sum(c => Convert.ToDecimal(c.GetValue("SettleValue")));
                var settleValue = String.IsNullOrEmpty(dic.GetValue("CurrentSettleValue")) ? 0m : Convert.ToDecimal(dic.GetValue("CurrentSettleValue"));
                if (sumSettleValue != settleValue)
                {
                    throw new Formula.Exceptions.BusinessException("确认产值的明细总和必须等于当期确认产值");
                }
            }
            else if (subTableName == "UserInfo" && detailList.Count > 0)
            {
                var sumSettleValue = detailList.Where(c => !String.IsNullOrEmpty(c.GetValue("SettleValue"))).Sum(c => Convert.ToDecimal(c.GetValue("SettleValue")));
                var settleValue = String.IsNullOrEmpty(dic.GetValue("CurrentSettleValue")) ? 0m : Convert.ToDecimal(dic.GetValue("CurrentSettleValue"));
                if (sumSettleValue != settleValue)
                {
                    throw new Formula.Exceptions.BusinessException("人员明细的确认产值总和必须等于当期确认产值");
                }
            }
        }

        protected override void OnFlowEnd(S_EP_ProductionSettleApply data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }
    }
}
