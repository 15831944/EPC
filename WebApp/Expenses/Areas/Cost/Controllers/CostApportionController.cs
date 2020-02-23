using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Formula.Helper;
using Expenses.Logic;
using Expenses.Logic.Domain;
using Formula;
using System.Data;
namespace Expenses.Areas.Cost.Controllers
{
    public class CostApportionController : ExpensesFormController<S_EP_CostApportion>
    {
        protected override string TableName
        {
            get
            {
                return "S_EP_CostApportion";
            }
        }

        protected override void AfterGetData(DataTable dt, bool isNew, string upperVersionID)
        {
            if (!isNew)
            {
                if (dt.Rows[0]["CtrlSourceAttr"] != null && dt.Rows[0]["CtrlSourceAttr"] != DBNull.Value)
                {
                    var dic = JsonHelper.ToObject(dt.Rows[0]["CtrlSourceAttr"].ToString());
                    foreach (var key in dic.Keys)
                    {
                        if (!dt.Columns.Contains(key))
                        {
                            dt.Columns.Add(key);
                        }
                        dt.Rows[0][key] = dic[key];
                    }
                }
            }
            string defineCode = GetQueryString("DefineCode");
            var allDefines = this.InfrasDB.ExecuteDataTable("select * from S_EP_DefineApportion where Code = '" + defineCode + "'");
            var inputHtmlList = new Dictionary<string, object>();
            if (allDefines.Rows.Count == 0) return;
            DataRow row = allDefines.Rows[0];
            if (row["InputSource"] == null || row["InputSource"] == DBNull.Value)
                return;

            var define = new S_EP_DefineApportion(FormulaHelper.DataRowToDic(row));
            inputHtmlList.Add(row["ID"].ToString(), define.GetInputDefineHtml());

            ViewBag.Script += "\n  var inputHtmlDic = " + JsonHelper.ToJson(inputHtmlList) + ";";

            //分摊明细
            string showDetail = (!string.IsNullOrEmpty(define.ModelDic.GetValue("ShowDetail"))
                && define.ModelDic.GetValue("ShowDetail").ToLower() == "true").ToString().ToLower();
            ViewBag.Script += "\n  var showDetail = " + showDetail + ";";
            ViewBag.Script += "\n  var detailUrl = '" + define.ModelDic.GetValue("DetailUrl") + "';";
            string paramSetting = JsonHelper.ToJson(define.DetailParamDefineList);
            ViewBag.Script += "\n  var paramSetting = " + paramSetting + ";";

            //控件数据源
            ViewBag.Script += define.GetFormCtrlDataSoruce();
        }

