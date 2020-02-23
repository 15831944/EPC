using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.ComponentModel;
using Config;
using System.Data;
using System.Configuration;

namespace HRScheduleJob
{
    [Description("雇员合同到期提醒")]
    public class EmployeeContractWarningJob : IJob
    {
        /// <summary>
        /// 定时作业显示周期，即轮循周期
        /// </summary>
        int CYCLEDAYS = Int32.Parse(ConfigurationManager.AppSettings["EmployeeContractCycleDays"]);

        /// <summary>
        /// 合同到期期限，即合同到期前多少天提醒
        /// </summary>
        int DEADLINEDAYS = Int32.Parse(ConfigurationManager.AppSettings["EmployeeContractDeadlineDays"]);

        /// <summary>
        /// 提醒类型
        /// </summary>
        string ALARMTYPE = "人力资源";
        /// <summary>
        /// 提醒标题
        /// </summary>
        string ALARMTITLE = "雇员合同到期提醒";

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
        string ALARMURL = "/hr/EmployeeMessage/EmployeeSearch/EmployeeContractSearh";


        SQLHelper hrHelper = SQLHelper.CreateSqlHelper(ConnEnum.HR);
        SQLHelper baseHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
        string INSERTSQL = " Insert into dbo.S_S_Alarm (ID,AlarmType,Important,Urgency,AlarmTitle,AlarmContent,AlarmUrl,OwnerID,OwnerName,SendTime,DeadlineTime)Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')      ";


        public void Execute(IJobExecutionContext context)
        {
            List<UserInfo> userList = Common.GetReceivePeople("HumanResource");
            if (userList.Count == 0)
                return;

            //找到员工最后签订的合同并判断是否到期（到期）
            string sql = string.Format(@"select employeeContract.DayDiff,employee.* from 
(select EmployeeID,DateDiff(day,getdate(),max(ContractEndDate)) as DayDiff 
from S_A_EmployContract  group by EmployeeID having DateDiff(day,getdate(),max(ContractEndDate)) <={0}) employeeContract
Join (Select * from dbo.S_A_Employee ) employee ON employee.ID=employeeContract.EmployeeID
", DEADLINEDAYS);
            DataTable dt = hrHelper.ExecuteDataTable(sql);

            if (dt.Rows.Count == 0)
                return;

            string employeeLimiting = "";//快要到期
            string emlpoyeeLimited = "";//已经到期
            foreach (DataRow row in dt.Rows)
            {
                if (Int32.Parse(row["DayDiff"].ToString()) <= 0)
                    emlpoyeeLimited += row["Name"].ToString() + ",";
                else if (Int32.Parse(row["DayDiff"].ToString()) > 0)
                    employeeLimiting += row["Name"].ToString() + ",";
            }

            string content = "";
            if (!string.IsNullOrEmpty(emlpoyeeLimited))
                content += string.Format("员工：{0}的雇佣合同已经到期,请尽快续签。", emlpoyeeLimited.TrimEnd(','));
            if (!string.IsNullOrEmpty(employeeLimiting))
                content += string.Format("员工：{0}的雇佣合同即将到期，请及时做好续签准备工作。", employeeLimiting.TrimEnd(','));

            string updateSql = "";
            foreach (var user in userList)
            {
                updateSql += string.Format(INSERTSQL, Common.CreateGuid(), ALARMTYPE, IMPORTANT, URGENCY, ALARMTITLE, content, ALARMURL, user.UserID, user.UserName, DateTime.Now, DateTime.Now.AddDays(CYCLEDAYS));
            }

            baseHelper.ExecuteNonQuery(updateSql);
        }
    }
}
