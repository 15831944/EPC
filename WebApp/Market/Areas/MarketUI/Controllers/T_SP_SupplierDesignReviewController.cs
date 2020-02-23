using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Market.Logic.Domain;
using Formula;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierDesignReviewController : MarketFormContorllor<T_SP_DesignReview>
    {
        protected override void OnFlowEnd(T_SP_DesignReview entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity != null)
            {
                var suppierInfo = this.BusinessEntities.Set<S_SP_Supplier>().FirstOrDefault(d => d.ID == entity.Suppier);
                if (suppierInfo == null) throw new Formula.Exceptions.BusinessException("合作单位【" + entity.SuppierName + "】信息不存在，请重新确认或联系管理员！");
                var coopProjectInfo = new S_SP_Supplier_CoopProjectInfo();
                this.UpdateEntity<S_SP_Supplier_CoopProjectInfo>(coopProjectInfo, entity.ToDic());
                coopProjectInfo.ID = FormulaHelper.CreateGuid();
                coopProjectInfo.S_SP_SupplierID = entity.Suppier;
                this.BusinessEntities.Set<S_SP_Supplier_CoopProjectInfo>().Add(coopProjectInfo);

                var coopProjectList = this.BusinessEntities.Set<S_SP_Supplier_CoopProjectInfo>().Where(d => d.S_SP_SupplierID == entity.Suppier).ToList();
                if (coopProjectList.Count == 0)
                    coopProjectInfo.SortIndex = 0;
                else
                    coopProjectInfo.SortIndex = coopProjectList.Max(d => d.SortIndex) + 1;

                // 反写合作单位级别和状态到S_SP_Supplier
                suppierInfo.State = "已合作";

                var evaluateCount = coopProjectList.Count() + 1;

                var evaluates = coopProjectList.Sum(d => d.Evaluate);
                var evaluateSum = evaluates.HasValue ? evaluates.Value : 0;
                var suppierEvaluate = (evaluateSum + (entity.Evaluate.HasValue ? entity.Evaluate.Value : 0)) / evaluateCount;
                if (suppierEvaluate >= 90)
                    suppierInfo.SupplierLevel = "优";
                else if (suppierEvaluate >= 80)
                    suppierInfo.SupplierLevel = "良";
                else if (suppierEvaluate >= 70)
                    suppierInfo.SupplierLevel = "中";
                else if (suppierEvaluate >= 60)
                    suppierInfo.SupplierLevel = "合格";
                else
                    suppierInfo.SupplierLevel = "不合格";


                this.BusinessEntities.SaveChanges();
            }
        }

    }
}
