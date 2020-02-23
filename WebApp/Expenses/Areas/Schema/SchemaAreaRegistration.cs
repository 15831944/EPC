using System.Web.Mvc;

namespace Expenses.Areas.Schema
{
    public class SchemaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Schema";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Schema_default",
                "Schema/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
