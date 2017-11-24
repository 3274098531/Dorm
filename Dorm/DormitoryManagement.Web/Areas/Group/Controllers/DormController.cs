using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using EntityFramework.Extensions;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class DormController : GroupBaseController
    {
        public DormController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(GroupService.GetAllDorm(index, pagesize));
        }
         
          
    }
}
