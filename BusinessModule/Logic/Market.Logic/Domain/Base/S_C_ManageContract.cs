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
    /// 合同领域对象模型
    /// </summary>
    public partial class S_C_ManageContract
    {

        #region 公共属性

        /// <summary>
        /// 合同开票总金额(只读)
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal InvoiceValue
        {
            get
            {
                return Convert.ToDecimal(this.S_C_Invoice.Where(d => d.State == InvoiceState.Normal.ToString()).Sum(d => d.Amount));
            }
        }

        /// <summary>
        /// 合同收款总金额(只读)
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal RecieptValue
        {
            get
            {
                return this.S_C_Receipt.Sum(d => d.Amount);
            }
        }

        /// <summary>
        /// 预收款金额（只读，合同收款总和减去已关联发票冲销的收款金额）
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal PreReceipt
        {
            get
            {
                var _preReceipt = 0M;
                var entities = this.GetMarketContext();
                var relationValueSummary = 0M;
                if (entities.S_C_InvoiceReceiptRelation.Where(d => d.ContractInfoID == this.ID).Count() > 0)
                {
                    relationValueSummary = entities.S_C_InvoiceReceiptRelation.Where(d => d.ContractInfoID == this.ID).Sum(d => d.RelationValue);
                }
                _preReceipt = this.RecieptValue - relationValueSummary;
                return _preReceipt;
            }
        }

        decimal _receivable = -1;
        /// <summary>
        /// 合同对象的应收款(只读，合同的开票总金额-合同的收款总金额)
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal Receivable
        {
            get
            {
                if (_receivable == -1)
                {
                    _receivable = this.InvoiceValue - this.RecieptValue;
                }
                return _receivable;
            }
        }

        /// <summary>
        /// 合同的剩余可开票金额(只读，合同金额-已开票金额)
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal RemainInvoiceValue
        {
            get
            {
                return Convert.ToDecimal(this.ContractRMBAmount) - this.InvoiceValue;
            }
        }

        /// <summary>
        /// 合同坏账金额(只读属性，所有标识为坏账的计划收款剩余金额总和)
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal BadDebtValue
        {
            get
            {
                var _badDebtValue = 0M;
                var PlanReceipts = this.S_C_PlanReceipt.Where(d => d.State == PlanReceiptState.BadDebt.ToString()).ToList();
                if (PlanReceipts.Count() > 0)
                {
                    var badDebtValue = PlanReceipts.Sum(c => c.BadDebtValue);
                    if (badDebtValue != null)
                        _badDebtValue = Convert.ToDecimal(badDebtValue);
                }
                return _badDebtValue;
            }
        }

        /// <summary>
        /// 累计项目分解合同额(只读属性，所有标识为坏账的计划收款剩余金额总和)
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public decimal ProjectSplitValue
        {
            get
            {
                var _projectSplitValue = 0M;
                if (this.S_C_ManageContract_ReceiptObj.Where(d => !String.IsNullOrEmpty(d.ProjectInfo)).Count() > 0)
                    _projectSplitValue = Convert.ToDecimal(this.S_C_ManageContract_ReceiptObj.Where(d => !String.IsNullOrEmpty(d.ProjectInfo)).Sum(d => d.ReceiptValue));
                return _projectSplitValue;
            }
        }

        #endregion

        #region 公共实例方法

        public void Save()
        {
            var marketDBContext = this.GetDbContext<MarketEntities>();
            //this.ValidateContractInfo();
            this.SynchContractProperties();
            if (this.SignDate.HasValue)
            {
                var date = Convert.ToDateTime(this.SignDate);
                this.BelongQuarter = MarketHelper.GetQuarter(date);
                this.BelongYear = date.Year;
                this.BelongMonth = date.Month;
                this.IsSigned = ContractIsSigned.Signed.ToString();
                if (this.ContractState == "WaitSign" || this.ContractState == "Regist")
                {
                    //如果有签约日期，且合同状态为待签约或起草登记状态的，则修订合同状态为履行
                    this.ContractState = "Sign";
                }              
            }
            else
            {
                this.IsSigned = ContractIsSigned.UnSigned.ToString();
            }
            this.SummaryContractData();
            if (!String.IsNullOrEmpty(this.PartyA))
            {
                var customer = marketDBContext.S_F_Customer.Find(this.PartyA);
                if (customer == null) throw new Formula.Exceptions.BusinessException("指定的客户信息不存在，保存合同信息失败");
                if (customer.BusinessDate == null)
                    customer.BusinessDate = DateTime.Now;
                if (this.SignDate.HasValue)
                {
                    customer.IsCooperated = YesOrNo.Yes.ToString();
                }
                else if (marketDBContext.S_C_ManageContract.Count(d => d.SignDate.HasValue && d.PartyA == customer.ID
                    && d.ID != this.ID) == 0)
                {
                    customer.IsCooperated = YesOrNo.No.ToString();
                }
                else
                {
                    customer.IsCooperated = YesOrNo.Yes.ToString();
                }
            }
        }


        public void ValidateDelete()
        {
            if (this.SignDate.HasValue)
            {
                throw new Formula.Exceptions.BusinessException("合同【" + this.Name + "】已经签约，不能进行删除操作");
            }
            //已经开过票的合同不能删除
            if (this.S_C_Invoice.Where(d => d.State == InvoiceState.Normal.ToString()).Count() > 0)
                throw new Formula.Exceptions.BusinessException("合同【" + this.Name + "】已经开过发票，不能进行删除操作");
            //已经收过款的合同不能删除
            if (this.S_C_Receipt.Count() > 0)
                throw new Formula.Exceptions.BusinessException("合同【" + this.Name + "】已有到款记录，不能进行删除操作");
        }

        /// <summary>
        /// 删除合同方法
        /// </summary>
        /// <param name="destory">是否销毁删除（当参数为true时，不再进行逻辑校验。否则开具过发票的合同将不能进行删除）</param>
        public void Delete(bool destory = false)
        {
            var marketEntities = this.GetMarketContext();
            onDelete();
            if (!destory)
                this.ValidateDelete();

            //删除关联的计划收款及项目绑定关系
            marketEntities.S_C_PlanReceipt.Delete(d => d.ContractInfoID == this.ID);
            marketEntities.S_C_ManageContract_ReceiptObj.Delete(d => d.S_C_ManageContractID == this.ID);
            marketEntities.S_C_ManageContract_ProjectRelation.Delete(d => d.S_C_ManageContractID == this.ID);

            this.S_C_ManageContract_ProjectRelation.Clear();

            var customer = marketEntities.S_F_Customer.Find(this.PartyA);
            if (customer != null)
            {
                if (marketEntities.S_C_ManageContract.Count(d => d.SignDate.HasValue && d.PartyA == customer.ID
                        && d.ID != this.ID) == 0)
                {
                    customer.IsCooperated = YesOrNo.No.ToString();
                }
                else
                {
                    customer.IsCooperated = YesOrNo.Yes.ToString();
                }
            }

            //如果合同强制删除则，需要清空已经开具的发票信息
            foreach (var item in this.S_C_Invoice.ToList())
                item.Delete();
            marketEntities.S_C_ManageContract.Remove(this);

            //删除补充协议
            marketEntities.Set<S_C_ManageContract_Supplementary>().Delete(d => d.ContractInfoID == this.ID);
            onDeleted();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SummaryInvoiceData()
        {
            this.SumInvoiceValue = this.InvoiceValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SummaryReceiptData()
        {
            this.SumReceiptValue = this.RecieptValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SummaryBadDebtData()
        {
            this.SumBadDebtValue = this.BadDebtValue;
        }

        /// <summary>
        /// 汇总合同开票金额，收款金额和坏账金额信息
        /// </summary>
        public void SummaryContractData()
        {
            this.SumInvoiceValue = this.InvoiceValue;
            this.SumReceiptValue = this.RecieptValue;
            this.SumBadDebtValue = this.BadDebtValue;
        }


        /// <summary>
        /// 保存收款项信息
        /// </summary>
        /// <param name="receiptObjects">收款项对象集合</param>
        public void SaveReceiptObjects(List<S_C_ManageContract_ReceiptObj> receiptObjects)
        {
            foreach (var item in receiptObjects)
                SaveReceiptObject(item);
        }

        /// <summary>
        /// 保存收款项信息
        /// </summary>
        /// <param name="receiptObject">收款项对象</param>
        public void SaveReceiptObject(S_C_ManageContract_ReceiptObj receiptObject)
        {
            var marketEntities = this.GetMarketContext();
            var receiptObj = this.S_C_ManageContract_ReceiptObj.SingleOrDefault(d => d.ID == receiptObject.ID);
            var receiptValue = this.S_C_ManageContract_ReceiptObj.Where(d => d.ID != receiptObject.ID).Sum(d => d.ReceiptValue);
            if (receiptValue > this.ContractRMBAmount) throw new Formula.Exceptions.BusinessException("收款项金额合计不能大于合同金额");
            if (receiptObj == null)
            {
                receiptObj = new S_C_ManageContract_ReceiptObj();
                receiptObject.ID = FormulaHelper.CreateGuid();
                FormulaHelper.UpdateModel(receiptObj, receiptObject);
                receiptObj.MileStoneState = false.ToString();
                this.S_C_ManageContract_ReceiptObj.Add(receiptObj);
                var plan = new S_C_PlanReceipt();
                receiptObj.S_C_ManageContract = this;
                //新增合同收款项的同时，新建计划收款项
                plan.Name = receiptObj.Name;
                plan.PlanReceiptDate = receiptObj.PlanFinishDate;
                plan.PlanReceiptValue = Convert.ToDecimal(receiptObj.ReceiptValue);
                receiptObj.AddReceiptPlan(plan);
            }
            else
            {
                if (receiptObj.FactInvoiceValue > 0 || receiptObj.FactReceiptValue > 0 || receiptObj.FactBadValue > 0)
                {
                    receiptObj.SummaryReceiptObjectData();
                    receiptObj.ProjectInfo = receiptObject.ProjectInfo;
                    receiptObj.ProjectInfoName = receiptObject.ProjectInfoName;
                }
                else
                {
                    FormulaHelper.UpdateModel(receiptObj, receiptObject);
                    receiptObj.ResetPlan();
                }
            }
            receiptObj.SummaryReceiptObjectData();
            if (receiptObj.ReceiptValue < receiptObj.FactInvoiceValue)
                throw new Formula.Exceptions.BusinessException("【" + receiptObj.Name + "】收款项金额不能小于该收款项的已开票金额：【" + receiptObj.FactInvoiceValue + "】");
            if (receiptObj.ReceiptValue < receiptObj.FactReceiptValue)
                throw new Formula.Exceptions.BusinessException("【" + receiptObj.Name + "】收款项金额不能小于该收款项的已到款金额：【" + receiptObj.FactReceiptValue + "】");
            if (receiptObj.ReceiptValue < receiptObj.FactReceiptValue)
                throw new Formula.Exceptions.BusinessException("【" + receiptObj.Name + "】收款项金额不能小于该收款项的坏账金额：【" + receiptObj.FactBadValue + "】");
            receiptObj.SetPlanFinishDate();
            if (!receiptObj.OriginalPlanFinishDate.HasValue)
                receiptObj.OriginalPlanFinishDate = receiptObj.PlanFinishDate;
            if (this.S_C_ManageContract_ProjectRelation.Count == 1)
            {
                var projectRelation = this.S_C_ManageContract_ProjectRelation.FirstOrDefault();
                if (String.IsNullOrEmpty(receiptObj.ProjectInfo))
                {
                    receiptObj.ProjectInfo = projectRelation.ProjectID;
                    receiptObj.ProjectInfoName = projectRelation.ProjectName;
                }
            }

            if (!String.IsNullOrEmpty(receiptObj.ProjectInfo))
            {
                if (this.S_C_ManageContract_ProjectRelation.Count(d => d.ProjectID == receiptObj.ProjectInfo) == 0 &&
                    !String.IsNullOrEmpty(receiptObj.MileStoneID))
                {
                    throw new Formula.Exceptions.BusinessException("已经绑定里程碑的收款项不能绑定其它项目，请重新添加项目【" + receiptObj.ProjectInfoName + "】");
                }
                else if (receiptObj.FactReceiptValue > 0 && this.S_C_ManageContract_ProjectRelation.Count(d => d.ProjectID == receiptObj.ProjectInfo) == 0)
                {
                    throw new Formula.Exceptions.BusinessException("已经有过到款的收款项不能绑定其它项目，请重新添加项目【" + receiptObj.ProjectInfoName + "】");
                }
            }
            else
            {
            }
            receiptObj.SchyorPlanRecipet();
        }


        /// <summary>
        /// 更新合同冗余属性(目前只同步更新合同名称,合同编号)
        /// 主要更新数据库中的计划收款表，合同与项目关系表
        /// </summary>
        public void SynchContractProperties()
        {
            var marketDB = this.GetMarketSqlHelper();
            string updateSql = @" update S_C_PlanReceipt set ContractName='{1}',ContractCode = '{2}' where ContractInfoID='{0}';
update S_C_Receipt set ContractName='{1}',ContractCode = '{2}' where ContractInfoID='{0}';
update S_C_Invoice set ContractName='{1}',ContractCode = '{2}' where ContractInfoID='{0}';
";
            updateSql = string.Format(updateSql, this.ID, this.Name, this.SerialNumber);
            marketDB.ExecuteNonQuery(updateSql);
        }


        /// <summary>
        /// 校验合同信息是否正确
        /// </summary>
        public void ValidateContractInfo()
        {
            var entities = this.GetMarketContext();
            if (!string.IsNullOrEmpty(this.SerialNumber))
            {
                bool codeIsExist = entities.Set<S_C_ManageContract>().Where(p => p.ID != this.ID).Any(p => p.SerialNumber == this.SerialNumber);
                if (codeIsExist == true)
                    throw new Formula.Exceptions.BusinessException(string.Format("合同编码【{0}】已存在，请重新输入！", this.SerialNumber));
            }
            if (!string.IsNullOrEmpty(this.Name))
            {
                bool nameIsExist = entities.Set<S_C_ManageContract>().Any(p => p.ID != this.ID && p.Name == this.Name);
                if (nameIsExist == true)
                    throw new Formula.Exceptions.BusinessException(string.Format("合同名称【{0}】已存在，请重新输入！", this.Name));
            }
        }

        /// <summary>
        /// 合同绑定项目信息
        /// </summary>
        /// <param name="projects">合同绑定的项目信息</param>
        public void BindingProject(List<S_C_ManageContract_ProjectRelation> projects)
        {
            var entities = this.GetMarketContext();
            entities.S_C_ManageContract_ProjectRelation.Delete(d => d.S_C_ManageContractID == this.ID);
            foreach (var project in projects)
            {
                var entity = entities.Set<S_C_ManageContract_ProjectRelation>().Create();
                entity.ID = FormulaHelper.CreateGuid();
                entity.ProjectID = project.ProjectID;
                entity.ProjectCode = project.ProjectCode;
                entity.ProjectName = project.ProjectName;
                entity.S_C_ManageContractID = this.ID;
                entities.Set<S_C_ManageContract_ProjectRelation>().Add(entity);
                entity.Scale = project.Scale;
                entity.ProjectValue = project.ProjectValue;
            }
        }

        public void ClearProjectBinding()
        {
            var entities = this.GetMarketContext();
            foreach (var item in this.S_C_ManageContract_ProjectRelation.ToList())
            {
                entities.S_C_ManageContract_ProjectRelation.Remove(item);
            }
            this.S_C_ManageContract_ProjectRelation.Clear();
        }

        /// <summary>
        /// 删除绑定的项目
        /// </summary>
        /// <param name="projectInfoID">项目ID</param>
        public void RemoveProject(string projectInfoID)
        {
            var relation = this.S_C_ManageContract_ProjectRelation.FirstOrDefault(d => d.ProjectID == projectInfoID);
            if (relation == null) return;
            var entities = this.GetMarketContext();
            var list = this.S_C_ManageContract_ReceiptObj.Where(d => d.ProjectInfo == projectInfoID).ToList();
            foreach (var item in list)
            {
                //if (item.FactReceiptValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + item.Name + "】已经有实际收款，无法拆分项目【" + item.ProjectInfoName + "】绑定，删除项目失败");
                if (!String.IsNullOrEmpty(item.MileStoneID)) throw new Formula.Exceptions.BusinessException("收款项【" + item.Name + "】已经绑定了里程碑，项目【" + item.ProjectInfoName + "】不能删除");
                item.ProjectInfo = "";
                item.ProjectInfoName = "";
            }
            entities.S_C_ManageContract_ProjectRelation.Remove(relation);
        }

        #endregion

        #region 分布扩展方法

        partial void onDelete();

        partial void onDeleted();

        #endregion

    }
}
