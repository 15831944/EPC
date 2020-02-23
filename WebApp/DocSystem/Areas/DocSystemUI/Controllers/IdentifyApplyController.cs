using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using Workflow.Logic;
using Base.Logic.BusinessFacade;
using Formula;
using Formula.Helper;
using Config;
using Config.Logic;
using DocSystem;
using DocSystem.Logic;
using DocSystem.Logic.Domain;
using Workflow.Logic.Domain;

namespace DocSystem.Areas.DocSystemUI.Controllers
{
    public class IdentifyApplyController : DocSystemFormContorllor<S_I_IdentifyApply>
    {
        public override bool ExecTaskExec(S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing, string nextExecUserIDs, string nextExecUserNames, string nextExecUserIDsGroup, string nextExecRoleIDs, string nextExecOrgIDs, string execComment)
        {
            var entity = this.BusinessEntities.Set<S_I_IdentifyApply>().SingleOrDefault(c => c.ID == taskExec.S_WF_InsFlow.FormInstanceID);
            if (entity != null)
            {
                var state = IdentifyState.InFlow.ToString();
                var infoIDs = entity.S_I_IdentifyApply_DetailInfo.Select(a => a.IdentifyInfoID).ToArray();
                this.BusinessEntities.Set<S_I_IdentifyInfo>().Where(a => infoIDs.Contains(a.ID)).Update(a => a.State = state);
                this.BusinessEntities.SaveChanges();
            }
            return base.ExecTaskExec(taskExec, routing, nextExecUserIDs, nextExecUserNames, nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
        }

        public override JsonResult Delete()
        {
            var ids = Request["ID"];
            var entity = this.BusinessEntities.Set<S_I_IdentifyApply>().SingleOrDefault(c => c.ID == ids);
            if (entity != null)
            {
                var state = IdentifyState.Create.ToString();
                var infoIDs = entity.S_I_IdentifyApply_DetailInfo.Select(a => a.IdentifyInfoID).ToArray();
                this.BusinessEntities.Set<S_I_IdentifyInfo>().Where(a => infoIDs.Contains(a.ID)).Update(a => a.State = state);
            }
            return base.Delete();
        }

        protected override void OnFlowEnd(S_I_IdentifyApply entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            var state = IdentifyState.Identified.ToString();
            var infoIDs = entity.S_I_IdentifyApply_DetailInfo.Select(a => a.IdentifyInfoID).ToArray();
            this.BusinessEntities.Set<S_I_IdentifyInfo>().Where(a => infoIDs.Contains(a.ID)).Update(a => a.State = state);
            //添加鉴定记录
            var IdentifyApplyDetails=entity.S_I_IdentifyApply_DetailInfo.Select(a => a).ToList();
            InventoryType type = new InventoryType();
            foreach (var IdentifyApplyDetail in IdentifyApplyDetails)
            {
                var identifyInfo=this.BusinessEntities.Set<S_I_IdentifyInfo>().FirstOrDefault(b => b.ID.Equals(IdentifyApplyDetail.IdentifyInfoID));
                S_NodeInfo node = new S_NodeInfo(identifyInfo.NodeID, FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(b => b.ID.Equals(identifyInfo.SpaceID)));
                if (IdentifyApplyDetail.IdentifyResult.Equals(InventoryType.Destroy.ToString()))
                    type = InventoryType.Destroy;
                else
                    type = InventoryType.ChangePeriod;
                InventoryFO.CreateNewInventoryLedger(node, "鉴定结果为："+EnumBaseHelper.GetEnumDescription(type.GetType(),type.ToString()), type);
            }
        }
    }
}
