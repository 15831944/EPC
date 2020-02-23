using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Reflection;
using Formula.DynConditionObject;
using Formula.Helper;
using Formula;
using DocSystem.Logic.Domain;
using DocSystem.Logic;
using Config;
using Config.Logic;
using MvcAdapter;
using System.Web.Mvc;
using Formula.Exceptions;
using System.Collections;
using System.Data;



namespace DocSystem
{
    public class FileController : MvcAdapter.BaseController
    {
        #region 公开属性

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

        string _configID = string.Empty;
        protected virtual string ConfigID
        {
            get
            {
                if (String.IsNullOrEmpty(_configID))
                    _configID = this.Request["ConfigID"];
                if (String.IsNullOrEmpty(_configID))
                    _configID = this.GetQueryString("ConfigID");
                return _configID;
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

        S_DOC_File _nodeConfig;
        protected virtual S_DOC_File Config
        {
            get
            {
                if (_nodeConfig == null)
                {
                    if (this.Space == null)
                        throw new Formula.Exceptions.BusinessException("未指定档案实体空间，无法获取数据访问对象");
                    _nodeConfig = this.Space.S_DOC_File.FirstOrDefault(d => d.ID == this.ConfigID);
                    if (_nodeConfig == null)
                        throw new Formula.Exceptions.BusinessException("未能找ID为【" + this.ConfigID + "】的编目定义对象");
                }
                return _nodeConfig;
            }
        }

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

        #endregion

        #region 子类扩展虚方法

        protected virtual void BeforeSave(Dictionary<string, object> entity, bool isNew) { }

        protected virtual void AfterGetMode(Dictionary<string, object> entityDic, bool isNew) { }

        protected virtual void BeforeSave(S_FileInfo file, Dictionary<string, object> row, bool isNew) { }

        protected virtual void AfterSave(S_FileInfo file, Dictionary<string, object> row, bool isNew) { }

        protected virtual void BeforeDelete(S_FileInfo file) { }

        protected virtual void AfterDelete(S_FileInfo file) { }

        #endregion

        public virtual JsonResult GetList(QueryBuilder qb)
        {
            string fileFields = this.Config.GetQueryFields();
            string sql = "select " + fileFields + ",S_Attachment.Attachments,S_Attachment.ThumbNail,S_Attachment.SWFFile,"
                + " S_Attachment.PDFFile,S_Attachment.MainFile,S_Attachment.PlotFile,S_Attachment.XrefFile,S_Attachment.DwfFile,S_Attachment.TiffFile,S_Attachment.SignPdfFile"
                + " from S_FileInfo {0} left join S_Attachment  on FileID=S_FileInfo.ID  and CurrentVersion='True' "
                + " where S_FileInfo.ConfigID='" + this.ConfigID + "' and (1=1 {1})";
            this.FillQueryBuilderFilter(qb);
            DocInstance.Logic.DocInstanceHepler.QueryBuilderExtend(qb);
            if (!String.IsNullOrEmpty(this.Request["IncludeChildren"]) && this.Request["IncludeChildren"].ToLower() == true.ToString().ToLower())
            {
                var nodeID = this.Request["NodeID"];
                var node = S_NodeInfo.GetNode(nodeID, this.SpaceID);
                if (node != null)
                {
                    var cnd = qb.Items.SingleOrDefault(c => c.Field == "NodeID");
                    qb.Items.Remove(cnd);
                    qb.Add("FullNodeID", QueryMethod.StartsWith, node.FullPathID);
                }
            }

            #region 高级查询
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
            #endregion

            var data = this.InstanceDB.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public virtual JsonResult GetModel(string id)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            bool isNew = false;
            if (String.IsNullOrEmpty(id))
            {
                isNew = true;
                var nodeID = this.Request["NodeID"];
                if (String.IsNullOrEmpty(nodeID)) nodeID = this.GetQueryString("NodeID");
                result.SetValue("NodeID", nodeID);
                result.SetValue("ConfigID", this.ConfigID);
                var node = S_NodeInfo.GetNode(nodeID, this.SpaceID);
                var attrs = this.Config.S_DOC_FileAttr.Where(d => !String.IsNullOrEmpty(d.DefaultValue));
                foreach (var attr in attrs)
                {
                    if (attr.DefaultValue.Split(',').Length > 0 && attr.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0)
                    {
                        SetDefualtValue(result, attr.FileAttrField, attr.DefaultValue.Split(',')[0], node);
                        SetDefualtValue(result, attr.FileAttrField + "Name", attr.DefaultValue.Split(',')[1], node);
                    }
                    else
                        SetDefualtValue(result, attr.FileAttrField, attr.DefaultValue, node);
                }
            }
            else
            {
                var file = new S_FileInfo(id, this.Space);
                result = file.DataEntity;
                if (file.CurrentAttachment != null)
                {
                    result["MainFile"] = file.CurrentAttachment.DataEntity.GetValue("MainFile");
                    result["Attachments"] = file.CurrentAttachment.DataEntity.GetValue("Attachments");
                    result["PdfFile"] = file.CurrentAttachment.DataEntity.GetValue("PDFFile");
                    result["PlotFile"] = file.CurrentAttachment.DataEntity.GetValue("PlotFile");
                    result["XrefFile"] = file.CurrentAttachment.DataEntity.GetValue("XrefFile");
                    result["DwfFile"] = file.CurrentAttachment.DataEntity.GetValue("DwfFile");
                    result["TiffFile"] = file.CurrentAttachment.DataEntity.GetValue("TiffFile");
                    result["SignPdfFile"] = file.CurrentAttachment.DataEntity.GetValue("SignPdfFile");
                    result["AtlasFile"] = JsonHelper.ToList(file.CurrentAttachment.DataEntity.GetValue("AtlasFile"));
                }
            }
            this.AfterGetMode(result, isNew);
            return Json(result);
        }

        private void SetDefualtValue( Dictionary<string, object> result,string attrAttrField, string attrDefaultValue,S_NodeInfo node)
        {
            if (attrDefaultValue.IndexOf("{") >= 0)
            {
                var defaultStr = attrDefaultValue.Replace("{", "").Replace("}", "");
                if (defaultStr == "Now")
                    result.SetValue(attrAttrField, DateTime.Now);
                else if (defaultStr == "UserID")
                    result.SetValue(attrAttrField, CurrentUserInfo.UserID);
                else if (defaultStr == "UserName")
                    result.SetValue(attrAttrField, CurrentUserInfo.UserName);
                else
                {
                    var defaultValue = attrDefaultValue.Replace("{", "").Replace("}", "").Split(':');
                    var nodePositon = defaultValue[0];
                    var defaultField = defaultValue[1];
                    if (nodePositon == "Root")
                    {
                        if (node.RootNode.DataEntity.ContainsKey(defaultField))
                            result.SetValue(attrAttrField, node.RootNode.DataEntity[defaultField]);
                    }
                    else if (nodePositon == "Node")
                    {
                        if (node.DataEntity.ContainsKey(defaultField))
                            result.SetValue(attrAttrField, node.DataEntity[defaultField]);
                    }
                    else if (nodePositon.Split('.').Length > 0)
                    {
                        var pnode = _getParentNode(node, nodePositon.Split('.').Length);
                        if (pnode.DataEntity.ContainsKey(defaultField))
                            result.SetValue(attrAttrField, node.DataEntity[defaultField]);
                    }
                }
            }
            else
                result.SetValue(attrAttrField, attrDefaultValue);
        }

        public virtual JsonResult Save()
        {
            var user = FormulaHelper.GetUserInfo();
            string json = Request.Form["FormData"];
            bool isUpVersion = string.IsNullOrEmpty(this.Request["UpVersion"]) ? false : this.Request["UpVersion"].ToLower() == "true";
            bool isNew = false;

            var row = JsonHelper.ToObject<Dictionary<string, object>>(json);
            S_FileInfo file;
            if (!row.ContainsKey("ID") || Tool.IsNullOrEmpty(row["ID"]))
            {
                isNew = true;
                file = new S_FileInfo(this.SpaceID, this.ConfigID);
                file.SetData(row);
                var node = new S_NodeInfo(file.NodeID, this.Space);
                file.FullNodeID = node.FullPathID;
                file.ID = FormulaHelper.CreateGuid();
                file.DataEntity.SetValue("State", DocState.Normal.ToString());
                string mainFile = row.GetValue("MainFile");
                string attachments = row.GetValue("Attachments");
                string pdfFile = row.GetValue("PdfFile");
                string plotFile = row.GetValue("PlotFile");
                string xrefFile = row.GetValue("XrefFile");
                string dwfFile = row.GetValue("DwfFile");
                string tiffFile = row.GetValue("TiffFile");
                string signPdfFile = row.GetValue("SignPdfFile");
                string atlasFile = row.GetValue("AtlasFile");
                if (!String.IsNullOrEmpty(mainFile) || !String.IsNullOrEmpty(attachments)
                    || !String.IsNullOrEmpty(pdfFile) || !String.IsNullOrEmpty(plotFile)
                    || !String.IsNullOrEmpty(plotFile) || !String.IsNullOrEmpty(xrefFile)
                    || !String.IsNullOrEmpty(dwfFile) || !String.IsNullOrEmpty(signPdfFile)
                    || !string.IsNullOrEmpty(atlasFile))
                {
                    S_Attachment attach = new S_Attachment(this.SpaceID);
                    attach.DataEntity.SetValue("MainFile", mainFile);
                    attach.DataEntity.SetValue("Attachments", attachments);
                    attach.DataEntity.SetValue("PDFFile", pdfFile);
                    attach.DataEntity.SetValue("PlotFile", plotFile);
                    attach.DataEntity.SetValue("XrefFile", xrefFile);
                    attach.DataEntity.SetValue("DwfFile", dwfFile);
                    attach.DataEntity.SetValue("TiffFile", tiffFile);
                    attach.DataEntity.SetValue("SignPdfFile", signPdfFile);
                    attach.DataEntity.SetValue("AtlasFile", atlasFile);
                    attach.DataEntity.SetValue("CreateDate", DateTime.Now.ToString());
                    attach.DataEntity.SetValue("CreateUser", user.UserID);
                    attach.DataEntity.SetValue("CreateUserName", user.UserName);
                    file.AddAttachment(attach);
                }
            }
            else
            {
                file = new S_FileInfo(row["ID"].ToString(), this.Space);
                string mainFile = row.GetValue("MainFile");
                string attachments = row.GetValue("Attachments");
                string pdfFile = row.GetValue("PdfFile");
                string plotFile = row.GetValue("PlotFile");
                string xrefFile = row.GetValue("XrefFile");
                string dwfFile = row.GetValue("DwfFile");
                string tiffFile = row.GetValue("TiffFile");
                string signPdfFile = row.GetValue("SignPdfFile");
                string atlasFile = row.GetValue("AtlasFile");
                if (!String.IsNullOrEmpty(mainFile) || !String.IsNullOrEmpty(attachments)
                    || !String.IsNullOrEmpty(pdfFile) || !String.IsNullOrEmpty(plotFile)
                    || !String.IsNullOrEmpty(plotFile) || !String.IsNullOrEmpty(xrefFile)
                    || !String.IsNullOrEmpty(dwfFile) || !String.IsNullOrEmpty(signPdfFile)
                    || !string.IsNullOrEmpty(atlasFile))
                {
                    if (isUpVersion || file.CurrentAttachment == null || mainFile != file.CurrentAttachment.DataEntity.GetValue("MainFile"))
                    {
                        S_Attachment attach = new S_Attachment(this.SpaceID);
                        attach.DataEntity.SetValue("MainFile", mainFile);
                        attach.DataEntity.SetValue("Attachments", attachments);
                        attach.DataEntity.SetValue("PDFFile", pdfFile);
                        attach.DataEntity.SetValue("PlotFile", plotFile);
                        attach.DataEntity.SetValue("XrefFile", xrefFile);
                        attach.DataEntity.SetValue("DwfFile", dwfFile);
                        attach.DataEntity.SetValue("TiffFile", tiffFile);
                        attach.DataEntity.SetValue("SignPdfFile", signPdfFile);
                        attach.DataEntity.SetValue("AtlasFile", atlasFile);
                        attach.DataEntity.SetValue("CreateDate", DateTime.Now.ToString());
                        attach.DataEntity.SetValue("CreateUser", user.UserID);
                        attach.DataEntity.SetValue("CreateUserName", user.UserName);
                        file.AddAttachment(attach);
                    }
                    else
                    {
                        file.CurrentAttachment.DataEntity.SetValue("Attachments", attachments);
                        file.CurrentAttachment.DataEntity.SetValue("PDFFile", pdfFile);
                        file.CurrentAttachment.DataEntity.SetValue("PlotFile", plotFile);
                        file.CurrentAttachment.DataEntity.SetValue("XrefFile", xrefFile);
                        file.CurrentAttachment.DataEntity.SetValue("DwfFile", dwfFile);
                        file.CurrentAttachment.DataEntity.SetValue("TiffFile", tiffFile);
                        file.CurrentAttachment.DataEntity.SetValue("SignPdfFile", signPdfFile);
                        file.CurrentAttachment.DataEntity.SetValue("AtlasFile", atlasFile);
                        file.CurrentAttachment.Save();
                    }
                }
                file.SetData(row);
            }
            if (file.DataEntity.ContainsKey("DocIndexID"))
                file.DataEntity.Remove("DocIndexID");
            BeforeSave(file, row, isNew);
            file.Save(false);
            AfterSave(file, row, isNew);
            return Json(file.DataEntity);
        }

        public virtual JsonResult Delete()
        {
            string listIDs = Request["ListIDs"];
            foreach (var ID in listIDs.Split(','))
            {
                var file = new S_FileInfo(ID, this.Space);
                BeforeDelete(file);
                file.Delete();
                AfterDelete(file);
            }
            return Json("");
        }


        public virtual void publish()
        {
            string listIDs = Request["ID"];
            foreach (var ID in listIDs.Split(','))
            {
                if (Tool.IsNullOrEmpty(ID))
                    continue;
                var file = S_FileInfo.GetFile(ID, this.SpaceID);
                file.Publish();
            }
        }

        public virtual void forbidden()
        {
            string listIDs = Request["ID"];
            foreach (var ID in listIDs.Split(','))
            {
                if (Tool.IsNullOrEmpty(ID))
                    continue;
                var file = S_FileInfo.GetFile(ID, this.SpaceID);
                file.Recover();
            }
        }

        public virtual void FillQueryBuilderFilter(QueryBuilder qb)
        {
            string tokenKey = !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["TokenKey"]) ? System.Configuration.ConfigurationManager.AppSettings["TokenKey"] : "GWToken";
            //地址栏参数作为查询条件
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key == null || key == "_" || key == "_t" || key == "_winid" ||
                    key == "ShowInclude" || key == "FullPathID" || key == "ID" ||
                    key == "FullID" || key == "FULLID" || key.StartsWith("$") ||
                    key == "ShowAdvanceQuery" || key == "QueryType")
                    continue;
                if (key.ToLower() == tokenKey.ToLower()) continue;
                if (key.ToLowerInvariant() == "functype") continue;
                qb.Add(key, QueryMethod.In, Request[key].Split(','));
            }
        }

