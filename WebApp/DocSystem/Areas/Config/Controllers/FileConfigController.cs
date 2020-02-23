using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using MvcAdapter;
using DocSystem.Logic;
using DocSystem.Logic.Domain;
using Config;
using Formula.DynConditionObject;
using Config.Logic;
using Base.Logic.Domain;

namespace DocSystem.Areas.Config.Controllers
{
    public class FileConfigController : DocConfigController<S_DOC_File>
    {
        public ActionResult FileList()
        {
            var baseEntities = FormulaHelper.GetEntities<BaseEntities>();
            var list = baseEntities.Set<S_UI_Selector>().Select(c => new { value = c.Code, text = c.Name }).ToList();
            list.Insert(0, new { value = "SystemOrg", text = "选择部门" });
            list.Insert(0, new { value = "SystemUser", text = "选择用户" });
            ViewBag.SelectorEnum = JsonHelper.ToJson(list);
            return View();
        }

        public JsonResult FileConfigGetList(QueryBuilder qb)
        {
            string SpaceID = this.Request["SpaceID"];
            var list = this.entities.Set<S_DOC_File>().Where(d => d.SpaceID == SpaceID).WhereToGridData(qb);
            return Json(list);
        }

        public JsonResult FileAttrConfigList(QueryBuilder qb)
        {
            qb.SetSort("AttrSort", "asc");
            string FileID = this.Request["FileID"];
            var sql = "select *,'true' as IsEdit from S_DOC_FileAttr where FileID='" + FileID + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql, qb);
            foreach (DataRow row in dt.Rows)
            {
                var defaultAttr = S_DOC_Space.DefaultFileAttrArray.FirstOrDefault(a => a.FileAttrField == row["FileAttrField"].ToString());
                if (defaultAttr != null)
                    row["IsEdit"] = defaultAttr.IsEdit.ToString();
            }

            GridData gridData = new GridData(dt);
            gridData.total = qb.TotolCount;
            return Json(gridData);
        }
        public JsonResult FileQueryConfigList(QueryBuilder qb)
        {
            qb.SetSort("QuerySort", "asc");
            string FileID = this.Request["FileID"];
            var File = this.GetEntityByID<S_DOC_File>(FileID);
            string listconfig = "";
            if (File != null)
            {
                listconfig = File.ListConfig().ID;
            }
            var list = this.entities.Set<S_DOC_QueryParam>().Where(d => d.ListConfigID == listconfig).WhereToGridData(qb);
            return Json(list);
        }

        public JsonResult FileListConfigDetail(QueryBuilder qb)
        {
            qb.SetSort("DetailSort", "asc");
            string FileID = this.Request["FileID"];
            var File = this.GetEntityByID<S_DOC_File>(FileID);
            string listconfig = "";
            if (File != null)
            {
                listconfig = File.ListConfig().ID;
            }
            var list = this.entities.Set<S_DOC_ListConfigDetail>().Where(d => d.ListConfigID == listconfig).WhereToGridData(qb);
            return Json(list);
        }

        public JsonResult FileAtlasListDetail(QueryBuilder qb)
        {
            qb.SetSort("DetailSort", "asc");
            string FileID = this.Request["FileID"];
            var list = this.entities.Set<S_DOC_AtlasConfigDetail>().Where(d => d.FileID == FileID && d.Type == "List").WhereToGridData(qb);
            return Json(list);
        }

        public JsonResult FileAtlasBlockDetail(QueryBuilder qb)
        {
            qb.SetSort("DetailSort", "asc");
            string FileID = this.Request["FileID"];
            var list = this.entities.Set<S_DOC_AtlasConfigDetail>().Where(d => d.FileID == FileID && d.Type == "Block").WhereToGridData(qb);
            return Json(list);
        }

