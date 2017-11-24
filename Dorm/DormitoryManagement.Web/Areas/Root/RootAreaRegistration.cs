using System.Web.Mvc;

namespace DormitoryManagement.Web.Areas.Root
{
    public class RootAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Root";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Root_default",
                "Root/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 new[] { "DormitoryManagement.Web.Areas.Root.Controllers" }
            );
        }
    }
}
