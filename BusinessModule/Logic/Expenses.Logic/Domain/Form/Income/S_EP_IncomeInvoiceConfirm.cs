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
    public partial class S_EP_IncomeInvoiceConfirm : BaseEPModel
    {
        public S_EP_IncomeInvoiceConfirm()
        {

        }

        public S_EP_IncomeInvoiceConfirm(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }
    }
}
