using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monitor.Logic.Domain;
using Monitor.Logic.DTO;
using Monitor.Logic.Helper;

namespace Monitor.Logic.BusinessFacade
{
    public class ProjectDetailBll
    {
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <returns></returns>
        public static ResultDTO GetPrjInfo(string id)
        {
            var sql = @"SELECT p.Code,p.Name,p.CustomerName,p.PhaseName,p.ChargeDeptName,
                    p.ChargeUserName,p.CreateDate,p.PlanStartDate,p.PlanFinishDate,
                    p.Major,r.Remark
                    FROM S_I_ProjectInfo p
                    LEFT JOIN FE_ProjectBaseConfig.dbo.S_D_WBSAttrDefine d
                    ON p.PhaseValue = d.Code
                    LEFT JOIN T_CP_TaskNotice r
                    ON r.ProjectInfoID = p.ID
                    WHERE p.ID='{0}'";
            var db = new ProjectContext();
            return WebApi.Success(db.Database.SqlQuery<ProjectDTO>(string.Format(sql, id)).FirstOrDefault());
        }

        /// <summary>
        /// 获取项目下用户信息
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public static ResultDTO GetPrjUsers(string id, string condition)
        {
            var basedbname = Base.GetDbName("Base");
            var prjdbname = Base.GetDbName("Project");
            var sql = @"SELECT case when m.MajorName is null then m.RoleName else
                        m.MajorName+'·'+ m.RoleName end MajorRole
                        ,m.UserName,u.MobilePhone,u.Email
                        FROM [{3}].[dbo].[S_W_OBSUser] m
                        LEFT JOIN [{2}].[dbo].[S_A_User] u
                        ON m.UserID = u.ID 
                        WHERE ProjectInfoID = '{0}' and m.UserName like '%{1}%'
                        order by MajorName+RoleName";
            var db = new ProjectContext();
            return WebApi.Success(db.Database.SqlQuery<ProjectUser>(string.Format(sql, id, condition, basedbname, prjdbname)));
        }
    }
}