        public JsonResult DeleteNodeAttrGrid()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.TrimEnd(',').Split(','), QueryMethod.In);
            var list = entities.Set<S_DOC_FileAttr>().Where(res.GetExpression<S_DOC_FileAttr>()).ToList();
            foreach (var item in list)
                entities.Set<S_DOC_FileAttr>().Remove(item);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult deletelistConfig()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.TrimEnd(',').Split(','), QueryMethod.In);
            var list = entities.Set<S_DOC_ListConfigDetail>().Where(res.GetExpression<S_DOC_ListConfigDetail>()).ToList();
            foreach (var item in list)
                entities.Set<S_DOC_ListConfigDetail>().Remove(item);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult DeletelistQueryConfigGrid()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.TrimEnd(',').Split(','), QueryMethod.In);
            var list = entities.Set<S_DOC_QueryParam>().Where(res.GetExpression<S_DOC_QueryParam>()).ToList();
            foreach (var item in list)
                entities.Set<S_DOC_QueryParam>().Remove(item);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult DeleteAtlasConfigDetail()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.TrimEnd(',').Split(','), QueryMethod.In);
            var list = entities.Set<S_DOC_AtlasConfigDetail>().Where(res.GetExpression<S_DOC_AtlasConfigDetail>()).ToList();
            foreach (var item in list)
                entities.Set<S_DOC_AtlasConfigDetail>().Remove(item);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult SaveNodeAttrGrid(string fileID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var File = this.GetEntityByID<S_DOC_File>(fileID);
            if (File == null)
                throw new Formula.Exceptions.BusinessException("未找到ID 未【" + fileID + "】的节点定义信息");
            File.SaveNodeAttr(list);
            this.entities.SaveChanges();
            return Json("");
        }

        public JsonResult SaveListConfig(string fileID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var file = this.GetEntityByID<S_DOC_File>(fileID);
            if (file == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + fileID + "】的节点定义对象");
            file.SaveListConfigDetail(list);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult SaveListQueryConfig(string fileID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var file = this.GetEntityByID<S_DOC_File>(fileID);
            if (file == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + fileID + "】的节点定义对象");
            file.SaveQueryParam(list);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult SaveAtlasConfigDetail(string fileID, string ListData, string type)
        {
            var list = JsonHelper.ToList(ListData);
            var file = this.GetEntityByID<S_DOC_File>(fileID);
            if (file == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + fileID + "】的节点定义对象");
            file.SaveAtlasConfig(list, type);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult addListConfig(string FileID, string ListData, string GridID)
        {
            var list = JsonHelper.ToList(ListData);
            var File = this.GetEntityByID<S_DOC_File>(FileID);
            if (File == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + FileID + "】的节点定义对象");
            if (GridID == "listConfigGrid")
            {
                foreach (var item in list)
                {
                    string id = item.GetValue("ID");
                    var attr = this.GetEntityByID<S_DOC_FileAttr>(id);
                    File.AddListConfigDetail(attr);
                }
            }
            else if (GridID == "queryConfigGrid")
            {
                foreach (var item in list)
                {
                    string id = item.GetValue("ID");
                    var attr = this.GetEntityByID<S_DOC_FileAttr>(id);
                    File.AddQueryParam(attr);
                }
            }
            else if (GridID == "atlasListGrid" || GridID == "atlasBlockGrid")
            {
                foreach (var item in list)
                {
                    string id = item.GetValue("ID");
                    var attr = this.GetEntityByID<S_DOC_FileAttr>(id);
                    if (GridID == "atlasListGrid")
                        File.AddAtlasConfig(attr, "List");
                    else
                        File.AddAtlasConfig(attr, "Block");
                }
            }
            entities.SaveChanges();
            return Json("");

        }
        protected override void BeforeSave(S_DOC_File entity, bool isNew)
        {
            string json = Request.Form["FormData"];
            var formDic = JsonHelper.ToObject<Dictionary<string, object>>(json);
            var extDic = new Dictionary<string, object>();
            foreach (var item in formDic)
            {
                if (item.Key.StartsWith("Ext_"))
                    extDic.Add(item.Key, item.Value);
                if (item.Key == "StorageType")
                {
                    if (item.Value.ToString().Contains("Elec"))
                        entity.IsElectronic = TrueOrFalse.True.ToString();
                    else
                        entity.IsElectronic = TrueOrFalse.False.ToString();
                    if (item.Value.ToString().Contains("Phyc"))
                        entity.IsPhysical = TrueOrFalse.True.ToString();
                    else
                        entity.IsPhysical = TrueOrFalse.False.ToString();
                }
            }
            entity.ExtentionJson = JsonHelper.ToJson<Dictionary<string, object>>(extDic);
            if (isNew)
                entity.Add();
            else
            {
                entity.Save();

            }
        }
        protected override void AfterSave(S_DOC_File entity, bool isNew)
        {
            var listConfig = entity.ListConfig();
            if (listConfig != null)
            {
                string FormData = this.Request["FormData"];
                var json = JsonHelper.ToObject(FormData);
                listConfig.QueryKeyText = json["QueryKeyText"].ToString();
            }
        }
        protected override void BeforeDelete(List<S_DOC_File> entityList)
        {
            foreach (var item in entityList)
                item.Delete();
        }
        public void moveup(string ID, string gridID)
        {
            if (gridID == "nodeAttrGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_FileAttr>(ID);
                nodeAttr.MoveUp();
            }
            else if (gridID == "listConfigGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_ListConfigDetail>(ID);
                nodeAttr.MoveUp();
            }
            else if (gridID == "queryConfigGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_QueryParam>(ID);
                nodeAttr.MoveUp();
            }
            else if (gridID == "relationGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_FileAttr>(ID);
                nodeAttr.MoveUp();
            }
            else if (gridID == "atlasListGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_AtlasConfigDetail>(ID);
                nodeAttr.MoveUp("List");
            }
            else if (gridID == "atlasBlockGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_AtlasConfigDetail>(ID);
                nodeAttr.MoveUp("Block");
            }
            this.entities.SaveChanges();
        }

        public void movedown(string ID, string gridID)
        {
            if (gridID == "nodeAttrGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_FileAttr>(ID);
                nodeAttr.MoveDown();
            }
            else if (gridID == "listConfigGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_ListConfigDetail>(ID);
                nodeAttr.MoveDown();
            }
            else if (gridID == "queryConfigGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_QueryParam>(ID);
                nodeAttr.MoveDown();
            }
            else if (gridID == "atlasListGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_AtlasConfigDetail>(ID);
                nodeAttr.MoveDown("List");
            }
            else if (gridID == "atlasBlockGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_AtlasConfigDetail>(ID);
                nodeAttr.MoveDown("Block");
            }

            this.entities.SaveChanges();
        }

        protected override void AfterGetMode(Dictionary<string, object> dic, S_DOC_File entity, bool isNew)
        {
            if (!isNew)
            {
                var listConfig = entity.ListConfig();
                dic["QueryKeyText"] = listConfig.QueryKeyText;
                var StorageType = "";
                if (dic.GetValue("IsPhysical") == "True")
                    StorageType += "Phyc";
                if (dic.GetValue("IsElectronic") == "True")
                    StorageType += "Elec";
                dic.SetValue("StorageType", StorageType);
            }
            else
            {
                dic["QueryKeyText"] = "请输入编号或名称";
                dic["CanBorrow"] = TrueOrFalse.False.ToString();
                dic["CanDownload"] = TrueOrFalse.False.ToString();
                dic["AllowDisplay"] = TrueOrFalse.True.ToString();
                dic["AllowAdvancedQuery"] = TrueOrFalse.True.ToString();
                dic["IsShowIndex"] = TrueOrFalse.False.ToString();
            }
        }
    }
}
