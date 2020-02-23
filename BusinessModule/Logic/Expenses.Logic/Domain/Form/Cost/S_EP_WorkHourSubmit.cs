using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Formula;
using Formula.Helper;
using Config.Logic;
using Config;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_WorkHourSubmit : BaseEPModel
    {
        public S_EP_WorkHourSubmit()
        {

        }

        public S_EP_WorkHourSubmit(Dictionary<string,object> dic)
        {
            FillModel(dic);
        }

        public void Push()
        {
            var detailDt = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_WorkHourSubmit_WorkHourDetail 
where S_EP_WorkHourSubmitID='{0}'", this.ID));

            foreach (DataRow dr in detailDt.Rows)
            {
                var dicToInsert = new Dictionary<string, object>();
                dicToInsert.SetValue("ID", FormulaHelper.CreateGuid());
                dicToInsert.SetValue("BelongYear", this.ModelDic["BelongYear"]);
                dicToInsert.SetValue("BelongMonth", this.ModelDic["BelongMonth"]);
                dicToInsert.SetValue("CostUnitInfo", this.ModelDic["CostUnitInfo"]);
                dicToInsert.SetValue("CostUnitInfoName", this.ModelDic["CostUnitInfoName"]);
                dicToInsert.SetValue("UserDeptInfo", dr["UserDeptInfo"]);
                dicToInsert.SetValue("UserDeptInfoName", dr["UserDeptInfoName"]);
                dicToInsert.SetValue("UserInfo", dr["UserInfo"]);
                dicToInsert.SetValue("UserInfoName", dr["UserInfoName"]);
                dicToInsert.SetValue("WorkHour", dr["WorkHour"]);
                dicToInsert.SetValue("IsProduction", "true");
                dicToInsert.SetValue("RelateID", dr["ID"]);
                dicToInsert.InsertDB(this.DB, "S_EP_WorkHourUser");
            }
        }
    }
}
