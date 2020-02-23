using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcAdapter;
using Config;
using Config.Logic;
using DocSystem.Logic.Domain;
using DocSystem.Logic;
using Formula.Exceptions;
using System.Collections;
using System.Data;
using Formula.Helper;
using Base.Logic.BusinessFacade;
using System.Text;
using Formula;
using Base.Logic.Domain;

namespace DocSystem.Areas.Reorganize.Controllers
{
    public class MainController : DocConstController
    {
        #region 整编首页

        public ActionResult Index()
        {
            //var CreateDataCount = this.SqlHelper.ExecuteScalar("select count(1) from S_R_Reorganize where ReorganizeState in ('" + ReorganizeState.Create.ToString() + "','" + ReorganizeState.Execute.ToString() + "')");
            //ViewBag.CreateDataCount = CreateDataCount;//待整编任务数
            //var userID = this.CurrentUserInfo.UserID;
            //var FinishDataCount = this.SqlHelper.ExecuteScalar("select count(1) from S_R_Reorganize where ReorganizeState='" + ReorganizeState.Finish.ToString() + "' and ReorganizeUser='" + userID + "'");
            //ViewBag.FinishDataCount = FinishDataCount;//已整编资料数
            //var LastDate = this.entities.Set<S_R_Reorganize>().Where(a => a.ReorganizeUser == userID).Max(a => a.ReorganizeDate);
            //ViewBag.LastDate = LastDate.HasValue ? LastDate.Value.ToString("yyyy-MM-dd") : "";//最后整理日期

            var SpaceList = new List<Dictionary<string, string>>();
            var spaces = FormulaHelper.GetEntities<DocConfigEntities>().Set<S_DOC_ReorganizeConfig>()
                .Where(a => a.Enabled == "true").Select(a => a.S_DOC_Space).ToList();
            foreach (var space in spaces)
            {
                var fileCount = string.Empty;
                var reorgConfig = space.S_DOC_ReorganizeConfig.FirstOrDefault();
                if (reorgConfig != null && !string.IsNullOrEmpty(reorgConfig.CountSQL))
                {
                    var sqlInsHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                    try
                    {
                        var obj = sqlInsHelper.ExecuteScalar(reorgConfig.CountSQL);
                        if (obj != null && obj != DBNull.Value)
                            fileCount = obj.ToString();
                    }
                    catch (Exception e)
                    {
                        fileCount = e.Message;
                    }
                }
                if (reorgConfig.ElectronicMainFormCode == null || reorgConfig.ElectronicMainFormCode == "")
                    continue;
                var dic = new Dictionary<string, string>();
                dic.SetValue("Name", space.Name);
                dic.SetValue("FileCount", fileCount);
                dic.SetValue("SpaceID", space.ID);
                dic.SetValue("SpaceKey", space.SpaceKey);
                dic.SetValue("ElectronicMainFormCode", reorgConfig.ElectronicMainFormCode);
                SpaceList.Add(dic);
            }
            ViewBag.SpaceList = SpaceList;
            ViewBag.SpaceStr = JsonHelper.ToJson(SpaceList);
            return View();
        }
        //整编TabsEnum
        public ActionResult ElectronicReorganTabs()
        {
            return View();
        }
        public JsonResult GetTaskList(QueryBuilder qb)
        {
            var sql = @"select *,(select count(1) from S_R_Reorganize_DocumentList where S_R_ReorganizeID = S_R_Reorganize.ID) DocCount from S_R_Reorganize ";
            var grid = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(grid);
        }

        public JsonResult CheckExecuteReorganize(string IDs)
        {
            var rtn = false;
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_Reorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            if (list.Any(a => a.ReorganizeState == DocSystem.Logic.ReorganizeState.Execute.ToString()
                && a.ReorganizeUser != CurrentUserInfo.UserID))
            {
                var task = list.FirstOrDefault(a => a.ReorganizeState == DocSystem.Logic.ReorganizeState.Execute.ToString()
                && a.ReorganizeUser != CurrentUserInfo.UserID);
                throw new Formula.Exceptions.BusinessException("【" + task.TaskName + "】已经由【" + task.ReorganizeUserName + "】整编，请等【" + task.ReorganizeUserName + "】整编完成以后再操作");
            }
            if (list.Any(a =>a.ReorganizeState == DocSystem.Logic.ReorganizeState.Finish.ToString()))
                rtn = true;
            return Json(new { rtn });
        }

