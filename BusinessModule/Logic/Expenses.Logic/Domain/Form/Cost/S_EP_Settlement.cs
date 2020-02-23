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
    public partial class S_EP_Settlement : BaseEPModel
    {
        public S_EP_Settlement()
        {

        }

        public S_EP_Settlement(Dictionary<string, object> dic)
        {
            FillModel(dic);
        }

        public void Push()
        {
            if (this.ModelDic.GetValue("IsClosed").ToLower() == "true")
            {
                var costUnitID = this.ModelDic.GetValue("CostUnit");
                var sql = "update S_EP_CBSNode set IsClosed = 'true' where ID in (select S_EP_CBSNode.ID from S_EP_CBSNode inner join S_EP_CostUnit on S_EP_CostUnit.CBSNodeID = S_EP_CBSNode.ID where S_EP_CostUnit.ID = '" + costUnitID + "')";
                this.DB.ExecuteNonQuery(sql);
            }            
        }
    }
}
