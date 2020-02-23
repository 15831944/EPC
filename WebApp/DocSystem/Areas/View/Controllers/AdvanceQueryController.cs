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
using System.IO;
using System.Net;

namespace DocSystem.Areas.View.Controllers
{
    public class AdvanceQueryController : BaseController
    {
        UserInfo _userInfo;
        protected UserInfo CurrentUserInfo
        {
            get
            {
                if (_userInfo == null)
                    _userInfo = FormulaHelper.GetUserInfo();
                return _userInfo;
            }
        }

        public ActionResult QueryWin()
        {
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            var userInfo = FormulaHelper.GetUserInfo();
            var userQueryInfo = entities.Set<S_UserAdvanceQueryInfo>().FirstOrDefault(d => d.UserID == userInfo.UserID);

            ViewBag.UserAdQueryData = userQueryInfo == null ? "{}" : userQueryInfo.QueryData;

            return View();
        }

        public ActionResult List()
        {
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            var userInfo = FormulaHelper.GetUserInfo();
            var userQueryInfo = entities.Set<S_UserAdvanceQueryInfo>().FirstOrDefault(d => d.UserID == userInfo.UserID);
            if (userQueryInfo == null) throw new Formula.Exceptions.BusinessException("");

            return View();
        }

        public JsonResult GetModel(string id)
        {
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            var result = new Dictionary<string, object>();
            var userInfo = FormulaHelper.GetUserInfo();
            var userQueryInfo = entities.Set<S_UserAdvanceQueryInfo>().FirstOrDefault(d => d.UserID == userInfo.UserID);
            if (userQueryInfo != null)
            {
                if (!String.IsNullOrEmpty(userQueryInfo.QueryData))
                {
                    result = JsonHelper.ToObject(userQueryInfo.QueryData);
                }
            }
            return Json(result);
        }

        public JsonResult GetQueryNodeInfo(string QueryType, string SpaceID)
        {
            //if (QueryType == "Node")
            //{
            //    var list = this.Space.S_DOC_Node.Where(d => d.AllowAdvancedQuery == "True")
            //        .Select(c => new { text = c.Name, value = c.ID, c.CanBorrow }).ToList();
            //    return Json(list, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var list = this.Space.S_DOC_File.Where(d => d.AllowAdvancedQuery == "True")
            //        .Select(c => new { text = c.Name, value = c.ID, c.CanBorrow, c.CanDownload }).ToList();
            //    return Json(list, JsonRequestBehavior.AllowGet);
            //}
            return Json("");
        }

