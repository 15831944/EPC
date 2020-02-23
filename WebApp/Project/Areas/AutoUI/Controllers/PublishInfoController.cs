using Formula.Helper;
using Project.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Config.Logic;
using Formula;
using Config;
using Market.Logic.Domain;
using Market.Logic;

namespace Project.Areas.AutoUI.Controllers
{
    public class PublishInfoController : ProjectFormContorllor<S_EP_PublishInfo>
    {
        protected override void AfterGetData(System.Data.DataTable dt, bool isNew, string upperVersionID)
        {
            if (dt.Columns.Contains("Products"))
            {
                var productList = JsonHelper.ToObject<List<S_EP_PublishInfo_Products>>(dt.Rows[0]["Products"].ToString());
                var ids = productList.Select(a => a.VersionID).Distinct();
                var versions = this.BusinessEntities.Set<S_E_ProductVersion>().Where(a => ids.Contains(a.ID));
                foreach (var p in productList)
                {
                    var version = versions.FirstOrDefault(a => a.ID == p.VersionID);
                    if (version != null)
                    {
                        var id = p.ID;
                        FormulaHelper.UpdateModel(p, version);
                        p.ID = id;
                    }
                }
                dt.Rows[0]["Products"] = JsonHelper.ToJson(productList);
            }
        }

        public JsonResult GetProductList(string publishInfoID)
        {
            var productList = this.BusinessEntities.Set<S_EP_PublishInfo_Products>().Where(a => a.S_EP_PublishInfoID == publishInfoID).ToList();
            var ids = productList.Select(a => a.VersionID).Distinct();
            var versions = this.BusinessEntities.Set<S_E_ProductVersion>().Where(a => ids.Contains(a.ID));
            foreach (var p in productList)
            {
                var version = versions.FirstOrDefault(a => a.ID == p.VersionID);
                if (version != null)
                {
                    var id = p.ID;
                    FormulaHelper.UpdateModel(p, version);
                    p.ID = id;
                }
            }
            return Json(productList);
        }

        public JsonResult SetOperateUser(string id)
        {
            var user = FormulaHelper.GetUserInfo();
            var publishInfo = this.GetEntityByID(id);
            if (publishInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + id + "】的出图任务信息");
            if (string.IsNullOrEmpty(publishInfo.OperateUser))
            {
                publishInfo.OperateUser = user.UserID;
                publishInfo.OperateUserName = user.UserName;
                this.BusinessEntities.SaveChanges();
            }
            else if (publishInfo.OperateUser != user.UserID)
                throw new Formula.Exceptions.BusinessException("此出图任务已被" + publishInfo.OperateUserName + "锁定");
            return Json("");
        }

