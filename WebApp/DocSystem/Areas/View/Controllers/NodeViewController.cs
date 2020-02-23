using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocSystem.Logic;
using System.Collections;
using DocSystem.Logic.Domain;

namespace DocSystem.Areas.View.Controllers
{
    public class NodeViewController : NodeController
    {
        public JsonResult getnodeconfiginfo()
        {
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            var list = this.Space.S_DOC_Node.Where(d => d.AllowDisplay == "True").ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edit()
        {
            string nodeConfigID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var node = space.S_DOC_Node.FirstOrDefault(d => d.ID == nodeConfigID);
            if (node == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + nodeConfigID + "】的节点定义对象");
            var queryForm = node.GetEditForm();
            var canBorrow = false;
            if (node.CanBorrow == TrueOrFalse.True.ToString())
                canBorrow = true;
            ViewBag.FormHtml = queryForm.Render();
            ViewBag.CanBorrow = canBorrow;
            var enumsList = node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).ToList();
            ViewBag.Enums = enumsList;
            return View();
        }

        public ActionResult List()
        {
            string QueryKey = "";
            string btnTemplate = "<a class=\"mini-button\" iconcls=\"{1}\" onclick=\"{2}\" plain=\"true\" id=\"{3}\">{0}</a>";
            string nodeConfigID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var node = space.S_DOC_Node.FirstOrDefault(d => d.ID == nodeConfigID);
            if (node == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + nodeConfigID + "】的节点定义对象");
            var grid = node.GetDataGrid(true) as MiniDataGrid;
            grid.Url = "GetList"; grid.Multiselect = true;
            ViewBag.DatagridHtml = grid.Render(node.IsShowIndex == "True");
            var queryForm = node.GetQueryForm();
            var table = queryForm.Controls.FirstOrDefault() as Table;
            int height = table.Rows.Count * 30 + 60;
            ViewBag.QueryFormHtml = queryForm.Render();
            QueryKey = space.S_DOC_ListConfig.FirstOrDefault(d => d.RelationID == nodeConfigID).QueryKeyText;
            if (node.S_DOC_FileNodeRelation.Count > 0)
                ViewBag.BtnHTML += String.Format(btnTemplate, "查看文件", "icon-extensions", "openfile()", "btnOpenFile");
            ViewBag.BtnHTML += String.Format(btnTemplate, "打开树形图", "icon-goto", "opentree()", "btnOpenTree");
            if (node.CanBorrow == "True")
            {
                ViewBag.BtnHTML += String.Format(btnTemplate, "借阅", "icon-copy", "borrow()", "btnBorrow");
            }
            var enumsList = node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).Distinct().ToList();
            ViewBag.Enums = enumsList;
            foreach (var item in node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList())
            {
                if (String.IsNullOrEmpty(item.EnumKey)) continue;
                var enumKey = item.EnumKey;
                if (enumKey.Split('.').Length > 1) enumKey = enumKey.Split('.')[1];
                ViewBag.Script += "addGridEnum(\"dataGrid\", \"" + item.AttrField + "\", \"" + enumKey + "\");";
            }
            return View();
        }

