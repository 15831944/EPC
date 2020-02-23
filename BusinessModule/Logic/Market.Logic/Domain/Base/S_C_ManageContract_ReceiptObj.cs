using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Formula;
//using Project.Logic.Domain;

namespace Market.Logic.Domain
{
    public partial class S_C_ManageContract_ReceiptObj
    {

        /// <summary>
        /// 初始化收款计划
        /// </summary>
        public void CreateReceiptPlan()
        {
            var dbContext = this.GetDbContext<MarketEntities>();
            var userInfo = FormulaHelper.GetUserInfo();
            var plan = this.S_C_PlanReceipt.FirstOrDefault(d => d.State == PlanReceiptState.UnReceipt.ToString());
            if (plan != null) throw new Formula.Exceptions.BusinessException("已经拥有收款计划的收款项不能初始化计划");
            plan = new Domain.S_C_PlanReceipt();
            plan.ID = FormulaHelper.CreateGuid();
            plan.ContractInfoID = this.S_C_ManageContractID;
            plan.ContractCode = this.S_C_ManageContract.SerialNumber;
            plan.ContractName = this.S_C_ManageContract.Name;
            plan.CreateDate = DateTime.Now;
            plan.CreateUser = userInfo.UserName;
            plan.CreateUserID = userInfo.UserID;
            plan.RelateParentID = plan.ID;
            plan.Name = this.Name;
            if (String.IsNullOrEmpty(plan.ProduceMasterID))
            {
                if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionManager))
                {
                    plan.ProduceMasterID = this.S_C_ManageContract.ProductionManager.Split(',')[0];
                    plan.ProduceMasterName = this.S_C_ManageContract.ProductionManagerName.Split(',')[0];
                }
            }
            if (String.IsNullOrEmpty(plan.MasterID))
            {
                if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessManager))
                {
                    plan.MasterID = this.S_C_ManageContract.BusinessManager.Split(',')[0];
                    plan.MasterName = this.S_C_ManageContract.BusinessManagerName.Split(',')[0];
                }
            }
            if (String.IsNullOrEmpty(plan.MasterUnitID))
            {
                if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessDept))
                {
                    plan.MasterUnitID = this.S_C_ManageContract.BusinessDept.Split(',')[0];
                    plan.MasterUnit = this.S_C_ManageContract.BusinessDeptName.Split(',')[0];
                }
            }
            if (String.IsNullOrEmpty(plan.ProductionUnitID))
            {
                if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionDept))
                {
                    plan.ProductionUnitID = this.S_C_ManageContract.ProductionDept.Split(',')[0];
                    plan.ProductionUnitName = this.S_C_ManageContract.ProductionDeptName.Split(',')[0];
                }
            }
            plan.State = PlanReceiptState.UnReceipt.ToString();
            plan.PlanReceiptDate = this.PlanFinishDate;
            if (plan.PlanReceiptDate.HasValue)
            {
                plan.PlanReceiptYearMonth = plan.PlanReceiptDate.Value.Year.ToString() + plan.PlanReceiptDate.Value.Month.ToString("00");
                plan.BelongYear = plan.PlanReceiptDate.Value.Year;
                plan.BelongMonth = plan.PlanReceiptDate.Value.Month;
                plan.BelongQuarter = (plan.PlanReceiptDate.Value.Month - 1) / 3 + 1;
            }
            plan.PlanReceiptValue = this.ReceiptValue.HasValue ? this.ReceiptValue.Value : 0m;
            
            plan.ProjectID = this.ProjectInfo;
            plan.ProjectName = this.ProjectInfoName;
            var prjList = this.S_C_ManageContract.S_C_ManageContract_ProjectRelation.ToList();
            var project = prjList.FirstOrDefault(a => a.ProjectID == this.ProjectInfo);
            if (project != null)
                plan.ProjectCode = project.ProjectCode;
            plan.CustomerID = this.S_C_ManageContract.PartyA;
            plan.CustomerName = this.S_C_ManageContract.PartyAName;
            plan.CustomerFullID = this.S_C_ManageContract.CustomerFullID;
            plan.ReceiptObjectID = this.ID;
            this.S_C_PlanReceipt.Add(plan);
            plan.S_C_ManageContract_ReceiptObj = this;
        }

        /// <summary>
        /// 新增收款计划
        /// </summary>
        /// <param name="plan">收款计划对象</param>
        public void AddReceiptPlan(S_C_PlanReceipt plan)
        {
            var entitites = this.GetMarketContext();
            if (entitites.Entry<S_C_PlanReceipt>(plan).State != System.Data.EntityState.Added && entitites.Entry<S_C_PlanReceipt>(plan).State != System.Data.EntityState.Detached)
                throw new Formula.Exceptions.BusinessException("只有新增的计划收款对象能够调用AddReceiptPlan方法");
            var userInfo = FormulaHelper.GetUserInfo();
            plan.ID = FormulaHelper.CreateGuid();
            plan.ContractInfoID = this.S_C_ManageContractID;
            plan.ContractCode = this.S_C_ManageContract.SerialNumber;
            plan.ContractName = this.S_C_ManageContract.Name;
            plan.CreateDate = DateTime.Now;
            plan.CreateUser = userInfo.UserName;
            plan.CreateUserID = userInfo.UserID;
            plan.RelateParentID = plan.ID;
            if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionManager))
            {
                plan.ProduceMasterID = this.S_C_ManageContract.ProductionManager.Split(',')[0];
                plan.ProduceMasterName = this.S_C_ManageContract.ProductionManagerName.Split(',')[0];
            }
            if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessManager))
            {
                plan.MasterID = this.S_C_ManageContract.BusinessManager.Split(',')[0];
                plan.MasterName = this.S_C_ManageContract.BusinessManagerName.Split(',')[0];
            }
            if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessDept))
            {
                plan.MasterUnitID = this.S_C_ManageContract.BusinessDept.Split(',')[0];
                plan.MasterUnit = this.S_C_ManageContract.BusinessDeptName.Split(',')[0];
            }
            if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionDept))
            {
                plan.ProductionUnitID = this.S_C_ManageContract.ProductionDept.Split(',')[0];
                plan.ProductionUnitName = this.S_C_ManageContract.ProductionDeptName.Split(',')[0];
            }
            plan.State = PlanReceiptState.UnReceipt.ToString();
            if (plan.PlanReceiptDate.HasValue)
            {
                var date = Convert.ToDateTime(plan.PlanReceiptDate);
                plan.BelongYear = date.Year;
                plan.BelongMonth = date.Month;
                plan.BelongQuarter = MarketHelper.GetQuarter(date);
                if (date.Month >= 10)
                    plan.PlanReceiptYearMonth = date.Year.ToString() + date.Month.ToString();
                else
                    plan.PlanReceiptYearMonth = date.Year.ToString() + "0" + date.Month.ToString();
            }
            //plan.FirstDutyManID = this.S_C_ManageContract.BusinessManager;
            //plan.FirstDutyManName = this.S_C_ManageContract.BusinessManagerName;
            plan.ProjectCode = this.ProjectInfo;
            plan.ProjectID = this.ProjectInfo;
            plan.ProjectName = this.ProjectInfoName;
            plan.CustomerID = this.S_C_ManageContract.PartyA;
            plan.CustomerName = this.S_C_ManageContract.PartyAName;
            plan.ReceiptObjectID = this.ID;
            entitites.S_C_PlanReceipt.Add(plan);
            plan.S_C_ManageContract = this.S_C_ManageContract;
        }

        /// <summary>
        /// 同步更新计划收款的项目信息
        /// </summary>
        public void SchyorPlanRecipet()
        {
            var marketEntities = this.GetMarketContext();
            var project = marketEntities.S_I_Project.SingleOrDefault(d => d.ID == this.ProjectInfo);
            string sql = @"update S_C_PlanReceipt set ProjectID='{1}',ProjectName='{2}',ProjectCode='{3}',
ProductionUnitID='{4}',ProductionUnitName='{5}' where ReceiptObjectID='{0}' and State='" + PlanReceiptState.UnReceipt.ToString() + "'";
            if (project == null)
                sql = String.Format(sql, this.ID, "", "", "", "", "");
            else
                sql = String.Format(sql, this.ID, project.ID, project.Name, project.Code, project.ChargerDept,
                    project.ChargerDeptName);
            var sqlDb = this.GetMarketSqlHelper();
            sqlDb.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 重设计划（当收款项变动时）
        /// </summary>
        public void ResetPlan()
        {
            var marketEntities = this.GetMarketContext();
            var state = PlanReceiptState.UnReceipt.ToString();

            if (this.FactInvoiceValue > 0 || this.FactReceiptValue > 0 || this.FactBadValue > 0)
            {
                var planList = marketEntities.S_C_PlanReceipt.Where(d => d.State == state && d.ReceiptObjectID == this.ID).ToList();
                var prjList= this.S_C_ManageContract.S_C_ManageContract_ProjectRelation.ToList();
                foreach (var item in planList)
                {
                    item.ProjectID = this.ProjectInfo;
                    item.ProjectName = this.ProjectInfoName;
                    var project = prjList.FirstOrDefault(a=>a.ProjectID==this.ProjectInfo);
                    if (project != null)
                        item.ProjectCode = project.ProjectCode;
                    item.CustomerID = this.S_C_ManageContract.PartyA;
                    item.CustomerName = this.S_C_ManageContract.PartyAName;
                    item.CustomerFullID = this.S_C_ManageContract.CustomerFullID;
                    if (String.IsNullOrEmpty(item.ProduceMasterID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionManager))
                        {
                            item.ProduceMasterID = this.S_C_ManageContract.ProductionManager.Split(',')[0];
                            item.ProduceMasterName = this.S_C_ManageContract.ProductionManagerName.Split(',')[0];
                        }
                    }
                    if (String.IsNullOrEmpty(item.MasterID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessManager))
                        {
                            item.MasterID = this.S_C_ManageContract.BusinessManager.Split(',')[0];
                            item.MasterName = this.S_C_ManageContract.BusinessManagerName.Split(',')[0];
                        }
                    }
                    if (String.IsNullOrEmpty(item.MasterUnitID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessDept))
                        {
                            item.MasterUnitID = this.S_C_ManageContract.BusinessDept.Split(',')[0];
                            item.MasterUnit = this.S_C_ManageContract.BusinessDeptName.Split(',')[0];
                        }
                    }
                    if (String.IsNullOrEmpty(item.ProductionUnitID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionDept))
                        {
                            item.ProductionUnitID = this.S_C_ManageContract.ProductionDept.Split(',')[0];
                            item.ProductionUnitName = this.S_C_ManageContract.ProductionDeptName.Split(',')[0];
                        }
                    }
                }
                return;
            }


            //重置计划时，重新计算收款项的差额，并自动加到最后一条收款计划上
            var list = marketEntities.S_C_PlanReceipt.Where(d => d.State == state && d.ReceiptObjectID == this.ID).ToList();
            if (list.Count == 0)
            {
                this.CreateReceiptPlan();
            }
            else
            {
                var sumPlanReceiptValue = list.Sum(d => d.PlanReceiptValue);
                var remainValue = Convert.ToDecimal(this.ReceiptValue) - sumPlanReceiptValue;
                var plan = list.OrderByDescending(d => d.ID).FirstOrDefault();
                plan.PlanReceiptValue = plan.PlanReceiptValue + remainValue;
                plan.PlanReceiptDate = this.PlanFinishDate;
                var prjList = this.S_C_ManageContract.S_C_ManageContract_ProjectRelation.ToList();
                foreach (var item in list)
                {
                    item.ProjectID = this.ProjectInfo;
                    item.ProjectName = this.ProjectInfoName;
                    var project = prjList.FirstOrDefault(a => a.ProjectID == this.ProjectInfo);
                    if (project != null)
                        item.ProjectCode = project.ProjectCode;
                    item.CustomerID = this.S_C_ManageContract.PartyA;
                    item.CustomerName = this.S_C_ManageContract.PartyAName;
                    if (String.IsNullOrEmpty(item.ProduceMasterID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionManager))
                        {
                            item.ProduceMasterID = this.S_C_ManageContract.ProductionManager.Split(',')[0];
                            item.ProduceMasterName = this.S_C_ManageContract.ProductionManagerName.Split(',')[0];
                        }
                    }
                    if (String.IsNullOrEmpty(item.MasterID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessManager))
                        {
                            item.MasterID = this.S_C_ManageContract.BusinessManager.Split(',')[0];
                            item.MasterName = this.S_C_ManageContract.BusinessManagerName.Split(',')[0]; 
                        }
                    }
                    if (String.IsNullOrEmpty(item.MasterUnitID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.BusinessDept))
                        {
                            item.MasterUnitID = this.S_C_ManageContract.BusinessDept.Split(',')[0];
                            item.MasterUnit = this.S_C_ManageContract.BusinessDeptName.Split(',')[0];
                        }
                    }
                    if (String.IsNullOrEmpty(item.ProductionUnitID))
                    {
                        if (!string.IsNullOrEmpty(this.S_C_ManageContract.ProductionDept))
                        {
                            item.ProductionUnitID = this.S_C_ManageContract.ProductionDept.Split(',')[0];
                            item.ProductionUnitName = this.S_C_ManageContract.ProductionDeptName.Split(',')[0];
                        }
                    }
                }
                if (plan.PlanReceiptDate.HasValue)
                {
                    plan.BelongYear = plan.PlanReceiptDate.Value.Year;
                    plan.BelongQuarter = MarketHelper.GetQuarter(plan.PlanReceiptDate.Value);
                    plan.BelongMonth = plan.PlanReceiptDate.Value.Month;
                }
            }
        }

        /// <summary>
        /// 汇总收款项的收款金额
        /// </summary>
        public void SummaryReceiptValue()
        {
            this.FactReceiptValue = Convert.ToDecimal(this.S_C_PlanReceipt.Sum(d => d.FactReceiptValue));
        }

        /// <summary>
        ///  汇总收款项的开票
        /// </summary>
        public void SummaryInvoiceValue()
        {
            this.FactInvoiceValue = Convert.ToDecimal(this.S_C_Invoice_ReceiptObject.Sum(d => d.RelationValue));
        }

        /// <summary>
        ///  汇总收款项的坏账金额
        /// </summary>
        public void SummaryBadDebtValue()
        {
            this.FactBadValue = this.S_C_PlanReceipt.Where(d => d.State == PlanReceiptState.BadDebt.ToString()).Sum(d => d.BadDebtValue);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SummaryReceiptObjectData()
        {
            this.SummaryReceiptValue();
            this.SummaryInvoiceValue();
            this.SummaryBadDebtValue();
        }

        /// <summary>
        /// 重新设置计划时间
        /// </summary>
        /// <param name="planDate">计划完成日期</param>
        public void SetPlanFinishDate(DateTime planDate)
        {
            this.PlanFinishDate = planDate;
        }

        /// <summary>
        /// 重新设置计划时间（根据自身的PlanFinishDate进行设置）
        /// </summary>
        public void SetPlanFinishDate()
        {
            if (!this.PlanFinishDate.HasValue) return;
            this.SetPlanFinishDate(Convert.ToDateTime(this.PlanFinishDate));
        }

        public void SyncSupplementary()
        {
            if (string.IsNullOrEmpty(this.SupplementaryID)) return;
            var entities = this.GetMarketContext();
            var supplementaryItem = entities.Set<S_C_ManageContract_Supplementary_ReceiptObj>().FirstOrDefault(a => a.ID == this.ID);
            if (supplementaryItem == null) return;
            supplementaryItem.FactBadValue = this.FactBadValue;
            supplementaryItem.FactInvoiceValue = this.FactInvoiceValue;
            supplementaryItem.FactReceiptValue = this.FactReceiptValue;
            supplementaryItem.MileStoneID = this.MileStoneID;
            supplementaryItem.MileStoneName = this.MileStoneName;
            supplementaryItem.MileStoneFactEndDate = this.MileStoneFactEndDate;
            supplementaryItem.MileStonePlanEndDate = this.MileStonePlanEndDate;
            supplementaryItem.MileStoneState = this.MileStoneState;
            supplementaryItem.ProjectInfo = this.ProjectInfo;
            supplementaryItem.ProjectInfoName = this.ProjectInfoName;
            supplementaryItem.OriginalPlanFinishDate = this.OriginalPlanFinishDate;
            supplementaryItem.PlanFinishDate = this.PlanFinishDate;
        }

        /// <summary>
        /// 删除收款项
        /// </summary>
        public void Delete()
        {
            if (this.FactInvoiceValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + this.Name + "】已经有开票信息，无法进行删除");
            if (this.FactReceiptValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + this.Name + "】已经有收款信息，无法进行删除");
            if (this.FactBadValue > 0) throw new Formula.Exceptions.BusinessException("收款项【" + this.Name + "】已经有坏账金额，无法进行删除");
            var entities = this.GetMarketContext();
            foreach (var item in this.S_C_PlanReceipt.ToList())
                entities.S_C_PlanReceipt.Remove(item);
            entities.S_C_ManageContract_ReceiptObj.Remove(this);
            entities.Set<S_C_ManageContract_Supplementary_ReceiptObj>().Delete(a => a.ID == this.ID);
        }

        /*

        /// <summary>
        /// 更新里程碑有关的收款项
        /// </summary>
        /// <param name="mileStoneIDs">里程碑ID</param>
        public void UpdateReceiptObj(string mileStoneIDs)
        {
            
            var marketEntities = FormulaHelper.GetEntities<MarketEntities>();
            var projectEntities = FormulaHelper.GetEntities<ProjectEntities>();

            var receiptObjInfo = marketEntities.Set<S_C_ManageContract_ReceiptObj>();
            var mileStoneInfo = projectEntities.Set<S_P_MileStone>();

            if (string.IsNullOrEmpty(mileStoneIDs))
            {
                return;
            }
            string[] mileStoneList = mileStoneIDs.Split(',');
            foreach (var mileStoneID in mileStoneList)
            {
                var receiptObjs = receiptObjInfo.Where(c => c.MileStoneID.Contains(mileStoneID)).ToList();
                if (receiptObjs.Count() <= 0)
                {
                    continue;
                }
                foreach (var item in receiptObjs)
                {
                    var aboutMileStone = mileStoneInfo.Where(c => item.MileStoneID.Contains(c.ID));
                    if (aboutMileStone.Count() > 0)
                    {
                        var maxPlanFinishDate = aboutMileStone.Max(c => c.PlanFinishDate);
                        item.MileStonePlanEndDate = maxPlanFinishDate;

                        var maxFactFinishDate = aboutMileStone.Max(c => c.FactFinishDate);
                        item.MileStoneFactEndDate = maxFactFinishDate;

                        if (aboutMileStone.Count(c => !c.FactFinishDate.HasValue) == 0)
                        {
                            item.MileStoneState = true.ToString();
                        }

                        item.SyncSupplementary();
                    }
                }
            }
            marketEntities.SaveChanges();
            
        }
        /// <summary>
        /// 更新里程碑有关的收款项
        /// </summary>
        /// <param name="projectInfo">项目信息</param>
        public void UpdateReceiptObj(S_I_ProjectInfo projectInfo)
        {
            if (projectInfo != null)
            {
                if (projectInfo.S_P_MileStone.Count > 0)
                {
                    string mileStoneIDs = string.Join(",", projectInfo.S_P_MileStone.Select(c => c.ID).ToList());
                    UpdateReceiptObj(mileStoneIDs);
                }
            }
        }
        */
    }
}
