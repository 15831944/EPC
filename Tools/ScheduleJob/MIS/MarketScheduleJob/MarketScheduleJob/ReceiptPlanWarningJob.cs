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
    [Description("计划收款预警")]
    public class ReceiptPlanWarningJob : IJob
    {
        /// <summary>
        /// 提醒类型
        /// </summary>
        string ALARMTYPE = "市场经营";
        /// <summary>
        /// 提醒标题
        /// </summary>
        string ALARMTITLE = "计划收款预警";
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
            DataRow dr = common.GetAlarmConfigRow(MarketScheduleJob.Enum.AlarmTypeEnum.ReceiptPlanWarning.ToString());
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

            //需要提醒的客户的Where条件
            string yearMonth = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');
            string where = string.Format(" Where PlanReceiptYearMonth<='{0}' And ID Not IN (Select distinct PlanReceiptID from S_C_ReceiptPlanReceiptRelation) ", yearMonth);

            //获取需要提醒的客户信息
            string alarmSql = string.Format("Select * From dbo.S_C_PlanReceipt {0}", where);
            DataTable dt = marketHelper.ExecuteDataTable(alarmSql);

            //获取扩展（表单）接收人字典
            Dictionary<string, string> dic = common.GetAlarmExendPerson("S_C_PlanReceipt", dr[ALARMEXENDPERSONIDFIELD].ToString(), dr[ALARMEXENDPERSONNAMEFIELD].ToString(), where);

            //获取需要提醒的计划收款的合同
            string contractSql = string.Format("Select distinct ContractID,ContractName From dbo.S_C_PlanReceipt {0}", where);
            DataTable dtContract = marketHelper.ExecuteDataTable(alarmSql);

            //指定接收人提醒
            StringBuilder alarmPersonContent = new StringBuilder("");
            if (dr[ALARMPERSONID] != null)
            {
                //遍历需要提醒的计划收款所属的合同
                for (int i = 0; i < dtContract.Rows.Count; i++)
                {
                    if (dtContract.Rows[i]["ContractID"] == null || dtContract.Rows[i]["ContractID"].ToString() == "")
                        continue;

                    DataRow[] drPlanReceipt = dt.Select(string.Format(" ContractID='{0}' ", dtContract.Rows[i]["ContractID"].ToString()));
                    alarmPersonContent.AppendFormat("合同\"{0}\"有计划收款：</br>", dt.Rows[i]["ContractName"].ToString());
                    foreach (var item in drPlanReceipt)
                        alarmPersonContent.AppendFormat("计划名称：{0}（￥{1}）；</br>", item["Name"], item["Amount"]);

                    alarmPersonContent.Append("</br>");
                }

                //提醒内容不为空时
                if (!string.IsNullOrEmpty(alarmPersonContent.ToString()))
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
                StringBuilder alarmExendPersonContent = new StringBuilder("");
                foreach (var item in dic)
                {
                    //遍历需要提醒的计划收款所属的合同
                    for (int i = 0; i < dtContract.Rows.Count; i++)
                    {
                        if (dtContract.Rows[i]["ContractID"] == null || dtContract.Rows[i]["ContractID"].ToString() == "")
                            continue;

                        DataRow[] drPlanReceipt = dt.Select(string.Format(" ContractID='{0}' And {1} ", dtContract.Rows[i]["ContractID"].ToString(), common.GetAlarmExendPersonIDWhereFormat(dr[ALARMEXENDPERSONIDFIELD].ToString(), item.Key)));
                        alarmExendPersonContent.AppendFormat("合同\"{0}\"有计划收款：</br>", dt.Rows[i]["ContractName"].ToString());
                        foreach (var pr in drPlanReceipt)
                            alarmExendPersonContent.AppendFormat("计划名称：{0}（￥{1}）；</br>", pr["Name"], pr["Amount"]);

                        alarmPersonContent.Append("</br>");
                    }

                    //提醒内容不为空时
                    if (!string.IsNullOrEmpty(alarmExendPersonContent.ToString()))
                        updateSql.AppendFormat(INSERTSQL, Common.CreateGuid(), ALARMTYPE, important, urgency, ALARMTITLE, alarmExendPersonContent, URL, item.Key, item.Value, DateTime.Now, DateTime.Now.AddDays(Convert.ToInt32(dr[DEADLINEDAYS])));
                }
            }
            if (!string.IsNullOrEmpty(updateSql.ToString()))
                baseHelper.ExecuteNonQuery(updateSql.ToString());

        }

    }
}
