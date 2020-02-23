using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using Config.Logic;
using Formula;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_ProgressConfirmRevert : BaseEPModel
    {
        public void Push()
        {
            string nodeID = this.ModelDic.GetValue("CurrentProgressNode");
            string sql = String.Format("SELECT * FROM S_EP_IncomeUnit_ProgressNode WHERE ID = '{0}'", nodeID);
            var progressNodeDT = this.DB.ExecuteDataTable(sql);
            var progressNodeDic = new Dictionary<string, object>();
            if (progressNodeDT.Rows.Count > 0)
            {
                progressNodeDic = FormulaHelper.DataRowToDic(progressNodeDT.Rows[0]);
            }
            string unitSql = String.Format("SELECT * FROM S_EP_IncomeUnit WHERE ID = '{0}'", progressNodeDic.GetValue("IncomeUnitID"));
            var incomeUnitDT = this.DB.ExecuteDataTable(unitSql);

            string sqlQuery = "";

            //获取上一个带流程审核的节点
            var lastFlowProgressNodeSql = @"select top 1 S_EP_IncomeUnit_ProgressNode.* from S_EP_IncomeUnit_ProgressNode left join S_EP_ProgressConfirm 
                                          on S_EP_ProgressConfirm.CurrentProgressNode = S_EP_IncomeUnit_ProgressNode.ID
                                          where S_EP_ProgressConfirm.ID is not null and S_EP_IncomeUnit_ProgressNode.IncomeUnitID = '{0}' and SortIndex < {1}
                                          order by SortIndex desc";
            var lastFlowProgressNodeDT = this.DB.ExecuteDataTable(string.Format(lastFlowProgressNodeSql, progressNodeDic.GetValue("IncomeUnitID"), progressNodeDic.GetValue("SortIndex")));
            decimal lastFlowProgressNodeSortIndex = 0;
            decimal lastFlowProgressNodeTotalAllScale = 0;
            if (lastFlowProgressNodeDT.Rows.Count > 0)
            {
                decimal.TryParse(lastFlowProgressNodeDT.Rows[0]["SortIndex"].ToString(), out lastFlowProgressNodeSortIndex);
                decimal.TryParse(lastFlowProgressNodeDT.Rows[0]["TotalAllScale"].ToString(), out lastFlowProgressNodeTotalAllScale);
            }

            //更新节点信息
            sqlQuery += String.Format(@"update S_EP_IncomeUnit_ProgressNode Set State = 'Create', FactEndDate = NULL where IncomeUnitID ='{0}' and SortIndex > {1} and SortIndex <= {2} ",
                            progressNodeDic.GetValue("IncomeUnitID"), lastFlowProgressNodeSortIndex, progressNodeDic.GetValue("SortIndex"));

            //更新S_EP_IncomeUnit
            string lastIncomeNodeSql = @"select top 1 TotalAllScale from S_EP_IncomeUnit_ProgressNode where IncomeUnitID = '{0}' and IsIncomeNode = 'true' and SortIndex <= {1} order by SortIndex desc ";
            var lastIncomeNodeTotalAllScaleObj = this.DB.ExecuteScalar(string.Format(lastIncomeNodeSql, progressNodeDic.GetValue("IncomeUnitID"), lastFlowProgressNodeSortIndex));
            decimal lastIncomeNodeTotalAllScale = 0;
            if (lastIncomeNodeTotalAllScaleObj != null)
            {
                decimal.TryParse(lastIncomeNodeTotalAllScaleObj.ToString(), out lastIncomeNodeTotalAllScale);
            }

            sqlQuery += String.Format(@"update S_EP_IncomeUnit Set TotalScale = {1}, TotalIncomeScale = {2} where ID='{0}' ",
                            progressNodeDic.GetValue("IncomeUnitID"), lastFlowProgressNodeTotalAllScale, lastIncomeNodeTotalAllScale
                            );

            //删除当前流程
            sqlQuery += String.Format(@"delete S_EP_ProgressConfirm where CurrentProgressNode='{0}'",
                            nodeID
                            );

            //TODO 产值确认要撤销 S_EP_ProductionSettleValue

            this.DB.ExecuteNonQuery(sqlQuery);
        }
    }
}
