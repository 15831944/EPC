using System.Web.Mvc;

namespace DocSystem.Areas.DocSystemUI
{
    public class DocSystemUIAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DocSystemUI";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DocSystemUI_default",
                "DocSystemUI/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
