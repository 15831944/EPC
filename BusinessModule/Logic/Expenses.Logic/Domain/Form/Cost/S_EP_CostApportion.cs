using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using Formula.Exceptions;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_CostApportion : BaseEPModel
    {
        public S_EP_CostApportion()
        {

        }

        public S_EP_CostApportion(Dictionary<string, object> model)
        {
            this.FillModel(model);
        }


        public Dictionary<string, object> GetCtrlSourceAttrDic()
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            string attrStr = this.ModelDic.GetValue("CtrlSourceAttr");
            if (!string.IsNullOrEmpty(attrStr))
            {
                res = JsonHelper.ToObject(attrStr);
            }
            return res;
        }


        public void Push()
        {
            var userInfo = FormulaHelper.GetUserInfo();
            string sql = "select * from S_EP_CostApportion_Detail where S_EP_CostApportionID = '" + this.ModelDic.GetValue("ID") + "'";
            var dt = this.DB.ExecuteDataTable(sql);

            var allUnitChildCBSNodeDt = this.DB.ExecuteDataTable(@"select S_EP_CBSNode.ID as CBSNodeID,S_EP_CBSNode.FullID as CBSNodeFullID,S_EP_CBSNode.Name,S_EP_CBSNode.SubjectCode,S_EP_CBSNode.SubjectType,S_EP_CBSNode.FullCode as CBSFullCode,
S_EP_CBSNode.CBSInfoID,S_EP_CostUnit.ID as UnitID,S_EP_CBSNode.NodeType from S_EP_CostUnit  with(nolock)
left join S_EP_CBSNode with(nolock) on S_EP_CBSNode.FullID like S_EP_CostUnit.CBSNodeFullID+'%'").AsEnumerable();
            var costUnitIDs = dt.AsEnumerable().Select(c => c["CostUnitID"].ToString()).Distinct().ToList();
            var cbsInfoIDList = new List<string>();

            string insertSql = "";
            foreach (DataRow row in dt.Rows)
            {
                var costInfo = FormulaHelper.DataRowToDic(row);
                if (String.IsNullOrEmpty(costInfo.GetValue("CBSInfoID")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("分摊时，必需指定核算项目的ID");
                }

                //记录下本次参与分摊的核算项目ID，以便后续做成本汇总用
                if (!cbsInfoIDList.Exists(c => c == costInfo.GetValue("CBSInfoID")))
                    cbsInfoIDList.Add(costInfo.GetValue("CBSInfoID"));

                costInfo.SetValue("ID", FormulaHelper.CreateGuid());
                if (String.IsNullOrEmpty(costInfo.GetValue("CostUnitID")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("分摊时必须指定成本单元ID");
                }
                costInfo.SetValue("CostType", this.ModelDic.GetValue("CostType"));
                costInfo.SetValue("RelateID", row["ID"]);
                costInfo.SetValue("CostDate", DateTime.Now);
                costInfo.SetValue("UserDept", costInfo.GetValue("BelongDept"));
                costInfo.SetValue("UserDeptName", costInfo.GetValue("BelongDeptName"));
                costInfo.SetValue("BelongYear", this.ModelDic.GetValue("BelongYear"));
                costInfo.SetValue("BelongMonth", this.ModelDic.GetValue("BelongMonth"));
                if (!String.IsNullOrEmpty(costInfo.GetValue("BelongMonth")))
                {
                    costInfo.SetValue("BelongQuarter", (Convert.ToInt32(costInfo.GetValue("BelongMonth")) - 1) / 3 + 1);
                }
                costInfo.SetValue("State", "Finish");
                costInfo.SetValue("Status", "Finish");
                costInfo.SetValue("CloseUser", userInfo.UserID);
                costInfo.SetValue("CloseUserName", userInfo.UserName);
                costInfo.SetValue("CreateUser", userInfo.UserID);
                costInfo.SetValue("CreateUserName", userInfo.UserName);
                costInfo.SetValue("CreateDate", DateTime.Now);
                //如果指定了科目编码，成本则默认绑定至成本单元下的指定科目
                var subjectNode = allUnitChildCBSNodeDt.FirstOrDefault(c => c["SubjectType"] != DBNull.Value && c["SubjectType"].ToString() == costInfo.GetValue("SubjectType")
                     && c["UnitID"].ToString() == costInfo.GetValue("CostUnitID"));

                if (subjectNode == null)
                {
                    subjectNode = allUnitChildCBSNodeDt.FirstOrDefault(c => c["SubjectType"] != DBNull.Value && c["SubjectType"].ToString() == SubjectType.InDirectCost.ToString()
                     && c["UnitID"].ToString() == costInfo.GetValue("CostUnitID"));

                    if (subjectNode == null)
                        throw new Formula.Exceptions.BusinessValidationException("未找到SubjectType为【" + costInfo.GetValue("SubjectType") + "】和【" + SubjectType.InDirectCost.ToString() + "】的CBSNode,请检查分摊定义");
                }
                costInfo.SetValue("CBSNodeID", subjectNode["CBSNodeID"]);
                costInfo.SetValue("CBSNodeFullID", subjectNode["CBSNodeFullID"]);
                costInfo.SetValue("CBSFullCode", subjectNode["CBSFullCode"]);
                costInfo.SetValue("SubjectCode", subjectNode["SubjectCode"]);

                insertSql += costInfo.CreateInsertSql(this.DB, "S_EP_CostInfo", costInfo.GetValue("ID")) + " ";
            }
            if (!string.IsNullOrEmpty(insertSql))
                this.DB.ExecuteNonQuery(insertSql);

            //汇总核算项目的成本，及各个CBS节点的成本属性
            sql = @"update S_EP_CBSNode
set CostValue= (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%')
where CBSInfoID='{0}'";
            var cbsInfoSql = @"update S_EP_CBSInfo
set CostValue= (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSInfoID = S_EP_CBSInfo.ID) 
where ID='{0}'";
            foreach (var cbsInfoID in cbsInfoIDList)
            {
                this.DB.ExecuteNonQuery(String.Format(sql, cbsInfoID));
                this.DB.ExecuteNonQuery(String.Format(cbsInfoSql, cbsInfoID));
            }

            //汇总成本单元上的总成本数据
            foreach (var unitID in costUnitIDs)
            {
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CostUnit 
set TotalCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID)
where ID='{0}'", unitID));
            }

            this.DB.ExecuteNonQuery(String.Format("update S_EP_CostApportion set State='Finish' where ID='{0}'", this.ID));
        }

        public void Remove()
        {
            string sql = @"select S_EP_CostInfo.* from S_EP_CostApportion_Detail
left join S_EP_CostInfo on S_EP_CostApportion_Detail.ID=S_EP_CostInfo.RelateID where S_EP_CostApportionID='{0}'";
            var costInfoDt = this.DB.ExecuteDataTable(String.Format(sql, this.ID));
            var costUnitIDs = costInfoDt.AsEnumerable().Select(c => c["CostUnitID"].ToString()).Distinct().ToList();
            var cbsInfoIDList = costInfoDt.AsEnumerable().Select(c => c["CBSInfoID"].ToString()).Distinct().ToList();

            foreach (DataRow row in costInfoDt.Rows)
            {
                this.DB.ExecuteNonQuery(String.Format("delete from S_EP_CostInfo where ID='{0}'", row["ID"]));
            }

            //汇总核算项目的成本，及各个CBS节点的成本属性
            sql = @"update S_EP_CBSNode
set CostValue= (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSNodeFullID like S_EP_CBSNode.FullID+'%')
where CBSInfoID='{0}'";
            var cbsInfoSql = @"update S_EP_CBSInfo
set CostValue= (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo
where S_EP_CostInfo.CBSInfoID = S_EP_CBSInfo.ID) 
where ID='{0}'";
            foreach (var cbsInfoID in cbsInfoIDList)
            {
                this.DB.ExecuteNonQuery(String.Format(sql, cbsInfoID));
                this.DB.ExecuteNonQuery(String.Format(cbsInfoSql, cbsInfoID));
            }

            //汇总成本单元上的总成本数据
            foreach (var unitID in costUnitIDs)
            {
                this.DB.ExecuteNonQuery(String.Format(@"update S_EP_CostUnit 
set TotalCostValue = (select isnull(Sum(TotalPrice),0) from S_EP_CostInfo where S_EP_CostInfo.CostUnitID=S_EP_CostUnit.ID)
where ID='{0}'", unitID));
            }

            this.DB.ExecuteNonQuery(String.Format("update S_EP_CostApportion set State='Removed' where ID='{0}'", this.ID));

        }
    }
}
