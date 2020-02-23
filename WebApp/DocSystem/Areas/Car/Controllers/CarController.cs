using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocSystem.Logic;
using Config;
using System.Data;
using DocSystem.Logic.Domain;
using MvcAdapter;
using Formula.DynConditionObject;
using Formula;
using Formula.Helper;
using Config.Logic;
using System.Collections;

namespace DocSystem.Areas.Car.Controllers
{
    public class CarController : DocConstController
    {
        public ActionResult BorrowCar()
        {
            var tab = new Tab();
            var enumServcie = FormulaHelper.GetService<IEnumService>();
            var category = getBorrowDownKey("DocConst.BorrowDownFlowKey", "借阅流程", "FlowKey", "Borrow");
            category.Multi = false;
            category.SetDefaultItem();
            tab.Categories.Add(category);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public ActionResult DownloadCar()
        {
            var tab = new Tab();
            var enumServcie = FormulaHelper.GetService<IEnumService>();
            var category = getBorrowDownKey("DocConst.BorrowDownFlowKey", "下载流程", "FlowKey", "Download");
            category.Multi = false;
            category.SetDefaultItem();
            tab.Categories.Add(category);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        private Category getBorrowDownKey(string enumKey, string name, string queryField, string type, bool hasAllAttr = true)
        {
            var enumServcie = FormulaHelper.GetService<IEnumService>();
            var dataTable = enumServcie.GetEnumTable(enumKey, type);
            var category = new Category();
            category.Name = name;
            category.Key = queryField;
            category.QueryField = queryField;
            if (hasAllAttr)
            {
                var item = new CategroyItem();
                item.Name = "全部";
                item.Value = "All";
                item.SortIndex = 0;
                item.IsDefault = true;
                category.Items.Add(item);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var dataRow = dataTable.Rows[i];
                var item = new CategroyItem();
                item.Name = dataRow["text"].ToString();
                item.Value = dataRow["value"].ToString();
                item.SortIndex = i + 1;
                category.Items.Add(item);
            }
            return category;
        }

        public JsonResult GetCarList(QueryBuilder qb, string Key)
        {
            var dt = GetCarDataTable(qb, Key);
            GridData gd = new GridData(dt);
            gd.total = qb.TotolCount;
            return Json(gd);
        }

        private DataTable GetCarDataTable(QueryBuilder qb, string Key)
        {
            var configEntities = FormulaHelper.GetEntities<DocConfigEntities>();
            var configSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            var configSql = "select ID as ConfigID,BorrowFlowKey as FlowKey from S_DOC_Node union all select ID as ConfigID,BorrowFlowKey as FlowKey from S_DOC_File";
            if (Key == "Download")
                configSql = "select ID as ConfigID,DownloadFlowKey as FlowKey from S_DOC_Node union all select ID as ConfigID,DownloadFlowKey as FlowKey from S_DOC_File";
            var configInfo = configSqlHelper.ExecuteList<FlowKeyCls>(configSql);
            var spaceList = configEntities.Set<S_DOC_Space>().ToList();

            var flowKey = string.Empty;
            foreach (var condition in qb.Items)
            {
                if (condition.Field == "FlowKey")
                    flowKey = condition.Value.ToString();
            }
            qb.Items.RemoveWhere(a => a.Field == "FlowKey");
            var whereStr = string.Empty;
            if (!string.IsNullOrEmpty(flowKey))
            {
                var configIDs = configInfo.Where(a => a.FlowKey == flowKey).Select(a => a.ConfigID);
                whereStr = " and ConfigID in ('" + string.Join("','", configIDs) + "')";
            }

            var sql = "select *,'' as FlowKey,'' as SpaceName from S_CarInfo where State = 'New' and UserID='" + this.CurrentUserInfo.UserID + "' and Type = '" + Key + "'";
            if (!string.IsNullOrEmpty(whereStr))
                sql += whereStr;
            var dt = this.SqlHelper.ExecuteDataTable(sql, qb);
            foreach (DataRow row in dt.Rows)
            {
                var configID = row["ConfigID"].ToString();
                var info = configInfo.FirstOrDefault(a => a.ConfigID == configID);
                if (info != null)
                    row["FlowKey"] = info.FlowKey.ToString();
                var space = spaceList.FirstOrDefault(a => a.ID == row["SpaceID"].ToString());
                if (space != null)
                    row["SpaceName"] = space.Name;
            }
            return dt;
        }

        public JsonResult GetBorrowTree(QueryBuilder qb)
        {
            var dt = GetCarDataTable(qb, "Borrow");

            #region 创建上级节点的集合
            var result = new DataTable();
            result.Columns.Add("SpaceID");
            result.Columns.Add("FileID");
            result.Columns.Add("NodeID");
            result.Columns.Add("ID");
            result.Columns.Add("UID");
            result.Columns.Add("ParentUID");
            result.Columns.Add("Name");
            result.Columns.Add("InsName");
            result.Columns.Add("Code");
            result.Columns.Add("SpaceName");
            result.Columns.Add("DataType");
            result.Columns.Add("FlowKey");
            result.Columns.Add("CreateDate");
            result.Columns.Add("IsSelect");

            result.Columns["CreateDate"].DataType = typeof(DateTime);
            #endregion

            var spaceList = new List<S_DOC_Space>();
            foreach (DataRow item in dt.Rows)
            {
                var space = spaceList.FirstOrDefault(a => a.ID == item["SpaceID"].ToString());
                if (space == null)
                {
                    space = DocConfigHelper.CreateConfigSpaceByID(item["SpaceID"].ToString());
                    spaceList.Add(space);
                }
                var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                var fullID = "";
                if (string.IsNullOrEmpty(item["FileID"].ToString()))
                {
                    var nodeInfo = new S_NodeInfo(item["NodeID"].ToString(), space);
                    fullID = nodeInfo.FullPathID;

                    var newRow = result.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == nodeInfo.ID
                        || a["NodeID"].ToString() == item["ID"].ToString());
                    if (newRow == null)
                    {
                        newRow = result.NewRow();
                        result.Rows.Add(newRow);
                    }

                    newRow["SpaceID"] = item["SpaceID"].ToString();
                    newRow["FileID"] = item["FileID"].ToString();
                    newRow["NodeID"] = item["NodeID"].ToString();
                    newRow["ID"] = item["ID"].ToString();
                    newRow["UID"] = nodeInfo.ID;
                    newRow["ParentUID"] = nodeInfo.ParentID;
                    newRow["Name"] = item["Name"].ToString();
                    newRow["InsName"] = nodeInfo.Name;
                    newRow["Code"] = nodeInfo.DataEntity.GetValue("Code");
                    newRow["SpaceName"] = space.Name;
                    newRow["DataType"] = item["DataType"].ToString();
                    newRow["FlowKey"] = item["FlowKey"].ToString();
                    newRow["CreateDate"] = Convert.ToDateTime(item["CreateDate"]);
                    newRow["IsSelect"] = true;
                }
                else
                {
                    var fileInfo = new S_FileInfo(item["FileID"].ToString(), space);
                    fullID = fileInfo.FullNodeID;

                    var newRow = result.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == fileInfo.ID
                        || a["FileID"].ToString() == item["ID"].ToString());
                    if (newRow == null)
                    {
                        newRow = result.NewRow();
                        result.Rows.Add(newRow);
                    }

                    newRow["SpaceID"] = item["SpaceID"].ToString();
                    newRow["FileID"] = item["FileID"].ToString();
                    newRow["NodeID"] = item["NodeID"].ToString();
                    newRow["ID"] = item["ID"].ToString();
                    newRow["UID"] = fileInfo.ID;
                    newRow["ParentUID"] = fileInfo.NodeID;
                    newRow["Name"] = item["Name"].ToString();
                    newRow["InsName"] = fileInfo.Name;
                    newRow["Code"] = fileInfo.DataEntity.GetValue("Code");
                    newRow["SpaceName"] = space.Name;
                    newRow["DataType"] = item["DataType"].ToString();
                    newRow["FlowKey"] = item["FlowKey"].ToString();
                    newRow["CreateDate"] = Convert.ToDateTime(item["CreateDate"]);
                    newRow["IsSelect"] = true;
                }
                string sql = String.Format("select * from S_NodeInfo where ID in ('{0}') ", fullID.Replace(".", "','"));
                var ancestors = instanceDB.ExecuteDataTable(sql);
                foreach (DataRow ancestor in ancestors.Rows)
                    FillTreeData(result, ancestor, space.Name);
            }
            return Json(result);
        }

