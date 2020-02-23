using System.Web.Mvc;

namespace DocSystem.Areas.Reorganize
{
    public class ReorganizeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Reorganize";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Reorganize_default",
                "Reorganize/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
