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
using DocSystem.Logic;
using System.Collections;
using System.Text;

namespace DocSystem.Areas.Manager.Controllers
{
    public class NodeManagerController : DocSystem.NodeController
    {

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
            var grid = node.GetDataGrid(true, true, 40) as MiniDataGrid;
            grid.Url = "GetList"; grid.Multiselect = true;

            if (node.CanBorrow == "True")
            {
                var column = new MiniGridColumn();
                column.Field = "BorrowUserName";
                column.HeaderText = "借阅人";
                column.align = "center"; column.Allowsort = true; column.Width = 80;
                grid.AddControl(column);
            }
            MiniGridColumn columnLif = new MiniGridColumn();
            columnLif.Field = "ArchivesLifeCycle";
            columnLif.HeaderText = "操作记录";
            columnLif.align = "center"; columnLif.Allowsort = false; columnLif.Width = 100;
            grid.AddControl(columnLif);
            ViewBag.DataGridHtml = grid.Render(node.IsShowIndex == "True");
            var queryForm = node.GetQueryForm();
            var table = queryForm.Controls.FirstOrDefault() as Table;
            int height = table.Rows.Count * 30 + 60;
            ViewBag.QueryFormHtml = queryForm.Render();
            QueryKey = space.S_DOC_ListConfig.FirstOrDefault(d => d.RelationID == nodeConfigID).QueryKeyText;
            if (GetQueryString("$IsSelect") == "true")
                ViewBag.BtnHTML += " <a id='returnSelectList' class='mini-button' onclick='returnSelectList()' iconcls='icon-refer' plain='true'>选择</a>";
            if (node.Structs.Exists(d => d.Parent.NodeID == "Root"))
                ViewBag.BtnHTML += String.Format(btnTemplate, "添加", "icon-add", "add({width: '70%', height: '70%'})", "btnAdd");
            if (GetQueryString("$IsSelect") != "true")
            {
                ViewBag.BtnHTML += String.Format(btnTemplate, "修改", "icon-edit", "edit({width: '70%', height: '70%'})", "btnEdit");
                ViewBag.BtnHTML += String.Format(btnTemplate, "批量修改", "icon-edit", "multiEdit('/DocSystem/Manager/NodeManager/Edit')", "btnMultiEdit");
                ViewBag.BtnHTML += String.Format(btnTemplate, "删除", "icon-remove", "del()", "btnDel");
                ViewBag.BtnHTML += String.Format(btnTemplate, "打开树形图", "icon-goto", "opentree()", "btnOpenTree");
                //if (node.CanBorrow == "True")
                //    ViewBag.BtnHTML += String.Format(btnTemplate, "归还", "icon-copy", "borrowreturn()", "btnBorrowReturn");
                if (node.S_DOC_FileNodeRelation.Count > 0)
                    ViewBag.BtnHTML += String.Format(btnTemplate, "查看文件", "icon-extensions", "openfile()", "btnOpenFile");
                ViewBag.BtnHTML += String.Format(btnTemplate, "发布", "icon-downgrade", "publish()", "btnPublish");
                ViewBag.BtnHTML += String.Format(btnTemplate, "禁用", "icon-cancel", "forbidden()", "btnForbid");
                ViewBag.BtnHTML += String.Format(btnTemplate, "解封", "icon-unlock", "RelieveSeal()", "btnForbid");
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
            ViewBag.FormHtml = queryForm.Render();
            var enumsList = node.S_DOC_NodeAttr.Where(d => d.IsEnum == "True").ToList().Select(d => d.EnumKey).ToList();
            ViewBag.Enums = enumsList;
            ViewBag.IsMulti = this.Request["IsMulti"];
            var fileShowItems = node.S_DOC_NodeAttr.Where(a => a.Visible == "True");
            var dic = new Dictionary<string, string>();
            foreach (var item in fileShowItems)
                dic.SetValue(item.AttrField, item.AttrName);
            ViewBag.FieldInfo = JsonHelper.ToJson(dic);
            ViewBag.SelectorScipts = renderSelectorScipts(node);
            return View();
        }

