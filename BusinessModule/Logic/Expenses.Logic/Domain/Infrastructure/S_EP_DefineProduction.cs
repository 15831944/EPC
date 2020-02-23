﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_DefineProduction : BaseEPModel
    {
        public S_EP_DefineProduction()
        {

        }

        public S_EP_DefineProduction(Dictionary<string, object> dic)
        {
            if (dic == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_DefineProduction的键值对不能对空值");
            this.FillModel(dic);
        }

    }
}