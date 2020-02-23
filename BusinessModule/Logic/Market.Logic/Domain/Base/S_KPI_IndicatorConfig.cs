using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using Formula;

namespace Market.Logic.Domain
{
    public partial class S_KPI_IndicatorConfig
    {
        public void Publish()
        {
            var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var tableNames = @"S_KPI_IndicatorCompany,S_KPI_IndicatorOrg,S_KPI_IndicatorPerson,S_KPITemp_IndicatorCompany,S_KPITemp_IndicatorOrg,S_KPITemp_IndicatorPerson";
            StringBuilder UpdateTbSql = new StringBuilder();
            foreach (var tableName in tableNames.Split(','))
            {
                string fieldSql = " [" + this.IndicatorCode + "]  decimal(18,4) ";
                string costFieldSql = " [" + this.IndicatorCode + "Cost]  decimal(18,4) ";
                UpdateTbSql.Append("  if exists (" +
                  " select * from dbo.syscolumns " +
                  " where [name] = '" + this.IndicatorCode + "' " +
                  " and [id]=object_id(N'[dbo].[" + tableName + "]')  " +
                  " and OBJECTPROPERTY(id, N'IsUserTable') = 1 ) ");
                UpdateTbSql.Append(" begin ");
                UpdateTbSql.Append(" ALTER TABLE [dbo].[" + tableName + "] ALTER COLUMN  " + fieldSql + " ");
                UpdateTbSql.Append(" end " + " else begin ");
                UpdateTbSql.Append(" ALTER TABLE [dbo].[" + tableName + "] ADD " + fieldSql + " end  ");

                UpdateTbSql.Append("  if exists (" +
                  " select * from dbo.syscolumns " +
                  " where [name] = '" + this.IndicatorCode + "Cost" + "' " +
                  " and [id]=object_id(N'[dbo].[" + tableName + "]')  " +
                  " and OBJECTPROPERTY(id, N'IsUserTable') = 1 ) ");
                UpdateTbSql.Append(" begin ");
                UpdateTbSql.Append(" ALTER TABLE [dbo].[" + tableName + "] ALTER COLUMN  " + costFieldSql + " ");
                UpdateTbSql.Append(" end " + " else begin ");
                UpdateTbSql.Append(" ALTER TABLE [dbo].[" + tableName + "] ADD " + costFieldSql + " end  ");
            }
            sqlDB.ExecuteNonQuery(UpdateTbSql.ToString());
        }

        public void Delete()
        {

        }

        /// <summary>
        /// 获取当前版本的指标
        /// </summary>
        /// <returns></returns>
        public static List<S_KPI_IndicatorConfig> GetCurrentVersionIndicatorConfig(IndicatorOrgType orgType)
        {
            var entities = FormulaHelper.GetEntities<MarketEntities>();
            var state = YesOrNo.Yes.ToString();
            var orgtype = orgType.ToString();
            var list = entities.Set<S_KPI_IndicatorConfig>().Where(d => d.State == state && d.OrgType.Contains(orgtype)).OrderBy(d => d.SortIndex).ToList();
            return list;
        }
    }
}
