using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using LinqSpecs;
using MyFramework.Helper;
using MyFramework.Web;

namespace DormitoryManagement.Web.Areas.Admin.Controllers
{
    public class ClassController : AdminBaseContorller
    {
        public ClassController(IAdminService adminService) : base(adminService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(AdminService.GetAllClass(index, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string AcademyName, string AcademyId, string Name)
        {
            Search<Class> serch = new Search<Class>()
                .Add(Name, new ByName<Class>(Name))
                .Add(AcademyName, new Class.ByAcademyName(AcademyName))
                .Add(AcademyId, new Class.ByAcademy(AcademyId));
            Specification<Class>[] specifications = serch.Result();
            return View(AdminService.GetAllClass(1, null,specifications));
        }

        public ActionResult Create()
        {
            ViewData[Keys.Academy] =
                AdminService.GetAllAcademy().ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            AdminService.CreateClass().Name(form[Keys.Name])
                .Academy(form[Keys.Academy]);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            Class _class = AdminService.GetClassById(id);
            ViewData[Keys.Academy] =
                AdminService.GetAllAcademy().ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                    .Selected(_class.AcademyId.ToString());
            return View(_class);
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection form)
        {
            AdminService.EditClass(id).Name(form[Keys.Name])
                .Academy(form[Keys.Academy]);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            AdminService.DeleteClass(id);
            return RedirectToAction("Index");
        }
    }
}