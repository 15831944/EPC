using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Config;
using System.Data;
using System.ComponentModel;

namespace MarketScheduleJob
{
    [Description("重点客户联系预警")]
    public class ImportantCustomerLinkCustomerWarningJob : IJob
    {
        /// <summary>
        /// 提醒类型
        /// </summary>
        string ALARMTYPE = "市场经营";
        /// <summary>
        /// 提醒标题
        /// </summary>
        string ALARMTITLE = "重点客户联系预警";
        /// <summary>
        /// 指定提醒人
        /// </summary>
        string ALARMPERSON = "AlarmPerson";
        /// <summary>
        /// 指定提醒人ID
        /// </summary>
        string ALARMPERSONID = "AlarmPersonID";
        /// <summary>
        /// 扩展提醒人（表单上提醒人）ID字段
        /// </summary>
        string ALARMEXENDPERSONIDFIELD = "AlarmExendPersonIDField";
        /// <summary>
        /// 扩展提醒人（表单上提醒人）名称字段
        /// </summary>
        string ALARMEXENDPERSONNAMEFIELD = "AlarmExendPersonNameField";
        /// <summary>
        /// 预警天数
        /// </summary>
        string DEADLINEDAYS = "DeadlineDays";

        SQLHelper marketHelper = SQLHelper.CreateSqlHelper("Market");
        SQLHelper baseHelper = SQLHelper.CreateSqlHelper("Base");
        string INSERTSQL = " Insert into dbo.S_S_Alarm (ID,AlarmType,Important,Urgency,AlarmTitle,AlarmContent,AlarmUrl,OwnerID,OwnerName,SendTime,DeadlineTime)Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')  ";
        Common common = new Common();



        public void Execute(IJobExecutionContext context)
        {
            StringBuilder updateSql = new StringBuilder();
            //获取提醒配置信息（市场经营内配置页面）
            DataRow dr = common.GetAlarmConfigRow(MarketScheduleJob.Enum.AlarmTypeEnum.ImportantCustomerLinkCustomerWarning.ToString());
            //无配置信息时不提醒
            if (dr == null)
                return;

            string URL = dr["AlarmUrl"] == null ? "" : dr["AlarmUrl"].ToString();//快捷入口
            //不允许发送消息则返回
            string cycle = dr["Cycle"] == null ? "" : dr["Cycle"].ToString();//提醒周期
            string day = dr["Day"] == null ? "" : dr["Day"].ToString();//周期中的第几天
            string state = dr["State"] == null ? "" : dr["State"].ToString();//是否提醒：ON、OFF
            string configKey = dr["ConfigKey"] == null ? "" : dr["ConfigKey"].ToString();//提醒标识
            string important = dr["Important"] == null ? "" : dr["Important"].ToString();//重要度
            string urgency = dr["Urgency"] == null ? "" : dr["Urgency"].ToString();//紧急度
            if (!common.IsSend(cycle, day, state, configKey))
                return;


            //没有配置接收人,不提醒
            if ((dr[ALARMPERSONID] == null || dr[ALARMPERSONID].ToString() == "") && (dr[ALARMEXENDPERSONIDFIELD] == null || dr[ALARMEXENDPERSONIDFIELD].ToString() == ""))
                return;

            //获取需要提醒的客户信息的Where条件
            int addDays = common.GetWarningDayByCycle(cycle);//周期天数
            string date = DateTime.Now.AddDays(-addDays).Year.ToString() + DateTime.Now.AddDays(-addDays).Month.ToString().PadLeft(2, '0') + DateTime.Now.AddDays(-addDays).Day.ToString().PadLeft(2, '0');
            string where = string.Format(" Where CONVERT(varchar(100), CreateDate, 112) <'{0}' And IsImportantCustomer='是' And ID NOT IN (SELECT Distinct CustomerID From dbo.S_F_CustomerContactLog)", date); ;

            //获取需要提醒的客户信息
            string alarmSql = string.Format("SELECT * From  dbo.S_F_Customer {0}", where);
            DataTable dt = marketHelper.ExecuteDataTable(alarmSql);

            //获取提醒扩展（表单）接收人字典
            Dictionary<string, string> dic = common.GetAlarmExendPerson("S_F_Customer", dr[ALARMEXENDPERSONIDFIELD].ToString(), dr[ALARMEXENDPERSONNAMEFIELD].ToString(), where);


            //指定接收人提醒
            StringBuilder alarmPersonContent = new StringBuilder(string.Format("自登记日起,{0}天内未与以下重点客户联系：</br>", addDays));
            if (dr[ALARMPERSONID] != null)
            {
                //指定接收人接收的客户信息
                for (int i = 0; i < dt.Rows.Count; i++)
                    alarmPersonContent.AppendFormat("客户名称：{0},</br>客户编号：{1}；</br></br>", dt.Rows[i]["Name"], dt.Rows[i]["Code"]);

                //提醒内容不为空时
                if (alarmPersonContent.ToString() != string.Format("自登记日起,{0}天内未与以下重点客户联系：</br>", addDays))
                {
                    //每个接收人发一条提醒
                    string[] alarmPersonIDArray = dr[ALARMPERSONID].ToString().Split(',');
                    string[] alarmPersonNameArray = dr[ALARMPERSON].ToString().Split(',');
                    for (int i = 0; i < alarmPersonIDArray.Length; i++)
                    {
                        //添加更新语句
                        updateSql.AppendFormat(INSERTSQL, Common.CreateGuid(), ALARMTYPE, important, urgency, ALARMTITLE, alarmPersonContent, URL, alarmPersonIDArray[i], alarmPersonNameArray[i], DateTime.Now, DateTime.Now.AddDays(Convert.ToInt32(dr[DEADLINEDAYS])));

                        //扩展（表单上）提醒人出现在指定提醒人中时，删除他（她）.
                        if (dic != null)
                            dic.Remove(alarmPersonIDArray[i]);
                    }
                }
            }



            //扩展（表单上）提醒人移除指定提醒人后为空，则执行更新语句.
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    DataRow[] customerRows = dt.Select(common.GetAlarmExendPersonIDWhereFormat(dr[ALARMEXENDPERSONIDFIELD].ToString(), item.Key));

                    //指定接收人提醒客户
                    StringBuilder alarmExendPersonContent = new StringBuilder(string.Format("自登记日起,{0}天内未与以下重点客户联系：</br>", addDays));
                    foreach (var row in customerRows)
                        alarmExendPersonContent.AppendFormat("客户名称：{0},</br>客户编号：{1}；</br></br>", row["Name"], row["Code"]);

                    //提醒内容不为空时返回
                    if (alarmExendPersonContent.ToString() != string.Format("自登记日起,{0}天内未与以下重点客户联系：</br>", addDays))
                        updateSql.AppendFormat(INSERTSQL, Common.CreateGuid(), ALARMTYPE, important, urgency, ALARMTITLE, alarmExendPersonContent, URL, item.Key, item.Value, DateTime.Now, DateTime.Now.AddDays(Convert.ToInt32(dr[DEADLINEDAYS])));
                }
            }

            if (!string.IsNullOrEmpty(updateSql.ToString()))
                baseHelper.ExecuteNonQuery(updateSql.ToString());
        }


    }
}
