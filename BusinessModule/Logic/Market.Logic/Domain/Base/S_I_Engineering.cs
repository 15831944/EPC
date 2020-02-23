using Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Market.Logic.Domain
{
    public partial class S_I_Engineering
    {
        public void Delete(bool Destory)
        {
            var entities = this.GetDbContext<MarketEntities>();
            var projectDB = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            if (!Destory)
            {
                if (entities.S_I_Project.Count(d => d.EngineeringInfo == this.ID) > 0)
                    throw new Formula.Exceptions.BusinessException("【" + this.Name + "】已经下达任务单，不能删除");
            }
            entities.S_I_Project.Delete(d => d.EngineeringInfo == this.ID);
            var contractInfolist = entities.S_C_ManageContract.Where(d => d.EngineeringInfo == this.ID).ToList();
            foreach (var contract in contractInfolist)
            {
                contract.ClearProjectBinding();
                contract.EngineeringInfo = "";
                contract.EngineeringInfoName = "";
            }

            #region 删除设计项目管理库中的所有数据
            var projectDt = projectDB.ExecuteDataTable("select ID,Name,Code from S_I_ProjectInfo where EngineeringInfoID='" + this.ID + "'");
            foreach (DataRow dataRow in projectDt.Rows)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("delete from S_I_UserDefaultProjectInfo where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from S_I_UserFocusProjectInfo where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_CP_TaskNotice where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_SC_DesignInput where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_SC_DesignPlan where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_SC_SchemeForm where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_SC_FascicleTask where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_SC_DesignOutline where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_EXE_Audit where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_EXE_AuditReview where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_EXE_MajorPutInfo where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_EXE_MettingSign where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from T_EXE_PublishApply where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from S_W_WBS_Log where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from S_W_WBSVersion where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from S_AE_Mistake where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from S_W_Activity where ProjectInfoID='" + dataRow["ID"].ToString() + "';");
                sql.AppendLine("delete from S_I_ProjectInfo where ID='" + dataRow["ID"].ToString() + "';");

                projectDB.ExecuteNonQuery(sql.ToString());
            }
            projectDB.ExecuteNonQuery("delete from S_I_ProjectGroup where RelateID='" + this.ID + "'");
            #endregion
        }
    }
}
