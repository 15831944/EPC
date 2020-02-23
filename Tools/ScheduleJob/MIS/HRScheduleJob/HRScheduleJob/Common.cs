using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using System.Data;

namespace HRScheduleJob
{
    public static class Common
    {
        #region 生成ID

        /// <summary>
        /// 创建一个按时间排序的Guid
        /// </summary>
        /// <returns></returns>
        public static string CreateGuid()
        {
            //CAST(CAST(NEWID() AS BINARY(10)) + CAST(GETDATE() AS BINARY(6)) AS UNIQUEIDENTIFIER)
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime now = DateTime.Now;

            DateTime baseDate = new DateTime(1900, 1, 1);

            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);

            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            Array.Copy(daysArray, 0, guidArray, 2, 2);
            //毫秒高位
            Array.Copy(msecsArray, 2, guidArray, 0, 2);
            //毫秒低位
            Array.Copy(msecsArray, 0, guidArray, 4, 2);
            return new System.Guid(guidArray).ToString();
        }

        public static DateTime GetDateTimeFromGuid(string strGuid)
        {
            Guid guid = Guid.Parse(strGuid);

            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);

            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);

            return date;
        }

        #endregion

        /// <summary>
        /// 获取接收提醒的人
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<UserInfo> GetReceivePeople(string type)
        {
            List<UserInfo> list = new List<UserInfo>();
            if (type == "HumanResource")
                list = GetHumanResourseDeptLeader();

            return list;
        }

        private static List<UserInfo> GetHumanResourseDeptLeader()
        {

            SQLHelper baseHelper = SQLHelper.CreateSqlHelper("Base");
            string sql = @" select * from dbo.S_A_User u 
where exists (
select * from dbo.S_A__OrgUser ou where OrgID in
    (select org.ID from (select * from S_A_Org where Type='Post' ) org 
	    Join (select * from dbo.S_A_Org where name like '%人力资源%' and Type!='Post') parentOrg on  parentOrg.ID=org.ParentID
	    JOin (select OrgID from dbo.S_A__OrgRole where RoleID in (select ID from dbo.S_A_Role where name like '%部门经理%')) roleOrg ON roleOrg.OrgID=org.ID
	) 
	and ou.UserID=u.ID)";
            DataTable dt = baseHelper.ExecuteDataTable(sql);

            List<UserInfo> list = new List<UserInfo>();
            foreach (DataRow row in dt.Rows)
            {
                UserInfo user = new UserInfo()
                {
                    UserID = row["ID"].ToString(),
                    UserName = row["Name"].ToString(),
                    UserOrgID = row["DeptID"].ToString(),
                    UserOrgName = row["DeptName"].ToString()

                };
                list.Add(user);
            }

            return list;
        }
    }
}
