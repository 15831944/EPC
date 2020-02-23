using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Base.Logic.Model.UI.Form;
using Base.Logic.BusinessFacade;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_BudgetUnit : BaseEPModel
    {

        public S_EP_CBSNode _node;
        [NotMapped]
        [JsonIgnore]
        public S_EP_CBSNode CBSNode
        {
            get
            {
                if (_node == null)
                {
                    var cbsDic = this.GetDataDicByID("S_EP_CBSNode", this.ModelDic.GetValue("CBSNodeID"));
                    if (cbsDic != null)
                    {
                        _node = new S_EP_CBSNode(cbsDic);
                    }
                }
                return _node;
            }
        }

        public S_EP_BudgetUnit()
        {

        }

        public S_EP_BudgetUnit(Dictionary<string, object> model)
        {
            if (model == null)
                throw new Formula.Exceptions.BusinessValidationException("初始化构造S_EP_BudgetUnit的键值对不能对空值");
            this.FillModel(model);
        }

        public void SetBudgetDefineID()
        {
            var sql = "SELECT * FROM S_EP_DefineBudget WITH(NOLOCK) WHERE NodeID='" + this.CBSNode.ModelDic.GetValue("DefineID") + "'";
            var budgetDefineDt = this.InfrasDB.ExecuteDataTable(sql);
            var resID = string.Empty;
            foreach (DataRow budgetDefine in budgetDefineDt.Rows)
            {
                if (budgetDefine["Condition"] != null && budgetDefine["Condition"] != DBNull.Value &&
                    !String.IsNullOrEmpty(budgetDefine["Condition"].ToString()))
                {
                    var resultList = new List<InComeCondition>();
                    bool result = false;
                    var conditions = JsonHelper.ToList(budgetDefine["Condition"].ToString());
                    foreach (var condition in conditions)
                    {
                        var conditionResult = false;
                        #region 校验条件定义
                        var propertyValue = this.CBSNode.ModelDic.GetValue(condition.GetValue("FieldName"));
                        var condiftionValue = condition.GetValue("Value");
                        switch (condition.GetValue("QueryMode"))
                        {
                            default:
                            case "In":
                                conditionResult = condiftionValue.Split(',').Contains(propertyValue);
                                break;
                            case "Like":
                                conditionResult = propertyValue.Contains(condiftionValue);
                                break;
                            case "GreaterThanOrEqual":
                                conditionResult = Convert.ToDecimal(propertyValue) >= Convert.ToDecimal(condiftionValue);
                                break;
                            case "LessThanOrEqual":
                                conditionResult = Convert.ToDecimal(propertyValue) <= Convert.ToDecimal(condiftionValue);
                                break;
                            case "Equal":
                                conditionResult = propertyValue.Trim() == condiftionValue.Trim();
                                break;
                            case "LessThan":
                                conditionResult = Convert.ToDecimal(propertyValue) < Convert.ToDecimal(condiftionValue);
                                break;
                            case "GreaterThan":
                                conditionResult = Convert.ToDecimal(propertyValue) > Convert.ToDecimal(condiftionValue);
                                break;
                        }
                        #endregion
                        condition.SetValue("Result", conditionResult);
                        resultList.Add(new InComeCondition
                        {
                            FieldName = condition.GetValue("FieldName"),
                            GroupName = condition.GetValue("Group"),
                            Result = conditionResult
                        });
                    }
                    var groupInfoList = resultList.Select(d => d.GroupName).Distinct().ToList();
                    foreach (var groupInfo in groupInfoList)
                    {
                        if (resultList.Where(d => d.GroupName == groupInfo).Count(d => !d.Result) == 0)
                        {
                            result = true; break;
                        }
                    }
                    if (result)
                    {
                        resID = budgetDefine["ID"].ToString();
                        break;
                    }
                }
            }
            if (String.IsNullOrEmpty(resID))
            {
                var defaultMode = budgetDefineDt.AsEnumerable().FirstOrDefault(d => d["IsDefault"].ToString() == true.ToString().ToLower());
                if (defaultMode != null)
                    resID = defaultMode["ID"].ToString();
            }
            if (String.IsNullOrEmpty(resID))
            {
                throw new Formula.Exceptions.BusinessValidationException("没有匹配到对应的预算定义方式，无法建立预算单元，请确定预算配置中是否有满足条件的配置或默认值");
            }
            this.DB.ExecuteNonQuery(String.Format("UPDATE S_EP_BudgetUnit SET UnitDefineID='{0}' WHERE ID='{1}'", resID, this.ModelDic.GetValue("ID")));
            this.ModelDic.SetValue("UnitDefineID", resID);
        }

        public S_EP_BudgetVersion NewBudgetVersion(string formTmpCode = "")
        {
            var user = FormulaHelper.GetUserInfo();
            var obj = Convert.ToInt32(this.DB.ExecuteScalar(String.Format("SELECT COUNT(ID) FROM S_EP_BudgetVersion WHERE BudgetUnitID='{0}' AND FlowPhase!='End'", this.ID)));
            if (obj > 0)
            {
                throw new Formula.Exceptions.BusinessValidationException(String.Format("【{0}】的预算已经在编制或审批中，请刷新数据查看最新状态", this.ModelDic.GetValue("Name")));
            }
            var maxVersionNo = Convert.ToInt32(this.DB.ExecuteScalar(String.Format(@"SELECT ISNULL(MAX(VersionNumber),'0') FROM S_EP_BudgetVersion WITH(NOLOCK) 
            WHERE BudgetUnitID='{0}'", this.ID)));
            var unitCbsNode = this.GetDataDicByID("S_EP_CBSNode", this.ModelDic.GetValue("CBSNodeID"));
            if (unitCbsNode == null)
            {
                throw new Formula.Exceptions.BusinessValidationException("没有找到预算指定的CBS节点，无法编制预算");
            }

            var lastVersionDt = this.DB.ExecuteDataTable(String.Format(@"SELECT TOP 1 * FROM S_EP_BudgetVersion WITH(NOLOCK) 
WHERE BudgetUnitID='{0}' AND FLOWPHASE='End' ORDER BY ID DESC", this.ID));

            var versionDic = new Dictionary<string, object>();

            #region 判断是否有父级预算单元，是否有做过预算
            string sqlParentUnit = @"select S_EP_CBSNode.* from S_EP_CBSNode left join {1}.dbo.S_EP_DefineCBSNode on S_EP_DefineCBSNode.ID = S_EP_CBSNode.DefineID
                                   where '{0}' like S_EP_CBSNode.FullID + '%' and S_EP_CBSNode.FullID != '{0}' and S_EP_DefineCBSNode.BudgetUnit = 'true'";
            var parentUnitDt = this.DB.ExecuteDataTable(string.Format(sqlParentUnit, unitCbsNode.GetValue("FullID"), this.InfrasDB.DbName));
            versionDic.SetValue("IsSub", "false");
            //有父级预算单元
            if (parentUnitDt != null && parentUnitDt.Rows.Count > 0)
            {
                versionDic.SetValue("IsSub", "true");
                string sqlParentUnitVersion = @"select S_EP_BudgetVersion.* from 
                                                (select max(ID) as MaxID ,BudgetUnitID from S_EP_BudgetVersion group by BudgetUnitID) latestBudgetVersion
                                                left join S_EP_BudgetVersion on S_EP_BudgetVersion.ID = latestBudgetVersion.MaxID
                                                left join S_EP_BudgetUnit on S_EP_BudgetUnit.ID = S_EP_BudgetVersion.BudgetUnitID
                                                where S_EP_BudgetVersion.FlowPhase = 'End' and S_EP_BudgetUnit.CBSNodeID = '{0}'";
                var parentUnitVersionDt = this.DB.ExecuteDataTable(string.Format(sqlParentUnitVersion, parentUnitDt.Rows[0]["ID"]));
                if (parentUnitVersionDt == null || parentUnitVersionDt.Rows.Count == 0)
                {
                    throw new Formula.Exceptions.BusinessValidationException(string.Format("必须先编制【{0}】预算", parentUnitDt.Rows[0]["Name"]));
                }
                versionDic.SetValue("BudgetValueLimit", unitCbsNode.GetValue("BudgetValue"));//子预算单元的预算上限
            }
            #endregion

            foreach (var key in unitCbsNode.Keys)
            {
                if (unitCbsNode[key] != null)
                    versionDic.SetValue(key, unitCbsNode[key]);
            }

            this.setBudgetFormDefaultValue(versionDic, formTmpCode);

            decimal tmpContractValue = 0;
            decimal.TryParse(versionDic.GetValue("ContractClearValue"), out tmpContractValue);
            decimal tmpbudgetValueValue = 0;
            decimal.TryParse(versionDic.GetValue("BudgetValue"), out tmpbudgetValueValue);
            decimal budgetProfit = tmpContractValue - tmpbudgetValueValue;
            versionDic.SetValue("BudgetProfit", budgetProfit);
            if (tmpContractValue != 0)
            {
                versionDic.SetValue("BudgetProfitScale", (budgetProfit * 100 / tmpContractValue).ToString("0.00"));
                versionDic.SetValue("BudgetScale", (tmpbudgetValueValue * 100 / tmpContractValue).ToString("0.00"));
            }

            versionDic.SetValue("ID", FormulaHelper.CreateGuid());
            versionDic.SetValue("FlowPhase", "Start");
            versionDic.SetValue("BudgetUnitID", this.ID);
            if (String.IsNullOrEmpty(versionDic.GetValue("Name")))
                versionDic.SetValue("Name", unitCbsNode.GetValue("Name"));
            if (String.IsNullOrEmpty(versionDic.GetValue("Code")))
                versionDic.SetValue("Code", unitCbsNode.GetValue("Code"));
            versionDic.SetValue("RegisterUser", user.UserID);
            versionDic.SetValue("RegisterUserName", user.UserName);
            versionDic.SetValue("CBSInfoID", this.ModelDic.GetValue("CBSInfoID"));
            versionDic.SetValue("CreateDate", DateTime.Now);
            versionDic.SetValue("CreateUser", user.UserName);
            versionDic.SetValue("CreateUserID", user.UserID);
            versionDic.SetValue("VersionNumber", maxVersionNo + 1);
            versionDic.InsertDB(this.DB, "S_EP_BudgetVersion", versionDic.GetValue("ID"));
            var newVersion = new S_EP_BudgetVersion(versionDic);
            if (lastVersionDt.Rows.Count > 0)
            {
                //升版
                var lastVersionDic = FormulaHelper.DataRowToDic(lastVersionDt.Rows[0]);
                var lastVersion = new S_EP_BudgetVersion(lastVersionDic);
                var cbsDt = this.DB.ExecuteDataTable(String.Format(@"SELECT ID,Name,SubjectID,SubjectCode,SubjectFullCode,CostValue FROM S_EP_CBSNode
WHERE CBSInfoID='{0}'", this.ModelDic.GetValue("CBSInfoID")));
                var detailDt = this.DB.ExecuteDataTable(String.Format(@"SELECT * FROM S_EP_BudgetVersion_Detail WITH(NOLOCK) 
WHERE S_EP_BudgetVersionID='{0}' AND MODIFYSTATE!='{1}' ORDER BY CBSFULLID",
                    lastVersionDic.GetValue("ID"), ModifyState.Removed.ToString()));
                foreach (DataRow newDetailRow in detailDt.Rows)
                {
                    var newDetail = FormulaHelper.DataRowToDic(newDetailRow);
                    newDetail.SetValue("ID", FormulaHelper.CreateGuid());
                    newDetail.SetValue("S_EP_BudgetVersionID", versionDic.GetValue("ID"));
                    newDetail.SetValue("LastVersionValue", newDetailRow["TotalValue"]);
                    newDetail.SetValue("AdjustValue", 0);
                    newDetail.SetValue("ModifyState", ModifyState.Normal.ToString());
                    var cbsNode = cbsDt.Select("ID='" + newDetail.GetValue("CBSID") + "'").FirstOrDefault();
                    if (cbsNode != null)
                    {
                        newDetail.SetValue("CostValue", cbsNode["CostValue"]);
                        var scale = 0m;
                        if (cbsNode["CostValue"] != null && cbsNode["CostValue"] != DBNull.Value && !String.IsNullOrEmpty(newDetail.GetValue("TotalValue"))
                            && Convert.ToDecimal(newDetail.GetValue("TotalValue")) > 0)
                        {
                            scale = Convert.ToDecimal(cbsNode["CostValue"]) / Convert.ToDecimal(newDetail.GetValue("TotalValue")) * 100;
                        }
                        newDetail.SetValue("CostScale", Math.Round(scale, 2));
                        newDetail.SetValue("CostValue", cbsNode["CostValue"]);
                    }
                    newDetail.InsertDB(this.DB, "S_EP_BudgetVersion_Detail", newDetail.GetValue("ID"));
                }
            }
            else
            {
                //新建
                newVersion.InitBudgetDetail();
            }
            return newVersion;
        }

        void setBudgetFormDefaultValue(Dictionary<string, object> budgetForm, string formTmpCode)
        {
            var uiFO = FormulaHelper.CreateFO<UIFO>();
            var baseEntities = FormulaHelper.GetEntities<Base.Logic.Domain.BaseEntities>();
            if (string.IsNullOrEmpty(formTmpCode)) return;
            var formInfo = baseEntities.Set<Base.Logic.Domain.S_UI_Form>().Where(c => c.Code == formTmpCode).OrderByDescending(c => c.ID).FirstOrDefault(); //获取最新一个版本即可
            if (formInfo == null)
                throw new Exception("表单编号为" + formTmpCode + "不存在!");
            var items = JsonHelper.ToObject<List<FormItem>>(formInfo.Items);
            Dictionary<string, DataTable> defaultValueRows = null;// uiFO.GetDefaultValueDic(formInfo.DefaultValueSettings);
            defaultValueRows = uiFO.GetDefaultValueDic(formInfo.DefaultValueSettings);

            #region 新对象默认值
            //包含默认值设置则初始化默认值
            items.Where(c => !string.IsNullOrEmpty(c.DefaultValue)).ToList().ForEach(d =>
            {
                if (d.ItemType == "SubTable")
                {
                    //子表                   
                }
                if ((d.ItemType == "ButtonEdit" || d.ItemType == "ComboBox") && d.DefaultValue.Split(',').Count() == 2)
                {
                    //键值控件
                    string v1 = uiFO.GetDefaultValue(d.Code, d.DefaultValue.Split(',')[0], defaultValueRows);
                    string v2 = uiFO.GetDefaultValue(d.Code, d.DefaultValue.Split(',')[1], defaultValueRows);
                    if (!string.IsNullOrEmpty(v1) && v1.Contains('{') == false)
                        budgetForm.SetValue(d.Code, v1);
                    if (!string.IsNullOrEmpty(v2) && v2.Contains('{') == false)
                    {
                        var fieldName = "";
                        if (Config.Constant.IsOracleDb)
                        {
                            fieldName = d.Code + "NAME";
                            if (!String.IsNullOrWhiteSpace(d.Settings))
                            {
                                var settings = JsonHelper.ToObject(d.Settings);
                                var txtName = settings.GetValue("textName");
                                if (!String.IsNullOrEmpty(txtName))
                                {
                                    fieldName = txtName;
                                }
                            }
                        }
                        else
                        {
                            fieldName = d.Code + "Name";
                            if (!String.IsNullOrWhiteSpace(d.Settings))
                            {
                                var settings = JsonHelper.ToObject(d.Settings);
                                var txtName = settings.GetValue("textName");
                                if (!String.IsNullOrEmpty(txtName))
                                {
                                    fieldName = txtName;
                                }
                            }
                        }
                        budgetForm.SetValue(fieldName, v2);
                    }
                }
                else
                {
                    //单值控件
                    string v = uiFO.GetDefaultValue(d.Code, d.DefaultValue, defaultValueRows);
                    if (!string.IsNullOrEmpty(v) && v.Contains('{') == false)
                        budgetForm.SetValue(d.Code, v);
                }
            });

            #endregion

        }
    }
}
