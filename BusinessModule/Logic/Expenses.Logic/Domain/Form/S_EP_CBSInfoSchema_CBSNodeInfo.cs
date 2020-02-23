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
    public class S_EP_CBSInfoSchema_CBSNodeInfo : BaseEPModel
    {
        public S_EP_CBSInfoSchema_CBSNodeInfo(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }
    }
}
