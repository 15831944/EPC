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
using System.IO;
using System.Net;

namespace DocSystem.Areas.View.Controllers
{
    public class FileViewController : FileController
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
            var grid = fileConfig.GetDataGrid(true) as MiniDataGrid;
            grid.Url = "GetList"; grid.Multiselect = true;

            ViewBag.DatagridHtml = grid.Render(fileConfig.IsShowIndex == "True");
            var queryForm = fileConfig.GetQueryForm();
            ViewBag.QueryFormHtml = queryForm.Render();
            var table = queryForm.Controls.FirstOrDefault() as Table;
            int height = table.Rows.Count * 30 + 60;

            ViewBag.QueryFormHtml = queryForm.Render();
            QueryKey = space.S_DOC_ListConfig.FirstOrDefault(d => d.RelationID == configID).QueryKeyText;
            if (String.IsNullOrEmpty(nodeID))
                ViewBag.BtnHTML += String.Format(btnTemplate, "打开树形图", "icon-goto", "opentree()", "btnOpenTree");
            if (fileConfig.CanBorrow == "True")
            {
                ViewBag.BtnHTML += String.Format(btnTemplate, "借阅", "icon-copy", "borrow()", "btnBorrow");
            }

            if (fileConfig.FileCategory == FileCategory.Atlas.ToString())
                ViewBag.IsAtlas = "True";
            else
                ViewBag.IsAtlas = "False";

            if (fileConfig.CanDownload == "True")
                ViewBag.BtnHTML += String.Format(btnTemplate, "下载", "icon-download", "download()", "btnDownload");
            ViewBag.BtnHTML += String.Format(btnTemplate, "浏览", "icon-zoomin", "browse()", "btnBrowse");

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
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var file = space.S_DOC_File.FirstOrDefault(d => d.ID == configID);
            if (file == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + configID + "】的节点定义对象");
            var queryForm = file.GetEditForm(true);
            ViewBag.FormHtml = queryForm.Render();
            var enumsList = file.S_DOC_FileAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).ToList();
            ViewBag.Enums = enumsList;
            return View();
        }

        public ActionResult QueryList()
        {
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space != null)
            {
                ViewBag.CarHTML = DocInstanceHepler.GetBtnHTML("True", "True");
                var fileConfig = space.S_DOC_File.FirstOrDefault();
                var queryForm = fileConfig.GetQueryForm();
                ViewBag.QueryFormHtml = queryForm.Render();
                SQLHelper sqlhelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
                string sql = "select distinct AttrField as value,AttrName as text from  S_DOC_ListConfig a inner join S_DOC_File b on a.RelationID=b.ID inner join S_DOC_ListConfigDetail c on c.ListConfigID=a.ID order by AttrName";
                var dt = sqlhelper.ExecuteDataTable(sql);

                ViewBag.SortField = JsonHelper.ToJson(dt);
            }
            var ArchiveType = System.Configuration.ConfigurationManager.AppSettings["ArchiveType"];
            var archiveTypeEnum = new List<object>();
            archiveTypeEnum.Add(new { value = "MainFile", text = "主文件" });
            if (string.IsNullOrEmpty(ArchiveType))
                ArchiveType = "PdfFile";
            ArchiveType = ArchiveType.Replace("PdfFile", "PDFFile");
            foreach (var item in ArchiveType.Split(','))
                archiveTypeEnum.Add(new { value = item, text = item.Replace("File", "") });
            ViewBag.ArchiveType = JsonHelper.ToJson(archiveTypeEnum);
            return View();
        }

        #region 大图HTML模板
        string divTempalte = "<DIV class=\"Item\">"
