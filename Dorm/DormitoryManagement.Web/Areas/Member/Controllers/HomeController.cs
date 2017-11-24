using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class HomeController : MemberBaseContorller
    {
        //
        // GET: /Minister/Home/


        public HomeController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckCodeIsExists(string Code, string Name)
        {
            var student = MemberService.GetStudentByCode(Code);
            if (student == null) return Json(true, JsonRequestBehavior.AllowGet);
            if (Name == null) return Json(true, JsonRequestBehavior.AllowGet);
            return student.Name == Name
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(false, JsonRequestBehavior.AllowGet);
        }

        
    }
}