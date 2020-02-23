using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_IncomeInvoiceConfirmRevert : BaseEPModel
    {
        public S_EP_IncomeInvoiceConfirmRevert()
        {

        }

        public S_EP_IncomeInvoiceConfirmRevert(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            //判断是否是最后确认节点
            var dt = this.DB.ExecuteDataTable(string.Format("select top 1 ID,ConfirmDate from S_EP_IncomeInvoiceConfirm where InvoiceID='{0}' order by ID desc ", this.ModelDic.GetValue("InvoiceID")));
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

            string sql = string.Format(@"delete from S_EP_IncomeInvoiceConfirm where ID='{0}' ", this.ModelDic.GetValue("InvoiceConfirmID"));
            this.DB.ExecuteNonQuery(sql);
        }
    }
}
