using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_RevenueInfo : BaseEPModel
    {
        public S_EP_RevenueInfo()
        {

        }

        public S_EP_RevenueInfo(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            this.ModelDic.SetValue("State", "Finish");
            this.ModelDic.SetValue("FlowPhase", "End");
            this.ModelDic.UpdateDB(this.DB, "S_EP_RevenueInfo", this.ID);
            string sql = "SELECT * FROM S_EP_RevenueInfo_Detail WHERE S_EP_RevenueInfoID='{0}'";
            var detailDt = this.DB.ExecuteDataTable(String.Format(sql, this.ID));
            var incomeUnitIDs = detailDt.AsEnumerable().Select(c => c["IncomeUnitID"].ToString()).Distinct().ToList();
            var cbsInfoIDs = detailDt.AsEnumerable().Select(c => c["CBSInfoID"].ToString()).Distinct().ToList();
            foreach (var unitID in incomeUnitIDs)
            {
                //重新计算收入单元的累计确认收入
                this.DB.ExecuteNonQuery(@"
                UPDATE S_EP_IncomeUnit
set TotalValue=(SELECT ISNULL(Sum(IncomeValue),0) 
FROM S_EP_RevenueInfo_Detail LEFT JOIN  S_EP_RevenueInfo 
ON S_EP_RevenueInfo.ID=S_EP_RevenueInfoID WHERE IncomeUnitID=S_EP_IncomeUnit.ID AND S_EP_RevenueInfo.STATE='Finish'),
TotalClearValue=(SELECT ISNULL(Sum(ClearIncomeValue),0) 
FROM S_EP_RevenueInfo_Detail LEFT JOIN  S_EP_RevenueInfo 
ON S_EP_RevenueInfo.ID=S_EP_RevenueInfoID WHERE IncomeUnitID=S_EP_IncomeUnit.ID AND S_EP_RevenueInfo.STATE='Finish'),
TotalScale=(SELECT ISNULL(MAX(TotalScale),0) 
FROM S_EP_RevenueInfo_Detail LEFT JOIN  S_EP_RevenueInfo
ON S_EP_RevenueInfo.ID=S_EP_RevenueInfoID WHERE IncomeUnitID=S_EP_IncomeUnit.ID AND S_EP_RevenueInfo.STATE='Finish')
WHERE ID='" + unitID + "'");
            }

            foreach (var cbsInfoID in cbsInfoIDs)
            {
                //汇总核算项目上的累计确认收入数据
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSInfo
set IncomeValue= (select isnull(Sum(IncomeValue),0) from
(select IncomeValue,CBSInfoID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='Finish') RevInfo
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID),
IncomeTaxValue= (select isnull(Sum(TaxValue),0) from
(select TaxValue,CBSInfoID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='Finish') RevInfo
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID),
IncomeClearValue= (select isnull(Sum(ClearIncomeValue),0) from
(select ClearIncomeValue,CBSInfoID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='Finish') RevInfo
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID) where ID='{0}'", cbsInfoID));

                //汇总收入数据至CBS节点上
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set IncomeValue=(select isnull(Sum(IncomeValue),0) from
(select IncomeValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%'),
IncomeTaxValue=(select isnull(Sum(TaxValue),0) from
(select TaxValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%') ,
IncomeClearValue=(select isnull(Sum(ClearIncomeValue),0) from
(select ClearIncomeValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%') 
where CBSInfoID='{0}' and NodeType!='{2}'", cbsInfoID, "Finish", CBSNodeType.Subject.ToString()));
            }
        }

        public void Remove()
        {
            #region 根据全局定义的撤销校验模式，来确认是否校验撤销收入的方式
            if (SysParams.Params.GetValue("IncomeRemoveValidateMode") == IncomeValidateMode.OnlyAfter.ToString())
            {
                //后月份校验，暨当所需撤销的收入之后的月份有收入时，禁止删除
                var existRevInfo = this.DB.ExecuteDataTable(String.Format(@"SELECT  TOP 1 * FROM S_EP_RevenueInfo WITH(NOLOCK) 
                WHERE IncomeDate>'{0}' AND STATE!='{1}' ORDER BY ID DESC"
               , this.ModelDic.GetValue("IncomeDate"), ModifyState.Removed.ToString()));
                if (existRevInfo.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("【" + existRevInfo.Rows[0]["BelongYear"] + "】年【" + existRevInfo.Rows[0]["BelongMonth"] + "】月已经有收入数据，不能撤销，只能撤销最近一个月的收入");
                }
            }
            else if (SysParams.Params.GetValue("IncomeRemoveValidateMode") == IncomeValidateMode.BeforeAndAfter.ToString())
            {
                var existRevInfo = this.DB.ExecuteDataTable(String.Format(@"SELECT  TOP 1 * FROM S_EP_RevenueInfo WITH(NOLOCK) 
                WHERE IncomeDate>'{0}' AND IncomeDate<'{0}'  AND STATE!='{1}' ORDER BY ID DESC"
             , this.ModelDic.GetValue("IncomeDate"), ModifyState.Removed.ToString()));
                if (existRevInfo.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("【" + existRevInfo.Rows[0]["BelongYear"] + "】年【" + existRevInfo.Rows[0]["BelongMonth"] + "】月已经有收入数据，不能撤销，只能撤销最近一个月的收入");
                }
            }
            else if (SysParams.Params.GetValue("IncomeRemoveValidateMode") == IncomeValidateMode.OnlyBefore.ToString())
            {
                var existRevInfo = this.DB.ExecuteDataTable(String.Format(@"SELECT  TOP 1 * FROM S_EP_RevenueInfo WITH(NOLOCK) 
                WHERE IncomeDate<'{0}'  AND STATE!='{1}' ORDER BY ID DESC"
             , this.ModelDic.GetValue("IncomeDate"), ModifyState.Removed.ToString()));
                if (existRevInfo.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("【" + existRevInfo.Rows[0]["BelongYear"] + "】年【" + existRevInfo.Rows[0]["BelongMonth"] + "】月已经有收入数据，不能撤销，只能撤销最近一个月的收入");
                }
            }
            #endregion

            var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_RevenueInfo_Detail WHERE S_EP_RevenueInfoID='{0}'", this.ID));
            var unitIDs = dt.AsEnumerable().Select(c => c["IncomeUnitID"].ToString()).Distinct().ToList();
            var cbsInfoIDs = dt.AsEnumerable().Select(c => c["CBSInfoID"].ToString()).Distinct().ToList();
            if (this.ModelDic.GetValue("State") == "Finish")
            {
                //已经发布过的收入数据不做数据真删除
                this.DB.ExecuteNonQuery(String.Format("update S_EP_RevenueInfo set State='Removed' where ID='{0}'", this.ID));
            }
            else
            {
                //未发布的编制中的数据可以随时删除
                this.ModelDic.DeleteDB(this.DB, "S_EP_RevenueInfo", this.ID);
            }

            foreach (var unitID in unitIDs)
            {
                //重新计算收入单元的累计确认收入
                this.DB.ExecuteNonQuery(@"
          UPDATE S_EP_IncomeUnit
set TotalValue=(SELECT ISNULL(Sum(IncomeValue),0) 
FROM S_EP_RevenueInfo_Detail LEFT JOIN  S_EP_RevenueInfo 
ON S_EP_RevenueInfo.ID=S_EP_RevenueInfoID WHERE IncomeUnitID=S_EP_IncomeUnit.ID AND S_EP_RevenueInfo.STATE='Finish'),
TotalClearValue=(SELECT ISNULL(Sum(ClearIncomeValue),0) 
FROM S_EP_RevenueInfo_Detail LEFT JOIN  S_EP_RevenueInfo 
ON S_EP_RevenueInfo.ID=S_EP_RevenueInfoID WHERE IncomeUnitID=S_EP_IncomeUnit.ID AND S_EP_RevenueInfo.STATE='Finish'),
TotalScale=(SELECT ISNULL(MAX(TotalScale),0) 
FROM S_EP_RevenueInfo_Detail LEFT JOIN  S_EP_RevenueInfo
ON S_EP_RevenueInfo.ID=S_EP_RevenueInfoID WHERE IncomeUnitID=S_EP_IncomeUnit.ID AND S_EP_RevenueInfo.STATE='Finish')
WHERE ID='" + unitID + "'");
            }

            foreach (var cbsInfoID in cbsInfoIDs)
            {
                //汇总核算项目上的累计确认收入数据
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSInfo
set IncomeValue= (select isnull(Sum(IncomeValue),0) from
(select IncomeValue,CBSInfoID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='Finish') RevInfo
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID),
IncomeTaxValue= (select isnull(Sum(TaxValue),0) from
(select TaxValue,CBSInfoID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='Finish') RevInfo
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID),
IncomeClearValue= (select isnull(Sum(ClearIncomeValue),0) from
(select ClearIncomeValue,CBSInfoID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='Finish') RevInfo
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID) where ID='{0}'", cbsInfoID));

                //汇总收入数据至CBS节点上
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set IncomeValue=(select isnull(Sum(IncomeValue),0) from
(select IncomeValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%'),
IncomeTaxValue=(select isnull(Sum(TaxValue),0) from
(select TaxValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%'),
IncomeClearValue=(select isnull(Sum(ClearIncomeValue),0) from
(select ClearIncomeValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%') 
where CBSInfoID='{0}' and NodeType!='{2}'", cbsInfoID, "Finish", CBSNodeType.Subject.ToString()));
            }
        }
    }
}
