using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DormitoryManagement.Web.Default;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Web.Areas.Root.Controllers
{
    public class HomeController : RootBaseController
    {
         
        public HomeController(IFrameworkService groupService) : base(groupService)
        {
        } 
        public ActionResult Index()
        {
            return View();
        }

    }
}
