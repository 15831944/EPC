using System.Web.Mvc;

namespace Expenses.Areas.Cost
{
    public class CostAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Cost";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Cost_default",
                "Cost/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
