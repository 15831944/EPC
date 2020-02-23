using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Formula;
using OfficeAuto.Logic;
using OfficeAuto.Logic.Domain;
using Formula.Helper;
using Config.Logic;
using Config;

namespace OfficeAuto.Logic.Domain
{
    public partial class T_F_ReimbursementApply
    {
        public void Push()
        {
            var entities = Formula.FormulaHelper.GetEntities<OfficeAutoEntities>();
            var marketDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var enumService = FormulaHelper.GetService<IEnumService>();
            if (this.ReimbursementClass == OfficeAuto.Logic.ReimbursementClass.冲账.ToString())
            {
                var loanValue = 0m;
                if (entities.S_FC_UserLoanAccount.Where(d => d.UserID == this.ApplyUser).Count() > 0)
                    loanValue = entities.S_FC_UserLoanAccount.Where(d => d.UserID == this.ApplyUser).Sum(d => d.AccountValue);
                if (loanValue <= 0) { }
                else
                {
                    var loanInfo = entities.S_FC_UserLoanAccount.Create();
                    loanInfo.ID = FormulaHelper.CreateGuid();
                    loanInfo.AccountType = LoanAccountType.还款.ToString();
                    loanInfo.RelateFormID = this.ID;
                    loanInfo.UserDeptID = this.ApplyDept;
                    loanInfo.UserDeptName = this.ApplyDeptName;
                    loanInfo.UserID = this.ApplyUser;
                    loanInfo.UserName = this.ApplyUserName;
                    loanInfo.CreateDate = DateTime.Now;
                    if (this.ReimbursementValue > loanValue)
                    {
                        loanInfo.AccountValue = 0 - loanValue;
                    }
                    else
                    {
                        loanInfo.AccountValue = 0 - Convert.ToDecimal(this.ReimbursementValue);
                    }
                    entities.S_FC_UserLoanAccount.Add(loanInfo);
                }          
            }
            foreach (var detailInfo in this.T_F_ReimbursementApply_Details.ToList())
            {
                var cost = entities.S_FC_CostInfo.Create();
                cost.ID = FormulaHelper.CreateGuid();           
                cost.CostDate = this.ApplyDate.HasValue ? this.ApplyDate.Value : DateTime.Now;
                cost.BelongMonth = cost.CostDate.Month;
                cost.BelongQuarter = (cost.CostDate.Month - 1) / 3 + 1;
                cost.BelongYear = cost.CostDate.Year;
                cost.CostType = this.ReimbursementType;
                cost.CostUserDeptID = this.ApplyDept;
                cost.CostUserDeptName = this.ApplyDeptName;
                cost.CostUserID = this.ApplyUser;
                cost.CostUserName = this.ApplyUserName;
                cost.CostValue = detailInfo.ApplyValue.HasValue ? detailInfo.ApplyValue.Value : 0;     
                if (this.ReimbursementType == "Market" || this.ReimbursementType=="Production")
                {
                    var sql = "select Code,ChargerDeptName,ChargerDept,ChargerUserName,ChargerUser,ProjectClass from S_I_Project where ID='{0}'";
                    if (this.ReimbursementType == "Market")
                    {
                        sql = @"select '' as ProjectClass,SerialNumber as Code,BusinessManagerName as ChargerUserName,
BusinessManager as ChargerUser,DeptInfo as ChargerDept,DeptInfoName as ChargerDeptName
from dbo.S_P_MarketClue where ID='{0}'";
                    }
                    var dt = Config.SQLHelper.CreateSqlHelper(Config.ConnEnum.Market).ExecuteDataTable(String.Format(sql,detailInfo.ProjectInfo));
                    if (dt.Rows.Count > 0)
                    {
                        cost.ProjectChargerUserID = dt.Rows[0]["ChargerUser"].ToString();
                        cost.ProjectChargerUserName = dt.Rows[0]["ChargerUserName"].ToString();
                        cost.ProjectClass = dt.Rows[0]["ProjectClass"].ToString();
                        cost.ProjectCode = dt.Rows[0]["Code"].ToString();
                        cost.ProjectDeptID = dt.Rows[0]["ChargerDept"].ToString();
                        cost.ProjectDeptName = dt.Rows[0]["ChargerDeptName"].ToString();
                    }
                }
                cost.ProjectID = detailInfo.ProjectInfo;
                cost.ProjectName = detailInfo.ProjectInfoName;
                if (!string.IsNullOrEmpty(detailInfo.Dept))
                {
                    cost.ProjectDeptID = detailInfo.Dept;
                    cost.ProjectDeptName = detailInfo.DeptName;
                }
                cost.ProjectType = this.ReimbursementType;
                cost.Quantity = 0;
                cost.SubjectCode = detailInfo.SubjectCode;
                cost.SubjectName = enumService.GetEnumText("System.FinanceSubjects", detailInfo.SubjectCode);
                cost.UnitPrice = 0;
                cost.RelateID = detailInfo.ID;
                cost.FormID = this.ID;
                entities.S_FC_CostInfo.Add(cost);
                var marketDic = cost.ToDic();
                marketDic.InsertDB(marketDB, "S_FC_CostInfo");
            }
        }
    }
}
