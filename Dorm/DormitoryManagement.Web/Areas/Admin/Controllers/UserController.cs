using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseContorller
    {
        //
        // GET: /Admin/Account/ 
        public UserController(IAdminService adminService, IFrameworkService frameworkService) : base(adminService)
        {
            FrameworkService = frameworkService;
        }

        public IFrameworkService FrameworkService { get; set; }

        public ActionResult Index(int? index, int? pagesize, string username)
        {
        
            if (username == null) return View(FrameworkService.GetAllAccount(index, pagesize ));
            return View(FrameworkService.GetAllAccount(index, pagesize, new Account.ByUserName(username)));
        }

        public ActionResult Create()
        {
            ViewData[Keys.Role] = FrameworkService
                .GetAllRole().ToSelectListItem(x => x.Id, x => x.Discription);
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            FrameworkService.CreateAccount(form[FrameworkKeys.UserName]) 
                .RealName(form[FrameworkKeys.RealName])
                .PassWord(IoC.Get<IConfig>().GetValue(Keys.DefaultPassWord))
                .AddRole(form[Keys.Role])
                .Email(form[Keys.Email])
                .IsEable(); 
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            FrameworkService.DeleteAccount(id); 
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Account account = FrameworkService.GetAccountById(id); 
            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            FrameworkService.EditAccountById(id) 
                .RealName(form[FrameworkKeys.RealName])
                .Email(form[Keys.Email]); 
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddRole(string accountid, string roleid)
        {
            FrameworkService.EditAccountById(accountid)
                .RemoveAllRole()
                .AddRole(roleid);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveRole(string accountid, string roleid)
        {
            FrameworkService.EditAccountById(accountid)
                .RemoveRole(roleid);
            return RedirectToAction("Index");
        }

        public ActionResult EditAble(string id)
        {
            FrameworkService.EditAccountById(id)
                .IsEable();
            return RedirectToAction("Index");
        }

        public ActionResult InitPassword(string id)
        {
            string password =
               IoC.Get<IConfig>().GetValue(Keys.DefaultPassWord);

            FrameworkService.EditAccountById(id)
                .PassWord(password);
            return RedirectToAction("Index");
        }
    }
}