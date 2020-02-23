using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Formula;

namespace Market.Logic.Domain
{
    /// <summary>
    /// 计划收款
    /// </summary>
    public partial class S_C_PlanReceipt
    {
        #region 公共属性

        S_C_PlanReceipt _relateParentPlan;
        /// <summary>
        /// 所关联的原始滚动计划
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public S_C_PlanReceipt RelateParentPlan
        {
            get
            {
                if (_relateParentPlan == null)
                {
                    if (String.IsNullOrEmpty(this.RelateParentID)) _relateParentPlan = this;
                    var entities = this.GetDbContext<MarketEntities>();
                    _relateParentPlan = entities.S_C_PlanReceipt.SingleOrDefault(d => d.ID == this.RelateParentID);
                    if (_relateParentPlan == null)
                        _relateParentPlan = this;
                }
                return _relateParentPlan;
            }
        }

        #endregion

        #region 公共实例方法


        /// <summary>
        /// 拆分计划收款
        /// </summary>
        /// <param name="splitValue">拆分金额</param>
        /// <param name="planDate">计划日期</param>
        /// <param name="IsPartReceipt">是否部分到款自动拆分</param>
        public S_C_PlanReceipt Split(decimal splitValue, DateTime? planDate = null, bool PartReceiptAutoSplit = false)
        {
            if (this.State != PlanReceiptState.UnReceipt.ToString() && !PartReceiptAutoSplit)
                throw new Formula.Exceptions.BusinessException("只能拆分未到款的记录，操作失败");
            if (splitValue < 0) throw new Formula.Exceptions.BusinessException("拆分金额不能小于0");
            if (splitValue >= this.PlanReceiptValue) throw new Formula.Exceptions.BusinessException("拆分金额【" + splitValue + "】必须小于原计划收款金额【" + this.PlanReceiptValue + "】");
            var marketEntities = this.GetMarketContext();
            var plan = this.Clone<S_C_PlanReceipt>();
            plan.ID = FormulaHelper.CreateGuid();

            if (String.IsNullOrEmpty(this.RelateParentID))
                plan.RelateParentID = this.ID;
            if (!PartReceiptAutoSplit)
                this.PlanReceiptValue = this.PlanReceiptValue - splitValue;
            if (!PartReceiptAutoSplit)
                plan.RelateParentID = plan.ID;
            plan.PlanReceiptValue = splitValue;
            plan.FactReceiptValue = 0;
            plan.PlanReceiptDate = planDate;
            plan.ReceiptDate = null;
            plan.ContractInfoID = this.ContractInfoID;
            plan.ContractName = this.ContractName;
            if (planDate.HasValue)
            {
                var date = Convert.ToDateTime(planDate);
                plan.BelongMonth = date.Month;
                plan.BelongQuarter = MarketHelper.GetQuarter(date);
                plan.BelongYear = date.Year;
                if (date.Month >= 10)
                    plan.PlanReceiptYearMonth = date.Year.ToString() + date.Month.ToString();
                else
                    plan.PlanReceiptYearMonth = date.Year.ToString() + "0" + date.Month.ToString();
            }
            marketEntities.S_C_PlanReceipt.Add(plan);
            return plan;
        }


        /// <summary>
        /// 合并计划收款
        /// </summary>
        /// <param name="planReceiptList">计划收款集合</param>
        public void Combine(List<S_C_PlanReceipt> planReceiptList)
        {
            if (this.State != PlanReceiptState.UnReceipt.ToString())
                throw new Formula.Exceptions.BusinessException("只有未到款的计划收款可以进行合并操作");
            var marketEntities = FormulaHelper.GetEntities<MarketEntities>();
            foreach (var item in planReceiptList)
            {
                if (this.ID == item.ID) continue;
                if (item.State != PlanReceiptState.UnReceipt.ToString())
                    throw new Formula.Exceptions.BusinessException("【" + item.ContractName + "-" + item.Name + "】状态不是未到款，无法进行合并操作");
                if (item.ReceiptObjectID != this.ReceiptObjectID)
                    throw new Formula.Exceptions.BusinessException("非同一收款项的计划收款不能进行合并操作");
                this.PlanReceiptValue += item.PlanReceiptValue;
                marketEntities.S_C_PlanReceipt.Remove(item);
            }
        }

