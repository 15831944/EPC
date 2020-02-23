using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Newtonsoft.Json;
using System.Data;
using Formula.Exceptions;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_ProductionProgressConfirm_DetailAdjustInfo : BaseEPModel
    {
        public void Push()
        {
            var confirmAdjustInfoDic = this.ModelDic;
            var productionUnit = this.GetDataDicByID("S_EP_ProductionUnit", confirmAdjustInfoDic.GetValue("ProductionUnit"));
            if (productionUnit == null)
                throw new BusinessException("无法找到ID为【" + confirmAdjustInfoDic.GetValue("ProductionUnit") + "】的产值单元");
            var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", productionUnit.GetValue("CBSNodeID"));
            if (cbsNodeDic == null)
                throw new BusinessException("无法找到ID为【" + productionUnit.GetValue("CBSNodeID") + "】的CBSNode");

            string sql = "";
            var productionSettleValueDt = this.DB.ExecuteDataTable(
                "select top 1 * from S_EP_ProductionSettleValue where ConfirmDetailID = '" + confirmAdjustInfoDic.GetValue("ConfirmDetailID") + "'");

            var productionSettleValue = FormulaHelper.DataRowToDic(productionSettleValueDt.Rows[0]);
            productionSettleValue.SetValue("TotalScale", confirmAdjustInfoDic.GetValue("CurrentConfirmScaleTotalNew"));
            productionSettleValue.SetValue("TotalProductionValue", confirmAdjustInfoDic.GetValue("CurrentConfirmValueTotalNew"));
            productionSettleValue.SetValue("CurrentProductionValue", confirmAdjustInfoDic.GetValue("CurrentConfirmValueNew"));

            sql = productionSettleValue.CreateUpdateSql(this.DB, "S_EP_ProductionSettleValue", productionSettleValue.GetValue("ID"));

            #region 更新ProductionUnit的产值信息 更新cbsNode的产值信息
            //更新ProductionUnit的产值信息

            decimal tmp1 = 0;
            decimal.TryParse(productionUnit.GetValue("ProductionSettleValue"), out tmp1);
            decimal tmp2 = 0;
            decimal.TryParse(confirmAdjustInfoDic.GetValue("CurrentConfirmValueAdjust"), out tmp2);
            productionUnit.SetValue("ProductionSettleValue", tmp1 + tmp2);
            sql += productionUnit.CreateUpdateSql(this.DB, "S_EP_ProductionUnit", productionUnit.GetValue("ID"));
            //更新cbsNode的产值信息
            cbsNodeDic.SetValue("ProductionSettleValue", productionUnit.GetValue("ProductionSettleValue"));

            sql += cbsNodeDic.CreateUpdateSql(this.DB, "S_EP_CBSNode", cbsNodeDic.GetValue("ID"));
            this.DB.ExecuteNonQuery(sql);

            //更新cbsNode上级节点及cbsinfo产值信息
            S_EP_CBSNode cbsNode = new S_EP_CBSNode(cbsNodeDic);
            cbsNode.SumProductionSettleValueToTop();
            #endregion

            #region 产值公积,同时向上累加公积部分的金额到计划产值父节点上
            if (this.ModelDic.GetValue("AdjustMethod") == "CommunalAdjust")
            {
                sql = "";
                //新的计划产值
                decimal oldProductionValue = 0;
                decimal.TryParse(confirmAdjustInfoDic.GetValue("PlanProductionValue"), out oldProductionValue);
                decimal currentConfirmValueAdjust = 0;
                decimal.TryParse(confirmAdjustInfoDic.GetValue("CurrentConfirmValueAdjust"), out currentConfirmValueAdjust);

                productionSettleValue.SetValue("PlanProductionValue", oldProductionValue + currentConfirmValueAdjust);
                sql += productionSettleValue.CreateUpdateSql(this.DB, "S_EP_ProductionSettleValue", productionSettleValue.GetValue("ID"));

                //更新产值单元
                productionUnit = this.GetDataDicByID("S_EP_ProductionUnit", confirmAdjustInfoDic.GetValue("ProductionUnit"));
                productionUnit.SetValue("ProductionValue", oldProductionValue + currentConfirmValueAdjust);
                sql += productionUnit.CreateUpdateSql(this.DB, "S_EP_ProductionUnit", productionUnit.GetValue("ID"));

                //更新cbsnode计划产值
                cbsNodeDic.SetValue("ProductionValue", oldProductionValue + currentConfirmValueAdjust);
                sql += cbsNodeDic.CreateUpdateSql(this.DB, "S_EP_CBSNode", cbsNodeDic.GetValue("ID"));

                this.DB.ExecuteNonQuery(sql);

                S_EP_CBSNode newCbsNode = new S_EP_CBSNode(cbsNodeDic);
                //更新父节点计划产值
                newCbsNode.SumProductionValueToTop();
                //产值公积信息更新
                setChangeLoad(cbsNodeDic);
                foreach (var ancestor in cbsNode.Ancestors)
                {
                    setChangeLoad(ancestor.ModelDic);
                }
            }
            #endregion
        }

        void setChangeLoad(Dictionary<string, object> cbsDic)
        {
            #region 记录CBS变更记录
            var subsidyInfo = new Dictionary<string, object>();
            subsidyInfo.SetValue("UseType", ProductionChangeType.Subsidy.ToString());
            subsidyInfo.SetValue("UseCBSNode", cbsDic.GetValue("ID"));
            subsidyInfo.SetValue("UseCBSInfo", cbsDic.GetValue("CBSInfoID"));
            subsidyInfo.SetValue("UseValue", this.ModelDic.GetValue("CurrentConfirmValueAdjust"));
            subsidyInfo.SetValue("OperationUser", this.ModelDic.GetValue("CreateUserID"));
            subsidyInfo.SetValue("OperationUserName", this.ModelDic.GetValue("CreateUser"));
            subsidyInfo.SetValue("UseDate", DateTime.Now);
            subsidyInfo.SetValue("UseReason", "确认产值进度调整");
            subsidyInfo.SetValue("ApplyFormID", this.ModelDic.GetValue("ID"));
            subsidyInfo.SetValue("ApplyFormCode", "ProductionProgressConfirmDetailAdjustinfo");
            subsidyInfo.InsertDB(this.DB, "S_EP_ProductionCBSChangeLog");
            #endregion
        }
    }
}
