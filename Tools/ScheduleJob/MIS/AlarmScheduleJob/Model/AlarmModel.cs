using Config;
using Formula.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Config.Logic;
using Formula;
using System.Text.RegularExpressions;

namespace Alarm.Model
{
    public class AlarmModel
    {
        #region 属性
        List<Dictionary<string, object>> _configreceivers;
        public List<Dictionary<string, object>> ConfigReceivers
        {
            get
            {
                if (_configreceivers == null)
                    _configreceivers = new List<Dictionary<string, object>>();
                return _configreceivers;
            }
        }

        List<Dictionary<string, object>> _instanceReceivers;
        public List<Dictionary<string, object>> instanceReceivers
        {
            get
            {
                if (_instanceReceivers == null)
                    _instanceReceivers = new List<Dictionary<string, object>>();
                return _instanceReceivers;
            }
        }

        public string AlarmContentTemplate
        {
            get;
            set;
        }

        public string Connection { get; set; }

        public string Condition { get; set; }

        public string TableName { get; set; }

        public string PlanDateField { get; set; }

        public string FinishDateField { get; set; }

        public string ProjectInfoIDField { get; set; }

        public string ReceiverIDField { get; set; }

        public DataTable AlarmDataSource
        { get; set; }

        public int AlarmFrequency
        { get; set; }

        public SQLHelper DB
        {
            get;
            set;
        }

        public SQLHelper BaseDB
        {
            get
            {
                return SQLHelper.CreateSqlHelper(ConnEnum.Base);
            }
        }

        public SQLHelper ProjectDB
        {
            get
            {
                return SQLHelper.CreateSqlHelper(ConnEnum.Project);
            }
        }

        public string ConfigID { get; set; }

        public string LinkUrl { get; set; }

        public string Title { get; set; }

        public bool Important { get; set; }

        public bool Ugencry { get; set; }

        public string ReceiversString { get; set; }

        public DateTime? LastAlarmDate { get; set; }

        #endregion

        #region 构造函数
        public AlarmModel(DataRow configInfo)
        {
            InitModel(configInfo);
        }
        #endregion

        public void InitModel(DataRow configInfo)
        {
            this.Connection = configInfo["Connection"].ToString();
            this.DB = SQLHelper.CreateSqlHelper(this.Connection);
            this.TableName = configInfo["TableName"].ToString();
            this.PlanDateField = configInfo["PlanDateTimeField"].ToString();
            this.FinishDateField = configInfo["FinishDateTimeField"].ToString();
            this.AlarmContentTemplate = configInfo["ContentTemplate"].ToString();
            this.Condition = configInfo["Condition"].ToString();
            this.ProjectInfoIDField = configInfo["ProjectIDField"].ToString();
            var frequency = (Base.Logic.AlarmFrequency)Enum.Parse(typeof(Base.Logic.AlarmFrequency), configInfo["Frequency"].ToString());
            this.AlarmFrequency = Convert.ToInt32(frequency);
            this.Title = configInfo["Title"].ToString();
            this.LinkUrl = configInfo["LinkURL"].ToString();
            this.ReceiversString = configInfo["Receivers"].ToString();
            this.Important = false; this.Ugencry = false;
            this.ConfigID = configInfo["ID"].ToString();
            this.ReceiverIDField = configInfo["ReceiverIDField"].ToString();
            if (!String.IsNullOrEmpty(configInfo["IsImportant"].ToString()) && configInfo["IsImportant"].ToString() == "1")
                this.Important = true;
            if (!String.IsNullOrEmpty(configInfo["IsUrgency"].ToString()) && configInfo["IsUrgency"].ToString() == "1")
                this.Ugencry = true;
            if (!String.IsNullOrEmpty(configInfo["LastAlarmDate"].ToString()))
            {
                this.LastAlarmDate = Convert.ToDateTime(configInfo["LastAlarmDate"]);
            }
            string whereStr = " WHERE 1<>1";
            if (String.IsNullOrEmpty(this.Condition) && !String.IsNullOrEmpty(this.FinishDateField))
            {
                whereStr = " WHERE  " + this.FinishDateField + " IS NULL ";
            }
            else if (!String.IsNullOrEmpty(this.Condition))
            {
                if (this.Condition.ToUpper().IndexOf("WHERE") >= 0)
                {
                    whereStr = this.Condition;
                }
                else
                {
                    whereStr = " WHERE " + this.Condition;
                }
            }
            var sql = "SELECT * FROM {0} {1}";
            this.AlarmDataSource = DB.ExecuteDataTable(String.Format(sql, this.TableName, whereStr));
            this._setReceivers(this.ReceiversString);
        }

