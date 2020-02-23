using Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OEMSzsowAPI.Common
{
    public class GlobalData
    {
        static Dictionary<string, string> _PhaseEnum;
        public static Dictionary<string, string> PhaseEnum
        {
            get
            {
                if (_PhaseEnum == null)
                {
                    var _BusinessConfigSQLHelper = SQLHelper.CreateSqlHelper("ProjectBaseConfig");
                    var dt = _BusinessConfigSQLHelper.ExecuteDataTable("select distinct code,name from S_D_WBSAttrDefine where Type = 'Phase'");
                    _PhaseEnum = new Dictionary<string, string>();
                    foreach (DataRow item in dt.Rows)
                        _PhaseEnum.Add(item["code"].ToString(), item["name"].ToString());
                }
                return _PhaseEnum;
            }
        }

        static Dictionary<string, string> _MajorEnum;
        public static Dictionary<string, string> MajorEnum
        {
            get
            {
                if (_MajorEnum == null)
                {
                    var _BusinessConfigSQLHelper = SQLHelper.CreateSqlHelper("ProjectBaseConfig");
                    var dt = _BusinessConfigSQLHelper.ExecuteDataTable("select distinct code,name from S_D_WBSAttrDefine where Type = 'Major'");
                    _MajorEnum = new Dictionary<string, string>();
                    foreach (DataRow item in dt.Rows)
                        _MajorEnum.Add(item["code"].ToString(), item["name"].ToString());
                }
                return _MajorEnum;
            }
        }

        static DataTable _UserDt;
        public static DataTable UserDt
        {
            get
            {
                if (_UserDt == null)
                {
                    var _BaseSQLHelper = SQLHelper.CreateSqlHelper(ConnEnum.Base);
                    _UserDt = _BaseSQLHelper.ExecuteDataTable("select ID,Name from S_A_User ");
                }
                return _UserDt;
            }
        }
    }
}
