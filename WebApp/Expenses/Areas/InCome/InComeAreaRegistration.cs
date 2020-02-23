using System.Web.Mvc;

namespace Expenses.Areas.InCome
{
    public class InComeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "InCome";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "InCome_default",
                "InCome/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
