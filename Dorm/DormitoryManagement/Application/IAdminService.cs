using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using DormitoryManagement.Domain;
using LinqSpecs;
using MyFramework.Web.Page;

namespace DormitoryManagement.Application
{
    public interface IAdminService
    {
        IAcademyCommand CreateAcademy();
        Academy GetAcademyByName(string name);
        IList<Academy> GetAllAcademy(params Specification<Academy>[] expressions);
        PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize);
        void DeleteAcademy(string id);
        IAcademyCommand EditAcademy(string id);
        Academy GetAcademyById(string id);
        IClassCommand CreateClass();
        Class GetClassByName(string name);
        Class GetClassById(string id);
        IList<Class> GetAllClass(params Specification<Class>[] specifications);
        PageList<Class> GetAllClass(int? currentsize, int? pagesize, params Specification<Class>[] specifications );
        IClassCommand EditClass(string id);
        void DeleteClass(string id);
        MemoryStream GetMenmorstreamToStudent(string name);
        IStudentCommand CreateStudent(string code);
        IStudentCommand EditStudent(string id);
        void DeleteStudent(string id);
        IList<Student> GetAllStudent(params Specification<Student>[] specifications);
        bool StudentIsExistByCode(string code);
        PageList<Student> GetAllStudent(int? currentindex, int? pagesize, params Specification<Student>[] specifications);
        Student GetStudentById(string id);
        Student GetStudentByCode(string code);
        void InputStudents(string path);
        void DeleteAllStudent(string code);
        IRoomCommand CreateRoom();
         
        Room GetRoomById(string id);
        IList<Room> GetAllRoom(params Specification<Room>[] specifications);
        PageList<Room> GetAllRoom(int? currentsize, int? pagesize, params Specification<Room>[] specifications);
        IRoomCommand EditRoom(string id);
        void DeleteRoom(string id);
        void InputRoom(string data);
        IDormCommand CreateDorm();
        Dorm GetDormByName(string name);
        IList<Dorm> GetAllDorm(params Specification<Dorm>[] specifications);
        PageList<Dorm> GetAllDorm(int? currentindex, int? pagesize);
        void DeleteDorm(string id);
        IDormCommand EditDorm(string id);
        Dorm GetDormById(string id);

        string ClassToJson(IList<Class> Class);
        string RoomToJson(IList<Room> room);
        
        
     

         
    }
}