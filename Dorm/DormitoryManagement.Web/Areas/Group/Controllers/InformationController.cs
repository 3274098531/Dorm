using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class InformationController : GroupBaseController
    {
        //
        // GET: /Member/Information/


        public InformationController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index()
        {
            Domain.Member menber = GroupService.GetMenber();
            return View(menber);
        }

        public ActionResult Edit()
        {
            return View(GroupService.GetMenber());
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            GroupService.EditMenber()
                .Phone(form[Keys.Phone])
                .Email(form[Keys.Email]);
            return RedirectToAction("Index");
        }
    }
}