        public JsonResult SaveQueryData(string Data)
        {
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            var userInfo = FormulaHelper.GetUserInfo();
            var userQueryInfo = entities.Set<S_UserAdvanceQueryInfo>().FirstOrDefault(d => d.UserID == userInfo.UserID);
            if (userQueryInfo == null)
            {
                userQueryInfo = new S_UserAdvanceQueryInfo();
                userQueryInfo.ID = FormulaHelper.CreateGuid();
                userQueryInfo.UserID = userInfo.UserID;
                userQueryInfo.CreateDate = DateTime.Now;
                entities.Set<S_UserAdvanceQueryInfo>().Add(userQueryInfo);
            }
            userQueryInfo.QueryData = Data;
            userQueryInfo.ModifyDate = DateTime.Now;
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            var QueryType = this.Request["QueryType"].ToString();
            var ConfigID = this.Request["ConfigID"].ToString();
            var SpaceID = this.Request["SpaceID"].ToString();
            var QueryList = this.Request["AdvanceQueryList"].ToString();
            List<Dictionary<string, object>> queryList = JsonHelper.ToList(QueryList);
            List<Dictionary<string, object>> attrList = JsonHelper.ToList("");//结果字段对应的属性列表
            var enumService = FormulaHelper.GetService<IEnumService>();

            #region 拼查询sql
            string fileFields = string.Empty;
            string resultFileds = string.Empty;
            string nodeStr = string.Empty;
            string resultSql = string.Empty;
            if (QueryType == "File")
            {
                var nodeConfig = this.Space.S_DOC_File.FirstOrDefault(d => d.ID == ConfigID);
                if (nodeConfig == null)
                    throw new Formula.Exceptions.BusinessException("未能找ID为【" + ConfigID + "】查询结果定义对象");

                resultSql = "select main.*{0} from S_FileInfo main {1} where ConfigID='" + ConfigID + "' and State='" + DocState.Published.ToString() + "' ";
                attrList = FormulaHelper.CollectionToListDic(nodeConfig.S_DOC_FileAttr);
            }
            else if (QueryType == "Node")
            {
                var nodeConfig = this.Space.S_DOC_Node.FirstOrDefault(d => d.ID == ConfigID);
                if (nodeConfig == null)
                    throw new Formula.Exceptions.BusinessException("未能找ID为【" + ConfigID + "】查询结果定义对象");
                resultSql = "select main.*{0} from S_NodeInfo main {1} where ConfigID='" + ConfigID + "' and State='" + DocState.Published.ToString() + "' ";
                attrList = FormulaHelper.CollectionToListDic(nodeConfig.S_DOC_NodeAttr);
            }
            else
                throw new Formula.Exceptions.BusinessException("未定义查询类型【" + QueryType + "】");

            //查询条件作为查询对象属性
            string typeSql = string.Empty;
            var types = queryList.Select(a => a.GetValue("TypeValue")).Distinct().ToList();//编目节点
            string relateField = QueryType == "File" ? "FullNodeID" : "FullPathID";
            foreach (var type in types)
            {
                var _queryList = queryList.Where(a => a.GetValue("TypeValue") == type).ToList();
                var name = _queryList.FirstOrDefault().GetValue("Type");

                string sql = " left join (select ID{2} from S_NodeInfo where ConfigID='{0}' and State='{4}' ) {1} on charindex({1}.ID,main.{3},1)>0 ";
                string attrField = string.Empty;
                foreach (var queryItem in _queryList)
                {
                    var value = queryItem.GetValue("Value");
                    var af = queryItem.GetValue("AttrField");
                    var group = queryItem.GetValue("Group");
                    if (string.IsNullOrEmpty(value)) continue;
                    string fieldStr = af;
                    if (ConfigID != type)
                    {
                        fieldStr = name + "_" + af;
                        attrField += "," + af + " as " + fieldStr; //属性名
                        //结果列表字段
                        resultFileds += "," + fieldStr;  //属性名
                    }
                }
                if (!string.IsNullOrEmpty(attrField))
                {
                    sql = string.Format(sql, type, name, attrField, relateField, DocState.Published.ToString());
                    //查询条件 是 查询结果自身的属性
                    typeSql += sql;
                }
            }

            resultSql = string.Format(resultSql, resultFileds, typeSql);
            #endregion

            #region 拼结果列字段
            //枚举key列表
            List<Dictionary<string, object>> enumList = JsonHelper.ToList("");
            //查询列表字段
            List<Dictionary<string, object>> columnList = JsonHelper.ToList("");
            var listCofig = this.Space.S_DOC_ListConfig.FirstOrDefault(d => d.RelationID == ConfigID);
            var gridColumnDetailList = listCofig.S_DOC_ListConfigDetail.Where(d => d.Dispaly == "True").OrderBy(d => d.DetailSort).ToList();
            columnList.Add(new Dictionary<string, object>() { { "type", "checkcolumn" } });//选择列
            //columnList.Add(new Dictionary<string, object>() { { "type", "indexcolumn" } });//序号
            columnList.Add(new Dictionary<string, object>() { 
            { "header", "" }, { "field", "View" }, { "width", "70" } 
            , { "align", "center" } , { "headerAlign", "center" } });//查看
            string attrFieldStr = "FileAttrField";
            if (QueryType != "File")
                attrFieldStr = "AttrField";

            foreach (var item in gridColumnDetailList)
            {
                var attrDic = attrList.FirstOrDefault(a => a.GetValue(attrFieldStr) == item.AttrField);
                if (attrDic == null) continue;
                var column = new Dictionary<string, object>();
                column.Add("header", item.AttrName);
                column.Add("field", item.AttrField);
                column.Add("name", item.AttrField);
                column.Add("width", Convert.ToInt32(item.Width));
                column.Add("allowSort", Convert.ToBoolean(item.AllowSort));
                //if (Convert.ToBoolean(item.AllowSort))
                //    column.Add("sortField", item.AttrField);
                column.Add("align", item.Align);
                column.Add("headerAlign", "center");
                var dataType = attrDic.GetValue("DataType");
                if (dataType == AttrDataType.DateTime.ToString())
                {
                    column.Add("dataType", "date");
                    column.Add("dateFormat", "yyyy-MM-dd");
                }
                //处理枚举
                var isEnum = attrDic.GetValue("IsEnum");
                if (isEnum == TrueOrFalse.True.ToString())
                {
                    var enumKey = attrDic.GetValue("EnumKey");
                    var enumName = enumKey.Split('.').Last();
                    var enumJson = enumService.GetEnumJson(enumKey);

                    column.Add("renderer", "onEnumRender");
                    enumList.Add(new Dictionary<string, object>() { 
                    { "Field", item.AttrField }, 
                    { "EnumKey", enumKey }, 
                    { "EnumName", enumName }, 
                    { "EnumData",  string.Format("var {0} = {1};", enumName, enumJson) }
                    });
                }
                columnList.Add(column);
            }


            #endregion

            DocInstance.Logic.DocInstanceHepler.QueryBuilderExtend(qb, ConfigID, queryList);

            DataTable dt = this.InstanceDB.ExecuteDataTable(resultSql, qb);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.SetValue("data", dt);
            result.SetValue("total", qb.TotolCount);
            result.SetValue("columns", columnList);
            result.SetValue("enums", enumList);

            #region 保存查询数据

            var userDic = new Dictionary<string, object>();
            userDic.Add("QueryType", QueryType);
            userDic.Add("QueryNode", ConfigID);
            userDic.Add("List", queryList);
            var entities = FormulaHelper.GetEntities<DocConstEntities>();
            var userInfo = FormulaHelper.GetUserInfo();
            var userQueryInfo = entities.Set<S_UserAdvanceQueryInfo>().FirstOrDefault(d => d.UserID == userInfo.UserID);
            if (userQueryInfo == null)
            {
                userQueryInfo = new S_UserAdvanceQueryInfo();
                userQueryInfo.ID = FormulaHelper.CreateGuid();
                userQueryInfo.UserID = userInfo.UserID;
                userQueryInfo.CreateDate = DateTime.Now;
                entities.Set<S_UserAdvanceQueryInfo>().Add(userQueryInfo);
            }
            userQueryInfo.QueryData = JsonHelper.ToJson<Dictionary<string, object>>(userDic);
            userQueryInfo.ModifyDate = DateTime.Now;
            entities.SaveChanges();

            #endregion

            return Json(result);
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

        public JsonResult getcarinfo()
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            var arraylist = new ArrayList();
            //var borrowRpt = this.CreateEmptyEntity<S_BorrowDetail>();
            //var downloadRpt = this.CreateEmptyEntity<S_DownloadDetail>();
            Dictionary<string, object> borrow = new Dictionary<string, object>();
            borrow["borrow"] = 0;
            string sql = "select ID from S_CarInfo where UserID='" + this.CurrentUserInfo.UserID + "' and State='New' and Type='" + ItemType.Borrow.ToString() + "'";
            var dt = sqlHelper.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                borrow["borrow"] = dt.Rows.Count;
            arraylist.Add(borrow);
            Dictionary<string, object> download = new Dictionary<string, object>();
            download["download"] = 0;
            sql = "select ID from S_CarInfo where UserID='" + this.CurrentUserInfo.UserID + "' and State='New' and Type='" + ItemType.DownLoad.ToString() + "'";

            dt = sqlHelper.ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                download["download"] = dt.Rows.Count;
            arraylist.Add(download);
            return Json(arraylist, JsonRequestBehavior.AllowGet);

        }

