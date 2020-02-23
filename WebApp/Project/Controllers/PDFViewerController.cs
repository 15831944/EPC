using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO;
using Config.Logic;
using Project.Logic.Domain;
using Formula.Helper;
using Formula;

namespace Project.Controllers
{
    public class PDFViewerController : ProjectController
    {
        public ActionResult List()
        {
            ViewBag.IsApply = GetQueryString("IsApply");
            ViewBag.SignRolePDF = EnumBaseHelper.GetEnumDef("Project.SignRolePDF").EnumItem.ToList();
            return View();
        }

        public ActionResult Viewer()
        {
            var list = new List<object>();
            var frameInfos = this.entities.Set<S_F_FrameInfo_FrameManageInfo>().ToList();
            var borderConfigs = this.entities.Set<S_F_BorderConfig>().ToList();
            foreach (var item in frameInfos)
            {
                var bc = borderConfigs.Where(a => a.ManageInfoID == item.ID).Select(a => new { a.Category, a.Width, a.Height, a.Angle, a.CharHeight });
                if (bc.Count() == 0)
                    bc = borderConfigs.Where(a => a.CurrentDefault == "1").Select(a => new { a.Category, a.Width, a.Height, a.Angle, a.CharHeight });
                var obj = new { text = item.TemplateName, value = item.TemplateName, borderConfig = bc };
                list.Add(obj);
            }
            ViewBag.FrameInfos = "var frameInfos = " + JsonHelper.ToJson(list);
            return View();
        }

        public JsonResult GetRelateInfo(string ProductID, string Version)
        {
            var result = new Dictionary<string, object>();
            var sql = "select top 1 * from S_E_ProductVersion with(nolock) where ProductID='" + ProductID + "' and Version='" + Version + "'";
            var productVersions = SqlHelper.ExecuteList<S_E_ProductVersion>(sql);
            if (productVersions.Count() == 0)
                throw new Formula.Exceptions.BusinessException("找不到成果ID为【" + ProductID + "】版本为【" + Version + "】的成果！");
            var pdfPositon = JsonHelper.ToObject(productVersions[0].PDFSignPositionInfo);
            result.SetValue("Position", pdfPositon);
            result.SetValue("TF", productVersions[0].FileSize);

            var groupIDs = new List<string>();
            var sealIDs = new List<string>();
            var groupInfo = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(productVersions[0].PlotSealGroup))
            {
                if (productVersions[0].PlotSealGroup.Contains('{'))
                {
                    var dic = JsonHelper.ToObject(productVersions[0].PlotSealGroup);
                    groupIDs.AddRange(dic.GetValue("Groups").Split(','));
                    sealIDs.AddRange(dic.GetValue("Seals").Split(','));
                }
                else
                    groupIDs.AddRange(productVersions[0].PlotSealGroup.Split(','));
            }
            foreach (var gID in groupIDs)
            {
                if (!string.IsNullOrEmpty(gID))
                {
                    var group = _GetPlotSealGroupInfo(gID);
                    groupInfo.SetValue(group.GetValue("BlockKey"), group);
                }
            }
            foreach (var sID in sealIDs)
            {
                if (!string.IsNullOrEmpty(sID))
                {
                    var seal = _GetPlotSealInfo(sID);
                    groupInfo.SetValue(seal.GetValue("BlockKey"), seal);
                }
            }
            result.SetValue("GroupInfo", groupInfo);
            return Json(result);
        }

