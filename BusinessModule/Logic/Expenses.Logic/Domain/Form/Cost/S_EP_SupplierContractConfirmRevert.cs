using Config.Logic;
using Formula;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_SupplierContractConfirmRevert : BaseEPModel
    {
        public void Push()
        {
            #region 判断节点是否是最后确认节点
            var sql = string.Format(@"select top 1 ID,CBSInfoID,ConfirmDate,FlowPhase as ConfirmFlowPhase from S_EP_SupplierContractConfirm 
where SubContractDetailID='{0}' order by ID desc ", this.ModelDic.GetValue("SubContractDetailID"));
            var dt = this.DB.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("要撤销的节点不存在！");
            }
            var dicConfirm = FormulaHelper.DataRowToDic(dt.Rows[0]);
            if (this.ModelDic.GetValue("SubContractConfirmID") != dicConfirm.GetValue("ID"))
            {
                throw new Formula.Exceptions.BusinessValidationException("只能逐步撤销节点");
            }

            #endregion

            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dicConfirm.GetValue("ConfirmDate")));

            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", dicConfirm.GetValue("CBSInfoID"));
            if (cbsInfoDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法撤销！");
            }

            sql = string.Format(@"delete from S_EP_SupplierContractConfirm where ID='{0}' ", this.ModelDic.GetValue("SubContractConfirmID"));
            sql += string.Format(@"delete from S_EP_CostInfo where RelateID='{0}' ", this.ModelDic.GetValue("SubContractConfirmID"));
            sql += string.Format(@"update S_SP_SupplierContract_ContractSplit 
set MinContractValue=(select ISNULL(sum(CurrentValue),0) from S_EP_SupplierContractConfirm where SubContractDetailID='{0}') 
where ID='{0}'  ", this.ModelDic.GetValue("SubContractDetailID"));
            this.DB.ExecuteNonQuery(sql);

            var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
            cbsInfo.SummaryCostValue();

        }
    }
}