        protected override void BeforeSave(Dictionary<string, string> dic, Base.Logic.Domain.S_UI_Form formInfo, bool isNew)
        {
            Dictionary<string, object> objDic = new Dictionary<string, object>();
            foreach (var tmp in dic)
            {
                objDic.Add(tmp.Key, tmp.Value);
            }
            dic.SetValue("CtrlSourceAttr", JsonHelper.ToJson(objDic));
            var apportionDefine = this.GetDataDicByID("S_EP_DefineApportion", dic.GetValue("ApportionDefine"), Config.ConnEnum.InfrasBaseConfig);
            if (apportionDefine == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到对应的成本分摊定义，保存失败");
            }

            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_LockAccount WHERE State='{2}' AND BelongYear={0} AND BelongMonth={1}",
               dic.GetValue("BelongYear"), dic.GetValue("BelongMonth"), "Finish"));
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经结账锁定，无法进行分摊计算", dic.GetValue("BelongYear"),
                   dic.GetValue("BelongMonth")));
            }

            dic.SetValue("CostType", apportionDefine.GetValue("CostType"));
            dic.SetValue("ApportionDefineCode", apportionDefine.GetValue("Code"));
            //CostFO.ValidatePeriodIsSettled(DateTime.Now);
        }

        public JsonResult ApportionCost(string DefineID, string InputData, string BelongYear, string BelongMonth)
        {
            var dic = this.GetDataDicByID("S_EP_DefineApportion", DefineID, Config.ConnEnum.InfrasBaseConfig);
            var inputDataDic = new Dictionary<string, object>();
            if (!String.IsNullOrEmpty(InputData))
            {
                inputDataDic = JsonHelper.ToObject(InputData);
            }

            if (String.IsNullOrEmpty(BelongYear)) throw new Formula.Exceptions.BusinessValidationException("成本分摊必须指定年份");
            if (String.IsNullOrEmpty(BelongMonth)) throw new Formula.Exceptions.BusinessValidationException("成本分摊必须指定月份");
            var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_LockAccount WHERE State='{2}' AND BelongYear='{0}' AND BelongMonth='{1}'",
              BelongYear, BelongMonth, "Finish"));
            if (dt.Rows.Count > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经结账锁定，无法进行分摊计算", dic.GetValue("BelongYear"),
                   dic.GetValue("BelongMonth")));
            }

            var define = new S_EP_DefineApportion(dic);
            var result = define.CalculateCost(inputDataDic);
            return Json(result);
        }

        public JsonResult RemoveApportion(string ListIDs)
        {
            Action action = () =>
            {
                foreach (var Id in ListIDs.Split(','))
                {
                    var dic = this.GetDataDicByID(this.TableName, Id);
                    if (dic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到对应的分摊记录，无法删除");
                    var costApportion = new S_EP_CostApportion(dic);
                    costApportion.Remove();
                }
            };
            this.ExecuteAction(action);
            return Json("");
        }

        public JsonResult ValidateRemove(string ListIDs)
        {
            var result = new Dictionary<string, object>();
            result.SetValue("Result", true.ToString().ToLower());
            foreach (var id in ListIDs.Split(','))
            {
                var dic = this.GetDataDicByID(this.TableName, id);
                if (dic == null) continue;
                if (String.IsNullOrEmpty(dic.GetValue("BelongYear"))) throw new Formula.Exceptions.BusinessValidationException("成本分摊必须指定年份");
                if (String.IsNullOrEmpty(dic.GetValue("BelongMonth"))) throw new Formula.Exceptions.BusinessValidationException("成本分摊必须指定月份");
                var dateTime = new DateTime(Convert.ToInt32(dic.GetValue("BelongYear"))
                    , Convert.ToInt32(dic.GetValue("BelongMonth")), 1);

                var dt = this.SQLDB.ExecuteDataTable(String.Format("SELECT * FROM S_EP_LockAccount WHERE State='{2}' AND BelongYear={0} AND BelongMonth={1}",
                  dic.GetValue("BelongYear"), dic.GetValue("BelongMonth"), "Finish"));
                if (dt.Rows.Count > 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(String.Format("{0}年{1}月已经结账锁定，无法移除成本分摊记录", dic.GetValue("BelongYear"),
                       dic.GetValue("BelongMonth")));
                }
                dt = this.SQLDB.ExecuteDataTable(String.Format(@"select top 1 ID,BelongYear,BelongMonth from S_EP_RevenueInfo 
where IncomeDate >= '{0}'", dateTime.ToString()));
                if (dt.Rows.Count > 0)
                {
                    result.SetValue("Result", false.ToString().ToLower());
                    result.SetValue("Msg", String.Format("{0}年{1}月已经有收入确认", dt.Rows[0]["BelongYear"], dt.Rows[0]["BelongMonth"]));
                    break;
                }
            }
            if (SysParams.Params.GetValue("RemoveApportionValidateMode").ToLower() == "none")
            {
                result.SetValue("Result", true.ToString().ToLower());
            }
            else if (SysParams.Params.GetValue("RemoveApportionValidateMode").ToLower() == "confirm")
            {
                if (result.GetValue("Result") == false.ToString().ToLower())
                    result.SetValue("Result", "confirm");
            }
            return Json(result);
        }

        protected override void OnFlowEnd(S_EP_CostApportion data, Workflow.Logic.Domain.S_WF_InsTaskExec taskExec, Workflow.Logic.Domain.S_WF_InsDefRouting routing)
        {
            if (data != null)
            {
                data.Push();
            }
        }



    }
}
