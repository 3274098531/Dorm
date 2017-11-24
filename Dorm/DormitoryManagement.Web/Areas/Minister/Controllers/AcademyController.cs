using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using EntityFramework.Extensions;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Minister.Controllers
{
    public class AcademyController : MinisterBaseController
    {
        //
        // GET: /Admin/Academy/


        public AcademyController(IMinisterService ministerService) : base(ministerService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        { 
            PageList<Academy> list = MinisterService.GetAllAcademy(index, pagesize);
            return View(list);
        }
        
         
         
    }
}
