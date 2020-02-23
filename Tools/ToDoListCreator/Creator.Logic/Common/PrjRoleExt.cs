using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Formula;
using System.Data;
using System.Configuration;


namespace Creator.Logic.Common
{
    public class PrjRoleExt
    {
        public static string GetRoleUserIDs(string roles, string instanceID)
        {
            return GetPrjRoleUserStr(FilterPrjRoleCode(roles), instanceID);
        }


        private static string GetPrjRoleUserStr(string roleCodes, string prjID)
        {
        
                if (string.IsNullOrEmpty(roleCodes) || string.IsNullOrEmpty(prjID))
                    return "";

                string sql = "select distinct UserID from S_I_OBS_User where EngineeringInfoID='{0}' and RoleCode in('{1}') ";
                try
                {
                string majorWhere = "";
                if (prjID.Contains('|'))
                {
                    var majorValue = prjID.Split('|')[1];
                    prjID = prjID.Split('|')[0];
                    majorWhere = " and MajorCode in ('" + majorValue.Replace(",", "','") + "') ";
                }

                sql = string.Format(sql + majorWhere, prjID, roleCodes.Replace(",", "','"));
                SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Project", System.Configuration.ConfigurationManager.ConnectionStrings["Project"].ConnectionString);
                var dt = sqlHelper.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    var idArr = dt.AsEnumerable().Select(c => c["UserID"].ToString()).ToArray();
                    return string.Join(",", idArr);
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("sql语句执行出错:{0}", sql), ex);   
            }
        }

        private static string FilterPrjRoleCode(string roleIDs)
        {
            if (string.IsNullOrEmpty(roleIDs))
                return null;

            string prjRoleCodes = "";
            Guid guid = new Guid();
            foreach (var id in roleIDs.Split(','))
            {
                if (Guid.TryParse(id, out guid)) //项目角色肯定不是Guid格式
                    continue;
                prjRoleCodes += "," + id;
            }
            return prjRoleCodes.Trim(',');
        }

    }
}
