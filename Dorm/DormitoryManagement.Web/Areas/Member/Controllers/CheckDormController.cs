using System;
using System.Collections.Generic;
using System.IO;
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
    public class CheckDormController : MemberBaseContorller
    {
        //
        // GET: /Minister/CheckiDorm/


        public CheckDormController(IMemberService menberService) : base(menberService)
        {
        }

        public ActionResult Index(int? index, int? pagesize, string name)
        {
            if (!string.IsNullOrEmpty(name))
                return View(MemberService.GetAllInspection(index, pagesize, new ByName<Inspection>(name)));
            return View(MemberService.GetAllInspection(index, pagesize));
        }

       
        
        [HttpPost]
        public ActionResult CheckIndex(string AcademyName,string InspectionId,  string DormName, string RoomId)
        {
            ViewData[Keys.InspectionId] = InspectionId;
            ViewData[Keys.Grade] = typeof(Grade).ToSelectListItems();
            Search<Check> serch = new Search<Check>() 
                .Add(InspectionId, new Check.ByInspection(InspectionId))
                .Add(AcademyName, new Check.ByAcademyName(AcademyName))
                .Add(DormName, new Check.ByDormName(DormName))
                .Add(RoomId, new Check.ByRoom(RoomId));
            return View(MemberService.GetAllCheck(AcademyName!=null, serch.Result()));
        }
         

        public ActionResult ExportCheckResultToAcademy(string name)
        {
            MemberService.ExportCheckResultToAcademy(name);

            return File("C:/BaseFile/学院反馈表.zip",
                "学院反馈表/zip", name + "学院反馈表.zip");
        }

        public ActionResult ExportCheckResultToCheck(string name)
        {
            MemberService.ExportCheckResultToCheck(name);
            return File("C:/BaseFile/卫生检查反馈表.zip",
                "卫生检查反馈表/zip", name + "卫生检查反馈表.zip");
        }

        public ActionResult ExportCheck(string name)
        {
            MemberService.ExportCheck(name);


            return File("C:/BaseFile/卫生抽查表.zip",
                "卫生抽查表1/zip", "卫生抽查表.zip");
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