        public virtual T CreateEmptyEntity<T>(string ID = "") where T : class, new()
        {
            var result = new T();
            if (String.IsNullOrEmpty(ID))
                FormulaHelper.SetProperty(result, "ID", FormulaHelper.CreateGuid());
            else
                FormulaHelper.SetProperty(result, "ID", ID);
            return result;
        }

        public virtual T GetEntityByID<T>(string ID) where T : class, new()
        {
            var spec = new Specifications();
            spec.AndAlso("ID", ID, QueryMethod.Equal);
            var result = this.entities.Set<T>().FirstOrDefault(spec.GetExpression<T>());
            return result;
        }

        public virtual void movefile()
        {
            string nodeID = Request["NodeID"];
            string listIDs = Request["ListIDs"];
            foreach (var ID in listIDs.Split(','))
            {
                if (!string.IsNullOrEmpty(ID))
                {
                    var FileNode = new S_FileInfo(ID, this.Space);
                    FileNode.MoveTo(nodeID);
                }
            }
        }

        private S_NodeInfo _getParentNode(S_NodeInfo parent, int len)
        {
            var node = parent;
            if (len == 1) return node;
            else
            {
                for (int i = 0; i < len - 2; i++)
                {
                    if (parent.Parent != null)
                        node = parent.Parent;
                }
            }
            return node;
        }

