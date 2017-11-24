using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using EntityFramework.Extensions;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class RoomController :  GroupBaseController
    {
        public RoomController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index(int? index, int? pagesize )
        { 
            return View(GroupService.GetAllRoom(index, pagesize));
        }
        [HttpPost]
        public ActionResult Index(string AcademyName, string StudentCount, string AcademyId, string DormId, string Name)
        {
            Search<Room> serch = new Search<Room>()
                .Add(Name, new Room.ByDormName(Name))
                .Add(AcademyName, new Room.ByAcademyName(AcademyName))
                .Add(StudentCount, new Room.ByStudentCount(StudentCount))
                .Add(AcademyId, new Room.ByAcademy(AcademyId))
                .Add(DormId, new Room.ByDorm(DormId));
            return View(GroupService.GetAllRoom(1, null, serch.Result()));
        }
         
    }
}