        public JsonResult GetDownloadTree(QueryBuilder qb)
        {
            var dt = GetCarDataTable(qb, "Download");

            #region 创建上级节点的集合
            var result = new DataTable();
            result.Columns.Add("SpaceID");
            result.Columns.Add("FileID");
            result.Columns.Add("NodeID");
            result.Columns.Add("ID");
            result.Columns.Add("UID");
            result.Columns.Add("ParentUID");
            result.Columns.Add("Name");
            result.Columns.Add("InsName");
            result.Columns.Add("Code");
            result.Columns.Add("SpaceName");
            result.Columns.Add("DataType");
            result.Columns.Add("FlowKey");
            result.Columns.Add("CreateDate");
            result.Columns.Add("IsSelect");

            result.Columns["CreateDate"].DataType = typeof(DateTime);
            #endregion

            var spaceList = new List<S_DOC_Space>();
            foreach (DataRow item in dt.Rows)
            {
                var space = spaceList.FirstOrDefault(a => a.ID == item["SpaceID"].ToString());
                if (space == null)
                {
                    space = DocConfigHelper.CreateConfigSpaceByID(item["SpaceID"].ToString());
                    spaceList.Add(space);
                }
                var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                var fullID = "";
                if (string.IsNullOrEmpty(item["FileID"].ToString()))
                {
                    var nodeInfo = new S_NodeInfo(item["NodeID"].ToString(), space);
                    fullID = nodeInfo.FullPathID;

                    var newRow = result.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == nodeInfo.ID
                        || a["NodeID"].ToString() == item["ID"].ToString());
                    if (newRow == null)
                    {
                        newRow = result.NewRow();
                        result.Rows.Add(newRow);
                    }

                    newRow["SpaceID"] = item["SpaceID"].ToString();
                    newRow["FileID"] = item["FileID"].ToString();
                    newRow["NodeID"] = item["NodeID"].ToString();
                    newRow["ID"] = item["ID"].ToString();
                    newRow["UID"] = nodeInfo.ID;
                    newRow["ParentUID"] = nodeInfo.ParentID;
                    newRow["Name"] = item["Name"].ToString();
                    newRow["InsName"] = nodeInfo.Name;
                    newRow["Code"] = nodeInfo.DataEntity.GetValue("Code");
                    newRow["SpaceName"] = space.Name;
                    newRow["DataType"] = item["DataType"].ToString();
                    newRow["FlowKey"] = item["FlowKey"].ToString();
                    newRow["CreateDate"] = Convert.ToDateTime(item["CreateDate"]);
                    newRow["IsSelect"] = true;
                }
                else
                {
                    var fileInfo = new S_FileInfo(item["FileID"].ToString(), space);
                    fullID = fileInfo.FullNodeID;

                    var newRow = result.AsEnumerable().FirstOrDefault(a => a["UID"].ToString() == fileInfo.ID
                        || a["FileID"].ToString() == item["ID"].ToString());
                    if (newRow == null)
                    {
                        newRow = result.NewRow();
                        result.Rows.Add(newRow);
                    }

                    newRow["SpaceID"] = item["SpaceID"].ToString();
                    newRow["FileID"] = item["FileID"].ToString();
                    newRow["NodeID"] = item["NodeID"].ToString();
                    newRow["ID"] = item["ID"].ToString();
                    newRow["UID"] = fileInfo.ID;
                    newRow["ParentUID"] = fileInfo.NodeID;
                    newRow["Name"] = item["Name"].ToString();
                    newRow["InsName"] = fileInfo.Name;
                    newRow["Code"] = fileInfo.DataEntity.GetValue("Code");
                    newRow["SpaceName"] = space.Name;
                    newRow["DataType"] = item["DataType"].ToString();
                    newRow["FlowKey"] = item["FlowKey"].ToString();
                    newRow["CreateDate"] = Convert.ToDateTime(item["CreateDate"]);
                    newRow["IsSelect"] = true;
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
                newRow["InsName"] = row["Name"].ToString();
                newRow["Code"] = row["Code"].ToString();
                newRow["SpaceName"] = spaceName;
                newRow["DataType"] = "";
                newRow["FlowKey"] = "";
                newRow["CreateDate"] = DBNull.Value;
                newRow["IsSelect"] = false;

                dt.Rows.Add(newRow);
            }
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            string type = this.Request["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                var carItem = this.entities.Set<S_CarInfo>().Where(d => d.State == "New" && d.UserID == this.CurrentUserInfo.UserID && d.Type == type).WhereToGridData(qb);
                return Json(carItem);
            }
            else
            {
                return Json("");

            }
        }

