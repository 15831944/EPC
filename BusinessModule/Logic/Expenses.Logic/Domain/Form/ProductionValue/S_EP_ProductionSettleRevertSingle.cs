using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_ProductionSettleSingleRevert : BaseEPModel
    {
        public void Push()
        {
            var applyDic = this.GetDataDicByID("S_EP_ProductionSettleApply", this.GetValue("SettleID"));
            if (applyDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的产值确认单，无法进行撤销");
            }
            string sql = String.Format("select * from S_EP_ProductionSettleValue where ConfirmFormID='{0}'", this.GetValue("SettleID"));
            var settleDetailInfo = this.DB.ExecuteDataTable(sql);
            var fullIDList = settleDetailInfo.AsEnumerable().Where(c => c["CBSNodeFullID"] != DBNull.Value).
                Select(c => c["CBSNodeFullID"].ToString()).Distinct().ToList();
            var productionUnitIDList = settleDetailInfo.AsEnumerable().Where(c => c["ProductionUnitID"] != DBNull.Value).
                Select(c => c["ProductionUnitID"].ToString()).Distinct().ToList();
            foreach (DataRow row in settleDetailInfo.Rows)
            {
                var finishNodeID = row["FinishProgressNodeID"] == DBNull.Value ? "" : row["FinishProgressNodeID"].ToString();
                var progressNode = this.GetDataDicByID("S_EP_ProductionUnit_ProgressNode", finishNodeID);
                if (progressNode != null)
                {
                    this.DB.ExecuteNonQuery(String.Format("update S_EP_ProductionUnit_ProgressNode set State='{0}' , FactEndDate=null where ID='{1}'",
                        "Create", progressNode.GetValue("ID")));
                }
            }
            this.DB.ExecuteNonQuery(String.Format("delete from S_EP_ProductionSettleValue where ConfirmFormID='{0}'", this.GetValue("SettleID")));
            foreach (var fullID in fullIDList)
            {
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set ProductionSettleValue=isnull((select Sum(CurrentProductionValue) from S_EP_ProductionSettleValue where CBSNodeFullID like S_EP_CBSNode.FullID+'%'),0) 
where '{0}' like S_EP_CBSNode.FullID+'%'", fullID));

                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set ProductionSettleValue=isnull((select Sum(CurrentProductionValue) from S_EP_ProductionSettleValue where CBSNodeFullID like S_EP_CBSNode.FullID+'%'),0) 
where S_EP_CBSNode.FullID like +'{0}%'", fullID));
            }
            foreach (var item in productionUnitIDList)
            {
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_ProductionUnit
set ProductionSettleValue=isnull((select Sum(CurrentProductionValue) from S_EP_ProductionSettleValue where ProductionUnitID = S_EP_ProductionUnit.ID),0) 
where ID='{0}'", item));
            }

            applyDic.SetValue("State", ModifyState.Removed.ToString());
            applyDic.UpdateDB(this.DB, "S_EP_ProductionSettleApply", applyDic.GetValue("ID"));
        }
    }
}
