using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Creator.Logic.Common
{
    public class UserService
    {
        public static string GetUserIDsInRoles(string roleIDs, string orgIDs)
        {
            if (orgIDs == null)
                orgIDs = "";

            string userIDs = "";
            DataTable dt = null;

            //过滤校验角色ID是否存在
            roleIDs = FilterRoleIDs(roleIDs);

            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base", System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ConnectionString);
            //获取系统角色下的人
            string sql = string.Format(@"select S_A_User.* from S_A_User join S_A__RoleUser on S_A_User.ID=UserID and RoleID in('{0}') join S_A_Role on RoleID=S_A_Role.ID and S_A_Role.Type='SysRole'"
                  , roleIDs.Replace(",", "','"));
            dt = sqlHelper.ExecuteDataTable(sql);
            userIDs += "," + string.Join(",", dt.AsEnumerable().Select(c => c["ID"].ToString()).ToArray());

            //获取继承的组织角色下的人
            foreach (string orgID in orgIDs.Split(','))
            {
                if (orgID == "")
                    continue;
                object fullID = sqlHelper.ExecuteScalar(string.Format("select FullID from S_A_Org where ID='{0}'", orgID));
                if (fullID == null)
                    continue;
                sql = string.Format(@"select S_A_User.ID from S_A_Org join S_A__OrgRole on FullID like '{0}%' and ID=OrgID  and roleID in('{1}') 
join S_A__OrgUser on S_A__OrgUser.OrgID=S_A_Org.ID join S_A_User on S_A_User.ID=S_A__OrgUser.UserID and S_A_User.IsDeleted='0'
union
select S_A_User.ID from S_A_Org join S_A__OrgRoleUser on FullID like '{0}%' and ID=OrgID  and roleID in('{1}') 
 join S_A_User on S_A_User.ID=UserID and S_A_User.IsDeleted='0'
", fullID, roleIDs.Replace(",", "','"));

                dt = sqlHelper.ExecuteDataTable(sql);

                //如果当前部门找不到组织角色用户，则查找上级部门
                if (dt.Rows.Count == 0 && fullID.ToString().Contains('.')) //组织角色下没有用户，递归找上一级组织角色下的人员
                {
                    dt = GetUpperOrgRole(fullID.ToString(), roleIDs);
                }

                userIDs += "," + string.Join(",", dt.AsEnumerable().Select(c => c["ID"].ToString()).ToArray());
            }

            //消除重复
            userIDs = string.Join(",", userIDs.Trim(',', ' ').Split(',').Distinct()).Trim(',');

            return userIDs;
        }

        public static string GetUserNames(string userIDs)
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base", System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ConnectionString);

            string sql = @"Select ID,Name from S_A_User where ID in ('{0}')";
            sql = string.Format(sql, userIDs.Replace(",", "','"));

            DataTable dt = sqlHelper.ExecuteDataTable(sql);

            string userName = "";
            var userIDArr = userIDs.Split(',');
            for (int i = 0; i < userIDArr.Length; i++)
            {
                if (userIDArr[i] == "")
                    userName += ",";
                else
                    userName += "," + dt.AsEnumerable().Where(c => c["ID"].ToString().ToLower() == userIDArr[i].ToLower()).SingleOrDefault()["Name"].ToString();
            }
            userName = userName.Trim(',');
            return userName;
        }

        private static DataTable GetUpperOrgRole(string fullID, string roleIDs)
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base", System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ConnectionString);
            string sql = "";
            if (fullID.Contains('.') == false)
            {
                return sqlHelper.ExecuteDataTable("select * from S_A_User where 1=2");
            }

            string upperDeptFullID = fullID.Remove(fullID.LastIndexOf('.'));//上级部门ID

            sql = string.Format(@"select S_A_User.ID from S_A_Org join S_A__OrgRole on FullID like '{2}%' and Type='Post' and len(FullID)<{3} and ID=OrgID  and roleID in('{1}') 
join S_A__OrgUser on S_A__OrgUser.OrgID=S_A_Org.ID join S_A_User on S_A_User.ID=S_A__OrgUser.UserID and S_A_User.IsDeleted='0' 
union
select S_A_User.ID from S_A_Org join S_A__OrgRoleUser on OrgID in ('{0}') and ID=OrgID  and roleID in('{1}') 
 join S_A_User on S_A_User.ID=UserID and S_A_User.IsDeleted='0'
", upperDeptFullID.ToString().Split('.').Last()
, roleIDs.Replace(",", "','")
, upperDeptFullID
, upperDeptFullID.ToString().Length + 38   //只向下查找一级岗位
);

            DataTable dt = sqlHelper.ExecuteDataTable(sql);

            if (dt.Rows.Count == 0)
                return GetUpperOrgRole(upperDeptFullID, roleIDs); //递归
            else
                return dt;
        }

        private static string FilterRoleIDs(string roleIDs)
        {
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Base", System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ConnectionString);
            string sql = string.Format("select ID from S_A_Role where ID in('{0}')", roleIDs.Replace(",", "','"));
            DataTable dt = sqlHelper.ExecuteDataTable(sql);

            var sysArr = dt.AsEnumerable().Select(c => c["ID"].ToString()).ToArray();
            var roleArr = roleIDs.Split(',');

            Guid guid = new Guid();
            foreach (var id in roleArr)
            {
                if (sysArr.Contains(id)) continue;
                if (Guid.TryParse(id, out guid)) //项目角色肯定不是Guid格式
                    throw new Exception(string.Format("角色不存在：{0}！", id));
            }
            return string.Join(",", sysArr);
        }

    }
}
