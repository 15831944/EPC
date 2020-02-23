using Config;
using Formula;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Alarm
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql = "SELECT * FROM S_S_AlarmConfig WHERE (State !='Valid' OR State IS NULL)";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.Base);
            var alarmDefTable = db.ExecuteDataTable(sql);
            foreach (DataRow def in alarmDefTable.Rows)
            {
                try
                {
                    var alarm = new Alarm.Model.AlarmModel(def);
                    alarm.Alarm();
                }
                catch (Exception exp)
                {
                    LogWriter.Error(exp.Message);
                }
            }
        }
    }
}