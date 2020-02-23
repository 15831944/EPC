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
    public class NodeConfigController : DocConfigController<S_DOC_Node>
    {
        public ActionResult NodeList()
        {
            var baseEntities = FormulaHelper.GetEntities<BaseEntities>();
            var list = baseEntities.Set<S_UI_Selector>().Select(c => new { value = c.Code, text = c.Name }).ToList();
            list.Insert(0, new { value = "SystemOrg", text = "选择部门" });
            list.Insert(0, new { value = "SystemUser", text = "选择用户" });
            ViewBag.SelectorEnum = JsonHelper.ToJson(list);
            return View();
        }

        public JsonResult NodeConfigGetList(QueryBuilder qb)
        {
            string SpaceID = this.Request["SpaceID"];
            var list = this.entities.Set<S_DOC_Node>().Where(d => d.SpaceID == SpaceID).WhereToGridData(qb);
            return Json(list);
        }
        public JsonResult NodeAttrConfigGetList(QueryBuilder qb)
        {
            qb.SetSort("AttrSort", "asc");
            string NodeID = this.Request["NodeID"];
            var sql = "select *,'true' as IsEdit from S_DOC_NodeAttr where NodeID='" + NodeID + "'";
            var dt = this.SqlHelper.ExecuteDataTable(sql, qb);
            foreach (DataRow row in dt.Rows)
            {
                var defaultAttr = S_DOC_Space.DefaultNodeAttrArray.FirstOrDefault(a => a.AttrField == row["AttrField"].ToString());
                if (defaultAttr != null)
                    row["IsEdit"] = defaultAttr.IsEdit.ToString();
            }

            GridData gridData = new GridData(dt);
            gridData.total = qb.TotolCount;
            return Json(gridData);
        }

        public JsonResult SaveNodeList(string NodeID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            if (node == null)
                throw new Formula.Exceptions.BusinessException("未找到ID 未【" + NodeID + "】的节点定义信息");
            node.SaveNodeAttr(list);
            this.entities.SaveChanges();
            return Json("");
        }
        public JsonResult DeleteNodeList()
        {
            string listIDs = Request["ListIDs"];
            Specifications res = new Specifications();
            res.AndAlso("ID", listIDs.Split(','), QueryMethod.In);
            var list = entities.Set<S_DOC_NodeAttr>().Where(res.GetExpression<S_DOC_NodeAttr>()).ToList();
            foreach (var item in list)
                entities.Set<S_DOC_NodeAttr>().Remove(item);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult DeletelistConfig()
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

        public JsonResult addListConfig(string NodeID, string ListData, string GridID)
        {
            var list = JsonHelper.ToList(ListData);
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            if (node == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + NodeID + "】的节点定义对象");
            if (GridID == "listConfigGrid")
            {
                foreach (var item in list)
                {
                    string id = item.GetValue("ID");
                    var attr = this.GetEntityByID<S_DOC_NodeAttr>(id);
                    node.AddListConfigDetail(attr);

                }
            }
            else if (GridID == "queryConfigGrid")
            {
                foreach (var item in list)
                {
                    string id = item.GetValue("ID");
                    var attr = this.GetEntityByID<S_DOC_NodeAttr>(id);
                    node.AddQueryParam(attr);

                }
            }
            entities.SaveChanges();
            return Json("");

        }
        public JsonResult SaveListConfig(string NodeID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            if (node == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + NodeID + "】的节点定义对象");
            node.SaveListConfigDetail(list);
            entities.SaveChanges();
            return Json("");
        }
        public JsonResult SaveListQueryConfig(string NodeID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            if (node == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + NodeID + "】的节点定义对象");
            node.SaveQueryParam(list);
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult DeleteNodeConfigList()
        {
            string ID = Request["ID"];
            var item = this.GetEntityByID<S_DOC_Node>(ID);
            entities.Set<S_DOC_Node>().Remove(item);
            entities.SaveChanges();
            return Json("");
        }
        public JsonResult ListConfigDetailGetList(QueryBuilder qb)
        {
            qb.SetSort("detailsort", "asc");
            string NodeID = this.Request["NodeID"];
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            string listconfig = "";
            if (node != null)
            {
                listconfig = node.ListConfig().ID;
            }
            var list = this.entities.Set<S_DOC_ListConfigDetail>().Where(d => d.ListConfigID == listconfig).WhereToGridData(qb);
            return Json(list);
        }
        public JsonResult QueryConfigGetList(QueryBuilder qb)
        {
            qb.SetSort("QuerySort", "asc");
            string NodeID = this.Request["NodeID"];
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            string listconfig = "";
            if (node != null)
            {
                listconfig = node.ListConfig().ID;
            }
            var list = this.entities.Set<S_DOC_QueryParam>().Where(d => d.ListConfigID == listconfig).WhereToGridData(qb);
            return Json(list);
        }
        protected override void BeforeSave(S_DOC_Node entity, bool isNew)
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
                        entity.IsElectronicBox = TrueOrFalse.True.ToString();
                    else
                        entity.IsElectronicBox = TrueOrFalse.False.ToString();
                    if (item.Value.ToString().Contains("Phyc"))
                        entity.IsPhysicalBox = TrueOrFalse.True.ToString();
                    else
                        entity.IsPhysicalBox = TrueOrFalse.False.ToString();
                }
            }
            entity.ExtentionJson = JsonHelper.ToJson<Dictionary<string, object>>(extDic);
            if (isNew)
                entity.Add();
            else
                entity.Save();


        }

        protected override void AfterSave(S_DOC_Node entity, bool isNew)
        {
            var listConfig = entity.ListConfig();
            if (listConfig != null)
            {
                string FormData = this.Request["FormData"];
                var json = JsonHelper.ToObject(FormData);
                listConfig.QueryKeyText = json["QueryKeyText"].ToString();
            }
            var Struct = entity.GetStructList();
            if (Struct != null)
            {

                for (int i = 0; i < Struct.Count; i++)
                {
                    Struct[i].Name = entity.Name;
                }
            }
        }

        protected override void BeforeDelete(List<S_DOC_Node> entityList)
        {
            foreach (var item in entityList)
                item.Delete();
        }
        protected override void AfterGetMode(Dictionary<string, object> dic, S_DOC_Node entity, bool isNew)
        {
            if (!isNew)
            {
                var listConfig = entity.ListConfig();
                dic["QueryKeyText"] = listConfig.QueryKeyText;
                var StorageType = "";
                if (dic.GetValue("IsPhysicalBox") == "True")
                    StorageType += "Phyc";
                if (dic.GetValue("IsElectronicBox") == "True")
                {
                    if (string.IsNullOrEmpty(StorageType))
                        StorageType += "Elec";
                    else
                        StorageType += ",Elec";
                }
                dic.SetValue("StorageType", StorageType);
            }
            else
            {
                dic["CanBorrow"] = TrueOrFalse.False.ToString();
                dic["CanDownload"] = TrueOrFalse.False.ToString();
                dic["QueryKeyText"] = "请输入编号或名称";
                dic["AllowDisplay"] = TrueOrFalse.True.ToString();
                dic["AllowAdvancedQuery"] = TrueOrFalse.True.ToString();
                dic["IsShowIndex"] = TrueOrFalse.False.ToString();
                dic.SetValue("StorageType", "Elec");
            }
            if (dic.ContainsKey("IsFreeNode") && (dic["IsFreeNode"] == null || string.IsNullOrEmpty(dic["IsFreeNode"].ToString())))
                dic["IsFreeNode"] = TrueOrFalse.False.ToString();
        }
        public void moveup(string ID, string gridID)
        {
            if (gridID == "nodeAttrGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_NodeAttr>(ID);
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
            this.entities.SaveChanges();
        }

        public void movedown(string ID, string gridID)
        {
            if (gridID == "nodeAttrGrid")
            {
                var nodeAttr = this.GetEntityByID<S_DOC_NodeAttr>(ID);
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

            this.entities.SaveChanges();
        }

        public JsonResult getrelation()
        {
            string nodeID = this.GetQueryString("NodeID");
            if (!string.IsNullOrEmpty(nodeID))
            {
                string sql = @"select S_DOC_File.Name,S_DOC_File.ID from S_DOC_FileNodeRelation
inner join S_DOC_File on S_DOC_FileNodeRelation.FileID = S_DOC_File.ID where NodeID ='" + nodeID + "' ";
                var dt = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig).ExecuteDataTable(sql);
                return Json(dt);
            }
            else
            {
                return Json("");
            }

        }

        public void addfilenode(string NodeID, string ListData)
        {
            var list = JsonHelper.ToList(ListData);
            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            if (node == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + NodeID + "】的节点定义对象");
            foreach (var item in list)
            {
                string id = item.GetValue("ID");
                if (string.IsNullOrEmpty(id)) continue;
                node.AddFileNode(id);
            }
            this.entities.SaveChanges();
        }
        public JsonResult FileConfigGetList(QueryBuilder qb)
        {
            string SpaceID = this.Request["SpaceID"];
            var list = this.entities.Set<S_DOC_File>().Where(d => d.SpaceID == SpaceID).WhereToGridData(qb);
            return Json(list);


        }
        public JsonResult deletefilenode(string NodeID, string ListIDs)
        {

            var node = this.GetEntityByID<S_DOC_Node>(NodeID);
            if (node == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + NodeID + "】的节点定义对象");
            Specifications res = new Specifications();
            res.AndAlso("ID", ListIDs.Split(','), QueryMethod.In);
            var list = entities.Set<S_DOC_File>().Where(res.GetExpression<S_DOC_File>()).ToList();
            foreach (var item in list)
            {
                string id = item.ID;
                node.DeleteFileNode(id);
            }
            entities.SaveChanges();
            return Json("");
        }

    }
}
