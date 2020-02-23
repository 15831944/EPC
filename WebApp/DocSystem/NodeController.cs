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
using MvcAdapter;
using System.Web.Mvc;
using Formula.Exceptions;
using System.Collections;
using Config.Logic;


namespace DocSystem
{
    public class NodeController : MvcAdapter.BaseController
    {
        #region 公共属性

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

        S_DOC_Node _nodeConfig;
        protected virtual S_DOC_Node Config
        {
            get
            {
                if (_nodeConfig == null)
                {
                    if (this.Space == null)
                        throw new Formula.Exceptions.BusinessException("未指定档案实体空间，无法获取数据访问对象");
                    _nodeConfig = this.Space.S_DOC_Node.FirstOrDefault(d => d.ID == this.ConfigID);
                    if (_nodeConfig == null)
                        throw new Formula.Exceptions.BusinessException("未能找ID为【" + this.ConfigID + "】的编目定义对象");
                }
                return _nodeConfig;
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

        protected virtual void BeforeSave(S_NodeInfo node, Dictionary<string, object> row, bool isNew) { }

        protected virtual void AfterSave(S_NodeInfo node, Dictionary<string, object> row, bool isNew) { }

        protected virtual void BeforeDelete(S_NodeInfo node) { }

        protected virtual void AfterDelete(S_NodeInfo node) { }

        #endregion

        public virtual JsonResult GetModel(string id)
        {
            var db = SQLHelper.CreateSqlHelper(this.Space.SpaceKey, this.Space.ConnectString);
            var entityDic = new Dictionary<string, object>();
            bool isNew = false;
            if (String.IsNullOrEmpty(id))
            {
                isNew = true;
                string parentID = this.Request["ParentID"];
                string configID = this.Request["ConfigID"];
                entityDic["ParentID"] = parentID;
                entityDic["ConfigID"] = configID;
                S_NodeInfo parent = null;
                if (!string.IsNullOrEmpty(parentID))
                    parent = S_NodeInfo.GetNode(parentID, this.SpaceID);
                var attrs = this.Config.S_DOC_NodeAttr.Where(d => !String.IsNullOrEmpty(d.DefaultValue));
                foreach (var attr in attrs)
                {
                    if (attr.DefaultValue.Split(',').Length > 0 && attr.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0 )
                    {
                        SetDefualtValue(entityDic,attr.AttrField, attr.DefaultValue.Split(',')[0],parent);
                        SetDefualtValue(entityDic, attr.AttrField + "Name", attr.DefaultValue.Split(',')[1], parent);
                    }
                    else
                        SetDefualtValue(entityDic, attr.AttrField, attr.DefaultValue, parent);
                }
            }
            else
            {
                var node = new S_NodeInfo(id, this.Space);
                entityDic = node.DataEntity;
            }
            this.AfterGetMode(entityDic, isNew);
            return Json(entityDic);
        }

        private void SetDefualtValue(Dictionary<string, object> result, string attrAttrField, string attrDefaultValue, S_NodeInfo parent)
        {
            var attrs = this.Config.S_DOC_NodeAttr.Where(d => !String.IsNullOrEmpty(d.DefaultValue));
            foreach (var attr in attrs)
            {
                if (attr.DefaultValue.IndexOf("{") >= 0)
                {
                    var defaultStr = attr.DefaultValue.Replace("{", "").Replace("}", "");
                    if (defaultStr == "Now")
                        result.SetValue(attr.AttrField, DateTime.Now);
                    else if (defaultStr == "UserID")
                        result.SetValue(attr.AttrField, CurrentUserInfo.UserID);
                    else if (defaultStr == "UserName")
                        result.SetValue(attr.AttrField, CurrentUserInfo.UserName);
                    else if (parent != null)
                    {
                        var defaultValue = attr.DefaultValue.Replace("{", "").Replace("}", "").Split(':');
                        if (defaultValue.Length > 1)
                        {
                            var nodePositon = defaultValue[0];
                            var defaultField = defaultValue[1];
                            if (nodePositon == "Root")
                            {
                                if (parent.RootNode.DataEntity.ContainsKey(defaultField))
                                    result.SetValue(attr.AttrField, parent.RootNode.DataEntity[defaultField]);
                            }
                            else
                            {
                                S_NodeInfo node = _getParentNode(parent, nodePositon.Split('.').Length);
                                if (node.DataEntity.ContainsKey(defaultField))
                                    result.SetValue(attr.AttrField, node.DataEntity[defaultField]);
                            }
                        }
                    }
                }
                else
                    result.SetValue(attr.AttrField, attr.DefaultValue);
            }
        }

        public virtual JsonResult Save()
        {
            string json = Request.Form["FormData"];
            bool isNew = false;

            var row = JsonHelper.ToObject<Dictionary<string, object>>(json);
            S_NodeInfo node;
            if (!row.ContainsKey("ID") || Tool.IsNullOrEmpty(row["ID"]))
            {
                isNew = true;
                node = new S_NodeInfo(this.SpaceID, this.ConfigID);
            }
            else
                node = new S_NodeInfo(row["ID"].ToString(), this.Space);
            node.SetData(row);
            BeforeSave(node, row, isNew);
            node.Save();
            AfterSave(node, row, isNew);
            return Json(node.DataEntity);
        }

        public virtual JsonResult Delete()
        {

            string listIDs = Request["ListIDs"];
            foreach (var ID in listIDs.Split(','))
            {
                var node = new S_NodeInfo(ID, this.Space);
                BeforeDelete(node);
                node.Delete();
                AfterDelete(node);
            }
            //清空S_R_PhysicalReorganize_NodeDetail中的归档目录
            string[] archiveIDs = listIDs.Split(new char[]{','});
            var nodeDetailEntity = FormulaHelper.GetEntities<DocConstEntities>().S_R_PhysicalReorganize_NodeDetail.Where(a => archiveIDs.Contains(a.ArchiveVolumnID)).Select(a=>a).ToList();
            foreach (var nodeDetail in nodeDetailEntity)
            {
                nodeDetail.ReorganizeFullID = "";
                nodeDetail.ReorganizePath = "";
                nodeDetail.ArchiveVolumnID = "";
            }
            FormulaHelper.GetEntities<DocConstEntities>().SaveChanges();
            return Json("");
        }

        public virtual JsonResult GetList(QueryBuilder qb)
        {
            string sql = "select thisNode.* from S_NodeInfo thisNode {0} where thisNode.ConfigID='" + this.ConfigID + "' and (1=1 {1})";
            this.FillQueryBuilderFilter<S_NodeInfo>(qb, true);

            #region 选择页面
            var queryType = this.GetQueryString("$QueryType");
            if (queryType.ToLower() == "sealup")
            {
                sql += " and Quantity = StorageNum and State not in ('" + DocState.SealUp.ToString() + "','" + DocState.SealUpApply.ToString() + "')";
            }
            
            #endregion

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
                var tableName = ItemName.Equals(configID) ? "thisNode" : "parent";
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
                    joinSql = "left join S_NodeInfo parent on parent.ID=thisNode.ParentID";
                    whereSql += " AND parent.ConfigID='" + configID + "'";
                }
            }
            sql = string.Format(sql, joinSql, whereSql);
            #endregion

            DocInstance.Logic.DocInstanceHepler.QueryBuilderExtend(qb);
            var data = this.InstanceDB.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public virtual void FillQueryBuilderFilter<T>(QueryBuilder qb, bool igorPtyExist = false)
        {
            string tokenKey = !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["TokenKey"]) ? System.Configuration.ConfigurationManager.AppSettings["TokenKey"] : "GWToken";
            //地址栏参数作为查询条件
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key == null || key == "_" || key == "_t" || key == "_winid" ||
                    key == "ID" || key == "FullID" || key == "FULLID" || key.StartsWith("$") ||
                    key == "ShowAdvanceQuery" || key == "QueryType")
                    continue;
                if (key.ToLower() == tokenKey.ToLower()) continue;
                if (key.ToLowerInvariant() == "functype") continue;
                if (igorPtyExist)
                    qb.Add(key, QueryMethod.In, Request[key].Split(','));
                else
                {
                    if (typeof(T).GetProperty(key) != null)
                    {
                        qb.Add(key, QueryMethod.In, Request[key].Split(','));
                    }
                    else if (typeof(T).GetProperty(key.ToUpper()) != null)//兼容Oracle
                    {
                        qb.Add(key, QueryMethod.In, Request[key].Split(','));
                    }
                }
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

