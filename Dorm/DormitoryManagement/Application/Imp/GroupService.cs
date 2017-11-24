using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using EntityFramework.Extensions;
using EntityFramework.Future;
using LinqSpecs;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;
using NPOI.HPSF;

namespace DormitoryManagement.Application.Imp
{
    [IocRegister]
    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;

        public GroupService()
        {
        }

        public GroupService(IRepository repository)
        {
            _repository = repository;
        }

        #region Check

        public IEnumerable<SelectListItem> GetAllCheckName()
        {
            List<string> namelist = GetAllCheck().Select(x => x.Name).Distinct().ToList();
            namelist.Add("All");
            return namelist.OrderBy(x => x.Count()).Select(x => new SelectListItem {Text = x, Value = x});
        }



        public IList<Check> GetAllCheck(params Specification<Check>[] specifications)
        {
            return _repository.GetAll(specifications). ToList();
        }

        public IList<Check> GetAllCheck(bool orderbyacademy, params Specification<Check>[] specifications)
        {
            IQueryable<Check> checks = _repository.GetAll(specifications) ;
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


        public Check GetCheckById(string id)
        {
            return _repository.GetOne(new ById<Check>(id));
        }


        public void EditCheckResultByCheckId(string id, string grade)
        {
            Check check = GetCheckById(id);
            check.Result = grade.To<Grade>();
            if (!_repository.IsExisted(new Check.ByRoom(id)))
            {
                _repository.Save(new CheckResult {Room = check.Room, Grade = check.Result, CheckTime = check.Inspection.CheckTime});
            }
            else
            {
                CheckResult checkresult = _repository.GetOne(new CheckResult.ByRoom(id));
                checkresult.Grade = grade.To<Grade>();
                _repository.Save(checkresult);
            }
        }

        #endregion

        public IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x=>x.CreateTime).ToList();  
        }

        public PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize)
        {
            var index = currentsize ?? 1;
            var size = pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            IQueryable<Academy> academies = _repository.GetAll<Academy>()
                                                      .OrderBy(x => x.CreateTime);
            var count = academies.Count();
            return new PageList<Academy>(currentsize ?? 1, size, count, academies.Skip((index - 1) * size).Take(size).ToList());
        }

        public IMemberCommand EditMenber()
        {
            Member member = GetMenber();
            _repository.Save(member);
            return new MemberCommand(member, _repository);
        }


        public Member GetMenber()
        {
            return _repository.GetOne(new Member.ByCode(UserName));
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

        #region Student 

        public IList<Student> GetAllStudent(params Specification<Student>[] specifications)
        {

            return _repository.GetAll(specifications)
                .OrderBy(x => x.Class.Academy.CreateTime)
                .ThenBy(x => x.Class.Name)
                .ThenBy(x => x.Code).ToList();
        }


        public PageList<Student> GetAllStudent(int? currentindex, int? pagesize, params Specification<Student>[] specifications)
        {
            var index = currentindex ?? 1;
            
            IQueryable<Student> students = _repository.GetAll(specifications)
                .OrderBy(x => x.Class.Academy.CreateTime)
                .ThenBy(x => x.Class.Name)
                .ThenBy(x => x.Code);
            var count = students.Count();
            var size = specifications.Any()
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            var list = students.Skip((index - 1) * size).Take(size).ToList();
            return new PageList<Student>(index, size, count, list);
        } 

        public Student GetStudentById(string id)
        {
            return _repository.GetOne(new ById<Student>(id));
        }

        #endregion

        #region Room 

        public Room GetRoomById(string id)
        {
            return _repository.GetById<Room>(new Guid(id));
        }


        public IList<Room> GetAllRoom(params Specification<Room>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Academy.CreateTime)
                .ThenBy(x => x.Dorm.Name.Length)
                .ThenBy(x => x.Dorm.Name)
                .ThenBy(x => x.Name). ToList();
        }

        public PageList<Room> GetAllRoom(int? currentsize, int? pagesize, params Specification<Room>[] specifications)
        {
            var index = currentsize ?? 1;
           
            IQueryable<Room> rooms = _repository.GetAll(specifications)
                .OrderBy(x => x.Dorm.Name.Length)
                .ThenBy(x => x.Dorm.Name)
                .ThenBy(x => x.Name);
            var count = rooms.Count();
            var size = specifications.Any()
               ? count
               : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            return new PageList<Room>(index, size, count, rooms.Skip((index - 1) * size).Take(size).ToList());
        }



        public IRoomCommand EditRoom(string id)
        {
            Room room = GetRoomById(id);
            _repository.Save(room);
            return new RoomCommand(room, _repository);
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
            var size = pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            IQueryable<Dorm> dorms = _repository.GetAll<Dorm>()
                                               .OrderBy(x => x.Name.Length)
                                               .ThenBy(x => x.Name);
            var count = dorms.Count();
            return new PageList<Dorm>(currentindex ?? 1, size, count, dorms.Skip((currentindex ?? 1 - 1) * size).Take(size).ToList());
        }

        #endregion

        #region Class

        public string UserName { get; set; }

        public Class GetClassById(string id)
        {
            if (!_repository.IsExisted(new Class.By(id)))
                throw new Exception("班级不存在");
            return _repository.GetById<Class>(new Guid(id));
        }


        public IList<Class> GetAllClass(params Specification<Class>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Academy.CreateTime)
                .ThenBy(x => x.Name)
                . ToList();
        }

        public PageList<Class> GetAllClass(int? currentsize, int? pagesize, params Specification<Class>[] specifications)
        {
            var index = currentsize ?? 1;
            
            var classes = _repository.GetAll(specifications)
                .OrderBy(x => x.Academy.CreateTime).ThenBy(x => x.Name);
            var count = classes.Count();
            var size = specifications.Any()
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            return new PageList<Class>(index, size, count, classes.Skip((index - 1) * size).Take(size).ToList());
        }

         

        #endregion
    }
}