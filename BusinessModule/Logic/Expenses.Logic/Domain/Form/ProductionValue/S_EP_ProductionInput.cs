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
    public partial class S_EP_ProductionInput : BaseEPModel
    {
        private Dictionary<string, string> dic;

        public S_EP_ProductionInput()
        {

        }
        public S_EP_ProductionInput(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            Expenses.Logic.CBSInfoFO.SynCBSInfo(this.ModelDic, SetCBSOpportunity.ProductionInput);
        }

        public void Validate()
        {
             
        }
    }
}
