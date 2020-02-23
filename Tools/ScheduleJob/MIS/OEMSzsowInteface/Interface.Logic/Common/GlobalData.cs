using Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public class GlobalData
    {
        static List<S_A_User> _UserList;
        public static List<S_A_User> UserList
        {
            get
            {
                if (_UserList == null)
                {
                    var _InterfaceSQLHelper = SQLHelper.CreateSqlHelper("SzsowInteface");
                    var userSql = "select * from S_A_User where 1=1 ";
                    _UserList = _InterfaceSQLHelper.ExecuteList<S_A_User>(userSql);
                }
                return _UserList;
            }
        }

        static List<S_A_BaseEnum> _BaseEnumList;
        public static List<S_A_BaseEnum> BaseEnumList
        {
            get
            {
                if (_BaseEnumList == null)
                {
                    var _InterfaceSQLHelper = SQLHelper.CreateSqlHelper("SzsowInteface");
                    var sql = "select * from S_A_BaseEnum where 1=1 ";
                    _BaseEnumList = _InterfaceSQLHelper.ExecuteList<S_A_BaseEnum>(sql);
                }
                return _BaseEnumList;
            }
        }

        public static List<S_A_BaseEnum> MajorList
        {
            get
            {
                return BaseEnumList.Where(a => a.Type == "Major").ToList();
            }
        }
    }
}
