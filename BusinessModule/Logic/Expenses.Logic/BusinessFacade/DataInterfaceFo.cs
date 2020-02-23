using Config;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Logic.BusinessFacade
{
    public class DataInterfaceFo
    {
        public static void ValidateDataSyn(string tableName, string id)
        {
            return;
            //if (string.IsNullOrEmpty(tableName))
            //{
            //    throw new Formula.Exceptions.BusinessValidationException("表名不能为空");
            //}
            //Dictionary<string, string> map = GetTableMap();

            // if (map.ContainsKey(tableName) && !string.IsNullOrWhiteSpace(map[tableName])) {
            //    var sql = string.Format(@"select SynID from {0} where ID='{1}' ", map[tableName], id);
            //    var db = SQLHelper.CreateSqlHelper(ConnEnum.DataInterface);
            //    var obj = db.ExecuteScalar(sql);
            //    if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            //    {
            //        throw new Formula.Exceptions.BusinessValidationException("数据已同步，不允许操作！");
            //    }
            //}
        }

        private static Dictionary<string, string> GetTableMap()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            //source                 target
            map["S_EP_RevenueInfo_Detail"] = "S_EP_Income";
            map["S_EP_ReimbursementApply"] = "S_EP_Reimbursement";
            map["T_F_LoanApply"] = "S_EP_UserLoan";
            map["S_C_ManageContract"] = "S_M_Contract";
            map["S_F_Customer"] = "S_M_Customer";
            map["S_B_Bond"] = "S_M_Deposit";

            map["S_C_Invoice"] = "S_M_Invoice";
            map["S_SP_Payment"] = "S_M_Payment";
            map["S_C_Receipt"] = "S_M_Receipt";
            map["S_SP_Invoice"] = "S_M_SPInvoice";
            map["S_SP_Supplier"] = "S_M_Supplier";
            map["S_I_Project"] = "S_P_Project";
            return map;
        }

    }

}
