using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DormitoryManagement.Web.Default;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Web.Areas.Root.Controllers
{
    public class RequestLogController : RootBaseController
    {
        //
        // GET: /Root/Log/

        public RequestLogController(IFrameworkService groupService) : base(groupService)
        {
        }

        public ActionResult Index()
        {
            return View(GroupService.GetAllRequestLog());
        }

    }
}
