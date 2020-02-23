using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_IncomeSubmit : BaseEPModel
    {
        public void Push()
        {
            var totalScale = String.IsNullOrEmpty(this.ModelDic.GetValue("TotalScale")) ? 0 : Convert.ToDecimal(this.ModelDic.GetValue("TotalScale"));
            var unit = this.GetDataDicByID("S_EP_IncomeUnit", this.ModelDic.GetValue("IncomeUnitID"));
            if (unit == null)
            {
                throw new Formula.Exceptions.BusinessException("没有找到对应的收入单元，ID【" + this.ID + "】进度申报单无法确认进度");
            }
            unit.SetValue("TotalScale",totalScale);
            unit.SetValue("TotalIncomeScale", totalScale);
            unit.UpdateDB(this.DB, "S_EP_IncomeUnit", unit.GetValue("ID"));
        }
    }
}
