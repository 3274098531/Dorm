using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DormitoryManagement.Domain;
using DormitoryManagement.Helps;
using EntityFramework.Extensions;
using EntityFramework.Future;
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
    public class MinisterService : IMinisterService
    {
        public MinisterService()
        {
        }

        public MinisterService(IRepository repository)
        {
            _repository = repository;
        }

        public IRepository _repository { get; set; }

        #region Student

        public IStudentCommand CreateStudent(string code)
        {
            if (_repository.IsExisted(new ByCode<Student>(code)))
                throw new Exception("学生已经存在");
            var student = new Student();
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
                .OrderBy(x => x.Class.Academy.CreateTime)
                .ThenBy(x => x.Class.Name)
                .ThenBy(x => x.Code).ToList();
        }


        public Student GetStudentById(string id)
        {
            return _repository.GetById<Student>(new Guid(id));
        }

        public Student GetStudentByCode(string code)
        {
            if (!_repository.IsExisted(new ByCode<Student>(code)))
                throw new Exception("该学生不存在");
            Student student = _repository.GetOne(new ByCode<Student>(code));
            return student;
        }

        public bool StudentExistByCode(string code)
        {
            return _repository.IsExisted(new ByCode<Student>(code));
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


        public void EditAllRoom(string dormid, string roomnames, string remark, DateTime starttime, DateTime endtime)
        {
            string[] roomnamess = roomnames.Split(new[] {',', '，'});
            foreach (var roomname in roomnamess)
            {
                var room = _repository.GetOne(new Room.ByDorm(dormid), new ByName<Room>(roomname));
                room.StartTime = starttime;
                room.EndTime = endtime;
                room.Remark = remark;
                _repository.Save(room);
            }
            
        }

        public IList<Room> GetAllRoom(params Specification<Room>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Academy.CreateTime)
                .ThenBy(x => x.Dorm.Name.Length)
                .ThenBy(x => x.Dorm.Name)
                .ThenBy(x => x.Name).ToList();
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
            int index = currentindex ?? 1;
            int size = pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            IQueryable<Dorm> dorms = _repository.GetAll<Dorm>()
                .OrderBy(x => x.Name.Length)
                .ThenBy(x => x.Name);
            int count = dorms.Count();
            return new PageList<Dorm>(index, size, count,
                dorms.Skip((index - 1)*size).Take(size).ToList());
        }

        #endregion

        #region Inspection

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
            return new PageList<Inspection>(index, size, count, inspections.Skip((index - 1)*size).Take(size).ToList());
        }

        public IList<Inspection> GetAllInspection(params Specification<Inspection>[] specifications)
        {
            return _repository.GetAll(specifications).OrderBy(x => x.CheckTime).ToList();
        }

        public IInspectionCommand CreateInspection()
        {
            var inspection = new Inspection();
            _repository.Save(inspection);
            return new InspectionCommand(inspection, _repository);
        }

        public IInspectionCommand EditInspection(string id)
        {
            Inspection inspection = GetInspectionById(id);
            _repository.Save(inspection);
            return new InspectionCommand(inspection, _repository);
        }

        public void DeleteInspection(string id)
        {
            _repository.Remove(new ById<Inspection>(id));
        }

        public Inspection GetInspectionById(string id)
        {
            if (!_repository.IsExisted(new ById<Inspection>(id)))
                throw new Exception("检查不存在");
            return _repository.GetOne(new ById<Inspection>(id));
        }

        #endregion

        #region Class

        public Class GetClassById(string id)
        {
            return _repository.GetById<Class>(new Guid(id));
        }


        


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

        public IList<CheckResult> GetAllCheckResultByRoomId(string id)
        {
            return _repository.GetAll(new CheckResult.ByRoom(id))
                .OrderBy(x => x.CheckTime).ToList();
        }

        public void EditCheckResultByCheckId(string id, string grade)
        {
            Check check = GetCheckById(id);
            check.Result = grade.To<Grade>();
            if (!_repository.IsExisted(new Check.ByRoom(id)))
            {
                _repository.Save(new CheckResult
                {
                    Room = check.Room,
                    Grade = check.Result,
                    CheckTime = check.Inspection.CheckTime
                });
            }
            else
            {
                CheckResult checkresult = _repository.GetOne(new CheckResult.ByRoom(id));
                checkresult.Grade = grade.To<Grade>();
                _repository.Save(checkresult);
            }
        }


        /// <summary>
        ///     学院反馈表
        /// </summary>
        /// <param name="name"></param>
        public void ExportCheckResultToAcademy(string name)
        {
            ExportCheckHelps.ExportCheckResultToAcademy(GetAllAcademy(), GetAllCheck(new Check.ByInspectionName(name)).OrderBy(x=>x.Room.Dorm.Name.Length).ThenBy(x=>x.Room.Dorm.Name).ThenBy(x=>x.Room.Name).ToList(),
                name);
        }

        public MemoryStream    ExportCheckResultToCheck(string name)
        {
            return  ExportCheckHelps.ExportCheckResultToCheck(name, _repository);
        }

        /// <summary>
        ///     抽查表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="acount"></param>
        public void ExportCheck(string name, int acount = 8)
        {
            ExportCheckHelps.ExportCheck(name, _repository, acount);
        }

        /// <summary>
        ///     随机添加
        /// </summary>
        /// <param name="id"></param>
        public void RandCreateCheckByInspectionId(Guid id)
        {
            Inspection inspection = _repository.GetById<Inspection>(id);
            foreach (Academy academy in GetAllAcademy())
            {
                DateTime now = DateTime.Now;
                IQueryable<Room> rooms = _repository
                    .GetAll(new Room.ByAcademyName(academy.Name), new Room.ByCanCheck(now));
                int amount = rooms.Count()/inspection.Ratio;

                IList<Check> checks = new List<Check>();
                IList<CheckResult> checkResults=new List<CheckResult>();
                rooms.OrderBy(x => Guid.NewGuid())
                    .Take(amount).ToList()
                    .ForEach(x =>
                    {
                        var room = GetRoomById(x.Id.ToString());
                        checks.Add(new Check
                        {
                            Room = room,
                            Result = Grade.A,
                            Inspection = inspection
                        });
                        checkResults.Add(new CheckResult()
                        {
                            CheckTime = inspection.CheckTime,
                            Room = room,
                            Grade = Grade.A,
                            InspecitionName = inspection.Name
                        });
                    });

                _repository.Save(checks);
                _repository.Save(checkResults);
            }
        }


        public Check GetCheckById(string id)
        {
            return _repository.GetById<Check>(new Guid(id));
        }

        public void DeleteCheck(string id)
        {
            _repository.Remove(new ById<Check>(id));
        }

        public ICheckCommand CreateCheck()
        {
            var check = new Check();
            _repository.Save(check);
            return new CheckCommand(check, _repository);
        }

        public ICheckCommand EditCheck(string id)
        {
            Check check = GetCheckById(id);
            _repository.Save(check);
            return new CheckCommand(check, _repository);
        }

        /// <summary>
        ///     排名表
        /// </summary>
        /// <param name="inspectioncode"></param>
        /// <returns></returns>
        public MemoryStream ExportCheckRank(string inspectionname)
        {
            return ExportCheckHelps.ExportCheckRank(inspectionname, _repository);
        }

        public void DeleteAllCheckByInspection(string id)
        {
            _repository.Remove(new Check.ByInspection(id));
        }

        public Inspection GetInspectionByName(string name)
        {
            return _repository.GetOne(new ByName<Inspection>(name));
        }

        
         
        public MemoryStream ExportAllCheckByInspectionName(string name)
        {
            return ExportCheckHelps.ExportAllCheckByInspectionName(GetAllCheck(new Check.ByInspectionName(name)));
        }

        public IList<Check> GetAllCheck(bool orderbyacademy, params Specification<Check>[] specifications)
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

        public IList<Check> GetAllCheck(params Specification<Check>[] specifications)
        {
            return _repository.GetAll(specifications).OrderBy(x=>x.Room.Dorm.Name.Length)
                .ThenBy(x=>x.Room.Dorm.Name)
                .ThenBy(x=>x.Room.Name).ToList();
        }

        #endregion

        #region Academy

        public IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications)
        {
            return _repository.GetAll(specifications).OrderBy(x => x.Num).ToList();
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

  

        #endregion

        #region Member

        public PageList<Member> GetAllMenber(int? currentindex, int? pagesize,
            params Specification<Member>[] specifications)
        {
            int index = currentindex ?? 1;
            IQueryable<Member> members = _repository.GetAll(specifications).OrderByDescending(x => x.Position);
            int count = members.Count();
            int size = specifications != null
                ? count
                : pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);

            return new PageList<Member>(index, size, count, members.Skip((index - 1)*size).Take(size).ToList());
        }

        public IList<Member> GetAllMenber(params Specification<Member>[] specifications)
        {
            return _repository.GetAll(specifications).ToList();
        }

        public IMemberCommand CreateMenber(string code)
        {
            if (!_repository.IsExisted(new ByCode<Student>(code)))
                throw new Exception("学生不存在");
            var member = new Member(code);
            _repository.Save(member);
            member.Student = _repository.GetOne(new ByCode<Student>(code));
            return new MemberCommand(member, _repository);
        }

        public IMemberCommand EditMenber(string id)
        {
            Member member = GetMenberById(id);
            _repository.Save(member);
            return new MemberCommand(member, _repository);
        }

        public void DeleteMenber(string id)
        {
            _repository.Remove(new ById<Member>(id));
        }

        public Member GetMenberByCode(string code)
        {
            return _repository.GetOne(new Member.ByCode(code));
        }

        public Member GetMenberById(string id)
        {
            return _repository.GetById<Member>(new Guid(id));
        }

        public bool MemberIsExitByCode(string code)
        {
            return _repository.IsExisted(new ByCode<Member>(code));
        }

        #endregion

         
         
        
    }
}