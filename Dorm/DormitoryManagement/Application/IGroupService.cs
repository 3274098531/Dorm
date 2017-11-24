using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using LinqSpecs;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace DormitoryManagement.Application
{
    public interface IGroupService
    {
        string UserName { get; set; }
        Class GetClassById(string id);
        IList<Class> GetAllClass(params Specification<Class>[] specifications);
        PageList<Class> GetAllClass(int? currentsize, int? pagesize,params Specification<Class>[] specifications );

        IList<Student> GetAllStudent(params Specification<Student>[] specifications);

        PageList<Student> GetAllStudent(int? currentindex, int? pagesize, params Specification<Student>[] specifications);

        Student GetStudentById(string id);


        Room GetRoomById(string id);
        IList<Room> GetAllRoom(params Specification<Room>[] specifications);

        PageList<Room> GetAllRoom(int? currentsize, int? pagesize, params Specification<Room>[] specifications);

        IRoomCommand EditRoom(string id);


        IList<Dorm> GetAllDorm(params Specification<Dorm>[] specifications);
        PageList<Dorm> GetAllDorm(int? currentindex, int? pagesize);

        IEnumerable<SelectListItem> GetAllCheckName();
        IList<Check> GetAllCheck(bool orderbyacademy, params Specification<Check>[] specifications);
        IList<Check> GetAllCheck(params Specification<Check>[] specifications);
        Check GetCheckById(string id);
        void EditCheckResultByCheckId(string id, string grade);


        IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications);
        PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize);

        IMemberCommand EditMenber();
        Member GetMenber();
        PageList<Inspection> GetAllInspection(int? index, int? pagesize, params Specification<Inspection>[] specifications );
    }
}