using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Formula;
using Config;
using Config.Logic;


namespace Market.Logic.Domain
{
    /// <summary>
    /// 收款对象模型
    /// </summary>
    public partial class S_C_Receipt
    {
        #region 公共属性

        decimal _planReceiptWriteoff = -1;
        /// <summary>
        /// 最大可关联计划收款金额
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal MaxPlanReceiptWriteoff
        {
            get
            {
                var writeOffValue = this.S_C_ReceiptPlanRelation.Sum(d => d.RelationValue);
                var _planReceiptWriteoff = this.Amount - writeOffValue;
                return _planReceiptWriteoff;
            }
        }

        /// <summary>
        /// 最大可关联发票金额
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal MaxInvoiceWriteoff
        {
            get
            {
                var writeOffValue = 0M;
                if (this.S_C_InvoiceReceiptRelation.Count > 0)
                    writeOffValue = Convert.ToDecimal(this.S_C_InvoiceReceiptRelation.Sum(d => d.RelationValue));
                var _invoiceWriteoff = this.Amount - writeOffValue;
                return _invoiceWriteoff;
            }
        }

        #endregion

        #region 公共实例方法
        /// <summary>
        /// 删除收款信息
        /// </summary>
        /// <param name="isDestory">是否销毁删除</param>
        public void Delete(bool isDestory = false)
        {
            onDelete();
            var marketEntities = this.GetMarketContext();
            foreach (var item in this.S_C_InvoiceReceiptRelation.ToList())
                marketEntities.S_C_InvoiceReceiptRelation.Remove(item);
            foreach (var item in this.S_C_ReceiptPlanRelation.ToList())
            {
                var plan = item.S_C_PlanReceipt;
                marketEntities.S_C_ReceiptPlanRelation.Remove(item);
                plan.Reset();
            }
            var contractInfo = this.S_C_ManageContract;
            marketEntities.S_C_Receipt.Remove(this);
            contractInfo.SummaryReceiptData();
            onDeleted();
        }

        /// <summary>
        /// 保存收款信息
        /// </summary>
        public void Save()
        {
            var marketEntites = this.GetMarketContext();
            this.BelongMonth = this.ArrivedDate.Month;
            this.BelongYear = this.ArrivedDate.Year;
            this.BelongQuarter = MarketHelper.GetQuarter(this.ArrivedDate);
            if (String.IsNullOrEmpty(this.ContractInfoID))
                throw new Formula.Exceptions.BusinessException("必须指定合同ID，请关联合同后再进行收款");
            var contractInfo = marketEntites.S_C_ManageContract.SingleOrDefault(d => d.ID == this.ContractInfoID);
            if (contractInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + this.ContractInfoID + "】的关联合同对象");
            this.ContractName = contractInfo.Name;
            this.ContractCode = contractInfo.SerialNumber;
            if (String.IsNullOrEmpty(this.CustomerID))
            {
                this.CustomerID = contractInfo.PartyA;
                this.CustomerName = contractInfo.PartyAName;
            }
            if (String.IsNullOrEmpty(this.ReceiptMasterUnitID))
            {
                this.ReceiptMasterUnitID = contractInfo.BusinessDept;
                this.ReceiptMasterUnit = contractInfo.BusinessDeptName;
            }
            if (String.IsNullOrEmpty(this.ProductUnit)) {
                this.ProductUnit = contractInfo.ProductionDept;
                this.ProductUnitName = contractInfo.ProductionDeptName;
            }
            contractInfo.SummaryReceiptData();
        }

