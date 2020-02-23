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
    public partial class S_EP_ReimbursementApply : BaseEPModel
    {
        public S_EP_ReimbursementApply()
        {

        }

        public S_EP_ReimbursementApply(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        public void Push()
        {
            if (!String.IsNullOrEmpty(this.ModelDic.GetValue("ProjectInfo")))
            {
                #region 项目相关报销直接进入项目成本
                //根据项目报销，一定是获取的核算项目下的CBS节点
                string detailSql = @"select S_EP_CBSNode.SubjectCode, S_EP_ReimbursementApply_Details.*,
S_EP_CostUnit.Code as CostUnitCode,
S_EP_CBSNode.ID as CBSNodeID,
S_EP_CBSNode.Code as CBSNodeCode,
S_EP_CBSNode.FullCode as CBSNodeFullCode,
S_EP_CBSNode.FullID as CBSNodeFullID,
S_EP_CBSNode.CBSInfoID,
ExpenseType,SubjectType
from S_EP_ReimbursementApply_Details 
inner join S_EP_CBSNode on S_EP_CBSnode.ID = S_EP_ReimbursementApply_Details.SubjectDetailID
inner join S_EP_CostUnit on S_EP_CostUnit.ID = S_EP_ReimbursementApply_Details.CostUnit 
                               where S_EP_ReimbursementApplyID = '" + this.ModelDic.GetValue("ID") + "'";
                var detailDt = this.DB.ExecuteDataTable(detailSql);
                var detailDicList = FormulaHelper.DataTableToListDic(detailDt);
                var user = FormulaHelper.GetUserInfo();
                var cbsInfoList = new List<Dictionary<string, object>>();

                var dt = this.DB.ExecuteDataTable(" SELECT * FROM S_EP_CostInfo with(nolock) WHERE 1<>1 ");
                foreach (var detailDic in detailDicList)
                {
                    if (!cbsInfoList.Exists(c => c.GetValue("ID") == detailDic.GetValue("CBSInfoID")))
                    {
                        var cbsInfo = this.GetDataDicByID("S_EP_CBSInfo", detailDic.GetValue("CBSInfoID"));
                        if (cbsInfo == null)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("没有找到核算项目信息，无法完成报销");
                        }
                        cbsInfoList.Add(cbsInfo);
                    }
                    #region
                    var row = dt.NewRow();
                    row["ID"] = FormulaHelper.CreateGuid();
                    row["Name"] = detailDic.GetValue("SubjectDetailName");
                    row["Code"] = detailDic.GetValue("CBSNodeCode");
                    row["ExpenseType"] = this.ModelDic.GetValue("ReimbursementType");
                    row["CostType"] = SubjectType.DirectCost.ToString();
                    row["CBSInfoID"] = detailDic.GetValue("CBSInfoID");
                    row["CostUnitID"] = detailDic.GetValue("CostUnit");
                    row["SubjectCode"] = detailDic.GetValue("SubjectCode");
                    row["CBSFullCode"] = detailDic.GetValue("CBSNodeFullCode");
                    row["CBSNodeID"] = detailDic.GetValue("CBSNodeID");
                    row["CBSNodeFullID"] = detailDic.GetValue("CBSNodeFullID");
                    row["RelateID"] = detailDic.GetValue("ID");
                    row["CostDate"] = DateTime.Now;
                    row["BelongYear"] = DateTime.Now.Year;
                    row["BelongMonth"] = DateTime.Now.Month;
                    row["BelongQuarter"] = (DateTime.Now.Month - 1) / 3 + 1;
                    row["TotalPrice"] = detailDic.GetValue("ApplyValue");
                    row["BelongDept"] = detailDic.GetValue("ChargerDept");
                    row["BelongDeptName"] = detailDic.GetValue("ChargerDeptName");
                    row["BelongUser"] = ModelDic.GetValue("ChargerUser");
                    row["BelongUserName"] = ModelDic.GetValue("ChargerUserName");
                    row["UserID"] = this.ModelDic.GetValue("ApplyUser");
                    row["UserName"] = this.ModelDic.GetValue("ApplyUserName");
                    row["UserDept"] = this.ModelDic.GetValue("ApplyDept");
                    row["UserDeptName"] = this.ModelDic.GetValue("ApplyDeptName");
                    row["State"] = "Finish";
                    row["Status"] = "Finish";
                    row["CreateUser"] = user.UserID;
                    row["CreateUserName"] = user.UserName;
                    row["CreateDate"] = DateTime.Now;
                    row["ModifyUser"] = user.UserID;
                    row["ModifyUserName"] = user.UserName;
                    row["ModifyDate"] = DateTime.Now;
                    #endregion
                    dt.Rows.Add(row);
                }
                this.DB.BulkInsertToDB(dt, "S_EP_CostInfo");
                foreach (var item in cbsInfoList)
                {
                    var cbsInfo = new S_EP_CBSInfo(item);
                    cbsInfo.SummaryCostValue();
                }
                #endregion
            }
            else
            {
                //非项目类报销进入间接费用表中
                string detailSql = string.Format(@"select Detail.*,ReimbursementApply.ApplyDate,
ReimbursementApply.ApplyUser,ReimbursementApply.ApplyUserName,ReimbursementApply.ApplyDept,ReimbursementApply.ApplyDeptName
from (select * from S_EP_ReimbursementApply_Details where S_EP_ReimbursementApplyID = '{0}') Detail
left join S_EP_ReimbursementApply ReimbursementApply on  Detail.S_EP_ReimbursementApplyID=ReimbursementApply.ID ", this.ModelDic.GetValue("ID"));
                var detailDt = this.DB.ExecuteDataTable(detailSql);
                var detailDicList = FormulaHelper.DataTableToListDic(detailDt);
                var user = FormulaHelper.GetUserInfo();
                var applyDate = Convert.ToDateTime(detailDicList[0].GetValue("ApplyDate"));
                var dic = new Dictionary<string, object>();
                var sqlInsert = string.Empty;
                foreach (var detailDic in detailDicList)
                {
                    dic.SetValue("CreateDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    dic.SetValue("CreateUserID", user.UserID);
                    dic.SetValue("CreateUser", user.UserName);
                    dic.SetValue("RelateID", detailDic.GetValue("ID"));
                    dic.SetValue("ApplyDate", detailDic.GetValue("ApplyDate"));
                    dic.SetValue("ApplyValue", detailDic.GetValue("ApplyValue"));
                    dic.SetValue("Subject", detailDic.GetValue("SubjectID"));
                    dic.SetValue("SubjectName", detailDic.GetValue("SubjectName"));
                    dic.SetValue("ChargerUser", detailDic.GetValue("ApplyUser"));
                    dic.SetValue("ChargerUserName", detailDic.GetValue("ApplyUserName"));
                    dic.SetValue("ChargerDept", this.ModelDic.GetValue("ApplyDept"));
                    dic.SetValue("ChargerDeptName", this.ModelDic.GetValue("ApplyDeptName"));
                    dic.SetValue("BelongYear", applyDate.Year);
                    dic.SetValue("BelongQuarter", (applyDate.Month + 2) / 3);
                    dic.SetValue("BelongMonth", applyDate.Month);
                    dic.SetValue("Source", "Reimbursement");
                    sqlInsert += dic.CreateInsertSql(this.DB, "S_EP_IndirectExpenses", FormulaHelper.CreateGuid());
                }
                this.DB.ExecuteNonQuery(sqlInsert);

            }
        }

        /// <summary>
        /// 还款
        /// </summary>
        public void DoRepay()
        {
            string ReimbursementClass = this.ModelDic.GetValue("ReimbursementClass");
            if (ReimbursementClass == "冲账")
            {
                SQLHelper OaDb = SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto);
                //获取当前用户欠款
                string sql = @"select isnull(sum(AccountValue),0) 
                            FROM S_FC_UserLoanAccount
                            where UserID='{0}'";
                sql = string.Format(sql, this.ModelDic.GetValue("ApplyUser"));
                decimal DebtAmount = Convert.ToDecimal(OaDb.ExecuteScalar(sql));

                string detailApplyValueSql = "select isnull(sum(ApplyValue),0) from S_EP_ReimbursementApply_Details where S_EP_ReimbursementApplyID = '{0}'";
                detailApplyValueSql = string.Format(detailApplyValueSql, this.ModelDic.GetValue("ID"));
                decimal ApproveAmount = Convert.ToDecimal(this.DB.ExecuteScalar(detailApplyValueSql));

                decimal AccountValue = 0;
                if (ApproveAmount > DebtAmount)
                    AccountValue = -DebtAmount;
                else
                    AccountValue = -ApproveAmount;
                var dic = new Dictionary<string, object>();
                dic.SetValue("ID", FormulaHelper.CreateGuid());
                dic.SetValue("AccountType", "还款");
                dic.SetValue("RelateFormID", this.ModelDic.GetValue("ID"));
                dic.SetValue("UserID", this.ModelDic.GetValue("ApplyUser"));
                dic.SetValue("UserName", this.ModelDic.GetValue("ApplyUserName"));
                dic.SetValue("UserDeptID", this.ModelDic.GetValue("ApplyDept"));
                dic.SetValue("UserDeptName", this.ModelDic.GetValue("ApplyDeptName"));
                dic.SetValue("CreateDate", DateTime.Now);
                dic.SetValue("AccountValue", AccountValue);
                dic.SetValue("AccountValueABS", 0 - AccountValue);
                dic.InsertDB(SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto), "S_FC_UserLoanAccount", dic.GetValue("ID"));
            }
        }
    }
}
