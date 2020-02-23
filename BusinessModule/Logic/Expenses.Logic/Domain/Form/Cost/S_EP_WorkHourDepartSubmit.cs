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
    public partial class S_EP_WorkHourDepartSubmit : BaseEPModel
    {
        public S_EP_WorkHourDepartSubmit()
        {

        }
        public S_EP_WorkHourDepartSubmit(Dictionary<string, object> dic)
        {
            FillModel(dic);
        }
        public void Push()
        {
            var detailDt = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_WorkHourDepartSubmit_WorkHourDetail 
where S_EP_WorkHourDepartSubmitID='{0}'", this.ID));

            foreach (DataRow dr in detailDt.Rows)
            {
                var dicToInsert = new Dictionary<string, object>();
                dicToInsert.SetValue("ID", FormulaHelper.CreateGuid());
                dicToInsert.SetValue("BelongYear", this.ModelDic["BelongYear"]);
                dicToInsert.SetValue("BelongMonth", this.ModelDic["BelongMonth"]);
                if (string.IsNullOrEmpty(dr["CostUnitInfo"].ToString()))
                {
                    dicToInsert.SetValue("IsProduction", "false");
                }
                else
                {
                    dicToInsert.SetValue("IsProduction", "true");
                }
                dicToInsert.SetValue("CostUnitInfo", dr["CostUnitInfo"]);
                dicToInsert.SetValue("CostUnitInfoName", dr["CostUnitInfoName"]);
                dicToInsert.SetValue("UserDeptInfo", this.ModelDic["DepartInfo"]);
                dicToInsert.SetValue("UserDeptInfoName", this.ModelDic["DepartInfoName"]);
                dicToInsert.SetValue("UserInfo", dr["UserInfo"]);
                dicToInsert.SetValue("UserInfoName", dr["UserInfoName"]);
                dicToInsert.SetValue("WorkHour", dr["WorkHour"]);
                dicToInsert.SetValue("RelateID", dr["ID"]);
                dicToInsert.InsertDB(this.DB, "S_EP_WorkHourUser");
            }
        }
    }
}
