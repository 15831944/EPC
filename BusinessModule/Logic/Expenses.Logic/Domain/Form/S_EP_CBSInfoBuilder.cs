using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_CBSInfoBuilder : BaseEPModel
    {
        public void Push()
        {
            Expenses.Logic.CBSInfoFO.SynCBSInfo(this.ModelDic, SetCBSOpportunity.CBSInfoBuilder);
        }
    }
}