        public JsonResult ExecuteReorganize(string IDs)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_Reorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
                item.Execute();
            this.entities.SaveChanges();
            return Json("");
        }
        public JsonResult WaithReorganize(string IDs)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_Reorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
            {
                if (item.ReorganizeState.ToString().Equals(DocSystem.Logic.PhysicalReorganizeState.Finish.ToString()))
                    continue;
                item.Wait();
            }
            this.entities.SaveChanges();
            return Json("");
        }
        public JsonResult CheckFinishReorganize(string IDs)
        {
            var rtn = false;
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_Reorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
            {
                item.CheckFinish();
                var detaillist = item.S_R_Reorganize_DocumentList.ToList();
                if (detaillist.Any(a => string.IsNullOrEmpty(a.ReorganizeFullID)))
                {
                    rtn = true;
                    break;
                }
            }
            return Json(new { rtn });
        }

        public JsonResult FinishReorganize(string IDs)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_Reorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
                item.Finish();
            this.entities.SaveChanges();
            return Json("");
        }

        #endregion

        #region 整编操作页面

        public ActionResult Reorganize()
        {
            var SpaceID = GetQueryString("SpaceID");
            //baseEntities.Set<S_UI_Form>()
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            //var config = space.S_DOC_ReorganizeConfig.FirstOrDefault();
            //if (config == null || string.IsNullOrEmpty(config.Items))
            //    throw new BusinessException("请先配置档案整编区定义");
            var sbField = new System.Text.StringBuilder();
            var sbScript = new System.Text.StringBuilder();
            var sbSelector = new System.Text.StringBuilder();
            //var fields = JsonHelper.ToList(config.Items);
            var baseEntities = FormulaHelper.GetEntities<BaseEntities>();
            var formDef = baseEntities.Set<S_UI_Form>().FirstOrDefault(a => a.Code == "Reorganize");
            if (formDef == null || string.IsNullOrEmpty(formDef.Items))
                throw new BusinessException("请先配置表单");
            var field = JsonHelper.ToList(formDef.Items).FirstOrDefault(a => a.GetValue("Code").ToString() == "DocumentList");
            var fo = new UIFO();
            //foreach (var field in fields)
            //{
            //var settingStr = field.GetValue("Settings");
            if (field == null || string.IsNullOrEmpty(field.GetValue("Code").ToString()))
                throw new BusinessException("请先配置表单子表");
            var settings = JsonHelper.ToObject(field.GetValue("Settings"));
            var documentLists = JsonHelper.ToList(settings.GetValue("listData"));//拿到子表字段
            foreach (var docList in documentLists)
            {
                string header = docList.GetValue("Name");
                var settingStrDocList = docList.GetValue("Settings");
                var settingsDocList = JsonHelper.ToObject(settingStrDocList);
                if (docList.GetValue("Code").ToString().Equals("ReorganizePath") && !Convert.ToBoolean(docList.GetValue("Visible")))//当整编目录在子表中时隐藏的,改为显示,
                    docList.SetValue("Visible", "true");
                if (Convert.ToBoolean(docList.GetValue("Visible"))) //该字段是否可见
                {
                    string selector = settingsDocList.GetValue("SelectorKey");
                    if (!string.IsNullOrEmpty(selector))//弹出选择
                        sbField.AppendFormat("<div name='{3}' field='{3}' {0} {1} header='{2}'  allowdrag='true' headeralign='center'></div>", fo.GetMiniuiSettings(docList)
                                , fo.GetMiniuiSettings(docList.GetValue("Settings"))
                                , header, docList.GetValue("Code")+"Name");
                    else
                        sbField.AppendFormat("<div name='{3}' field='{3}' {0} {1} header='{2}'  allowdrag='true' headeralign='center'></div>", fo.GetMiniuiSettings(docList)
                                , fo.GetMiniuiSettings(docList.GetValue("Settings"))
                                , header, docList.GetValue("Code"));
                }
                if (string.IsNullOrEmpty(settingStrDocList))
                    continue;
                
                //子表枚举
                string enumKey = settingsDocList.GetValue("data");
                if (!string.IsNullOrEmpty(enumKey))
                {

                    string tableName = formDef.TableName.Split(',')[0];
                    var key = GetEnumKey(formDef.ConnName, tableName, docList.GetValue("Code"), enumKey);
                    string enumStr = GetEnumString(formDef.ConnName, tableName, docList.GetValue("Code"), enumKey);

                    if (string.IsNullOrEmpty(enumStr))
                        enumStr = "[]";
                    sbScript.AppendFormat("\n var {0} = {1}", key, enumStr);

                    sbScript.AppendFormat("\n addGridEnum('fileTreeGrid', '{0}', '{1}');", field.GetValue("Code"), key);
                }
                //}
            }
            
            ViewBag.FileTreeFieldsHtml = sbField.ToString();
            ViewBag.FileTreeFieldsScript = sbScript.ToString();
            return View();
        }

        public JsonResult GetFileTreeList(QueryBuilder qb)
        {
            var reorganizeID = GetQueryString("ReorganizeID");
            qb.PageSize = 0;
            var sql = string.Format(@"
select *,'File' NodeType, '' ParentID from S_R_Reorganize_DocumentList
where S_R_ReorganizeID='{0}' ", reorganizeID);
            var grid = this.SqlHelper.ExecuteDataTable(sql, qb);
            return Json(grid);
        }

        public JsonResult GetNodeTreeList(QueryBuilder qb)
        {
            var reorganizeID = GetQueryString("ReorganizeID");


            S_R_Reorganize Reorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_Reorganize.FirstOrDefault(a => a.ID.Equals(reorganizeID));
           // string[] defaultNodes = Reorganize.DefaultNodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
 


            //var sql = @"select * from S_R_Reorganize where ID='" + reorganizeID + "'";
            //var reogDetailList = this.SqlHelper.ExecuteList<S_R_Reorganize>(sql);
            //var ids = string.Join(",", reogDetailList.Where(a => !string.IsNullOrEmpty(a.DefaultNodes))
            //    .Select(a => a.DefaultNodes).Distinct().ToArray());
            //ids = ids.TrimEnd(',');
            var spaceID = this.GetQueryString("SpaceID");
            if (!string.IsNullOrEmpty(Reorganize.SelectNodeIDs))
                return GetSelectedTreeList(Reorganize.SelectNodeIDs, spaceID);
            else return Json("");
        }

        public JsonResult GetSelectedTreeList(string DefaultNodes, string SpaceID, bool ShowFile = false)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            //string idStrs = "FullPathID like '" + IDs.Replace(",", "%' or FullPathID like '") + "%' or ( '" + IDs.Replace(",", "' like FullPathID+'%' or '") + "' like FullPathID+'%')";//查出所有上层节点与自身及下层节点
            string idStrs = "id in {0}";
            string nodeIDs = GetQueryString("nodeIDs");
            if (!string.IsNullOrEmpty(nodeIDs))
            {
                string sqlFullPathId = "select FullPathID,Name from S_NodeInfo where ID in ('{0}')";
                string nodeFullPathIDs = string.Empty;
                sqlFullPathId = string.Format(sqlFullPathId, nodeIDs.Replace(",", "','"));
                SQLHelper helper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                DataRowCollection Rows = helper.ExecuteDataTable(sqlFullPathId).Rows;
                string nodeNames = string.Empty;
                foreach (DataRow row in Rows)
                {
                    nodeFullPathIDs += row["FullPathID"] + ",";
                }
                nodeFullPathIDs = nodeFullPathIDs.TrimEnd(',');
                idStrs = string.Format(idStrs, "('" + nodeFullPathIDs.Replace(",", ".").Replace(".", "','") + "') or FullPathID like '" + nodeFullPathIDs.Replace(",", "%' or FullPathID like '") + "%'");
            }
            else
            {
                string ReorganizeFullID = string.Join("','", GetUniqFullPathIDs(DefaultNodes));
                if (!string.IsNullOrEmpty(ReorganizeFullID))
                    idStrs = string.Format(idStrs, "('" + ReorganizeFullID + "')");
            }
            string sql = @"select ID,ParentID,{0},FullPathID,ConfigID,'' as NodeType,'' rootRemove  from S_NodeInfo where (" + idStrs + ") and SpaceID='" + SpaceID + "' {1}";
            S_R_Reorganize reorganize=new S_R_Reorganize();
            string ReorganizeID = GetQueryString("ReorganizeID");
            if (!string.IsNullOrEmpty(ReorganizeID))
                reorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_Reorganize.FirstOrDefault(a => a.ID == ReorganizeID);
            var treeConfig = space.S_DOC_TreeConfig.FirstOrDefault();
            if (treeConfig != null)
                sql = String.Format(sql, "Name", treeConfig.GetOrderByStr());
            else
                sql = String.Format(sql, "Name", "");
            var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            var dt = instanceDB.ExecuteDataTable(sql);
            var fileSql = "select ID,Name,NodeID as ParentID,FullNodeID+'.'+ID as FullPathID,ConfigID from S_FileInfo where NodeID in (select ID from (" + (treeConfig != null ? sql.Replace(treeConfig.GetOrderByStr(), "") : sql) + ") tmp)";
            var fileDt = instanceDB.ExecuteDataTable(fileSql);
            //节点都能关联文件
            //var fileNodeIDs = space.S_DOC_Node.Where(a => a.S_DOC_FileNodeRelation.Count > 0).Select(a => a.ID).ToList();
            var resultDt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var configID = row["ConfigID"].ToString();
                var config = space.S_DOC_Node.FirstOrDefault(a => a.ID == configID);
                //if (fileNodeIDs.Contains(configID))
                //    row["HasFile"] = 1;
                if (DefaultNodes.Split(',').Contains(row["ID"].ToString()))
                    row["NodeType"] = "Root";
                else
                    row["NodeType"] = "Child";
                resultDt.ImportRow(row);

                var fileRelationList = config.S_DOC_FileNodeRelation.ToList();
                foreach (var relation in fileRelationList)
                {
                        var relationRow = resultDt.NewRow();//虚拟文件类型节点
                        relationRow["ID"] = row["ID"].ToString() + '.' + relation.FileID;//NodeID.FileConfigID 作为虚拟节点ID;
                        relationRow["Name"] = relation.S_DOC_File.Name;
                        relationRow["ParentID"] = row["ID"];
                        relationRow["FullPathID"] = row["FullPathID"];
                        relationRow["ConfigID"] = relation.FileID;
                        relationRow["NodeType"] = "FileConfig";
                        resultDt.Rows.Add(relationRow);
                        //追加已有文件
                        if (ShowFile)
                        {
                            var files = fileDt.Select("ParentID='" + row["ID"].ToString() + "' and ConfigID = '" + relation.FileID + "'");
                            foreach (var fileRow in files)
                            {
                                var _filerow = resultDt.NewRow();
                                _filerow["ID"] = fileRow["ID"];
                                _filerow["Name"] = fileRow["Name"];
                                _filerow["ParentID"] = relationRow["ID"];
                                _filerow["FullPathID"] = fileRow["FullPathID"];
                                _filerow["ConfigID"] = fileRow["ConfigID"];
                                _filerow["NodeType"] = "File";
                                resultDt.Rows.Add(_filerow);
                            }
                        }
                }

            }
            return Json(resultDt, JsonRequestBehavior.AllowGet);
        }
        //最加文件类型节点
        public JsonResult AddFileNodeType(string NodeData)
        {
            var data = JsonHelper.ToObject(NodeData);
            var children = new List<Dictionary<string, string>>();
            var configID = data.GetValue("ConfigID");
            var SpaceID = data.GetValue("SpaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var config = space.S_DOC_Node.FirstOrDefault(a => a.ID == configID);
            var fileRelationList = config.S_DOC_FileNodeRelation.ToList();
            foreach (var relation in fileRelationList)
            {
                Dictionary<string, string> relationDic = new Dictionary<string, string>();
                relationDic["ID"] = data.GetValue("ID") + '.' + relation.FileID;//NodeID.FileConfigID 作为虚拟节点ID;
                relationDic["Name"] = relation.S_DOC_File.Name;
                relationDic["ParentID"] = data.GetValue("ID");
                relationDic["FullPathID"] = data.GetValue("FullPathID");
                relationDic["ConfigID"] = relation.FileID;
                relationDic["NodeType"] = "FileConfig";
                children.Add(relationDic);
                //table.Rows.Add(relationRow);
            }
            data.SetValue("children", children);
            return Json(data);
        }
        //移除实物整编结点
        public JsonResult RemoveReorganizeNode(string nodeID, string ReorganizeID)
        {

            string spaceID = GetQueryString("SpaceID");
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID == spaceID);
            List<S_R_Reorganize_DocumentList> fileDetailList = entities.Set<S_R_Reorganize_DocumentList>().Where(a => a.S_R_ReorganizeID == ReorganizeID && a.ReorganizeFullID.Contains(nodeID)).ToList();
            if (fileDetailList.Count > 0)
                throw new Formula.Exceptions.BusinessException("有归档文件，不可移除");
            S_R_Reorganize PhysicalData = entities.Set<S_R_Reorganize>().FirstOrDefault(a => a.ID == ReorganizeID);
            List<string> nodeIDs = PhysicalData.SelectNodeIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();//全部路径ID


            string fullIDSql = "select FullPathID from S_NodeInfo where ID='" + nodeID + "'";
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            string fullID = sqlHelper.ExecuteScalar(fullIDSql).ToString();//得到移除节点的全路径
            string sql = @"select ID from S_NodeInfo where FullPathID like '" + fullID + "%'";//得到移除节点及子节点ID
            DataRowCollection rows = sqlHelper.ExecuteDataTable(sql).Rows;
            List<string> listIDs = new List<string>();
            foreach (DataRow row in rows)
                listIDs.Add(row["ID"].ToString());
            nodeIDs.RemoveWhere(a => listIDs.Contains(a.ToString()));

            PhysicalData.SelectNodeIDs = string.Join(",", nodeIDs);
            entities.SaveChanges();
            return Json("");
        }
        public JsonResult GetNodeTreeMenu(string SpaceID, string ConfigID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            var nodeConfig = space.S_DOC_Node.FirstOrDefault(d => d.ID == ConfigID);
            if (nodeConfig == null)
                throw new BusinessException("未能找ID为【" + ConfigID + "】的编目定义对象");
            var list = nodeConfig.StructNode.Children.ToList();
            var arraylist = new ArrayList();
            foreach (var item in list)
            {
                arraylist.Add(createMenuItem(item.ID, item.NodeID, "添加" + item.Name, "icon-add", "addNode"));
            }
            arraylist.Add(createMenuItem("", "edit", "编辑", "icon-edit", "editNode"));
            arraylist.Add(createMenuItem("", "delete", "删除", "icon-remove", "deleteNode"));
            arraylist.Add(createMenuItem("", "remove", "移除", "icon-remove", "RemoveNode"));
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

        public JsonResult MoveUp(string ID, string SpaceID, string TargetNodeID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            var node = new S_NodeInfo(ID, space);
            node.MoveUp(TargetNodeID);
            return Json("");
        }

        public JsonResult MoveDown(string ID, string SpaceID, string TargetNodeID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            var node = new S_NodeInfo(ID, space);
            node.MoveDown(TargetNodeID);
            return Json("");
        }

        public JsonResult DeleteReorganizeFile(string FileList, string SpaceID)
        {
            var docSpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var list = JsonHelper.ToList(FileList);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                var archFileID = item.GetValue("ArchiveFileID");
                S_FileInfo fileInfo = new S_FileInfo(archFileID, docSpace);
                fileInfo.Delete();
                item.SetValue("ReorganizePath", "");
                item.SetValue("ReorganizeFullID", "");
                item.SetValue("ReorganizeConfigID", "");
                item.SetValue("ArchiveFileID", "");
                item.SetValue("ArchiveFileAttrs", "");
                var sql = "update S_R_Reorganize_DocumentList set ReorganizePath='',ReorganizeFullID='',ReorganizeConfigID='',ArchiveFileID='',ArchiveFileAttrs='' where id='{0}'";
                sql = string.Format(sql, item.GetValue("ID"));
                sb.AppendLine(sql);
            }
            if (sb.Length > 0)
                this.SqlHelper.ExecuteNonQuery(sb.ToString());
            return Json(list);
        }

        public JsonResult ArchiveReorganize(string FileList, string SpaceID, string TargetNodeID)
        {
            //直接归档
            var docSpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            S_NodeInfo node = S_NodeInfo.GetNode(TargetNodeID, SpaceID);

            var list = JsonHelper.ToList(FileList);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                var archFileID = item.GetValue("ArchiveFileID");
                if (string.IsNullOrEmpty(archFileID))
                {
                    //新增档案文件记录
                    S_FileInfo fileInfo = archFile(item, node, item.GetValue("ConfigID"));
                    archFileID = fileInfo.ID;
                    item.SetValue("ArchiveFileID", archFileID);
                }
                else
                {
                    //移动文件
                    S_FileInfo fileInfo = new S_FileInfo(archFileID, docSpace);
                    fileInfo.MoveTo(TargetNodeID, item.GetValue("ConfigID"));
                }
                var sql = "update S_R_Reorganize_DocumentList set ReorganizePath='{1}',ReorganizeFullID='{2}',ArchiveFileID='{3}',ReorganizeConfigID='{4}' where id='{0}'";
                sql = string.Format(sql, item.GetValue("ID"), item.GetValue("ReorganizePath"), item.GetValue("ReorganizeFullID"), archFileID, item.GetValue("ConfigID"));
                sb.AppendLine(sql);
            }
            if (sb.Length > 0)
                this.SqlHelper.ExecuteNonQuery(sb.ToString());
            return Json(list);
        }

        private S_FileInfo archFile(Dictionary<string, object> reorganizeDetail, S_NodeInfo node, string targetConfigID = "")
        {
            var fileConfig = node.ConfigInfo.S_DOC_FileNodeRelation.FirstOrDefault(a => a.FileID == targetConfigID);
            if (fileConfig == null )
                throw new Formula.Exceptions.BusinessException("节点【"+node.Name+"】下无法绑定该文件类型【"+targetConfigID+"】！");
            if (fileConfig != null && fileConfig.S_DOC_File != null)
            {

                var file = new DocSystem.Logic.Domain.S_FileInfo(node.Space.ID, fileConfig.S_DOC_File.ID);
                var fileFields = fileConfig.S_DOC_File.S_DOC_FileAttr.Select(a => a.FileAttrField).ToList();
                var buttonEditFields = fileConfig.S_DOC_File.S_DOC_FileAttr.Where(a => a.InputType.IndexOf(ControlType.ButtonEdit.ToString()) > -1).ToList();
                foreach (var item in buttonEditFields)
                    fileFields.Add(item.FileAttrField + "Name");

                foreach (var fileds in fileFields)
                {
                    if (fileds.ToLower() == "id" || fileds.ToLower() == "attr" || fileds.ToLower() == "storagenum" || fileds.ToLower() == "storagetype" || fileds.ToLower() == "mainfile")
                        continue;
                    var _value = reorganizeDetail.GetValue(fileds);
                    if (fileds.ToLower().Equals("quantity"))
                        file.DataEntity.SetValue("StorageNum", _value);
                    if (!string.IsNullOrEmpty(_value))
                        file.DataEntity.SetValue(fileds, _value);
                }



                //var file = new DocSystem.Logic.Domain.S_FileInfo(node.Space.ID, fileConfig.S_DOC_File.ID);
                if (!String.IsNullOrEmpty(reorganizeDetail.GetValue("Attr")))
                    SetFileAttr(file, reorganizeDetail.GetValue("Attr"));
                //file.DataEntity.SetValue("Name", reorganizeDetail.GetValue("Name"));
                file.DataEntity.SetValue("StorageType","Electronic");
                file.DataEntity.SetValue("NodeID", node.ID);
                //file.DataEntity.SetValue("Code", reorganizeDetail.GetValue("Code"));
                //file.DataEntity.SetValue("RelateID", reorganizeDetail.GetValue("RelateID"));
                //file.DataEntity.SetValue("State", DocState.Normal);
                //归档人、部门、时间存储
                if(!string.IsNullOrEmpty(GetQueryString("SendUser")))
                {
                    Dictionary<string,string> depate= GetDepartment(GetQueryString("SendUser"));
                    file.DataEntity.SetValue("ArchivePeople", GetQueryString("SendUser"));
                    file.DataEntity.SetValue("ArchivePeopleName", depate.GetValue("SendUserName"));
                    file.DataEntity.SetValue("ArchiveDate", GetQueryString("SendDate"));
                    file.DataEntity.SetValue("ArchiveDepartment", depate.GetValue("DeptID"));
                    file.DataEntity.SetValue("ArchiveDepartmentName", depate.GetValue("DeptName"));
                }
                if (!String.IsNullOrEmpty(reorganizeDetail.GetValue("MainFile")))
                {
                    var attachment = new S_Attachment(node.Space.ID);
                    attachment.DataEntity.SetValue("MainFile", reorganizeDetail.GetValue("MainFile"));
                    attachment.DataEntity.SetValue("PDFFile", reorganizeDetail.GetValue("PDFFile"));
                    attachment.DataEntity.SetValue("PlotFile", reorganizeDetail.GetValue("PlotFile"));
                    attachment.DataEntity.SetValue("XrefFile", reorganizeDetail.GetValue("XrefFile"));
                    attachment.DataEntity.SetValue("DwfFile", reorganizeDetail.GetValue("DwfFile"));
                    attachment.DataEntity.SetValue("TiffFile", reorganizeDetail.GetValue("TiffFile"));
                    attachment.DataEntity.SetValue("SignPdfFile", reorganizeDetail.GetValue("SignPdfFile"));
                    file.AddAttachment(attachment);
                }
                file.IsValidateDataAttr = false;
                DocSystem.FileController.FileValidateDataAttr(file);
                //node.AddFile(file, true);
                file.Save(true);
                return file;
            }
            return null;
        }

        void SetFileAttr(S_FileInfo file, string Attr)
        {
            var dic = JsonHelper.ToObject(Attr);
            foreach (string key in dic.Keys)
            {
                if (key == "ID" || key == "NodeID" || key == "SpaceID" || key == "ConfigID" || key == "FullNodeID" || key == "State" || key == "BorrowState"
                    || key == "BorrowUserID" || key == "BorrowUserName") continue;
                file.DataEntity.SetValue(key, dic.GetValue(key));
            }
        }
        //GetFileList显示全部文件非整编文件
        public JsonResult GetFileList(string NodeIDs, string SpaceID)
        {
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID == SpaceID);
            SQLHelper sqlHelpFile = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            string sqlFile = @"select *,NodeID+'.'+ConfigID as ParentID,'File' NodeType from S_FileInfo with(nolock) where NodeID in ('{0}')";

            DataTable tableFile = sqlHelpFile.ExecuteDataTable(string.Format(sqlFile, NodeIDs.Replace(",", "','")));
            return Json(tableFile);
        }
        //在整编操作页面，修改签收登记下的归档目录选择
        public void saveRootNodes(string FullPathIDs, string names, string ReorganizeID, string spaceID)
        {
            S_R_Reorganize PhysicalReorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_Reorganize.FirstOrDefault(a => a.ID == ReorganizeID);
            string nodeIds = string.Empty;
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(spaceID));
            
            //得到选中节点的子节点id
            FullPathIDs = FullPathIDs.TrimEnd(',');
            string sqlNodeId = "select ID from S_NodeInfo where FullPathID like '" + FullPathIDs.Replace(",", "% or FullPathID like") + "%'";
            string nodeFullPathIDs = string.Empty;
            sqlNodeId = string.Format(sqlNodeId);
            SQLHelper helper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            DataRowCollection Rows = helper.ExecuteDataTable(sqlNodeId).Rows;
            string nodeChildrenId = string.Empty;
            foreach (DataRow row in Rows)
                nodeChildrenId += row["ID"] + ",";

            if (!string.IsNullOrEmpty(PhysicalReorganize.SelectNodeIDs))
                FullPathIDs += "," + PhysicalReorganize.SelectNodeIDs;
            if (!string.IsNullOrEmpty(nodeChildrenId))
                FullPathIDs += "," + nodeChildrenId.TrimEnd(',');
            List<string> fullPathNodeIDs = GetUniqFullPathIDs(FullPathIDs);

            //string defaultNodesIds = string.Empty;
            PhysicalReorganize.SelectNodeIDs = string.Join(",", fullPathNodeIDs);
            FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
        }
        //集合数据消除重复
        protected List<string> GetUniqFullPathIDs(string FullPathIDs)
        {

            List<string> array = FullPathIDs.Replace(',', '.').Split('.').ToList<string>();
            List<string> fullPathNodeIDs = new List<string>();
            for (var i = 0; i < array.Count; i++)
            {
                //如果当前数组的第i项在当前集合中第一次出现的位置是i，才存入集合；否则代表是重复的
                if (array.IndexOf(array[i]) == i)
                {
                    fullPathNodeIDs.Add(array[i]);
                }
            }
            return fullPathNodeIDs;
        }
        //签收单删除
        public JsonResult Deleted()
        {
            string[] IDs =  GetQueryString("IDs").Split(',');
            //SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            //string sqlCom = "select count(1) from S_R_PhysicalReorganize where ID in ('{0}') and State is not null and State!='Create'";
            //sqlCom = string.Format(sqlCom, IDs);
            //int count = Convert.ToInt32(sqlHelp.ExecuteScalar(sqlCom));
            //if (count > 0)
            //    throw new Formula.Exceptions.BusinessException("只可删除【已签收】的任务");
            //sqlCom = "delete from S_R_PhysicalReorganize where ID in ('{0}')";
            //sqlCom = string.Format(sqlCom, IDs);
            //sqlHelp.ExecuteNonQuery(sqlCom);
            List<S_R_Reorganize> reorganizes= FormulaHelper.GetEntities<DocConstEntities>().S_R_Reorganize.Where(a => IDs.Contains(a.ID)&&a.ReorganizeState=="Create").ToList();
            if(reorganizes.Count<=0)
                throw new Formula.Exceptions.BusinessException("只可删除【已签收】的任务");
            foreach (S_R_Reorganize item in reorganizes)
            {
                FormulaHelper.GetEntities<DocConstEntities>().S_R_Reorganize.Remove(item);
            }
            FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
            return Json("");
        }
        #endregion

        #region 获取枚举字符串
        private string GetEnumString(string connName, string tableName, string fieldCode, string data)
        {
            bool fromMeta = false;
            if (data.StartsWith("[") == false)
            {
                var arr = data.Split(',');
                if (arr.Length == 3) //如果data为ConnName,tableName,fieldCode时
                {
                    connName = arr[0];
                    tableName = arr[1];
                    fieldCode = arr[2];
                    fromMeta = true;
                }
                else if (arr.Length == 2)//如果data为tableName,fieldCode时
                {
                    tableName = arr[0];
                    fieldCode = arr[1];
                    fromMeta = true;
                }
            }

            string result = "";
            if (data.StartsWith("["))
            {
                var LGID = FormulaHelper.GetCurrentLGID();
                if (!string.IsNullOrEmpty(LGID))
                {
                    var enums = JsonHelper.ToObject<List<Dictionary<string, object>>>(data);
                    if (enums.Count > 0 && enums.Where(c => c.Keys.Contains("textEN")).Count() > 0)
                    {
                        foreach (var item in enums)
                        {
                            var text = item.GetValue("textEN");
                            item.SetValue("text", text);
                            item.Remove("textEN");
                        }
                        result = JsonHelper.ToJson(enums);
                    }
                    else
                        result = data;
                }
                else
                    result = data;
            }
            else if (data == "" || data == "FromMeta" || fromMeta == true)
            {
                var entities = FormulaHelper.GetEntities<BaseEntities>();
                var field = entities.Set<S_M_Field>().FirstOrDefault(c => c.Code == fieldCode && c.S_M_Table.Code == tableName && c.S_M_Table.ConnName == connName);
                if (field == null || string.IsNullOrEmpty(field.EnumKey))
                    result = string.Format("var enum_{0}_{1} = {2};", tableName, fieldCode, "[]");
                else if (field.EnumKey.Trim().StartsWith("["))
                    result = field.EnumKey;
                else
                {
                    IEnumService enumService = FormulaHelper.GetService<IEnumService>();
                    try
                    {
                        result = JsonHelper.ToJson(enumService.GetEnumTable(field.EnumKey));
                    }
                    catch (Exception) { }
                }
            }
            else
            {
                IEnumService enumService = FormulaHelper.GetService<IEnumService>();
                try
                {
                    result = JsonHelper.ToJson(enumService.GetEnumTable(data));
                }
                catch (Exception) { }
            }

            return result;
        }
        private string GetEnumKey(string connName, string tableName, string fieldCode, string data)
        {
            bool fromMeta = false;
            if (data.StartsWith("[") == false)
            {
                var arr = data.Split(',');
                if (arr.Length == 3) //如果data为ConnName,tableName,fieldCode时
                {
                    connName = arr[0];
                    tableName = arr[1];
                    fieldCode = arr[2];
                    fromMeta = true;
                }
                else if (arr.Length == 2)//如果data为tableName,fieldCode时
                {
                    tableName = arr[0];
                    fieldCode = arr[1];
                    fromMeta = true;
                }
            }

            string result = "";
            if (data.StartsWith("["))
                result = string.Format("enum_{0}_{1}", tableName, fieldCode);
            else if (data == "" || data == "FromMeta" || fromMeta == true)
            {
                result = string.Format("enum_{0}_{1}", tableName, fieldCode);
            }
            else
            {
                result = data.Split('.').Last();
            }

            return result;
        }
        #endregion
        #region 根据userID找到相关部门
        private Dictionary<string, string> GetDepartment(string userID)
        {
            var user = FormulaHelper.GetEntities<Base.Logic.Domain.BaseEntities>().S_A_User.FirstOrDefault(a => a.ID.Equals(userID));
            Dictionary<string, string> depart = new Dictionary<string, string>();
            depart.Add("DeptID", user.DeptID);
            depart.Add("DeptName", user.DeptName);
            depart.Add("SendUserName", user.Name);
            return depart;
        }
        #endregion
    }
}
