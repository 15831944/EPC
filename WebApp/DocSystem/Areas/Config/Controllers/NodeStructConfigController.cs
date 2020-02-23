using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAdapter;
using DocSystem.Logic.Domain;
using Formula.DynConditionObject;
using Formula;
using Formula.Helper;
using Config.Logic;
using Config;
using System.Data;

namespace DocSystem.Areas.Config.Controllers
{
    public class NodeStructConfigController : DocConfigController<S_DOC_NodeStrcut>
    {
        //
        // GET: /Config/NodeStructConfig/

        public override JsonResult GetTree()
        {
            string SpaceID = this.Request["SpaceID"];
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable dt = sqlHelper.ExecuteDataTable(string.Format(@"select S_DOC_NodeStrcut.ID,ParentID,S_DOC_NodeStrcut.Name,FullPathID,NodeID,IsFreeNode from S_DOC_NodeStrcut
left join S_DOC_Node on S_DOC_NodeStrcut.NodeID = S_DOC_Node.ID  where S_DOC_NodeStrcut.SpaceID = '{0}'", SpaceID));
            return Json(dt, JsonRequestBehavior.AllowGet);
        }
        public override JsonResult Save()
        {
            S_DOC_NodeStrcut mod = this.GetEntityByID<S_DOC_NodeStrcut>(this.Request["ParentID"]);

            S_DOC_NodeStrcut Child = new S_DOC_NodeStrcut();
            Child.Name = this.Request["NodeTypeName"];
            Child.SpaceID = this.Request["SpaceID"];
            Child.ParentID = this.Request["ParentID"];
            Child.NodeID = this.Request["NodeTypeCode"];
            mod.AddChild(Child);
            entities.SaveChanges();
            return Json("");
        }

        public override JsonResult Delete()
        {
            string ID = Request["ListIDs"];

            S_DOC_NodeStrcut entity = this.GetEntityByID<S_DOC_NodeStrcut>(ID);
            entity.Delete();

            entities.Set<S_DOC_NodeStrcut>().Remove(entity);
            entities.SaveChanges();

            return Json("");
        }
    }
}
