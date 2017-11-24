using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Member.Controllers
{
    public class StudentController : MemberBaseContorller
    {
        //
        // GET: /Admin/Student/


        public StudentController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index(int? index, int? pagesize )
        {
            return  
                View( MemberService.GetAllStudent(index, pagesize));
        }
        [HttpPost]
        public ActionResult Index(string AcademyName, string ClassName, string ClassId, string Code, string Name, string DormName,
            string RoomId,string NameOrCode)
        {
            Search<Student> serch = new Search<Student>()
                .Add(Name, new ByName<Student>(Name))
                .Add(NameOrCode, new ByNameOrCode<Student>(NameOrCode)) 
                .Add(Code, new ByCode<Student>(Code))
                .Add(AcademyName, new Student.ByAcademyName(AcademyName))
                .Add(ClassName, new Student.ByClassName(ClassName))
                .Add(ClassId, new Student.ByClass(ClassId))
                .Add(DormName, new Student.ByDormName(DormName))
                .Add(RoomId, new Student.ByRoom(RoomId));
            return View(MemberService.GetAllStudent(1, null, serch.Result()));
        }

         

        public ActionResult Edit(string id)
        {
            Student student = MemberService.GetStudentById(id);
             
            ViewBag.Dorm = MemberService.GetAllDorm().ToSelectListItem(x => x.Id, x => x.Name)
                .Selected(student.Room.DormId.ToString());
            ViewBag.Room = MemberService.GetAllRoom(new ById<Room>(student.RoomId.ToString()))
                .ToSelectListItem(x => x.Id, x => x.Name)
                .Selected(student.RoomId.ToString());
            return View(student);
        }

         

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            MemberService.EditStudent(id) 
                .Discipline(collection[Keys.Discipline]).
                Room(collection[Keys.Room]);
            return RedirectToAction("Index");
        }
    }
}