using System.Web.Mvc;

namespace Expenses.Areas.Build
{
    public class BuildAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Build";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Build_default",
                "Build/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
