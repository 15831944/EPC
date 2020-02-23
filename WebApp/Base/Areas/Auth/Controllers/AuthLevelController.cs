using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formula.Helper;
using Base.Logic.Domain;
using Formula;
using Config;
using System.Data;
using MvcAdapter;
using System.Linq;

namespace Base.Areas.Auth.Controllers
{
    public class AuthLevelController : BaseController
    {
        public JsonResult GetList()
        {
            return Json(entities.Set<S_A_AuthLevel>());
        }

        public JsonResult SaveList()
        {
            return JsonSaveList<S_A_AuthLevel>();
        }



        public JsonResult GetOrgTree()
        {
            string sql = "select * from S_A_Org order by SortIndex";
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var dt = sqlHelper.ExecuteDataTable(sql, new SearchCondition());
            return Json(dt);
        }

        public JsonResult GetOrgTreeByUserID(string userId)
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);

            var obj = sqlHelper.ExecuteScalar(string.Format("select CorpID from S_A_AuthLevel where UserID='{0}'", userId));

            string corpIDs = obj == null ? "" : obj.ToString();

            string sql = string.Format("select *,Checked=case when ID in('{0}') then 1 else 0 end from S_A_Org order by SortIndex", corpIDs.ToString().Replace(",", "','"));

            var dt = sqlHelper.ExecuteDataTable(sql);

