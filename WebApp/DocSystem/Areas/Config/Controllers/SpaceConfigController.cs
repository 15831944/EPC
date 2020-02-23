using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Text;
using Formula;
using Formula.Helper;
using MvcAdapter;
using DocSystem.Logic;
using DocSystem.Logic.Domain;
using Config;

namespace DocSystem.Areas.Config.Controllers
{
    public class SpaceConfigController : DocConfigController<S_DOC_Space> 
    {
        protected override void BeforeDelete(List<S_DOC_Space> entityList)
        {
            foreach (var item in entityList)
                item.Delete();
        }

        protected override void BeforeSave(S_DOC_Space entity, bool isNew)
        {
            entity.Save();
        }
        public void publish()
        {
            string ID = this.Request["ID"];
            if (!string.IsNullOrEmpty(ID))
                DocConfigHelper.PublishSpaceBySpaceID(ID);
        }

        public JsonResult DoCopy(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                throw new Formula.Exceptions.BusinessException("请选择要复制的模版");

            var space = DocConfigHelper.CreateConfigSpaceByID(ID);
            space.DoCopy();
            entities.SaveChanges();

            return Json("");
        }
    }
}
