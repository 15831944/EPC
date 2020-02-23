using System.Web.Mvc;

namespace DocSystem.Areas.ApplyInfo
{
    public class ApplyInfoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ApplyInfo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ApplyInfo_default",
                "ApplyInfo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
