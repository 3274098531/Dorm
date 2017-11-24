using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class RoomController : MemberBaseContorller
    {
        public RoomController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index(int? index, int? pagesize,string name)
        {
            if(name==null)
            return View(MemberService.GetAllRoom(index, pagesize ));
            return View(MemberService.GetAllRoom(index, pagesize,new Room.ByDormName(name)));
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
            return View(MemberService.GetAllRoom(1, null, serch.Result()));
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Academy = new SelectList(MemberService.GetAllAcademy(), "Id", "Name");

            ViewBag.Dorm = new SelectList(MemberService.GetAllDorm(), "Id", "Name");
            return View(MemberService.GetRoomById(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            string checkbox = collection[Keys.AbleCheck].Contains("true").ToString();
            MemberService.EditRoom(id)
                .Name(collection[Keys.Name])
                .StartTime(checkbox != "true"
                    ? collection[Keys.StartTime]
                    : new DateTime(1753, 1, 1, 12, 0, 0).ToString())
                .EndTime(checkbox != "true" ? collection[Keys.EndTime] : new DateTime(1753, 1, 1, 12, 0, 0).ToString())
                .Dorm(collection[Keys.Dorm])
                .Academy(collection[Keys.Academy])
                .MaxBedNum(collection[Keys.MaxBedNum])
                .Discipline(collection[Keys.Remark]) ;
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            return View(MemberService.GetRoomById(id));
        }


        public ActionResult CheckResults(string id)
        {
            return View(MemberService.GetAllCheckResultByRoomId(id));
        }
    }
}