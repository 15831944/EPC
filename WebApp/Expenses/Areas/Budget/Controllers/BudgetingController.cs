using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config;
using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using MvcAdapter;
using Formula;

namespace Expenses.Areas.Budget.Controllers
{
    public class BudgetingController : ExpensesController
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_BudgetVersion";
            }
        }

        public ActionResult TreeList()
        {
            string ID = this.GetQueryString("ID");
            var versionDic = this.GetDataDicByID("S_EP_BudgetVersion", ID);
            var basisField = "ContractValue"; //默认预算依据为合同金额
            Dictionary<string, object> defineUnitDic = null;
            if (versionDic == null)
            {
                var unitID = this.GetQueryString("UnitID");
                var budgetUnitDic = this.GetDataDicByID("S_EP_BudgetUnit", unitID);
                if (budgetUnitDic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的预算单元，无法编制预算");
                var versionDt = this.SQLDB.ExecuteDataTable(String.Format(" SELECT TOP 1 * FROM S_EP_BudgetVersion WHERE BudgetUnitID='{0}' ORDER BY ID DESC ", unitID));
                if (versionDt.Rows.Count > 0)
                {
                    versionDic = Formula.FormulaHelper.DataRowToDic(versionDt.Rows[0]);
                }
                defineUnitDic = this.GetDataDicByID("S_EP_DefineBudget", budgetUnitDic.GetValue("UnitDefineID"));
            }
            bool flowEnd = true; bool isFirst = false;
            if (versionDic == null)
            {
                //没有任何预算信息，则默认显示新增界面
                versionDic = new Dictionary<string, object>();
                flowEnd = true;
                isFirst = true;
                ViewBag.VersionID = "";
                ViewBag.FlowPhase = "";
                ViewBag.VersionNo = "0";
            }
            else
            {
                if (versionDic.GetValue("FlowPhase") != "End")
                    flowEnd = false;
                ViewBag.FlowPhase = versionDic.GetValue("FlowPhase");
                ViewBag.VersionID = versionDic.GetValue("ID");
                ViewBag.VersionNo = versionDic.GetValue("VersionNumber");
                if (defineUnitDic == null)
                {
                    var budgetUnit = this.GetDataDicByID("S_EP_BudgetUnit", versionDic.GetValue("BudgetUnitID"));
                    if (budgetUnit != null)
                        defineUnitDic = this.GetDataDicByID("S_EP_DefineBudget", budgetUnit.GetValue("UnitDefineID"));
                }
            }
            if (defineUnitDic != null && !String.IsNullOrEmpty(defineUnitDic.GetValue("BudgetBasisField")))
            {
                basisField = defineUnitDic.GetValue("BudgetBasisField");
            }
            ViewBag.BudgetData = JsonHelper.ToJson(versionDic);
            ViewBag.FlowEnd = flowEnd;
            ViewBag.First = isFirst;
            ViewBag.BasisField = basisField;
            return View();
        }

        public JsonResult GetVersionTreeList(QueryBuilder qb, string VersionID, string ShowType, string showAll)
        {
            qb.PageSize = 0;
            qb.Add("S_EP_BudgetVersionID", QueryMethod.Equal, VersionID);
            qb.SortField = "SortIndex";
            qb.SortOrder = "asc";
            if (!String.IsNullOrEmpty(ShowType) && ShowType.ToLower() == "diff")
            {
                //只显示差异数据
                qb.Add("ModifyState", QueryMethod.NotEqual, ModifyState.Normal.ToString());
            }
            else if (!String.IsNullOrEmpty(ShowType) && ShowType.ToLower() == "new")
            {
                //只显示最新版本的数据，不体现差异
                qb.Add("ModifyState", QueryMethod.NotEqual, ModifyState.Removed.ToString());
            }
            if (!String.IsNullOrEmpty(showAll) && showAll.ToLower() != "true")
            {
                qb.Add("NodeType", QueryMethod.NotEqual, "Detail");
            }
            var data = this.SQLDB.ExecuteDataTable(@"SELECT AllowAddChild,AllowEdit,AllowDelete,CalExpression,
CalDescription,S_EP_BudgetVersion_Detail.* FROM S_EP_BudgetVersion_Detail
left join S_EP_DefineBudget_Subject
on S_EP_BudgetVersion_Detail.BudgetDetailDefineID = S_EP_DefineBudget_Subject.ID", qb);
            return Json(data);
        }

        public JsonResult AddVersionCBS(string NodeID, string AddMode, string VersionID)
        {
            var result = new Dictionary<string, object>();
            Action action = () =>
            {
                var nodeDic = this.GetDataDicByID("S_EP_BudgetVersion_Detail", NodeID);
                if (nodeDic == null) throw new Formula.Exceptions.BusinessValidationException("未能找到选中的费用科目节点，无法新增");
                var node = new S_EP_BudgetVersion_Detail(nodeDic);
                if (AddMode == "After")
                {
                    var child = node.AddBrotherNode();
                    result = child.ModelDic;
                }
                else if (AddMode == "AddChild")
                {
                    var child = node.AddChildNode();
                    result = child.ModelDic;
                }
            };
            this.ExecuteAction(action);

            return Json(result);
        }

        public JsonResult SaveNodes(string ListData, string VersionID)
        {
            var list = JsonHelper.ToList(ListData);
            Action action = () =>
            {
                var versionDic = this.GetDataDicByID("S_EP_BudgetVersion", VersionID);
                if (versionDic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的预算版本信息，保存失败");
                var version = new S_EP_BudgetVersion(versionDic);
                foreach (var item in list)
                {
                    if (item.GetValue("_state").ToLower() == "removed") continue;
                    var lastVersionValue = String.IsNullOrEmpty(item.GetValue("LastVersionValue")) ? 0m : Convert.ToDecimal(item.GetValue("LastVersionValue"));
                    var currentValue = String.IsNullOrEmpty(item.GetValue("TotalValue")) ? 0m : Convert.ToDecimal(item.GetValue("TotalValue"));

                    if (item.GetValue("CustomEdit") == "True")
                    {
                        if (Convert.ToInt32(this.SQLDB.ExecuteScalar(@"SELECT COUNT(ID) FROM S_EP_BudgetVersion_Detail 
WHERE CBSParentID='" + item.GetValue("CBSID") + "' AND S_EP_BudgetVersionID='" + VersionID + "'")) > 0)
                        {
                            item.SetValue("CustomEdit", "True");
                        }
                        else
                        {
                            item.SetValue("CustomEdit", "False");
                        }
                    }
                    if (currentValue == lastVersionValue && item.GetValue("ModifyState") == ModifyState.Modified.ToString())
                    {
                        item.SetValue("ModifyState", ModifyState.Normal.ToString());
                    }
                    else if (item.GetValue("ModifyState") == ModifyState.Normal.ToString())
                    {
                        item.SetValue("ModifyState", ModifyState.Modified.ToString());
                    }
                    item.UpdateDB(this.SQLDB, "S_EP_BudgetVersion_Detail", item.GetValue("ID"));
                }
                version.SummaryTotalValue();
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult DeleteNodes(string ListData)
        {
            Action action = () =>
            {
                var list = JsonHelper.ToList(ListData);
                foreach (var item in list)
                {
                    var budgetDetail = new S_EP_BudgetVersion_Detail(item);
                    budgetDetail.Delete();
                    var parentDetail = budgetDetail.ParentNode;
                    if (parentDetail != null)
                    {
                        parentDetail.SumTotalValue();
                    }
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult Revert(string VersionID)
        {
            Action action = () =>
            {
                var versionDic = this.GetDataDicByID("S_EP_BudgetVersion", VersionID);
                var budget = new S_EP_BudgetVersion(versionDic);
                if (versionDic.GetValue("FlowPhase") != "Start")
                {
                    throw new Formula.Exceptions.BusinessValidationException("禁止删除已经发起流程的预算");
                }
                this.SQLDB.ExecuteNonQuery(String.Format("DELETE FROM S_EP_BudgetVersion WHERE ID='{0}'", VersionID));
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult CreateBugetVersion(string BudgetUnitID, string FormTmpCode)
        {
            var result = new Dictionary<string, object>();
            var unitDic = this.GetDataDicByID("S_EP_BudgetUnit", BudgetUnitID);
            if (unitDic == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("未能找到ID为【" + BudgetUnitID + "】的预算单元，请确认预算单元是否存在");
            }
            Action action = () =>
            {              
                var budgetUnit = new S_EP_BudgetUnit(unitDic);
                var version = budgetUnit.NewBudgetVersion(FormTmpCode);
                result = version.ModelDic;
            };
            this.ExecuteAction(action);
            return Json(result);
        }

    }
}
