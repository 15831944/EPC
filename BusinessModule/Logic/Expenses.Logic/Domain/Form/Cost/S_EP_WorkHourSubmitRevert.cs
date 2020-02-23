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
    public partial class S_EP_WorkHourSubmitRevert : BaseEPModel
    {
        public void Push()
        {
            string sqlQuery = String.Format("delete S_EP_WorkHourSubmit where ID='{0}' ", this.ModelDic.GetValue("WorkSubmitFormID"));
            var detailDT = this.DB.ExecuteDataTable("select * from S_EP_WorkHourSubmitRevert_WorkHourDetail where S_EP_WorkHourSubmitRevertID = '" + this.ModelDic.GetValue("ID") + "'");
            foreach (DataRow dr in detailDT.Rows)
            {
                sqlQuery += String.Format("delete S_EP_WorkHourUser where RelateID='{0}' ", dr["SubmitDetailID"]);
            }
            this.DB.ExecuteNonQuery(sqlQuery);
        }
    }
}