        public JsonResult ApplyFileDownload(string files, string SpaceID)
        {
            ArrayList dirDownloads = new ArrayList();
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("为找到【ID】为【" + SpaceID + "】的图档实例库");
            var list = JsonHelper.ToList(files);
            var ArchiveType = System.Configuration.ConfigurationManager.AppSettings["ArchiveType"];
            if (string.IsNullOrEmpty(ArchiveType))
                ArchiveType = "PdfFile";
            ArchiveType = ArchiveType.Replace("PdfFile", "PDFFile");
            foreach (var file in list)
            {
                S_FileInfo fileInfo = new S_FileInfo(file.GetValue("ID"), space);
                if (fileInfo.ConfigInfo == null)
                    throw new Formula.Exceptions.BusinessException("未能找到文件【" + fileInfo.ID + "】的配置对象信息");
                if (fileInfo.DataEntity.GetValue("StorageType") != StorageType.Electronic.ToString())
                    throw new Formula.Exceptions.BusinessException("实物文件不能下载！");
                if (fileInfo.CurrentAttachment == null)
                    throw new Formula.Exceptions.BusinessException("【" + fileInfo.Name + "】还没有主文件，不能下载！");
                if (String.IsNullOrEmpty(fileInfo.ConfigInfo.DownloadFlowKey))
                {
                    dirDownloads.Add(fileInfo.CurrentAttachment.DataEntity);
                    var entities = FormulaHelper.GetEntities<DocConstEntities>();
                    var user = FormulaHelper.GetUserInfo();
                    S_DocumentLog log = entities.S_DocumentLog.Create();
                    log.ID = FormulaHelper.CreateGuid();
                    log.LogType = "DownLoad";
                    log.Name = fileInfo.Name;
                    log.NodeID = fileInfo.NodeID;
                    log.SpaceID = fileInfo.Space.ID;
                    log.ConfigID = fileInfo.ConfigInfo.ID;
                    log.CreateDate = DateTime.Now;
                    log.CreateUserID = user.UserID;
                    log.CreateUserName = user.UserName;
                    log.FileID = fileInfo.ID;
                    log.FullNodeID = fileInfo.FullNodeID;
                    entities.S_DocumentLog.Add(log);
                }
                else
                {
                    CarFO.CreateCarItem(fileInfo, ItemType.DownLoad.ToString());
                }
            }
            this.entities.SaveChanges();
            return Json(dirDownloads);
        }

