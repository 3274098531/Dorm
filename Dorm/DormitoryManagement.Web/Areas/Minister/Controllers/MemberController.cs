using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Application.Interface;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web;

namespace DormitoryManagement.Web.Areas.Minister.Controllers
{
    public class MemberController : MinisterBaseController
    {
        //
        // GET: /Minister/Member/

        public MemberController(IMinisterService ministerService, IFrameworkService frameworkService)
            : base(ministerService)
        {
            FrameworkService = frameworkService;
        }

        public IFrameworkService FrameworkService { get; set; }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(MinisterService.GetAllMenber(index, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string NameOrCode)
        {
            return
                View(MinisterService.GetAllMenber(1, null,
                    new ByNameOrCode<Domain.Member>(NameOrCode)));
        }

        //
        // GET: /Minister/Member/Create

        public ActionResult Create()
        {
            ViewData[Keys.Position] = typeof (Position).ToSelectListItems();
             
            return View();
        }

        //
        // POST: /Minister/Member/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (!MinisterService.StudentExistByCode(collection[Keys.Code]))
            {
                ViewData[Keys.Message] = "学生不存在";
                ViewData[Keys.Position] = typeof(Position).ToSelectListItems();
                return View();
            }
            if (MinisterService.MemberIsExitByCode(collection[Keys.Code]))
            {
                ViewData[Keys.Position] = typeof(Position).ToSelectListItems();
                ViewData[Keys.Message] = "部员已经存在";
                return View();
            }

            MinisterService.CreateMenber(collection[Keys.Code])
                .Phone(collection[Keys.Phone]) 
                .Email(collection[Keys.Email])
                .Position(collection[Keys.Position]);

            if (!FrameworkService.AccountIsExit(collection[Keys.Code]))
            {
                FrameworkService.CreateAccount(collection[Keys.Code])
                    .PassWord(ConfigurationManager.AppSettings[Keys.DefaultPassWord])
                    .AddRole(FrameworkService.GetRoleByName(Roles.Member.Value()).Id.ToString())
                    .IsEable()
                    .Email(collection[Keys.Email]);
            }
            else
            {
                FrameworkService.EditAccountByUserName(collection[Keys.Code])
                    .AddRole(FrameworkService.GetRoleByName(Roles.Member.Value()).Id.ToString());
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Minister/Member/Edit/5

        public ActionResult Edit(string id)
        {
            var member = MinisterService.GetMenberById(id);
            ViewData[Keys.Position] = typeof (Position)
                .ToSelectListItems()
                .Selected(member.Position.Value());
             
            return View(MinisterService.GetMenberById(id));
        }

        //
        // POST: /Minister/Member/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            MinisterService.EditMenber(id)
                .Phone(collection[Keys.Phone])
                .Email(collection[Keys.Email])
                .Position(collection[Keys.Position]);
            return RedirectToAction("Index");
        }

        //
        // GET: /Minister/Member/Delete/5

        public ActionResult Delete(string id)
        {
            MinisterService.DeleteMenber(id);
            return RedirectToAction("Index");
        }
    }
}