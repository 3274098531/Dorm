using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using MyFramework.Helper;
using MyFramework.Web;

namespace DormitoryManagement.Web.Areas.Admin.Controllers
{
    public class StudentController : AdminBaseContorller
    {
        //
        // GET: /Admin/Student/

        public StudentController(IAdminService adminService) : base(adminService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(AdminService.GetAllStudent(index, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string AcademyName, string ClassName, string ClassId, string Code, string Name,
            string DormName, string RoomId, string NameOrCode)
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
            return View(AdminService.GetAllStudent(1, null, serch.Result()));
        }

        //
        // GET: /Admin/Student/Create

        public ActionResult Create()
        {
            ViewBag.Sex =  typeof (Sex) 
                .ToSelectListItems();
            ViewData[Keys.Academy] =
                AdminService.GetAllAcademy().ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            ViewData[Keys.Class] = new List<Class>()
                .ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            ViewBag.Room = new List<Room>()
                .ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            ViewBag.Dorm = AdminService.GetAllDorm().ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            return View();
        }

        //
        // POST: /Admin/Student/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            AdminService.CreateStudent(collection[Keys.Code])
                .Class(collection[Keys.Class])
                .Name(collection[Keys.Name])
                .Sex(collection[Keys.Sex])
                .Discipline(collection[Keys.Discipline]).
                Room(collection[Keys.Room]);

            return RedirectToAction("Index");
        }


        public ActionResult Details(string id)
        {
            return View(AdminService.GetStudentById(id));
        }

        //
        // GET: /Admin/Student/Edit/5

        public ActionResult Edit(string id)
        {
            Student student = AdminService.GetStudentById(id);
            ViewBag.Sex = Enum.GetNames(typeof (Sex))
                .ToSelectListItem(x => x, x => x.To<Sex>().Text()).Selected(student.Sex.Value());
            ViewData[Keys.Academy] =
                AdminService.GetAllAcademy().ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                    .Selected(student.Class.Academy.Id.ToString());
            ViewData[Keys.Class] = AdminService.GetAllClass(new ById<Class>(student.Class.Id.ToString()))
                .ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                .Selected(student.ClassId.ToString());
            ViewBag.Room = AdminService.GetAllRoom(new ById<Room>(student.RoomId.ToString()))
                .ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                .Selected(student.RoomId.ToString());
            ViewBag.Dorm = AdminService.GetAllDorm().ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                .Selected(student.Room.DormId.ToString());
            return View(student);
        }

        //
        // POST: /Admin/Student/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            AdminService.EditStudent(id)
                .Class(collection[Keys.Class])
                .Name(collection[Keys.Name])
                .Sex(collection[Keys.Sex])
                .Discipline(collection[Keys.Discipline])
                .Room(collection[Keys.Room]);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(string code)
        {
            MemoryStream memoryStream;

            memoryStream = AdminService.GetMenmorstreamToStudent(code);
            AdminService.DeleteAllStudent(code);
            return File(memoryStream, "application/vnd.ms-excel", code + "级学生信息表.xls");
        }


        public ActionResult Delete(string id)
        {
            AdminService.DeleteStudent(id);


            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult InputFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new Exception("文件不存在");
            }
            Stream stream = file.InputStream;
            var content = new byte[stream.Length];
            stream.Read(content, 0, content.Length);
            stream.Seek(0, SeekOrigin.Begin);
            string data = Encoding.Default.GetString(content); 
            AdminService.InputStudents(data);
            return RedirectToAction("Index");
        }
    }
}