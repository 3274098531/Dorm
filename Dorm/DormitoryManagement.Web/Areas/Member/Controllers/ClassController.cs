using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Helper;

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class ClassController : MemberBaseContorller
    {
        public ClassController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(MemberService.GetAllClass(index, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string AcademyName, string AcademyId, string Name)
        {
            Search<Class> serch = new Search<Class>()
                .Add(Name, new ByName<Class>(Name))
                .Add(AcademyName, new Class.ByAcademyName(AcademyName))
                .Add(AcademyId, new Class.ByAcademy(AcademyId));
            return View(MemberService.GetAllClass(1, null, serch.Result()));
        }
    }
}