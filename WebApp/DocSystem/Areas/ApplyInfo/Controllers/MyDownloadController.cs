using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdapter;
using DocSystem.Logic.Domain;
using Formula.DynConditionObject;
using Formula;
using Formula.Helper;
using Config.Logic;
using Config;
using System.Collections;
using DocSystem.Logic;

namespace DocSystem.Areas.ApplyInfo.Controllers
{
    public class MyDownloadController : DocConstController<S_DownloadDetail>
    {
        public override JsonResult GetList(QueryBuilder qb)
        {
            string sql = "select *,datediff(dd,getdate(),[DownloadExpireDate]) as RemainDay from S_DownloadDetail where DownloadExpireDate>='" + DateTime.Now.ToString() + "' and CreateUserID='" + FormulaHelper.GetUserInfo().UserID + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql, qb);
            var gd = new GridData(dt);
            gd.total = qb.TotolCount;
            return Json(gd);
        }

        public JsonResult GetExpireList(QueryBuilder qb)
        {
            string sql = "select * from S_DownloadDetail where DownloadExpireDate<'" + DateTime.Now.ToString() + "' and CreateUserID='" + FormulaHelper.GetUserInfo().UserID + "'";
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.DocConst).ExecuteDataTable(sql, qb);
            var gd = new GridData(dt);
            gd.total = qb.TotolCount;
            return Json(gd);
        }

        public JsonResult GetFileAttachments(string ListData)
        {
            var configEntities = FormulaHelper.GetEntities<DocConfigEntities>();
            var spaceList = configEntities.Set<S_DOC_Space>().ToList();

            var fileType = new string[] { "Attachments", "SWFFile", "MainFile", "PDFFile", "PlotFile", "XrefFile", "DwfFile", "TiffFile", "SignPdfFile" };
            var result = new List<string>();
            var list = JsonHelper.ToList(ListData);
            foreach (var row in list)
            {
                var space = spaceList.FirstOrDefault(a => a.ID == row.GetValue("SpaceID"));
                if (space != null)
                {
                    var fileInfo = new S_FileInfo(row["FileID"].ToString(), space);
                    foreach (var type in fileType)
                    {
                        var fileData = fileInfo.CurrentAttachment.DataEntity.GetValue(type);
                        if (!string.IsNullOrEmpty(fileData))
                        {
                            foreach (var item in fileData.Split(','))
                            {
                                var fileID = item.Split('_')[0];
                                if (!result.Contains(fileID))
                                    result.Add(fileID);
                            }
                        }
                    }
                }
            }
            return Json(new { FileIDs = string.Join(",", result) });
        }

        public void SetExpire(string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            foreach (var item in list)
            {
                string id = item.GetValue("ID");
                var detail = this.entities.Set<S_DownloadDetail>().Find(id);
                detail.DownloadExpireDate = new DateTime(1900, 1, 1);
            }
            this.entities.SaveChanges();
        }

        public JsonResult ApplyReDownload()
        {
            string fileID = this.GetQueryString("files");
            string SpaceID = this.GetQueryString("SpaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("为找到【ID】为【" + SpaceID + "】的图档实例库");
            S_FileInfo fileInfo = new S_FileInfo(fileID, space);
            if (fileInfo.ConfigInfo == null) throw new Formula.Exceptions.BusinessException("未能找到文件【" + fileInfo.ID + "】的配置对象信息");
            if (fileInfo.CurrentAttachment == null)
                throw new Formula.Exceptions.BusinessException("附件不存在");
            if (fileInfo.DataEntity.GetValue("StorageType") != StorageType.Electronic.ToString())
                throw new Formula.Exceptions.BusinessException("实物文件不能下载！");
            CarFO.CreateCarItem(fileInfo, ItemType.DownLoad.ToString());
            this.entities.SaveChanges();
            return Json(new { });
        }
    }
}
