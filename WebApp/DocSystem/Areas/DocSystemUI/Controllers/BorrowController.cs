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
    public class BorrowController : DocSystemFormContorllor<T_Borrow>
    {
        protected override void OnFlowExecute(T_Borrow entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            entity.BorrowState = ItemState.Audit.ToString();
            entity.SynchCarInfoState(ItemState.Audit.ToString());
            this.BusinessEntities.SaveChanges();
        }

        protected override void OnFlowEnd(T_Borrow entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity != null)
                entity.Push();
            this.BusinessEntities.SaveChanges();
            entity.SyncInstanceDataState();
        }

        public JsonResult ReturnDetail()
        {
            var ids = this.GetQueryString("ListIDs").Split(',');
            if (ids.Length == 0)
                ids = this.GetQueryString("ID").Split(',');
            var user = FormulaHelper.GetUserInfo();
            var list = this.BusinessEntities.Set<S_BorrowDetail>().Where(a => ids.Contains(a.ID)).ToList();
            foreach (var item in list)
            {
                if (item.BorrowState == BorrowReturnState.Return.ToString())
                    continue;
                item.ReturnDate = DateTime.Now;
                item.ReturnUserID = user.UserID;
                item.ReturnUser = user.UserName;
                item.BorrowState = BorrowReturnState.Return.ToString();
            }
            this.BusinessEntities.SaveChanges();
            var spaces = list.Select(a => a.SpaceID).Distinct().ToList();
            foreach (var spaceID in spaces)
            {
                var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
                if (space == null) continue;
                var fileIDs = list.Where(a => a.SpaceID == spaceID && a.DetailType == NodeType.File.ToString()).Select(a => a.RelateID);
                var nodeIDs = list.Where(a => a.SpaceID == spaceID && a.DetailType == NodeType.Node.ToString()).Select(a => a.RelateID);
                foreach (var fileID in fileIDs)
                {
                    var file = new S_FileInfo(fileID, space);
                    if (file == null) continue;
                    file.DataEntity.SetValue("BorrowState", "");
                    file.DataEntity.SetValue("BorrowUserID", "");
                    file.DataEntity.SetValue("BorrowUserName", "");
                    file.Save(false);
                }
                foreach (var nodeID in nodeIDs)
                {
                    var node = new S_NodeInfo(nodeID, space);
                    if (node == null) continue;
                    node.DataEntity.SetValue("BorrowState", "");
                    node.DataEntity.SetValue("BorrowUserID", "");
                    node.DataEntity.SetValue("BorrowUserName", "");
                    node.Save(false);
                }
            }
            return Json("");
        }

        public JsonResult ReturnFile()
        {
            var ids = this.GetQueryString("ListIDs").Split(',');
            if (ids.Length == 0)
                ids = this.GetQueryString("ID").Split(',');
            var spaceID = this.GetQueryString("SpaceID");
            var user = FormulaHelper.GetUserInfo();
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            foreach (var fileID in ids)
            {
                var file = new S_FileInfo(fileID, space);
                file.DataEntity.SetValue("BorrowState", "");
                file.DataEntity.SetValue("BorrowUserID", "");
                file.DataEntity.SetValue("BorrowUserName", "");
                file.Save(false);
            }
            var list = this.BusinessEntities.Set<S_BorrowDetail>().Where(a => ids.Contains(a.RelateID)).ToList();
            foreach (var item in list)
            {
                if (item.BorrowState == BorrowReturnState.Return.ToString())
                    continue;
                item.ReturnDate = DateTime.Now;
                item.ReturnUserID = user.UserID;
                item.ReturnUser = user.UserName;
                item.BorrowState = BorrowReturnState.Return.ToString();
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult ReturnNode()
        {
            var ids = this.GetQueryString("ListIDs").Split(',');
            if (ids.Length == 0)
                ids = this.GetQueryString("ID").Split(',');
            var spaceID = this.GetQueryString("SpaceID");
            var user = FormulaHelper.GetUserInfo();
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            foreach (var nodeID in ids)
            {
                var node = new S_NodeInfo(nodeID, space);
                node.DataEntity.SetValue("BorrowState", "");
                node.DataEntity.SetValue("BorrowUserID", "");
                node.DataEntity.SetValue("BorrowUserName", "");
                node.Save(false);
            }
            var list = this.BusinessEntities.Set<S_BorrowDetail>().Where(a => ids.Contains(a.RelateID)).ToList();
            foreach (var item in list)
            {
                if (item.BorrowState == BorrowReturnState.Return.ToString())
                    continue;
                item.ReturnDate = DateTime.Now;
                item.ReturnUserID = user.UserID;
                item.ReturnUser = user.UserName;
                item.BorrowState = BorrowReturnState.Return.ToString();
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }
    }
}
