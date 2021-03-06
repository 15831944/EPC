﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Logic.Domain;
using HR.Logic.BusinessFacade;
using Formula;
using System.Data;

namespace HR.Areas.AutoForm.Controllers
{
    public class QualificationApplyController : HRFormContorllor<T_C_QualificationApply>
    {
        protected override void OnFlowEnd(T_C_QualificationApply entity, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (string.IsNullOrEmpty(entity.CertificateType)) throw new Formula.Exceptions.BusinessException("证书类别为空，证书状态同步失败！");
            var borrowIDList = entity.T_C_QualificationApply_BorrowInfo.Select(d => d.BorrowID).ToList();
            var borrowIDArray = "'" + string.Join("','", borrowIDList) + "'";

            string tableName = "S_C_" + entity.CertificateType;
            var sql = string.Format(" update {0} set State='{1}',Borrower='{2}',BorrowerName='{3}' where ID in ({4})",
                                    tableName, CertificateState.已借出.ToString(), entity.Borrower, entity.BorrowerName, borrowIDArray);
            this.HRSQLDB.ExecuteNonQuery(sql);

            //新增资质借用记录
            foreach (var row in entity.T_C_QualificationApply_BorrowInfo)
            {
                var applyLog = new S_C_QualificationApplyLog();
                applyLog.ID = FormulaHelper.CreateGuid();
                applyLog.QualificationID = row.BorrowID;
                applyLog.Name = row.Name;
                applyLog.QualificationType = entity.CertificateType;
                applyLog.BorrowUserID = entity.Borrower;
                applyLog.BorrowUserName = entity.BorrowerName;
                applyLog.BorrowerDeptID = entity.BorrowerDept;
                applyLog.BorrowerDeptName = entity.BorrowerDeptName;
                applyLog.BorrowerTel = entity.BorrowerTel;
                applyLog.BorrowDate = entity.BorrowDate;
                applyLog.PlanReturnDate = entity.PlanReturnDate;
                applyLog.QualificationApplyID = entity.ID;
                this.BusinessEntities.Set<S_C_QualificationApplyLog>().Add(applyLog);
            }
            this.BusinessEntities.SaveChanges();

            base.OnFlowEnd(entity, taskExec, routing);
        }

        public void ReturnQualification(string ids, string type)
        {
            string tableName = "S_C_" + type;
            string rowIDs = "'" + ids.Replace(",", "','") + "'";
            string checkSql = string.Format("select * from {0} where ID in ({1})", tableName, rowIDs);
            var dbInfo = this.HRSQLDB.ExecuteDataTable(checkSql);
            foreach (DataRow row in dbInfo.Rows)
            {
                if (row["State"].ToString() == "正常") throw new Formula.Exceptions.BusinessException("资质已归还，无需再次进行归还操作！");
                if (row["State"].ToString() != "已借出") throw new Formula.Exceptions.BusinessException("借出状态为【已借出】的资质才可以进行归还操作！");
            }

            string sql = string.Format(@"update {0} set State='正常',Borrower='',BorrowerName=''
where ID in ({1}) and State!='已作废'and State!='已遗失'

            update S_C_QualificationApplyLog
set ReturnDate='{2}'
where ID in
(select ID from S_C_QualificationApplyLog 
where QualificationID in ({1}) and (ReturnDate is null or ReturnDate='')) ", tableName, rowIDs, DateTime.Now);

            this.HRSQLDB.ExecuteNonQuery(sql);
        }
    }
}
