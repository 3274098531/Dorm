using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Default;
using Microsoft.JScript;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Helper;
using MyFramework.Web;

namespace DormitoryManagement.Web.Areas.Minister.Controllers
{
    public class CheckDormController : MinisterBaseController
    {
        //
        // GET: /Minister/CheckiDorm/

        public CheckDormController(IMinisterService ministerService) : base(ministerService)
        {
        }

        public ActionResult Index(int? index, int? pagesize, string name)
        {
            if (!string.IsNullOrEmpty(name))
                return View(MinisterService.GetAllInspection(index, pagesize, new ByName<Inspection>(name)));
            return View(MinisterService.GetAllInspection(index, pagesize));
        } 
        public ActionResult Create()
        {
            ViewData[Keys.InspectionType] = typeof (InspectionType).ToSelectListItems();
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string id)
        { 
            MinisterService.DeleteAllCheckByInspection(id);
            MinisterService.DeleteInspection(id);
            return RedirectToAction("Index");
        }
        public ActionResult Backups(string name)
        { 
            MemoryStream memoryStream = MinisterService.ExportAllCheckByInspectionName(name);
            using (var fs = new FileStream(GetSavePath(name) + "卫生检查详情备份.xls", FileMode.Create, FileAccess.Write))
            {
                byte[] data = memoryStream.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            return File(memoryStream, "application/vnd.ms-excel", name + "卫生检查详情表.xls");
        }
        public ActionResult Edit(string id)
        {
            Inspection inspection = MinisterService.GetInspectionById(id);
            ViewData[Keys.InspectionType]
                = typeof (InspectionType)
                    .ToSelectListItems().Selected(inspection.Type.Value());
            return View(inspection);
        }

        [HttpPost]
        public ActionResult Edit(string id,FormCollection form)
        {
            MinisterService.EditInspection(id)
                .CheckTime(form[Keys.CheckTime])
                .Name(form[Keys.Name])
                .Ratio(form[Keys.Ratio])
                .Type(form[Keys.InspectionType]);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
           var id= MinisterService.CreateInspection()
                 
                .CheckTime(form[Keys.CheckTime])
                .Name(form[Keys.Name])
                .Ratio(form[Keys.Ratio])
                .Type(form[Keys.InspectionType]).Id;
            MinisterService.RandCreateCheckByInspectionId(id); 
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CheckIndex(string AcademyName, string InspectionId, string Name, string DormName,
            string RoomId)
        {
            ViewData[Keys.Grade] = typeof (Grade).ToSelectListItems();
            ViewData[Keys.InspectionId] = InspectionId;
            Search<Check> serch = new Search<Check>()
                .Add(InspectionId, new Check.ByInspection(InspectionId))
                .Add(Name, new ByName<Check>(Name))
                .Add(AcademyName, new Check.ByAcademyName(AcademyName))
                .Add(DormName, new Check.ByDormName(DormName))
                .Add(RoomId, new Check.ByRoom(RoomId));
            return View(MinisterService.GetAllCheck(AcademyName!=null,serch.Result()));
        } 
        public ActionResult ExportCheckRank(string name)
        {
            Inspection inspection = MinisterService.GetInspectionByName(name);
            MemoryStream memoryStream = MinisterService.ExportCheckRank(name);
            return File(memoryStream, "application/vnd.ms-excel", inspection.Name + "排名表.xls");
        }

        //
        // POST: /Minister/CheckiDorm/Create
        public ActionResult CreateCkeck()
        {
            ViewBag.Dorm = MinisterService.GetAllDorm().ToSelectListItem(x => x.Id, x => x.Name);
            ViewBag.Room = new List<Room>().ToSelectListItem(x => x.Id, x => x.Name);
            ViewData[Keys.Inspection] = MinisterService.GetAllInspection()
                .ToSelectListItem(x => x.Code, x => x.Name);
            return View();
        }

        [HttpPost]
        public ActionResult CreateCkeck(FormCollection collection)
        {
            MinisterService.CreateCheck()
                .Inspection(collection[Keys.Inspection])
                .Room(collection[Keys.Room]);
            return RedirectToAction("Index");
        }

        //
        // GET: /Minister/CheckiDorm/Edit/5

        public ActionResult EditCheck(string id)
        {
            Check check = MinisterService.GetCheckById(id);
            ViewBag.Dorm = MinisterService.GetAllDorm().ToSelectListItem(x => x.Id, x => x.Name)
                .Selected(check.Room.DormId.ToString());
            ViewBag.Room = MinisterService.GetAllRoom(new ById<Room>(check.Room.Id.ToString()))
                .ToSelectListItem(x => x.Id.ToString(), x => x.Name)
                .Selected(check.Room.Id.ToString());
             
            return View(check);
        }

        //
        // POST: /Minister/CheckiDorm/Edit/5

        [HttpPost]
        public ActionResult EditCheck(string id, FormCollection collection)
        {
            MinisterService.EditCheck(id)
                .Inspection(collection[Keys.Inspection])
                .Room(collection[Keys.Room]);
            return RedirectToAction("CheckIndex");
        }


        public ActionResult EditResult(string id, string grade)
        {
            MinisterService.EditCheckResultByCheckId(id, grade);
            return RedirectToAction("Index");
        }

        //
        // GET: /Minister/CheckiDorm/Delete/5

        public ActionResult DeleteCheck(string id)
        {
            MinisterService.DeleteCheck(id);
            return RedirectToAction("CheckIndex");
        }

        public ActionResult Details(string id)
        {
            return View(MinisterService.GetRoomById(id));
        }

        public ActionResult CheckResults(string id)
        {
            return View(MinisterService.GetAllCheckResultByRoomId(id));
        }

        #region BaseFile
        public static string GetSavePath(string name)
        {
            string path = IoC.Get<IConfig>().GetValue("CheckBaseFile") + "/DownLoad/" + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path + "/";
        }
        public ActionResult ExportCheckResultToAcademy(string name)
        {
            MinisterService.ExportCheckResultToAcademy(name); 
            return File(GetSavePath(name)+ "学院反馈表.zip",
                "学院反馈表/zip", name + "学院反馈表.zip");
        }

        public ActionResult ExportCheckResultToCheck(string name)
        {
           Inspection inspection = MinisterService.GetInspectionByName(name);
           return  File(MinisterService.ExportCheckResultToCheck(name), "application/vnd.ms-excel", inspection.Name + "反馈表.xls");
        }

        public ActionResult ExportCheck(string name)
        {
            MinisterService.ExportCheck(name);
            return File(GetSavePath(name) + "卫生抽查表.zip",
                "卫生抽查表/zip", name+"卫生抽查表.zip");
        }

        #endregion
    }
}