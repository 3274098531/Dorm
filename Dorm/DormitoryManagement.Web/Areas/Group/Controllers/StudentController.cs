using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace DormitoryManagement.Web.Areas.Group.Controllers
{
    public class StudentController : GroupBaseController
    {
        //
        // GET: /Admin/Student/


        public StudentController(IGroupService groupService) : base(groupService)
        {
        }

        public ActionResult Index(int? index, int? pagesize )
        {
            return  
                View(  GroupService.GetAllStudent(index, pagesize));
        }
        [HttpPost]
        public ActionResult Index(string AcademyName, string ClassName, string ClassId, string Code, string Name, string DormName,
            string NameOrCode,string RoomId)
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
            return View(GroupService.GetAllStudent(1, null, serch.Result()));
        }
        public ActionResult Details(string id)
        {
            return View(GroupService.GetStudentById(id));
        }
    }
}