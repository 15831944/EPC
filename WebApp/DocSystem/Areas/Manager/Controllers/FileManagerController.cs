using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocSystem.Logic;
using DocInstance.Logic;
using DocSystem.Logic.Domain;
using System.Text;
using System.Data;
using MvcAdapter;
using Config;
using Config.Logic;
using System.Collections;
using Formula.Helper;
using Formula;

namespace DocSystem.Areas.Manager.Controllers
{
    public class FileManagerController : FileController
    {
        public ActionResult List()
        {
            string QueryKey = "";
            string btnTemplate = "<a class=\"mini-button\" iconcls=\"{1}\" onclick=\"{2}\" plain=\"true\" id=\"{3}\">{0}</a>";
            string configID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            string nodeID = this.Request["NodeID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var fileConfig = space.S_DOC_File.FirstOrDefault(d => d.ID == configID);
            if (fileConfig == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + configID + "】的节点定义对象");
            var grid = fileConfig.GetDataGrid(true, true, 40) as MiniDataGrid;
            grid.Url = "GetList"; grid.Multiselect = true;

            //var column = new MiniGridColumn();
            //column.Field = "BorrowUserName";
            //column.HeaderText = "借阅人";
            //column.align = "center"; column.Allowsort = true; column.Width = 80;
            //grid.AddControl(column);

            MiniGridColumn column = new MiniGridColumn();
            column.Field = "ArchivesLifeCycle";
            column.HeaderText = "操作记录";
            column.align = "center"; column.Allowsort = false; column.Width = 100;
            grid.AddControl(column);
            ViewBag.DatagridHtml = grid.Render(fileConfig.IsShowIndex == "True");
            var queryForm = fileConfig.GetQueryForm();
            var table = queryForm.Controls.FirstOrDefault() as Table;
            int height = table.Rows.Count * 30 + 60;
            ViewBag.QueryFormHtml = queryForm.Render();
            QueryKey = space.S_DOC_ListConfig.FirstOrDefault(d => d.RelationID == configID).QueryKeyText;
            if (GetQueryString("$IsSelect") == "true")
                ViewBag.BtnHTML += " <a id='returnSelectList' class='mini-button' onclick='returnSelectList()' iconcls='icon-refer' plain='true'>选择</a>";
            else
            {
                if (!String.IsNullOrEmpty(nodeID))
                    ViewBag.BtnHTML += String.Format(btnTemplate, "添加", "icon-add", "add({width: 800, height: 500})", "btnAdd");
                ViewBag.BtnHTML += String.Format(btnTemplate, "修改", "icon-edit", "edit({width: 800, height: 500})", "btnEdit");
                ViewBag.BtnHTML += String.Format(btnTemplate, "批量修改", "icon-edit", "multiEdit('/DocSystem/Manager/FileManager/Edit')", "btnMultiEdit");
                ViewBag.BtnHTML += String.Format(btnTemplate, "删除", "icon-remove", "del()", "btnDel");
                if (String.IsNullOrEmpty(nodeID))
                    ViewBag.BtnHTML += String.Format(btnTemplate, "打开树形图", "icon-goto", "opentree()", "btnOpenTree");
                if (fileConfig.CanDownload == "True")
                    ViewBag.BtnHTML += String.Format(btnTemplate, "下载", "icon-download", "download()", "btnDownload");
                //if (fileConfig.CanBorrow == "True")
                //    ViewBag.BtnHTML += String.Format(btnTemplate, "归还", "icon-copy", "borrowreturn()", "btnBorrowReturn");

                ViewBag.BtnHTML += String.Format(btnTemplate, "浏览", "icon-zoomin", "browse()", "btnBrowse");
                ViewBag.BtnHTML += String.Format(btnTemplate, "发布", "icon-downgrade", "publish()", "btnPublish");
                ViewBag.BtnHTML += String.Format(btnTemplate, "禁用", "icon-cancel", "forbidden()", "btnForbid");
                ViewBag.BtnHTML += String.Format(btnTemplate, "移动", "icon-goto", "movefile()", "btnMove");
                //ViewBag.BtnHTML += String.Format(btnTemplate, "升版", "icon-upload", "upVersion()", "btnUpVersion");
                //ViewBag.BtnHTML += String.Format(btnTemplate, "查看版本", "icon-search", "viewVersion()", "btnViewVersion");
            }
            if (fileConfig.FileCategory == FileCategory.Atlas.ToString())
                ViewBag.IsAtlas = "True";
            else
                ViewBag.IsAtlas = "False";

            var enumsList = fileConfig.S_DOC_FileAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).Distinct().ToList();
            ViewBag.Enums = enumsList;
            foreach (var item in fileConfig.S_DOC_FileAttr.Where(d => d.IsEnum == "True").ToList())
            {
                if (String.IsNullOrEmpty(item.EnumKey)) continue;
                var enumKey = item.EnumKey;
                if (enumKey.Split('.').Length > 1) enumKey = enumKey.Split('.')[1];
                ViewBag.Script += "addGridEnum(\"dataGrid\", \"" + item.FileAttrField + "\", \"" + enumKey + "\");";
            }
            var ArchiveType = System.Configuration.ConfigurationManager.AppSettings["ArchiveType"];
            ViewBag.ArchiveType = string.IsNullOrEmpty(ArchiveType) ? "PdfFile" : ArchiveType;
            return View();
        }