        public ActionResult ListSelect()
        {
            string QueryKey = "";
            string btnTemplate = "<a class=\"mini-button\" iconcls=\"{1}\" onclick=\"{2}\" plain=\"true\" id=\"{3}\">{0}</a>";
            string nodeConfigID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var node = space.S_DOC_Node.FirstOrDefault(d => d.ID == nodeConfigID);
            if (node == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + nodeConfigID + "】的节点定义对象");
            var grid = node.GetDataGrid(true) as MiniDataGrid;
            grid.Url = "GetList"; grid.Multiselect = true;
            ViewBag.DatagridHtml = grid.Render(node.IsShowIndex == "True");
            var queryForm = node.GetQueryForm();
            var table = queryForm.Controls.FirstOrDefault() as Table;
            int height = table.Rows.Count * 30 + 60;
            ViewBag.QueryFormHtml = queryForm.Render();
            QueryKey = space.S_DOC_ListConfig.FirstOrDefault(d => d.RelationID == nodeConfigID).QueryKeyText;
            if (node.S_DOC_FileNodeRelation.Count > 0)
                ViewBag.BtnHTML += String.Format(btnTemplate, "查看文件", "icon-extensions", "openfile()", "btnOpenFile");

            var enumsList = node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).Distinct().ToList();
            ViewBag.Enums = enumsList;
            foreach (var item in node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList())
            {
                if (String.IsNullOrEmpty(item.EnumKey)) continue;
                var enumKey = item.EnumKey;
                if (enumKey.Split('.').Length > 1) enumKey = enumKey.Split('.')[1];
                ViewBag.Script += "addGridEnum(\"dataGrid\", \"" + item.AttrField + "\", \"" + enumKey + "\");";
            }
            return View();
        }

        public ActionResult TreeEdit()
        {
            string nodeConfigID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var node = space.S_DOC_Node.FirstOrDefault(d => d.ID == nodeConfigID);
            if (node == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + nodeConfigID + "】的节点定义对象");
            var queryForm = node.GetEditForm();
            ViewBag.FormHtml = queryForm.Render();
            var enumsList = node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).ToList();
            ViewBag.Enums = enumsList;
            return View();
        }

        public JsonResult getnodefiletabs()
        {
            string ID = this.Request["ID"];
            var arraylist = new ArrayList();
            var fileRelations = this.Config.S_DOC_FileNodeRelation.OrderBy(d => d.Sort).ToList();
            foreach (var item in fileRelations)
            {
                var url = "../FileView/List?SpaceID=" + this.SpaceID + "&ConfigID=" + item.S_DOC_File.ID + "&NodeID=" + ID;
                arraylist.Add(createTab(item.S_DOC_File.Name, url));
            }
            return Json(arraylist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getnodeconfigtabs()
        {
            string ID = this.Request["ID"];
            var arraylist = new ArrayList();
            var fileRelations = this.Config.S_DOC_FileNodeRelation.OrderBy(d => d.Sort).ToList();

            foreach (var item in fileRelations)
            {
                var url = "../FileView/List?SpaceID=" + this.SpaceID + "&ConfigID=" + item.S_DOC_File.ID + "&ShowInclude=true&NodeID=" + ID + "&ShowAdvanceQuery=true&QueryType=File";
                if (item.S_DOC_File.FileCategory == FileCategory.Atlas.ToString())
                    url = "../FileView/GraphicList?SpaceID=" + this.SpaceID + "&ConfigID=" + item.S_DOC_File.ID + "&ShowInclude=true&NodeID=" + ID + "&ShowAdvanceQuery=true&QueryType=File";
                arraylist.Add(createTab(item.S_DOC_File.Name, url));
            }
            arraylist.Add(createTab(this.Config.Name + "属性", "TreeEdit?SpaceID=" + this.SpaceID + "&ConfigID=" + this.ConfigID
          + "&ID=" + ID + "&FuncType=view"));
            return Json(arraylist, JsonRequestBehavior.AllowGet);
        }

        public Dictionary<string, object> createMenuItem(string id, string name, string text, string iconCls, string onclick)
        {
            Dictionary<string, object> menuItem = new Dictionary<string, object>();
            menuItem["name"] = name;
            if (!String.IsNullOrEmpty(id))
                menuItem["id"] = id;
            menuItem["text"] = text;
            menuItem["iconCls"] = iconCls;
            menuItem["onClick"] = onclick;
            return menuItem;
        }

        public Dictionary<string, object> createTab(string title, string url)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["title"] = title;
            dic["url"] = url;
            return dic;

        }

        public override JsonResult GetList(MvcAdapter.QueryBuilder qb)
        {
            var statePush = qb.Items.FirstOrDefault(a => a.Field == "State");
            if (statePush == null)
                qb.Add("State", Formula.QueryMethod.In, "Published");
            return base.GetList(qb);
        }

        public override JsonResult gettreelist(bool isPublished = false)
        {
            return base.gettreelist(true);
        }
    }
}