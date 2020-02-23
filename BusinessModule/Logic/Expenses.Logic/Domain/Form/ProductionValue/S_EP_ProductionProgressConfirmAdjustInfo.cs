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
    public partial class S_EP_ProductionProgressConfirmAdjustInfo : BaseEPModel
    {
        public void Push()
        {
            var confirmAdjustInfoDic = this.ModelDic;
            var cbsNodeDic = this.GetDataDicByID("S_EP_CBSNode", confirmAdjustInfoDic.GetValue("CBSNodeID"));
            if (cbsNodeDic == null)
                throw new BusinessException("无法找到ID为【" + confirmAdjustInfoDic.GetValue("CBSNodeID") + "】的CBSNode");

            string sql = "";
            var productionSettleValue = new Dictionary<string, object>();
            productionSettleValue.SetValue("ID", FormulaHelper.CreateGuid());
            productionSettleValue.SetValue("CBSInfo", cbsNodeDic.GetValue("CBSInfoID"));
            productionSettleValue.SetValue("CBSNodeID", confirmAdjustInfoDic.GetValue("CBSNodeID"));
            productionSettleValue.SetValue("CBSNodeFullID", cbsNodeDic.GetValue("FullID"));
            productionSettleValue.SetValue("ProductionUnitID", confirmAdjustInfoDic.GetValue("ProductionUnitID"));
            productionSettleValue.SetValue("UnitName", cbsNodeDic.GetValue("Name"));
            productionSettleValue.SetValue("UnitCode", cbsNodeDic.GetValue("Code"));
            //productionSettleValue.SetValue("SettleDataFrom", SettleDataFrom.ConfirmAdjust.ToString());
            productionSettleValue.SetValue("LastScale", "0");
            productionSettleValue.SetValue("LastProductionValue", confirmAdjustInfoDic.GetValue("ProductionSettleValue"));
            productionSettleValue.SetValue("TotalScale", "0");
            productionSettleValue.SetValue("TotalProductionValue", confirmAdjustInfoDic.GetValue("ProductionSettleValueNew"));
            productionSettleValue.SetValue("CurrentProductionValue", confirmAdjustInfoDic.GetValue("ProductionSettleValueAdjust"));
            productionSettleValue.SetValue("ConfirmFormID", "");
            productionSettleValue.SetValue("ConfirmDetailID", "");
            productionSettleValue.SetValue("FactEndDate", confirmAdjustInfoDic.GetValue("OperateDate"));
            productionSettleValue.SetValue("ChargerUser", cbsNodeDic.GetValue("ChargerUser"));
            productionSettleValue.SetValue("ChargerUserName", cbsNodeDic.GetValue("ChargerUserName"));
            productionSettleValue.SetValue("ChargerDept", cbsNodeDic.GetValue("ChargerDept"));
            productionSettleValue.SetValue("ChargerDeptName", cbsNodeDic.GetValue("ChargerDeptName"));
            productionSettleValue.SetValue("ApplyUser", confirmAdjustInfoDic.GetValue("ApplyUser"));
            productionSettleValue.SetValue("ApplyUserName", confirmAdjustInfoDic.GetValue("ApplyUserName"));
            productionSettleValue.SetValue("Company", cbsNodeDic.GetValue("OrgID"));
            var org = this.GetDataDicByID("S_A_Org", cbsNodeDic.GetValue("OrgID"), ConnEnum.Base);
            if (org != null) productionSettleValue.SetValue("CompanyName", org.GetValue("Name"));
            productionSettleValue.SetValue("FinishProgressNodeID", "");
            sql = productionSettleValue.CreateInsertSql(this.DB, "S_EP_ProductionSettleValue", productionSettleValue.GetValue("ID"));

            #region 更新ProductionUnit的产值信息 更新cbsNode的产值信息
            //更新ProductionUnit的产值信息
            var productionUnit = this.GetDataDicByID("S_EP_ProductionUnit", confirmAdjustInfoDic.GetValue("ProductionUnitID"));
            if (productionUnit != null)
            {
                decimal tmp1 = 0;
                decimal.TryParse(productionUnit.GetValue("ProductionSettleValue"), out tmp1);
                decimal tmp2 = 0;
                decimal.TryParse(confirmAdjustInfoDic.GetValue("ProductionSettleValueAdjust"), out tmp2);
                productionUnit.SetValue("ProductionSettleValue", tmp1 + tmp2);
                sql += productionUnit.CreateUpdateSql(this.DB, "S_EP_ProductionUnit", productionUnit.GetValue("ID"));
            }
            //更新cbsNode的产值信息
            if (cbsNodeDic != null)
            {
                cbsNodeDic.SetValue("ProductionSettleValue", confirmAdjustInfoDic.GetValue("ProductionSettleValueNew"));
                sql += cbsNodeDic.CreateUpdateSql(this.DB, "S_EP_CBSNode", cbsNodeDic.GetValue("ID"));
            }
            this.DB.ExecuteNonQuery(sql);

            //更新cbsNode上级节点及cbsinfo产值信息
            if (cbsNodeDic != null)
            {
                S_EP_CBSNode cbsNode = new S_EP_CBSNode(cbsNodeDic);
                cbsNode.SumProductionSettleValueToTop();
            }
            #endregion
        }
    }
}
