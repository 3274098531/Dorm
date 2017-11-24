using System;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Helper;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class CheckDormController : GroupBaseController
    {
        public CheckDormController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index(int? index, int? pagesize, string name)
        {
            if (!string.IsNullOrEmpty(name))
                return View(GroupService.GetAllInspection(index, pagesize, new ByName<Inspection>(name)));
            return View(GroupService.GetAllInspection(index, pagesize));
        }


         
        [HttpPost]
        public ActionResult CheckIndex(string AcademyName,string InspectionId,   string DormName, string RoomId)
        {
            ViewData[Keys.InspectionId] = InspectionId;
            ViewData[Keys.Grade] = typeof(Grade).ToSelectListItems();
            Search<Check> serch = new Search<Check>()
                 
                .Add(AcademyName, new Check.ByAcademyName(AcademyName))
                .Add(InspectionId, new Check.ByInspection(InspectionId))
                .Add(DormName, new Check.ByDormName(DormName))
                .Add(RoomId, new Check.ByRoom(RoomId));
            return View(GroupService.GetAllCheck(AcademyName!=null, serch.Result()));
        }
         
        public ActionResult Details(string id)
        {
            return View(GroupService.GetRoomById(id));
        }

        public ActionResult EditResutl(string id, string grade)
        {
            GroupService.EditCheckResultByCheckId(id,grade);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        
    }
}