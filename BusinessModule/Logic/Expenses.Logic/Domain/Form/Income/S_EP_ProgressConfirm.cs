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
    public partial class S_EP_ProgressConfirm : BaseEPModel
    {
        Dictionary<string, object> _progressNode;
        [NotMapped]
        [JsonIgnore]
        public Dictionary<string, object> ProgressNode
        {
            get
            {
                if (_progressNode == null)
                {
                    var dt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_IncomeUnit_ProgressNode WHERE ID='{0}'", this.ModelDic.GetValue("CurrentProgressNode")));
                    if (dt.Rows.Count > 0)
                    {
                        _progressNode = FormulaHelper.DataRowToDic(dt.Rows[0]);
                    }
                }
                return _progressNode;
            }
        }

        public void Push()
        {
            var progressNode = this.GetDataDicByID("S_EP_IncomeUnit_ProgressNode", this.ModelDic.GetValue("CurrentProgressNode"));
            if (progressNode == null)
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的节点信息，无法确认节点");
            var incomeUnit = this.GetDataDicByID("S_EP_IncomeUnit", this.ModelDic.GetValue("IncomeUnit"));
            if (incomeUnit == null)
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的收入单元，无法确认节点");

            string sql = "";
            //为了支持用户可在表单上修改比例，故此处需要与节点数据做比较
            var currentTotalScale = String.IsNullOrEmpty(this.ModelDic.GetValue("TotalScale")) ? 0m : Convert.ToDecimal(this.ModelDic.GetValue("TotalScale"));
            var nodeScale = String.IsNullOrEmpty(progressNode.GetValue("TotalAllScale")) ? 0m : Convert.ToDecimal(progressNode.GetValue("TotalAllScale"));
            if (nodeScale != currentTotalScale)
            {
                //进度确认单上的比例和节点定义的比例不相同，表示在进度确认过程中，用户手工修改了比例
                //则以手工修改的比例为准，那么下一个节点自动需要扣除多余的比例
                var sd = currentTotalScale - nodeScale;
                var nextNodes = this.DB.ExecuteDataTable(String.Format(@"select top 1 * from S_EP_IncomeUnit_ProgressNode 
where IncomeUnitID='{0}' and SortIndex>{1}", incomeUnit.GetValue("ID"), progressNode.GetValue("SortIndex")));
                if (nextNodes.Rows.Count > 0)
                {
                    if (nextNodes.Rows[0]["IsIncomeNode"].ToString() == true.ToString())
                    {
                        sql += String.Format(@"UPDATE S_EP_IncomeUnit_ProgressNode SET TotalAllScale=TotalAllScale+({1}),AllIncomeScale=AllIncomeScale+({1}) WHERE ID='{0}'",
                             nextNodes.Rows[0]["ID"], sd
                             );
                    }
                    else
                        sql += String.Format(@"UPDATE S_EP_IncomeUnit_ProgressNode SET =TotalAllScale+({1}) WHERE ID='{0}'",
                            nextNodes.Rows[0]["ID"], sd);
                }
                else
                {
                    throw new Formula.Exceptions.BusinessValidationException("最后的节点不能修改确认比例");
                }
            }

            var beforeNodes = this.DB.ExecuteDataTable(String.Format(@"select * from S_EP_IncomeUnit_ProgressNode 
where IncomeUnitID='{0}' and SortIndex<={1} and (State<>'{2}' or State is null )order by sortIndex ", incomeUnit.GetValue("ID"), progressNode.GetValue("SortIndex"), "Finish"));            //优化
            foreach (DataRow node in beforeNodes.Rows)
            {
                if (node["IsIncomeNode"] != DBNull.Value && node["IsIncomeNode"].ToString() == true.ToString().ToLower())
                {
                    this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_IncomeUnit set TotalScale={0},TotalIncomeScale={0} where ID='{1}'", node["TotalAllScale"], incomeUnit.GetValue("ID")));
                }
                this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_IncomeUnit_ProgressNode SET FactEndDate='{1}' ,State='Finish' WHERE ID='{0}'", node["ID"],
                    String.IsNullOrEmpty(this.ModelDic.GetValue("FactEndDate")) ? DateTime.Now.ToString() : this.ModelDic.GetValue("FactEndDate")
                    ));
            }

            if (progressNode.GetValue("IsIncomeNode") == true.ToString().ToLower())
            {
                sql += String.Format("UPDATE S_EP_IncomeUnit set TotalScale={0},TotalIncomeScale={0} where ID='{1}'", currentTotalScale, incomeUnit.GetValue("ID"));
            }
            else
                sql += String.Format("UPDATE S_EP_IncomeUnit set TotalScale={0} where ID='{1}'", currentTotalScale, incomeUnit.GetValue("ID"));

            //如果该单元对应的节点是产值节点则往S_EP_ProductionSettleValue插入数据
            var cbsNodeDt = this.DB.ExecuteDataTable(string.Format(@"select S_EP_CBSNode.*, {0}..S_EP_DefineCBSNode.ProductionUnit from S_EP_CBSNode 
                            inner join {0}..S_EP_DefineCBSNode on {0}..S_EP_DefineCBSNode.ID = S_EP_CBSNode.DefineID where S_EP_CBSNode.ID = '{1}'"
                            , this.InfrasDB.DbName, incomeUnit.GetValue("CBSNodeID")));

            var lastProgressNodeDt = this.DB.ExecuteDataTable(String.Format(@"select top 1 * from S_EP_IncomeUnit_ProgressNode 
where IncomeUnitID='{0}' and SortIndex<{1} order by SortIndex desc", incomeUnit.GetValue("ID"), progressNode.GetValue("SortIndex")));

            if (cbsNodeDt.Rows.Count > 0)
            {
                var cbsNodeRow = cbsNodeDt.Rows[0];
                if (cbsNodeRow["ProductionUnit"].ToString() == "true")
                {
                    var productionUnitDt = this.DB.ExecuteDataTable("select * from S_EP_ProductionUnit where CBSNodeID = '" + cbsNodeRow["ID"].ToString() + "'");
                    if (productionUnitDt.Rows.Count == 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("虽然该节点为产值节点，但无法找到产值单元。");
                    }
                    var productionUnitRow = productionUnitDt.Rows[0];

                    var productionSettleValueDic = new Dictionary<string, object>();
                    productionSettleValueDic.SetValue("ID", FormulaHelper.CreateGuid());
                    productionSettleValueDic.SetValue("CBSInfo", cbsNodeRow["CBSInfoID"]);
                    productionSettleValueDic.SetValue("CBSNodeID", cbsNodeRow["ID"]);
                    productionSettleValueDic.SetValue("CBSNodeFullID", cbsNodeRow["FullID"]);
                    productionSettleValueDic.SetValue("ProductionUnitID", productionUnitRow["ID"]);
                    productionSettleValueDic.SetValue("UnitName", productionUnitRow["Name"]);
                    productionSettleValueDic.SetValue("UnitCode", productionUnitRow["Code"]);
                    productionSettleValueDic.SetValue("Method", "收入节点进度确认");
                    if (lastProgressNodeDt.Rows.Count > 0)
                    {
                        productionSettleValueDic.SetValue("LastScale", lastProgressNodeDt.Rows[0]["AllScale"]);
                    }                    
                    //productionSettleValueDic.SetValue("LastProductionValue", "");//
                    productionSettleValueDic.SetValue("TotalScale", currentTotalScale);
                    //productionSettleValueDic.SetValue("TotalProductionValue", "");//
                    //productionSettleValueDic.SetValue("CurrentProductionValue", "");//
                    productionSettleValueDic.SetValue("ConfirmFormID", this.ModelDic["ID"]);
                    productionSettleValueDic.SetValue("ConfirmDetailID", this.ModelDic["ID"]);
                    productionSettleValueDic.SetValue("FactEndDate", progressNode["FactEndDate"]);
                    productionSettleValueDic.SetValue("ChargerUser", cbsNodeRow["ChargerUser"]);
                    productionSettleValueDic.SetValue("ChargerUserName", cbsNodeRow["ChargerUserName"]);
                    productionSettleValueDic.SetValue("ChargerDept", cbsNodeRow["ChargerDept"]);
                    productionSettleValueDic.SetValue("ChargerDeptName", cbsNodeRow["ChargerDeptName"]);
                    productionSettleValueDic.SetValue("Company", cbsNodeRow["OrgID"]);
                    var companyDt = SQLHelper.CreateSqlHelper(ConnEnum.Base).ExecuteDataTable("select * S_A_Org where ID = '" + cbsNodeRow["OrgID"] + "'");
                    if (companyDt.Rows.Count > 0)
                        productionSettleValueDic.SetValue("CompanyName", companyDt.Rows[0]["Name"]);
                    productionSettleValueDic.SetValue("FinishProgressNodeID", progressNode["ID"]);
                    sql += productionSettleValueDic.CreateInsertSql(this.DB, "S_EP_ProductionSettleValue", productionSettleValueDic.GetValue("ID"));
                }
            }
            this.DB.ExecuteNonQuery(sql);
        }
    }
}
