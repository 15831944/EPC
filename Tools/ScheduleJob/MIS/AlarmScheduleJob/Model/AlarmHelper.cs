using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using Config.Logic;
using System.Text.RegularExpressions;
using System.Data;

namespace Alarm
{
    public class AlarmHelper
    {
        private static DataTable _allUsers;
        public static DataTable AllUsers
        {
            get
            {
                if (_allUsers == null)
                {
                    var sql = "select ID,Name from S_A_User where IsDeleted='0' or IsDeleted is null";
                    var db = SQLHelper.CreateSqlHelper(ConnEnum.Base);
                    _allUsers = db.ExecuteDataTable(sql);
                }
                return _allUsers;
            }
        }

        public static void SendAlarmMulti(string alarmTitle, string alarmContent, string alarmUrl, 
            List<Dictionary<string,object>> users, DateTime? deadlineTime, string alarmType, string projectInfoID, string formCode,
            UserInfo sendUser, Dictionary<string, object> dic = null, bool isImportant = false, bool isUrgency = false)
        {
            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                if (String.IsNullOrEmpty(user.GetValue("UserID"))) continue;
                SendAlarm(alarmTitle, alarmContent, alarmUrl, user.GetValue("UserName"), user.GetValue("UserID"), deadlineTime, alarmType, projectInfoID, formCode, sendUser, dic, isImportant,
                    isUrgency);
            }
        }

        public static void SendAlarmMulti(string alarmTitle, string alarmContent, string alarmUrl, string ownerNames,
            string ownerIDs, DateTime? deadlineTime, string alarmType, string projectInfoID, string formCode, 
            UserInfo sendUser, Dictionary<string, object> dic = null, bool isImportant = false, bool isUrgency = false)
        {
            var userIDs = ownerIDs.Split(',');
            var userNames = ownerNames.Split(',');
            for (int i = 0; i < userIDs.Length; i++)
			{
                SendAlarm(alarmTitle, alarmContent, alarmUrl, userNames[i], userIDs[i], deadlineTime, alarmType, projectInfoID, formCode, sendUser, dic, isImportant,
                    isUrgency);
			}
        }

        /// <summary>
        /// 发送提醒
        /// </summary>
        /// <param name="alarmTitle">提醒标题</param>
        /// <param name="alarmContent">正文内容</param>
        /// <param name="alarmUrl">事务地址</param>
        /// <param name="ownerName">提醒人</param>
        /// <param name="ownerID">提醒人ID</param>
        /// <param name="deadlineTime">提醒截止时间（过期日期）</param>
        /// <param name="alarmType">提醒模块名，比如经营提醒、OA提醒等等文本</param>
        /// <param name="projectInfoID">关联ID</param>
        /// <param name="formCode">关联Code</param>
        /// <param name="sendUser">发送人</param>
        /// <param name="isImportant">是否重要，默认否.</param>
        /// <param name="isUrgency">是否紧急,默认否.</param>
        public static void SendAlarm(string alarmTitle, string alarmContent, string alarmUrl, string ownerName, string ownerID, DateTime? deadlineTime, string alarmType, string projectInfoID, string formCode, UserInfo sendUser, Dictionary<string, object> dic = null, bool isImportant = false, bool isUrgency = false)
        {
            if (dic != null)
            {
                Regex reg = new Regex("\\{[0-9a-zA-Z_\u4e00-\u9faf]*\\}");
                alarmTitle = reg.Replace(alarmTitle, (Match m) =>
                {
                    string value = m.Value.Trim('{', '}');

                    if (dic != null && dic.ContainsKey(value))
                        return dic[value].ToString();
                    else
                        return value;
                });
                alarmContent = reg.Replace(alarmContent, (Match m) =>
                {
                    string value = m.Value.Trim('{', '}');

                    if (dic != null && dic.ContainsKey(value))
                        return dic[value].ToString();
                    else
                        return value;
                });
                alarmUrl = reg.Replace(alarmUrl, (Match m) =>
                {
                    string value = m.Value.Trim('{', '}');

                    if (dic != null && dic.ContainsKey(value))
                        return dic[value].ToString();
                    else
                        return value;
                });
            }

            alarmTitle = alarmTitle.Replace("'", "''");
            alarmContent = alarmContent.Replace("'", "''");

            string alarmID = GuidHelper.CreateGuid();

            string userID = "";
            string userName = "系统";

            if (sendUser != null)
            {
                userID = sendUser.UserID;
                userName = sendUser.UserName;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(alarmSql,
               alarmID,
               isImportant ? "1" : "0",
               isUrgency ? "1" : "0",
               alarmType,
               alarmTitle,
               alarmContent,
               alarmUrl,
               ownerName,
               ownerID,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            deadlineTime == null ? "null" : Convert.ToDateTime(deadlineTime).ToString("yyyy-MM-dd HH:mm:ss"),
            userName,
            userID,
            projectInfoID,
            formCode);

            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);

            if (Config.Constant.IsOracleDb)
            {
                string sql = sb.ToString();
                sql = sql.Replace("\r\n", "").Replace("\n", "");
                sql = "begin " + sql + " end;";
                sqlHelper.ExecuteNonQuery(sql);
            }
            else
            {
                sqlHelper.ExecuteNonQuery(sb.ToString());
            }
        }
        static string alarmSql
        {
            get
            {
                string sql = @" INSERT INTO S_S_Alarm(ID, Important, Urgency, AlarmType, AlarmTitle, AlarmContent, AlarmUrl, OwnerName, OwnerID, SendTime, DeadlineTime, SenderName, SenderID,ProjectInfoID,FormCode,IsDelete)
VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','0');";
                return sql;
            }
        }
    }
}