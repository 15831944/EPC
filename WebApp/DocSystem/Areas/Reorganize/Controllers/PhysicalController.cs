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
    public class PhysicalController : DocConstController
    {
        //
        // GET: /Reorganize/Physical/

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
                if (reorgConfig.PhysicalMainFormCode == null || reorgConfig.PhysicalMainFormCode == "")
                    continue;
                var dic = new Dictionary<string, string>();
                dic.SetValue("Name", space.Name);
                dic.SetValue("FileCount", fileCount);
                dic.SetValue("SpaceID", space.ID);
                dic.SetValue("SpaceKey", space.SpaceKey);
                dic.SetValue("PhysicalMainFormCode", reorgConfig.PhysicalMainFormCode);
                SpaceList.Add(dic);
            }
            ViewBag.SpaceList = SpaceList;
            ViewBag.SpaceStr = JsonHelper.ToJson(SpaceList);
            return View();
        }
        //实物整编SatePage
        public ActionResult PhysicalReorganizeTabs() {
            return View();
        }
        public JsonResult GetPhysicalMainFormList(QueryBuilder qb)
        {
            var sql = @"select * from S_R_PhysicalReorganize";
            var grid = this.SqlHelper.ExecuteGridData(sql, qb);
            return Json(grid);
        }
        #region S_R_PhysicalReorganize.Execute已存在判断
        //点击整理时验证是否符合整编
        public JsonResult CheckExecuteReorganize(string IDs, string SpaceID)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_PhysicalReorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            if (list.Any(a => a.State == "Execute"
                && a.ReorganizeUser != CurrentUserInfo.UserID))
            {
                var task = list.FirstOrDefault(a => a.State == "Execute"
                && a.ReorganizeUser != CurrentUserInfo.UserID);
                throw new Formula.Exceptions.BusinessException("【" + task.Name + "】已经由【" + task.ReorganizeUserName + "】整编，请等【" + task.ReorganizeUserName + "】整编完成以后再操作");
            }
            var space = FormulaHelper.GetEntities<DocConfigEntities>().Set<S_DOC_Space>().FirstOrDefault(a => a.ID == SpaceID);
            var Reorganize = space.S_DOC_ReorganizeConfig.FirstOrDefault(a => a.SpaceID == SpaceID).ToJson();
            return Json(Reorganize);
        }
        #endregion
        //整编状态更改为整编中
        public JsonResult ExecuteReorganize(string IDs, string SpaceID)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_PhysicalReorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
                item.Execute();
            this.entities.SaveChanges();
            return Json("");
        }

        //点击整编完成检测
        public JsonResult CheckFinishReorganize(string IDs, string ReorganizeType)
        {
            var rtn = false;
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_PhysicalReorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
            {
                item.CheckFinish();
                if (ReorganizeType.Equals("Volumn"))
                {
                    var VolumnDetailList = item.S_R_PhysicalReorganize_NodeDetail.ToList();
                    if (VolumnDetailList.Any(a => string.IsNullOrEmpty(a.ReorganizeFullID)))
                    {
                        rtn = true;
                        break;
                    }
                }
                else
                {
                    var detaillist = item.S_R_PhysicalReorganize_FileDetail.ToList();
                    if (detaillist.Any(a => string.IsNullOrEmpty(a.ReorganizeFullID)))
                    {
                        rtn = true;
                        break;
                    }
                }

            }
            if (rtn)
                throw new BusinessException("还有未整编的文件，禁止完成整编");
            return Json(new { rtn });
        }
        //整编完成功能
        public JsonResult FinishReorganize(string IDs)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_PhysicalReorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
                item.Finish();
            this.entities.SaveChanges();
            return Json("");
        }

        #endregion
         #region 归档案卷/文件目录
        protected List<string> PrintButton()
        {
            string SpaceID = GetQueryString("SpaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            //按钮根据配置展现
            var buttons = space.S_DOC_ReorganizeConfig.Select(a => a.PhysicalButtons);
            List<string> listButton = new List<string>();
            foreach (string item in buttons)
            {
                listButton.AddRange(item.Split(','));
            }
            return listButton;
        }
            #endregion
        #region 按件整编操作
        public ActionResult PhysicalReorganize(string tmplCode)
        {
            string SpaceID = GetQueryString("SpaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var ReorganizeConfig = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_ReorganizeConfig.FirstOrDefault(a => a.SpaceID == SpaceID);

            UIFO uiFO = FormulaHelper.CreateFO<UIFO>();
            ViewBag.ListHtml = uiFO.CreateListHtml(ReorganizeConfig.PhysicalMainListCode);
            ViewBag.Script = uiFO.CreateListScript(ReorganizeConfig.PhysicalMainListCode);
            ViewBag.FixedFields = string.Format("var FixedFields={0};", Newtonsoft.Json.JsonConvert.SerializeObject(uiFO.GetFixedWidthFields(ReorganizeConfig.PhysicalMainListCode)));
            #region 归档案卷/文件目录
            //按钮根据配置展现
            //var buttons = space.S_DOC_ReorganizeConfig.Select(a => a.PhysicalButtons);
            //List<string> listButton = new List<string>();
            //foreach (string item in buttons)
            //{
            //    listButton.AddRange(item.Split(','));
            //}
            ViewBag.PrintButton = PrintButton();
            #endregion
            ViewBag.GetFileConfigName = GetFileConfigName();//添加按钮表单选择
            return View();
        }
        //展示整编区数据
        public JsonResult GetList(QueryBuilder qb)
        {
            string PhysicalReorganizeID = GetQueryString("PhysicalReorganizeID");
            if (PhysicalReorganizeID == "")
                throw new BusinessException("签收单ID不存在");
            string sql = @"select *,'File' NodeType, '' ParentID from S_R_PhysicalReorganize_FileDetail with(nolock) where S_R_PhysicalReorganizeID='" + PhysicalReorganizeID + "'";
            var grid = this.SqlHelper.ExecuteGridData(sql, qb);
            //var grid = this.SqlHelper.ExecuteNonQuery(sql);
            return Json(grid);
        }
        //添加按钮表单选择
        public DataTable GetFileConfigName()
        {
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            var sql = @"SELECT FileID FileConfigID,Name,FormCode FROM S_DOC_ReorganizeConfig_FileFormRelation 
left join S_DOC_File on S_DOC_ReorganizeConfig_FileFormRelation.FileID=S_DOC_File.ID
where FormCode!='' and FormCode is not null and SpaceID='" + GetQueryString("SpaceID") + "' order by SortIndex asc";
            return sqlHelp.ExecuteDataTable(sql);
        }
        //按件整编页面,删除操作
        public JsonResult Delete()
        {
            string id = GetQueryString("IDs");
            string SpaceID = GetQueryString("SpaceID");
            var idAry = id.TrimEnd(',').Split(',');
            var list = entities.Set<S_R_PhysicalReorganize_FileDetail>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
                entities.Set<S_R_PhysicalReorganize_FileDetail>().Remove(item);

            var docSpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            string fileName = string.Empty;
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.ArchiveFileID))
                    fileName += item.Name + ",";

                //if (string.IsNullOrEmpty(item.ArchiveFileID))
                //    continue;
                //S_FileInfo fileInfo = new S_FileInfo(item.ArchiveFileID, docSpace);
                //fileInfo.DataEntity.DeleteDB(fileInfo.InstanceDB, "S_FileInfo", fileInfo.ID);
            }
            fileName = fileName.TrimEnd(',');
            if (!string.IsNullOrEmpty(fileName))
                throw new BusinessException("文件【" + fileName + "】已归档，不可删除");
            entities.SaveChanges();

            return Json("");
        }
        //暂存操作
        public JsonResult SaveTemporary(string IDs)
        {
            var idAry = IDs.Split(',');
            var list = this.entities.Set<S_R_PhysicalReorganize>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
            {
               if(item.State.ToString().Equals( DocSystem.Logic.PhysicalReorganizeState.Finish.ToString()))
                   continue;
               item.SaveTemporary();
            }
            this.entities.SaveChanges();
            return Json("");
        }
        //在整编操作页面，修改签收登记下的归档目录选择
        public void saveRootNodes(string FullPathIDs, string names, string PhysicalReorganizeID, string spaceID)
        {
            S_R_PhysicalReorganize PhysicalReorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.FirstOrDefault(a => a.ID == PhysicalReorganizeID);
            //string nodeIds = string.Empty;
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID.Equals(spaceID));
            //得到选中的节点id 
            //foreach (var nodeId in FullPathIDs.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            //{
            //    string[] nodeIdTemp = nodeId.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (nodeIdTemp.Length >= 1){
            //        if (!PhysicalReorganize.DefaultNodes.Contains(nodeIdTemp[nodeIdTemp.Length - 1]))//当选择编目节点不是表单归档目录选择时，添加选择的节点
            //            nodeIds += nodeIdTemp[nodeIdTemp.Length - 1] + ",";
            //    }
            //    else
            //        nodeIds += nodeId + ",";
            //}
            //得到选中节点的子节点id
            FullPathIDs = FullPathIDs.TrimEnd(',');
            string sqlNodeId = "select ID from S_NodeInfo where FullPathID like '" + FullPathIDs.Replace(",","% or FullPathID like")+ "%'";
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
            //PhysicalReorganize.DefaultNodes = !string.IsNullOrEmpty(PhysicalReorganize.DefaultNodes) ? PhysicalReorganize.DefaultNodes.TrimEnd(',') + "," + nodeIds : nodeIds;
            //PhysicalReorganize.DefaultNodesName = !string.IsNullOrEmpty(PhysicalReorganize.DefaultNodesName) ? PhysicalReorganize.DefaultNodesName.TrimEnd(',') + "," + names : names;
            FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
        }
        //集合数据消除重复
        protected List<string> GetUniqFullPathIDs(string FullPathIDs)
        {

            List<string> array = FullPathIDs.Replace(',','.').Split('.').ToList<string>();
            List<string> fullPathNodeIDs = new List<string>();
            for (var i = 0; i < array.Count; i++) {
                //如果当前数组的第i项在当前集合中第一次出现的位置是i，才存入集合；否则代表是重复的
                if (array.IndexOf(array[i]) == i) {
                    fullPathNodeIDs.Add(array[i]);
                }
            }
            return fullPathNodeIDs;
        }
        //整编操作页面右边树的数据
        public JsonResult GetNodeTreeList(QueryBuilder qb)
        {
            string SpaceID = GetQueryString("SpaceID");
            string PhysicalReorganizeID = GetQueryString("PhysicalReorganizeID");

            S_R_PhysicalReorganize PhysicalReorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.FirstOrDefault(a => a.ID.Equals(PhysicalReorganizeID));
            bool showAllFile = Convert.ToBoolean(string.IsNullOrEmpty(GetQueryString("showAllFile")) ? "false" : GetQueryString("showAllFile"));
            //string[] defaultNodes= PhysicalReorganize.DefaultNodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //bool isDefaultFullID=true;
            //foreach (var defaultNode in defaultNodes)//比较表单编目选择节点ID是否存在整编页面编目树列表中
            //{
            //    if (string.IsNullOrEmpty(PhysicalReorganize.SelectNodeIDs)) { isDefaultFullID = false; break; }
            //    if (!PhysicalReorganize.SelectNodeIDs.Contains(defaultNode))
            //    {
            //        isDefaultFullID = false;
            //        break;
            //    }
            //}
            if (!string.IsNullOrEmpty(PhysicalReorganize.SelectNodeIDs))
                return GetSelectedTreeList(PhysicalReorganize.SelectNodeIDs, SpaceID, "", showAllFile);
            else
                return GetSelectedTreeList("", SpaceID,PhysicalReorganize.DefaultNodes, showAllFile);

        }
        public JsonResult GetSelectedTreeList(string FullPathIDs, string SpaceID, string DefaultNodes, bool showAllFile = false, bool isSelect=false)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            string PhysicalReorganizeID = GetQueryString("PhysicalReorganizeID");
            SQLHelper PhysicalReorganizeHelp = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            //string idStrs = "FullPathID like '" + IDs.Replace(",", "%' or FullPathID like '") + "%' or ( '" + IDs.Replace(",", "' like FullPathID+'%' or '") + "' like FullPathID+'%')";//查出所有上层节点与自身及下层节点
            string idStrs = "id in {0}";
            if (!string.IsNullOrEmpty(DefaultNodes) && string.IsNullOrEmpty(FullPathIDs))
            {
                string sqlFullPathId = "select FullPathID,Name from S_NodeInfo where ID in ('{0}')";
                string nodeFullPathIDs = string.Empty;
                sqlFullPathId = string.Format(sqlFullPathId, DefaultNodes.Replace(",", "','"));
                SQLHelper helper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                DataRowCollection Rows = helper.ExecuteDataTable(sqlFullPathId).Rows;
                string nodeNames=string.Empty;
                foreach (DataRow row in Rows){
                    nodeFullPathIDs += row["FullPathID"]+",";
                }
                nodeFullPathIDs = nodeFullPathIDs.TrimEnd(',');
                idStrs = string.Format(idStrs, "('" + nodeFullPathIDs.Replace(",", ".").Replace(".", "','") + "') or FullPathID like '" + nodeFullPathIDs.Replace(",","%' or FullPathID like '") + "%'");
                //S_R_PhysicalReorganize PhysicalReorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.FirstOrDefault(a => a.ID == PhysicalReorganizeID);
                //PhysicalReorganize.SelectNodeIDs = "";
                //FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
                saveRootNodes(nodeFullPathIDs, nodeNames, PhysicalReorganizeID, SpaceID);//用于表单编目时进行一次存储
            }
            else if(isSelect)
            {
                string ReorganizeFullID = string.Join("','", GetUniqFullPathIDs(FullPathIDs));
                if (!string.IsNullOrEmpty(ReorganizeFullID))
                    idStrs = string.Format(idStrs,"('"+ReorganizeFullID+"') or FullPathID like '" + FullPathIDs.Replace(",","%' or FullPathID like '") + "%'");
                else
                    idStrs = "1=2";
            }
            else
            {
                string ReorganizeFullID = string.Join("','", GetUniqFullPathIDs(FullPathIDs));
                if (!string.IsNullOrEmpty(ReorganizeFullID))
                    idStrs = string.Format(idStrs,"('"+ReorganizeFullID+"')");
                else
                    idStrs = "1=2";//没有归档目录
            }
            string sql = @"select ID,ParentID,{0},FullPathID,ConfigID,'' as NodeType,'' HasFile,'' PhysicalType  from S_NodeInfo where (" + idStrs + ") and SpaceID='" + SpaceID + "' {1}";
            var treeConfig = space.S_DOC_TreeConfig.FirstOrDefault();
            if (treeConfig != null)
                sql = String.Format(sql, "Name", treeConfig.GetOrderByStr());
            else
                sql = String.Format(sql, "Name", "");
            var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            var dt = instanceDB.ExecuteDataTable(sql);
            var fileSql = "select ID,Name,Code,RelateID,NodeID as ParentID,FullNodeID+'.'+ID as FullPathID,ConfigID from S_FileInfo where NodeID in (select ID from (" + (treeConfig != null ? sql.Replace(treeConfig.GetOrderByStr(), "") : sql) + ") tmp)";
            var fileDt = instanceDB.ExecuteDataTable(fileSql);
            //节点是实物案卷可以关联文件
            var fileNodeIDs = space.S_DOC_Node.Where(a => a.S_DOC_FileNodeRelation.Count > 0 && a.IsPhysicalBox == "True").Select(a => a.ID).ToList();
            SQLHelper sqlhelpFile = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            StringBuilder fileID = new StringBuilder();
            var resultDt = dt.Clone();
            //追加已添加的文件
            var addFiles = entities.Set<S_R_PhysicalReorganize_FileDetail>().Where(a => !string.IsNullOrEmpty(a.ReorganizeFullID) && a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a => a).ToList();
            if (addFiles.Count > 0)
            {
                foreach (var fileRow in addFiles)
                {
                    var _row = resultDt.NewRow();
                    string[] fullIDList = fileRow.ReorganizeFullID.Split(new char[] { '.' });
                    _row["ID"] = fileRow.ID;
                    _row["Name"] = fileRow.Name.ToString() + "[" + fileRow.Code + "]";
                    _row["ParentID"] = fullIDList[fullIDList.Length - 1];
                    _row["FullPathID"] = fileRow.ReorganizeFullID;
                    _row["ConfigID"] = fileRow.FileConfigID;
                    _row["HasFile"] = 0;
                    _row["NodeType"] = "File";
                    resultDt.Rows.Add(_row);
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                string sqlFileID = @"select FileID from S_DOC_FileNodeRelation where NodeID in ('{0}')";
                var configID = row["ConfigID"].ToString();
                //用于判断实体文件是否可以拖放到该结点下
                fileID.Clear();
                sqlFileID = string.Format(sqlFileID, configID);

                DataRowCollection rowFiles = sqlhelpFile.ExecuteDataTable(sqlFileID).Rows;
                foreach (DataRow rowFile in rowFiles)
                {
                    fileID.Append(rowFile["FileID"]);
                    fileID.Append(',');
                }
                if (fileNodeIDs.Contains(configID))
                    row["HasFile"] = fileID.ToString().TrimEnd(',');
                if (FullPathIDs.IndexOf(row["ID"].ToString())==0)
                    row["NodeType"] = "Root";
                else
                    row["NodeType"] = "Child";
                //if (PhysicalReorganizeTable.Rows[0]["DefaultNodes"].ToString().IndexOf(row["ID"].ToString())==0)
                //    row["PhysicalType"] = "Physical";
                //else
                //    row["PhysicalType"] = "Other";
                resultDt.ImportRow(row); //追加文件结点
                if (fileNodeIDs.Contains(configID) && showAllFile)
                {
                    //追加父节点下的其他文件
                    var files = fileDt.Select("ParentID='" + row["ID"].ToString() + "'");
                    var addFileIDs = addFiles.Select(a => a.ArchiveFileID).ToList();
                    foreach (var fileRow in files)
                    {
                        if (addFileIDs.Contains(fileRow["ID"]))
                            continue;
                        string fID = fileRow["ID"].ToString();
                        string code = fileRow["code"] == null ? "" : fileRow["code"].ToString();
                        var _allRow = resultDt.NewRow();
                        _allRow["ID"] = fileRow["ID"];
                        _allRow["Name"] = fileRow["Name"].ToString() + "[" + code + "]";
                        _allRow["ParentID"] = fileRow["ParentID"];
                        _allRow["FullPathID"] = fileRow["FullPathID"];
                        _allRow["ConfigID"] = fileRow["ConfigID"];
                        _allRow["HasFile"] = 0;
                        _allRow["NodeType"] = "File";
                        resultDt.Rows.Add(_allRow);
                    }
                }

            }
            return Json(resultDt, JsonRequestBehavior.AllowGet);
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
        //重置归档目录
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
                //fileInfo.DataEntity.DeleteDB(fileInfo.InstanceDB, "S_FileInfo", fileInfo.ID);
                item.SetValue("ReorganizePath", "");
                item.SetValue("ReorganizeFullID", "");
                item.SetValue("ArchiveFileID", "");
                //item.SetValue("ArchiveFileAttrs", "");
                var sql = "update S_R_PhysicalReorganize_FileDetail set ReorganizePath='',ReorganizeFullID='',ArchiveFileID='',YesOrNo='否' where ID='{0}'";
                sql = string.Format(sql, item.GetValue("ID"));
                sb.AppendLine(sql);
            }
            if (sb.Length > 0)
                this.SqlHelper.ExecuteNonQuery(sb.ToString());
            return Json(list);
        }
        //移除实物整编结点
        public JsonResult DeleteReorganizeNode(string nodeID, string FileDetailID, string PhysicalReorganizeID)
        {
            string spaceID = GetQueryString("SpaceID");
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID == spaceID);
            List<S_R_PhysicalReorganize_FileDetail> fileDetailList = entities.Set<S_R_PhysicalReorganize_FileDetail>().Where(a => a.S_R_PhysicalReorganizeID == PhysicalReorganizeID && a.ReorganizeFullID.Contains(nodeID)).ToList();
            if (fileDetailList.Count > 0)
                throw new Formula.Exceptions.BusinessException("有归档文件，不可移除");
            S_R_PhysicalReorganize PhysicalData = entities.Set<S_R_PhysicalReorganize>().FirstOrDefault(a => a.ID == PhysicalReorganizeID);
            List<string> nodeIDs = PhysicalData.SelectNodeIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();//全部路径ID
            string fullIDSql = "select FullPathID from S_NodeInfo where ID='" + nodeID + "'";
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            string fullID=sqlHelper.ExecuteScalar(fullIDSql).ToString();//得到移除节点的全路径
            string sql = @"select ID from S_NodeInfo where FullPathID like '" + fullID + "%'";//得到移除节点及子节点ID
            DataRowCollection rows= sqlHelper.ExecuteDataTable(sql).Rows;
            List<string> listIDs = new List<string>();
            foreach (DataRow row in rows)
                listIDs.Add(row["ID"].ToString());
            nodeIDs.RemoveWhere(a => listIDs.Contains(a.ToString()));
            //表单选择编目不移除
            //List<string> defaultNodeids = PhysicalData.DefaultNodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            //List<string> defaultNodeNames = PhysicalData.DefaultNodesName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            //string fullPathID = string.Empty;
            //foreach (var nodeid in defaultNodeids)
            //{
            //    if (listIDs.Contains(nodeid))
            //    {
            //        defaultNodeNames.RemoveAt(defaultNodeids.IndexOf(nodeid));
            //    }
            //}
            //defaultNodeids.RemoveWhere(a => listIDs.Contains(a.ToString()));
            
            //PhysicalData.DefaultNodes = string.Join(",", defaultNodeids);
            //PhysicalData.DefaultNodesName = string.Join(",", defaultNodeNames);

            PhysicalData.SelectNodeIDs = string.Join(",", nodeIDs);
            entities.SaveChanges();
            return Json("");
        }
        public JsonResult ArchiveReorganize(string FileList, string SpaceID, string TargetNodeID)
        {
            //直接归档

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
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
                    S_FileInfo fileInfo = archFile(item, node);
                    archFileID = fileInfo.ID;
                    item.SetValue("ArchiveFileID", archFileID);
                    var sql = "update S_R_PhysicalReorganize_FileDetail set ReorganizePath='{1}',ReorganizeFullID='{2}',ArchiveFileID='{3}' where id='{0}'";
                    sql = string.Format(sql, item.GetValue("ID"), item.GetValue("ReorganizePath"), item.GetValue("ReorganizeFullID"), archFileID);
                    sb.AppendLine(sql);
                }
                else
                {
                    //移动文件
                    S_FileInfo fileInfo = new S_FileInfo(archFileID, docSpace);
                    fileInfo.MoveTo(TargetNodeID);
                }
            }
            if (sb.Length > 0)
                this.SqlHelper.ExecuteNonQuery(sb.ToString());

            //sw.Stop();
            //throw new Formula.Exceptions.BusinessException(sw.ElapsedMilliseconds.ToString());
            return Json(list);
        }

        private S_FileInfo archFile(Dictionary<string, object> reorganizeDetail, S_NodeInfo node)
        {
            var fileConfig = node.ConfigInfo.S_DOC_FileNodeRelation.FirstOrDefault(a => a.FileID == reorganizeDetail.GetValue("FileConfigID"));
            if (fileConfig != null && fileConfig.S_DOC_File != null)
            {
                var file = new DocSystem.Logic.Domain.S_FileInfo(node.Space.ID, fileConfig.S_DOC_File.ID);
                var fileFields = fileConfig.S_DOC_File.S_DOC_FileAttr.Select(a => a.FileAttrField).ToList();
                var buttonEditFields = fileConfig.S_DOC_File.S_DOC_FileAttr.Where(a => a.InputType.IndexOf(ControlType.ButtonEdit.ToString()) > -1).ToList();
                foreach (var item in buttonEditFields)
                    fileFields.Add(item.FileAttrField + "Name");

                foreach (var fileds in fileFields)
                {
                    if (fileds.ToLower() == "id"||fileds.ToLower() == "storagenum")
                        continue;
                    var _value = reorganizeDetail.GetValue(fileds);
                    if (fileds.ToLower().Equals("quantity"))
                        file.DataEntity.SetValue("StorageNum", _value);
                    if (!string.IsNullOrEmpty(_value))
                        file.DataEntity.SetValue(fileds, _value);
                }
                //file.DataEntity.SetValue("RelateID", reorganizeDetail.GetValue("ID"));
                file.DataEntity.SetValue("NodeID", node.ID);
                file.DataEntity.SetValue("FullNodeID", node.FullPathID);
                file.DataEntity.SetValue("FullNodeName", node.FullPathName);
                file.DataEntity.SetValue("State", DocState.Normal);
                file.IsValidateDataAttr = false;
                DocSystem.FileController.FileValidateDataAttr(file);
                file.Save(true);
                return file;
            }

            return null;
        }
        //签收单删除
        public JsonResult Deleted()
        {
            string[] IDs = GetQueryString("IDs").Split(',');
            List<S_R_PhysicalReorganize> reorganizes = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.Where(a => IDs.Contains(a.ID) && a.State == "Create").ToList();
            if (reorganizes.Count <= 0)
                throw new Formula.Exceptions.BusinessException("只可删除【已签收】的任务");
            foreach (S_R_PhysicalReorganize item in reorganizes)
            {
                FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.Remove(item);
            }
            FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
            return Json("");
        }
        #region 归档案卷/文件目录
        //获取已有归档文件的归档目录
        public JsonResult NodeInfo(string PhysicalReorganizeID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(GetQueryString("SpaceID"));
            string sqlNode = "select * from S_NodeInfo where ConfigID in ('{0}') and FullPathID in ('{1}')";
            string fullPathID = string.Empty;
            string nodeID = string.Empty;
            string sqlFile = "select ReorganizeFullID PhysicalReorganizeID from S_R_PhysicalReorganize_FileDetail where S_R_PhysicalReorganizeID='{0}' and ReorganizeFullID is not null and ReorganizeFullID!=''";
            DataTable nodeInfoTable = new DataTable();
            SQLHelper nodeSqlHelper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            SQLHelper fileDetSql = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            DataRowCollection rows = fileDetSql.ExecuteDataTable(string.Format(sqlFile, PhysicalReorganizeID)).Rows;//得到归档文件全路径ID
            foreach (DataRow row in rows)
            {
                fullPathID += row[0] + "','";
            }
            fullPathID = fullPathID.TrimEnd(new char[] { '\'', ',', '\'' });
            foreach (var item in FileNodes())
            {
                nodeID += item.ID + "','";
            }
            nodeID = nodeID.TrimEnd(new char[] { '\'', ',', '\'' });
            nodeInfoTable = nodeSqlHelper.ExecuteDataTable(string.Format(sqlNode, nodeID, fullPathID));
            DataColumn PhyReID_Column = new DataColumn();
            PhyReID_Column.DataType = System.Type.GetType("System.String");//该列的数据类型 
            PhyReID_Column.ColumnName = "PhysicalReorganizeID";//该列得名称 
            PhyReID_Column.DefaultValue = PhysicalReorganizeID;//该列得默认值 
            nodeInfoTable.Columns.Add(PhyReID_Column);
            return Json(nodeInfoTable);
        }
        //获取归档案卷/文件目录字段用于分组
        public JsonResult GetGroupField()
        {
            return Json(FileNodes());
        }
        //获取归档的文件
        public JsonResult FileDetial(string ReorganizeFullID, string PhysicalReorganizeID)
        {
            string sqlFile = "select * from S_R_PhysicalReorganize_FileDetail where ReorganizeFullID in ('{0}') and S_R_PhysicalReorganizeID='{1}'";
            SQLHelper fileDetSql = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            DataTable table = fileDetSql.ExecuteDataTable(string.Format(sqlFile, ReorganizeFullID, PhysicalReorganizeID));
            return Json(table);
        }
        //node File节点组合
        public JsonResult NodeFileDetial(string PhysicalReorganizeID)
        {
            string spaceID=GetQueryString("SpaceID");
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a=>a.ID==spaceID);
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(space.DbName,space.ConnectString);
            var archiveFileIDs = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize_FileDetail
                .Where(a => a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a => a.ArchiveFileID).ToList();
            string fileInfoIDs = string.Join("','", archiveFileIDs);
            string sqlFileInfo = @"select *,NodeID ParentID,'' DocumentCode,
                    '' StorageRoomName,'' RackNumberName,'' PhysicalCount from S_FileInfo with(nolock) where ID in ('{0}')";
            DataTable fileTable= sqlHelp.ExecuteDataTable(string.Format(sqlFileInfo, fileInfoIDs));//整编文件
            var resultDt = fileTable.Copy();

            var fullNodeIDs = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize_FileDetail
                .Where(a => a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a => a.ReorganizeFullID).ToList();
            string nodeInfoIDs = string.Join(".", fullNodeIDs);

            nodeInfoIDs=nodeInfoIDs.Replace(".", "','");
            string sqlNodeInfo = "select * from S_NodeInfo with(nolock) where ID in ('{0}')";//整编节点
            DataTable nodeTable = sqlHelp.ExecuteDataTable(string.Format(sqlNodeInfo, nodeInfoIDs));
            foreach (DataRow nodeRow in nodeTable.Rows)
            {
                var _row = resultDt.NewRow();
                foreach (DataColumn columnName in fileTable.Columns)
                {
                    if (!nodeTable.Columns.Contains(columnName.ColumnName))
                        continue;
                    _row[columnName.ColumnName] = nodeRow[columnName.ColumnName];
                }
                resultDt.Rows.Add(_row);
            }
            return Json(resultDt);
        }
        //归档文件列表
        public JsonResult FileList(string PhysicalReorganizeID)
        {
            string sqlFile = @"select * from S_R_PhysicalReorganize_FileDetail  filedetail with(nolock)
                left join 
                (select FullPathID,FullPathName from EPM_DesignDocument..S_NodeInfo) nodeinfo
                on filedetail.ReorganizeFullID=nodeinfo.FullPathID where S_R_PhysicalReorganizeID='{0}' and ArchiveFileID is not null and ArchiveFileID!=''";
            SQLHelper fileDetSql = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            DataTable table = fileDetSql.ExecuteDataTable(string.Format(sqlFile, PhysicalReorganizeID));
            return Json(table);
        }
        //volumnNodeTreeDetil
        public JsonResult volumnNodeTreeDetil(string PhysicalReorganizeID)
        {
            string spaceID = GetQueryString("SpaceID");
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID == spaceID);
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(space.DbName, space.ConnectString);
            var fullVolumnNodeIDs = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize_NodeDetail
                .Where(a => a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a=>a.ReorganizeFullID).ToList();
            string nodeInfoIDs = string.Join(".", fullVolumnNodeIDs);

            nodeInfoIDs = nodeInfoIDs.Replace(".", "','");
            var ArchiveVolumnIDs = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize_NodeDetail
                .Where(a => a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a => a.ArchiveVolumnID).ToList();
            string archiveVolumnID = string.Join(".", ArchiveVolumnIDs);
            archiveVolumnID = archiveVolumnID.Replace(".", "','");
            nodeInfoIDs +="','"+archiveVolumnID;

            string sql = @"select * from S_NodeInfo where ID in ('{0}')";
            DataTable nodeTable = sqlHelp.ExecuteDataTable(string.Format(sql, nodeInfoIDs));

            return Json(nodeTable);
        }
        //整编卷册节点
        public JsonResult ReorganizeVolumnNodeList(string PhysicalReorganizeID)
        {
            string spaceID = GetQueryString("SpaceID");
            var space = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_Space.FirstOrDefault(a => a.ID == spaceID);
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(space.DbName, space.ConnectString);
            var ArchiveVolumnIDs = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize_NodeDetail
                .Where(a => a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a => a.ArchiveVolumnID).ToList();
            string VolumnNodeInfoIDs = string.Join("','", ArchiveVolumnIDs);
            string sql = @"select * from S_NodeInfo where ID in ('{0}')";
            DataTable reorganizeNodeTable = sqlHelp.ExecuteDataTable(string.Format(sql, VolumnNodeInfoIDs));
            //DataColumn PhyReID_Column = new DataColumn();
            //PhyReID_Column.DataType = System.Type.GetType("System.String");//该列的数据类型 
            //PhyReID_Column.ColumnName = "PhysicalVolumnReorganizeID";//该列得名称 
            //PhyReID_Column.DefaultValue = PhysicalReorganizeID;//该列得默认值 
            //reorganizeNodeTable.Columns.Add(PhyReID_Column);
            return Json(reorganizeNodeTable);
        }
        public List<S_DOC_Node> FileNodes()
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(GetQueryString("SpaceID"));
            //节点都能关联文件
            var fileNode = space.S_DOC_Node.Where(a => a.S_DOC_FileNodeRelation.Count > 0 && a.IsPhysicalBox == "True").Select(a => a).ToList();
            //if (fileNode.Count > 1)
            //    throw new BusinessException("卷册有且只有一个");
            return fileNode;
        }
        #endregion
        public JsonResult SmartReminder(string key)
        {
            var SpaceID = GetQueryString("SpaceID");
            var ConfigID = GetQueryString("ConfigID");
            var NodeIDs = GetQueryString("NodeIDs");
            string nodeWhereStr = string.Empty;
            NodeIDs = NodeIDs.TrimEnd(',');
            foreach (var item in NodeIDs.Split(','))
                nodeWhereStr += " or FullNodeID like '%" + item + "%'";
            if (!string.IsNullOrEmpty(nodeWhereStr))
                nodeWhereStr = " and (" + nodeWhereStr.TrimStart(' ', 'o', 'r') + ")";
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            string sql = "select * from S_FileInfo where Code like '%" + key + "%' and ConfigID='" + ConfigID + "' " + nodeWhereStr;
            DataTable data = instanceDB.ExecuteDataTable(sql);

            return Json(data);
        }
        #endregion
        /***********************************************************分割线******************************************************/
        #region 卷册整编操作
        public ActionResult PhysicalVolumnReorganize()
        {
            //卷册配置
            ViewBag.GetNodeConfigName = GetNodeConfigName();
            //卷册配置列表
            string SpaceID = GetQueryString("SpaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var ReorganizeConfig = FormulaHelper.GetEntities<DocConfigEntities>().S_DOC_ReorganizeConfig.FirstOrDefault(a => a.SpaceID == SpaceID);
            //归档案卷/文件目录打印按钮
            ViewBag.PrintButton = PrintButton();
            UIFO uiFO = FormulaHelper.CreateFO<UIFO>();
            ViewBag.VolumnListHtml = uiFO.CreateListHtml(ReorganizeConfig.PhysicalVolumnListCode);
            ViewBag.VolumnScript = uiFO.CreateListScript(ReorganizeConfig.PhysicalVolumnListCode);
            ViewBag.VolumnFixedFields = string.Format("var FixedFields={0};", Newtonsoft.Json.JsonConvert.SerializeObject(uiFO.GetFixedWidthFields(ReorganizeConfig.PhysicalVolumnListCode)));
            return View();
        }
        //添加按卷整编按钮表单选择
        public DataTable GetNodeConfigName()
        {
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            var sql = @"SELECT NodeID NodeConfigID,Name,FormCode FROM S_DOC_ReorganizeConfig_NodeFormRelation 
                      left join S_DOC_Node on S_DOC_ReorganizeConfig_NodeFormRelation.NodeID=S_DOC_Node.ID
                      where FormCode!='' and FormCode is not null and SpaceID='" + GetQueryString("SpaceID") + "' and IsPhysicalBox='True' order by SortIndex asc";
            return sqlHelp.ExecuteDataTable(sql);
        }
        //展示卷册整编区数据
        public JsonResult GetVolumnList(QueryBuilder qb)
        {
            string PhysicalReorganizeID = GetQueryString("PhysicalReorganizeID");
            if (PhysicalReorganizeID == "")
                throw new BusinessException("签收单ID不存在");
            string sql = @"select *,'' ParentID from S_R_PhysicalReorganize_NodeDetail with(nolock) where S_R_PhysicalReorganizeID='" + PhysicalReorganizeID + "'";
            var grid = this.SqlHelper.ExecuteGridData(sql, qb);
            //var grid1 = this.SqlHelper.ExecuteDataTable(sql);
            return Json(grid.data);
        }
        //GetVolumnNodeTreeList卷册整编下的右边树
        public JsonResult GetVolumnTreeList(QueryBuilder qb)
        {
            string SpaceID = GetQueryString("SpaceID");
            string PhysicalReorganizeID = GetQueryString("PhysicalReorganizeID");

            S_R_PhysicalReorganize PhysicalReorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.FirstOrDefault(a => a.ID.Equals(PhysicalReorganizeID));
            bool showAllFile = Convert.ToBoolean(string.IsNullOrEmpty(GetQueryString("showAllFile")) ? "false" : GetQueryString("showAllFile"));
            //string[] defaultNodes = PhysicalReorganize.DefaultNodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //bool isDefaultFullID = true;
            //foreach (var defaultNode in defaultNodes)
            //{
            //    if (string.IsNullOrEmpty(PhysicalReorganize.SelectNodeIDs)) { isDefaultFullID = false; break; }
            //    if (!PhysicalReorganize.SelectNodeIDs.Contains(defaultNode))//判断签收单，选择编目节点在FullPathID中是否存在，不存在对fullpathid数据进行更新
            //    {
            //        isDefaultFullID = false;
            //        break;
            //    }
            //}
            if (!string.IsNullOrEmpty(PhysicalReorganize.SelectNodeIDs))
                return GetSelectedVolumnTreeList(PhysicalReorganize.SelectNodeIDs, SpaceID, PhysicalReorganize.DefaultNodes);
            else
                return GetSelectedVolumnTreeList("", SpaceID, PhysicalReorganize.DefaultNodes);
        }

        public JsonResult GetSelectedVolumnTreeList(string FullPathIDs, string SpaceID, string DefaultNodes,bool isSelect=false)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            string PhysicalReorganizeID = GetQueryString("PhysicalReorganizeID");
            string PhysicalReorganizeSql = @"select DefaultNodes from S_R_PhysicalReorganize where ID='" + PhysicalReorganizeID + "'";
            SQLHelper PhysicalReorganizeHelp = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            DataTable PhysicalReorganizeTable = PhysicalReorganizeHelp.ExecuteDataTable(PhysicalReorganizeSql);//用于区分哪些是实物归档下添加的结点,用于结点的移除
            if (space == null)
                throw new BusinessException("未指定档案实体空间，无法获取数据访问对象");
            //string idStrs = "FullPathID like '" + IDs.Replace(",", "%' or FullPathID like '") + "%' or ( '" + IDs.Replace(",", "' like FullPathID+'%' or '") + "' like FullPathID+'%')";//查出所有上层节点与自身及下层节点
            string idStrs = "id in {0}";
            if (!string.IsNullOrEmpty(DefaultNodes) && string.IsNullOrEmpty(FullPathIDs))
            {
                string sqlFullPathId = "select FullPathID,Name from S_NodeInfo where ID in ('{0}')";
                string nodeFullPathIDs = string.Empty;
                sqlFullPathId = string.Format(sqlFullPathId, DefaultNodes.Replace(",", "','"));
                SQLHelper helper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
                DataRowCollection Rows = helper.ExecuteDataTable(sqlFullPathId).Rows;
                string nodeNames = string.Empty;
                foreach (DataRow row in Rows)
                {
                    nodeFullPathIDs += row["FullPathID"] + ",";
                }
                nodeFullPathIDs = nodeFullPathIDs.TrimEnd(',');
                idStrs = string.Format(idStrs, "('" + nodeFullPathIDs.Replace(",", ".").Replace(".", "','") + "') or FullPathID like '" + nodeFullPathIDs.Replace(",", "%' or FullPathID like '") + "%'");
                //S_R_PhysicalReorganize PhysicalReorganize = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize.FirstOrDefault(a => a.ID == PhysicalReorganizeID);
                //PhysicalReorganize.SelectNodeIDs = "";
                //FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
                saveRootNodes(nodeFullPathIDs, nodeNames, PhysicalReorganizeID,SpaceID);
            }else if(isSelect){
                string ReorganizeFullID = string.Join("','", GetUniqFullPathIDs(FullPathIDs));
                if (!string.IsNullOrEmpty(ReorganizeFullID))
                    idStrs = string.Format(idStrs, "('" + ReorganizeFullID + "') or FullPathID like '" + FullPathIDs.Replace(",", "%' or FullPathID like '") + "%'");
                else
                    idStrs = "1=2";
            }
            else
            {
                string ReorganizeFullID = string.Join("','", GetUniqFullPathIDs(FullPathIDs));
                if (!string.IsNullOrEmpty(ReorganizeFullID))
                    idStrs = string.Format(idStrs, "('" + ReorganizeFullID + "')");
                else
                    idStrs = "1=2";
            }
            string sql = @"select ID,Quantity,RelateID,ParentID,{0},FullPathID,ConfigID,'' as NodeType,'' HasVolumn,'' PhysicalType,'' VolumnType  from S_NodeInfo where (" + idStrs + ") and SpaceID='" + SpaceID + "' {1}";
            var treeConfig = space.S_DOC_TreeConfig.FirstOrDefault();
            DocConstEntities entities = FormulaHelper.GetEntities<DocConstEntities>();
            var NodeDetailIDs = entities.Set<S_R_PhysicalReorganize_NodeDetail>().Select(a => a.ArchiveVolumnID).ToList();
            if (treeConfig != null)
                sql = String.Format(sql, "Name", treeConfig.GetOrderByStr());
            else
                sql = String.Format(sql, "Name", "");
            var instanceDB = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
            var dt = instanceDB.ExecuteDataTable(sql);
            //节点不是自由结点的可以关联卷册
            var NodeIDs = space.S_DOC_Node.Where(a => a.IsFreeNode == "False" && a.IsPhysicalBox == "True").Select(a => a.ID).ToList();
            SQLHelper sqlhelpFile = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            StringBuilder NodeID = new StringBuilder();
            var resultDt = dt.Clone();
            //追加已添加的卷册
            var addNodes = entities.Set<S_R_PhysicalReorganize_NodeDetail>().Where(a => !string.IsNullOrEmpty(a.ReorganizeFullID) && a.S_R_PhysicalReorganizeID == PhysicalReorganizeID).Select(a => a).ToList();
            if (addNodes.Count > 0)
            {
                foreach (var addNode in addNodes)
                {
                    var _row = resultDt.NewRow();
                    string[] nodeFullIDs = addNode.ReorganizeFullID.Split(new char[] { '.' }).ToArray();
                    _row["ID"] = addNode.ID;
                    _row["Name"] = addNode.Name.ToString() + "[" + addNode.DocumentCode + "]";
                    _row["ParentID"] = nodeFullIDs[nodeFullIDs.Length-1];
                    _row["FullPathID"] = addNode.ReorganizeFullID+"."+addNode.ID;
                    _row["ConfigID"] = addNode.NodeConfID;
                    _row["NodeType"] = "Volumn";
                    resultDt.Rows.Add(_row);
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                var configID = row["ConfigID"].ToString();
                if (!NodeDetailIDs.Contains(row["ID"].ToString()))
                {
                    var nodeStructIDs = space.S_DOC_NodeStrcut.Where(a => a.NodeID == configID).Select(a => a.ID).ToList();
                    var nodeStructNodeIDs = space.S_DOC_NodeStrcut.Where(a => nodeStructIDs.Contains(a.ParentID)).Select(a => a.NodeID).ToList();
                    if (NodeIDs.Contains(configID) && string.IsNullOrEmpty(row["Quantity"].ToString())) //判断添加的结点是否在结构定义中配置//Quantity份数为null时可以添加实物
                        row["HasVolumn"] = string.Join(",", nodeStructNodeIDs);
                    if (FullPathIDs.Split(',').Contains(row["ID"].ToString()))
                        row["NodeType"] = "Root";
                    else
                        row["NodeType"] = "Child";
                    //if (PhysicalReorganizeTable.Rows[0]["DefaultNodes"].ToString().Contains(row["ID"].ToString()))
                    //    row["PhysicalType"] = "Physical";  //用于根节点的移除
                    //else
                    //    row["PhysicalType"] = "Other";
                    //用于判断节点是否时归档的按卷
                    //row["VolumnType"]="Reorganize";
                    resultDt.ImportRow(row);
                }
            }
            return Json(resultDt, JsonRequestBehavior.AllowGet);
        }
        ////显示全部卷册
        //public JsonResult GetAllVolumn(string nodeTreeIDs, string volumnIDs,string spaceID)
        //{
        //    var entities=FormulaHelper.GetEntities<DocConfigEntities>();
        //    var space = entities.S_DOC_Space.FirstOrDefault(a => a.ID.Equals(spaceID));
        //    SQLHelper helper = SQLHelper.CreateSqlHelper(space.SpaceKey, space.ConnectString);
        //    string sql = @"select * from S_NodeInfo where ParentID in ('{0}')";//得到右边树的子节点的configID
        //    sql = string.Format(sql, nodeTreeIDs.TrimEnd(',').Replace(",", "','"));
        //    DataTable data = helper.ExecuteDataTable(sql);
        //    List<string> configList = new List<string>();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        if (!volumnIDs.Split(',').Contains(row["ID"]) && !nodeTreeIDs.Split(',').Contains(row["ID"]))
        //            configList.Add(row["ConfigID"].ToString());
        //    }

        //    List<string> nodeIDs = entities.S_DOC_Node.Where(b => configList.Contains(b.ID)&&b.Name.Equals("卷册")).Select(c=>c.ID).ToList();//得到的节点为卷册的ID
        //    DataTable result=data.Copy();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        if (nodeIDs.Contains(row["ConfigID"]))
        //            result.Rows.Add(row.ItemArray);
        //    }
        //    return Json(result);
        
        //}
        //卷册归档
        public JsonResult VolumnReorganize(string VolumnList, string SpaceID, string TargetNodeID)
        {
            //直接归档
            var docSpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            S_NodeInfo node = S_NodeInfo.GetNode(TargetNodeID, SpaceID);

            var list = JsonHelper.ToList(VolumnList);
            StringBuilder sb = new StringBuilder();
            SQLHelper sqlHelpNode = SQLHelper.CreateSqlHelper(docSpace.DbName, docSpace.ConnectString);
            StringBuilder nodeCodes = new StringBuilder();
            foreach (var item in list)
                nodeCodes.Append(item.GetValue("Code") + "','");
            nodeCodes.Remove(nodeCodes.Length - 3, 3);
            string sqlNode = "select * from S_NodeInfo where Code in ('{0}')";
            DataTable tableNodes = sqlHelpNode.ExecuteDataTable(string.Format(sqlNode, nodeCodes)); //找到卷册号存在的卷册
            foreach (var item in list)
            {
                DataRow[] tableFinds = tableNodes.Select("Code='" + item.GetValue("Code") + "'"); //查找出添加的卷册已有的卷册，进行条件过滤
                var volumnNodeID = item.GetValue("ArchiveVolumnID");
                if (string.IsNullOrEmpty(volumnNodeID) && tableFinds.Length <= 0)
                {
                    //新增档案卷册记录
                    S_NodeInfo volumnInfo = archVolumn(item, node);
                    volumnNodeID = volumnInfo.ID;
                    item.SetValue("VolumnNodeID", volumnNodeID);
                }
                else
                {
                    //移动文件
                    S_NodeInfo nodeInfo = null;
                    if (!string.IsNullOrEmpty(volumnNodeID))
                        throw new BusinessException("有归档卷册，禁止移动!");
                        //nodeInfo = new S_NodeInfo(volumnNodeID, docSpace); //添加过的卷册的移动的卷册
                    else if (tableFinds.Length > 0)
                    {
                        nodeInfo = new S_NodeInfo(tableFinds[0]["ID"].ToString(), docSpace); //存在的电子卷册
                        volumnNodeID = nodeInfo.ID;
                    }
                    nodeInfo.MoveTo(TargetNodeID);
                }
                var sql = "update S_R_PhysicalReorganize_NodeDetail set ReorganizePath='{1}',ReorganizeFullID='{2}',ArchiveVolumnID='{3}' where id='{0}'";
                sql = string.Format(sql, item.GetValue("ID"), item.GetValue("ReorganizePath"), item.GetValue("ReorganizeFullID"), volumnNodeID);
                sb.AppendLine(sql);
            }
            if (sb.Length > 0)
                this.SqlHelper.ExecuteNonQuery(sb.ToString());
            return Json(list);
        }
        //添加卷册结点
        private S_NodeInfo archVolumn(Dictionary<string, object> reorganizeDetail, S_NodeInfo node)
        {
            //var nodeConfig = node.ConfigInfo.S_DOC_FileNodeRelation.FirstOrDefault(a => a.NodeID == reorganizeDetail.GetValue("NodeConfigID"));
            var nodeConfigID = reorganizeDetail.GetValue("NodeConfID");
            var nodeConfig = FormulaHelper.GetEntities<DocConfigEntities>().Set<S_DOC_Node>().FirstOrDefault(a => a.ID == nodeConfigID);
            if (nodeConfig != null)
            {
                var volumn = new DocSystem.Logic.Domain.S_NodeInfo(node.Space.ID, nodeConfig.ID);
                var nodeFields = nodeConfig.S_DOC_NodeAttr.Select(a => a.AttrField).ToList();
                var buttonEditVolumnFields = nodeConfig.S_DOC_NodeAttr.Where(a => a.InputType.IndexOf(ControlType.ButtonEdit.ToString()) > -1).ToList();
                foreach (var item in buttonEditVolumnFields)
                    nodeFields.Add(item.AttrField + "Name");
                foreach (var nodesField in nodeFields)
                {
                    if (nodesField.ToLower() == "id"||nodesField.ToLower() == "storagenum")
                        continue;
                    var _value = reorganizeDetail.GetValue(nodesField);
                    if (nodesField.ToLower().Equals("quantity"))
                        volumn.DataEntity.SetValue("StorageNum", _value);
                    if (!string.IsNullOrEmpty(_value))
                        volumn.DataEntity.SetValue(nodesField, _value);
                }
                volumn.DataEntity.SetValue("RelateID", reorganizeDetail.GetValue("ID"));
                volumn.IsValidateDataAttr = false;
                node.AddChild(volumn, true);
                return volumn;
            }
            return null;
        }
        //重置归档卷册及移除
        public JsonResult DeleteReorganizeVolumn(string VolumnList, string SpaceID)
        {
            var docSpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            var list = JsonHelper.ToList(VolumnList);
            List<string> archiveVolumnIDs = new List<string>();
            //判断移除结点是否也属于电子卷册，是否含有电子文件及实物文件，有的话禁止移除
            foreach (var item in list)
                archiveVolumnIDs.Add(item.GetValue("ArchiveVolumnID").ToString());
            string listIDs = string.Join(",", archiveVolumnIDs);
            var whereStr = listIDs.Replace(",", "%' or FullPathID like '");
            var sqlCheck = @"select 1 from S_NodeInfo with(nolock) where State='Published' and (FullPathID like '%" + whereStr + "%')";
            var dt = SQLHelper.CreateSqlHelper(docSpace.DbName, docSpace.ConnectString).ExecuteDataTable(sqlCheck);
            if (dt.Rows.Count > 0)
                throw new Formula.Exceptions.BusinessValidationException("存在节点已发布，不能删除！");
            var sqlCheckChild = @"select Count(1) from S_NodeInfo
                                    left join(
                                    select * from S_FileInfo )tempFile
                                    on S_NodeInfo.ID=tempFile.NodeID
                                    where 
                                    S_NodeInfo.ParentID in ('{0}') or tempFile.NodeID in ('{0}')";
            var countRow = SQLHelper.CreateSqlHelper(docSpace.DbName, docSpace.ConnectString).ExecuteScalar(string.Format(sqlCheckChild,listIDs.Replace(",","','")));
            if (Convert.ToInt32(countRow.ToString()) > 0) 
                throw new Formula.Exceptions.BusinessValidationException("存在节点存在子节点或文件，不能删除！");
            foreach (var item in list)
            {
                var archiveVolumnID = item.GetValue("ArchiveVolumnID");
                S_NodeInfo nodeInfo = new S_NodeInfo(archiveVolumnID, docSpace);
                nodeInfo.Delete();
                //nodeInfo.DataEntity.DeleteDB(nodeInfo.InstanceDB, "S_NodeInfo", nodeInfo.ID);
                //archiveVolumnIDs.Add(archiveVolumnID.ToString());
            }
            var sql = "update S_R_PhysicalReorganize_NodeDetail set ReorganizePath='',ReorganizeFullID='',ArchiveVolumnID='' where ArchiveVolumnID in ('{0}')";
            sql = string.Format(sql,string.Join("','",archiveVolumnIDs));
            this.SqlHelper.ExecuteNonQuery(sql.ToString());
            return Json(list);
        }
        //获取已归档的案卷目录
        public JsonResult DocVolumn(string PhysicalReorganizeID)
        {
            var space = DocConfigHelper.CreateConfigSpaceByID(GetQueryString("SpaceID"));
            var nodeDetailEntities = FormulaHelper.GetEntities<DocConstEntities>().Set<S_R_PhysicalReorganize_NodeDetail>();
            var volumnIDs = nodeDetailEntities.Where(a => !string.IsNullOrEmpty(a.ArchiveVolumnID) && PhysicalReorganizeID == a.S_R_PhysicalReorganizeID).Select(a => a.ReorganizeFullID).ToList();//取到已归档的卷册在NodeInfo表中的ID
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(space.DbName, space.ConnectString);
            string sql = "select * from S_NodeInfo where FullPathID in ('{0}')";//拿到S_Nodeinfo表中的实物卷册归档的节点
            DataTable table = sqlHelp.ExecuteDataTable(string.Format(sql, string.Join("','", volumnIDs)));
            DataColumn addPhysicalColumn = new DataColumn();
            addPhysicalColumn.ColumnName = "S_R_PhysicalReorganizeID";
            addPhysicalColumn.DataType = System.Type.GetType("System.String");
            addPhysicalColumn.DefaultValue = PhysicalReorganizeID;
            table.Columns.Add(addPhysicalColumn);
            return Json(table);
        }
        //VolumnDetial归档卷册详细信息

        public JsonResult VolumnDetail(string ReorganizeFullID, string PhysicalReorganizeID)
        {
            string sqlFile = "select * from S_R_PhysicalReorganize_NodeDetail where ReorganizeFullID in ('{0}') and S_R_PhysicalReorganizeID='{1}'";
            SQLHelper fileDetSql = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            DataTable table = fileDetSql.ExecuteDataTable(string.Format(sqlFile, ReorganizeFullID, PhysicalReorganizeID));
            return Json(table);
        }
        //按卷的删除操作
        public JsonResult VolumnDelete()
        {
            string id = GetQueryString("IDs");
            string SpaceID = GetQueryString("SpaceID");
            var idAry = id.TrimEnd(',').Split(',');
            var list = entities.Set<S_R_PhysicalReorganize_NodeDetail>().Where(a => idAry.Contains(a.ID)).ToList();
            foreach (var item in list)
                entities.Set<S_R_PhysicalReorganize_NodeDetail>().Remove(item);

            var docSpace = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
            string nodeName = string.Empty;
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.ArchiveVolumnID))
                    nodeName += item.Name + ",";//获得已归档的结点名
            }
            nodeName = nodeName.TrimEnd(',');
            if (!string.IsNullOrEmpty(nodeName))
                throw new BusinessException("卷册【" + nodeName + "】已归档，删除失败！");
            entities.SaveChanges();

            return Json("");
        }
        public JsonResult GetConfigNodes() {
            var space = DocConfigHelper.CreateConfigSpaceByID(GetQueryString("SpaceID"));
            //节点都能关联文件
            var fileNode = space.S_DOC_Node.Select(a => a).ToList();
            return Json(fileNode);
        }


        #endregion
        #region 完成整编，向归档人发送“整编完成”消息
        public void SendMessage(string userId, string userName, string reciptName)
        {
            var msgService = FormulaHelper.GetService<IMessageService>();
            msgService.SendMsg("图文档案：归档成功消息", "档案管理员" + userName + ":您送归档的【" + reciptName + "】已归档", string.Empty, string.Empty, userId, userName);
        }
        //文件及文件父节点发布
        public void Publish()
        {
            string spaceID = GetQueryString("spaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(space.DbName, space.ConnectString);
            string fileIDs = GetQueryString("fileIDs");
            fileIDs = fileIDs.TrimEnd(',');
            fileIDs = fileIDs.Replace(",", "','");
            string sql = @"update S_FileInfo set State='{0}' where ID in ('{1}') ";
            sql += @"update S_NodeInfo set State='{0}' where ID in (select S_NodeInfo.ID from S_NodeInfo left join S_FileInfo on S_NodeInfo.ID=S_FileInfo.NodeID
where S_FileInfo.ID in ('{1}'))";
            sql = string.Format(sql, DocState.Published.ToString(), fileIDs);
            sqlHelp.ExecuteNonQuery(sql);
        }
        //卷册发布
        public void VolumnPublish()
        {
            string spaceID = GetQueryString("spaceID");
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(space.DbName, space.ConnectString);
            string volumnIDs = GetQueryString("volumnIDs");
            volumnIDs = volumnIDs.TrimEnd(',');
            volumnIDs = volumnIDs.Replace(",", "','");
            string sql = @"update S_NodeInfo set State='{0}' where ID in ('{1}')";
            sql = string.Format(sql, DocState.Published.ToString(), volumnIDs);
            sqlHelp.ExecuteNonQuery(sql);
        }
        #endregion
    }
}
