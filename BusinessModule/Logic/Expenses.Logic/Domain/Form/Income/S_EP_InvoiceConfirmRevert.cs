using Config.Logic;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_InvoiceConfirmRevert : BaseEPModel
    {
        public void Push()
        {
            //判断是否是最后确认节点
            var dt = this.DB.ExecuteDataTable(string.Format("select top 1 ID,CBSInfoID,ConfirmDate from S_EP_InvoiceConfirm where InvoiceID='{0}' order by ID desc ", this.ModelDic.GetValue("InvoiceID")));
            if (dt.Rows.Count == 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("无确认信息，无需撤销！");
            }
            var dicConfirm = FormulaHelper.DataRowToDic(dt.Rows[0]);
            if (this.ModelDic.GetValue("InvoiceConfirmID") != dicConfirm.GetValue("ID"))
            {
                throw new Formula.Exceptions.BusinessValidationException("只能逐步撤销节点");
            }
            CostFO.ValidatePeriodIsClosed(Convert.ToDateTime(dicConfirm.GetValue("ConfirmDate")));

            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", dicConfirm.GetValue("CBSInfoID"));
            if (cbsInfoDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到成本单元对应的核算项目，无法撤销！");
            }

            string sql = string.Format(@"delete from S_EP_InvoiceConfirm where ID='{0}' ", this.ModelDic.GetValue("InvoiceConfirmID"));
            sql += string.Format(@"delete from S_EP_CostInfo where RelateID='{0}' ", this.ModelDic.GetValue("InvoiceConfirmID"));
            this.DB.ExecuteNonQuery(sql);

            var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
            cbsInfo.SummaryCostValue();


        }
    }
}
