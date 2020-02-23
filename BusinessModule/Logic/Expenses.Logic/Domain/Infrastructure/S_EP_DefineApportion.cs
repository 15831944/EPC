using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Base.Logic.BusinessFacade;
using System.Data;
using System.Text.RegularExpressions;
using Formula.Exceptions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Base.Logic.Model.UI.Form;
using Base.Logic.Domain;

namespace Expenses.Logic.Domain
{
    public partial class S_EP_DefineApportion : BaseEPModel
    {
        public const string InputPre = "Input";
        public const string ParamPre = "Param";
        public const string RangePre = "Range";
        public const string FormPre = "Form";

        #region 公共属性

        public S_EP_DefineApportion(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }

        List<Dictionary<string, object>> _paramDefineList;
        [NotMapped]
        [JsonIgnore]
        public List<Dictionary<string, object>> ParamDefineList
        {
            get
            {
                if (_paramDefineList == null)
                {
                    _paramDefineList = String.IsNullOrEmpty(this.ModelDic.GetValue("ParamSource")) ? new List<Dictionary<string, object>>() :
                        JsonHelper.ToList(this.ModelDic.GetValue("ParamSource"));
                }
                return _paramDefineList;
            }
        }

        List<Dictionary<string, object>> _caculateDefineList;
        [NotMapped]
        [JsonIgnore]
        public List<Dictionary<string, object>> CaculateDefineList
        {
            get
            {
                if (_caculateDefineList == null)
                {
                    _caculateDefineList = String.IsNullOrEmpty(this.ModelDic.GetValue("CalList")) ? new List<Dictionary<string, object>>() :
                        JsonHelper.ToList(this.ModelDic.GetValue("CalList"));
                }
                return _caculateDefineList;
            }
        }

        List<Dictionary<string, object>> _inputDefineList;
        [NotMapped]
        [JsonIgnore]
        public List<Dictionary<string, object>> InputDefineList
        {
            get
            {
                if (_inputDefineList == null)
                {
                    _inputDefineList = String.IsNullOrEmpty(this.ModelDic.GetValue("InputSource")) ? new List<Dictionary<string, object>>() :
                        JsonHelper.ToList(this.ModelDic.GetValue("InputSource")).OrderBy(c => c["SortIndex"]).ToList();
                }
                return _inputDefineList;
            }
        }

        DataTable _rangeStructDt;
        [NotMapped]
        [JsonIgnore]
        public DataTable RangeStructDt
        {
            get
            {
                if (_rangeStructDt == null)
                {
                    var sql = "SELECT * FROM (" + this.GetStructRangeSQL() + ") TABLEINFO WHERE 1!=1";
                    _rangeStructDt = this.DB.ExecuteDataTable(sql);
                }
                return _rangeStructDt;
            }
        }

        List<Dictionary<string, object>> _detailParamDefineList;
        [NotMapped]
        [JsonIgnore]
        public List<Dictionary<string, object>> DetailParamDefineList
        {
            get
            {
                if (_detailParamDefineList == null)
                {
                    _detailParamDefineList = String.IsNullOrEmpty(this.ModelDic.GetValue("DetailParamDefine")) ? new List<Dictionary<string, object>>() :
                        JsonHelper.ToList(this.ModelDic.GetValue("DetailParamDefine")).ToList();
                }
                return _detailParamDefineList;
            }
        }
        #endregion

