using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;

namespace Market.Logic.Domain
{
    public partial class S_C_PlanReceipt
    {
        /// <summary>
        /// 验证计划收款的信息
        /// </summary>
        public void ValidatePlanReceiptInfo()
        {
            var entities = this.GetMarketContext();          
            if (!string.IsNullOrEmpty(this.Name))
            {
                bool nameIsExist = entities.Set<S_C_PlanReceipt>().Any(p => p.ID != this.ID && p.Name == this.Name);
                if (nameIsExist == true)
                    throw new Formula.Exceptions.BusinessException(string.Format("收款项名称【{0}】已存在，请重新输入！", this.Name));
            }
        }

        ///// <summary>
        ///// 计划收款变更
        ///// </summary>
        //public void UpdateVersion()
        //{
        //    var user = FormulaHelper.GetUserInfo();
        //    var entities = this.GetMarketContext();
        //    S_C_PlanReceiptChangeRecord record = new S_C_PlanReceiptChangeRecord();
        //    record.ID = FormulaHelper.CreateGuid();
        //    record.Importance = this.Importance;
        //    record.ProductionUnitID = this.ProductionUnitID;
        //    record.ProductionUnitName = this.ProductionUnitName;
        //    record.PlanReceiptAmount = this.Amount;
        //    record.DutyType = this.DutyType;
        //    record.FirstDutyManID = this.FirstDutyManID;
        //    record.FirstDutyManName = this.FirstDutyManName;
        //    record.Remark = this.Remark;
        //    record.RiskLevel = this.RiskLevel;
        //    record.State = this.State;
        //    record.PlanReceiptYearMonth = this.PlanReceiptYearMonth;
        //    record.PlanReceiptCode = this.Code;
        //    record.PlanReceiptName = this.Name;
        //    record.PlanReceiptID = this.ID;
        //    record.ChangedOrder = Convert.ToInt32(this.ChangedCount) + 1;
        //    record.CreateDate = DateTime.Now;
        //    record.CreateUser = user.UserName;
        //    record.CreateUserID = user.UserID;
        //    entities.Set<S_C_PlanReceiptChangeRecord>().Add(record);
        //    this.ChangedCount = this.ChangedCount + 1;
        //}

    }
}