        //更新所属合同的所有部门收款汇总
        public void SumContractDeptRelation()
        {
            var marketDB = this.GetMarketSqlHelper();
            var sql = @"update S_C_ManageContract_DeptRelation
set SumDeptReceiptValue = (select sum(RelationValue) from S_C_Receipt_DeptRelation 
where ContractInfoID = '{0}' and Dept = S_C_ManageContract_DeptRelation.Dept)
where S_C_ManageContractID = '{0}'";
            sql = string.Format(sql, this.ContractInfoID);
            marketDB.ExecuteNonQuery(sql);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoice"></param>
        public void RemoveInvoiceRelation(S_C_PlanReceipt invoice)
        {
            var marketEntities = this.GetMarketContext();
            var relationPlan = this.S_C_InvoiceReceiptRelation.FirstOrDefault(d => d.InvoiceID == invoice.ID);
            if (relationPlan == null) return;
            marketEntities.S_C_InvoiceReceiptRelation.Remove(relationPlan);
            if (this.S_C_ManageContract != null)
                this.S_C_ManageContract.SummaryInvoiceData();
        }

        /// <summary>
        /// 清除所有开票关联
        /// </summary>
        public void ClearInvoice()
        {
            var marketEntities = this.GetMarketContext();
            foreach (var item in this.S_C_InvoiceReceiptRelation.ToList())
                marketEntities.S_C_InvoiceReceiptRelation.Remove(item);
            if (this.S_C_ManageContract != null)
                this.S_C_ManageContract.SummaryInvoiceData();
        }

        /// <summary>
        /// 将收款对象关联到发票对象
        /// </summary>
        /// <param name="invoiceID">发票ID</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于收款金额。当默认为0，且offset为true,关联金额则取发票和收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToInvoice(string invoiceID, decimal relateValue, bool offSet = false)
        {
            var entities = this.GetMarketContext();
            var invoice = entities.S_C_Invoice.SingleOrDefault(d => d.ID == invoiceID);
            if (invoice == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + invoiceID + "】的发票对象，无法进行关联操作");
            this.RelateToInvoice(invoice, relateValue, offSet);
        }

        /// <summary>
        /// 将收款对象关联到发票对象
        /// </summary>
        /// <param name="invoice">发票对象</param>
        /// <param name="relateValue">关联金额（默认为0，当为0时，且offset为false，关联金额自动等于收款金额。当默认为0，且offset为true,关联金额则取发票和收款的金额小的金额）</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToInvoice(S_C_Invoice invoice, decimal relateValue = 0, bool offSet = false)
        {
            if (relateValue == 0)
                relateValue = Convert.ToDecimal(invoice.MaxReceiptWriteOff);

            //是否是以冲销帐方式来关联发票与收款的关系
            //如果不以冲销帐来关联，则关联金额不允许大于开票金额，也不允许大于收款金额
            //如果是以冲销帐方式来关联则根据小金额来关联发票
            if (!offSet)
            {
                if (relateValue > invoice.MaxReceiptWriteOff)
                    throw new Formula.Exceptions.BusinessException("收款对应到发票的金额，不能高于发票可冲销金额【" + invoice.MaxReceiptWriteOff + "】");
                if (relateValue > this.MaxInvoiceWriteoff)
                    relateValue = this.MaxInvoiceWriteoff;
            }
            else
            {
                if (relateValue > invoice.MaxReceiptWriteOff)
                    relateValue = Convert.ToDecimal(invoice.MaxReceiptWriteOff);
                if (relateValue > this.MaxInvoiceWriteoff)
                    relateValue = Convert.ToDecimal(this.MaxInvoiceWriteoff);
                if (this.MaxInvoiceWriteoff <= 0) throw new Formula.Exceptions.BusinessException("收款可对账金额【" + this.MaxInvoiceWriteoff + "】小于等于0，无法再次进行对账");
                if (invoice.MaxReceiptWriteOff <= 0) throw new Formula.Exceptions.BusinessException("发票可对账金额【" + invoice.MaxReceiptWriteOff + "】小于等于0，无法再次进行对账");
            }
            if (relateValue <= 0) return;
            var marketEntities = this.GetMarketContext();
            var relation = this.S_C_InvoiceReceiptRelation.FirstOrDefault(d => d.InvoiceID == invoice.ID);
            if (relation == null)
            {
                relation = marketEntities.S_C_InvoiceReceiptRelation.Create();
                relation.ID = FormulaHelper.CreateGuid();
                relation.InvoiceID = invoice.ID;
                relation.ReceiptID = this.ID;
                relation.ContractInfoID = this.ContractInfoID;
                marketEntities.S_C_InvoiceReceiptRelation.Add(relation);
            }
            relation.RelationValue += relateValue;
            if (this.ContractInfoID != invoice.ContractInfoID)
                throw new Formula.Exceptions.BusinessException("非同一合同下的发票和收款不能进行关联");
            if (invoice.S_C_ManageContract == null) throw new Formula.Exceptions.BusinessException("对冲的发票未绑定合同信息，无法进行关联");
            invoice.S_C_ManageContract.SummaryContractData();
        }

        /// <summary>
        /// 移除收款与计划的绑定关系
        /// </summary>
        /// <param name="plan">计划对象</param>
        public void RemovePlanRelation(S_C_PlanReceipt plan)
        {
            var marketEntities = this.GetMarketContext();
            var relationPlan = this.S_C_ReceiptPlanRelation.FirstOrDefault(d => d.PlanID == plan.ID);
            if (relationPlan == null) return;
            marketEntities.S_C_ReceiptPlanRelation.Remove(relationPlan);
            plan.Reset();
            if (this.S_C_ManageContract != null)
                this.S_C_ManageContract.SummaryReceiptData();
        }



        /// <summary>
        /// 清除所有计划收款关联
        /// </summary>
        public void ClearPlanReceipt()
        {
            var marketEntities = this.GetMarketContext();
            var testentity = new S_C_PlanReceipt();
            foreach (var item in this.S_C_ReceiptPlanRelation.ToList())
            {
                var plan = item.S_C_PlanReceipt;
                marketEntities.S_C_ReceiptPlanRelation.Remove(item);
                plan.Reset();
            }
            if (this.S_C_ManageContract != null)
                this.S_C_ManageContract.SummaryReceiptData();
        }

        /// <summary>
        /// 将收款对象关联到计划收款对象
        /// </summary>
        /// <param name="planID">计划收款ID</param>
        /// <param name="relateValue">关联金额</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToPlanReceipt(string planID, decimal relateValue = 0, bool offSet = false)
        {
            var entities = this.GetMarketContext();
            var plan = entities.S_C_PlanReceipt.SingleOrDefault(d => d.ID == planID);
            if (plan == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + planID + "】的计划收款对象，无法进行关联操作");
            this.RelateToPlanReceipt(plan, relateValue, offSet);
        }


        /// <summary>
        /// 将收款对象关联到计划收款对象
        /// </summary>
        /// <param name="plan">计划收款对象</param>
        /// <param name="relateValue">关联金额</param>
        /// <param name="offSet">是否冲销关联</param>
        public void RelateToPlanReceipt(S_C_PlanReceipt plan, decimal relateValue = 0, bool offSet = false)
        {
            if (relateValue == 0)
                relateValue = Convert.ToDecimal(plan.PlanReceiptValue);
            if (plan.State != PlanReceiptState.UnReceipt.ToString()) throw new Formula.Exceptions.BusinessException("只能关联状态为【未到款】的计划收款对象");
            if (!offSet)
            {
                if (relateValue > plan.PlanReceiptValue)
                    throw new Formula.Exceptions.BusinessException("收款对应到计划的金额，不能高于计划收款金额");
                if (relateValue > this.MaxPlanReceiptWriteoff)
                    throw new Formula.Exceptions.BusinessException("收款对应到计划的金额，不能高于收款可对应金额，请先拆分收款项");
            }
            else
            {
                if (relateValue > plan.PlanReceiptValue)
                    relateValue = Convert.ToDecimal(plan.PlanReceiptValue);
                if (relateValue > this.MaxPlanReceiptWriteoff)
                    relateValue = Convert.ToDecimal(this.MaxPlanReceiptWriteoff);
            }

            if (relateValue <= 0) return;
            var userInfo = FormulaHelper.GetUserInfo();
            var marketEntities = this.GetMarketContext();
            var relation = this.S_C_ReceiptPlanRelation.FirstOrDefault(d => d.PlanID == plan.ID);
            if (relation == null)
            {
                relation = marketEntities.S_C_ReceiptPlanRelation.Create();
                relation.ID = FormulaHelper.CreateGuid();
                relation.PlanID = plan.ID;
                relation.ReceiptID = this.ID;
                relation.ReceiptObjectID = plan.ReceiptObjectID;
                relation.RelateParentID = plan.RelateParentID;
                relation.ModifyDate = DateTime.Now;
                relation.ModifyUser = userInfo.UserName;
                relation.ModifyUserID = userInfo.UserID;
                marketEntities.S_C_ReceiptPlanRelation.Add(relation);
            }
            if (this.ContractInfoID != plan.ContractInfoID)
                throw new Formula.Exceptions.BusinessException("非同一合同下的计划收款和收款不能进行关联");
            if (this.S_C_ManageContract == null)
                this.S_C_ManageContract = marketEntities.S_C_ManageContract.SingleOrDefault(d => d.ID == this.ContractInfoID);
            if (this.S_C_ManageContract == null) throw new Formula.Exceptions.BusinessException("收款对象未关联到合同信息，无法与计划收款关联");
            this.S_C_ManageContract.SummaryContractData();

            if (relateValue <= 0) throw new Formula.Exceptions.BusinessException("收款与计划的对账金额不能小于0");

            relation.RelationValue = relateValue;
            plan.FactReceiptValue = relateValue;
            plan.ReceiptDate = this.ArrivedDate;

            //如果关联金额小于计划收款金额，则表示计划收款未全额到款，需要进行计划拆分
            if (relateValue < plan.PlanReceiptValue)
            {
                var splitValue = plan.PlanReceiptValue - relateValue;
                plan.Split(splitValue, plan.PlanReceiptDate, true);
                plan.State = PlanReceiptState.PartReceipted.ToString();
            }
            else
                plan.State = PlanReceiptState.Receipted.ToString();

            if (plan.S_C_ManageContract_ReceiptObj == null) throw new Formula.Exceptions.BusinessException("未能找到计划所对应的收款项，无法进行收款与计划的关联");
            plan.S_C_ManageContract_ReceiptObj.SummaryReceiptValue();
        }

        /// <summary>
        /// 收款关联收款项
        /// </summary>
        /// <param name="receiptObjID">收款项ID</param>
        /// <param name="relateValue">关联金额</param>
        /// <param name="offSet">是否对账冲销</param>
        public void RelateToReceiptObject(string receiptObjID, decimal relateValue = 0, bool offSet = false)
        {
            var entities = this.GetMarketContext();
            var receiptObj = entities.S_C_ManageContract_ReceiptObj.SingleOrDefault(d => d.ID == receiptObjID);
            if (receiptObj == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + receiptObjID + "】的收款项对象，无法进行关联操作");
            this.RelateToReceiptObject(receiptObj, relateValue, offSet);
        }

        /// <summary>
        /// 收款关联收款项
        /// </summary>
        /// <param name="receiptObj">收款项对象</param>
        /// <param name="relateValue">关联金额</param>
        /// <param name="offSet">是否对账冲销</param>
        public void RelateToReceiptObject(S_C_ManageContract_ReceiptObj receiptObj, decimal relateValue = 0, bool offSet = false)
        {
            var plan = receiptObj.S_C_PlanReceipt.FirstOrDefault(d => d.State == PlanReceiptState.UnReceipt.ToString());
            if (plan == null) throw new Formula.Exceptions.BusinessException("收款项【" + receiptObj.Name + "】不存在未到款的计划，无法关联到款");
            this.RelateToPlanReceipt(plan, relateValue, offSet);
        }

        #endregion

        #region 分布扩展方法

        partial void onDelete();
        partial void onDeleted();

        #endregion
    }
}
