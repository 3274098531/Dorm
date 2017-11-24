using System;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Helper;
using MyFramework.Web;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Admin.Controllers
{
    public class DormController : AdminBaseContorller
    {
        public DormController(IAdminService adminService) : base(adminService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            PageList<Dorm> list = AdminService.GetAllDorm(index, pagesize);
            return View(list);
        }


        public ActionResult Create()
        {
            ViewBag.Type = typeof(Sex).ToSelectListItems();
            return View();
        }

        //
        // POST: /Admin/Dorm/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            AdminService
                .CreateDorm()
                .Name(collection[Keys.Name])
                .Type(collection[Keys.Type]);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            Dorm dorm = AdminService.GetDormById(id);
            ViewBag.Type = typeof(Sex).ToSelectListItems()
                .Selected(dorm.Type.Value());
            return View(dorm);
        }

        //
        // POST: /Admin/Dorm/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            AdminService.EditDorm(id)
                        .Name(collection[Keys.Name])
                        .Type(collection[Keys.Type]) ;

            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/Dorm/Delete/5

        public ActionResult Delete(string id)
        {
            AdminService.DeleteDorm(id);


            return RedirectToAction("Index");
        }
    }
}