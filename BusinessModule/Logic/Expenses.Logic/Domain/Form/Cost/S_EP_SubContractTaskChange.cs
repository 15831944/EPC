using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;

using Config;
using Config.Logic;
using Formula;
using Newtonsoft.Json;
using System.Web;
using Base.Logic.Domain;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_SubContractTaskChange : BaseEPModel
    {
        public void Push()
        {
            var taskID = this.ModelDic.GetValue("SubContractTaskID");
            if (String.IsNullOrEmpty(taskID))
                throw new Formula.Exceptions.BusinessValidationException("变更必须指定一个委外任务，委外任务ID为空时，无法进行变更操作");
            var sql = new StringBuilder();

            //不更新系统默认的字段
            this.ModelDic.Remove("FlowPhase");
            this.ModelDic.Remove("FlowInfo");
            this.ModelDic.Remove("StepName");
            this.ModelDic.Remove("SerialNumber");
            this.ModelDic.SetValue("ModifyUserID", this.ModelDic.GetValue("CreateUserID"));
            this.ModelDic.SetValue("ModifyUser", this.ModelDic.GetValue("CreateUserID"));
            this.ModelDic.SetValue("ModifyDate", this.ModelDic.GetValue("CreateDate"));
            this.ModelDic.Remove("CreateUserID");
            this.ModelDic.Remove("CreateDate");
            this.ModelDic.Remove("CreateUser");
            sql.AppendLine(this.ModelDic.CreateUpdateSql(this.DB, "S_EP_SubContractTask", taskID));
            var changeList = FormulaHelper.DataTableToListDic(
                this.DB.ExecuteDataTable(string.Format(@"SELECT * FROM S_EP_SubContractTaskChange_CostUnitDetail 
            WHERE S_EP_SubContractTaskChangeID='{0}'", this.ID)));

            var detailDt = this.DB.ExecuteDataTable(string.Format(@"SELECT * FROM S_EP_SubContractTask_CostUnitDetail 
            WHERE S_EP_SubContractTaskID='{0}'", taskID));
            foreach (var item in changeList)
            {
                if (String.IsNullOrEmpty(item.GetValue("OrlID")))
                {
                    item.SetValue("ID", FormulaHelper.CreateGuid());
                    item.SetValue("S_EP_SubContractTaskID", taskID);
                    sql.AppendLine(item.CreateInsertSql(this.DB, "S_EP_SubContractTask_CostUnitDetail", item.GetValue("ID")));
                }
                else
                {
                    item.SetValue("ID", item.GetValue("OrlID"));
                    item.SetValue("S_EP_SubContractTaskID", taskID);
                    var costValue = this.DB.ExecuteScalar(String.Format(@"select isnull(sum(CurrentValue),0) from S_EP_SubContractProgressConfirm
                    where TaskDetailID='{0}'", item.GetValue("OrlID")));
                    item.SetValue("CostValue", costValue);
                    sql.AppendLine(item.CreateUpdateSql(this.DB, "S_EP_SubContractTask_CostUnitDetail", item.GetValue("OrlID")));
                }
            }

            var orlIds = String.Join(",", changeList.Where(c => !String.IsNullOrEmpty(c.GetValue("OrlID"))).Select(c => c.GetValue("OrlID")).ToList());
            var removeIDs = detailDt.AsEnumerable().Where(c => !orlIds.Contains(c["ID"].ToString())).Select(c => c["ID"].ToString());
            foreach (var removeID in removeIDs)
            {
                var obj = this.DB.ExecuteScalar(String.Format("select count(ID) from S_EP_SubContractProgressConfirm where TaskDetailID='{0}'", removeID));
                if (Convert.ToInt32(obj) == 0)
                {
                    //没有确认过委外进度的数据才能进行删除
                    sql.AppendLine(String.Format("delete from S_EP_SubContractTask_CostUnitDetail where ID='{0}'", removeID));
                }
            }
            this.DB.ExecuteNonQuery(sql.ToString());
        }
    }
}