        /// <summary>
        /// 计划收款删除操作
        /// </summary>
        public void Delete()
        {
            var marketEntities = this.GetMarketContext();
            onDelete();
            if (this.State != PlanReceiptState.UnReceipt.ToString())
                throw new Formula.Exceptions.BusinessException("已经关闭的计划收款，不能进行删除操作");
            marketEntities.S_C_PlanReceipt.Remove(this);
            onDeleted();
        }

        /// <summary>
        /// 保存计划收款
        /// </summary>
        public void Save()
        {
            if (this.State != PlanReceiptState.UnReceipt.ToString())
                throw new Formula.Exceptions.BusinessException("已关闭的计划不能进行编辑操作");
            if (!this.PlanReceiptDate.HasValue)
                throw new Formula.Exceptions.BusinessException("保存计划时，必须确定计划收款时间");
            var planDate = Convert.ToDateTime(this.PlanReceiptDate);
            this.BelongYear = planDate.Year;
            this.BelongMonth = planDate.Month;
            this.BelongQuarter = MarketHelper.GetQuarter(planDate);
            if (planDate.Month >= 10)
                this.PlanReceiptYearMonth = planDate.Year.ToString() + planDate.Month.ToString();
            else
                this.PlanReceiptYearMonth = planDate.Year.ToString() + "0" + planDate.Month.ToString();
        }

        /// <summary>
        /// 收款计划延迟
        /// </summary>
        /// <param name="newDate">新日期</param>
        public void Delay(DateTime newDate)
        {
            if (this.PlanReceiptDate.HasValue)
            {
                if (Convert.ToDateTime(this.PlanReceiptDate) > newDate)
                    throw new Formula.Exceptions.BusinessException("延迟的新日期不能早于原计划完成日期");
            }
            var newPlan = this.Clone<S_C_PlanReceipt>();
            newPlan.RelateParentID = this.RelateParentID;
            newPlan.PlanReceiptDate = newDate;
            newPlan.BelongYear = newDate.Year;
            newPlan.BelongMonth = newDate.Month;
            newPlan.BelongQuarter = MarketHelper.GetQuarter(newDate);
            if (newDate.Month >= 10)
                newPlan.PlanReceiptYearMonth = newDate.Year.ToString() + newDate.Month.ToString();
            else
                newPlan.PlanReceiptYearMonth = newDate.Year.ToString() + "0" + newDate.Month.ToString();
            newPlan.ID = FormulaHelper.CreateGuid();
            newPlan.State = PlanReceiptState.UnReceipt.ToString();
            var marketEntities = FormulaHelper.GetEntities<MarketEntities>();
            this.State = PlanReceiptState.UnFinished.ToString();
            marketEntities.S_C_PlanReceipt.Add(newPlan);
            if (this.S_C_ManageContract_ReceiptObj == null) throw new Formula.Exceptions.BusinessException("计划收款未关联到收款项，无法进行延迟操作");
            this.S_C_ManageContract_ReceiptObj.SetPlanFinishDate(newDate);
            if (!this.S_C_ManageContract_ReceiptObj.OriginalPlanFinishDate.HasValue)
                this.S_C_ManageContract_ReceiptObj.OriginalPlanFinishDate = this.PlanReceiptDate;
        }

