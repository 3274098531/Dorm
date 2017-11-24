using System;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Helper;
using MyFramework.Web;

namespace DormitoryManagement.Web.Areas.Minister.Controllers
{
    public class RoomController : MinisterBaseController
    {
        public RoomController(IMinisterService ministerService) : base(ministerService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(MinisterService.GetAllRoom(index, pagesize));
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
            return View(MinisterService.GetAllRoom(1, null, serch.Result()));
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Academy = new SelectList(MinisterService.GetAllAcademy(), "Id", "Name");
            ViewBag.Dorm = new SelectList(MinisterService.GetAllDorm(), "Id", "Name");
            return View(MinisterService.GetRoomById(id));
        }

        public ActionResult EditAll()
        {
            ViewData[Keys.Dorm] = MinisterService.GetAllDorm()
                .ToSelectListItem(x => x.Id, x => x.Name);
            return View();
        }

        [HttpPost]
        public ActionResult EditAll(FormCollection form)
        {
            MinisterService.EditAllRoom(form[Keys.Dorm], form[Keys.Room], form[Keys.Remark],
                DateTime.Parse(form[Keys.StartTime]), DateTime.Parse(form[Keys.EndTime]));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            bool checkbox = collection  [Keys.AbleCheck].Contains("true");
            MinisterService.EditRoom(id)
                .Name(collection[Keys.Name])
                .StartTime(checkbox != true ? collection[Keys.StartTime] : new DateTime(1753, 1, 1, 12, 0, 0).ToString())
                .EndTime(checkbox != true ? collection[Keys.EndTime] : new DateTime(1753, 1, 1, 12, 0, 0).ToString())
                .MaxBedNum(collection[Keys.MaxBedNum])
                .Discipline(collection[Keys.Remark]);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            return View(MinisterService.GetRoomById(id));
        }

        public ActionResult CheckResults(string id)
        {
            return View(MinisterService.GetAllCheckResultByRoomId(id));
        }
    }
}