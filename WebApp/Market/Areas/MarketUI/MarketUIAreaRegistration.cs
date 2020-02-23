using System.Web.Mvc;

namespace Market.Areas.MarketUI
{
    public class MarketUIAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MarketUI";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MarketUI_default",
                "MarketUI/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
