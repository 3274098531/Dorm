using System;
using System.Collections.Generic;
using System.IO;
using DormitoryManagement.Domain;
using LinqSpecs;
using MyFramework.Web.Page;

namespace DormitoryManagement.Application
{
    public interface IMinisterService
    {
        IList<Class> GetAllClass(params Specification<Class>[] specifications);
        PageList<Class> GetAllClass(int? currentsize, int? pagesize, params Specification<Class>[] specifications);
        Class GetClassById(string id);


        IStudentCommand CreateStudent(string code);
        IStudentCommand EditStudent(string id);


        IList<Student> GetAllStudent(params Specification<Student>[] specifications);
        PageList<Student> GetAllStudent(int? currentindex, int? pagesize, params Specification<Student>[] specifications);

        Student GetStudentById(string id);
        Student GetStudentByCode(string code);
        bool StudentExistByCode(string code);

        Room GetRoomById(string id);
        IList<Room> GetAllRoom(params Specification<Room>[] specifications);
        PageList<Room> GetAllRoom(int? currentsize, int? pagesize, params Specification<Room>[] specifications);
        IRoomCommand EditRoom(string id);
        void EditAllRoom(string dormid, string roomnames, string remark, DateTime starttime, DateTime endtime);

        IList<Dorm> GetAllDorm(params Specification<Dorm>[] specifications);
        PageList<Dorm> GetAllDorm(int? currentindex, int? pagesize);


        PageList<Inspection> GetAllInspection(int? currentsize, int? pagesize,
            params Specification<Inspection>[] specifications);

        IList<Inspection> GetAllInspection(params Specification<Inspection>[] specifications);
        IInspectionCommand CreateInspection();
        IInspectionCommand EditInspection(string id);
        void DeleteInspection(string id);
        Inspection GetInspectionById(string id);
        IList<Check> GetAllCheck(bool orderbyacademy = false, params Specification<Check>[] specifications);
        IList<Check> GetAllCheck(params Specification<Check>[] specifications);

        void RandCreateCheckByInspectionId(Guid id);
        Check GetCheckById(string id);
        void DeleteCheck(string id);
        ICheckCommand CreateCheck();
        ICheckCommand EditCheck(string id);
        void ExportCheckResultToAcademy(string name);
        MemoryStream  ExportCheckResultToCheck(string name);
        void ExportCheck(string name, int acount = 8);


        MemoryStream ExportAllCheckByInspectionName(string name);
        IList<CheckResult> GetAllCheckResultByRoomId(string id);
        void EditCheckResultByCheckId(string id, string grade);
 

        PageList<Member> GetAllMenber(int? currentindex, int? pagesize, params Specification<Member>[] specifications);
        IList<Member> GetAllMenber(params Specification<Member>[] specifications);
        IMemberCommand CreateMenber(string code);
        IMemberCommand EditMenber(string id);
        void DeleteMenber(string id);
        Member GetMenberByCode(string name);
        Member GetMenberById(string id);

        IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications);
        PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize);

        bool MemberIsExitByCode(string code);
       
        MemoryStream ExportCheckRank(string name);
        void DeleteAllCheckByInspection(string id);
        Inspection GetInspectionByName(string name);
      
         
       
    }
}