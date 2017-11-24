using System;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;

namespace DormitoryManagement.Web.Areas.Minister.Controllers
{
    public class HomeController : MinisterBaseController
    {
        //
        // GET: /Minister/Home/

        public HomeController(IMinisterService ministerService) : base(ministerService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] // 只能用GET ！！！
        public ActionResult CheckCodeIsExists(string Code, string Name)
        {
            Student student = MinisterService.GetStudentByCode(Code);
            if (student == null) return Json(true, JsonRequestBehavior.AllowGet);
            if (Name == null) return Json(true, JsonRequestBehavior.AllowGet);
            return student.Name == Name
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckCodeIsExistsInMenber(string Code, string Id)
        {
            Student student = MinisterService.GetStudentByCode(Code);
            if (student == null) return Json("学号不存在", JsonRequestBehavior.AllowGet);
            Domain.Member menber = MinisterService.GetMenberByCode(Code);
            if (menber == null) return Json(true, JsonRequestBehavior.AllowGet);
            if (!string.IsNullOrEmpty(Id)&&new Guid(Id) == menber.Id) return Json(true, JsonRequestBehavior.AllowGet);

            return Json("部员已经存在", JsonRequestBehavior.AllowGet);
        }
    }
}