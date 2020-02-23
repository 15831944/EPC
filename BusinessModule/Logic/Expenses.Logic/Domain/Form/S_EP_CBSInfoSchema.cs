using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;
using Formula.Exceptions;
using System.Data;

namespace Expenses.Logic.Domain
{
    public class S_EP_CBSInfoSchema : BaseEPModel
    {
        public S_EP_CBSInfoSchema()
        {

        }
        public S_EP_CBSInfoSchema(Dictionary<string, object> dic)
        {
            this.FillModel(dic);
        }
        public void Push()
        {
            var cbsInfoDic = this.GetDataDicByID("S_EP_CBSInfo", this.ModelDic.GetValue("CBSInfoID"));
            if (cbsInfoDic == null) throw new Formula.Exceptions.BusinessValidationException("没有找到核算项目，无法变更");
            var cbsInfo = new S_EP_CBSInfo(cbsInfoDic);
            var defineNodeDt = this.InfrasDB.ExecuteDataTable(String.Format(@"SELECT * FROM S_EP_DefineCBSNode WHERE DefineID='{0}' ",
                 cbsInfoDic.GetValue("CBSDefineInfoID")));
            defineNodeDt.PrimaryKey = new DataColumn[] { defineNodeDt.Columns["ID"] };
            var detailDt = this.DB.ExecuteDataTable(@"SELECT * FROM S_EP_CBSInfoSchema_CBSNodeInfo 
WHERE S_EP_CBSInfoSchemaID='" + this.ModelDic.GetValue("ID") + "'");
            var sqlCommand = new StringBuilder();

            var addedList = new List<Dictionary<string, object>>();
            foreach (DataRow row in detailDt.Rows)
            {
                if (row["ModifyState"] == null || row["ModifyState"] == DBNull.Value)
                    continue;
                if (row["ModifyState"].ToString() == ModifyState.Added.ToString())
                {
                    var cbsNode = this.GetDataDicByID("S_EP_CBSNode", row["CBSNodeID"].ToString());
                    if (cbsNode != null)
                    {
                        Formula.LogWriter.Error("新增节点时候，CBS节点ID已存在，记录ID 为【" + row["ID"] + "】");
                        continue;
                    }
                    var defineNodeRow = defineNodeDt.Rows.Find(row["CBSDefineID"].ToString());
                    if (defineNodeRow == null)
                    {
                        Formula.LogWriter.Error("新增节点时候，没有找到CBS定义节点，记录ID 为【" + row["ID"] + "】");
                        continue;
                    }
                    var cbsNodeDic = FormulaHelper.DataRowToDic(row);
                    cbsNodeDic.SetValue("ID", row["CBSNodeID"].ToString());
                    cbsNodeDic.SetValue("CBSInfoID", cbsInfoDic.GetValue("ID"));
                    cbsNodeDic.SetValue("ParentID", row["CBSParentID"].ToString());
                    cbsNodeDic.SetValue("DefineID", row["CBSDefineID"].ToString());
                    cbsNodeDic.SetValue("FullID", row["CBSNodeFullID"].ToString());
                    cbsNodeDic.SetValue("NodeType", row["NodeType"].ToString());
                    cbsNodeDic.SetValue("CBSType", cbsInfoDic.GetValue("Type"));
                    if (String.IsNullOrEmpty(cbsNodeDic.GetValue("Code")))
                    {
                        cbsNodeDic.SetValue("Code",cbsInfoDic.GetValue("Code"));
                    }
                    cbsNodeDic.InsertDB(this.DB, "S_EP_CBSNode", cbsNodeDic.GetValue("ID"));
                    addedList.Add(cbsNodeDic);

                    cbsInfo.setUnit(cbsNodeDic, row, defineNodeRow);

                    #region 自动增加静态节点
                    var staticDefineNodeList = defineNodeDt.AsEnumerable().Where(c => c["FullID"] != DBNull.Value
                     && c["FullID"].ToString().StartsWith(defineNodeRow["FullID"].ToString()) && c["IsDynamic"].ToString() != "true").
                     OrderBy(c => c["FullID"].ToString()).ToList();
                    foreach (var staticDefineNodeRow in staticDefineNodeList)
                    {
                        var parentCBSNodeRows = addedList.Where(c => c["DefineID"] != DBNull.Value &&
                            c["DefineID"].ToString() == staticDefineNodeRow["ParentID"].ToString()).ToList();
                        foreach (var parentNodeRow in parentCBSNodeRows)
                        {
                            var staticNode = addedList.AsEnumerable().FirstOrDefault(c => c["ParentID"] != DBNull.Value &&
                                  c["ParentID"].ToString() == parentNodeRow["ID"].ToString() && c["DefineID"] != DBNull.Value &&
                                  c["DefineID"].ToString() == staticDefineNodeRow["ID"].ToString());
                            if (staticNode == null)
                            {
                                staticNode = new Dictionary<string, object>();
                                var staticDefineNodeDic = FormulaHelper.DataRowToDic(staticDefineNodeRow);
                                var parentCBS = new S_EP_CBSNode(parentNodeRow);
                                staticNode.SetValue("ID", FormulaHelper.CreateGuid());
                                staticNode.SetValue("DefineID", staticDefineNodeRow["ID"]);
                                staticNode.SetValue("Name", staticDefineNodeRow["Name"]);
                                staticNode.SetValue("Code", staticDefineNodeRow["Code"]);
                                staticNode.SetValue("NodeType", staticDefineNodeRow["NodeType"]);
                                if (staticNode.GetValue("NodeType") == CBSNodeType.Subject.ToString())
                                {
                                    staticNode.SetValue("SubjectID", staticDefineNodeRow["SubjectID"]);
                                    staticNode.SetValue("SubjectType", staticDefineNodeRow["SubjectType"]);
                                    staticNode.SetValue("Code", staticDefineNodeRow["SubjectCode"]);
                                    staticNode.SetValue("SubjectFullCode", staticDefineNodeRow["SubjectFullCode"]);
                                }
                                staticNode.SetValue("CBSType", cbsInfoDic.GetValue("Type"));
                                staticNode.SetValue("ChargerUser", cbsInfoDic.GetValue("ChargerUser"));
                                staticNode.SetValue("ChargerUserName", cbsInfoDic.GetValue("ChargerUserName"));
                                staticNode.SetValue("ChargerDept", cbsInfoDic.GetValue("ChargerDept"));
                                staticNode.SetValue("ChargerDeptName", cbsInfoDic.GetValue("ChargerDeptName"));
                                staticNode.SetValue("ContractInfoID", cbsInfoDic.GetValue("ContractInfoID"));
                                staticNode.SetValue("ProjectInfoID", cbsInfoDic.GetValue("ProjectInfoID"));
                                staticNode.SetValue("EngineeringInfoID", cbsInfoDic.GetValue("EngineeringInfoID"));
                                staticNode.SetValue("ClueInfoID", cbsInfoDic.GetValue("ClueInfoID"));
                                parentCBS.AddChild(staticNode);
                                addedList.Add(staticNode);
                            }
                        }
                    }
                    #endregion
                }
                else if (row["ModifyState"].ToString() == ModifyState.Modified.ToString())
                {
                    #region
                    var cbsNode = this.GetDataDicByID("S_EP_CBSNode", row["CBSNodeID"].ToString());
                    if (cbsNode == null) continue;
                    cbsNode.SetValue("Name", row["Name"]);
                    cbsNode.SetValue("Code", row["Code"]);
                    cbsNode.SetValue("ChargerUser", row["ChargerUser"]);
                    cbsNode.SetValue("ChargerUserName", row["ChargerUserName"]);
                    cbsNode.SetValue("ChargerDept", row["ChargerDept"]);
                    cbsNode.SetValue("ChargerDeptName", row["ChargerDeptName"]);
                    cbsNode.SetValue("ContractValue", row["ContractValue"]);
                    cbsNode.SetValue("ContractTaxValue", row["ContractTaxValue"]);
                    cbsNode.SetValue("ContractClearValue", row["ContractClearValue"]);
                    cbsNode.SetValue("TaxRate", row["TaxRate"]);
                    cbsNode.SetValue("DefineID", row["CBSDefineID"]);
                    cbsNode.SetValue("NodeType", row["NodeType"]);
                    sqlCommand.AppendLine(cbsNode.CreateUpdateSql(this.DB, "S_EP_CBSNode", cbsNode.GetValue("ID")));
                    #endregion

                    var defineNodeRow = defineNodeDt.Rows.Find(row["CBSDefineID"].ToString());
                    if (defineNodeRow == null)
                    {
                        Formula.LogWriter.Error("新增节点时候，没有找到CBS定义节点，记录ID 为【" + row["ID"] + "】");
                        continue;
                    }

                    cbsInfo.setUnit(cbsNode, row, defineNodeRow);
                }
                else if (row["ModifyState"].ToString() == ModifyState.Removed.ToString())
                {
                    var cbsNode = this.GetDataDicByID("S_EP_CBSNode", row["CBSNodeID"].ToString());
                    if (cbsNode == null) continue;
                    var node = new S_EP_CBSNode(cbsNode);
                    node.Delete();
                }
            }
            if (!String.IsNullOrEmpty(sqlCommand.ToString()))
                this.DB.ExecuteNonQuery(sqlCommand.ToString());
        }
    }
}
