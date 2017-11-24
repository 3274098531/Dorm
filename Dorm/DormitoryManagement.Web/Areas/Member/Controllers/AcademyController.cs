using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using EntityFramework.Extensions;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class AcademyController : MemberBaseContorller
    {
        //
        // GET: /Admin/Academy/


        public AcademyController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index(int? index = null, int? pagesize = null)
        {
            return View(MemberService.GetAllAcademy(index, pagesize));
        }
        
         
         
    }
}
