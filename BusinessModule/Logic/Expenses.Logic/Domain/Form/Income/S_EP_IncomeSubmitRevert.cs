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
    public partial class S_EP_IncomeSubmitRevert : BaseEPModel
    {
        public void Push()
        {
            string sqlQuery = "";
            string nodeID = this.ModelDic.GetValue("S_EP_IncomeSubmitID");
            decimal currentScale = 0;
            var dic = this.GetDataDicByID("S_EP_IncomeSubmit", nodeID);
            decimal.TryParse(dic.GetValue("CurrentScale"), out currentScale);
            string incomeUintID = dic.GetValue("IncomeUnitID");
            //更新incomeUnit
            sqlQuery += String.Format(@"update S_EP_IncomeUnit Set TotalScale = TotalScale - {1}, TotalIncomeScale = TotalIncomeScale - {1} where ID='{0}' ",
                            incomeUintID, currentScale
                            );

            //删除当前流程
            sqlQuery += String.Format(@"delete S_EP_IncomeSubmit where ID='{0}'",
                            nodeID
                            );

            this.DB.ExecuteNonQuery(sqlQuery);
        }
    }
}