        protected override DbContext entities
        {
            get { throw new NotImplementedException(); }
        }

        public virtual JsonResult gettreelist(bool isPublished = false)
        {
            string nodeID = this.Request["ID"];
            var node = S_NodeInfo.GetNode(nodeID, this.SpaceID);
            var root = node.RootNode;
            string sql = @"select {0},case when ParentID='' or ParentID is null then 'Root' else 'Child' end as Type
            from S_NodeInfo where 1=1 " + (isPublished ? "and State='Published'" : "")
                                        + " and FullPathID like '" + root.FullPathID + "%' and SpaceID='" + this.SpaceID + "' {1}";
            var treeConfig = this.Space.S_DOC_TreeConfig.FirstOrDefault();
            if (treeConfig != null)
                sql = String.Format(sql, treeConfig.GetDisplayStr(), treeConfig.GetOrderByStr());
            else
                sql = String.Format(sql, treeConfig.GetDisplayStr(), "");
            var dt = this.InstanceDB.ExecuteDataTable(sql);
            return Json(dt, JsonRequestBehavior.AllowGet);

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

        public virtual void publish()
        {
            string listIDs = Request["ID"];
            foreach (var ID in listIDs.Split(','))
            {
                if (Tool.IsNullOrEmpty(ID))
                    continue;
                var node = S_NodeInfo.GetNode(ID, this.SpaceID);
                node.Publish();
            }
        }

        public virtual void forbidden()
        {

            string listIDs = Request["ID"];
            foreach (var ID in listIDs.Split(','))
            {
                if (Tool.IsNullOrEmpty(ID))
                    continue;
                var node = S_NodeInfo.GetNode(ID, this.SpaceID);
                node.Recover();
            }
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

    }

}