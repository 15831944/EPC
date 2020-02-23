using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Market.Logic.Domain;
using Market.Logic;
using Formula;
using Config;
using Config.Logic;
using System.Data;
using Base.Logic.Domain;
using System.Transactions;

namespace Market.Areas.MarketUI.Controllers
{
    public class SupplierContractController : MarketFormContorllor<S_SP_SupplierContract>
    {
        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            var contractID = dic.GetValue("ID");
            dic.SetValue("Code", dic.GetValue("SerialNumber"));
            //添加过补充协议的合同状态不能改成登记
            var hasSupp = this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>().Any(a => a.MainContract == contractID);
            if (hasSupp && string.IsNullOrEmpty(dic.GetValue("SignDate")))
                throw new Formula.Exceptions.BusinessException("添加过补充协议的分包合同必须有签约日期");

            var entity = this.GetEntityByID(contractID);
            if (entity == null) entity = new S_SP_SupplierContract();
            this.UpdateEntity(entity, dic);
            entity.Save();
            var sumSettle = this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>().Where(a => a.MainContract == entity.ID).ToList().Sum(a => a.ContractValue);
            entity.ContractValue = entity.ThisContractValue + sumSettle;
        }

        protected override void BeforeDelete(string[] Ids)
        {
            foreach (var Id in Ids)
            {
                var supplier = this.GetEntityByID(Id);
                if (supplier == null) throw new Formula.Exceptions.BusinessException("未能找到指定的分包合同");
                if (supplier.State != ContractState.Regist.ToString())
                {
                    throw new Formula.Exceptions.BusinessValidationException("已经经过评审的合同信息不能进行删除操作");
                }
                if (this.BusinessEntities.Set<S_SP_Invoice>().Count(d => d.SupplierContract == Id) > 0)
                {
                    throw new Formula.Exceptions.BusinessException("已经有过分包发票记录的合同，不能删除");
                }
                if (this.BusinessEntities.Set<S_SP_Payment>().Count(d => d.Contract == Id) > 0)
                {
                    throw new Formula.Exceptions.BusinessException("已经有过分包发票记录的合同，不能删除");
                }
                if (this.BusinessEntities.Set<S_SP_SupplierContract_Supplementary>().Count(a => a.MainContract == Id) > 0)
                    throw new Formula.Exceptions.BusinessException("已经有过补充协议的合同，不能删除");

                var sql = string.Format(@"select COUNT(SupplierContractConfirm.ID) from (select SplitID from S_SP_SupplierContract_ContractSplit where S_SP_SupplierContractID='{0}') SupplierContractDetail
inner join S_SP_SupplierContractConfirm SupplierContractConfirm on SupplierContractDetail.SplitID=SupplierContractConfirm.SubContractDetailID
where SupplierContractConfirm.FlowPhase='End' ", Id);
                var sqlDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
                var count = Convert.ToInt32(sqlDB.ExecuteScalar(sql));
                if (count > 0)
                {
                    throw new Formula.Exceptions.BusinessException("已进行合同确认，无法进行删除操作");
                }
            }
        }

        public JsonResult SetContractState(string ListData, string State)
        {
            var list = Formula.Helper.JsonHelper.ToList(ListData);
            if (String.IsNullOrEmpty(State)) throw new Formula.Exceptions.BusinessValidationException("必须指定合同的状态");
            var sb = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                var contract = this.GetEntityByID(item.GetValue("ID"));
                if (contract == null) continue;
                if (State == ContractState.Sign.ToString())
                {
                    if (contract.State != ContractState.Pause.ToString() && contract.State != ContractState.Terminate.ToString())
                    {
                        throw new Formula.Exceptions.BusinessValidationException(String.Format("合同编号为【{0}】的合同不能恢复为履行状态，请确认该合同是否暂停或终止", contract.SerialNumber));
                    }
                }
                if (contract.State == ContractState.Regist.ToString() || contract.State == ContractState.WaitSign.ToString())
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("合同编号为【{0}】的合同未签约，不能设置合同装填", contract.SerialNumber));
                }
                contract.State = State;
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult GetProjectByContractID(string ContractInfoID)
        {
            string sql = @"select r.ProjectCode,r.ProjectID,r.ProjectName,r.ProjectValue,r.Scale,r.SortIndex,p.*
 from S_C_ManageContract_ProjectRelation r
left join S_I_Project p on p.ID=r.ProjectID where S_C_ManageContractID='" + ContractInfoID + "'";
            var dt = this.MarketSQLDB.ExecuteDataTable(sql);
            //var list = this.BusinessEntities.Set<S_C_ManageContract_ProjectRelation>().Where(d => d.S_C_ManageContractID == ContractInfoID).ToList();
            return Json(dt);
        }

