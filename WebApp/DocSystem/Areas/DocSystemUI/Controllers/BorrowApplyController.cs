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

namespace DocSystem.Areas.DocSystemUI.Controllers
{
    public class BorrowApplyController : DocSystemFormContorllor<T_B_BorrowApply>
    {
        public override bool ExecTaskExec(Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing, string nextExecUserIDs, string nextExecUserNames, string nextExecUserIDsGroup, string nextExecRoleIDs, string nextExecOrgIDs, string execComment)
        {
            var entity = this.BusinessEntities.Set<T_B_BorrowApply>().SingleOrDefault(c => c.ID == taskExec.S_WF_InsFlow.FormInstanceID);
            if (entity != null)
            {
                var state = ItemState.Audit.ToString();
                var carIds = entity.T_B_BorrowApply_DetailInfo.Select(a => a.CarInfoID).ToArray();
                this.BusinessEntities.Set<S_CarInfo>().Where(a => carIds.Contains(a.ID)).Update(a => a.State = state);
                this.BusinessEntities.SaveChanges();
            }
            return base.ExecTaskExec(taskExec, routing, nextExecUserIDs, nextExecUserNames, nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
        }

        protected override void OnFlowEnd(T_B_BorrowApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            var carIds = entity.T_B_BorrowApply_DetailInfo.Select(a => a.CarInfoID).ToArray();
            var carItems = this.BusinessEntities.Set<S_CarInfo>().Where(a => carIds.Contains(a.ID));
            foreach (var item in entity.T_B_BorrowApply_DetailInfo.ToList())
            {
                var carItem = carItems.FirstOrDefault(a => a.ID == item.CarInfoID);
                if (carItem != null)
                {
                    carItem.State = ItemState.Finish.ToString();

                    #region 创建S_BorrowDetail
                    S_BorrowDetail borrowDetail = new S_BorrowDetail
                    {
                        ID = FormulaHelper.CreateGuid(),
                        BorrowID = entity.ID,
                        SpaceID = carItem.SpaceID,
                        SpaceName = item.SpaceName,
                        DataType = carItem.DataType,
                        DetailType = string.IsNullOrEmpty(item.FileID) ? NodeType.Node.ToString() : NodeType.File.ToString(),
                        RelateID = string.IsNullOrEmpty(item.FileID) ? item.NodeID : item.FileID,
                        ConfigID = carItem.ConfigID,
                        Name = item.Name,
                        Code = item.Code,
                        CreateUserID = entity.ApplyUser,
                        CreateUserName = entity.ApplyUserName,
                        CreateDeptID = entity.ApplyDept,
                        CreateDeptName = entity.ApplyDeptName,
                        CreateDate = DateTime.Now,
                        ApplyNumber = item.ApplyAmount,
                        BorrowState = BorrowDetailState.ToLend.ToString()
                    };
                    this.BusinessEntities.Set<S_BorrowDetail>().Add(borrowDetail);
                    #endregion
                }
                S_FileInfo file = new S_FileInfo(item.FileID, FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(item.SpaceID)));
                InventoryFO.CreateNewInventoryLedger(file, "", InventoryType.BorrowFile);
            }
            this.BusinessEntities.SaveChanges();
        }

        public JsonResult GetBorrowDetailTree(string ListData)
        {
            var list = JsonHelper.ToObject<List<T_B_BorrowApply_DetailInfo>>(ListData);
            var spaceList = new List<S_DOC_Space>();

            #region 创建上级节点的集合
            var result = new DataTable();
            result.Columns.Add("ID");
            result.Columns.Add("UID");
            result.Columns.Add("ParentUID");
            result.Columns.Add("Name");
            result.Columns.Add("Code");
            result.Columns.Add("SpaceName");
            result.Columns.Add("DataType");
            result.Columns.Add("ApplyAmount");
            result.Columns.Add("IsEdit");
            #endregion

            foreach (var item in list)
            {
                var space = spaceList.FirstOrDefault(a => a.ID == item.SpaceID);
                if (space == null)
                {
                    space = DocConfigHelper.CreateConfigSpaceByID(item.SpaceID);
                    spaceList.Add(space);
                }
                var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                var fullID = "";
                if (string.IsNullOrEmpty(item.FileID))
                {
                    var nodeInfo = new S_NodeInfo(item.NodeID, space);
                    fullID = nodeInfo.FullPathID;

                    var newRow = result.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == nodeInfo.ID);
                    if (newRow == null)
                    {
                        newRow = result.NewRow();
                        result.Rows.Add(newRow);
                    }

                    newRow["ID"] = "";
                    newRow["UID"] = nodeInfo.ID;
                    newRow["ParentUID"] = nodeInfo.ParentID;
                    newRow["Name"] = nodeInfo.Name;
                    newRow["Code"] = nodeInfo.DataEntity.GetValue("DocumentCode");
                    newRow["SpaceName"] = space.Name;
                    newRow["DataType"] = item.DataType;
                    newRow["ApplyAmount"] = item.ApplyAmount;
                    newRow["IsEdit"] = true;
                }
                else
                {
                    var fileInfo = new S_FileInfo(item.FileID, space);
                    fullID = fileInfo.FullNodeID;

                    var newRow = result.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == fileInfo.ID);
                    if (newRow == null)
                    {
                        newRow = result.NewRow();
                        result.Rows.Add(newRow);
                    }

                    newRow["ID"] = "";
                    newRow["UID"] = fileInfo.ID;
                    newRow["ParentUID"] = fileInfo.NodeID;
                    newRow["Name"] = fileInfo.Name;
                    newRow["Code"] = fileInfo.DataEntity.GetValue("Code");
                    newRow["SpaceName"] = space.Name;
                    newRow["DataType"] = item.DataType;
                    newRow["ApplyAmount"] = item.ApplyAmount;
                    newRow["IsEdit"] = true;
                }
                string sql = String.Format("select * from S_NodeInfo where ID in ('{0}') ", fullID.Replace(".", "','"));
                var ancestors = instanceDB.ExecuteDataTable(sql);
                foreach (DataRow ancestor in ancestors.Rows)
                    FillTreeData(result, ancestor, space.Name);
            }

            return Json(result);
        }

        private void FillTreeData(DataTable dt, DataRow row, string spaceName)
        {
            if (dt.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == row["ID"].ToString()) == null)
            {
                var newRow = dt.NewRow();
                newRow["ID"] = "";
                newRow["UID"] = row["ID"].ToString();
                newRow["ParentUID"] = row["ParentID"].ToString();
                newRow["Name"] = row["Name"].ToString();
                newRow["Code"] = row["DocumentCode"].ToString();
                newRow["SpaceName"] = spaceName;
                newRow["DataType"] = "";
                newRow["ApplyAmount"] = "";
                newRow["IsEdit"] = false;

                dt.Rows.Add(newRow);
            }
        }
    }
}
