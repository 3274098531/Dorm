using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using LinqSpecs;
using MyFramework.Application.Interface;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace DormitoryManagement.Application
{
    public interface IMemberService
    {
        string UserName { get; set; }
        IList<Class> GetAllClass(params Specification<Class>[] specifications);
        PageList<Class> GetAllClass(int? currentsize, int? pagesize, params Specification<Class>[] specifications );
        IStudentCommand EditStudent(string id);
        IList<Student> GetAllStudent(params Specification<Student>[] specifications);
        PageList<Student> GetAllStudent(int? currentindex, int? pagesize,params Specification<Student>[] specifications );
        Student GetStudentById(string id);
        Student GetStudentByCode(string code);
        Room GetRoomById(string id);
        IList<Room> GetAllRoom(params Specification<Room>[] specifications);
        PageList<Room> GetAllRoom(int? currentsize, int? pagesize, params Specification<Room>[] specifications );
        IRoomCommand EditRoom(string id);
        IList<Dorm> GetAllDorm(params Specification<Dorm>[] specifications);
        PageList<Dorm> GetAllDorm(int? currentindex, int? pagesize);
        IList<Check> GetAllCheck(bool orderbyacademy, params Specification<Check>[] specifications );
        IList<Check> GetAllCheck(params Specification<Check>[] specifications);
        IEnumerable<SelectListItem> GetAllCheckName();
        Check GetCheckById(string id);
        ICheckCommand EditCheck(string id);
        void ExportCheckResultToAcademy(string name);
        void ExportCheckResultToCheck(string name);
        IMemberCommand EditMenber();
        Member GetMenber();
        IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications);
        PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize);
        void ExportCheck(string checkname, int acount = 8);
        
         
        IList<CheckResult> GetAllCheckResultByRoomId(string id);
        IStudentCommand CreateStudent(string code);
        Class GetClassById(string id);
        
        PageList<Inspection> GetAllInspection(int? index, int? pagesize, params Specification<Inspection>[] specifications );
    }
}