        public JsonResult GetManageContractExamine(string contractID)
        {
            string sql = @"select TOP 1 ID from T_SP_ContractReview where  SPContractInfo = '{0}' ORDER BY id desc";
            sql = string.Format(sql, contractID);
            DataTable dt = this.MarketSQLDB.ExecuteDataTable(sql);
            return Json(dt);
        }

        public JsonResult GetCostUnitInfo(string NodeID)
        {
            var dt = this.MarketSQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_CBSNode WHERE ID='{0}'", NodeID));
            if (dt.Rows.Count == 0)
            {
                return Json("");
            }
            var nodeFullID = dt.Rows[0]["FullID"].ToString();
            var sql = @"select S_EP_CostUnit.ID as UnitID,
isnull(SubContractValue,0) as SubContractValue,
SubContractCostValue,S_EP_CBSNode.* from S_EP_CostUnit
left join S_EP_CBSNode on CBSNodeID=S_EP_CBSNode.ID
left join (select Sum(SubContractValue) as SubContractValue,CostUnitID from dbo.S_EP_SubContractTask_CostUnitDetail
group by CostUnitID) SubContractTaskDetailInfo
on SubContractTaskDetailInfo.CostUnitID=S_EP_CostUnit.ID
where FullID like '{0}%' ";
            var result = this.MarketSQLDB.ExecuteDataTable(String.Format(sql, nodeFullID));
            return Json(result);
        }

        public JsonResult ValidateChange(string ContractID)
        {
            var contractInfo = this.GetEntityByID<S_SP_SupplierContract>(ContractID);
            if (!contractInfo.SignDate.HasValue)
            {
                throw new Formula.Exceptions.BusinessValidationException("尚未签约的合同不能进行合同变更，请直接通过编辑来修改未签订的合同");
            }
            var result = new Dictionary<string, object>();
            string sql = String.Format("select ID from T_SP_ContractChange where ContractID='{0}' and FlowPhase='{1}'", ContractID, "Start");
            var dt = this.MarketSQLDB.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                result.SetValue("ID", dt.Rows[0]["ID"]);
                return Json(result);
            }
            sql = String.Format("select Count(ID) from T_SP_ContractChange where ContractID='{0}' and FlowPhase='{1}'", ContractID, "Processing");
            var obj = Convert.ToInt32(this.MarketSQLDB.ExecuteScalar(sql));
            if (obj > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException("合同正在变更中，无法重复启动变更，请在变更结束后再启动变更");
            }
            return Json(result);
        }

        public ActionResult TabList()
        {
            string projectID = this.GetQueryString("ProjectID");
            var tab = new Tab();
            string sql = @"select * from S_SP_SupplierContract 
where State='Sign'
and ProjectInfo='{0}'
order by SignDate desc";

            sql = string.Format(sql, projectID);
            var dt = this.MarketSQLDB.ExecuteDataTable(sql);

            Category contractItem = new Category();
            contractItem.Key = "";
            contractItem.Name = "已签分包合同";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var dataRow = dt.Rows[i];
                var item = new CategroyItem();
                item.Name = dataRow["Name"].ToString();
                item.Value = dataRow["ID"].ToString();
                item.SortIndex = i + 1;
                contractItem.Items.Add(item);
                if (i == 0)
                    contractItem.SetDefaultItem(item.Value);
            }

            contractItem.Multi = false;
            tab.Categories.Add(contractItem);
            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }
    }
}