        public JsonResult RemovePlot(string id)
        {
            var publishInfo = this.GetEntityByID(id);
            if (publishInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + id + "】的出图任务信息");
            publishInfo.PlotTime = null;
            publishInfo.PublishTime = null;
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult ConfirmPlot(string id)
        {
            var publishInfo = this.GetEntityByID(id);
            if (publishInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + id + "】的出图任务信息");
            publishInfo.PlotTime = DateTime.Now;
            if (publishInfo.PlotTime != null && publishInfo.ConfirmTime != null)
            {
                publishInfo.PublishTime = publishInfo.PlotTime;
                publishInfo.BelongYear = ((DateTime)publishInfo.PublishTime).Year;
                publishInfo.BelongMonth = ((DateTime)publishInfo.PublishTime).Month;
                publishInfo.BelongQuarter = ((publishInfo.BelongMonth - 1) / 3) + 1;
            }
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult RemoveOperateUser(string id)
        {
            var publishInfo = this.GetEntityByID(id);
            if (publishInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + id + "】的出图任务信息");
            publishInfo.OperateUser = null;
            publishInfo.OperateUserName = null;
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult ConfirmCost(string id)
        {
            var publishInfo = this.GetEntityByID(id);
            if (publishInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + id + "】的出图任务信息");
            publishInfo.ConfirmTime = DateTime.Now;
            if (publishInfo.PlotTime != null && publishInfo.ConfirmTime != null)
            {
                publishInfo.PublishTime = publishInfo.ConfirmTime;
                publishInfo.BelongYear = ((DateTime)publishInfo.PublishTime).Year;
                publishInfo.BelongMonth = ((DateTime)publishInfo.PublishTime).Month;
                publishInfo.BelongQuarter = ((publishInfo.BelongMonth - 1) / 3) + 1;
            }
            SetCostInfo(publishInfo);
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        public JsonResult RemoveCost(string id)
        {
            var publishInfo = this.GetEntityByID(id);
            if (publishInfo == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + id + "】的出图任务信息");
            publishInfo.ConfirmTime = null;
            publishInfo.PublishTime = null;
            var costIDs = publishInfo.S_EP_PublishInfo_PriceDetail.Select(a => a.CostID).Distinct().ToList();
            var sql = "delete S_FC_CostInfo where ID in ('" + string.Join("','", costIDs) + "')";
            var oaDb = SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto);
            var marketDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            oaDb.ExecuteNonQuery(sql);
            marketDB.ExecuteNonQuery(sql);
            this.BusinessEntities.SaveChanges();
            return Json("");
        }

        private void SetCostInfo(S_EP_PublishInfo publishInfo)
        {
            var oaDb = SQLHelper.CreateSqlHelper(ConnEnum.OfficeAuto);
            var marketDB = SQLHelper.CreateSqlHelper(ConnEnum.Market);
            var project = this.GetEntityByID<S_I_ProjectInfo>(publishInfo.ProjectInfoID);
            if (project == null) throw new Formula.Exceptions.BusinessException("未找到ID为【" + publishInfo.ProjectInfoID + "】的项目信息");
            var marketEntities = FormulaHelper.GetEntities<MarketEntities>();
            var marketProject = marketEntities.Set<S_I_Project>().FirstOrDefault(a => a.ID == project.MarketProjectInfoID);
            var costInfoIDs = publishInfo.S_EP_PublishInfo_PriceDetail.Select(a => a.CostID).Distinct().ToList();
            var costs = marketEntities.Set<S_FC_CostInfo>().Where(a => costInfoIDs.Contains(a.ID)).ToList();
            foreach (var detail in publishInfo.S_EP_PublishInfo_PriceDetail)
            {
                bool isNew = false;
                var cost = costs.FirstOrDefault(a => a.ID == detail.CostID);
                if (cost == null)
                {
                    isNew = true;
                    cost = new S_FC_CostInfo();
                    cost.ID = FormulaHelper.CreateGuid();
                }
                cost.SubjectName = publishInfo.CostName;
                cost.SubjectCode = publishInfo.CostCode;
                cost.ProjectID = project.MarketProjectInfoID;
                cost.ProjectName = marketProject == null ? "" : marketProject.Name;
                cost.ProjectCode = marketProject == null ? "" : marketProject.Code;
                cost.CostType = Const.defaultDirectCostType;
                cost.ProjectType = Const.defaultDirectCostType;
                cost.UnitPrice = detail.RealSum;
                cost.Quantity = publishInfo.ProductCount;
                cost.CostValue = (decimal)detail.RealSumCost;
                cost.CostUserID = publishInfo.ApplyUser;
                cost.CostUserName = publishInfo.ApplyUserName;
                cost.CostUserDeptID = publishInfo.ApplyDept;
                cost.CostUserDeptName = publishInfo.ApplyDeptName;
                cost.ProjectDeptID = marketProject.ChargerDept;
                cost.ProjectDeptName = marketProject.ChargerDeptName;
                cost.ProjectChargerUserID = marketProject.ChargerUser;
                cost.ProjectChargerUserName = marketProject.ChargerUserName;
                cost.ProjectClass = marketProject.ProjectClass;
                cost.CostDate = DateTime.Now;
                cost.BelongYear = DateTime.Now.Year;
                cost.BelongMonth = DateTime.Now.Month;
                cost.BelongQuarter = (int)((DateTime.Now.Month - 1) / 3) + 1;
                cost.FormID = publishInfo.ID;
                cost.RelateID = detail.ID;
                cost.Extend1 = detail.PublicationType;
                cost.Extend2 = detail.Specification;
                cost.Extend3 = detail.RealPrice.ToString();
                cost.Extend4 = detail.RealNum.ToString();
                cost.Extend5 = publishInfo.MajorCode;
                if (isNew)
                {
                    cost.ToDic().InsertDB(oaDb, "S_FC_CostInfo", cost.ID);
                    cost.ToDic().InsertDB(marketDB, "S_FC_CostInfo", cost.ID);
                }
                else
                {
                    cost.ToDic().UpdateDB(oaDb, "S_FC_CostInfo", cost.ID);
                    cost.ToDic().UpdateDB(marketDB, "S_FC_CostInfo", cost.ID);
                }
                detail.CostID = cost.ID;
            }
        }
    }
}
