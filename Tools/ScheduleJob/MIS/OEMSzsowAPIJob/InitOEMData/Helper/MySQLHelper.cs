using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MySql.Data.MySqlClient;

namespace InitOEMData.Helper
{
    class MySQLHelper
    {

        #region 静态构造方法

        private static Dictionary<string, object> _dic = new Dictionary<string, object>();
        public static MySQLHelper CreateSqlHelper(string connName)
        {
            string key = string.Format("Conn_{0}", connName);

            MySQLHelper sqlHelper = null;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[key] != null)
                    return HttpContext.Current.Items[key] as MySQLHelper;

                if (System.Configuration.ConfigurationManager.ConnectionStrings[connName] == null)
                    throw new Exception(string.Format("配置文件中不包含数据库连接字符串：{0}", connName));

                sqlHelper = new MySQLHelper(ConfigurationManager.ConnectionStrings[connName].ConnectionString);
                HttpContext.Current.Items[key] = sqlHelper;
            }
            else
            {
                if (_dic.Keys.Contains(key))
                    return _dic[key] as MySQLHelper;

                sqlHelper = new MySQLHelper(ConfigurationManager.ConnectionStrings[connName].ConnectionString);
                _dic[key] = sqlHelper;
            }

            return sqlHelper;
        }

        public static MySQLHelper CreateSqlHelper(string connName, string connString)
        {
            string key = string.Format("Conn_{0}", connName);

            MySQLHelper sqlHelper = null;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[key] != null)
                    return HttpContext.Current.Items[key] as MySQLHelper;

                if (String.IsNullOrEmpty(connString))
                    throw new Exception(string.Format("连接字符串不能为空值：{0}", connName));
                sqlHelper = new MySQLHelper(connString);
                HttpContext.Current.Items[key] = sqlHelper;
            }
            else
            {
                if (_dic.Keys.Contains(key))
                    return _dic[key] as MySQLHelper;
                if (String.IsNullOrEmpty(connString))
                    throw new Exception(string.Format("连接字符串不能为空值：{0}", connName));
                sqlHelper = new MySQLHelper(connString);
                _dic[key] = sqlHelper;
            }

            return sqlHelper;
        }
        #endregion
        
        #region 常量
        // 数据库连接字符串
        private string connStr = "";
        private string _dbName = "";

        public string ConnStr
        {
            get
            {
                return connStr;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public string ShortName
        {
            get
            {
                return Name.Remove(0, Name.LastIndexOf('_') + 1);
            }
        }

        public string DbName
        {
            get
            {
                if (_dbName == "")
                {
                    if (connStr == "")
                        _dbName = ""; ;
                    MySqlConnection conn = new MySqlConnection(connStr);
                    _dbName = conn.Database;
                }
                return _dbName;
            }
        }
        #endregion

        #region 构造函数
        public MySQLHelper(string connStr)
        {
            this.connStr = connStr;
        }
        #endregion

        #region 私有方法

        public T RowToObj<T>(DataRow row, T obj = null) where T : class,new()
        {
            if (obj == null)
                obj = new T();

            foreach (var pi in typeof(T).GetProperties())
            {
                if (!row.Table.Columns.Contains(pi.Name))
                    continue;
                if (row[pi.Name] != System.DBNull.Value)
                    pi.SetValue(obj, row[pi.Name], null);
            }

            return obj;
        }

        #endregion

        #region 返回DataTable可操作的方法
        
        //返回DataTable的SQL执行
        public DataTable ExecuteDataTable(string cmdText)
        {
            DataTable dt = new DataTable();

            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlDataAdapter apt = new MySqlDataAdapter(cmdText, conn);
            apt.SelectCommand.CommandType = CommandType.Text;
            try
            {
                apt.Fill(dt);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        #endregion
    }

}
