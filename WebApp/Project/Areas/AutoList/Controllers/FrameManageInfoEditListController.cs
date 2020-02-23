using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

using Formula;
using Project.Logic.Domain;
using Config.Logic;
using Formula.Exceptions;
using Project.Logic;
using Formula.Helper;
using Config;
using System.Text;
using Workflow.Logic;
using System.Transactions;

namespace Project.Areas.AutoList.Controllers
{
    public class FrameManageInfoEditListController : ProjectEditListContorllor
    {
        public override ActionResult PageView(string tmplCode)
        {
            string userName = this.GetQueryString("SystemName");
            if (!string.IsNullOrEmpty(userName))
            {
                userName = HttpUtility.UrlDecode(userName);
                string pwd = this.GetQueryString("PWD");
                string sql = "select count(0) from S_A_User where Code ='" + userName + "'";
                var db = SQLHelper.CreateSqlHelper(ConnEnum.Base);
                if (Convert.ToInt32(db.ExecuteScalar(sql)) > 0 && !String.IsNullOrEmpty(userName))
                {
                    FormulaHelper.ContextSet("AgentUserLoginName", userName);
                    FormulaHelper.SetAuthCookie(userName);
                }
            }
            return base.PageView(tmplCode);
        }

        public override JsonResult SaveList()
        {
            Action action = () =>
            {
                var dataTable = this.ProjectSQLDB.ExecuteDataTable("select *,'0' as IsDelete from S_F_FrameInfo_FrameManageInfo").AsEnumerable();

                string listData = Request["ListData"];
                if (string.IsNullOrEmpty(listData))
                    throw new BusinessException("列表数据无法反序列化");

                var subTableDataList = JsonHelper.ToObject<List<Dictionary<string, string>>>(listData);
                var fieldDt = DbHelper.GetFieldTable(ConnEnum.Project.ToString(), "S_F_FrameInfo_FrameManageInfo").AsEnumerable();

                StringBuilder sql = new StringBuilder();
                var deleteDataList = subTableDataList.Where(a => a.GetValue("_state") == "removed").ToList();
                var idAry = deleteDataList.Select(a => a.GetValue("ID"));
                var ids = string.Join("','", idAry);
                if (!string.IsNullOrEmpty(ids))
                    sql.AppendFormat("\n delete from S_F_FrameInfo_FrameManageInfo where ID in('{0}');", ids);
                subTableDataList.RemoveWhere(a => a.GetValue("_state") == "removed");
                foreach (DataRow row in dataTable.Where(a => idAry.Contains(a["ID"].ToString())))
                    row["IsDelete"] = "1";

                foreach (var subItem in subTableDataList)
                {
                    var isNew = false;
                    string id = subItem.GetValue("ID");
                    string templateName = subItem.GetValue("TemplateName");
                    //modified、added、removed
                    if (subItem.GetValue("_state") == "removed")
                        continue;
                    if (subItem.GetValue("_state") == "added" || string.IsNullOrEmpty(id))
                    {
                        isNew = true;
                        id = FormulaHelper.CreateGuid();
                    }
                    subItem.SetValue("ID", id);

                    var sameTemplateNameCount = dataTable.Count(a => a["TemplateName"].ToString() == templateName
                        && a["ID"].ToString() != id && a["IsDelete"].ToString() == "0");
                    sameTemplateNameCount += subTableDataList.Count(a => a.GetValue("TemplateName") == templateName
                        && a.GetValue("ID") != id);
                    if (sameTemplateNameCount > 0)
                        throw new Formula.Exceptions.BusinessValidationException("图框【" + subItem.GetValue("TemplateName") + "】重复，保存失败");

                    if (subItem.ContainsKey("ModifyDate"))
                    {
                        subItem.SetValue("ModifyDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    if (subItem.ContainsKey("ModifyUserID"))
                    {
                        subItem.SetValue("ModifyUserID", FormulaHelper.GetUserInfo().UserID);
                    }
                    if (subItem.ContainsKey("ModifyUser"))
                    {
                        subItem.SetValue("ModifyUser", FormulaHelper.GetUserInfo().UserName);
                    }
                    sql.Append("\n");
                    if (isNew)
                        sql.Append(subItem.CreateInsertSql("Project", "S_F_FrameInfo_FrameManageInfo", id, fieldDt));
                    else
                        sql.Append(subItem.CreateUpdateSql("Project", "S_F_FrameInfo_FrameManageInfo", id, fieldDt));
                }

                if (sql.ToString() != "")
                {
                    if (Config.Constant.IsOracleDb)
                    {
                        this.ProjectSQLDB.ExecuteNonQuery(string.Format(@"
begin
{0}
end;", sql));
                    }
                    else
                    {
                        this.ProjectSQLDB.ExecuteNonQuery(sql.ToString());
                    }
                }
                this.AfterSave(subTableDataList, deleteDataList, null);
            };

            if (System.Configuration.ConfigurationManager.AppSettings["UseMsdtc"].ToLower() == "true")
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    action();
                    ts.Complete();
                }
            }
            else
            {
                action();
            }

            return Json("");
        }

        protected override void AfterSave(List<Dictionary<string, string>> list, List<Dictionary<string, string>> deleteList, Base.Logic.Domain.S_UI_List listInfo)
        {
            foreach (var dic in list)
            {
                var manageInfoID = dic.GetValue("ID");
                var bcs = this.BusinessEntities.Set<S_F_BorderConfig>().Where(a => a.ManageInfoID == manageInfoID).ToList();
                bcs.ForEach(a => this.BusinessEntities.Set<S_F_BorderConfig>().Remove(a));
                var borderConfigList = JsonHelper.ToList(dic.GetValue("BorderConfig"));
                foreach (var borderConfig in borderConfigList)
                {
                    var bc = new S_F_BorderConfig();
                    bc.ID = FormulaHelper.CreateGuid();
                    bc.BorderType = dic.GetValue("BorderType");
                    bc.BorderSize = dic.GetValue("Size");
                    bc.CurrentDefault = "0";
                    bc.DefaultTemplateName = "";
                    bc.ManageInfoID = manageInfoID;
                    this.BusinessEntities.Set<S_F_BorderConfig>().Add(bc);
                    var removeStr = "ID,BorderType,Size,ManageInfoID,CurrentDefault,DefaultTemplateName".Split(',');
                    borderConfig.RemoveWhere(a => removeStr.Contains(a.Key));

                    this.UpdateEntity<S_F_BorderConfig>(bc, borderConfig);
                }
            }
            foreach (var dic in deleteList)
            {
                var manageInfoID = dic.GetValue("ID");
                var bcs = this.BusinessEntities.Set<S_F_BorderConfig>().Where(a => a.ManageInfoID == manageInfoID).ToList();
                bcs.ForEach(a => this.BusinessEntities.Set<S_F_BorderConfig>().Remove(a));
            }
            this.BusinessEntities.SaveChanges();
        }

        public JsonResult GetFrameTypeEnum()
        {
            var sql = "select ID as value,FrameTypeName as text from S_F_FrameInfo";
            var data = this.ProjectSQLDB.ExecuteDataTable(sql);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBorderInfo(string ManageInfoID, string BorderType, string BorderSize)
        {
            var sql = "select * from S_F_BorderConfig where ManageInfoID='" + ManageInfoID + "'";
            var list = this.ProjectSQLDB.ExecuteDataTable(sql);
            if (string.IsNullOrEmpty(ManageInfoID) || list.Rows.Count == 0)
            {
                var categoryList = EnumBaseHelper.GetEnumDef("Project.BorderCategory").EnumItem;
                sql = "select * from S_F_BorderConfig where (ManageInfoID='' or ManageInfoID is null) and CurrentDefault='1'";
                var defaultList = this.ProjectSQLDB.ExecuteDataTable(sql);
                foreach (var category in categoryList)
                {
                    var newRow = list.NewRow();
                    var rows = defaultList.Select("Category='" + category.Code + "'");
                    if (rows.Length > 0)
                    {
                        newRow.ItemArray = rows[0].ItemArray;
                    }
                    else
                    {
                        newRow["Category"] = category.Code;
                    }
                    list.Rows.Add(newRow);
                }
            }
            return Json(list);
        }

        public JsonResult SetDefaultBorder(string DefaultTemplateName)
        {
            var sql = @"update S_F_BorderConfig set CurrentDefault='0'
update S_F_BorderConfig set CurrentDefault='1' where DefaultTemplateName='" + DefaultTemplateName + "'";
            this.ProjectSQLDB.ExecuteNonQuery(sql);
            return Json("");
        }
    }
}