        public void Validate()
        {
            var rangeSQL = this.ModelDic.GetValue("ApportionSQL");
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");

            var formDef = UIFO.GetFormDef("CostApportion", string.Empty);
            var formItems = JsonHelper.ToObject<List<FormItem>>(formDef.Items);

            #region 校验计算范围的SQL语句定义是否正确
            var variateList = reg.Matches(rangeSQL);
            foreach (Match variate in variateList)
            {
                string value = variate.Value.Trim('{', '}');
                var matchDatas = value.Split('.');
                if (matchDatas.Length == 1)
                {
                    throw new Formula.Exceptions.BusinessValidationException("计算数据源中定义变量必须指定变量的来源，以固定格式{XXX.XXX}来定义");
                }
                if (matchDatas[0] == InputPre)
                {
                    if (this.InputDefineList.Count == 0 || this.InputDefineList.Count(c => c.GetValue("Code") == matchDatas[1]) == 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能找到编号为【" + matchDatas[1] + "】的输入控件定义");
                    }
                }
                else if (matchDatas[0] == ParamPre)
                {
                    if (this.ParamDefineList.Count == 0 || this.ParamDefineList.Count(c => c.GetValue("Code") == matchDatas[1]) == 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能找到编号为【" + matchDatas[1] + "】的参数数据源定义");
                    }
                }
                else if (matchDatas[0] == FormPre)
                {
                    if (formItems.Count == 0 || formItems.Count(c => c.Code == matchDatas[1]) == 0)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能在分摊页面找到编号为【" + matchDatas[1] + "】的控件定义");
                    }
                }
                else
                {
                    throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，无法识别数据获取对象，获取对象前缀必须是 Input 或 Param");
                }
            }
            #endregion

