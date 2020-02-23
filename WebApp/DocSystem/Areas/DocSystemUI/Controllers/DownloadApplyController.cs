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
    public class DownloadApplyController : DocSystemFormContorllor<T_D_DownloadApply>
    {
        public override bool ExecTaskExec(Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing, string nextExecUserIDs, string nextExecUserNames, string nextExecUserIDsGroup, string nextExecRoleIDs, string nextExecOrgIDs, string execComment)
        {
            var entity = this.BusinessEntities.Set<T_D_DownloadApply>().SingleOrDefault(c => c.ID == taskExec.S_WF_InsFlow.FormInstanceID);
            if (entity != null)
            {
                var state = ItemState.Audit.ToString();
                var carIds = entity.T_D_DownloadApply_DetailInfo.Select(a => a.CarInfoID).ToArray();
                this.BusinessEntities.Set<S_CarInfo>().Where(a => carIds.Contains(a.ID)).Update(a => a.State = state);
                this.BusinessEntities.SaveChanges();
            }
            return base.ExecTaskExec(taskExec, routing, nextExecUserIDs, nextExecUserNames, nextExecUserIDsGroup, nextExecRoleIDs, nextExecOrgIDs, execComment);
        }

        protected override void OnFlowEnd(T_D_DownloadApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            var carIds = entity.T_D_DownloadApply_DetailInfo.Select(a => a.CarInfoID).ToArray();
            var carItems = this.BusinessEntities.Set<S_CarInfo>().Where(a => carIds.Contains(a.ID));
            foreach (var item in entity.T_D_DownloadApply_DetailInfo.ToList())
            {
                var carItem = carItems.FirstOrDefault(a => a.ID == item.CarInfoID);
                carItem.State = ItemState.Finish.ToString();

                #region 创建S_DownloadDetail
                int downloadExpireDate = 7;
                var DocDownloadExpireDays = System.Configuration.ConfigurationManager.AppSettings["DocDownloadExpireDays"];
                if (!string.IsNullOrEmpty(DocDownloadExpireDays))
                    downloadExpireDate = Convert.ToInt32(DocDownloadExpireDays);
                S_DownloadDetail downloadDetail = new S_DownloadDetail
                {
                    ID = FormulaHelper.CreateGuid(),
                    DownloadID = entity.ID,
                    Name = item.Name,
                    Code = item.Code,
                    SpaceID = carItem.SpaceID,
                    ConfigID = carItem.ConfigID,
                    FileID = carItem.FileID,
                    CreateUserID = entity.ApplyUser,
                    CreateUserName = entity.ApplyUserName,
                    CreateDate = DateTime.Now,
                    UserDeptID = entity.ApplyDept,
                    UserDeptName = entity.ApplyDeptName,
                    PassDate = DateTime.Now,
                    DownloadState = ItemState.Finish.ToString(),
                    DownloadExpireDate = Convert.ToDateTime(DateTime.Now.AddDays(downloadExpireDate).ToShortDateString() + " 23:59:59")
                };
                this.BusinessEntities.Set<S_DownloadDetail>().Add(downloadDetail);
                #endregion

                S_FileInfo file = new S_FileInfo(carItem.FileID, FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(carItem.SpaceID)));
                InventoryFO.CreateNewInventoryLedger(file, "", InventoryType.DownLoad);
            }
            this.BusinessEntities.SaveChanges();
        }
    }
}
