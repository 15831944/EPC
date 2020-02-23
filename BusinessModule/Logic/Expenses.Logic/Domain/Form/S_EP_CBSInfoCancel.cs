using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_CBSInfoCancel : BaseEPModel
    {
        private Dictionary<string, string> dic;

        public S_EP_CBSInfoCancel()
        {

        }
        public S_EP_CBSInfoCancel(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            string cbsInfoID = this.ModelDic.GetValue("CBSInfo");
            var cbsInfo = this.GetDataDicByID("S_EP_CBSInfo", cbsInfoID);
            if (cbsInfo != null)
            {
                cbsInfo.SetValue("State", "Removed");
                cbsInfo.UpdateDB(this.DB, "S_EP_CBSInfo", cbsInfo.GetValue("ID"));
            }   
        }

        public void Validate()
        {
            string cbsInfoID = this.ModelDic.GetValue("CBSInfo");
            string costInfoSql = string.Format("select ID from S_EP_CostInfo where CBSInfoID = '{0}' and State = 'Finish'", cbsInfoID);
            var costDT = this.DB.ExecuteDataTable(costInfoSql);
            if (costDT.Rows.Count > 0)
            {
                throw new BusinessException("改核算项目已经有成本数据，无法撤销");
            }
            string incomeSql = string.Format(@"select S_EP_RevenueInfo_Detail.ID from S_EP_RevenueInfo_Detail inner join S_EP_RevenueInfo 
                                             on S_EP_RevenueInfo_Detail.S_EP_RevenueInfoID = S_EP_RevenueInfo.ID
                                             where S_EP_RevenueInfo.State = 'Finish' and S_EP_RevenueInfo_Detail.CBSInfoID = '{0}'",cbsInfoID);
            var incomeDT = this.DB.ExecuteDataTable(incomeSql);
            if (incomeDT.Rows.Count > 0)
            {
                throw new BusinessException("改核算项目已经有收入数据，无法撤销");
            }
        }
    }
}