        public JsonResult GetTemplate(string TemplateID)
        {
            var sql = "select * from S_EP_PlotSealTemplate_GroupList where S_EP_PlotSealTemplateID='" + TemplateID + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql);
            var result = new Dictionary<string, object>();
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["GroupID"].ToString()))
                {
                    var group = new Dictionary<string, object>();
                    if (row["Category"].ToString().ToLower() == "group")
                        group = _GetPlotSealGroupInfo(row["GroupID"].ToString());
                    else
                        group = _GetPlotSealInfo(row["GroupID"].ToString());
                    group.SetValue("PositionXs", row["PositionXs"]);
                    group.SetValue("PositionYs", row["PositionYs"]);
                    group.SetValue("Category", row["Category"]);
                    group.SetValue("Angle", row["Angle"]);
                    group.SetValue("PageNum", row["PageNum"]);
                    list.Add(group);
                }
                else
                    list.Add(Formula.FormulaHelper.DataRowToDic(row));
            }
            result.SetValue("GroupList", list);
            return Json(result);
        }

        public JsonResult GetNewSealInfo(string GroupID, string Type)
        {
            if (Type.ToLower() == "group")
                return Json(_GetPlotSealGroupInfo(GroupID));
            else
                return Json(_GetPlotSealInfo(GroupID));
        }

        /// <summary>
        /// 根据传入的GroupIDs获取选择的出图章或者组合章
        /// </summary>
        /// <param name="GroupIDs">
        /// 格式为{"Groups":"****,****","Seals":"****,****"}
        /// 需要兼容以前只选组合章 格式为****,****
        /// </param>
        /// <returns></returns>
        public JsonResult GetPlotSealGroupInfos(string GroupIDs)
        {
            var groupIDs = new List<string>();
            var sealIDs = new List<string>();
            var result = new Dictionary<string, object>();
            var groupInfo = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(GroupIDs))
            {
                if (GroupIDs.Contains('{'))
                {
                    var dic = JsonHelper.ToObject(GroupIDs);
                    groupIDs.AddRange(dic.GetValue("Groups").Split(','));
                    sealIDs.AddRange(dic.GetValue("Seals").Split(','));
                }
                else
                    groupIDs.AddRange(GroupIDs.Split(','));
            }
            foreach (var gID in groupIDs)
            {
                if (!string.IsNullOrEmpty(gID))
                {
                    var group = _GetPlotSealGroupInfo(gID);
                    groupInfo.SetValue(group.GetValue("BlockKey"), group);
                }
            }
            foreach (var sID in sealIDs)
            {
                if (!string.IsNullOrEmpty(sID))
                {
                    var seal = _GetPlotSealInfo(sID);
                    groupInfo.SetValue(seal.GetValue("BlockKey"), seal);
                }
            }
            result.SetValue("GroupInfo", groupInfo);
            return Json(result);
        }

        private Dictionary<string, object> _GetPlotSealGroupInfo(string groupID)
        {
            var sealGroup = this.entities.Set<S_EP_PlotSealGroup>().FirstOrDefault(a => a.ID == groupID);
            if (sealGroup == null)
                throw new Formula.Exceptions.BusinessException("ID为【" + groupID + "】的出图章组合不存在！");
            var seals = sealGroup.S_EP_PlotSealGroup_GroupInfo;
            var mainSeal = seals.FirstOrDefault(a => a.IsMain == "true");
            if (mainSeal == null)
                throw new Formula.Exceptions.BusinessException("ID为【" + groupID + "】的出图章组合没有主章！");
            var followSeals = seals.Where(a => a.IsMain != "true");
            var group = new Dictionary<string, object>();
            group.SetValue("BlockKey", mainSeal.BlockKey);
            group.SetValue("GroupID", groupID);
            group.SetValue("GroupName", sealGroup.Name);
            group.SetValue("MainID", mainSeal.PlotSeal);
            group.SetValue("Name", mainSeal.Name);
            group.SetValue("Width", mainSeal.Width);
            group.SetValue("Height", mainSeal.Height);
            var follows = new List<Dictionary<string, object>>();
            foreach (var fSeal in followSeals)
            {
                var fS = new Dictionary<string, object>();
                fS.SetValue("FollowID", fSeal.PlotSeal);
                fS.SetValue("Name", fSeal.Name);
                fS.SetValue("Width", fSeal.Width);
                fS.SetValue("Height", fSeal.Height);
                fS.SetValue("CorrectPosX", fSeal.CorrectPosX);
                fS.SetValue("CorrectPosY", fSeal.CorrectPosY);
                follows.Add(fS);
            }
            group.SetValue("Follows", follows);
            group.SetValue("Category", "Group");

            return group;
        }

        private Dictionary<string, object> _GetPlotSealInfo(string sealID)
        {
            var sealInfo = this.entities.Set<S_EP_PlotSealInfo>().FirstOrDefault(a => a.ID == sealID);
            if (sealInfo == null)
                throw new Formula.Exceptions.BusinessException("ID为【" + sealID + "】的图章不存在！");
            var seal = new Dictionary<string, object>();
            seal.SetValue("BlockKey", sealInfo.BlockKey);
            seal.SetValue("GroupID", sealID);
            seal.SetValue("GroupName", sealInfo.Name);
            seal.SetValue("MainID", sealID);
            seal.SetValue("Name", sealInfo.Name);
            seal.SetValue("Width", sealInfo.Width);
            seal.SetValue("Height", sealInfo.Height);
            seal.SetValue("Category", sealInfo.Type);
            var follows = new List<Dictionary<string, object>>();
            seal.SetValue("Follows", follows);
            return seal;
        }

        public FileResult GetEmptyPDF()
        {
            var path = Server.MapPath("/Project/Views/PDFViewer/empty.pdf");
            if (!System.IO.File.Exists(path))
                throw new Formula.Exceptions.BusinessException("服务器上找不到浏览文件！");
            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];
            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return File(buff, "application/pdf");
        }

        public FileResult GetFile(string fileID)
        {
            byte[] file = FileStoreHelper.GetFile(fileID);

            if (file != null)
                return File(file, "application/pdf");
            else
                throw new Formula.Exceptions.WebException("服务器上找不到浏览文件！");
        }

        public JsonResult SavePosition(string ProductID, string Version,
            string PdfPositionInfo, string GroupIDs, string GroupNames, string GroupKeys)
        {
            var ver = Convert.ToDecimal(Version);
            var product = this.entities.Set<S_E_Product>().FirstOrDefault(a => a.ID == ProductID && a.Version == ver);
            if (product != null)
            {
                product.PDFSignPositionInfo = PdfPositionInfo;
                product.PlotSealGroup = GroupIDs;
                product.PlotSealGroupName = GroupNames;
                product.PlotSealGroupKey = GroupKeys;
            }
            var version = this.entities.Set<S_E_ProductVersion>().FirstOrDefault(a => a.ProductID == ProductID && a.Version == ver);
            if (version != null)
            {
                version.PDFSignPositionInfo = PdfPositionInfo;
                version.PlotSealGroup = GroupIDs;
                version.PlotSealGroupName = GroupNames;
                version.PlotSealGroupKey = GroupKeys;
            }
            this.entities.SaveChanges();
            return Json("");
        }

        public JsonResult ReSign(string Products)
        {
            var list = JsonHelper.ToList(Products);
            foreach (var item in list)
            {
                var ver = Convert.ToDecimal(item.GetValue("Version"));
                var productID = item.GetValue("ProductID");
                var product = this.entities.Set<S_E_Product>().FirstOrDefault(a => a.ID == productID && a.Version == ver);
                if (product != null)
                {
                    product.SignState = "ReSign";
                    product.SignPdfFile = "";
                }
                var version = this.entities.Set<S_E_ProductVersion>().FirstOrDefault(a => a.ProductID == productID && a.Version == ver);
                if (version != null)
                {
                    version.SignState = "ReSign";
                    version.SignPdfFile = "";
                }
            }
            this.entities.SaveChanges();
            return Json("");
        }
    }
}