        public JsonResult getcarinfo()
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            var arraylist = new ArrayList();
            var borrowRpt = this.CreateEmptyEntity<S_BorrowDetail>();
            var downloadRpt = this.CreateEmptyEntity<S_DownloadDetail>();
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

        public void savelog()
        {
            var result = new Hashtable();
            string spaceID = this.Request["SpaceID"];
            string FileID = this.Request["FileID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            S_FileInfo file = new S_FileInfo(FileID, space);
            // S_DocumentLog.Svae(file, this.PageDomainSession, DocInstance.Logic.ApplyType.Browse);
        }

        protected override DbContext entities
        {
            get { throw new NotImplementedException(); }
        }
        #region 验证文件唯一，有哪些不符合
        public static void FileValidateDataAttr(S_FileInfo file)
        {
            bool isNew = true;
            var valiateAttr = file.ConfigInfo.S_DOC_FileAttr.Where(d => d.ValidateType != ValidateType.None.ToString()).ToList();
            var uniqueList = valiateAttr.Where(d => d.ValidateType == ValidateType.Unique.ToString()).ToList();
            var typeUniqueList = valiateAttr.Where(d => d.ValidateType == ValidateType.TypeUnique.ToString()).ToList();
            var bortherUniqueList = valiateAttr.Where(d => d.ValidateType == ValidateType.BortherUnique.ToString()).ToList();
            string sql = " SELECT Name FROM " + file.TableName + " WHERE 1=1";
            string whereStr = string.Empty;
            string fileNames = string.Empty;
            string attrNames = string.Empty;
            foreach (var item in uniqueList)
            {
                whereStr += item.FileAttrField + " = '" + file.DataEntity.GetValue(item.FileAttrField) + "' OR ";
                attrNames += item.FileAttrName + ",";
            }

            if (!String.IsNullOrEmpty(whereStr))
            {
                whereStr = whereStr.Trim().TrimEnd(new char[] { 'O', 'R' });
                whereStr += ")";
                if (!isNew) whereStr += " AND ID !='" + file.ID + "'";
                DataRowCollection rows = file.InstanceDB.ExecuteDataTable(sql + "AND (" + whereStr).Rows;
                foreach (DataRow row in rows)
                    fileNames += row["Name"] + ",";
                if (fileNames.Length > 0)
                    throw new Formula.Exceptions.BusinessException("文件【" + attrNames.TrimEnd(',') + "】重复，请修改后重新归档\n文件【" + fileNames.TrimEnd(',') + "】");
            }

            attrNames = string.Empty; whereStr = string.Empty;
            foreach (var item in typeUniqueList)
            {
                whereStr += item.FileAttrField + " = '" + file.DataEntity.GetValue(item.FileAttrField) + "' OR ";
                attrNames += item.FileAttrName + ",";
            }
            if (!String.IsNullOrEmpty(whereStr))
            {
                whereStr = whereStr.Trim().TrimEnd(new char[] { 'O', 'R' });
                whereStr += ") AND CONFIGID='" + file.ConfigInfo.ID + "' ";
                if (!isNew) whereStr += " AND ID !='" + file.ID + "'";
                DataRowCollection rows = file.InstanceDB.ExecuteDataTable(sql + "AND (" + whereStr).Rows;
                foreach (DataRow row in rows)
                    fileNames += row["Name"] + ",";
                if (fileNames.Length > 0)
                    throw new Formula.Exceptions.BusinessException("文件【" + attrNames.TrimEnd(',') + "】重复，在" + file.ConfigInfo.Name + "范围内不存在重复的内容，请修改后重新归档\n文件【" + fileNames.TrimEnd(',') + "】");
            }

            attrNames = string.Empty; whereStr = string.Empty;
            foreach (var item in bortherUniqueList)
            {
                whereStr += item.FileAttrField + " = '" + file.DataEntity.GetValue(item.FileAttrField) + "' OR ";
                attrNames += item.FileAttrName + ",";
            }
            if (!String.IsNullOrEmpty(whereStr))
            {
                whereStr = whereStr.Trim().TrimEnd(new char[] { 'O', 'R' });
                whereStr += ") AND NodeID='" + file.DataEntity.GetValue("NodeID") + "' ";
                if (!isNew) whereStr += " AND ID !='" + file.ID + "'";
                DataRowCollection rows = file.InstanceDB.ExecuteDataTable(sql + "AND (" + whereStr).Rows;
                foreach (DataRow row in rows)
                    fileNames += row["Name"] + ",";
                if (fileNames.Length > 0)
                    throw new Formula.Exceptions.BusinessException("文件【" + attrNames.TrimEnd(',') + "】重复，请修改后重新归档\n文件【" + fileNames.TrimEnd(',') + "】");
            }
        }
        #endregion
    }

}