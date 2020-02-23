using DocSystem.Logic.Domain;
using Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Config.Logic;
using Base.Logic;
using Base.Logic.Domain;
using Formula.Helper;
using MvcAdapter;
using Config;
using System.Data;
using System.Data.SqlClient;

namespace DocSystem.Areas.Config.Controllers
{
    public class ReorganizeConfigController : DocConfigController<S_DOC_ReorganizeConfig>
    {
        //
        // GET: /Config/ReorganizeConfig/

        public ActionResult Config()
        {
            return View();
        }

        public override JsonResult GetModel(string id)
        {
            var spaceId = GetQueryString("SpaceID");
            if (string.IsNullOrEmpty(spaceId)) throw new Formula.Exceptions.BusinessException("没有找到SpaceID");
            var entity = this.entities.Set<S_DOC_ReorganizeConfig>().FirstOrDefault(a => a.SpaceID == spaceId);
            var dic = new Dictionary<string, object>();
            var items = new List<Dictionary<string, object>>();
            if (entity != null)
            {
                dic = FormulaHelper.ModelToDic<S_DOC_ReorganizeConfig>(entity);
                if (!string.IsNullOrEmpty(entity.Items))
                    items = JsonHelper.ToList(entity.Items);
            }
            var baseEntities = FormulaHelper.GetEntities<BaseEntities>();
            var formDef = baseEntities.Set<S_UI_Form>().FirstOrDefault(a => a.Code == "Reorganize");
            if (formDef != null && !string.IsNullOrEmpty(formDef.Items))
            {
                var columns = JsonHelper.ToList(formDef.Items);
                var subItem = columns.FirstOrDefault(a => a.GetValue("Code") == "DocumentList");
                var settingsJson = subItem.GetValue("Settings");
                if (!string.IsNullOrEmpty(settingsJson))
                {
                    var defItemsJson = JsonHelper.ToObject(settingsJson).GetValue("listData");
                    if (!string.IsNullOrEmpty(defItemsJson))
                    {
                        var defItems = JsonHelper.ToList(defItemsJson);
                        var removeItems = new List<Dictionary<string, object>>();
                        //删除已经不存在的字段
                        foreach (var item in items)
                        {
                            var code = item.GetValue("Code");
                            if (!defItems.Any(a => a.GetValue("Code") == code))
                                removeItems.Add(item);
                        }
                        //新增字段
                        foreach (var item in defItems)
                        {
                            var code = item.GetValue("Code");
                            if (!items.Any(a => a.GetValue("Code") == code))
                                items.Add(item);
                        }
                        foreach (var item in removeItems)
                            items.Remove(item);
                    }
                }
            }
            dic.SetValue("SpaceID", spaceId);
            dic.SetValue("dataGrid", JsonHelper.ToJson(items));
            return Json(dic);
        }

        protected override void BeforeSave(S_DOC_ReorganizeConfig entity, bool isNew)
        {
            entity.SpaceID = this.GetQueryString("SpaceID");
            var detail = this.GetQueryString("dataGrid");
            if (!string.IsNullOrEmpty(detail))
                entity.Items = JsonHelper.ToJson(detail);
            //按件整编文件与表单对应表
            var fileFormRelation = this.GetQueryString("dataGridPhysicalMainForm");
            var formRelationList = JsonHelper.ToObject<List<S_DOC_ReorganizeConfig_FileFormRelation>>(fileFormRelation);
            var existList = entity.S_DOC_ReorganizeConfig_FileFormRelation.ToList();
            //增加、修改
            if (!string.IsNullOrEmpty(fileFormRelation))
            {
                foreach (var item in formRelationList)
                {
                    var relation = existList.FirstOrDefault(a => a.ID == item.ID);
                    if (relation == null)
                    {
                        relation = new S_DOC_ReorganizeConfig_FileFormRelation();
                        relation.ID = FormulaHelper.CreateGuid();
                        relation.ReorganizeConfigID = entity.ID;
                        existList.Add(relation);
                        entity.S_DOC_ReorganizeConfig_FileFormRelation.Add(relation);
                    }
                    relation.FileID = item.FileID;
                    relation.FormCode = item.FormCode;
                }
                //删除
                existList.RemoveWhere(a => !formRelationList.Any(r => r.ID == a.ID));
            }
            //按卷整编卷与表单对应表dataGridPhysicalVolumeForm
            string PhysicalVolumeForm = this.GetQueryString("dataGridPhysicalVolumeForm");
            if (!string.IsNullOrEmpty(PhysicalVolumeForm))
            {
                var formVolumeRelationList = JsonHelper.ToObject<List<S_DOC_ReorganizeConfig_NodeFormRelation>>(PhysicalVolumeForm);
                var existVolumeList = entity.S_DOC_ReorganizeConfig_NodeFormRelation.ToList();
                //增加、修改
                foreach (var item in formVolumeRelationList)
                {
                    var relation = existVolumeList.FirstOrDefault(a => a.ID == item.ID);
                    if (relation == null)
                    {
                        relation = new S_DOC_ReorganizeConfig_NodeFormRelation();
                        relation.ID = FormulaHelper.CreateGuid();
                        relation.ReorganizeConfigID = entity.ID;
                        existVolumeList.Add(relation);
                        entity.S_DOC_ReorganizeConfig_NodeFormRelation.Add(relation);
                    }
                    relation.NodeID = item.NodeID;
                    relation.FormCode = item.FormCode;
                }
                //删除
                existVolumeList.RemoveWhere(a => !formVolumeRelationList.Any(r => r.ID == a.ID));
            }
            //string formCode = GetQueryString("formCode");
            string formCode = GetQueryString("PhysicalMainFormCode");
            //string listCode = GetQueryString("listCode");
            //PhysicalMainListCode
            string listCode = GetQueryString("PhysicalMainListCode");
            //保存并检测表单和列表是否存在
            SaveCheck(formCode, listCode);
            string formVolumeCode = GetQueryString("PhysicalMainListVolumeCode");
            SaveCheck("", formVolumeCode);
        }
        //实物登记表单数据
        public JsonResult FileConfigGetList()
        {
            string SpaceID = this.Request["SpaceID"];
            string sql = @"select  S_DOC_File.ID FileID,Name,SpaceID,FormRela.ID,FormCode,FormRela.ReorganizeConfigID
            from S_DOC_File with(nolock) left join  S_DOC_ReorganizeConfig_FileFormRelation FormRela
            on FormRela.FileID=S_DOC_File.ID where SpaceID='" + SpaceID + "' order by SortIndex asc";
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable dataList = sqlHelp.ExecuteDataTable(sql);
            return Json(dataList);
        }
        //案卷实物登记表单数据
        public JsonResult NodeConfigGetList(string SpaceID)
        {
            //var list = this.entities.Set<S_DOC_Node>().Where(d => d.SpaceID == SpaceID && d.IsPhysicalBox == "true").ToList();
            string nodeSql = @"select  S_DOC_Node.ID NodeID,Name,SpaceID,FormRela.ID,FormCode,FormRela.ReorganizeConfigID
            from S_DOC_Node with(nolock) left join  S_DOC_ReorganizeConfig_NodeFormRelation FormRela
            on FormRela.NodeID=S_DOC_Node.ID where SpaceID='" + SpaceID + "' and IsPhysicalBox='true' order by SortIndex asc";
            SQLHelper sqlHelp = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable dataList = sqlHelp.ExecuteDataTable(nodeSql);
            return Json(dataList);
        }

