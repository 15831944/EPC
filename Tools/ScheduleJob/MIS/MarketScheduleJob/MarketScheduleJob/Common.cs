using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Config;

namespace MarketScheduleJob
{
    public class Common
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


        SQLHelper baseHelper = SQLHelper.CreateSqlHelper("Base");
        SQLHelper marketHelper = SQLHelper.CreateSqlHelper("Market");

        /// <summary>
        /// 是否允许发送消息(系统作业默认一天执行一次)
        /// </summary>
        /// <param name="cycle">周期</param>
        /// <param name="day">周期内第几天</param>
        /// <param name="state">状态</param>
        /// <param name="configKey">预警key</param>
        /// <returns></returns>
        public bool IsSend(string cycle, string day, string state, string configKey)
        {
            if (state == "OFF")
                return false;

            DateTime dt = DateTime.Now;
            int quarter = (dt.Month + 2) / 3;
            switch (cycle)
            {
                case "Year":
                    if (dt.DayOfYear.ToString() == day)
                        return true;
                    else
                        return false;
                case "Quarter":
                    if (new DateTime(DateTime.Now.Year, quarter * 3 - 2, 1).ToShortDateString() == DateTime.Now.ToShortDateString())
                        return true;
                    else
                        return false;
                case "Month":
                    if (dt.Day.ToString() == day)
                        return true;
                    else
                        return false;
                case "Week":
                    if (dt.DayOfWeek.ToString() == day)
                        return true;
                    else
                        return false;
                case "Day":
                default:
                    return true;
            }

        }


        /// <summary>
        /// 通过名字获取用户ID
        /// </summary>
        /// <returns></returns>
        public string GetIDByName(string name)
        {
            string id = "";
            Dictionary<string, string> man = new Dictionary<string, string>();

            string sql = string.Format("SELECT ID FROM S_A_User Where Name='{0}' And IsDeleted='0' ", name);
            DataTable dt = baseHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                id = dt.Rows[0]["ID"].ToString();

            return id;
        }

        /// <summary>
        /// 根据提醒类型返回该条提醒的配置信息
        /// </summary>
        /// <param name="alarmType">提醒类型</param>
        /// <returns></returns>
        public DataRow GetAlarmConfigRow(string alarmType)
        {
            string alarmConfigSql = string.Format("SELECT * From S_S_AlarmConfig Where ConfigKey='{0}' ", alarmType);
            DataTable dt = marketHelper.ExecuteDataTable(alarmConfigSql);
            if (dt.Rows.Count == 0)
                return null;
            else
                return dt.Rows[0];
        }


        /// <summary>
        /// 获取接收提醒人信息
        /// </summary>
        /// <param name="tableName"> 表格名</param>
        /// <param name="AlarmExendPersonIDFields">接收提醒人ID字段（可多个）</param>
        /// <param name="AlarmExendPersonNameFields">接收提醒人Name字段（可多个）</param>
        /// <returns></returns>
        public Dictionary<string, string> GetAlarmExendPerson(string tableName, string AlarmExendPersonIDFields, string AlarmExendPersonNameFields, string where)
        {
            //没有表单接收人时返回空值
            if (string.IsNullOrEmpty(AlarmExendPersonIDFields) || string.IsNullOrEmpty(AlarmExendPersonIDFields))
                return null;

            //表单接收人ID及Name字段不对应，无法精确接收人，返回空
            string[] idArray = AlarmExendPersonIDFields.Split(',');
            string[] nameArray = AlarmExendPersonNameFields.Split(',');
            if (idArray.Length != nameArray.Length)
                return null;

            //获取接收人记录
            string sql = string.Format("SELECT {0},{1} from {2} {3}", AlarmExendPersonIDFields.TrimEnd(','), AlarmExendPersonNameFields.TrimEnd(','), tableName, where);
            DataTable dt = marketHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
                return null;

            //将接收人塞进字典
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //遍历行记录
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //遍历行记录中“接收人ID”字段
                for (int j = 0; j < idArray.Length; j++)
                {
                    //接收人ID为空，继续下一条
                    if (dt.Rows[i][idArray[j]] == null || dt.Rows[i][idArray[j]].ToString() == "")
                        continue;

                    //接收人含多人，且以逗号隔开
                    string[] receiverIDArray = dt.Rows[i][idArray[j]].ToString().Split(',');
                    string[] receiverNameArray = dt.Rows[i][nameArray[j]].ToString().Split(',');

                    //遍历接收人，并将其写入字典
                    for (int receiverIDCount = 0; receiverIDCount < receiverIDArray.Length; receiverIDCount++)
                    {
                        //接收人ID为空，继续下一条；字典已包含接收人，继续下一条
                        if (receiverIDArray[receiverIDCount] == null || receiverIDArray[receiverIDCount].ToString() == "" || dic.ContainsKey(receiverIDArray[receiverIDCount].ToString()))
                            continue;

                        //添加接收人
                        dic.Add(receiverIDArray[receiverIDCount].ToString(), receiverNameArray[receiverIDCount].ToString());
                    }
                }
            }

            return dic;

        }


        /// <summary>
        /// 获取扩展（表单上）提醒人字段包含提醒人的Where语句
        /// </summary>
        /// <param name="alarmExendPersonIDFields">扩展提醒人ID字段</param>
        /// <param name="alramPersonIDs">制定提醒人ID</param>
        /// <returns></returns>
        public string GetAlarmExendPersonIDWhereFormat(string alarmExendPersonIDFields, string alramPersonIDs)
        {
            if (string.IsNullOrEmpty(alarmExendPersonIDFields))
                return "";

            string[] array = alarmExendPersonIDFields.ToString().Split(',');
            string where = " ";
            if (array.Length == 1)
                where += string.Format("{0} = '{1}' ", array[0], alramPersonIDs);
            else
                for (int i = 0; i < array.Length; i++)
                {
                    if (i == array.Length - 1)
                        where += string.Format("{0} in ('{1}') ", array[i], alramPersonIDs.Replace(",", "','"));
                    else
                        where += string.Format("{0} in ('{1}') or ", array[i], alramPersonIDs.Replace(",","','"));
                }
            if (!string.IsNullOrEmpty(where))
                where = "( " + where + " )";
            return where;
        }


        /// <summary>
        /// 通过周期类型返回周期天数
        /// </summary>
        /// <param name="cycle">周期</param>
        /// <returns></returns>
        public int GetWarningDayByCycle(string cycle)
        {
            switch (cycle)
            {
                case "Year":
                    return 365;
                case "Quarter":
                    return 90;
                case "Month":
                    return 30;
                case "Week":
                    return 7;
                case "Day":
                default:
                    return 1;
            }
        }
    }
}
