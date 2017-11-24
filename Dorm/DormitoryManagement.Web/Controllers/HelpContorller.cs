using System.Web.Mvc;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Helper;

namespace DormitoryManagement.Web.Controllers
{
    public class HelpController:Controller
    {
        private IRepository _repository;

        public HelpController(IRepository repository)
        {
            this._repository = repository;
        }

        
        [HttpGet]
        public ActionResult GetStudentNameByCode(string code)
        {
            if (!_repository.IsExisted(new ByCode<Student>(code)))
            {
                return Json("学生不存在", JsonRequestBehavior.AllowGet);
            }
            return Json(_repository.GetOne(new ByCode<Student>(code)).Name, JsonRequestBehavior.AllowGet);
        }
    }
}