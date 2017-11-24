using System.Web.Mvc;

namespace DormitoryManagement.Web.Areas.Group
{
    public class GroupAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Group";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Group_default",
                "Group/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                 , new[] { "DormitoryManagement.Web.Areas.Group.Controllers" }
            );
        }
    }
}
