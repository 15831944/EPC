using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Formula;
using Config;


namespace Market.Logic.Domain
{
    public partial class S_I_Project
    {
        public void Save()
        {
            var marketEntities = this.GetMarketContext();
            var customer = marketEntities.S_F_Customer.SingleOrDefault(d => d.ID == this.Customer);
            if (customer != null)
            {
                if (String.IsNullOrEmpty(this.Province))
                    this.Province = customer.Province;
                if (String.IsNullOrEmpty(this.Country))
                    this.Country = customer.Country;
                if (String.IsNullOrEmpty(this.City))
                    this.City = customer.City;
                if (String.IsNullOrEmpty(this.Industry))
                    this.Industry = customer.Industry;
            }
            this.SychorInfo();
        }

        public void SychorInfo()
        {
            var sqlHelper = this.GetMarketSqlHelper();
            string sql = @"update S_C_ReceiptObject set ProjectInfoName='{0}' ,ProjectInfoCode='{1}' where ProjectInfoID='{2}';
update S_C_ManageContract_ProjectRelation set ProjectName='{0}' ,ProjectCode='{1}' where ProjectID='{2}';
update S_C_PlanReceipt set ProjectName='{0}' ,ProjectCode='{1}' where ProjectID='{2}';
";
            sql = String.Format(sql, this.Name, this.Code, this.ID);
            sqlHelper.ExecuteNonQuery(sql);
            string updateSql = @" update S_C_PlanReceipt set ProductionUnitID='{0}',ProductionUnitName='{1}' where ProjectID='{2}' and State='{3}'";
            if (String.IsNullOrEmpty(this.ChargerDept))
            {
                updateSql = String.Format(updateSql, this.ChargerDept, this.ChargerDeptName, this.ID, PlanReceiptState.UnReceipt.ToString());
                sqlHelper.ExecuteNonQuery(updateSql);
            }
        }

        public void Delete()
        {
            var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            string sql = "select count(0) from S_I_ProjectInfo where MarketProjectInfoID ='" + this.ID + "'";
            var obj = Convert.ToInt32(sqlDB.ExecuteScalar(sql));
            if (obj > 0) throw new Formula.Exceptions.BusinessException("项目【" + this.Name + "】已经在项目管理中立项，无法进行删除操作");
            var marketEntities = this.GetMarketContext();


            //删除项目时，同时查询项目合同关系表中是否有相关记录，一并删除
            //如果已经关联合同，并且关联了收款项，则需要清除收款项的项目数据
            //但是如果收款项有过收款数据，则项目不允许被删除
            var relationList = marketEntities.S_C_ManageContract_ProjectRelation.Where(d => d.ProjectID == this.ID).ToList();
            foreach (var relation in relationList)
            {
                //检索关联的合同
                var contract = marketEntities.S_C_ManageContract.FirstOrDefault(d => d.ID == relation.S_C_ManageContractID);
                //如果关联的合同是空的，则直接删除关联对象，清除垃圾数据
                if (contract == null)
                {
                    marketEntities.S_C_ManageContract_ProjectRelation.Remove(relation);
                    continue;
                }
                contract.RemoveProject(relation.ProjectID);
            }
            marketEntities.S_I_Project.Delete(d => d.ID == this.ID);
        }

        public void ValidateTaskPush()
        {
            var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Project);
            string sql = "select count(0) from S_I_ProjectInfo where MarketProjectInfoID ='" + this.ID + "'";
            var obj = Convert.ToInt32(sqlDB.ExecuteScalar(sql));
            if (obj > 0) throw new Formula.Exceptions.BusinessException("项目【" + this.Name + "】已经在项目管理中立项，无法进行操作");

        }
    }
}
