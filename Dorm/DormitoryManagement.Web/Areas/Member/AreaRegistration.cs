using System.Web.Mvc;

namespace DormitoryManagement.Web.Areas.Member
{
    public class MenberAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Member";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Menber_default",
                "Member/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                 , new[] { "DormitoryManagement.Web.Areas.Member.Controllers" }
            );
        }
    }
}
