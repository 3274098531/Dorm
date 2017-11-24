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

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class DormController : MemberBaseContorller
    {
        public DormController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(MemberService.GetAllDorm(index, pagesize));
        }
        
    }
}
