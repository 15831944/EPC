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
    public partial class S_EP_RevenueAdjustInfo : BaseEPModel
    {
        public S_EP_RevenueAdjustInfo()
        {

        }

        public S_EP_RevenueAdjustInfo(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            var detailDt = this.DB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_RevenueAdjustInfo_Detail WHERE S_EP_RevenueAdjustInfoID='{0}' and AdjustValue is not null and AdjustValue<>0 ", this.ID));
            foreach (DataRow row in detailDt.Rows)
            {
                if (row["MoveToRevenueInfo"] == DBNull.Value || String.IsNullOrEmpty(row["MoveToRevenueInfo"].ToString()))
                {
                    //变更收入确认的当期确认金额和当期末累计确认金额
                    string sql = @"update S_EP_RevenueInfo_Detail
set IncomeValue={0},
CurrentIncomeTotalValue={1},
TaxValue= TaxRate * {0}/(TaxRate + 1.0),ClearIncomeValue= {0}/(TaxRate + 1.0)
where ID='{2}' ";
                    sql = String.Format(sql,
                        row["IncomeValueNew"] == null || row["IncomeValueNew"] == DBNull.Value ? 0 : row["IncomeValueNew"],
                          row["CurrentIncomeTotalValue"] == null || row["CurrentIncomeTotalValue"] == DBNull.Value ? 0 : row["CurrentIncomeTotalValue"],
                          row["RevenueInfoDetailID"]
                        );
                    this.DB.ExecuteNonQuery(sql);
                }
                else
                {
                    var targetIncomeDt = this.DB.ExecuteDataTable(String.Format("select * from S_EP_RevenueInfo_Detail where S_EP_RevenueInfoID='{0}' and IncomeUnitID='{1}'", row["MoveToRevenueInfo"], row["IncomeUnitID"]));
                    var incomeValueNew = row["IncomeValueNew"] == null || row["IncomeValueNew"] == DBNull.Value ? 0m : Convert.ToDecimal(row["IncomeValueNew"]);
                    var incomeValueOrl = row["IncomeValueOrl"] == null || row["IncomeValueOrl"] == DBNull.Value ? 0m : Convert.ToDecimal(row["IncomeValueOrl"]);
                    if (incomeValueOrl == incomeValueNew)
                    {
                        //整个移动
                        //判定目标月份的确认收入中是否已经存在了这个收入单元的收入，如果没有则直接移动追加，如果有，则需要合并收入
                        if (targetIncomeDt.Rows.Count == 0)
                        {
                            string sql = @"update S_EP_RevenueInfo_Detail
set IncomeValue={0},
S_EP_RevenueInfoID='{3}',
CurrentIncomeTotalValue={1},
TaxValue= TaxRate * {0}/(TaxRate + 1.0),ClearIncomeValue= {0}/(TaxRate + 1.0)
where ID='{2}' ";
                            sql = String.Format(sql,
                              row["IncomeValueNew"] == null || row["IncomeValueNew"] == DBNull.Value ? 0 : row["IncomeValueNew"],
                                row["CurrentIncomeTotalValue"] == null || row["CurrentIncomeTotalValue"] == DBNull.Value ? 0 : row["CurrentIncomeTotalValue"],
                                row["RevenueInfoDetailID"], row["MoveToRevenueInfo"]
                              );
                            this.DB.ExecuteNonQuery(sql);
                        }
                        else
                        {
                            var sql = @"update S_EP_RevenueInfo_Detail 
set IncomeValue= IncomeValue + {0},
CurrentIncomeTotalValue=LastIncomeTotalValue+IncomeValue+{0},
TaxValue= TaxRate * (IncomeValue + {0})/(TaxRate + 1.0),ClearIncomeValue= (IncomeValue + {0})/(TaxRate + 1.0)
where ID='{1}' ";
                            sql = String.Format(sql,
                             row["IncomeValueNew"] == null || row["IncomeValueNew"] == DBNull.Value ? 0 : row["IncomeValueNew"],
                                  targetIncomeDt.Rows[0]["ID"]
                             );
                            this.DB.ExecuteNonQuery(sql);
                            this.DB.ExecuteNonQuery(String.Format("delete from S_EP_RevenueInfo_Detail where ID='{0}'", row["ID"]));
                        }
                    }
                    else
                    {
                        var remainValue = incomeValueOrl - incomeValueNew;
                        //部分金额移动
                        if (targetIncomeDt.Rows.Count == 0)
                        {
                            string sql = @"update S_EP_RevenueInfo_Detail
set IncomeValue={0},CurrentIncomeTotalValue=LastIncomeTotalValue+{0},TaxValue= TaxRate * {0}/(TaxRate + 1.0),ClearIncomeValue= {0}/(TaxRate + 1.0) where ID='{1}' ";
                            sql = String.Format(sql, remainValue, row["RevenueInfoDetailID"]);
                            this.DB.ExecuteNonQuery(sql);

                            var revDetailrow = this.DB.ExecuteDataTable("select * from S_EP_RevenueInfo_Detail where ID='" + row["RevenueInfoDetailID"] + "'").AsEnumerable().FirstOrDefault();
                            if (revDetailrow != null)
                            {
                                var dic = FormulaHelper.DataRowToDic(revDetailrow);
                                dic.SetValue("IncomeValue", incomeValueNew);
                                var lastTotalValue = String.IsNullOrEmpty(dic.GetValue("LastIncomeTotalValue")) ? 0m : Convert.ToDecimal(dic.GetValue("LastIncomeTotalValue"));
                                dic.SetValue("CurrentIncomeTotalValue", lastTotalValue + incomeValueNew);
                                dic.SetValue("S_EP_RevenueInfoID", row["MoveToRevenueInfo"]);
                                dic.InsertDB(this.DB, "S_EP_RevenueInfo_Detail");
                            }
                        }
                        else
                        {
                            string sql = @"update S_EP_RevenueInfo_Detail
set IncomeValue={0},CurrentIncomeTotalValue=LastIncomeTotalValue+{0},TaxValue= TaxRate * {0}/(TaxRate + 1.0),ClearIncomeValue= {0}/(TaxRate + 1.0) where ID='{1}' ";
                            sql = String.Format(sql, remainValue, row["RevenueInfoDetailID"]);
                            this.DB.ExecuteNonQuery(sql);

                            sql = @"update S_EP_RevenueInfo_Detail
set IncomeValue=IncomeValue+{0},CurrentIncomeTotalValue=LastIncomeTotalValue+IncomeValue+{0},TaxValue= TaxRate * (IncomeValue + {0})/(TaxRate + 1.0),ClearIncomeValue= (IncomeValue + {0})/(TaxRate + 1.0) where ID='{1}' ";
                            sql = String.Format(sql, incomeValueNew, targetIncomeDt.Rows[0]["ID"]);
                            this.DB.ExecuteNonQuery(sql);
                        }
                    }
                }
            }

            var incomeUnitIDs = detailDt.AsEnumerable().Select(c => c["IncomeUnitID"].ToString()).ToList();
            var cbsInfoIDs = detailDt.AsEnumerable().Select(c => c["CBSInfoID"].ToString()).Distinct().ToList();
            foreach (var unitID in incomeUnitIDs)
            {
                //重新计算收入单元的累计确认收入
                this.DB.ExecuteNonQuery(@"
                UPDATE S_EP_IncomeUnit
set TotalValue=(SELECT ISNULL(Sum(CurrentIncomeTotalValue),0) 
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
where RevInfo.CBSInfoID = S_EP_CBSInfo.ID)  where ID='{0}'", cbsInfoID));

                //汇总收入数据至CBS节点上
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CBSNode
set IncomeValue=(select isnull(Sum(IncomeValue),0) from
(select IncomeValue,CBSNodeFullID from S_EP_RevenueInfo_Detail
left join S_EP_RevenueInfo on S_EP_RevenueInfoID=S_EP_RevenueInfo.ID where State='{1}') RevInfo
where RevInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%') 
where CBSInfoID='{0}' and NodeType!='{2}'", cbsInfoID, "Finish", CBSNodeType.Subject.ToString()));
            }

            //重新汇总当期的收入确认信息汇总数据
            this.DB.ExecuteNonQuery(String.Format(@"update S_EP_RevenueInfo
set SumIncomeValue = (select isnull(Sum(IncomeValue),0) from S_EP_RevenueInfo_Detail
where S_EP_RevenueInfoID=S_EP_RevenueInfo.ID)
where ID='{0}'", this.ModelDic.GetValue("RevenueInfoID")));

        }
    }
}
