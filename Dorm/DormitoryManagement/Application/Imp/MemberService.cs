using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Helps;
using EntityFramework.Extensions;
using EntityFramework.Future;
using Ionic.Zip;
using LinqSpecs;
using MyFramework.Application.Interface;
using MyFramework.Application.InterfaceAchieve;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace DormitoryManagement.Application.Imp
{
    [IocRegister]
    public class MemberService : IMemberService
    {
        public MemberService(IRepository repository)
        {
            _repository = repository;
        }

        public IRepository _repository { get; set; }

        #region Student

        public IStudentCommand CreateStudent(string code)
        {
            if (_repository.IsExisted(new ByCode<Student>(code)))
                throw new Exception("学生已经存在");
            var student = new Student(code);
            _repository.Save(student);
            return new StudentCommand(student, _repository);
        }

        public PageList<Student> GetAllStudent(int? currentindex, int? pagesize,
            params Specification<Student>[] specifications)
        {
            int index = currentindex ?? 1;
            
            IQueryable<Student> students = _repository.GetAll(specifications)
                .OrderBy(x => x.Class.Academy.CreateTime)
                .ThenBy(x => x.Class.Name)
                .ThenBy(x => x.Code);
            int count = students.Count();
            int size = specifications.Any()
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            List<Student> list = students.Skip((index - 1)*size).Take(size).ToList();
            return new PageList<Student>(index, size, count, list);
        }

        public IStudentCommand EditStudent(string id)
        {
            Student student = GetStudentById(id);
            _repository.Save(student);
            return new StudentCommand(student, _repository);
        }

        public IList<Student> GetAllStudent(params Specification<Student>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Class.Name)
                .ThenBy(x => x.Code).ToList();
        }

        public Student GetStudentById(string id)
        {
            return _repository.GetOne(new ById<Student>(id));
        }

        public Student GetStudentByCode(string code)
        {
            if (!_repository.IsExisted(new ByCode<Student>(code)))
                throw new Exception("该学生不存在");
            Student student = _repository.GetOne(new ByCode<Student>(code));
            return student;
        }

        public void DeleteAllStudent(string code)
        {
            _repository.Remove(new Student.ByGrade(code));
        }

        public void DeleteStudent(string id)
        {
            _repository.Remove(new ById<Student>(id));
        }

        #endregion

        #region Room 

        public Room GetRoomById(string id)
        {
            return _repository.GetById<Room>(new Guid(id));
        }


        public IRoomCommand EditRoom(string id)
        {
            Room room = GetRoomById(id);
            _repository.Save(room);
            return new RoomCommand(room, _repository);
        }

        public IList<Room> GetAllRoom(params Specification<Room>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Dorm.Name.Length)
                .ThenBy(x => x.Dorm.Name)
                .ThenBy(x => x.Name)
                .ToList();
        }

        public PageList<Room> GetAllRoom(int? currentsize, int? pagesize, params Specification<Room>[] specifications)
        {
            int index = currentsize ?? 1;
            
            IQueryable<Room> rooms = _repository.GetAll(specifications)
                .OrderBy(x => x.Dorm.Name.Length)
                .ThenBy(x => x.Dorm.Name)
                .ThenBy(x => x.Name);
            int count = rooms.Count();
            int size = specifications.Any()
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            return new PageList<Room>(index, size, count, rooms.Skip((index - 1)*size).Take(size).ToList());
        }

        #endregion

        #region Dorm

        public IList<Dorm> GetAllDorm(params Specification<Dorm>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Name.Length)
                .ThenBy(x => x.Name).ToList();
        }

        public PageList<Dorm> GetAllDorm(int? currentindex, int? pagesize)
        {
            int size = pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            IQueryable<Dorm> dorms = _repository.GetAll<Dorm>()
                .OrderBy(x => x.Name.Length)
                .ThenBy(x => x.Name);
            int count = dorms.Count();
            return new PageList<Dorm>(currentindex ?? 1, size, count,
                dorms.Skip((currentindex ?? 1 - 1)*size).Take(size).ToList());
        }

        #endregion

        #region Class

        public Class GetClassById(string id)
        {
            return _repository.GetById<Class>(new Guid(id));
        }

        public PageList<Inspection> GetAllInspection(int? currentsize, int? pagesize,
            params Specification<Inspection>[] specifications)
        {
            int index = currentsize ?? 1;

            IOrderedQueryable<Inspection> inspections = _repository.GetAll(specifications)
                .OrderBy(x => x.CheckTime).ThenBy(x => x.Name);
            int count = inspections.Count();
            int size = specifications.Any()
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            return new PageList<Inspection>(index, size, count, inspections.Skip((index - 1) * size).Take(size).ToList());
        }


         

        public string UserName { get; set; }

        public IList<Class> GetAllClass(params Specification<Class>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Academy.CreateTime)
                .ThenBy(x => x.Name)
                .ToList();
        }

        public PageList<Class> GetAllClass(int? currentsize, int? pagesize, params Specification<Class>[] specifications)
        {
            int index = currentsize ?? 1;
            
            IOrderedQueryable<Class> classes = _repository.GetAll(specifications)
                .OrderBy(x => x.Academy.CreateTime).ThenBy(x => x.Name);
            int count = classes.Count();
            int size = specifications.Any()
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            return new PageList<Class>(index, size, count, classes.Skip((index - 1)*size).Take(size).ToList());
        }

        #endregion

        #region Check

        public IList<Check> GetAllCheck(params Specification<Check>[] specifications)
        {
            return _repository.GetAll(specifications) .ToList();
        }

        public IList<Check> GetAllCheck( bool orderbyacademy, params Specification<Check>[] specifications)
        {
            IQueryable<Check> checks = _repository.GetAll(specifications);
            if (!orderbyacademy)
            {
                return checks
                    .OrderBy(x => x.Room.Dorm.Name.Length)
                    .ThenBy(x => x.Room.Dorm.Name)
                    .ThenBy(x => x.Room.Name).ToList();
            }
            return checks
                .OrderBy(x => x.Room.Academy.CreateTime)
                .ThenBy(x => x.Room.Dorm.Name.Length)
                .ThenBy(x => x.Room.Dorm.Name)
                .ThenBy(x => x.Room.Name).ToList();
        }


        public void ExportCheckResultToAcademy(string name)
        {
             ExportCheckHelps.ExportCheckResultToAcademy(GetAllAcademy(),GetAllCheck(new Check.ByInspectionName(name)),name);
        }

        public void ExportCheckResultToCheck(string name)
        {
            ExportCheckHelps.ExportCheckResultToAcademy(GetAllAcademy(),GetAllCheck(new Check.ByInspectionName(name)),name);
        }
         
        public void ExportCheck(string name, int acount = 8)
        {
            ExportCheckHelps.ExportCheck(name,_repository,acount);
        }


        public Check GetCheckById(string id)
        {
            return _repository.GetOne(new ById<Check>(id));
        }


        public ICheckCommand EditCheck(string id)
        {
            Check check = GetCheckById(id);
            _repository.Save(check);
            return new CheckCommand(check, _repository);
        }

        public IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications)
        {
            return _repository.GetAll(specifications).OrderBy(x => x.CreateTime).ToList();
        }

        public PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize)
        {
            int index = currentsize ?? 1;
            int size = pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            IQueryable<Academy> academies = _repository.GetAll<Academy>()
                .OrderBy(x => x.CreateTime);
            int count = academies.Count();
            return new PageList<Academy>(currentsize ?? 1, size, count,
                academies.Skip((index - 1)*size).Take(size).ToList());
        }


        public IEnumerable<SelectListItem> GetAllCheckName()
        {
            List<string> namelist = GetAllCheck().Select(x => x.Name).Distinct().ToList();
            namelist.Add("All");
            return namelist.OrderBy(x => x.Count()).Select(x => new SelectListItem {Text = x, Value = x});
        }

 

        


        

        #endregion

        public IList<CheckResult> GetAllCheckResultByRoomId(string id)
        {
            return _repository.GetAll(new CheckResult.ByRoom(id))
                .OrderBy(x => x.CheckTime).ToList();
        }

        public IMemberCommand EditMenber()
        {
            Member member = _repository.GetOne(new Member.ByCode(UserName));

            return new MemberCommand(member, _repository);
        }


        public Member GetMenber()
        {
            return _repository.GetOne(new Member.ByCode(UserName));
        }
    }
}