        #region 选择属性界面方法
        public ActionResult AttrSelector()
        {
            ViewBag.SpaceID = this.GetQueryString("SpaceID");
            ViewBag.QueryType = this.GetQueryString("QueryType");
            var fileConfigID = ""; var fileConfigName = "文件";
            if (this.GetQueryString("QueryType") == "File")
            {
                fileConfigID = this.GetQueryString("ConfigID");
                if (!String.IsNullOrEmpty(fileConfigID))
                {
                    var fileConfig = this.Space.S_DOC_File.FirstOrDefault(d => d.ID == fileConfigID);
                    if (fileConfig == null) throw new Formula.Exceptions.BusinessException("未能找到文件节点的定义内容");
                    fileConfigName = fileConfig.Name;
                }
            }
            ViewBag.FileConfigID = fileConfigID;
            ViewBag.FileConfigName = fileConfigName;
            return View();
        }

        public JsonResult GetFileAttrList(string ConfigID)
        {
            string sql = @"select S_DOC_File.Name as [FileConfigName],S_DOC_FileAttr.* from S_DOC_FileAttr left join S_DOC_File
on S_DOC_File.ID=S_DOC_FileAttr.FileID
where Visible='True' and S_DOC_FileAttr.SpaceID='{0}' {1}
order by FileID,AttrSort";
            if (String.IsNullOrEmpty(ConfigID))
                sql = String.Format(sql, this.SpaceID, "");
            else
                sql = String.Format(sql, this.SpaceID, " and S_DOC_FileAttr.FileID='" + ConfigID + "'");
            var dt = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig).ExecuteDataTable(sql);
            return Json(dt, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNodeAttrList(string ConfigID)
        {
            var nodeConfig = this.Space.S_DOC_Node.FirstOrDefault(d => d.ID == ConfigID);
            if (nodeConfig == null) throw new Formula.Exceptions.BusinessException("未能找到编目节点的定义内容");
            var list = nodeConfig.S_DOC_NodeAttr.Where(d => d.Visible == true.ToString()).OrderBy(d => d.AttrSort).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNodeTree(string ConfigID, string QueryType)
        {
            var nodeStructList = new List<S_DOC_NodeStrcut>();
            if (QueryType == "Node")
            {
                var nodes = this.Space.S_DOC_NodeStrcut.Where(a => a.NodeID == ConfigID).ToList();
                nodeStructList = this.Space.S_DOC_NodeStrcut.Where(a => nodes.Any(b => b.FullPathID.StartsWith(a.FullPathID)) && a.NodeID != "Root")
                    .OrderBy(a => a.FullPathID).ToList();
            }
            else
            {
                if (String.IsNullOrEmpty(ConfigID))
                {
                    nodeStructList = this.Space.S_DOC_NodeStrcut.Where(a => a.NodeID != "Root").OrderBy(a => a.FullPathID).ToList();
                }
                else
                {
                    var fileConfig = this.Space.S_DOC_File.FirstOrDefault(d => d.ID == ConfigID);
                    if (fileConfig == null) throw new Formula.Exceptions.BusinessException("没有找到文件定义，无法进行查询");
                    var nodes = fileConfig.S_DOC_FileNodeRelation.Select(d => d.S_DOC_Node.StructNode).ToList();
                    nodeStructList = this.Space.S_DOC_NodeStrcut.Where(a => nodes.Any(b => b.FullPathID.StartsWith(a.FullPathID)) && a.NodeID != "Root")
                        .OrderBy(a => a.FullPathID).ToList();
                }
            }
            return Json(nodeStructList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 基础属性

        private SQLHelper _sqlHelper = null;
        protected virtual SQLHelper InstanceDB
        {
            get
            {
                if (_sqlHelper == null)
                {
                    _sqlHelper = SQLHelper.CreateSqlHelper(this.Space.SpaceKey, this.Space.ConnectString);
                }
                return _sqlHelper;
            }
        }

        string _spaceID = string.Empty;
        protected virtual string SpaceID
        {
            get
            {
                if (String.IsNullOrEmpty(_spaceID))
                    _spaceID = this.Request["SpaceID"];
                if (String.IsNullOrEmpty(_spaceID))
                    _spaceID = this.GetQueryString("SpaceID");
                return _spaceID;
            }
        }


        S_DOC_Space _space;
        protected virtual S_DOC_Space Space
        {
            get
            {
                if (_space == null)
                {
                    _space = DocConfigHelper.CreateConfigSpaceByID(SpaceID);
                    if (_space == null) throw new Formula.Exceptions.BusinessException("未能找ID为【" + SpaceID + "】的档案空间对象");
                }
                return _space;
            }
        }

        protected override System.Data.Entity.DbContext entities
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

    }
}