            return Json(dt);
        }

        public JsonResult GetUserList(QueryBuilder qb)
        {
            string nodeFullID = Request["NodeFullID"];
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            if (string.IsNullOrEmpty(nodeFullID))
            {
                var result = sqlHelp.ExecuteGridData(@"select a.ID,a.Code,a.Name,a.WorkNo,a.DeptName,a.SortIndex,case when a.ID=b.UserID then 1 else 0 end IsRole,-1 as ShowAll
                    from S_A_User a left join S_A_AuthLevel b on a.ID=b.UserID where a.IsDeleted<>'1'  order by SortIndex", qb);
                return Json(result);

            }

            string getUserListSQL = @"select b.ID,b.Code,b.Name,b.WorkNo,b.DeptName,b.SortIndex,case when a.UserID=c.UserID then 1 else 0 end IsRole,-1 as ShowAll
                from(select * from S_A__OrgUser where S_A__OrgUser.OrgID='{0}') a 
                join S_A_User b on a.UserID=b.ID And b.IsDeleted<>'1' 
                left join S_A_AuthLevel c on a.UserID=c.UserID order by SortIndex";
            var nodeID = nodeFullID.Split('.').Last();
            string sql = string.Format(getUserListSQL, nodeID);

            var data = sqlHelp.ExecuteGridData(sql, qb);
            return Json(data);
        }

        public JsonResult GetTreeByUserID(string userID, string type)
        {
            string fullID = Request["RootFullID"];
            if (string.IsNullOrEmpty(fullID))
                return Json("", JsonRequestBehavior.AllowGet);
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base");
            string sql = @"select 
                case when exists(select 1 from S_A_AuthLevel where {0} like '%'+a.FullID+'%' and UserID='{1}')
	                then 1 
		                else 0 
	                end RoleType,
                a.ID,a.ParentID,a.FullID,a.Code,a.Name,a.Type,a.SortIndex
                from S_A_Res a {2} order by ParentID,SortIndex,RoleType";
            if (string.IsNullOrEmpty(type))
                sql = string.Format(sql, "MenuRootFullID", userID, string.Format(" where FullID like '{0}%' and FullID not like '{1}%'", fullID, Config.Constant.SystemMenuFullID));
            else
                sql = string.Format(sql, "RuleRootFullID", userID, string.Format(" where FullID like '{0}%'", fullID));
            DataTable dt = sqlHelper.ExecuteDataTable(sql, new SearchCondition());

            foreach (DataRow row in dt.Rows)
            {
                string roleType = row["RoleType"].ToString();
                if (Convert.ToInt32(roleType) == 0)
                    row["Name"] = "<img src = '/CommonWebResource/Theme/Default/MiniUI/icons/unchecked.gif' id = " + "'" + row["ID"] + "'" + " />" + row["Name"].ToString();
                else
                    row["Name"] = "<img src = '/CommonWebResource/Theme/Default/MiniUI/icons/checked.gif' id = " + row["ID"] + " />" + row["Name"].ToString();

            }
            return Json(dt);
        }

        public JsonResult SaveUserRes(string userID, string checkedIDs)
        {
            checkedIDs = checkedIDs.Trim('"');

            //修复不能取消分级授权的bug
            if (checkedIDs == "")
            {
                entities.Set<S_A_AuthLevel>().Delete(c => c.UserID == userID);

                //删除分级授权菜单的权限
                string menuId = System.Configuration.ConfigurationManager.AppSettings["AuthLevelMenuID"];
                var menuIds = entities.Set<S_A_Res>().Where(c => c.FullID.Contains(menuId)).Select(c => c.ID).ToArray();
                entities.Set<S_A__UserRes>().Delete(c => c.UserID == userID && menuIds.Contains(c.ResID));

                entities.SaveChanges();
                return Json("");
            }

            //分级菜单授权到用户
            authToUser(userID);

            var level = entities.Set<S_A_AuthLevel>().SingleOrDefault(c => c.UserID == userID);
            if (level != null)
            {
                level.MenuRootFullID = checkedIDs;
            }
            else
            {
                S_A_AuthLevel authLevel = new S_A_AuthLevel();
                authLevel.ID = FormulaHelper.CreateGuid();
                authLevel.UserID = userID;
                authLevel.UserName = FormulaHelper.GetUserInfoByID(userID).UserName;
                authLevel.MenuRootFullID = checkedIDs;
                entities.Set<S_A_AuthLevel>().Add(authLevel);
            }
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult SaveUserRule(string userID, string checkedIDs)
        {
            checkedIDs = checkedIDs.Trim('"');
            //修复不能取消分级授权的bug
            if (checkedIDs == "")
            {
                entities.Set<S_A_AuthLevel>().Delete(c => c.UserID == userID);

                //删除分级授权菜单的权限
                string menuId = System.Configuration.ConfigurationManager.AppSettings["AuthLevelMenuID"];
                var menuIds = entities.Set<S_A_Res>().Where(c => c.FullID.Contains(menuId)).Select(c => c.ID).ToArray();
                entities.Set<S_A__UserRes>().Delete(c => c.UserID == userID && menuIds.Contains(c.ResID));

                entities.SaveChanges();
                return Json("");
            }

            //分级菜单授权到用户
            authToUser(userID);

            var level = entities.Set<S_A_AuthLevel>().SingleOrDefault(c => c.UserID == userID);
            if (level != null)
            {
                level.RuleRootFullID = checkedIDs;
            }
            else
            {
                S_A_AuthLevel authLevel = new S_A_AuthLevel();
                authLevel.ID = FormulaHelper.CreateGuid();
                authLevel.UserID = userID;
                authLevel.UserName = FormulaHelper.GetUserInfoByID(userID).UserName;
                authLevel.RuleRootFullID = checkedIDs;
                entities.Set<S_A_AuthLevel>().Add(authLevel);
            }
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult SaveUserOrg(string userID, string checkedIDs)
        {
            checkedIDs = checkedIDs.Trim('"');

            //修复不能取消分级授权的bug
            if (checkedIDs == "")
            {
                entities.Set<S_A_AuthLevel>().Delete(c => c.UserID == userID);

                //删除分级授权菜单的权限
                string menuId = System.Configuration.ConfigurationManager.AppSettings["AuthLevelMenuID"];
                var menuIds = entities.Set<S_A_Res>().Where(c => c.FullID.Contains(menuId)).Select(c => c.ID).ToArray();
                entities.Set<S_A__UserRes>().Delete(c => c.UserID == userID && menuIds.Contains(c.ResID));

                entities.SaveChanges();
                return Json("");
            }

            //分级菜单授权到用户
            authToUser(userID);

            var level = entities.Set<S_A_AuthLevel>().SingleOrDefault(c => c.UserID == userID);
            if (level != null)
            {
                level.CorpID = checkedIDs;
            }
            else
            {
                S_A_AuthLevel authLevel = new S_A_AuthLevel();
                authLevel.ID = FormulaHelper.CreateGuid();
                authLevel.UserID = userID;
                authLevel.UserName = FormulaHelper.GetUserInfoByID(userID).UserName;
                authLevel.CorpID = checkedIDs;
                entities.Set<S_A_AuthLevel>().Add(authLevel);
            }
            entities.SaveChanges();
            return Json("");
        }

        public JsonResult GetCheckedMenu(string id)
        {
            var level = entities.Set<S_A_AuthLevel>().SingleOrDefault(c => c.ID == id);
            if (level == null)
                return Json(new S_A_Res[] { });
            var arr = level.MenuRootFullID.Split(',');
            var result = entities.Set<S_A_Res>().Where(c => arr.Contains(c.FullID));
            return Json(result);
        }
        public JsonResult GetCheckedRule(string id)
        {
            var level = entities.Set<S_A_AuthLevel>().SingleOrDefault(c => c.ID == id);
            if (level == null)
                return Json(new S_A_Res[] { });
            var arr = (level.RuleRootFullID ?? "").Split(',');
            var result = entities.Set<S_A_Res>().Where(c => arr.Contains(c.FullID));
            return Json(result);
        }

        private void authToUser(string userID)
        {
            string menuId = System.Configuration.ConfigurationManager.AppSettings["AuthLevelMenuID"];

            var list = entities.Set<S_A_Res>().Where(c => c.FullID.Contains(menuId)).ToList();
            var arrID = list.Select(c => c.ID).ToArray();
            foreach (var item in list)
            {
                var obj = entities.Set<S_A__UserRes>().SingleOrDefault(c => c.UserID == userID && c.ResID == item.ID);
                if (obj == null)
                    entities.Set<S_A__UserRes>().Add(new S_A__UserRes() { ResID = item.ID, UserID = userID });
            }
        }

    }
}