        public JsonResult ApplyNodeBorrow(string nodes, string SpaceID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("为找到【ID】为【" + SpaceID + "】的图档实例库");
            var list = JsonHelper.ToList(nodes);
            foreach (var item in list)
            {
                S_NodeInfo nodeInfo = new S_NodeInfo(item.GetValue("ID"), space);
                if (nodeInfo == null) continue;
                CarFO.CreateCarItem(nodeInfo, ItemType.Borrow.ToString());
            }
            this.entities.SaveChanges();
            return Json(new { });
        }

        public JsonResult ApplyFileBorrow(string files, string SpaceID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("为找到【ID】为【" + SpaceID + "】的图档实例库");
            var list = JsonHelper.ToList(files);
            foreach (var file in list)
            {
                S_FileInfo fileInfo = new S_FileInfo(file.GetValue("ID"), space);
                if (fileInfo == null) continue;
                if (fileInfo.ConfigInfo == null) throw new Formula.Exceptions.BusinessException("未能找到文件【" + fileInfo.ID + "】的配置对象信息");
                if (fileInfo.DataEntity.GetValue("StorageType") != StorageType.Physical.ToString())
                    throw new Formula.Exceptions.BusinessException("电子版文件不能借阅！");
                CarFO.CreateCarItem(fileInfo, ItemType.Borrow.ToString());
            }
            this.entities.SaveChanges();
            return Json(new { });
        }

        public JsonResult gettreelist()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("FlowDefineKey");
            dataTable.Columns.Add("ParentID");

            fillTreeData(dataTable, "Apply", "我的借阅下载车", "", "Root");
            var list = this.entities.Set<S_CarInfo>().Where(d => d.CreateUserID ==
                this.CurrentUserInfo.UserID && d.State == "New" && d.Type == "Borrow").ToList();
            fillTreeData(dataTable, "Borrow", "借阅审批(" + list.Count + ")", "Apply", "Borrow");
            var downloadlist = this.entities.Set<S_CarInfo>().Where(d => d.CreateUserID ==
                this.CurrentUserInfo.UserID && d.State == "New" && d.Type == "DownLoad").ToList();
            fillTreeData(dataTable, "Download", "下载审批(" + downloadlist.Count + ")", "Apply", "Download");
            return Json(dataTable);

        }

        private void fillTreeData(DataTable dt, string ID, string Name, string ParentID, string Type)
        {
            var row = dt.NewRow();
            row["ID"] = ID;
            row["Name"] = Name;
            row["ParentID"] = ParentID;
            row["Type"] = Type;
            // row["FlowDefineKey"] = FlowDefineKey;
            dt.Rows.Add(row);
        }

        public JsonResult Delete()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.Split(','), QueryMethod.In);
            var list = entities.Set<S_CarInfo>().Where(res.GetExpression<S_CarInfo>()).ToList();

            foreach (var item in list)
                entities.Set<S_CarInfo>().Remove(item);
            entities.SaveChanges();

            return Json("");
        }
    }

    public class FlowKeyCls
    {
        public string ConfigID { get; set; }
        public string FlowKey { get; set; }
    }
}
