using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;


namespace Expenses.Logic.Domain
{
    public partial class S_EP_CostInfo : BaseEPModel
    {
        public S_EP_CostInfo(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Build()
        {
            if (String.IsNullOrEmpty(this.ModelDic.GetValue("ID")))
                this.ModelDic.SetValue("ID", FormulaHelper.CreateGuid());

            if (string.IsNullOrEmpty(this.ModelDic.GetValue("Name")))
                this.ModelDic.SetValue("Name", "未赋值");

            if (string.IsNullOrEmpty(this.ModelDic.GetValue("Code")))
                this.ModelDic.SetValue("Code", "未赋值");

            if (string.IsNullOrEmpty(this.ModelDic.GetValue("SubjectCode")))
                this.ModelDic.SetValue("SubjectCode", "未赋值");

            if (string.IsNullOrEmpty(this.ModelDic.GetValue("CBSFullCode")))
                this.ModelDic.SetValue("CBSFullCode", "未赋值");

            if (string.IsNullOrEmpty(this.ModelDic.GetValue("State")))
                this.ModelDic.SetValue("State", "未赋值");

            if (string.IsNullOrEmpty(this.ModelDic.GetValue("Status")))
                this.ModelDic.SetValue("Status", "未赋值");
             
            this.ModelDic.InsertDB(this.DB, "S_EP_CostInfo", this.ModelDic.GetValue("ID"));
        }
    }
}
