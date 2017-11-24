using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Admin.Controllers
{
    public class AcademyController : AdminBaseContorller
    {
        public AcademyController(IAdminService adminService) : base(adminService)
        {
        }

        public ActionResult Index(int? index = null, int? pagesize = null)
        { 
            return View(AdminService.GetAllAcademy(index,pagesize));
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
             
                AdminService.CreateAcademy().Name(form[Keys.Name])
                    .ShortName(form[Keys.ShortName]);

            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
             
                AdminService.DeleteAcademy(id);

                return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            return View(AdminService.GetAcademyById(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
             
                AdminService.EditAcademy(id).Name(form[Keys.Name])
                    .ShortName(form[Keys.ShortName]);

                return RedirectToAction("Index");
        }
    }
}