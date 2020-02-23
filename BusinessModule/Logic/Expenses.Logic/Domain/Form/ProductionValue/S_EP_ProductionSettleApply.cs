using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using System.Data;

namespace Expenses.Logic.Domain
{
    public class S_EP_ProductionSettleApply : BaseEPModel
    {
        public void Push()
        {
            var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", this.GetValue("CBSNode"));
            if (cbsNodeDic == null)
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的CBS节点，无法确认产值");
            var cbsNode = new S_EP_CBSNode(cbsNodeDic);
            if (String.IsNullOrEmpty(this.GetValue("SettleDate")))
            {
                throw new Formula.Exceptions.BusinessValidationException("没有产值确认日期，无法申请");
            }
            var productionDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_ProductionUnit WHERE CBSNodeID='{0}'", cbsNodeDic.GetValue("ID")));
            if (productionDt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到指定的产值单元，无法确认产值");
            }
            var productionDic = FormulaHelper.DataRowToDic(productionDt.Rows[0]);
            if (!String.IsNullOrEmpty(this.GetValue("ProgressNode")))
            {
                var pNode = this.GetDataDicByID("S_EP_ProductionUnit_ProgressNode", this.GetValue("ProgressNode"));
                pNode.SetValue("State", "Finish");
                pNode.SetValue("FactEndDate", Convert.ToDateTime(this.GetValue("SettleDate")));
                pNode.UpdateDB(this.DB, "S_EP_ProductionUnit_ProgressNode", pNode.GetValue("ID"));
            }


            var detailSettleInfo = this.DB.ExecuteDataTable(String.Format("select * from S_EP_ProductionSettleApply_DetailInfo where S_EP_ProductionSettleApplyID='{0}'", this.GetValue("ID")));
            if (detailSettleInfo.Rows.Count == 0)
            {
                #region 没有明细节点，结算的产值节点是最末级节点或不需要结算到下级明细
                var settleInfo = new Dictionary<string, object>();
                settleInfo.SetValue("CBSInfo", this.GetValue("RootInfo"));
                settleInfo.SetValue("CBSNodeID", cbsNodeDic.GetValue("ID"));
                settleInfo.SetValue("CBSNodeFullID", cbsNodeDic.GetValue("FullID"));
                settleInfo.SetValue("ProductionUnitID", productionDic.GetValue("ID"));
                settleInfo.SetValue("UnitName", productionDic.GetValue("Name"));
                settleInfo.SetValue("UnitCode", productionDic.GetValue("Code"));
                var lastValue = String.IsNullOrEmpty(this.GetValue("LastTotalSettleValue")) ? 0 : Convert.ToDecimal(this.GetValue("LastTotalSettleValue"));
                var planValue = String.IsNullOrEmpty(this.GetValue("ProductionValue")) ? 0 : Convert.ToDecimal(this.GetValue("ProductionValue"));
                if (planValue > 0)
                {
                    settleInfo.SetValue("LastScale", Math.Round(lastValue / planValue * 100, 2));
                }
                else
                {
                    settleInfo.SetValue("LastScale", 0);
                }
                settleInfo.SetValue("LastProductionValue", String.IsNullOrEmpty(this.GetValue("LastTotalSettleValue")) ? 0 : Convert.ToDecimal(this.GetValue("LastTotalSettleValue")));
                settleInfo.SetValue("TotalScale", String.IsNullOrEmpty(this.GetValue("TotalScale")) ? 0 : Convert.ToDecimal(this.GetValue("TotalScale")));
                settleInfo.SetValue("TotalProductionValue", String.IsNullOrEmpty(this.GetValue("TotalSettleValue")) ? 0 : Convert.ToDecimal(this.GetValue("TotalSettleValue")));
                settleInfo.SetValue("CurrentProductionValue", String.IsNullOrEmpty(this.GetValue("CurrentSettleValue")) ? 0 : Convert.ToDecimal(this.GetValue("CurrentSettleValue")));
                settleInfo.SetValue("ConfirmFormID", this.GetValue("ID"));
                settleInfo.SetValue("FactEndDate", this.GetValue("SettleDate"));
                settleInfo.SetValue("ChargerUser", cbsNodeDic.GetValue("ChargerUser"));
                settleInfo.SetValue("ChargerUserName", cbsNodeDic.GetValue("ChargerUserName"));
                settleInfo.SetValue("ChargerDept", cbsNodeDic.GetValue("ChargerUserDept"));
                settleInfo.SetValue("ChargerDeptName", cbsNodeDic.GetValue("ChargerUserDeptName"));
                settleInfo.SetValue("ApplyUser", this.GetValue("ApplyUser"));
                settleInfo.SetValue("ApplyUserName", this.GetValue("ApplyUserName"));
                settleInfo.SetValue("Company", "");
                settleInfo.SetValue("CompanyName", "");
                settleInfo.SetValue("FinishProgressNodeID", this.GetValue("ProgressNode"));
                settleInfo.SetValue("ID", FormulaHelper.CreateGuid());
                settleInfo.InsertDB(this.DB, "S_EP_ProductionSettleValue");
                #endregion
            }
            else
            {
                foreach (DataRow detailInfo in detailSettleInfo.Rows)
                {
                    var item = new Dictionary<string, object>();
                    if (detailInfo["Data"] == DBNull.Value || String.IsNullOrEmpty(detailInfo["Data"].ToString()))
                    {
                        item = FormulaHelper.DataRowToDic(detailInfo);
                    }
                    else
                    {
                        item = JsonHelper.ToObject(detailInfo["Data"].ToString());
                        item.SetValue("ID", detailInfo["ID"]);
                    }
                    var keys = item.Keys.Where(c => c.Split('_').Length == 2 && c.Split('_')[1] == "Value").ToList();
                    if (keys.Count > 0)
                    {
                        #region
                        foreach (var key in keys)
                        {
                            var keyArray = key.Split('_');
                            var detailNode = cbsNode.AllChildren.FirstOrDefault(c => c.GetValue("ParentID") == item.GetValue("CBSNodeID") && c.GetValue("DefineID") == keyArray[0]);
                            if (detailNode == null) continue;
                            var currentValue = 0m;
                            decimal.TryParse(item.GetValue(key), out currentValue);
                            var settleInfo = new Dictionary<string, object>();
                            settleInfo.SetValue("CBSInfo", this.GetValue("RootInfo"));
                            settleInfo.SetValue("CBSNodeID", detailNode.GetValue("ID"));
                            settleInfo.SetValue("CBSNodeFullID", detailNode.GetValue("FullID"));
                            settleInfo.SetValue("ProductionUnitID", productionDic.GetValue("ID"));
                            settleInfo.SetValue("UnitName", productionDic.GetValue("Name"));
                            settleInfo.SetValue("UnitCode", productionDic.GetValue("Code"));
                            var lastValue = String.IsNullOrEmpty(detailNode.GetValue("ProductionSettleValue")) ? 0 : Convert.ToDecimal(detailNode.GetValue("ProductionSettleValue"));
                            var planValue = String.IsNullOrEmpty(detailNode.GetValue("ProductionValue")) ? 0 : Convert.ToDecimal(detailNode.GetValue("ProductionValue"));
                            if (planValue > 0)
                            {
                                settleInfo.SetValue("LastScale", Math.Round(lastValue / planValue * 100, 2));
                            }
                            else
                            {
                                settleInfo.SetValue("LastScale", 0);
                            }
                            settleInfo.SetValue("LastProductionValue", lastValue);
                            settleInfo.SetValue("TotalScale", String.IsNullOrEmpty(this.GetValue("TotalScale")) ? 0 : Convert.ToDecimal(this.GetValue("TotalScale")));
                            settleInfo.SetValue("CurrentProductionValue", currentValue);
                            settleInfo.SetValue("TotalProductionValue", lastValue + currentValue);
                            settleInfo.SetValue("ConfirmFormID", this.GetValue("ID"));
                            settleInfo.SetValue("FactEndDate", this.GetValue("SettleDate"));
                            settleInfo.SetValue("ChargerUser", cbsNodeDic.GetValue("ChargerUser"));
                            settleInfo.SetValue("ChargerUserName", cbsNodeDic.GetValue("ChargerUserName"));
                            settleInfo.SetValue("ChargerDept", cbsNodeDic.GetValue("ChargerUserDept"));
                            settleInfo.SetValue("ChargerDeptName", cbsNodeDic.GetValue("ChargerUserDeptName"));
                            settleInfo.SetValue("ApplyUser", this.GetValue("ApplyUser"));
                            settleInfo.SetValue("ApplyUserName", this.GetValue("ApplyUserName"));
                            settleInfo.SetValue("Company", "");
                            settleInfo.SetValue("CompanyName", "");
                            settleInfo.SetValue("FinishProgressNodeID", this.GetValue("ProgressNode"));
                            settleInfo.SetValue("ID", FormulaHelper.CreateGuid());
                            settleInfo.InsertDB(this.DB, "S_EP_ProductionSettleValue");
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        var detailNode = cbsNode.AllChildren.FirstOrDefault(c => c.GetValue("ID") == item.GetValue("CBSNodeID"));
                        if (detailNode == null) continue;
                        var currentValue = 0m;
                        decimal.TryParse(item.GetValue("SettleValue"), out currentValue);
                        var settleInfo = new Dictionary<string, object>();
                        settleInfo.SetValue("CBSInfo", this.GetValue("RootInfo"));
                        settleInfo.SetValue("CBSNodeID", detailNode.GetValue("ID"));
                        settleInfo.SetValue("CBSNodeFullID", detailNode.GetValue("FullID"));
                        settleInfo.SetValue("ProductionUnitID", productionDic.GetValue("ID"));
                        settleInfo.SetValue("UnitName", productionDic.GetValue("Name"));
                        settleInfo.SetValue("UnitCode", productionDic.GetValue("Code"));
                        var lastValue = String.IsNullOrEmpty(detailNode.GetValue("ProductionSettleValue")) ? 0 : Convert.ToDecimal(detailNode.GetValue("ProductionSettleValue"));
                        var planValue = String.IsNullOrEmpty(detailNode.GetValue("ProductionValue")) ? 0 : Convert.ToDecimal(detailNode.GetValue("ProductionValue"));
                        if (planValue > 0)
                        {
                            settleInfo.SetValue("LastScale", Math.Round(lastValue / planValue * 100, 2));
                        }
                        else
                        {
                            settleInfo.SetValue("LastScale", 0);
                        }
                        settleInfo.SetValue("LastProductionValue", lastValue);
                        settleInfo.SetValue("TotalScale", String.IsNullOrEmpty(this.GetValue("TotalScale")) ? 0 : Convert.ToDecimal(this.GetValue("TotalScale")));
                        settleInfo.SetValue("CurrentProductionValue", currentValue);
                        settleInfo.SetValue("TotalProductionValue", lastValue + currentValue);
                        settleInfo.SetValue("ConfirmFormID", this.GetValue("ID"));
                        settleInfo.SetValue("FactEndDate", this.GetValue("SettleDate"));
                        settleInfo.SetValue("ChargerUser", cbsNodeDic.GetValue("ChargerUser"));
                        settleInfo.SetValue("ChargerUserName", cbsNodeDic.GetValue("ChargerUserName"));
                        settleInfo.SetValue("ChargerDept", cbsNodeDic.GetValue("ChargerUserDept"));
                        settleInfo.SetValue("ChargerDeptName", cbsNodeDic.GetValue("ChargerUserDeptName"));
                        settleInfo.SetValue("ApplyUser", this.GetValue("ApplyUser"));
                        settleInfo.SetValue("ApplyUserName", this.GetValue("ApplyUserName"));
                        settleInfo.SetValue("Company", "");
                        settleInfo.SetValue("CompanyName", "");
                        settleInfo.SetValue("FinishProgressNodeID", this.GetValue("ProgressNode"));
                        settleInfo.SetValue("ID", FormulaHelper.CreateGuid());
                        settleInfo.InsertDB(this.DB, "S_EP_ProductionSettleValue");
                        #endregion
                    }
                }
            }

            var userInfoList = this.DB.ExecuteDataTable(String.Format("select * from S_EP_ProductionSettleApply_UserInfo where S_EP_ProductionSettleApplyID='{0}'", this.GetValue("ID")));
            foreach (DataRow userInfo in userInfoList.Rows)
            {
                var item = new Dictionary<string, object>();
                if (userInfo["Data"] == DBNull.Value || String.IsNullOrEmpty(userInfo["Data"].ToString()))
                {
                    item = FormulaHelper.DataRowToDic(userInfo);
                }
                else
                {
                    item = JsonHelper.ToObject(userInfo["Data"].ToString());
                    item.SetValue("ID", userInfo["ID"]);
                }
                var keys = item.Keys.Where(c => c.Split('_').Length == 2 && c.Split('_')[1] == "Value").ToList();
                if (keys.Count > 0)
                {
                    foreach (var key in keys)
                    {
                        var defineID = key.Split('_')[0];
                        var detailNode = cbsNode.AllChildren.FirstOrDefault(c => c.GetValue("ParentID") == item.GetValue("ParentID") && c.GetValue("DefineID") == defineID);
                        if (detailNode == null) continue;
                        var userDic = new Dictionary<string, object>();
                        userDic.SetValue("Name", detailNode.GetValue("Name"));
                        userDic.SetValue("Code", detailNode.GetValue("Code"));
                        userDic.SetValue("CBSNodeID", detailNode.GetValue("ID"));
                        userDic.SetValue("CBSInfoID", detailNode.GetValue("CBSInfoID"));
                        userDic.SetValue("CBSFullID", detailNode.GetValue("FullID"));
                        userDic.SetValue("NodeType", detailNode.GetValue("NodeType"));
                        userDic.SetValue("UserID", item.GetValue("UserInfo"));
                        userDic.SetValue("UserName", item.GetValue("UserInfoName"));
                        userDic.SetValue("SettleValue", item.GetValue(key));
                        userDic.SetValue("Role", item.GetValue("Role"));
                        userDic.SetValue("RoleName", item.GetValue("RoleName"));
                        userDic.SetValue("SettleFormID", this.GetValue("ID"));
                        userDic.SetValue("SettleFormCode", "ProductionSettleApply");
                        userDic.InsertDB(this.DB, "S_EP_ProductionUserSettleValue");
                    }
                }
                else
                {
                    var detailNode = cbsNode.AllChildren.FirstOrDefault(c => c.GetValue("ID") == item.GetValue("ParentID"));
                    if (detailNode == null) continue;
                    var userDic = new Dictionary<string, object>();
                    userDic.SetValue("Name", detailNode.GetValue("Name"));
                    userDic.SetValue("Code", detailNode.GetValue("Code"));
                    userDic.SetValue("CBSNodeID", detailNode.GetValue("ID"));
                    userDic.SetValue("CBSInfoID", detailNode.GetValue("CBSInfoID"));
                    userDic.SetValue("CBSFullID", detailNode.GetValue("FullID"));
                    userDic.SetValue("NodeType", detailNode.GetValue("NodeType"));
                    userDic.SetValue("UserID", item.GetValue("UserInfo"));
                    userDic.SetValue("UserName", item.GetValue("UserInfoName"));
                    userDic.SetValue("SettleValue", item.GetValue("SettleValue"));
                    userDic.SetValue("Role", item.GetValue("Role"));
                    userDic.SetValue("RoleName", item.GetValue("RoleName"));
                    userDic.SetValue("SettleFormID", this.GetValue("ID"));
                    userDic.SetValue("SettleFormCode", "ProductionSettleApply");
                    userDic.InsertDB(this.DB, "S_EP_ProductionUserSettleValue");
                }
            }


            var sumProductionValue = this.DB.ExecuteScalar(String.Format(@"select isnull(Sum(CurrentProductionValue),0) from S_EP_ProductionSettleValue with(nolock) 
where CBSNodeFullID like '{0}%'", cbsNodeDic.GetValue("FullID")));

            productionDic.SetValue("ProductionSettleValue", sumProductionValue);
            productionDic.UpdateDB(this.DB, "S_EP_ProductionUnit", productionDic.GetValue("ID"));

            this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set ProductionSettleValue=isnull((select Sum(CurrentProductionValue) from S_EP_ProductionSettleValue where CBSNodeFullID like S_EP_CBSNode.FullID+'%'),0) 
where '{0}' like S_EP_CBSNode.FullID+'%'", cbsNodeDic.GetValue("FullID")));

            this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set ProductionSettleValue=isnull((select Sum(CurrentProductionValue) from S_EP_ProductionSettleValue where CBSNodeFullID like S_EP_CBSNode.FullID+'%'),0) 
where S_EP_CBSNode.FullID like +'{0}%'", cbsNodeDic.GetValue("FullID")));

        }
    }
}
