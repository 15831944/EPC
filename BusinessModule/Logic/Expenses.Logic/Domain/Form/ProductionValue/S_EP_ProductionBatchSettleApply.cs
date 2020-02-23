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
    public partial class S_EP_ProductionBatchSettleApply : BaseEPModel
    {
        public void Push()
        {
            #region 更新产值单元进度节点
            var sql = string.Format(@"select * from S_EP_ProductionBatchSettleApply_Detail 
where S_EP_ProductionBatchSettleApplyID='{0}' ", this.ModelDic.GetValue("ID"));
            var dt = this.DB.ExecuteDataTable(sql);
            var detailList = FormulaHelper.DataTableToListDic(dt);
            var currentUser = FormulaHelper.GetUserInfo();
            var progressNode = new Dictionary<string, object>();
            var productionQuery = new Dictionary<string, object>();
            var sqlUpdate = string.Empty;
            foreach (var dicDetail in detailList)
            {
                #region 更新节点信息
                progressNode = this.GetDataDicByID("S_EP_ProductionUnit_ProgressNode", dicDetail.GetValue("CurrentConfirmNode"));
                if (progressNode == null)
                    throw new Formula.Exceptions.BusinessValidationException("没有找到对应的节点信息，无法确认节点");

                sql = string.Format(@"select CBSNode.ChargerUser,CBSNode.ChargerUserName,
CBSNode.ChargerDept,CBSNode.ChargerDeptName, ProductionUnit.*  
from S_EP_ProductionUnit ProductionUnit
left join S_EP_CBSNode CBSNode on CBSNode.ID=ProductionUnit.CBSNodeID
where ProductionUnit.ID='{0}' ", dicDetail.GetValue("ProductionUnit"));
                dt = this.DB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                    throw new Formula.Exceptions.BusinessValidationException("没有找到对应的产值单元，无法确认节点");
                productionQuery = FormulaHelper.DataRowToDic(dt.Rows[0]);

                var beforeNodes = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_ProductionUnit_ProgressNode 
where ProductionUnitID='{0}' and SortIndex<={1} and (State<>'{2}' or State is null )order by sortIndex ", productionQuery.GetValue("ID"), progressNode.GetValue("SortIndex"), "Finish"));
                sqlUpdate = string.Empty;
                foreach (DataRow node in beforeNodes.Rows)
                {
                    sqlUpdate += string.Format(@"UPDATE S_EP_ProductionUnit_ProgressNode SET FactEndDate='{1}' ,State='Finish',
                        ModifyUserID='{2}',ModifyUser='{3}', ModifyDate=getdate() WHERE ID='{0}' ", node["ID"],
                        string.IsNullOrEmpty(dicDetail.GetValue("ConfirmDate")) ? DateTime.Now.ToString() : dicDetail.GetValue("ConfirmDate"),
                        currentUser.UserID, currentUser.UserName);
                }
                this.DB.ExecuteNonQuery(sqlUpdate);
                #endregion

                #region 抛入S_EP_ProductionSettleValue
                var dicProductionSettle = new Dictionary<string, object>();
                dicProductionSettle.SetValue("ID", FormulaHelper.CreateGuid());
                dicProductionSettle.SetValue("CBSInfo", productionQuery.GetValue("CBSInfoID"));
                dicProductionSettle.SetValue("CBSNodeID", productionQuery.GetValue("CBSNodeID"));
                dicProductionSettle.SetValue("CBSNodeFullID", productionQuery.GetValue("CBSNodeFullID"));
                dicProductionSettle.SetValue("ChargerUser", productionQuery.GetValue("ChargerUser"));
                dicProductionSettle.SetValue("ChargerUserName", productionQuery.GetValue("ChargerUserName"));
                dicProductionSettle.SetValue("ChargerDept", productionQuery.GetValue("ChargerDept"));
                dicProductionSettle.SetValue("ChargerDeptName", productionQuery.GetValue("ChargerDeptName"));
                dicProductionSettle.SetValue("ProductionUnitID", productionQuery.GetValue("ID"));
                dicProductionSettle.SetValue("UnitName", productionQuery.GetValue("Name"));
                dicProductionSettle.SetValue("UnitCode", productionQuery.GetValue("Code"));
                dicProductionSettle.SetValue("ConfirmFormID", this.GetValue("ID"));
                dicProductionSettle.SetValue("ConfirmDetailID", dicDetail.GetValue("ID"));
                dicProductionSettle.SetValue("LastScale", dicDetail.GetValue("LastConfirmedScaleTotal"));
                dicProductionSettle.SetValue("LastProductionValue", dicDetail.GetValue("LastConfirmedValueTotal"));
                dicProductionSettle.SetValue("TotalScale", dicDetail.GetValue("CurrentConfirmScaleTotal"));
                dicProductionSettle.SetValue("TotalProductionValue", dicDetail.GetValue("CurrentConfirmValueTotal"));
                dicProductionSettle.SetValue("CurrentProductionValue", dicDetail.GetValue("CurrentConfirmValue"));
                dicProductionSettle.SetValue("FinishProgressNodeID", dicDetail.GetValue("CurrentConfirmNode"));
                dicProductionSettle.SetValue("ApplyUser", this.ModelDic.GetValue("ApplyUser"));
                dicProductionSettle.SetValue("ApplyUserName", this.ModelDic.GetValue("ApplyUserName"));
                dicProductionSettle.SetValue("FactEndDate", dicDetail.GetValue("ConfirmDate"));
                dicProductionSettle.InsertDB(this.DB, "S_EP_ProductionSettleValue", dicProductionSettle.GetValue("ID"));
                #endregion

                #region 更新ProductionUnit的产值信息 更新cbsNode的产值信息
                //更新ProductionUnit的产值信息
                var productionDic = this.GetDataDicByID("S_EP_ProductionUnit", productionQuery.GetValue("ID"));
                var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", productionDic.GetValue("CBSNodeID"));
                if (cbsNodeDic == null) throw new Formula.Exceptions.BusinessValidationException("未能找到对应的CBS节点信息");
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

                #endregion
            }
            #endregion
        }
    }
}
