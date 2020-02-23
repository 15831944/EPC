using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Config;
using DocSystem.Logic;
using DocSystem.Logic.Domain;
using Formula;
using Formula.DynConditionObject;
using MvcAdapter;
using Newtonsoft.Json;

namespace DocSystem.Areas.View.Controllers
{
    public class MyPortalController : DocConstController
    {
        public ActionResult Index()
        {
            SQLHelper constSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            //下载车数据
            DataTable table = constSqlHelper.ExecuteDataTable(string.Format("select * from S_CarInfo with(nolock) where CreateUserId='{0}' and State='New' and Type='DownLoad'", this.CurrentUserInfo.UserID));
            ViewBag.table = JsonConvert.SerializeObject(table);
            //购物车数量
            //var carCount = constSqlHelper.ExecuteScalar(string.Format("select count(1) from S_CarInfo where CreateUserId='{0}' and State='New'", this.CurrentUserInfo.UserID));
            ViewBag.CarCount = CarFO.GetCarCount().ToString();
            //档案空间列表
            SQLHelper configSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable spaceTable = configSqlHelper.ExecuteDataTable("select * from S_DOC_Space  with(nolock)");
            ViewBag.spaceTable = spaceTable;
            //我的下载数量
            string myDownSql = "select count(1) from S_DownloadDetail  with(nolock) where DownloadExpireDate>='" + DateTime.Now.ToString() + "'";
            ViewBag.MyDownCount = constSqlHelper.ExecuteScalar(myDownSql);
            //我的借阅数量
            string myBorrowSql = @"select count(1) from S_BorrowDetail  with(nolock) where   BorrowState in ('Finish','" + BorrowReturnState.Borrow.ToString() + "')";
            ViewBag.MyBorrowCount = constSqlHelper.ExecuteScalar(myBorrowSql);
            //档案空间列表子项信息
            DataTable nodeConfigTable = configSqlHelper.ExecuteDataTable("select ID,Name,spaceID from S_DOC_Node  with(nolock) where AllowDisplay='true' order by SortIndex asc");
            ViewBag.nodeConfigTable = nodeConfigTable;
            //历史浏览
            DataTable userSearchHistory = UserSearchHistory();
            ViewBag.userSearchHistory = userSearchHistory;
            //档案空间添加数量
            List<DataTable> list = new List<DataTable>();
            foreach (DataRow spaceRow in spaceTable.Rows)
            {
                SQLHelper SqlHelper = new SQLHelper(@"Data Source=" + spaceRow["Server"] + ";initial Catalog=master;User ID=" + spaceRow["UserName"] + ";PassWord=" + spaceRow["Pwd"]);

                //判断数据库是否存在
                if ((int)SqlHelper.ExecuteScalar(" select COUNT(*) From master.dbo.sysdatabases where name='" + spaceRow["DbName"] + "'") <= 0)
                    continue;
                SQLHelper spaceSqlHelper = new SQLHelper(@"Data Source=" + spaceRow["Server"] + ";initial Catalog=" + spaceRow["DbName"] + ";User ID=" + spaceRow["UserName"] + ";PassWord=" + spaceRow["Pwd"]);
                //取总文档数 data source=10.10.1.235\sql2008;Initial Catalog=EPM_HR;User ID=sa;PWD=zxc.123
                DataTable fileCount = spaceSqlHelper.ExecuteDataTable(string.Format("select COUNT(1) spaceCount,SpaceID from S_FileInfo where SpaceID = '{0}' and State='Published' group by SpaceID ", spaceRow["ID"]));
                //spaceRow["Name"] = spaceRow["Name"] + "（" + fileCount.ToString() + "）";
                list.Add(fileCount);
            }
            ViewBag.spaceNameCount = list;
            //档案空间下的子项的数量
            List<DataTable> listChild = new List<DataTable>();
            foreach (DataRow spaceRow in spaceTable.Rows)
            {
                SQLHelper SqlHelper = new SQLHelper(@"Data Source=" + spaceRow["Server"] + ";initial Catalog=master;User ID=" + spaceRow["UserName"] + ";PassWord=" + spaceRow["Pwd"]);

                //判断数据库是否存在
                if ((int)SqlHelper.ExecuteScalar(" select COUNT(*) From master.dbo.sysdatabases where name='" + spaceRow["DbName"] + "'") <= 0)
                    continue;
                SQLHelper spaceSqlHelper = new SQLHelper(@"Data Source=" + spaceRow["Server"] + ";initial Catalog=" + spaceRow["DbName"] + ";User ID=" + spaceRow["UserName"] + ";PassWord=" + spaceRow["Pwd"]);


                DataTable childCount = spaceSqlHelper.ExecuteDataTable(string.Format("select COUNT(1) childCount,ConfigID,SpaceID from S_NodeInfo where State='Published' group by ConfigID,SpaceID "));

                listChild.Add(childCount);
            }
            ViewBag.childCount = listChild;
            return View();
        }
        //搜索条件下的多选选项数据
        public JsonResult GetSpaceTable()
        {
            //档案空间列表
            SQLHelper configSqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable spaceTable = configSqlHelper.ExecuteDataTable("select * from S_DOC_Space  with(nolock)");
            return Json(spaceTable);
        }
        public JsonResult GetGridData()
        {
            string type = this.GetQueryString("Type");
            DataTable table = null;
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.DocConst);
            if (!string.IsNullOrEmpty(type) && type == "down")
                //table = sqlHelp.ExecuteDataTable(string.Format("select * from S_CarInfo where CreateUserId='{0}' and State='New' and Type='DownLoad'", this.CurrentUserInfo.UserID));
                //我的下载列表
                table = sqlHelp.ExecuteDataTable(string.Format("select Name,datediff(dd,getdate(),[DownloadExpireDate]) as RemainDay,Attachments, FileID from S_DownloadDetail  with(nolock) where DownloadExpireDate>='" + DateTime.Now.ToString() + "' and CreateUserID='" + FormulaHelper.GetUserInfo().UserID + "'"));
            else if (!string.IsNullOrEmpty(type) && type == "borrow")
                //table = sqlHelp.ExecuteDataTable(string.Format("select * from S_CarInfo where CreateUserId='{0}' and State='New' and Type='Borrow'", this.CurrentUserInfo.UserID));
                //RemainDay剩余天数 我的借阅列表
                table = sqlHelp.ExecuteDataTable(string.Format("select  Name, case when Remain is null then '7' else Remain end as RemainDay from (select *, datediff(dd,getdate(),[BorrowExpireDate]) as Remain from S_BorrowDetail  with(nolock)) as A  where   BorrowState in ('Finish','" + BorrowReturnState.Borrow.ToString() + "') and CreateUserID='" + FormulaHelper.GetUserInfo().UserID + "' order by BorrowExpireDate asc"));
            else if (!string.IsNullOrEmpty(type) && type == "browse")
            {
                sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
                table = sqlHelp.ExecuteDataTable(string.Format("select Name,OperateType as RemainDay, datediff(mi,CreateDate,getdate()) min, FileID from S_DOC_UserSearhHistory  with(nolock) where OperateType='view' and createUserID='" + FormulaHelper.GetUserInfo().UserID + "' order by CreateDate desc"));
            }// 
            //table = sqlHelp.ExecuteDataTable(string.Format("select  *  from (select * from S_BorrowDetail) as A  where   BorrowState in ('Finish','" + BorrowReturnState.Borrow.ToString() + "')"));
            else
                return Json("");
            string Jsons = Newtonsoft.Json.JsonConvert.SerializeObject(table);
            return Json(Jsons);
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
            return Json(new { });
        }

