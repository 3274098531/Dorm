using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Helper;

namespace DormitoryManagement.Web.Areas.Minister.Controllers
{
    public class ClassController : MinisterBaseController
    {
        public ClassController(IMinisterService ministerService) : base(ministerService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(MinisterService.GetAllClass(index, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string AcademyName, string AcademyId, string Name)
        {
            Search<Class> serch = new Search<Class>()
                .Add(Name, new ByName<Class>(Name))
                .Add(AcademyName, new Class.ByAcademyName(AcademyName))
                .Add(AcademyId, new Class.ByAcademy(AcademyId));
            return View(MinisterService.GetAllClass(1, null, serch.Result()));
        }
    }
}