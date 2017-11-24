using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Helper.StringDecrypt;
using MyFramework.Web;
using MyFramework.Web.HtmlTags;

namespace DormitoryManagement.Web.Controllers
{
    [Transaction]
    public class AccountController : Controller
    {
        private readonly IAdminService _adminService;


        public AccountController(IAdminService adminService,
            IFrameworkService frameworkService, IHttpService httpService)
        {
            HttpService = httpService;
            FrameworkService = frameworkService;
            _adminService = adminService;
        }

        public IFrameworkService FrameworkService { get; set; }

        public IHttpService HttpService { get; set; }

        public ActionResult Register()
        {
            ViewData[FrameworkKeys.Version]
                   = FrameworkService.GetVersion().Num.ToString("0.00");
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {ViewData[FrameworkKeys.Version]
                = FrameworkService.GetVersion().Num.ToString("0.00");
            string username = form[FrameworkKeys.UserName];
            string password = form[FrameworkKeys.Password];
            string confrim = form[FrameworkKeys.ConfirmPassWord];
            string email = form[FrameworkKeys.Email];
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password)
                || string.IsNullOrWhiteSpace(confrim)
                || string.IsNullOrEmpty(email))
            {
                ViewData[FrameworkKeys.Notification] = "用户名、密码、确认密码、邮箱必须填写";
                return View();
            }
            if (password != confrim)
            {
                ViewData[FrameworkKeys.Notification] = "确认密码和密码不一致";
                return View();
            }
            if (FrameworkService.GetAccountByUserName(username) != null)
            {
                ViewData[FrameworkKeys.Notification] = "用户名已经存在，请登录！";
                return View();
            }
            var emailtest = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!emailtest.IsMatch(email))
            {
                ViewData[FrameworkKeys.Notification] = "邮箱格式不正确";
                return View();
            }
            FrameworkService.CreateAccount(username)
                .PassWord(password)
                .AddRole(FrameworkService.GetRoleByName(Keys.DefaultRole).Id.ToString())
                .IsEable()
                .Email(form[FrameworkKeys.Email]);
            ViewData[Keys.Notification] = "注册成功";
            return View();
        }


        public ActionResult LogOn()
        {
            ViewData[FrameworkKeys.Version]
                = FrameworkService.GetVersion().Num.ToString("0.00");
            return View();
        }

        

        [HttpPost]
        public ActionResult LogOn(FormCollection collection, string returnUrl)
        {
            string username = collection[FrameworkKeys.UserName];
            string password = collection[FrameworkKeys.Password].ToEncode();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData[Keys.Notification] = "用户名或密码必须填写";
                return View();
            }  
            Account account = FrameworkService.LogOnValid(username, password);
            if (account!=null)
            {
                if (account.IsEnable == false)
                {
                    ViewData[FrameworkKeys.Notification] = "您的账户已经被禁用，请联系管理员解锁";
                    return View();
                }Response.Cookies.Add(HttpService.LogOn(account)); 
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                
                return RedirectToAction("Index", "Home", new {Area = account.Roles.First().Name});
            }
            ViewData[Keys.Notification] = "用户名或密码错误";
            return View();
        }
         
        public ActionResult LogOff()
        {
            HttpService.LogOut();
            return RedirectToAction("LogOn", "Account", new { area = "" });
        }
        public ActionResult Info(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                ViewData[Keys.Message] = message; 
            return View(FrameworkService.GetCurrentAccount());
        }
        [HttpPost]
        public ActionResult Info(FormCollection form)
        {
            FrameworkService.EditAccount()
                .RealName(form[FrameworkKeys.RealName])
                .PassWord(form[FrameworkKeys.Password])
                .Email(form[FrameworkKeys.Email]);
            return Info("个人信息修改成功");
        }
        public ActionResult FindPassWord()
        {
            ViewData[FrameworkKeys.Version]
                   = FrameworkService.GetVersion().Num.ToString("0.00");
            return View();
        }

        [HttpPost]
        public ActionResult FindPassWord(FormCollection form)
        {
            if (string.IsNullOrWhiteSpace(form[FrameworkKeys.UserName]) || string.IsNullOrWhiteSpace(form[FrameworkKeys.Email]))
            {
                ViewData[FrameworkKeys.Notification] = "用户名或邮箱必须填写";
                return View();
            }
            var emailtest = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!emailtest.IsMatch(form[FrameworkKeys.Email]))
            {
                ViewData[FrameworkKeys.Notification] = "邮箱格式不正确";
                return View();
            }
            Account account = FrameworkService.GetAccountByUserName(form[FrameworkKeys.UserName]);
            if (account != null && account.Email == form[FrameworkKeys.Email])
            {
                var email = new EmailHelper
                {
                    ToEmail = new[] { form[FrameworkKeys.Email ] },
                    Subject = "密码找回",
                    Body = "您的密码为   " + FrameworkService.GetAccountByUserName(form[FrameworkKeys.UserName ]).PassWord.ToDecode()
                };
                email._Send();
                ViewData[FrameworkKeys.Notification] = "您的密码已经发送到您的邮箱，请注意查收！";
                return View();
            }
            ViewData[FrameworkKeys.Notification] = "用户名或邮箱错误";
            return View();
        } 
        public ActionResult GetPhoto(string username)
        {
            Account account = FrameworkService.GetAccountByUserName(username);
            byte[] buffer = account.Photo;
            if (buffer != null)
            {
                var stream = new MemoryStream(buffer);
                Image image = Image.FromStream(stream);
                var result = new ImageResult(image, ImageFormat.Jpeg);
                return result;
            }
            return File(Server.MapPath("~/Content/images/initimg.jpg"), "image/jpg");
        }
        [HttpPost]
        public ActionResult EditPhoto(HttpPostedFileBase file)
        { 
            if (file != null && file.ContentLength > 0)
            {
                Stream stream = file.InputStream;
                var content = new byte[stream.Length];
                stream.Read(content, 0, content.Length); 
                stream.Seek(0, SeekOrigin.Begin);
                FrameworkService.EditAccount().Photo(content) ;
            } 
            return RedirectToAction("Info", new {message = "头像修改成功"});
        }

      
        [HttpGet]
        public ActionResult FindClassesByAcademy(string academyName)
        {
            IList<Class> Class = _adminService.GetAllClass(new Class.ByAcademyName(academyName));
            return Json(_adminService.ClassToJson(Class), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FindRoomsByDorm(string dormName)
        {
            IList<Room> room = _adminService.GetAllRoom(new Room.ByDormName(dormName));
            return Json(_adminService.RoomToJson(room), JsonRequestBehavior.AllowGet);
        }

         

        [HttpGet]
        public ActionResult CheckPassWord(string currentPassWord)
        {
            bool flag = FrameworkService.GetCurrentAccount().PassWord == currentPassWord.ToEncode();
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStudentNameByCode(string code)
        {
            Student student = _adminService.GetStudentByCode(code);
            return Json(student == null
                ? "学号不存在"
                : student.Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckUserInApplyUser(string UserName)
        {
            Student student = _adminService.GetStudentByCode(UserName);
            Account user = FrameworkService.GetAccountByUserName(UserName);
            if (student == null)
                return Json("学号不存在", JsonRequestBehavior.AllowGet);
            return user == null
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json("用户已存在,请登录", JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckUserNameIsExistAndCheckEmail(string UserName, string Email)
        {
            Student student = _adminService.GetStudentByCode(UserName);
            Account user = FrameworkService.GetAccountByUserName(UserName);
            if (student == null)
                return Json("学号不存在", JsonRequestBehavior.AllowGet);
            if (user == null) return Json("账号不存在,请注册", JsonRequestBehavior.AllowGet);

            return user.Email != Email
                ? Json("邮箱地址错误,请重新输入", JsonRequestBehavior.AllowGet)
                : Json(true, JsonRequestBehavior.AllowGet);
        }
        
    }
}