        public JsonResult GetCarCount()
        {
            //购物车数量
            return Json(new { carCount = CarFO.GetCarCount() });
        }

        //usersearchhistory
        public DataTable UserSearchHistory()
        {
            SQLHelper historySqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable historyData = historySqlHelper.ExecuteDataTable(String.Format("select Name,CreateDate,NodeID,SpaceID,FileID from S_DOC_UserSearhHistory  with(nolock) where CreateUserID='{0}' order by CreateDate desc", this.CurrentUserInfo.UserID));
            return historyData;
        }

        //高级搜索条件treeselect下拉列表数据
        public JsonResult GetItemEnums(string spaceID)
        {
            var list = new List<object>();
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找ID为【" + spaceID + "】的档案空间对象");
            //查找所有node定义，拿到nodeattr，根据属性中高级检索是否启用
            var nodes = space.S_DOC_Node.OrderBy(a => a.SortIndex).ToList();
            foreach (var node in nodes)
            {
                var nodeStrcut = space.S_DOC_NodeStrcut.FirstOrDefault(a => a.NodeID == node.ID);

                var nodeAttrs = node.S_DOC_NodeAttr.OrderBy(a => a.AttrSort).Where(a => a.AdvancedSearch == true.ToString()).ToList();
                var nodeChildren = new List<object>();

                foreach (var nodeAttr in nodeAttrs)
                {
                    if (nodeAttr.IsEnum == true.ToString() && !string.IsNullOrEmpty(nodeAttr.EnumKey))
                        nodeChildren.Add(new { id = nodeAttr.AttrField + "(" + node.ID + ")" + "S_NodeInfo", text = nodeAttr.AttrName, isEnum = true, enumKey = nodeAttr.EnumKey, pid = "Node.Items(" + node.ID + ")" + "S_NodeInfo" });
                    else
                        nodeChildren.Add(new { id = nodeAttr.AttrField + "(" + node.ID + ")" + "S_NodeInfo", text = nodeAttr.AttrName, pid = "Node.Items(" + node.ID + ")" + "S_NodeInfo" });
                }
                if (nodeAttrs.Count > 0)
                {
                    var nodeName = nodeStrcut == null ? "" : nodeStrcut.Name;
                    list.Add(new { id = "Node.Items(" + node.ID + ")" + "S_NodeInfo", text = nodeName + "属性", children = nodeChildren });
                }
            }
            //查询所有file定义
            var fileConfigs = space.S_DOC_File.OrderBy(a => a.SortIndex).ToList();
            if (fileConfigs == null) throw new Formula.Exceptions.BusinessException("未能找到文件的定义内容");
            foreach (var fileConfig in fileConfigs)
            {
                var fileAttrs = fileConfig.S_DOC_FileAttr.OrderBy(a => a.AttrSort).Where(a => a.AdvancedSearch == true.ToString()).ToList();
                var fileChildren = new List<object>();
                foreach (var fileAttr in fileAttrs)
                {
                    if (fileAttr.IsEnum == true.ToString() && !string.IsNullOrEmpty(fileAttr.EnumKey))
                        fileChildren.Add(new { id = fileAttr.FileAttrField + "(" + fileConfig.ID + ")" + "S_FileInfo", text = fileAttr.FileAttrName, isEnum = true, enumKey = fileAttr.EnumKey, pid = "File.Items(" + fileConfig.ID + ")" + "S_FileInfo" });
                    else
                        fileChildren.Add(new { id = fileAttr.FileAttrField + "(" + fileConfig.ID + ")" + "S_FileInfo", text = fileAttr.FileAttrName, pid = "File.Items(" + fileConfig.ID + ")" + "S_FileInfo" });
                }
                if (fileAttrs.Count > 0)
                    list.Add(new { id = "File.Items(" + fileConfig.ID + ")" + "S_FileInfo", text = fileConfig.Name + "属性", children = fileChildren });
            }

            return Json(list);
        }
        //档案空间的项目列表展示
        public JsonResult GetDataGridHtml()
        {
            string nodeConfigID = this.Request["ConfigID"];
            string spaceID = this.Request["SpaceID"];
            var space = DocConfigHelper.CreateConfigSpaceByID(spaceID);
            if (space == null) throw new Formula.Exceptions.BusinessException("未能找到ID为【" + spaceID + "】的档案空间配置对象");
            var node = space.S_DOC_Node.FirstOrDefault(d => d.ID == nodeConfigID);
            if (node == null)
                throw new Formula.Exceptions.BusinessException("未能找到ID为【" + nodeConfigID + "】的节点定义对象");
            MiniDataGrid grid = node.GetDataGrid(true) as MiniDataGrid;
            MiniGridColumn column = new MiniGridColumn();
            column.Field = "img";
            column.HeaderText = "打开树形图";
            column.align = "center"; column.Allowsort = false; column.Width = 100;
            grid.AddControl(column, 0);
            grid.Url = "/DocSystem/View/NodeView/GetList?SpaceID=" + spaceID + "&ConfigID=" + nodeConfigID;
            //借阅状态显示红绿状态
            grid.SetAttribute("ondrawcell", "onDrawCell");

            if (node.CanBorrow == "True")
            {
                MiniGridColumn columnBorrow = new MiniGridColumn();
                columnBorrow.Field = "Borrow";
                columnBorrow.HeaderText = "操作";
                columnBorrow.align = "center"; columnBorrow.Allowsort = false; columnBorrow.Width = 100;
                grid.AddControl(columnBorrow, 1);
            }

            //var listconfig = node.ListConfig().ID;
            var list = node.ListConfig().S_DOC_QueryParam.Where(a => a.InKey == "True").ToList();
            var quickQueryField = String.Join(",", list.Select(a => a.InnerField));
            var quickQueryText = String.Join(",", list.Select(a => a.AttrName));
            return Json(new { QuickQuery = new { field = quickQueryField, text = quickQueryText }, GridHtml = grid.Render(node.IsShowIndex == "True") });
        }

    }
}
