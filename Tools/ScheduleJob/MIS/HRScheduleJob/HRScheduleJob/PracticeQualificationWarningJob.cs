using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Quartz;
using System.Configuration;
using Config;
using System.Data;

namespace HRScheduleJob
{
    [Description("执业资格到期提醒")]
    public class PracticeQualificationWarningJob : IJob
    {
        /// <summary>
        /// 定时作业显示周期，即轮循周期
        /// </summary>
        int CYCLEDAYS = Int32.Parse(ConfigurationManager.AppSettings["PracticeQualificationCycleDays"]);

        /// <summary>
        /// 合同到期期限，即合同到期前多少天提醒
        /// </summary>
        int DEADLINEDAYS = Int32.Parse(ConfigurationManager.AppSettings["PracticeQualificationDeadlineDays"]);

        /// <summary>
        /// 提醒类型
        /// </summary>
        string ALARMTYPE = "人力资源";
        /// <summary>
        /// 提醒标题
        /// </summary>
        string ALARMTITLE = "执业资格到期提醒";

        /// <summary>
        /// 重要程度（1重要，0不重要）
        /// </summary>
        string IMPORTANT = "1";

        /// <summary>
        /// 紧急度（1紧急，0不紧急）
        /// </summary>
        string URGENCY = "0";

        /// <summary>
        /// 提醒链接地址
        /// </summary>
        string ALARMURL = "/hr/EmployeeMessage/EmployeeSearch/PracticeQualificationSearh";

        SQLHelper hrHelper = SQLHelper.CreateSqlHelper(ConnEnum.HR);
        SQLHelper baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
        string INSERTSQL = " Insert into dbo.S_S_Alarm (ID,AlarmType,Important,Urgency,AlarmTitle,AlarmContent,AlarmUrl,OwnerID,OwnerName,SendTime,DeadlineTime)Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')      ";


        public void Execute(IJobExecutionContext context)
        {
            List<UserInfo> userList = Common.GetReceivePeople("HumanResource");
            if (userList.Count == 0)
                return;

            //找到员工执业资格是否到期（到期）
            string sqlEmployee = string.Format(@"select distinct employee.ID,employee.Name from (Select *,DateDiff(day,getdate(),RegistelLoseDate) as DayDiff from dbo.S_A_PracticeQualification pq where  DateDiff(day,getdate(),RegistelLoseDate) <={0}) pq
Join (Select * from dbo.S_A_Employee where isdeleted='0') employee ON employee.ID=pq.EmployeeID
", DEADLINEDAYS);

            //            string sqlQualification = string.Format(@"select pq.DayDiff,pq.QualificationName,employee.ID as EmployeeID from (Select *,DateDiff(day,getdate(),RegistelLoseDate) as DayDiff from dbo.S_A_PracticeQualification pq where  DateDiff(day,getdate(),RegistelLoseDate) <={0}) pq
            //Join (Select * from dbo.S_A_Employee where isdeleted='0') employee ON employee.ID=pq.EmployeeID
            //", DEADLINEDAYS);
            DataTable dtEmployee = hrHelper.ExecuteDataTable(sqlEmployee);
            //DataTable dtQualification = hrHelper.ExecuteDataTable(sqlQualification);

            if (dtEmployee.Rows.Count == 0)
                return;


            string content = "员工：";
            foreach (DataRow row in dtEmployee.Rows)
            {
                content += row["Name"] + ",";
            }

            content = content.TrimEnd(',') + "有失效或已失效的执业资格，请及时处理。";


            string updateSql = "";
            foreach (var user in userList)
            {
                updateSql += string.Format(INSERTSQL, Common.CreateGuid(), ALARMTYPE, IMPORTANT, URGENCY, ALARMTITLE, content, ALARMURL, user.UserID, user.UserName, DateTime.Now, DateTime.Now.AddDays(CYCLEDAYS));
            }

            baseHelper.ExecuteNonQuery(updateSql);
        }
    }
}
