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
    public class BudgetSubmitController : ExpensesFormController<S_EP_BudgetVersion>
    {
        protected override void AfterGetData(System.Data.DataTable dt, bool isNew, string upperVersionID)
        {
            if (!dt.Columns.Contains("Detail"))
            {
                dt.Columns.Add("Detail");
            }

            ViewBag.FlowEnd = false;
            if (!isNew)
            {
                dt.Columns.Add("BudgetBasisValue", typeof(decimal));
                var row = dt.Rows[0];
                var detailDt = this.SQLDB.ExecuteDataTable(String.Format(@"SELECT AllowAddChild,AllowEdit,AllowDelete,CalExpression,
CalDescription,S_EP_BudgetVersion_Detail.* FROM S_EP_BudgetVersion_Detail
LEFT JOIN {3}.dbo.S_EP_DefineBudget_Subject on S_EP_BudgetVersion_Detail.BudgetDetailDefineID = S_EP_DefineBudget_Subject.ID 
WHERE S_EP_BudgetVersionID='{0}' AND  ModifyState!='{1}' AND S_EP_BudgetVersion_Detail.NODETYPE!='{2}' ORDER BY SORTINDEX",
                                                                                                row["ID"], ModifyState.Removed.ToString(), CBSNodeType.Detail.ToString(),
                                                                                                this.InfrasDB.DbName));
                row["Detail"] = JsonHelper.ToJson(detailDt);
                //默认获取按比例计算的计算依据            
                var basisField = "ContractValue"; //默认预算依据为合同金额
                var budgetUnit = this.GetDataDicByID("S_EP_BudgetUnit", row["BudgetUnitID"].ToString());
                var defineUnitDic = this.GetDataDicByID("S_EP_DefineBudget", budgetUnit.GetValue("UnitDefineID"), ConnEnum.InfrasBaseConfig);
                if (defineUnitDic != null && !String.IsNullOrEmpty(defineUnitDic.GetValue("BudgetBasisField")))
                {
                    basisField = defineUnitDic.GetValue("BudgetBasisField");
                    if (dt.Columns.Contains(basisField))
                    {
                        row["BudgetBasisValue"] = row[basisField] == DBNull.Value ? 0m : Convert.ToDecimal(row[basisField]);
                    }
                    else
                    {
                        //如果配置的字段不存在，则默认是0；
                        row["BudgetBasisValue"] = 0m;
                    }
                }
                if (defineUnitDic == null)
                {
                    ViewBag.Tabs = "";
                }
                else
                {
                    ViewBag.Tabs = defineUnitDic.GetValue("DetailTabs");
                }
                ViewBag.FormID = row["ID"].ToString();
                ViewBag.FlowEnd = row["flowPhase"].ToString() == "End";
            }
            else
            {
                ViewBag.Tabs = "";
                string unitID = this.GetQueryString("UnitID");
                var budgetUnit = this.GetDataDicByID("S_EP_BudgetUnit", unitID);
                if (budgetUnit != null)
                {
                    var defineUnitDic = this.GetDataDicByID("S_EP_DefineBudget", budgetUnit.GetValue("UnitDefineID"));
                    if (defineUnitDic != null)
                        ViewBag.Tabs = defineUnitDic.GetValue("DetailTabs");
                }
                ViewBag.FormID = "";
            }
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            string detail = this.Request["Detail"];
            if (!String.IsNullOrEmpty(detail))
            {
                var list = JsonHelper.ToList(detail);

                #region 如果是子预算单元验证明细预算金额之和不能超过上限
                if (dic.GetValue("IsSub") == "true")
                {
                    var topList = list.Where(a => !list.Any(b => b.GetValue("CBSID") == a.GetValue("CBSParentID")));
                    string strbudgetValue = dic.GetValue("BudgetValueLimit");
                    decimal dBudgetValue = 0;
                    decimal.TryParse(strbudgetValue, out dBudgetValue);

                    decimal detailBudgetValueTotal = 0;
                    foreach (var detailDic in topList)
                    {
                        string strTotalValue = detailDic.GetValue("TotalValue");
                        decimal dTotalValue = 0;
                        decimal.TryParse(strTotalValue, out dTotalValue);
                        detailBudgetValueTotal += dTotalValue;
                    }
                    dBudgetValue = Math.Round(dBudgetValue, 2);
                    detailBudgetValueTotal = Math.Round(detailBudgetValueTotal, 2);

                    if (dBudgetValue < detailBudgetValueTotal)
                    {
                        throw new Formula.Exceptions.BusinessValidationException(string.Format("明细预算总和【{0}】不能超过预算金额上限【{1}】", detailBudgetValueTotal.ToString("f2"), dBudgetValue.ToString("f2")));
                    }
                }

                #endregion

                foreach (var item in list)
                {
                    if (item.GetValue("_state").ToLower() == "removed") continue;
                    if (item.GetValue("CustomEdit") == true.ToString().ToLower())
                    {
                        if (Convert.ToInt32(this.SQLDB.ExecuteScalar(
                            String.Format("SELECT COUNT(ID) FROM S_EP_BudgetVersion_Detail WHERE VersionID='{0}' AND CBSParentID='{1}'",
                            dic.GetValue("ID"), item.GetValue("CBSID")))) > 0
                            || !String.IsNullOrEmpty(item.GetValue("CalExpression")))
                        {
                            //只有非末级节点或者有公式的节点修改才会改变自定义编辑状态
                            item.SetValue("CustomEdit", true.ToString().ToLower());
                        }
                        else
                        {
                            item.SetValue("CustomEdit", false.ToString().ToLower());
                        }
                    }
                    var totalValue = String.IsNullOrEmpty(item.GetValue("TotalValue")) ? 0m : Convert.ToDecimal(item.GetValue("TotalValue"));
                    var lastVersionValue = String.IsNullOrEmpty(item.GetValue("LastVersionValue")) ? 0m : Convert.ToDecimal(item.GetValue("LastVersionValue"));

                    if (dic.GetValue("VersionNumber").Trim() != "1")
                    {
                        if (totalValue == lastVersionValue && item.GetValue("ModifyState") != ModifyState.Added.ToString() &&
                             item.GetValue("ModifyState") != ModifyState.Removed.ToString())
                        {
                            item.SetValue("ModifyState", ModifyState.Normal.ToString());
                        }
                        else
                        {
                            if (item.GetValue("ModifyState") == ModifyState.Normal.ToString())
                            {
                                item.SetValue("ModifyState", ModifyState.Modified.ToString());
                            }
                        }
                    }
                    item.UpdateDB(this.SQLDB, "S_EP_BudgetVersion_Detail", item.GetValue("ID"));
                }
            }
        }

        protected override void OnFlowEnd(S_EP_BudgetVersion data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
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
            if (String.IsNullOrEmpty(showAll) || showAll.ToLower() != "true")
            {
                qb.Add("NodeType", QueryMethod.NotEqual, "Detail");
            }
            var data = this.SQLDB.ExecuteDataTable(String.Format(@"SELECT AllowAddChild,AllowEdit,AllowDelete,CalExpression,
CalDescription,S_EP_BudgetVersion_Detail.* FROM S_EP_BudgetVersion_Detail
left join {0}.dbo.S_EP_DefineBudget_Subject
on S_EP_BudgetVersion_Detail.BudgetDetailDefineID = S_EP_DefineBudget_Subject.ID", this.InfrasDB.DbName), qb);
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

        public JsonResult AddVersionDetail(string NodeID, string AddMode, string VersionID)
        {
            var result = new Dictionary<string, object>();
            Action action = () =>
            {
                var nodeDic = this.GetDataDicByID("S_EP_BudgetVersion_Detail", NodeID);
                if (nodeDic == null) throw new Formula.Exceptions.BusinessValidationException("未能找到选中的费用科目节点，无法新增");
                var node = new S_EP_BudgetVersion_Detail(nodeDic);
                if (AddMode == "After")
                {
                    var child = node.AddBrotherNode(true);
                    result = child.ModelDic;
                }
                else if (AddMode == "AddChild")
                {
                    var child = node.AddChildNode(true);
                    result = child.ModelDic;
                }
            };
            this.ExecuteAction(action);
            return Json(result);
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

        public JsonResult ReloadForm(string ID, string showAll)
        {
            var dt = this.SQLDB.ExecuteDataTable("SELECT * FROM S_EP_BudgetVersion WHERE ID='" + ID + "'");
            var detailSql = @"SELECT * FROM S_EP_BudgetVersion_Detail WITH(NOLOCK) 
WHERE S_EP_BudgetVersionID='" + ID + "'";
            if (String.IsNullOrEmpty(showAll) || showAll.ToLower() == false.ToString().ToLower())
            {
                detailSql += "AND NODETYPE!='" + CBSNodeType.Detail.ToString() + "'";
            }
            var detail = this.SQLDB.ExecuteDataTable(detailSql);
            var result = new Dictionary<string, object>();
            if (dt.Rows.Count > 0)
            {
                result.SetValue("formData", FormulaHelper.DataRowToDic(dt.Rows[0]));
            }
            result.SetValue("DetailList", FormulaHelper.DataTableToListDic(detail));
            return Json(result);
        }

        public JsonResult SaveNodes(string NodeInfo, string VersionID)
        {
            Action action = () =>
            {
                var dic = this.GetDataDicByID("S_EP_BudgetVersion", VersionID);
                if (dic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到对应的预算信息，无法保存明细");
                var budgetVersion = new S_EP_BudgetVersion(dic);
                var list = JsonHelper.ToList(NodeInfo);
                foreach (var item in list)
                {
                    if (item.GetValue("_state").ToLower() == "removed") continue;
                    var totalValue = String.IsNullOrEmpty(item.GetValue("TotalValue")) ? 0m : Convert.ToDecimal(item.GetValue("TotalValue"));
                    var lastVersionValue = String.IsNullOrEmpty(item.GetValue("LastVersionValue")) ? 0m : Convert.ToDecimal(item.GetValue("LastVersionValue"));
                    if (dic.GetValue("VersionNumber").Trim() != "1")
                    {
                        if (totalValue == lastVersionValue && item.GetValue("ModifyState") != ModifyState.Added.ToString() &&
                             item.GetValue("ModifyState") != ModifyState.Removed.ToString())
                        {
                            item.SetValue("ModifyState", ModifyState.Normal.ToString());
                        }
                        else
                        {
                            if (item.GetValue("ModifyState") == ModifyState.Normal.ToString())
                            {
                                item.SetValue("ModifyState", ModifyState.Modified.ToString());
                            }
                        }
                        if (budgetVersion.BasisBudgetValue > 0)
                        {
                            var totalScale = totalValue / budgetVersion.BasisBudgetValue * 100;
                            item.SetValue("TotalScale", totalScale);
                        }
                    }
                    item.UpdateDB(this.SQLDB, "S_EP_BudgetVersion_Detail", item.GetValue("ID"));
                }
                budgetVersion.SumToTop();
                budgetVersion.SummaryTotalValue();
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult ImportResource(string VersionID, string ParentID, string ListData)
        {
            var result = new List<Dictionary<string, object>>();
            Action action = () =>
            {
                var parentNodeDic = this.GetDataDicByID("S_EP_BudgetVersion_Detail", ParentID);
                if (parentNodeDic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到父节点，无法导入资源");
                var parentBudgetDetail = new S_EP_BudgetVersion_Detail(parentNodeDic);
                var list = JsonHelper.ToList(ListData);
                foreach (var item in list)
                {
                    if (String.IsNullOrEmpty(item.GetValue("Name")))
                        throw new Formula.Exceptions.BusinessValidationException("导入的数据员必须指定Name列的值");
                    item.SetValue("ID", "");
                    item.SetValue("CBSID", "");
                   var detail = parentBudgetDetail.AddChildNode(item, true);
                   result.Add(detail.ModelDic);
                }
            };
            this.ExecuteAction(action);
            return Json(result);
        }

        public JsonResult MoveNode(string sourceID, string targetID, string dragAction)
        {
            var sourceNode = this.GetDataDicByID("S_EP_BudgetVersion_Detail", sourceID);
            if (sourceNode == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的设备，无法移动设备");
            Action action = () =>
            {
                if (dragAction.ToLower() == "before")
                {
                    var target = this.GetDataDicByID("S_EP_BudgetVersion_Detail", targetID);
                    if (target == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的目标节点，无法移动设备");
                    var targetSortIndex = String.IsNullOrEmpty(target.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(target.GetValue("SortIndex"));
                    this.SQLDB.ExecuteNonQuery(String.Format(@"UPDATE S_EP_BudgetVersion_Detail SET SortIndex=SortIndex-0.01 WHERE SortIndex<{0} AND 
                CBSParentID='{1}' AND S_EP_BudgetVersionID='{2}'", targetSortIndex, target.GetValue("CBSParentID"), sourceNode.GetValue("S_EP_BudgetVersionID")));
                    sourceNode.SetValue("SortIndex", targetSortIndex - 0.01m);
                    sourceNode.UpdateDB(this.SQLDB, "S_EP_BudgetVersion_Detail", sourceNode.GetValue("ID"));
                }
                else if (dragAction.ToLower() == "after")
                {
                    var target = this.GetDataDicByID("S_EP_BudgetVersion_Detail", targetID);
                    if (target == null) throw new Formula.Exceptions.BusinessValidationException("没有找到指定的目标节点，无法移动设备");
                    var targetSortIndex = String.IsNullOrEmpty(target.GetValue("SortIndex")) ? 0m : Convert.ToDecimal(target.GetValue("SortIndex"));
                    this.SQLDB.ExecuteNonQuery(String.Format(@"UPDATE S_EP_BudgetVersion_Detail SET SortIndex=SortIndex+0.01 WHERE SortIndex>{0} AND 
                CBSParentID='{1}' AND S_EP_BudgetVersionID='{2}'", targetSortIndex, target.GetValue("CBSParentID"), sourceNode.GetValue("S_EP_BudgetVersionID")));
                    sourceNode.SetValue("SortIndex", targetSortIndex + 0.01m);
                    sourceNode.UpdateDB(this.SQLDB, "S_EP_BudgetVersion_Detail", sourceNode.GetValue("ID"));
                }
            };
            this.ExecuteAction(action);
            return Json(sourceNode);
        }

        public JsonResult GetHRCostList(string VersionID)
        {
            var result = this.SQLDB.ExecuteDataTable(String.Format(@"select * from S_EP_BudgetVersion_Detail
where SubjectType='HRCost' and S_EP_BudgetVersionID='{0}' order by len(CBSFullID), SortIndex", VersionID));
            return Json(result);
        }

    }
}