        public void Alarm()
        {
            //判定间隔，如果上次提醒日期为空，则认为是第一次提醒，直接发送
            if (this.LastAlarmDate.HasValue)
            {
                if (this.LastAlarmDate.Value.AddDays(this.AlarmFrequency) >= DateTime.Now)
                    return;
            }
            //循环数据源，进行提醒操作
            foreach (DataRow dataRow in this.AlarmDataSource.Rows)
            {
                #region 替换关键字
                string title = this.Title; string content = this.AlarmContentTemplate;
                string linkUrl = this.LinkUrl;

                while (title.Contains('{'))
                {
                    title = Tools.ReplaceString(title, dataRow);
                }
                while (linkUrl.Contains('{'))
                {
                    linkUrl = Tools.ReplaceString(linkUrl, dataRow);
                }
                while (content.Contains('{'))
                {
                    content = Tools.ReplaceString(content, dataRow);
                }
                #endregion

                //设置提醒的过期期限，如果计划日期大于当前日期，则过期期限为计划日期，否则为当前日期+7天（已过期的）
                var deadLine = DateTime.Now.AddDays(7);
                if (dataRow[this.PlanDateField] != null && dataRow[this.PlanDateField] != DBNull.Value && !String.IsNullOrEmpty(dataRow[this.PlanDateField].ToString()))
                {
                    var planDate = Convert.ToDateTime(dataRow[this.PlanDateField]);
                    if (planDate > DateTime.Now)
                        deadLine = planDate;
                }

                this.instanceReceivers.Clear();

                //初始化配置信息中的系统角色，人员和组织角色（由于这些角色固定，不需要动态数据获取，所以每个模板都是固定的，不要重复取）
                _initConfigReceivers();

                //动态获取项目角色信息
                var projectInfoID = string.Empty;
                if (dataRow.Table.Columns.Contains(this.ProjectInfoIDField))
                {
                    projectInfoID = dataRow[this.ProjectInfoIDField].ToString();
                    _setProjectReceivers(this.ReceiversString, dataRow);
                }

                //动态获取表单字段作为接收人
                if (!string.IsNullOrEmpty(this.ReceiverIDField))
                    foreach (var rIDField in this.ReceiverIDField.Split(','))
                        if (dataRow.Table.Columns.Contains(rIDField))
                            _setFormReceivers(rIDField, dataRow);

                AlarmHelper.SendAlarmMulti(title, content, linkUrl, this.instanceReceivers, deadLine, "", projectInfoID, "EarlyWarning", null, null, this.Important, this.Ugencry);
            }
            string sql = "UPDATE S_S_AlarmConfig SET LastAlarmDate ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE ID='" + this.ConfigID + "'";
            this.BaseDB.ExecuteNonQuery(sql);
        }

        #region 私有方法
        void _setReceivers(string configReceivers)
        {
            var list = JsonHelper.ToList(configReceivers);
            foreach (var item in list)
            {
                var type = item.GetValue("Type");
                if (type == "User")
                {
                    _addUser(this.ConfigReceivers, item.GetValue("ID"), item.GetValue("Name"));
                }
                else if (type == "SysRole" || type == "OrgRole")
                {
                    string sql = @"SELECT UserID,Name FROM  S_A__RoleUser LEFT JOIN S_A_User ON UserID=S_A_USER.ID WHERE RoleID ='" + item.GetValue("ID") + "'";
                    var userDt = BaseDB.ExecuteDataTable(sql);
                    foreach (DataRow userRow in userDt.Rows)
                    {
                        _addUser(this.ConfigReceivers, userRow["UserID"].ToString(), userRow["Name"].ToString());
                    }
                }
            }
        }

        void _addUser(List<Dictionary<string, object>> userList, string userID, string userName)
        {
            foreach (var receiver in userList)
            {
                if (receiver.GetValue("ID") == userID)
                    return;
            }
            var user = new Dictionary<string, object>();
            user.SetValue("UserID", userID);
            user.SetValue("UserName", userName);
            userList.Add(user);
        }

        void _initConfigReceivers()
        {
            foreach (var item in this.ConfigReceivers)
            {
                string UserID = "";
                string UserName = "";
                if (item.ContainsKey("UserID"))
                    UserID = item["UserID"].ToString();
                else if (item.ContainsKey("UserID"))
                {
                    UserID = item["UserID"].ToString();
                }
                if (item.ContainsKey("UserName"))
                    UserName = item["UserName"].ToString();
                else if (item.ContainsKey("Name"))
                {
                    UserName = item["Name"].ToString();
                }
                else if (item.ContainsKey("Name"))
                {
                    UserName = item["Name"].ToString();
                }
                _addUser(this.instanceReceivers, UserID, UserName);
            }
        }

        void _setProjectReceivers(string configReceivers, DataRow data)
        {
            if (!data.Table.Columns.Contains(this.ProjectInfoIDField)) return;
            var list = JsonHelper.ToList(configReceivers);
            var projectInfoID = data[this.ProjectInfoIDField].ToString();
            string roleCode = "";
            foreach (var item in list)
            {
                string sql = "SELECT DISTINCT UserID,UserName FROM S_W_OBSUser WHERE ProjectInfoID='{0}' AND RoleCode ='{1}'";
                var type = item.GetValue("Type");
                if (type != "ProjectRole") continue;
                if (String.IsNullOrEmpty(item.GetValue("Code"))) continue;
                if (!String.IsNullOrEmpty(item.GetValue("MajorField")) && data.Table.Columns.Contains(item.GetValue("MajorField")))
                {
                    sql += " AND MajorValue='" + data[item.GetValue("MajorField")].ToString() + "'";
                }
                var userDt = this.ProjectDB.ExecuteDataTable(String.Format(sql, projectInfoID, item.GetValue("Code")));
                foreach (DataRow row in userDt.Rows)
                {
                    _addUser(this.instanceReceivers, row["UserID"].ToString(), row["UserName"].ToString());
                }
            }
        }

        void _setFormReceivers(string idField, DataRow data)
        {
            var userIDs = data[idField].ToString();
            foreach (var userID in userIDs.Split(','))
            {
                var users = AlarmHelper.AllUsers.Select("ID='" + userID + "'");
                if (users.Length > 0)
                    _addUser(this.instanceReceivers, userID, users[0]["Name"].ToString());
            }
        }

        #endregion

    }
}
