using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Newtonsoft.Json;
using System.Data;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_SubContractSettlement : BaseEPModel
    {
        public void Push()
        {
            var confirmDate = DateTime.Now.Date;
            var settlementID = string.Empty;
            var diffValue = 0m;
            var costChangeValue = 0m;
            var taxRate = 0m;
            var clearValue = 0m;
            var taxValue = 0m;
            var userInfo = FormulaHelper.GetUserInfo();
            var sql = string.Format(@"select Detail.*,SupplierContract.ProjectInfo,SupplierContract.TaxRate from (
select * from S_EP_SubContractSettlement_Detail where S_EP_SubContractSettlementID='{0}' ) Detail 
left join S_SP_SupplierContract SupplierContract on Detail.SubContract=SupplierContract.ID
order by SortIndex desc  ", ModelDic.GetValue("ID"));
            var dt = DB.ExecuteDataTable(sql);

            var list = FormulaHelper.DataTableToListDic(dt);
            foreach (var item in list)
            {
                costChangeValue = Convert.ToDecimal(item.GetValue("CostChangeValue"));

                #region 计算【税金】【去税金额】
                taxRate = string.IsNullOrEmpty(item.GetValue("TaxRate")) ? 0m : Convert.ToDecimal(item.GetValue("TaxRate"));//税率
                taxValue = costChangeValue * taxRate / (1 + taxRate);//税金  
                clearValue = costChangeValue / (1 + taxRate);//去税金额

                costChangeValue = Math.Round(costChangeValue, 2);
                taxValue = Math.Round(taxValue, 2);
                clearValue = Math.Round(clearValue, 2);
                #endregion

                #region 核算成本
                sql = string.Format(@"select CBSNode.*,CostUnit.ID as CostUnitID from
(select * from S_EP_CBSNode where ID='{0}') CBSNode 
left join S_EP_CostUnit CostUnit on CBSNode.ID=CostUnit.CBSNodeID ", item.GetValue("ProjectInfo"));
                dt = DB.ExecuteDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到合同对应的CBS节点，无法结算！");
                }
                var unitCBSNode = FormulaHelper.DataRowToDic(dt.Rows[0]);

                var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", unitCBSNode.GetValue("CBSInfoID"));
                if (cbsInfoDic == null)
                {
                    throw new Formula.Exceptions.BusinessValidationException("没有找到对应的核算项目，无法结算！");
                }

                var costState = "Finish";
                var costDetailDic = new Dictionary<string, object>();
                costDetailDic.SetValue("Name", "采购分包费");
                costDetailDic.SetValue("Code", unitCBSNode.GetValue("Code"));
                costDetailDic.SetValue("SubjectCode", unitCBSNode.GetValue("Code"));
                costDetailDic.SetValue("CBSFullCode", unitCBSNode.GetValue("FullCode"));
                costDetailDic.SetValue("CBSNodeID", unitCBSNode.GetValue("ID"));
                costDetailDic.SetValue("CBSNodeFullID", unitCBSNode.GetValue("FullID"));
                costDetailDic.SetValue("CostType", SubjectType.SubContractCost.ToString());
                costDetailDic.SetValue("CBSInfoID", unitCBSNode.GetValue("CBSInfoID"));
                costDetailDic.SetValue("CostUnitID", unitCBSNode.GetValue("CostUnitID"));
                costDetailDic.SetValue("RelateID", item.GetValue("ID"));
                costDetailDic.SetValue("CostDate", confirmDate);
                costDetailDic.SetValue("BelongYear", confirmDate.Year);
                costDetailDic.SetValue("BelongMonth", confirmDate.Month);
                costDetailDic.SetValue("BelongQuarter", (confirmDate.Month + 2) / 3);
                costDetailDic.SetValue("TotalPrice", costChangeValue);
                costDetailDic.SetValue("TaxRate", taxRate);
                costDetailDic.SetValue("TaxValue", taxValue);
                costDetailDic.SetValue("ClearValue", clearValue);
                costDetailDic.SetValue("BelongDept", unitCBSNode.GetValue("ChargerDept"));
                costDetailDic.SetValue("BelongDeptName", unitCBSNode.GetValue("ChargerDeptName"));
                costDetailDic.SetValue("BelongUser", unitCBSNode.GetValue("ChargerUser"));
                costDetailDic.SetValue("BelongUserName", unitCBSNode.GetValue("ChargerUserName"));
                costDetailDic.SetValue("State", costState);
                costDetailDic.SetValue("Status", costState);
                costDetailDic.SetValue("CreateUser", userInfo.UserID);
                costDetailDic.SetValue("CreateUserName", userInfo.UserName);
                costDetailDic.SetValue("CreateDate", DateTime.Now);
                costDetailDic.InsertDB(this.DB, "S_EP_CostInfo");
                var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
                cbsInfo.SummaryCostValue();


                #endregion

                sql = string.Format(@"update S_EP_SubContractSettlement_Detail set Status='End',BelongDate='{1}',CBSInfoID='{2}',CBSNodeID='{3}' 
where ID='{0}'  ", item.GetValue("ID"), confirmDate.ToString("yyyy-MM-dd"), unitCBSNode.GetValue("CBSInfoID"), unitCBSNode.GetValue("ID"));
                DB.ExecuteNonQuery(sql);
            }

        }
    }
}