            #region 校验计算公式定义
            foreach (var calDefine in this.CaculateDefineList)
            {
                var content = calDefine.GetValue("Content");
                if (String.IsNullOrEmpty(content))
                    continue;
                if (reg.IsMatch(content))
                {
                    Match m = reg.Match(content);
                    string value = m.Value.Trim('{', '}');
                    var matchDatas = value.Split('.');
                    if (matchDatas.Length == 1)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("计算配置定义中，字段【" + calDefine.GetValue("Code") + "】的定义不正确，以固定格式{XXX.XXX}来定义");
                    }
                    if (matchDatas[0] == InputPre)
                    {
                        if (this.InputDefineList.Count == 0 || this.InputDefineList.Count(c => c.GetValue("Code") == matchDatas[1]) == 0)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能找到编号为【" + matchDatas[1] + "】的输入控件定义");
                        }
                    }
                    else if (matchDatas[0] == ParamPre)
                    {
                        if (this.ParamDefineList.Count == 0 || this.ParamDefineList.Count(c => c.GetValue("Code") == matchDatas[1]) == 0)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能找到编号为【" + matchDatas[1] + "】的参数数据源定义");
                        }
                    }
                    else if (matchDatas[0] == FormPre)
                    {
                        if (formItems.Count == 0 || formItems.Count(c => c.Code == matchDatas[1]) == 0)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("计算数据源定义中，未能在分摊页面找到编号为【" + matchDatas[1] + "】的控件定义");
                        }
                    }
                    else if (matchDatas[0] == RangePre)
                    {
                        if (this.RangeStructDt == null)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("计算数据源定义错误，请检查SQL语句");
                        }
                        if (!this.RangeStructDt.Columns.Contains(matchDatas[1]))
                        {
                            throw new Formula.Exceptions.BusinessValidationException("计算配置定义中，字段【" + calDefine.GetValue("Code") + "】的定义不正确，计算数据源不包含【" + matchDatas[1] + "】的字段");
                        }
                    }
                    else
                    {
                        throw new Formula.Exceptions.BusinessValidationException("计算配置定义中，字段【" + calDefine.GetValue("Code") + "】的定义不正确，前缀只能是 【" + InputPre + "】或【" + ParamPre + "】或【" + RangePre + "】");
                    }
                }
            }
            #endregion

            #region 校验参数数据源的SQL定义是否正确
            foreach (var item in this.ParamDefineList)
            {
                if (String.IsNullOrEmpty(item.GetValue("SQL")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("编号为【" + item.GetValue("Code") + "】的参数数据源SQL语句未定义");
                }
                var matchs = reg.Matches(item.GetValue("SQL"));
                foreach (Match m in matchs)
                {
                    var value = m.Value.Trim('{', '}');
                    var matchDatas = value.Split('.');
                    if (matchDatas.Length == 1)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("参数数据源定义中，字段【" + item.GetValue("Code") + "】的定义不正确，以固定格式{XXX.XXX}来定义");
                    }
                    else if (matchDatas[0] == InputPre)
                    {
                        if (this.InputDefineList.Count == 0 || this.InputDefineList.Count(c => c.GetValue("Code") == matchDatas[1]) == 0)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("参数数据源定义中，未能找到编号为【" + matchDatas[1] + "】的输入控件定义");
                        }
                    }
                    else if (matchDatas[0] == FormPre)
                    {
                        if (formItems.Count == 0 || formItems.Count(c => c.Code == matchDatas[1]) == 0)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("参数数据源定义中，未能在分摊页面找到编号为【" + matchDatas[1] + "】的控件定义");
                        }
                    }
                    else
                    {
                        throw new Formula.Exceptions.BusinessValidationException("参数数据源定义中，字段【" + item.GetValue("Code") + "】的定义不正确，前缀只能是 【" + InputPre + "】");
                    }
                }
            }
            #endregion

        }

        public string GetInputDefineHtml()
        {
            if (this.InputDefineList.Count(c => c.GetValue("Visible") == "True") == 0) return String.Empty;
            string html = @"
<table>
<tbody>
<tr>
 <td style='width:15%;'></td>
<td style='width:35%;'></td>
<td style='padding-left:20px;'></td>
 <td style='width:35%;'></td>
</tr>
{0}
</tbody>
</table>";
            var rowHtml = "";
            var changeRow = true;
            foreach (var item in this.InputDefineList)
            {
                if (item.GetValue("Visible") != "True") continue;
                if (item.GetValue("FullRow").ToLower() == "true")
                {
                    rowHtml += "<tr><td>" + item.GetValue("Name") + "</td><td colspan='3'>" + GetFormItemHtml(item) + "</td></tr>";
                    changeRow = true;
                }
                else
                {
                    if (changeRow)
                    {
                        rowHtml += "<tr>";
                        rowHtml += "<td>" + item.GetValue("Name") + "</td><td>" + GetFormItemHtml(item) + "</td>";
                        changeRow = false;
                    }
                    else
                    {
                        rowHtml += "<td style='padding-left:20px;'>" + item.GetValue("Name") + "</td><td>" + GetFormItemHtml(item) + "</td>";
                        rowHtml += "</tr>";
                        changeRow = true;
                    }
                }
            }
            return String.Format(html, rowHtml);
        }

        public List<Dictionary<string, object>> CalculateCost(Dictionary<string, object> inputData)
        {
            var result = new List<Dictionary<string, object>>();
            var paramData = this.GetParamDataSource(inputData);
            var dt = this.GetCalDataTable(inputData, paramData);
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            Regex numReg = new Regex("^(-?\\d+)(\\.\\d+)?");
            Regex sReg = new Regex("[\\+\\-\\*/]");
            var calDt = new DataTable();
            foreach (DataRow row in dt.Rows)
            {
                var dic = FormulaHelper.DataRowToDic(row);
                foreach (var calDefine in this.CaculateDefineList)
                {
                    string expression = calDefine.GetValue("Content");
                    if (String.IsNullOrEmpty(expression) || String.IsNullOrEmpty(calDefine.GetValue("Code")))
                        continue;
                    var calExpression = reg.Replace(expression, (Match m) =>
                    {
                        #region 处理定义配置信息
                        var value = m.Value.Trim('{', '}');
                        var fields = value.Split('.');
                        if (fields.Length < 2)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("【" + calDefine.GetValue("Code") + "】的变量设置必须要求为XXX.XXX的格式，请检查配置");
                        }
                        if (fields[0] == InputPre)
                        {
                            var returnValue = inputData.GetValue(fields[1]);
                            if (String.IsNullOrEmpty(returnValue))
                            {
                                throw new Formula.Exceptions.BusinessValidationException("计算【" + calDefine.GetValue("Code")
                                    + "】时出错，参与计算的输入参数" + fields[1] + "不能为空，请检查配置及输入信息");
                            }
                            if (!numReg.IsMatch(returnValue))
                            {
                                throw new Formula.Exceptions.BusinessValidationException("计算【" + calDefine.GetValue("Code")
                                    + "】时出错，参与计算的输入参数" + fields[1] + "必须是数字，请检查配置信息");
                            }
                            return returnValue;
                        }
                        else if (fields[0] == ParamPre)
                        {
                            var returnValue = paramData.GetValue(fields[1]);
                            if (String.IsNullOrEmpty(returnValue))
                            {
                                throw new Formula.Exceptions.BusinessValidationException("计算【" + calDefine.GetValue("Code")
                                    + "】时出错，参与计算的参数" + fields[1] + "不能为空，请检查配置及输入信息");
                            }
                            if (!numReg.IsMatch(returnValue))
                            {
                                throw new Formula.Exceptions.BusinessValidationException("计算【" + calDefine.GetValue("Code")
                                    + "】时出错，参与计算的参数" + fields[1] + "必须是数字，请检查配置信息");
                            }
                            return returnValue;
                        }
                        else if (fields[0] == RangePre)
                        {
                            if (!dt.Columns.Contains(fields[1]))
                            {
                                throw new Formula.Exceptions.BusinessValidationException("计算【" + calDefine.GetValue("Code") + "】时出错，参与计算的计算列必须包含在SQL语句中，请检查配置及输入信息");
                            }
                            if (row[fields[1]] == null || row[fields[1]] == DBNull.Value)
                            {
                                return "";
                            }
                            return row[fields[1]].ToString();
                        }
                        else
                        {
                            return "";
                        }
                        #endregion
                    });
                    object objValue = null;
                    try
                    {
                        if (sReg.IsMatch(calExpression) && calDefine.GetValue("UserFormula") == "1")
                        {
                            objValue = calDt.Compute(calExpression, "");
                        }
                        else
                        {
                            objValue = calExpression;
                        }
                    }
                    catch
                    {
                        objValue = calExpression;
                    }
                    dic.SetValue(calDefine.GetValue("Code"), objValue);
                }
                if (String.IsNullOrEmpty(dic.GetValue("CostUnitID")))
                {
                    throw new Formula.Exceptions.BusinessValidationException("成本分摊必须分摊到成本单元上，所以必须指定成本单元的ID");
                }
                dic.SetValue("ID", "");
                result.Add(dic);
            }
            return result;
        }

        public Dictionary<string, object> GetParamDataSource(Dictionary<string, object> inputData)
        {
            var result = new Dictionary<string, object>();
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            foreach (var item in this.ParamDefineList)
            {
                if (String.IsNullOrEmpty(item.GetValue("SQL"))) continue;
                var sql = reg.Replace(item.GetValue("SQL"), (Match m) =>
                {
                    var value = m.Value.Trim('{', '}');
                    var sourceNames = value.Split('.');
                    if (sourceNames.Length <= 1)
                    {
                        throw new Formula.Exceptions.BusinessValidationException("【" + item.GetValue("Code") + "】的变量设置必须要求为XXX.XXX的格式，请检查配置");
                    }
                    else
                    {
                        if (sourceNames[0] != InputPre && sourceNames[0] != FormPre)
                        {
                            throw new Formula.Exceptions.BusinessValidationException("【" + item.GetValue("Code") + "】数据源的变量前缀只能是Input或Form 检查配置");
                        }
                        return inputData.GetValue(sourceNames[1]);
                    }
                });
                object obj = 0;
                try
                {
                    obj = this.DB.ExecuteScalar(sql);
                }
                catch (Exception exp)
                {
                    throw new Formula.Exceptions.BusinessValidationException("参数数据源【" + item.GetValue("Code") + "】的SQL语句执行错误，请检查配置信息【" + exp.Message + "】");
                }
                var dRes = 0m;
                if (obj == null || obj == DBNull.Value)
                    obj = 0;
                if (!decimal.TryParse(obj.ToString(), out dRes))
                {
                    throw new BusinessException("编号为【" + item.GetValue("Code") + "】查询结果非数值");
                }
                result.SetValue(item.GetValue("Code"), dRes);
            }
            return result;
        }

        public string GetFormCtrlDataSoruce()
        {
            StringBuilder sb = new StringBuilder("\n");

            string items = this.ModelDic.GetValue("InputSource");
            var list = JsonHelper.ToObject<List<FormItem>>(items);

            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.Settings))
                    continue;
                if (item.ItemType == "CheckBoxList" || item.ItemType == "RadioButtonList" || item.ItemType == "ComboBox")
                {
                    var dic = JsonHelper.ToObject(item.Settings);
                    var data = dic["data"].ToString().Trim();
                    if (data == "")
                        continue;

                    ////获取数据源配置信息，枚举如果是设置的数据源，后续则不再进行枚举获取
                    //if (!data.StartsWith("[") && data.Split('.').Length == 1)
                    //{
                    //    if (defaultValueSettings.Exists(c => c.ContainsKey("Code") && c["Code"].ToString() == data))
                    //        continue;
                    //}

                    var key = GetEnumKey("", "Document", item.Code, data);
                    var enumStr = GetEnumString("", "Document", item.Code, data);

                    if (!string.IsNullOrEmpty(enumStr))
                    {
                        sb.AppendFormat("\n var {0} = {1}; ", key, enumStr);
                    }
                }
            }

            return sb.ToString();
            //var key = GetEnumKey(form.ConnName, form.TableName, item.Code, data);
            //var enumStr = GetEnumString(form.ConnName, form.TableName, item.Code, data);

            //if (!string.IsNullOrEmpty(enumStr))
            //{
            //    if (isOutput)
            //        sb.Append(GetOutputEnumString(form.ConnName, form.TableName, item.Code, data));
            //    else
            //        sb.AppendFormat("\n var {0} = {1}; ", key, enumStr);
            //}
        }
        #region  私有方法
        private string GetEnumString(string connName, string tableName, string fieldCode, string data)
        {
            bool fromMeta = false;
            if (data.StartsWith("[") == false)
            {
                var arr = data.Split(',');
                if (arr.Length == 3) //如果data为ConnName,tableName,fieldCode时
                {
                    connName = arr[0];
                    tableName = arr[1];
                    fieldCode = arr[2];
                    fromMeta = true;
                }
                else if (arr.Length == 2)//如果data为tableName,fieldCode时
                {
                    tableName = arr[0];
                    fieldCode = arr[1];
                    fromMeta = true;
                }
            }

            string result = "";
            if (data.StartsWith("["))
            {
                var LGID = FormulaHelper.GetCurrentLGID();
                if (!string.IsNullOrEmpty(LGID))
                {
                    var enums = JsonHelper.ToObject<List<Dictionary<string, object>>>(data);
                    if (enums.Count > 0 && enums.Where(c => c.Keys.Contains("textEN")).Count() > 0)
                    {
                        foreach (var item in enums)
                        {
                            var text = item.GetValue("textEN");
                            item.SetValue("text", text);
                            item.Remove("textEN");
                        }
                        result = JsonHelper.ToJson(enums);
                    }
                    else
                        result = data;
                }
                else
                    result = data;
            }
            else if (data == "" || data == "FromMeta" || fromMeta == true)
            {
                var entities = FormulaHelper.GetEntities<BaseEntities>();
                var field = entities.Set<S_M_Field>().FirstOrDefault(c => c.Code == fieldCode && c.S_M_Table.Code == tableName && c.S_M_Table.ConnName == connName);
                if (field == null || string.IsNullOrEmpty(field.EnumKey))
                    result = string.Format("var enum_{0}_{1} = {2};", tableName, fieldCode, "[]");
                else if (field.EnumKey.Trim().StartsWith("["))
                    result = field.EnumKey;
                else
                {
                    IEnumService enumService = FormulaHelper.GetService<IEnumService>();
                    try
                    {
                        result = JsonHelper.ToJson(enumService.GetEnumTable(field.EnumKey));
                    }
                    catch (Exception) { }
                }
            }
            else
            {
                IEnumService enumService = FormulaHelper.GetService<IEnumService>();
                try
                {
                    result = JsonHelper.ToJson(enumService.GetEnumTable(data));
                }
                catch (Exception) { }
            }

            return result;
        }

        DataTable GetCalDataTable(Dictionary<string, object> inputData, Dictionary<string, object> paramData)
        {
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            var sql = reg.Replace(this.ModelDic.GetValue("ApportionSQL"), (Match m) =>
            {
                var value = m.Value.Trim('{', '}');
                var sourceNames = value.Split('.');
                if (sourceNames.Length <= 1)
                    return "";
                else
                {
                    if (sourceNames[0] == InputPre)
                    {
                        return inputData.GetValue(sourceNames[1]);
                    }
                    else if (sourceNames[0] == ParamPre)
                    {
                        return paramData.GetValue(sourceNames[1]);
                    }
                    else if (sourceNames[0] == FormPre)
                    {
                        return inputData.GetValue(sourceNames[1]);
                    }
                    else
                        return "";
                }
            });
            var dt = new DataTable();
            try
            {
                dt = this.DB.ExecuteDataTable(sql);
            }
            catch (Exception exp)
            {
                throw new Formula.Exceptions.BusinessValidationException("获取计算数据源的SQL语句执行错误，请检查配置信息【" + exp.Message + "】");
            }
            return dt;
        }

        string GetStructRangeSQL()
        {
            var sql = this.ModelDic.GetValue("ApportionSQL");
            if (String.IsNullOrEmpty(sql))
                return "";
            Regex reg = new Regex("\\{[0-9a-zA-Z_\\.]*\\}");
            sql = reg.Replace(sql, (Match m) =>
            {
                var value = m.Value.Trim('{', '}');
                return "";
            });
            return sql;
        }

        string GetFormItemHtml(Dictionary<string, object> item)
        {
            var uiFo = FormulaHelper.CreateFO<UIFO>();
            string miniuiSettings = uiFo.GetMiniuiSettings(item.GetValue("Settings"));
            if (miniuiSettings == "")
                miniuiSettings = "style='width:100%'";

            string dataPty = ""; //控件的data属性
            if (item.GetValue("ItemType") == "TextBox" | item.GetValue("ItemType") == "TextArea")
            {
                if (!miniuiSettings.Contains("maxLength"))
                {
                    miniuiSettings += " maxLength='500'";
                }
            }
            else if (item.GetValue("ItemType") == "CheckBoxList" || item.GetValue("ItemType") == "RadioButtonList"
                || item.GetValue("ItemType") == "ComboBox")
            {
                dataPty = GetFormItemDataPty("Document", item.GetValue("Code"), item.GetValue("Settings"));
            }

            return string.Format("<input name='{0}' {5} class='mini-{1}' {2} {3} {4} {6}/>"
                , item.GetValue("Code"), item.GetValue("ItemType").ToLower(), miniuiSettings
                , item.GetValue("Enable").ToLower() == "true" ? "" : "enabled='false'"
                , item.GetValue("Visible").ToLower() == "true" ? "" : "visible='false'"
                , item.GetValue("ItemType") == "ButtonEdit" ? (Config.Constant.IsOracleDb ? string.Format("textName='{0}NAME'", item.GetValue("Code")) : string.Format("textName='{0}Name'", item.GetValue("Code"))) : ""
                , dataPty
                );
        }

        string GetFormItemDataPty(string tableName, string fieldCode, string settings)
        {
            if (string.IsNullOrEmpty(settings))
                return "";

            var dic = JsonHelper.ToObject<Dictionary<string, string>>(settings);
            string data = "";
            if (dic.ContainsKey("data"))
                data = dic["data"];
            return string.Format(" data='{0}'", GetEnumKey("", tableName, fieldCode, data));
        }

        string GetEnumKey(string connName, string tableName, string fieldCode, string data)
        {
            bool fromMeta = false;
            if (data.StartsWith("[") == false)
            {
                var arr = data.Split(',');
                if (arr.Length == 3) //如果data为ConnName,tableName,fieldCode时
                {
                    connName = arr[0];
                    tableName = arr[1];
                    fieldCode = arr[2];
                    fromMeta = true;
                }
                else if (arr.Length == 2)//如果data为tableName,fieldCode时
                {
                    tableName = arr[0];
                    fieldCode = arr[1];
                    fromMeta = true;
                }
            }

            string result = "";
            if (data.StartsWith("["))
                result = string.Format("enum_{0}_{1}", tableName, fieldCode);
            else if (data == "" || data == "FromMeta" || fromMeta == true)
            {
                result = string.Format("enum_{0}_{1}", tableName, fieldCode);
            }
            else
            {
                result = data.Split('.').Last();
            }

            return result;
        }
        #endregion

    }
}
