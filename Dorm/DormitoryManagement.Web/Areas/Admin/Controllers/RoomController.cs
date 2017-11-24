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
    public class RoomController : AdminBaseContorller
    {
        public RoomController(IAdminService adminService) : base(adminService)
        {
        }

        public ActionResult Index(int? index, int? pagesize)
        {
            return View(AdminService.GetAllRoom(index, pagesize));
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
            return View(AdminService.GetAllRoom(1, null, serch.Result()));
        }

        [HttpPost]
        public ActionResult InputFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return Content("<script type='text/javascript'>alert('没有此文件');history.go(-1);</script>");
            } 
            Stream stream = file.InputStream;
            var content = new byte[stream.Length];
            stream.Read(content, 0, content.Length);
            stream.Seek(0, SeekOrigin.Begin);
            string data = Encoding.Default.GetString(content); 
            AdminService.InputRoom(data); 
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            ViewData[Keys.Academy] =
                AdminService.GetAllAcademy().ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            ViewBag.Dorm = AdminService.GetAllDorm().ToSelectListItem(x => x.Id.ToString(), x => x.Name);
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            AdminService.CreateRoom()
                .Dorm(collection[Keys.Dorm])
                .Name(collection[Keys.Name])
                .MaxBedNum(collection[Keys.MaxBedNum])
                .Academy(collection[Keys.Academy])
                .Discipline(Keys.Discipline);

            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            Room room = AdminService.GetRoomById(id);
            ViewData[Keys.Academy] =
                AdminService.GetAllAcademy().ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                    .Selected(room.Academy.Id.ToString());
            ViewBag.Dorm = AdminService.GetAllDorm().ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                .Selected(room.DormId.ToString());
            return View(AdminService.GetRoomById(id));
        }


        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            AdminService.EditRoom(id)
                .Academy(collection[Keys.Academy])
                .Name(collection[Keys.Name])
                .Dorm(collection[Keys.Dorm])
                .MaxBedNum(collection[Keys.MaxBedNum]);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            AdminService.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}