using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class InformationController : MemberBaseContorller
    {
        //
        // GET: /Member/Information/


        public InformationController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index()
        {
            Domain.Member menber = MemberService.GetMenber();
            return View(menber);
        }

        public ActionResult Edit()
        {
            return View(MemberService.GetMenber());
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            MemberService.EditMenber()
                .Phone(form[Keys.Phone])
                .Email(form[Keys.Email]);
            return RedirectToAction("Index");
        }
    }
}