+ "<div class=\"item-box\">"
+ "       <DIV class=\"pic\"><IMG style=\"HEIGHT: 200px; WIDTH: 100%\" alt=\"\" src=\"{0}\" onerror=\"this.src='/DocSystem/Scripts/Images/NoPic.jpg'\"> </DIV>"
+ "      <DIV class=\"summary\"><A href=\"javascript:ViewInfo('{3}','{4}')\">{1}<A/></DIV>"
+ "     <DIV class=\"row\">{2}</DIV>"
+ "    <DIV class=\"col\">"
+ "       <A  class=\"button\" style=\"WIDTH: 65px\" href=\"javascript:openTreeView('{6}','{5}')\" name=\"btnOpenTreeView\"><SPAN class=\"mini-button-text \">查看项目</SPAN></A> "
+ "        <A  class=\"button\" {7} style=\"WIDTH: 40px\" href=\"javascript:Browse('{3}','{5}')\" name=\"btnBrowse\"><SPAN class=\"mini-button-text \">浏览</SPAN></A> "
+ "       <A  class=\"button\" {7} style=\"WIDTH: 40px\" href=\"javascript:Download('{3}')\" name=\"btnDownload\"><SPAN class=\"mini-button-text \">下载</SPAN></A> "
+ "       <A  class=\"button\" {8} style=\"WIDTH: 40px\" href=\"javascript:Borrow('{3}')\" name=\"btnBorrow\"><SPAN class=\"mini-button-text \">借阅</SPAN></A> "
+ " </DIV>	"
+ "</div>"
+ "</DIV>";
        #endregion

        public JsonResult getfileconfiginfo()
        {
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            var list = this.Space.S_DOC_File.Where(d => d.AllowDisplay == "True").ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getgraphic(QueryBuilder qb)
        {
            string sql = @"
select S_FileInfo.*,S_Attachment.ThumbNail,S_Attachment.SWFFile,S_Attachment.MainFile,S_Attachment.PDFFile,
S_Attachment.Attachments{0} 
from S_FileInfo {1} 
left join S_Attachment on S_FileInfo.ID=S_Attachment.FileID and CurrentVersion='True'
where S_FileInfo.State='" + DocState.Published.ToString() + "' and (1=1 {2})";

            string sortField = this.Request["SortField"];
            string sortDir = this.Request["SortDir"];
            var pageIndex = String.IsNullOrEmpty(this.Request["pageIndex"]) ? 0 : Convert.ToInt32(this.Request["pageIndex"]);
            var queryData = this.Request["querydata"];

            string filterFileType = this.Request["FilterFileType"];
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space != null)
            {
                var fileConfig = space.S_DOC_File.FirstOrDefault();
                if (fileConfig == null) throw new Formula.Exceptions.BusinessException("没有找到文件对象的定义");
                var querySchema = fileConfig.GetQuerySchema();

                string orderByStr = " order by " + sortField + " " + sortDir;
                string whereStr = "";

                qb.SortField = sortField;
                if (!string.IsNullOrEmpty(sortDir))
                    qb.SortOrder = sortDir;
                qb.PageIndex = pageIndex;
                if (!String.IsNullOrEmpty(filterFileType))
                {
                    var fileTypeSqls = new List<string>();
                    foreach (var item in filterFileType.Split(','))
                    {
                        fileTypeSqls.Add(string.Format("({0} is not null and {0} <> '')", item));
                    }
                    whereStr += " and (" + string.Join("or", fileTypeSqls) + ")";
                }
                sql += whereStr + orderByStr;

                #region 根据高级查询拼结果sql

                //var QueryList = string.Empty;
                //if (this.Request["AdvanceQueryList"] != null)
                //    QueryList = this.Request["AdvanceQueryList"].ToString();
                //List<Dictionary<string, object>> queryList = JsonHelper.ToList(QueryList);
                //string resultFileds = string.Empty;
                ////查询条件作为查询对象属性
                //string typeSql = string.Empty;
                //var types = queryList.Select(a => a.GetValue("TypeValue")).Distinct().ToList();//编目节点
                //string relateField = "FullNodeID";
                //foreach (var type in types)
                //{
                //    var _queryList = queryList.Where(a => a.GetValue("TypeValue") == type).ToList();
                //    var name = _queryList.FirstOrDefault().GetValue("Type");

                //    string _sql = " left join (select ID{2} from S_NodeInfo where ConfigID='{0}' and State='{4}' ) {1} on charindex({1}.ID,S_FileInfo.{3},1)>0 ";
                //    string attrField = string.Empty;
                //    foreach (var queryItem in _queryList)
                //    {
                //        var value = queryItem.GetValue("Value");
                //        var af = queryItem.GetValue("AttrField");
                //        var group = queryItem.GetValue("Group");
                //        if (string.IsNullOrEmpty(value)) continue;
                //        string fieldStr = af;
                //        var configType = queryItem.GetValue("AttrType");
                //        if (configType != ListConfigType.File.ToString())
                //        {
                //            fieldStr = name + "_" + af;
                //            attrField += "," + af + " as " + fieldStr; //属性名
                //            //结果列表字段
                //            resultFileds += "," + fieldStr;  //属性名
                //        }
                //    }
                //    if (!string.IsNullOrEmpty(attrField))
                //    {
                //        _sql = string.Format(_sql, type, name, attrField, relateField, DocState.Published.ToString());
                //        //查询条件 是 查询结果自身的属性
                //        typeSql += _sql;
                //    }
                //}

                //DocInstance.Logic.DocInstanceHepler.QueryBuilderExtend(qb, "", queryList);
                #endregion

                var QueryList = string.Empty;
                var resultFileds = string.Empty;
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

                sql = string.Format(sql, resultFileds, joinSql, whereSql);
                var dt = this.InstanceDB.ExecuteDataTable(sql, qb);
                StringBuilder html = new StringBuilder();
                foreach (DataRow row in dt.Rows)
                {
                    string shotSnap = "";
                    string fileID = "";
                    if (row["PDFFile"] != null && row["PDFFile"] != DBNull.Value && !String.IsNullOrEmpty(row["PDFFile"].ToString()))
                    {
                        fileID = row["PDFFile"].ToString().Split('_')[0];
                    }
                    else if (row["MainFile"] != null && row["MainFile"] != DBNull.Value && !String.IsNullOrEmpty(row["MainFile"].ToString()))
                    {
                        fileID = row["MainFile"].ToString().Split('_')[0]; ;
                    }
                    if (!String.IsNullOrEmpty(fileID))
                    {
                        shotSnap = "/MvcConfig/ViewFile/GetSnap?FileID=" + fileID;
                    }
                    else
                    {
                        shotSnap = "/DocSystem/Scripts/Images/NoPic.jpg";
                    }
                    string btnDisableBrowseAndDownload = ""; string btnDisabledBorrow = "";
                    var config = space.S_DOC_File.SingleOrDefault(d => d.ID == row["ConfigID"].ToString());
                    if (config.CanBorrow != TrueOrFalse.True.ToString())
                        btnDisabledBorrow = " disabled ";
                    if (config.CanDownload != TrueOrFalse.True.ToString())
                        btnDisableBrowseAndDownload = " disabled ";
                    html.AppendFormat(divTempalte, shotSnap, row["Name"].ToString(), row["Code"].ToString(), row["ID"].ToString(),
                        row["ConfigID"].ToString(), row["SpaceID"].ToString(), row["NodeID"].ToString(), btnDisableBrowseAndDownload, btnDisabledBorrow);
                }
                Dictionary<object, object> dic = new Dictionary<object, object>();
                dic["html"] = html.ToString();
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
            var fileID = file.CurrentAttachment.DataEntity.GetValue("MainFile").Split('_')[0];
            if (String.IsNullOrEmpty(fileID))
                fileID = file.CurrentAttachment.DataEntity.GetValue("PDFFile").Split('_')[0];
            var constEntities = Formula.FormulaHelper.GetEntities<DocConstEntities>();
            var log = constEntities.S_DocumentLog.Create();
            log.ID = Formula.FormulaHelper.CreateGuid();
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
            InventoryFO.CreateNewInventoryLedger(file, "", InventoryType.View);
            return Json(new { DocType = docType, SWFFileID = fileID });
        }

        #region 高级检索

        public ActionResult AdvanceQueryList()
        {
            ViewBag.Lists = GetQueryString("List");
            //购物车数量
            //var carCount = constSqlHelper.ExecuteScalar(string.Format("select count(1) from S_CarInfo where CreateUserId='{0}' and State='New'", this.CurrentUserInfo.UserID));
            ViewBag.CarCount = CarFO.GetCarCount().ToString();
            return View();
        }

        public JsonResult GetAdvanceQueryList(string SpaceID, string QueryItems)
        {
            string nodeTmp = "inner join S_NodeInfo with(nolock) on charindex(S_NodeInfo.FullPathID,S_FileInfo.FullNodeID)>0 and S_NodeInfo.State='Published' ";
            string sqltmp = @"select S_FileInfo.ID,S_FileInfo.ConfigID,S_FileInfo.SpaceID,S_FileInfo.Name,S_FileInfo.NodeID,
S_Attachment.FileType,S_FileInfo.FullNodeName,StorageType from S_FileInfo  with(nolock) 
left join S_Attachment with(nolock) on FileID=S_FileInfo.ID and S_FileInfo.State='Published' 
{0}
where (CurrentVersion='True' or CurrentVersion is NULL)";
            var sql = string.Empty;
            #region 高级查询
            List<Dictionary<string, object>> queryList = JsonHelper.ToList(QueryItems);
            var sqlList = new List<string>();
            sqlList.Add(sqltmp);
            for (int i = 0; i < queryList.Count; i++)
            {
                var _sql = sqlList.Last();
                var _whereStr = string.Empty;
                var queryItem = queryList[i];
                var ItemName = queryItem.GetValue("ItemName");
                var Method = queryItem.GetValue("Method");
                var Value = queryItem.GetValue("Value").Replace("'", "''");
                var Logic = "AND";
                if (i != 0)
                    Logic = queryList[i - 1].GetValue("Logic");
                if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Method)
                    || string.IsNullOrEmpty(Value)) continue;
                var configID = System.Text.RegularExpressions.Regex.Replace(ItemName, @"(.*\()(.*)(\).*)", "$2");
                //tableName：S_FileInfo,S_NodeInfo;
                var tableName = System.Text.RegularExpressions.Regex.Replace(ItemName, @"(.*\()(.*\))(.*)", "$3");
                var itemName = ItemName.Replace("(" + configID + ")" + tableName, "");
                switch (Method)
                {
                    case "LK": _whereStr = tableName + "." + itemName + " like '%" + Value + "%' "; break;
                    case "EQ": _whereStr = tableName + "." + itemName + " ='" + Value + "' "; break;
                    case "GT": _whereStr = tableName + "." + itemName + " >'" + Value + "' "; break;
                    case "LT": _whereStr = tableName + "." + itemName + " <'" + Value + "' "; break;
                    case "FR": _whereStr = tableName + "." + itemName + " >='" + Value + "' "; break;
                    case "TO": _whereStr = tableName + "." + itemName + " <='" + Value + "' "; break;
                    case "UE": _whereStr = tableName + "." + itemName + " <>'" + Value + "' "; break;
                    case "IN": _whereStr = tableName + "." + itemName + " in('" + Value.Replace(",", "','") + "') "; break;
                    default: break;
                }
                _whereStr = " AND " + _whereStr + " AND " + tableName + ".ConfigID='" + configID + "'";
                if (Logic.ToLower() == "and")
                {
                    if (tableName.ToLower() == "s_nodeinfo" && _sql.ToLower().IndexOf("s_nodeinfo") < 0)
                    {
                        //查询节点时，才内联节点
                        _sql = string.Format(_sql, nodeTmp);
                    }
                    _sql += _whereStr;
                    //and逻辑更新最后一个sql
                    sqlList.Remove(sqlList.Last());
                    sqlList.Add(_sql);
                }
                else
                {
                    //更新最后一个sql
                    if (_sql.ToLower().IndexOf("s_nodeinfo") < 0)
                    {
                        _sql = string.Format(_sql, string.Empty);
                        sqlList.Remove(sqlList.Last());
                        sqlList.Add(_sql);
                    }

                    //or逻辑新增一个sql，最后union all 数组
                    _sql = sqltmp + _whereStr;
                    if (tableName.ToLower() == "s_nodeinfo")
                        _sql = string.Format(_sql, nodeTmp);
                    sqlList.Add(_sql);
                }
            }
            var last_sql = sqlList.Last();
            if (last_sql.ToLower().IndexOf("s_nodeinfo") < 0)
            {
                last_sql = string.Format(last_sql, string.Empty);
                sqlList.Remove(sqlList.Last());
                sqlList.Add(last_sql);
            }

            #endregion
            //var totalSql = "select count(distinct ID) from (" + string.Format(sql, totalFields) + ") tmp ";
            sql = string.Join(@"
union all 
", sqlList);
            sql = "select distinct top 1000 * from (" + sql + ") tmp order by ID desc ";
            var querySpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var sqlHelper = SQLHelper.CreateSqlHelper(querySpace.SpaceKey, querySpace.ConnectString);
            //var data = SQLHelperExtend.ExecuteGridData(sqlHelper, sql, qb);

            var data = new Dictionary<string, object>();
            DataTable dt = sqlHelper.ExecuteDataTable(sql);
            data.SetValue("data", dt);
            data.SetValue("total", dt.Rows.Count);
            return Json(data);

        }
        
        #endregion


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

        public JsonResult GetGraphicList(QueryBuilder qb)
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
