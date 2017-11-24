using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Web.Default;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class HomeController : GroupBaseController
    {
        //
        // GET: /Member/Home/


        public HomeController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
