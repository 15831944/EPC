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
    public partial class S_EP_ProductionDeductionApply : BaseEPModel
    {
        public S_EP_ProductionDeductionApply()
        {

        }

        public S_EP_ProductionDeductionApply(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            var cbsDic = this.GetDataDicByID("S_EP_CBSNode", this.ModelDic.GetValue("CBSNode"));
            if (cbsDic == null)
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的产值节点，无法补贴产值");
            cbsDic.SetValue("ProductionValue", this.ModelDic.GetValue("ProductionValue"));
            var cbsNode = new S_EP_CBSNode(cbsDic);
            var defineChildren = cbsNode.DefineNode.ChildrenNode.Where(c => c.ModelDic.GetValue("NodeType") != CBSNodeType.Communal.ToString()
                && c.ModelDic.GetValue("NodeType") != CBSNodeType.Reserved.ToString()).ToList();
            if (defineChildren.Count > 0)
            {
                //判定结构定义中是否有子节点，如果有才能够对子节点进行操作
                var detailRows = this.DB.ExecuteDataTable(String.Format("select * from S_EP_ProductionDeductionApply_Detail where S_EP_ProductionDeductionApplyID='{0}'"
               , this.ModelDic.GetValue("ID")));
                foreach (DataRow detailRow in detailRows.Rows)
                {
                    var detail = FormulaHelper.DataRowToDic(detailRow);
                    #region 同步更新产值CBS节点明细
                    if (String.IsNullOrEmpty(detail.GetValue("CBSID")))
                    {
                        var child = new Dictionary<string, object>();
                        child.SetValue("Name", detail.GetValue("Name"));
                        child.SetValue("Code", detail.GetValue("Value"));
                        child.SetValue("RelateID", detail.GetValue("RelateID"));
                        child.SetValue("ChargerDept", detail.GetValue("ChargerDept"));
                        child.SetValue("ChargerDeptName", detail.GetValue("ChargerDeptName"));
                        child.SetValue("ChargerUser", detail.GetValue("ChargerUser"));
                        child.SetValue("ChargerUserName", detail.GetValue("ChargerUserName"));
                        child.SetValue("ProductionValue", detail.GetValue("ProductionValueNew"));
                        var childNode = new S_EP_CBSNode(child);
                        cbsNode.AddChild(child);
                        childNode.SetUnit();
                    }
                    else
                    {
                        var child = this.GetDataDicByID("S_EP_CBSNode", detail.GetValue("CBSID"));
                        if (child == null) continue;
                        child.SetValue("Name", detail.GetValue("Name"));
                        if (!String.IsNullOrEmpty(detail.GetValue("Code")))
                            child.SetValue("Code", detail.GetValue("Code"));
                        child.SetValue("RelateID", detail.GetValue("RelateID"));
                        child.SetValue("ChargerDept", detail.GetValue("ChargerDept"));
                        child.SetValue("ChargerDeptName", detail.GetValue("ChargerDeptName"));
                        child.SetValue("ChargerUser", detail.GetValue("ChargerUser"));
                        child.SetValue("ChargerUserName", detail.GetValue("ChargerUserName"));
                        child.SetValue("ProductionValue", detail.GetValue("ProductionValueNew"));
                        child.UpdateDB(this.DB, "S_EP_CBSNode", child.GetValue("ID"));
                        this.DB.ExecuteNonQuery(String.Format("update S_EP_ProductionUnit set ProductionValue={0} where CBSNodeID='{1}'",
                            String.IsNullOrEmpty(detail.GetValue("ProductionValueNew")) ? "0" : detail.GetValue("ProductionValueNew"), child.GetValue("ID")));
                        var childNode = new S_EP_CBSNode(child);
                        childNode.SetUnit();
                        var changeValue = String.IsNullOrEmpty(detail.GetValue("AdjustValue")) ? 0m : Convert.ToDecimal(detail.GetValue("AdjustValue"));
                        childNode.AutoSplitProductionValueToReversed(changeValue);
                    }
                    #endregion
                    setChangeLoad(detail);
                }
            }
            if (cbsNode.DefineNode.ChildrenNode.Count(c => c.ModelDic.GetValue("NodeType") == CBSNodeType.Reserved.ToString()) > 0)
            {
                //当节点定义中有预留节点时，才能进行预留
                #region 更新预留产值节点
                var reserverNode = cbsNode.Children.FirstOrDefault(c => c.ModelDic.GetValue("NodeType") == CBSNodeType.Reserved.ToString());
                if (reserverNode == null)
                {
                    if (!String.IsNullOrEmpty(this.ModelDic.GetValue("ProductionReserveValue")) && Convert.ToDecimal(this.ModelDic.GetValue("ProductionReserveValue")) > 0)
                    {
                        var revDic = new Dictionary<string, object>();
                        revDic.SetValue("Name", "产值预留");
                        revDic.SetValue("Code", "Reserves");
                        revDic.SetValue("NodeType", CBSNodeType.Reserved.ToString());
                        var defineNode = cbsNode.Children.FirstOrDefault(c => c.GetValue("NodeType") == CBSNodeType.Reserved.ToString());
                        revDic.SetValue("DefineID", defineNode.GetValue("ID"));
                        revDic.SetValue("ProductionValue", Convert.ToDecimal(this.ModelDic.GetValue("ProductionReserveValue")));
                        revDic.SetValue("RelateID", cbsDic.GetValue("ID") + CBSNodeType.Reserved.ToString());
                        reserverNode = new S_EP_CBSNode(revDic);
                        cbsNode.AddChild(reserverNode);
                    }
                }
                else
                {
                    reserverNode.ModelDic.SetValue("ProductionValue",
                        String.IsNullOrEmpty(this.ModelDic.GetValue("ProductionReserveValue")) ? 0m :
                        Convert.ToDecimal(this.ModelDic.GetValue("ProductionReserveValue")));
                    reserverNode.ModelDic.UpdateDB(this.DB, "S_EP_CBSNode", reserverNode.ModelDic.GetValue("ID"));
                }
                #endregion

                //如果有预留节点，当前节点的计划产值等于所有子节点计划产值汇总
                //因为所有的计划产值必须在分解的时候全部分解完毕，如果暂不分解完成的，必须存放在预留节点内
                //没有预留节点说明不能预留
                cbsNode.SumProductionValue();
            }
            else
            {
                cbsDic.SetValue("ProductionValue", this.ModelDic.GetValue("ProductionValue"));
                cbsDic.UpdateDB(this.DB, "S_EP_CBSNode", cbsDic.GetValue("ID"));
            }
            cbsNode.SumProductionValueToTop();
            setChangeLoad(cbsDic);

            foreach (var ancestor in cbsNode.Ancestors)
            {
                setChangeLoad(ancestor.ModelDic);
            }
        }

        void setChangeLoad(Dictionary<string, object> cbsDic)
        {
            #region 记录CBS变更记录
            var subsidyInfo = new Dictionary<string, object>();
            subsidyInfo.SetValue("UseType", ProductionChangeType.Deduction.ToString());
            subsidyInfo.SetValue("UseCBSNode", cbsDic.GetValue("ID"));
            subsidyInfo.SetValue("UseCBSInfo", cbsDic.GetValue("CBSInfoID"));
            subsidyInfo.SetValue("UseValue", String.IsNullOrEmpty(this.ModelDic.GetValue("DeductionValue")) ? 0 :
                0 - Convert.ToDecimal(this.ModelDic.GetValue("DeductionValue")));
            subsidyInfo.SetValue("OperationUser", this.ModelDic.GetValue("CreateUserID"));
            subsidyInfo.SetValue("OperationUserName", this.ModelDic.GetValue("CreateUser"));
            subsidyInfo.SetValue("UseDate", DateTime.Now);
            subsidyInfo.SetValue("UseReason", this.ModelDic.GetValue("Reason"));
            subsidyInfo.SetValue("ApplyFormID", this.ModelDic.GetValue("ID"));
            subsidyInfo.SetValue("ApplyFormCode", "ProductionDeduction");
            subsidyInfo.InsertDB(this.DB, "S_EP_ProductionCBSChangeLog");
            #endregion
        }
    }
}