        private string renderSelectorScipts(S_DOC_Node node)
        {
            var baseEntities = FormulaHelper.GetEntities<Base.Logic.Domain.BaseEntities>();
            StringBuilder sb = new StringBuilder();
            var fields = node.S_DOC_NodeAttr.Where(d => d.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0)
                .ToList();
            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field.SelectorKey)) continue;
                string returnParams = "value:ID,text:Name";
                if (!string.IsNullOrEmpty(field.ReturnParams)) returnParams = field.ReturnParams;
                if (field.SelectorKey == "SystemUser")
                {
                    if(field.MultiSelect=="True")
                        sb.AppendFormat("addMultiUserSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.AttrField, returnParams, "");
                    else
                        sb.AppendFormat("addSingleUserSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.AttrField, returnParams, "");
                }
                else if (field.SelectorKey == "SystemOrg")
                {
                    if (field.MultiSelect == "True")
                        sb.AppendFormat("addMultiOrgSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.AttrField, returnParams, "");
                    else
                        sb.AppendFormat("addSingleOrgSelector('{0}',{{returnParams:'{1}',UrlParams:'{2}'}});\n", field.AttrField, returnParams, "");
                }
                else
                {
                    var selector = baseEntities.Set<Base.Logic.Domain.S_UI_Selector>().SingleOrDefault(c => c.Code == field.SelectorKey);
                    if (selector == null) throw new Exception("选择器【" + field.SelectorKey + "】不存在，绘制选择器控件【" + field.AttrName + "】失败");
                    var url = selector.URLSingle;
                    if (field.MultiSelect == "True")
                        url = selector.URLMulti;
                    sb.AppendFormat("addSelector('{0}', '{1}', {{ allowResize: true,title:'{2}',width:'{3}',height:'{4}',returnParams:'{5}' }});\n"
                        , field.AttrField, url, selector.Title, selector.Width, selector.Height, returnParams);
                }
            }
            return sb.ToString();
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
            ViewBag.SelectorScipts = renderSelectorScipts(node);
            return View();
        }

        protected override void AfterSave(S_NodeInfo node, Dictionary<string, object> row, bool isNew)
        {
            string ReorganizeID=GetQueryString("ReorganizeID");
            if (!isNew)
            {
                node.SynchronizeAttr();
            }
            node.DataEntity["HasFile"] = 0;//是否可以添加文件（整编区，整编界面右侧树增加节点需要此配置）'
            if (node.ConfigInfo.S_DOC_FileNodeRelation.Count > 0)
                //node.DataEntity["HasFile"] = 1;
                node.DataEntity["HasFile"] = string.Join(",", node.ConfigInfo.S_DOC_FileNodeRelation.Select(a => a.FileID));
            if (!string.IsNullOrEmpty(ReorganizeID))
            {
                var Reorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.Where(a => a.ID.Equals(ReorganizeID)).ToList();
                if (Reorganize.Count > 0)
                    new DocSystem.Areas.Reorganize.Controllers.PhysicalController().saveRootNodes(node.FullPathID, "", ReorganizeID, node.Space.ID);//实物存储
                else
                    new DocSystem.Areas.Reorganize.Controllers.MainController().saveRootNodes(node.FullPathID, "", ReorganizeID, node.Space.ID);//电子存储
            }
        }

        public override JsonResult Delete()
        {
            string listIDs = Request["ListIDs"];
            var whereStr = listIDs.Replace(",", "%' or FullPathID like '");
            var sql = @"select 1 from S_NodeInfo with(nolock) where State='Published' and (FullPathID like '%" + whereStr + "%') ";
            var dt = this.InstanceDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                throw new Formula.Exceptions.BusinessValidationException("存在节点已发布，不能删除！");
            sql = @"select 1 from S_FileInfo with(nolock) where State='Published' and (FullNodeID like '%" + whereStr.Replace("FullPathID", "FullNodeID") + "%') ";
            dt = this.InstanceDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                throw new Formula.Exceptions.BusinessValidationException("存在文件已发布，不能删除！");
            return base.Delete();
        }

        public JsonResult getnodeconfiginfo()
        {
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            var list = this.Space.S_DOC_Node.Where(d => d.AllowDisplay == "True").OrderBy(a => a.SortIndex).ToList();
            var queryType = this.GetQueryString("$QueryType");
            if (queryType.ToLower() == "sealup")
            {
                list = list.Where(a => a.IsPhysicalBox == TrueOrFalse.True.ToString()).ToList();
            }
            
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getnodeconfigtabs()
        {
            string ID = this.Request["ID"]; var node = S_NodeInfo.GetNode(ID, this.SpaceID);
            var arraylist = new ArrayList();
            var fileRelations = this.Config.S_DOC_FileNodeRelation.OrderBy(d => d.Sort).ToList();

            foreach (var item in fileRelations)
            {
                var fileconfig = item.S_DOC_File;
                var url = "../FileManager/List?SpaceID=" + this.SpaceID + "&ConfigID=" + item.S_DOC_File.ID + "&NodeID=" + ID + "&ShowInclude=true";
                if (String.IsNullOrEmpty(fileconfig.PreCondition))
                    arraylist.Add(createTab(item.S_DOC_File.Name, url));
                else
                {
                    string sql = "select count(0) from S_NodeInfo where ID='" + ID + "' and " + fileconfig.PreCondition;
                    var dt = this.InstanceDB.ExecuteDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        arraylist.Add(createTab(item.S_DOC_File.Name, url));
                    }
                }
            }
            arraylist.Add(createTab(this.Config.Name + "属性", "TreeEdit?SpaceID=" + this.SpaceID + "&ConfigID=" + this.ConfigID
          + "&ID=" + ID + "&action=edit"));
            return Json(arraylist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getnodefiletabs()
        {
            string ID = this.GetQueryString("ID");
            var arraylist = new ArrayList();
            var fileRelations = this.Config.S_DOC_FileNodeRelation.OrderBy(d => d.Sort).ToList();
            var node = S_NodeInfo.GetNode(ID, this.SpaceID);
            foreach (var item in fileRelations)
            {
                var fileconfig = item.S_DOC_File;
                var url = "../FileManager/List?SpaceID=" + this.SpaceID + "&ConfigID=" + fileconfig.ID + "&NodeID=" + ID + "&FullPathID=" + node.FullPathID;
                if (String.IsNullOrEmpty(fileconfig.PreCondition))
                    arraylist.Add(createTab(item.S_DOC_File.Name, url));
                else
                {
                    string sql = "select count(0) from S_NodeInfo where ID='" + ID + "' and " + fileconfig.PreCondition;
                    var dt = this.InstanceDB.ExecuteDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        arraylist.Add(createTab(item.S_DOC_File.Name, url));
                    }
                }
            }
            return Json(arraylist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult gettreecontextmenu()
        {
            var list = this.Config.StructNode.Children.ToList();
            var arraylist = new ArrayList();
            foreach (var item in list)
                arraylist.Add(createMenuItem(item.ID, item.NodeID, "添加" + item.Name, "icon-add", "addNode"));
            if (this.Config.StructNode.NodeEntity != null
                && this.Config.StructNode.NodeEntity.IsFreeNode == TrueOrFalse.True.ToString())
                arraylist.Add(createMenuItem("", this.Config.StructNode.NodeEntity.ID, "添加", "icon-add", "addNode"));
            arraylist.Add(createMenuItem("", "cancel", "发布", "icon-downgrade", "publish"));
            arraylist.Add(createMenuItem("", "cancel", "禁用", "icon-close", "forbidden"));
            arraylist.Add(createMenuItem("", "delete", "删除", "icon-remove", "deleteNode"));
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
        public JsonResult MoveUp(string ID, string SpaceID, string TargetNodeID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new Formula.Exceptions.BusinessException("未指定档案实体空间，无法获取数据访问对象");
            var node = new S_NodeInfo(ID, space);
            node.MoveUp(TargetNodeID);
            return Json("");
        }

        public JsonResult MoveDown(string ID, string SpaceID, string TargetNodeID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new Formula.Exceptions.BusinessException("未指定档案实体空间，无法获取数据访问对象");
            var node = new S_NodeInfo(ID, space);
            node.MoveDown(TargetNodeID);
            return Json("");
        }
        public ActionResult NormalStateListTab()
        {
            var spaces=FormulaHelper.GetEntities<DocConfigEntities>().Set<S_DOC_Space>().Select(a => a).ToList();
            ViewBag.spaces = spaces;
            return View();
        }
        public ActionResult NormalStateListForm()
        {
            return View();
        }
        //保存时对比数据是否修改
        protected override void BeforeSave(S_NodeInfo node, Dictionary<string, object> row, bool isNew)
        {
            string detail=string.Empty;
            if (!isNew)
            {
                S_NodeInfo oldNode = new S_NodeInfo(node.ID, node.Space);
                var nodeAttrs = node.ConfigInfo.S_DOC_NodeAttr.Select(a => a);
                var enumList = EnumBaseHelper.GetEnumDef("DocConst.KeepYear").EnumItem;
                foreach (S_DOC_NodeAttr nodeAttr in nodeAttrs)
                {
                    if (nodeAttr.InputType.Equals("ButtonEdit"))
                        detail += node.DataEntity.GetValue(nodeAttr.AttrField + "Name") == oldNode.DataEntity.GetValue(nodeAttr.AttrField + "Name") ? "" : "\"" + nodeAttr.AttrName + ":" + oldNode.DataEntity.GetValue(nodeAttr.AttrField + "Name") + "\"修改为\"" + node.DataEntity.GetValue(nodeAttr.AttrField + "Name") + "\";";

                    else if (nodeAttr.InputType.Equals("Combobox"))
                    {
                        var enums = EnumBaseHelper.GetEnumDef(nodeAttr.EnumKey).EnumItem;
                        var enumNameOld = enums.FirstOrDefault(a => a.Code.Equals(oldNode.DataEntity.GetValue(nodeAttr.AttrField)));
                        string oldName = enumNameOld != null ? enumNameOld.Name : "";
                        var enumName = enums.FirstOrDefault(a => a.Code.Equals(node.DataEntity.GetValue(nodeAttr.AttrField)));
                        detail += node.DataEntity.GetValue(nodeAttr.AttrField) == oldNode.DataEntity.GetValue(nodeAttr.AttrField) ? "" : "\"" + nodeAttr.AttrName + ":" + oldName + "\"修改为\"" + enumName.Name + "\";";
                    }
                    else
                    {
                        if (nodeAttr.AttrField.Equals("SortIndex") || nodeAttr.AttrField.Equals("DocIndexID"))
                            continue;
                        detail += node.DataEntity.GetValue(nodeAttr.AttrField) == oldNode.DataEntity.GetValue(nodeAttr.AttrField) ? "" : "\"" + nodeAttr.AttrName + ":" + oldNode.DataEntity.GetValue(nodeAttr.AttrField) + "\"修改为\"" + node.DataEntity.GetValue(nodeAttr.AttrField) + "\";";

                    }
                }   
                if (!string.IsNullOrEmpty(detail))
                    //修改，详情说明
                    InventoryFO.CreateNewInventoryLedger(oldNode, detail,InventoryType.Update);
            }
        }
    }
}
