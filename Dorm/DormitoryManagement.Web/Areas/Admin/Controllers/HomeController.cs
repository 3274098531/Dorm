using System;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Application;
using MyFramework.Application.Interface;
using MyFramework.Domain;

namespace DormitoryManagement.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseContorller
    {
        //
        // GET: /Default/Home/


        public HomeController(IAdminService adminService, IFrameworkService frameworkService) : base(adminService)
        {
            FrameworkService = frameworkService;
        }

        public IFrameworkService FrameworkService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] // 只能用GET ！！！
        public ActionResult CheckCodeIsExists(string Code, string id)
        {
            return !AdminService.StudentIsExistByCode(Code) 
                ? Json(true, JsonRequestBehavior.AllowGet) 
                : Json(!string.IsNullOrWhiteSpace(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet] // 只能用GET ！！！
        public ActionResult CheckCodeIsExistsToUser(string UserName, string UserId)
        {
            Student student = AdminService.GetStudentByCode(UserName);
            if (student == null) return Json("学号不存在", JsonRequestBehavior.AllowGet);
            Account account = FrameworkService.GetAccountByUserName(UserName);
            if (account == null) return Json(true, JsonRequestBehavior.AllowGet);
            if (!string.IsNullOrEmpty(UserId) && account.Id == new Guid(UserId))
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json("该学号已经绑定用户,请登录", JsonRequestBehavior.AllowGet);
        }
    }
}