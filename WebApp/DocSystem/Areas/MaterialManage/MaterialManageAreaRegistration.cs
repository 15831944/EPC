using System.Web.Mvc;

namespace DocSystem.Areas.MaterialManage
{
    public class MaterialManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MaterialManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MaterialManage_default",
                "MaterialManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
