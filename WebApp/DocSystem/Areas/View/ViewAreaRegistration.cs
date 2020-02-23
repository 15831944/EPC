using System.Web.Mvc;

namespace DocSystem.Areas.View
{
    public class ViewAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "View";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "View_default",
                "View/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
