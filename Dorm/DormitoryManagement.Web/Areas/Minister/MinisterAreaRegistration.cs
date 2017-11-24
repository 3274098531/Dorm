using System.Web.Mvc;

namespace DormitoryManagement.Web.Areas.Minister
{
    public class MinisterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Minister";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Minister_default",
                "Minister/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                , new[] { "DormitoryManagement.Web.Areas.Minister.Controllers" }
            );
        }
    }
}
