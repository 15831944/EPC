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
    public class DownloadController : DocSystemFormContorllor<T_Download>
    {
        protected override void OnFlowExecute(T_Download entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            entity.DownloadState = ItemState.Audit.ToString();
            entity.SynchCarInfoState(ItemState.Audit.ToString());
            this.BusinessEntities.SaveChanges();
        }

        protected override void OnFlowEnd(T_Download entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (entity != null)
                entity.Push();
            this.BusinessEntities.SaveChanges();
        }

        public void AddBorrowLog(T_Download_FileInfo item)
        {
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            var user = FormulaHelper.GetUserInfo();
            S_DocumentLog log = entities.S_DocumentLog.Create();
            log.ID = FormulaHelper.CreateGuid();
            log.LogType = "Download";
            log.Name = item.FileName;
            log.NodeID = item.NodeID;
            log.SpaceID = item.SpaceID;
            log.ConfigID = item.ConfigID;
            log.CreateDate = DateTime.Now;
            log.CreateUserID = user.UserID;
            log.CreateUserName = user.UserName;
            log.FileID = item.FileID;
            log.FullNodeID = "";
            entities.S_DocumentLog.Add(log);
        }
    }
}