        public ActionResult Edit()
        {
            string configID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            bool isUpVersion = string.IsNullOrEmpty(this.Request["UpVersion"]) ? false : this.Request["UpVersion"].ToLower() == "true";
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var file = space.S_DOC_File.FirstOrDefault(d => d.ID == configID);
            if (file == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + configID + "】的节点定义对象");
            var queryForm = file.GetEditForm(!(file.FileCategory == FileCategory.Atlas.ToString()), isUpVersion);
            ViewBag.FormHtml = queryForm.Render();
            var enumsList = file.S_DOC_FileAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).ToList();
            ViewBag.Enums = enumsList;
            ViewBag.IsMulti = this.Request["IsMulti"];
            if (file.FileCategory == FileCategory.Atlas.ToString())
                ViewBag.IsAtlas = "var isAtlas = true";
            else
                ViewBag.IsAtlas = "var isAtlas = false";
            var fileShowItems = file.S_DOC_FileAttr.Where(a => a.Visible == "True");
            var dic = new Dictionary<string, string>();
            foreach (var item in fileShowItems)
                dic.SetValue(item.FileAttrField, item.FileAttrName);
            ViewBag.FieldInfo = JsonHelper.ToJson(dic);
            ViewBag.SelectorScipts = renderSelectorScipts(file);
            return View();
        }

