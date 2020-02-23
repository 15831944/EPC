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
using Base.Logic.Domain;
using System.Text;

namespace DocSystem.Areas.DocSystemUI.Controllers
{
    public class DestroyApplyController : DocSystemFormContorllor<S_I_DestroyApply>
    {
        public override bool ExecTaskExec(S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing, string nextExecUserIDs, string nextExecUserNames, string nextExecUserIDsGroup, string nextExecRoleIDs, string nextExecOrgIDs, string execComment)
        {
            var entity = this.BusinessEntities.Set<S_I_DestroyApply>().SingleOrDefault(c => c.ID == taskExec.S_WF_InsFlow.FormInstanceID);
            if (entity != null)
            {
                var state = IdentifyState.InDestroy.ToString();
                var IdentifyInfoIDs = entity.S_I_DestroyApply_DetailInfo.Select(a => a.IdentifyInfoID).ToArray();
                var IdentifyApplyDetailInfoIDs = entity.S_I_DestroyApply_DetailInfo.Select(a => a.IdentifyApplyDetailInfoID).ToArray();

                this.BusinessEntities.Set<S_I_IdentifyInfo>().Where(a => IdentifyInfoIDs.Contains(a.ID)).Update(a => a.State = state);
                this.BusinessEntities.Set<S_I_IdentifyApply_DetailInfo>().Where(a => IdentifyApplyDetailInfoIDs.Contains(a.ID)).Update(a => a.HandleResult = state);
                this.BusinessEntities.SaveChanges();
            }
            return base.ExecTaskExec(taskExec, routing, nextExecUserIDs, nextExecUserNames, nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
        }

        public override JsonResult Delete()
        {
            var ids = Request["ID"];
            var entity = this.BusinessEntities.Set<S_I_DestroyApply>().SingleOrDefault(c => c.ID == ids);
            if (entity != null)
            {
                var IdentifiedState = IdentifyState.Identified.ToString();
                var state = IdentifyState.Create.ToString();
                var infoIDs = entity.S_I_DestroyApply_DetailInfo.Select(a => a.IdentifyInfoID).ToArray();
                var IdentifyApplyDetailInfoIDs = entity.S_I_DestroyApply_DetailInfo.Select(a => a.IdentifyApplyDetailInfoID).ToArray();
                this.BusinessEntities.Set<S_I_IdentifyInfo>().Where(a => infoIDs.Contains(a.ID)).Update(a => a.State = IdentifiedState);
                this.BusinessEntities.Set<S_I_IdentifyApply_DetailInfo>().Where(a => IdentifyApplyDetailInfoIDs.Contains(a.ID)).Update(a => a.HandleResult = state);
            }
            return base.Delete();
        }

        protected override void OnFlowEnd(S_I_DestroyApply entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            var state = IdentifyState.Finish.ToString();
            var infoIDs = entity.S_I_DestroyApply_DetailInfo.Select(a => a.IdentifyInfoID).ToArray();
            var IdentifyApplyDetailInfoIDs = entity.S_I_DestroyApply_DetailInfo.Select(a => a.IdentifyApplyDetailInfoID).ToArray();
            this.BusinessEntities.Set<S_I_IdentifyApply_DetailInfo>().Where(a => IdentifyApplyDetailInfoIDs.Contains(a.ID)).Update(a => a.HandleResult = state);

            var apply = this.BusinessEntities.Set<S_I_IdentifyApply>().FirstOrDefault(a => a.ID == entity.IdentifyApplyID);
            if (apply != null)
            {
                if (apply.S_I_IdentifyApply_DetailInfo.Count(a => !IdentifyApplyDetailInfoIDs.Contains(a.ID) && a.HandleResult != state) == 0)
                    apply.HandleState = HandleState.Finish.ToString();
                else
                    apply.HandleState = HandleState.Part.ToString();
            }

            var updateDic = new Dictionary<S_DOC_Space, StringBuilder>();
            var infos = this.BusinessEntities.Set<S_I_IdentifyInfo>().Where(a => infoIDs.Contains(a.ID)).ToList();
            foreach (var info in infos)
            {
                info.State = state;
                var space = DocConfigHelper.CreateConfigSpaceByID(info.SpaceID);
                var nodeInfo = new S_NodeInfo(info.NodeID, space);

                if (!updateDic.ContainsKey(space))
                    updateDic.Add(space, new StringBuilder());

                updateDic[space].AppendLine(string.Format(@"
update S_NodeInfo set [State] = '{0}' where FullPathID like '{1}%' 
update S_FileInfo set [State] = '{0}' where FullNodeID like '{1}%'", DocState.Invalid.ToString(), nodeInfo.FullPathID));
            }

            foreach (var item in updateDic.Keys)
            {
                var instanceDB = SQLHelper.CreateSqlHelper(item.SpaceKey, item.ConnectString);
                instanceDB.ExecuteNonQuery(updateDic[item].ToString());
            }
            //添加销毁记录
            var IdentifyApplyDetails = this.BusinessEntities.Set<S_I_IdentifyApply_DetailInfo>().Where(a => IdentifyApplyDetailInfoIDs.Contains(a.ID)).Select(a => a).ToList();

            foreach (var IdentifyApplyDetail in IdentifyApplyDetails)
            {
                var identifyInfo = this.BusinessEntities.Set<S_I_IdentifyInfo>().FirstOrDefault(b => b.ID.Equals(IdentifyApplyDetail.IdentifyInfoID));
                S_NodeInfo node = new S_NodeInfo(identifyInfo.NodeID, FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(b => b.ID.Equals(identifyInfo.SpaceID)));
                InventoryFO.CreateNewInventoryLedger(node, "", InventoryType.Destroy);
            }
        }
    }
}
