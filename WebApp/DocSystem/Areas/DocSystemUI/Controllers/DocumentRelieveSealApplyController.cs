using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DocSystem.Logic.Domain;
using Workflow.Logic.Domain;
using DocSystem.Logic;
using Config;
using Config.Logic;
using System.Data;
using System.Text;
using Formula.Helper;

namespace DocSystem.Areas.DocSystemUI.Controllers
{
    public class DocumentRelieveSealApplyController : DocSystemFormContorllor<S_SU_DocumentRelieveSeal>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var detailJsonStr = dic.GetValue("DetailList");
            var spaceList = new List<S_DOC_Space>();
            if (!string.IsNullOrEmpty(detailJsonStr))
            {
                var formID = dic.GetValue("ID");
                var subTableDataList = JsonHelper.ToObject<List<Dictionary<string, string>>>(detailJsonStr);
                var spaceIDs = subTableDataList.Where(a => !string.IsNullOrEmpty(a.GetValue("SpaceID"))).Select(a => a.GetValue("SpaceID")).Distinct();
                foreach (var spaceID in spaceIDs)
                {
                    //所有关联节点的ID
                    var nodeIds = subTableDataList.Where(c => c.GetValue("SpaceID") == spaceID && !string.IsNullOrEmpty(c.GetValue("RelateID"))).Select(c => c.GetValue("RelateID")).ToList();
                    //获取全部子表项ID
                    var oldIds = this.DocConstSQLDB.ExecuteDataTable(string.Format("select RelateID from S_SU_DocumentRelieveSeal_DetailList where S_SU_DocumentRelieveSealID='{0}' and SpaceID='{1}'", formID, spaceID))
                       .AsEnumerable().Select(c => c.Field<string>("RelateID")).ToList();
                    //本次移除的节点ID
                    string notExistIDs = string.Join("','", oldIds.Where(c => !nodeIds.Contains(c)).ToArray());

                    var space = spaceList.FirstOrDefault(a => a.ID == spaceID);
                    if (space == null)
                    {
                        space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
                        spaceList.Add(space);
                    }
                    var db = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                    StringBuilder sb = new StringBuilder();
                    foreach (var nodeid in nodeIds)
                    {
                        sb.AppendLine("update S_NodeInfo set State = '" + DocState.SealUpApply.ToString() + "',RackNumber='" + dic.GetValue("SealUpRackNumber") + "',RackNumberName='" + dic.GetValue("SealUpRackNumberName") + "',StorageRoom='" + dic.GetValue("SealUpStorageRoom") + "',StorageRoomName='" + dic.GetValue("SealUpStorageRoomName") + "' where State <>'" + DocState.SealUpApply.ToString() + "' and FullPathID like '%" + nodeid + "%'");
                        sb.AppendLine("update S_FileInfo set State = '" + DocState.SealUpApply.ToString() + "' where State <>'" + DocState.SealUpApply.ToString() + "' and FullNodeID like '%" + nodeid + "%'");
                    }
                    foreach (var nodeid in notExistIDs)
                    {
                        sb.AppendLine("update S_NodeInfo set State = '" + DocState.SealUp.ToString() + "' where State ='" + DocState.SealUpApply.ToString() + "' and FullPathID like '%" + nodeid + "%'");
                        sb.AppendLine("update S_FileInfo set State = '" + DocState.SealUp.ToString() + "' where  State ='" + DocState.SealUpApply.ToString() + "' and FullNodeID like '%" + nodeid + "%'");
                    }
                    if (sb.Length > 0)
                        db.ExecuteNonQuery(sb.ToString());

                }
            }
        }

        public override JsonResult Delete()
        {
            var ids = Request["ID"];
            var entity = this.BusinessEntities.Set<S_SU_DocumentRelieveSeal>().SingleOrDefault(c => c.ID == ids);
            if (entity != null)
            {
                var detailIst = entity.S_SU_DocumentRelieveSeal_DetailList.ToList();
                var spaceList = new List<S_DOC_Space>();
                var spaceIDs = detailIst.Where(a => !string.IsNullOrEmpty(a.SpaceID)).Select(a => a.SpaceID).ToList();
                foreach (var spaceID in spaceIDs)
                {
                    var nodeIds = detailIst.Where(a => a.SpaceID == spaceID && !string.IsNullOrEmpty(a.RelateID)).Select(a => a.RelateID);
                    StringBuilder sb = new StringBuilder();
                    var space = spaceList.FirstOrDefault(a => a.ID == spaceID);
                    if (space == null)
                    {
                        space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
                        spaceList.Add(space);
                    }
                    var db = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                    foreach (var nodeid in nodeIds)
                    {
                        //将解封申请中、解封状态的节点及文件设置为禁用
                        sb.AppendLine("update S_NodeInfo set State = '" + DocState.SealUp.ToString() + "' where State in ('" + DocState.SealUpApply.ToString() + "','" + DocState.Normal.ToString() + "') and FullPathID like '%" + nodeid + "%'");
                        sb.AppendLine("update S_FileInfo set State = '" + DocState.SealUp.ToString() + "' where State in ('" + DocState.SealUpApply.ToString() + "','" + DocState.Normal.ToString() + "') and FullNodeID like '%" + nodeid + "%'");
                    }
                    if (sb.Length > 0)
                        db.ExecuteNonQuery(sb.ToString());
                }
            }
            return base.Delete();
        }

        protected override void OnFlowEnd(S_SU_DocumentRelieveSeal entity, S_WF_InsTaskExec taskExec, S_WF_InsDefRouting routing)
        {
            var detailIst = entity.S_SU_DocumentRelieveSeal_DetailList.ToList();
            var spaceList = new List<S_DOC_Space>();
            var spaceIDs = detailIst.Where(a => !string.IsNullOrEmpty(a.SpaceID)).Select(a => a.SpaceID).ToList();
            foreach (var spaceID in spaceIDs)
            {
                var nodeIds = detailIst.Where(a => a.SpaceID == spaceID && !string.IsNullOrEmpty(a.RelateID)).Select(a => a.RelateID);
                StringBuilder sb = new StringBuilder();
                var space = spaceList.FirstOrDefault(a => a.ID == spaceID);
                if (space == null)
                {
                    space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
                    spaceList.Add(space);
                }
                var db = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                foreach (var nodeid in nodeIds)
                {
                    sb.AppendLine("update S_NodeInfo set State = '" + DocState.Normal.ToString() + "',RackNumber='" + entity.RelieveSealRackNumber + "',RackNumberName='" + entity.RelieveSealRackNumberName + "',StorageRoom='" + entity.RelieveSealStorageRoom + "',StorageRoomName='" + entity.RelieveSealStorageRoomName + "' where FullPathID like '%" + nodeid + "%'");
                    sb.AppendLine("update S_FileInfo set State = '" + DocState.Normal.ToString() + "' where FullNodeID like '%" + nodeid + "%'");
                }
                if (sb.Length > 0)
                    db.ExecuteNonQuery(sb.ToString());
            }
            foreach (var detail in detailIst)
            {
                if (detail.RelateType.Equals("Node"))
                {
                    S_NodeInfo node = new S_NodeInfo(detail.RelateID, Formula.FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(detail.SpaceID)));
                    InventoryFO.CreateNewInventoryLedger(node, "\"库房\"修改为：\""+entity.RelieveSealStorageRoomName+"\";\"货架号\"修改为：\""+entity.RelieveSealRackNumberName+"\"", InventoryType.RelieveSeal);
                }
                else
                {
                    S_FileInfo file = new S_FileInfo(detail.RelateID, Formula.FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(detail.SpaceID)));
                    InventoryFO.CreateNewInventoryLedger(file, "\"库房\"修改为：\"" + entity.RelieveSealStorageRoomName + "\";\"货架号\"修改为：\"" + entity.RelieveSealRackNumberName + "\"", InventoryType.RelieveSeal);
                }
            }
        }
    }
}
