using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfficeAuto.Logic.Domain
{
    public partial class T_F_LoanApply
    {
        public void Push()
        {
            var entities = Formula.FormulaHelper.GetEntities<OfficeAutoEntities>();
            var loan = entities.S_FC_UserLoanAccount.Create();
            if (!this.FactValue.HasValue) { throw new Formula.Exceptions.BusinessException("实际借出金额不能为空"); }
            loan.ID = Formula.FormulaHelper.CreateGuid();
            loan.UserID = this.ApplyUser;
            loan.UserName = this.ApplyUserName;
            loan.AccountType = LoanAccountType.借款.ToString();
            loan.AccountValue = this.FactValue.Value;
            loan.RelateFormID = this.ID;
            loan.UserDeptID = this.ApplyDept;
            loan.UserDeptName = this.ApplyDeptName;
            loan.CreateDate = DateTime.Now;
            entities.S_FC_UserLoanAccount.Add(loan);
        }
    }
}