        private string renderSelectorScipts(S_DOC_File file)
        {
            var baseEntities = FormulaHelper.GetEntities<Base.Logic.Domain.BaseEntities>();
            StringBuilder sb = new StringBuilder();
            var fields = file.S_DOC_FileAttr.Where(d => d.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0)
                .ToList();
            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field.SelectorKey)) continue;
                string returnParams = "value:ID,text:Name";
                if (!string.IsNullOrEmpty(field.ReturnParams)) returnParams = field.ReturnParams;
                if (field.SelectorKey == "SystemUser")
                {
                    if (field.MultiSelect == "True")
                        sb.AppendFormat("addMultiUserSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.FileAttrField, returnParams, "");
                    else
                        sb.AppendFormat("addSingleUserSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.FileAttrField, returnParams, "");
                }
                else if (field.SelectorKey == "SystemOrg")
                {
                    if (field.MultiSelect == "True")
                        sb.AppendFormat("addMultiOrgSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.FileAttrField, returnParams, "");
                    else
                        sb.AppendFormat("addSingleOrgSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.FileAttrField, returnParams, "");
                }
                else
                {
                    var selector = baseEntities.Set<Base.Logic.Domain.S_UI_Selector>().SingleOrDefault(c => c.Code == field.SelectorKey);
                    if (selector == null) throw new Exception("选择器【" + field.SelectorKey + "】不存在，绘制选择器控件【" + field.FileAttrName + "】失败");
                    var url = selector.URLSingle;
                    if (field.MultiSelect == "True")
                        url = selector.URLMulti;
                    sb.AppendFormat("addSelector('{0}', '{1}', {{ allowResize: true,title:'{2}',width:'{3}',height:'{4}',returnParams:'{5}' }});\n"
                        , field.FileAttrField, url, selector.Title, selector.Width, selector.Height, returnParams);
                }
            }
            return sb.ToString();
        }

        public ActionResult VersionList()
        {
            var ArchiveType = System.Configuration.ConfigurationManager.AppSettings["ArchiveType"];
            ViewBag.ArchiveType = string.IsNullOrEmpty(ArchiveType) ? "PdfFile" : ArchiveType;
            return View();
        }

        public JsonResult GetVersionList(string fileID)
        {
            string sql = "select * from S_Attachment where FileID = '" + fileID + "'";
            var data = this.InstanceDB.ExecuteDataTable(sql);
            return Json(data);
        }

        public JsonResult GetAtlasList(string fileID)
        {
            string sql = "select AtlasFile from S_Attachment where FileID = '" + fileID + "' and CurrentVersion='True'";
            var data = this.InstanceDB.ExecuteDataTable(sql);
            if (data.Rows.Count > 0)
            {
                var result = JsonHelper.ToList(data.Rows[0]["AtlasFile"].ToString());
                return Json(result);
            }
            return Json("");
        }

        public JsonResult getfileconfiginfo()
        {
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            var list = this.Space.S_DOC_File.Where(d => d.AllowDisplay == "True").OrderBy(a => a.SortIndex).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public JsonResult BrownFile(string FileID, string SpaceID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var file = new S_FileInfo(FileID, space);
            if (file.CurrentAttachment == null || String.IsNullOrEmpty(file.CurrentAttachment.DataEntity.GetValue("MainFile")))
            {
                throw new Formula.Exceptions.BusinessException("没有上传附件，无法进行浏览！");
            }
            var fileDB = SQLHelper.CreateSqlHelper(ConnEnum.FileStore);
            var docType = file.CurrentAttachment.DataEntity.GetValue("FileType");
            var fileID = file.CurrentAttachment.DataEntity.GetValue("PDFFile").Split('_')[0];
            if (String.IsNullOrEmpty(fileID))
                fileID = file.CurrentAttachment.DataEntity.GetValue("MainFile").Split('_')[0];
            var constEntities = FormulaHelper.GetEntities<DocConstEntities>();
            var log = constEntities.S_DocumentLog.Create();
            log.ID = FormulaHelper.CreateGuid();
            log.Name = file.Name;
            log.LogType = "Browse";
            log.NodeID = file.NodeID;
            log.SpaceID = file.Space.ID;
            log.FileID = file.ID;
            log.FullNodeID = file.FullNodeID;
            log.ConfigID = file.ConfigInfo.ID;
            log.CreateDate = DateTime.Now;
            log.CreateUserID = this.CurrentUserInfo.UserID;
            log.CreateUserName = this.CurrentUserInfo.UserName;
            constEntities.S_DocumentLog.Add(log);
            constEntities.SaveChanges();
            return Json(new { DocType = docType, SWFFileID = fileID });
        }

        protected override void BeforeSave(S_FileInfo file, Dictionary<string, object> row, bool isNew)
        {
            if (isNew)
            {
                var isPhys = row.GetValue("StorageType") == "Physical";
                var node = S_NodeInfo.GetNode(row.GetValue("NodeID"), row.GetValue("SpaceID"));
                if (isPhys && node.ConfigInfo.IsPhysicalBox != "True")
                    throw new Formula.Exceptions.BusinessException("当前编目节点不能保存实物文件");
                if (!isPhys && node.ConfigInfo.IsElectronicBox != "True")
                    throw new Formula.Exceptions.BusinessException("当前编目节点不能保存电子档文件");
            }
            else
            {
                string detail = string.Empty;
                S_FileInfo oldFile = new S_FileInfo(file.ID, file.Space);
                var fileAttrs = file.ConfigInfo.S_DOC_FileAttr.Select(a => a);
                foreach (S_DOC_FileAttr fileAttr in fileAttrs)
                {
                    if (fileAttr.InputType.Equals("ButtonEdit"))//弹出选择
                        detail += file.DataEntity.GetValue(fileAttr.FileAttrField + "Name") == oldFile.DataEntity.GetValue(fileAttr.FileAttrField + "Name") ? "" : "\"" + fileAttr.FileAttrName + ":" + oldFile.DataEntity.GetValue(fileAttr.FileAttrField + "Name") + "\"修改为\"" + file.DataEntity.GetValue(fileAttr.FileAttrField + "Name") + "\";";
                       
                    else if (fileAttr.InputType.Equals("Combobox"))//枚举
                    {
                        var enums = EnumBaseHelper.GetEnumDef(fileAttr.EnumKey).EnumItem;
                        var enumNameOld = enums.FirstOrDefault(a => a.Code.Equals(oldFile.DataEntity.GetValue(fileAttr.FileAttrField)));
                        string oldName=enumNameOld!=null?enumNameOld.Name:"";
                        var enumName=enums.FirstOrDefault(a => a.Code.Equals(file.DataEntity.GetValue(fileAttr.FileAttrField)));
                        detail += file.DataEntity.GetValue(fileAttr.FileAttrField) == oldFile.DataEntity.GetValue(fileAttr.FileAttrField) ? "" : "\"" + fileAttr.FileAttrName + ":" + oldName + "\"修改为\"" + enumName.Name + "\";";
                    }
                    else
                    {
                         if (fileAttr.FileAttrField.Equals("SortIndex") || fileAttr.FileAttrField.Equals("DocIndexID"))
                            continue;
                         detail += file.DataEntity.GetValue(fileAttr.FileAttrField) == oldFile.DataEntity.GetValue(fileAttr.FileAttrField) ? "" : "\"" + fileAttr.FileAttrName + ":" + oldFile.DataEntity.GetValue(fileAttr.FileAttrField) + "\"修改为\"" + file.DataEntity.GetValue(fileAttr.FileAttrField) + "\";";

                    }
                }
                if (!string.IsNullOrEmpty(detail))
                    //修改，详情说明
                    InventoryFO.CreateNewInventoryLedger(oldFile, detail, InventoryType.Update);
            }
        }

        #region 大图相关
        public ActionResult GraphicList()
        {
            string spaceID = this.Request["SpaceID"];
            var configID = this.Request["ConfigID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space != null)
            {
                var fileConfig = space.S_DOC_File.FirstOrDefault();
                var queryForm = fileConfig.GetQueryForm();
                ViewBag.QueryFormHtml = queryForm.Render();
                SQLHelper sqlhelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
                string sql = "select AttrField as value,AttrName as text from S_DOC_AtlasConfigDetail where FileID='" + configID + "' and Type='List' order by DetailSort";
                var dt = sqlhelper.ExecuteDataTable(sql);

                ViewBag.SortField = JsonHelper.ToJson(dt);
            }
            return View();
        }

        public JsonResult GetGraphic(QueryBuilder qb)
        {
            string sql = @"
select S_FileInfo.*,S_Attachment.AtlasFile
from S_FileInfo {0} 
left join S_Attachment on S_FileInfo.ID=S_Attachment.FileID and CurrentVersion='True'
where S_FileInfo.State='" + DocState.Published.ToString() + "' and (1=1 {1})";

            string sortField = this.Request["SortField"];
            string sortDir = this.Request["SortDir"];
            var pageIndex = String.IsNullOrEmpty(this.Request["pageIndex"]) ? 0 : Convert.ToInt32(this.Request["pageIndex"]);

            string spaceID = this.Request["SpaceID"];
            string fileConfigID = this.Request["ConfigID"];
            var nodeID = this.Request["NodeID"];
            if (!string.IsNullOrEmpty(nodeID))
                sql += " and S_FileInfo.NodeID='" + nodeID + "'";

            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space != null)
            {
                var fileConfig = space.S_DOC_File.FirstOrDefault(a => a.ID == fileConfigID);
                if (fileConfig == null) throw new Formula.Exceptions.BusinessException("没有找到文件对象的定义");
                var querySchema = fileConfig.GetQuerySchema();
                var atlasConfig = fileConfig.S_DOC_AtlasConfigDetail.Where(a => a.Type == "List" && a.Dispaly == "True").ToArray();

                string orderByStr = " order by " + sortField + " " + sortDir;
                string whereStr = "";

                qb.SortField = sortField;
                if (!string.IsNullOrEmpty(sortDir))
                    qb.SortOrder = sortDir;
                qb.PageIndex = pageIndex;
                sql += whereStr + orderByStr;

                var QueryList = string.Empty;
                var joinSql = string.Empty;
                var whereSql = string.Empty;
                if (this.Request["QueryItems"] != null)
                    QueryList = this.Request["QueryItems"].ToString();
                List<Dictionary<string, object>> queryList = JsonHelper.ToList(QueryList);
                if (queryList.Count > 0)
                    qb.Items.Clear();
                foreach (var queryItem in queryList)
                {
                    var ItemName = queryItem.GetValue("ItemName");
                    var Method = queryItem.GetValue("Method");
                    var Value = queryItem.GetValue("Value");
                    var Logic = queryItem.GetValue("Logic");
                    if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Method)
                        || string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Logic)) continue;
                    var configID = System.Text.RegularExpressions.Regex.Replace(ItemName, @"(.*\()(.*)(\).*)", "$2");
                    var tableName = ItemName.Equals(configID) ? "S_FileInfo" : "S_NodeInfo";
                    var itemName = ItemName.Replace("(" + configID + ")", "");
                    switch (Method)
                    {
                        case "LK": whereSql += Logic + " " + tableName + "." + itemName + " like '%" + Value + "%' "; break;
                        case "EQ": whereSql += Logic + " " + tableName + "." + itemName + " ='" + Value + "' "; break;
                        case "GT": whereSql += Logic + " " + tableName + "." + itemName + " >'" + Value + "' "; break;
                        case "LT": whereSql += Logic + " " + tableName + "." + itemName + " <'" + Value + "' "; break;
                        case "FR": whereSql += Logic + " " + tableName + "." + itemName + " >='" + Value + "' "; break;
                        case "TO": whereSql += Logic + " " + tableName + "." + itemName + " <='" + Value + "' "; break;
                        case "UE": whereSql += Logic + " " + tableName + "." + itemName + " <>'" + Value + "' "; break;
                        case "IN": whereSql += Logic + " " + tableName + "." + itemName + " in('" + Value.Replace(",", "','") + "') "; break;
                        default: break;
                    }
                    if (!ItemName.Equals(configID))
                    {
                        joinSql = "left join S_NodeInfo on S_NodeInfo.ID=S_FileInfo.NodeID";
                        whereSql += " AND S_NodeInfo.ConfigID='" + configID + "'";
                    }
                }

                sql = string.Format(sql, joinSql, whereSql);
                var dt = this.InstanceDB.ExecuteDataTable(sql, qb);
                var htmlDta = new List<object>();
                foreach (DataRow row in dt.Rows)
                {
                    string fileID = "";

                    if (row["AtlasFile"] != DBNull.Value)
                    {
                        var atlasFile = JsonHelper.ToList(row["AtlasFile"].ToString());
                        if (atlasFile.Count > 0)
                        {
                            fileID = atlasFile[0].GetValue("PictureFileID");
                        }
                    }
                    var title = "";
                    var subTitle = new List<string>();
                    var listInfos = new List<string>();
                    for (int i = 0; i < atlasConfig.Count(); i++)
                    {
                        var item = atlasConfig[i];
                        var value = "";
                        if (dt.Columns.Contains(item.AttrField) && row[item.AttrField] != DBNull.Value)
                        {
                            if (dt.Columns[item.AttrField].DataType.Name.Contains("DateTime"))
                                value = ((DateTime)row[item.AttrField]).ToString("yyyy年MM月dd日");
                            else
                                value = (row[item.AttrField]).ToString();
                        }
                        listInfos.Add(item.AttrName + "：" + value);
                        if (i == 0)
                            title = value;
                        if (i == 1)
                            subTitle.Add(item.AttrName + "：" + value);
                        if (i == 2)
                            subTitle.Add(item.AttrName + "：" + value);
                    }
                    htmlDta.Add(new
                    {
                        FileID = fileID,
                        Title = title,
                        SubTitle = subTitle,
                        ID = row["ID"].ToString(),
                        ConfigID = row["ConfigID"].ToString(),
                        ListInfos = string.Join("&#10;", listInfos)
                    });
                }
                var dic = new Dictionary<string, object>();
                dic["HtmlData"] = htmlDta;
                dic["PageSize"] = qb.PageSize;
                dic["Total"] = qb.TotolCount;
                dic["CurrentPage"] = qb.PageIndex;
                return Json(dic);
            }
            else
            {
                return Json("");
            }
        }
        #endregion
    }
}