        /// <summary>
        /// 计划收款坏账标记
        /// </summary>
        /// <param name="badDebtValue">坏账金额(默认为0，当不为0时，如果小于计划收款金额，则自动拆分,如果未0则坏账金额等于计划收款金额)</param>
        public void BadDebt(decimal badDebtValue = 0)
        {
            if (this.State != PlanReceiptState.UnReceipt.ToString())
                throw new Formula.Exceptions.BusinessException("【" + this.Name + "】不是未到款状态，不能进行操作");
            if (badDebtValue > 0)
            {
                if (badDebtValue > this.PlanReceiptValue) throw new Formula.Exceptions.BusinessException("坏账标记金额不能大于计划收款金额");
                var splitValue = this.PlanReceiptValue - badDebtValue;
                this.Split(splitValue, this.PlanReceiptDate);
                this.BadDebtValue = badDebtValue;
                this.IsBadDebt = YesOrNo.Yes.ToString();
                this.State = PlanReceiptState.BadDebt.ToString();
            }
            else
            {
                this.BadDebtValue = this.PlanReceiptValue;
                this.IsBadDebt = YesOrNo.Yes.ToString();
                this.State = PlanReceiptState.BadDebt.ToString();
            }
            if (this.S_C_ManageContract_ReceiptObj == null) throw new Formula.Exceptions.BusinessException("计划未关联到收款项，无法标记坏账");
            this.S_C_ManageContract_ReceiptObj.SummaryBadDebtValue();
            if (this.S_C_ManageContract_ReceiptObj.S_C_ManageContract == null) throw new Formula.Exceptions.BusinessException("未能找到计划关联的合同信息，无法标记坏账");
            this.S_C_ManageContract_ReceiptObj.S_C_ManageContract.SummaryBadDebtData();
        }

        /// <summary>
        /// 撤销坏账
        /// </summary>
        public void RevertBadDebt()
        {
            if (this.State != PlanReceiptState.BadDebt.ToString())
                throw new Formula.Exceptions.BusinessException("非坏账的计划收款不能进行撤销坏账操作");
            this.BadDebtValue = 0;
            this.State = PlanReceiptState.UnReceipt.ToString();
            if (this.S_C_ManageContract_ReceiptObj == null) throw new Formula.Exceptions.BusinessException("计划未关联到收款项，无法撤销坏账");
            this.S_C_ManageContract_ReceiptObj.SummaryBadDebtValue();
            if (this.S_C_ManageContract_ReceiptObj.S_C_ManageContract == null) throw new Formula.Exceptions.BusinessException("未能找到计划关联的合同信息，无法撤销坏账");
            this.S_C_ManageContract_ReceiptObj.S_C_ManageContract.SummaryBadDebtData();
        }

        /// <summary>
        /// 重置计划收款内容（当到款关联被删除时，需要重新计算计划收款的所有相关信息）
        /// 删除所有派生的计划收款，并重新计算计划收款金额
        /// 新的计划收款金额=收款项金额-已收款金额-拆分未到款的计划收款金额总和
        /// </summary>
        public void Reset()
        {
            var marketEntities = this.GetMarketContext();
            var childList = this.S_C_ManageContract_ReceiptObj.S_C_PlanReceipt.Where(d => d.RelateParentID ==
                    this.ID && d.State == PlanReceiptState.UnReceipt.ToString() && d.ID != this.ID).ToList();
            foreach (var childPlan in childList)
                marketEntities.S_C_PlanReceipt.Remove(childPlan);
            this.State = PlanReceiptState.UnReceipt.ToString();
            this.FactReceiptValue = 0;
            this.ReceiptDate = null;
            if (this.S_C_ManageContract_ReceiptObj == null)
                this.S_C_ManageContract_ReceiptObj = marketEntities.S_C_ManageContract_ReceiptObj.SingleOrDefault(d => d.ID == this.ReceiptObjectID);
            if (this.S_C_ManageContract_ReceiptObj == null) throw new Formula.Exceptions.BusinessException("未能找到关联的收款项，无法重置计划");
            this.S_C_ManageContract_ReceiptObj.SummaryReceiptValue();
            var otherList = this.S_C_ManageContract_ReceiptObj.S_C_PlanReceipt.Where(d => d.State == PlanReceiptState.UnReceipt.ToString()
                 && d.ID != this.ID).ToList();
            this.PlanReceiptValue = Convert.ToDecimal(this.S_C_ManageContract_ReceiptObj.ReceiptValue) -
                Convert.ToDecimal(this.S_C_ManageContract_ReceiptObj.FactReceiptValue) - otherList.Sum(d => d.PlanReceiptValue);
        }

        /// <summary>
        /// 重计算本计划到款金额
        /// </summary>
        public void ReCalFactReceiptValue()
        {
            this.FactReceiptValue = this.S_C_ReceiptPlanRelation.Sum(d => d.RelationValue);
        }

        #endregion

        #region 分布扩展方法

        partial void onDelete();
        partial void onDeleted();

        #endregion

    }
}
