using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using EntityFramework.Extensions;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class ClassController:GroupBaseController
    {
        public ClassController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        { 
            return View(GroupService.GetAllClass(index, pagesize ));
        }
        [HttpPost]
        public ActionResult Index(string AcademyName,  string AcademyId, string Name)
        {
            Search<Class> serch = new Search<Class>()
                .Add(Name, new ByName<Class>(Name))
                .Add(AcademyName, new Class.ByAcademyName(AcademyName))
                .Add(AcademyId, new Class.ByAcademy(AcademyId));
            return View(GroupService.GetAllClass(1, null, serch.Result()));
        }   
           
    }
}