        #region 整编区定义表单跳转
        public JsonResult GetCategoryID(string Code, string type)
        {
            JsonResult list = null;
            if (type == "form")
                list = FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == Code) != null ? Json(FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == Code)) : Json(null);
            else if (type == "list")
                list = FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_List>().FirstOrDefault(a => a.Code == Code) != null ? Json(FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_List>().FirstOrDefault(a => a.Code == Code)) : Json(null);
            if (list.Data == null && type == "form")
                throw new Exception("【" + Code + "】表单不存在");
            else if (list.Data == null && type == "list")
                throw new Exception("【" + Code + "】列表不存在");
            return list;
        }
        #endregion
        //保存前进行查询
        public void SaveCheck(string formCode, string listCode)
        {
            string[] codes = formCode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            JsonResult form = null;
            JsonResult list = null;
            //表单定义判断
            if (!string.IsNullOrEmpty(formCode))
            {
                form = FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == formCode) != null ? Json(FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == formCode)) : Json(null);
                if (form.Data == null)
                    throw new Exception("【" + formCode + "】表单不存在");
            }
            //列表定义判断
            if (!string.IsNullOrEmpty(listCode))
            {
                list = FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_List>().FirstOrDefault(a => a.Code == listCode) != null ? Json(FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_List>().FirstOrDefault(a => a.Code == listCode)) : Json(null);
                if (list.Data == null)
                    throw new Exception("【" + listCode + "】列表不存在");
            }
            //按件对应表单判断
            string formRelationStr = string.Empty;
            var fileFormRelation = this.GetQueryString("dataGridPhysicalMainForm");
            var formRelationList = JsonHelper.ToObject<List<S_DOC_ReorganizeConfig_FileFormRelation>>(fileFormRelation);
            //按卷对应表单判断
            var fileFormVolumeRelation = this.GetQueryString("dataGridPhysicalVolumeForm");
            var formVolumeRelationList = JsonHelper.ToObject<List<S_DOC_ReorganizeConfig_NodeFormRelation>>(fileFormVolumeRelation);
            if (!string.IsNullOrEmpty(fileFormRelation))
            {
                foreach (var item in formRelationList)
                {
                    if (!string.IsNullOrEmpty(item.FormCode))
                    {
                        form = FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == item.FormCode) != null ? Json(FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == item.FormCode)) : Json(null);
                        if (form.Data == null)
                            formRelationStr += "【" + item.FormCode + "】" + ",";
                    }
                }
                if (formRelationStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
                {
                    throw new Exception("【" + formRelationStr.TrimEnd(',') + "表单不存在" + "】列表不存在"); 
                }
            }
            else if (!string.IsNullOrEmpty(fileFormVolumeRelation))
            {
                foreach (var item in formVolumeRelationList)
                {
                    if (!string.IsNullOrEmpty(item.FormCode))
                    {
                        form = FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == item.FormCode) != null ? Json(FormulaHelper.GetEntities<BaseEntities>().Set<S_UI_Form>().FirstOrDefault(a => a.Code == item.FormCode)) : Json(null);
                        if (form.Data == null)
                            formRelationStr += "【" + item.FormCode + "】" + ",";
                    }
                }
                if (formRelationStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
                {
                    throw new Exception("【" + formRelationStr.TrimEnd(',') + "表单不存在" + "】列表不存在");
                }
            }
        }
    }
}
