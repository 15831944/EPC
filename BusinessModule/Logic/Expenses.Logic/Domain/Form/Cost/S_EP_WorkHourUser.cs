using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Formula;
using Formula.Helper;
using Config.Logic;
using Config;
using System.Data;
using Formula.Exceptions;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_WorkHourUser : BaseEPModel
    {
        public S_EP_WorkHourUser(DataRow dr)
        {
            FillModel(dr);
        }

        public S_EP_WorkHourUser(Dictionary<string, object> dic)
        {
            FillModel(dic);
        }

        public void Revert()
        {
            var costUnitDic = this.GetDataDicByID("S_EP_CostUnit", this.ModelDic["CostUnitInfo"].ToString());
            if (costUnitDic == null) return;
            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", costUnitDic.GetValue("CBSInfoID"));
            if (cbsInfoDic == null) return;
            this.DB.ExecuteNonQuery(String.Format("delete from S_EP_CostInfo where RelateID='{0}'", this.ID));
            this.DB.ExecuteNonQuery(String.Format("delete from S_EP_WorkHourUser where ID='{0}'", this.ID));
            var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
            cbsInfo.SummaryCostValue();
        }
    }
}
