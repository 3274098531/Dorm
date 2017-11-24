using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using DormitoryManagement.Domain;
using EntityFramework.Extensions;
using LinqSpecs;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web;
using MyFramework.Web.Page;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace DormitoryManagement.Application.Imp
{
    [IocRegister]
    public class AdminService : IAdminService
    {
        public AdminService()
        {
        }

        public AdminService(IRepository repository) : this()
        {
            _repository = repository;
        }

        public IRepository _repository { get; set; }

        #region Json

        public string ClassToJson(IList<Class> Class)
        {
            return Class.ToSelectListItem(x => x.Id, x => x.Name).ToJson();
        }

        public string RoomToJson(IList<Room> room)
        {
            return room.ToSelectListItem(x => x.Id, x => x.Name).ToJson();
        }

        #endregion

        #region Academy

        public Academy GetAcademyByName(string name)
        {
            return _repository.GetOne(new Academy.ByName(name));
        }

        public IList<Academy> GetAllAcademy(params Specification<Academy>[] specifications)
        {
            return _repository.GetAll(specifications).OrderBy(x => x.CreateTime).ToList();
        }

        public void DeleteAcademy(string id)
        {
            _repository.Remove(new ById<Academy>(id));
        }

        public IAcademyCommand CreateAcademy()
        {
            var academy = new Academy();
            _repository.Save(academy);
            return new AcademyCommand(academy, _repository);
        }

        public IAcademyCommand EditAcademy(string id)
        {
            Academy acadmey = GetAcademyById(id);
            _repository.Save(acadmey);
            return new AcademyCommand(acadmey, _repository);
        }

        public Academy GetAcademyById(string id)
        {
            if (!_repository.IsExisted(new Academy.By(id)))
                throw new Exception("学院不存在");
            return _repository.GetOne(new ById<Academy>(id));
        }

        public PageList<Academy> GetAllAcademy(int? currentsize, int? pagesize)
        {
            int index = currentsize ?? 1;
            int size = pagesize ?? int.Parse(ConfigurationManager.AppSettings[FrameworkKeys.PageSize]);
            IQueryable<Academy> academies = _repository.GetAll<Academy>()
                .OrderBy(x => x.CreateTime);
            int count = academies.Count();
            return new PageList<Academy>(index, size, count,
                academies.Skip((index - 1)*size).Take(size).ToList());
        }

        #endregion

        #region Class

        public IClassCommand CreateClass()
        {
            var class1 = new Class();
            _repository.Save(class1);
            return new ClassCommand(class1, _repository);
        }

        public Class GetClassByName(string name)
        {
            if (!_repository.IsExisted(new Class.ByName(name)))
                throw new Exception("班级不存在");
            return _repository.GetOne(new Class.ByName(name));
        }

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

        public IClassCommand EditClass(string id)
        {
            Class _class = GetClassById(id);
            _repository.Save(_class);
            return new ClassCommand(_class, _repository);
        }

        public void DeleteClass(string id)
        {
            _repository.Remove(new ById<Class>(id));
        }

        #endregion

        #region Student

        public IStudentCommand CreateStudent(string code)
        {
            if (_repository.IsExisted(new ByCode<Student>(code)))
                throw new Exception("学生已经存在");
            var student = new Student(code);
            _repository.Save(student);
            return new StudentCommand(student, _repository);
        }

        public bool StudentIsExistByCode(string code)
        {
            return _repository.IsExisted(new ByCode<Student>(code));
        }

        public IStudentCommand EditStudent(string id)
        {
            Student student = GetStudentById(id);
            _repository.Save(student);
            return new StudentCommand(student, _repository);
        }

        public void DeleteAllStudent(string code)
        {
            _repository.Remove(new Student.ByGrade(code));
        }

        public void DeleteStudent(string id)
        {
            _repository.Remove(new ById<Student>(id));
        }

        public IList<Student> GetAllStudent(params Specification<Student>[] specifications)
        {
            return _repository.GetAll<Student>()
                .OrderBy(x => x.Class.Name)
                .ThenBy(x => x.Code)
                .ToList();
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

        public IRoomCommand CreateRoom()
        {
            var room = new Room();
            _repository.Save(room);
            return new RoomCommand(room, _repository);
        }


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

        public void DeleteRoom(string id)
        {
            _repository.Remove(new ById<Room>(id));
        }


        public IList<Room> GetAllRoom(params Specification<Room>[] specifications)
        {
            return _repository.GetAll(specifications)
                .OrderBy(x => x.Dorm.Name.Length)
                .ThenBy(x => x.Dorm.Name)
                .ThenBy(x => x.Name)
                 .ToList();
        }

        public MemoryStream GetMenmorstreamToStudent(string name)
        {
            var book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");

            int rowcount = 0;
            //获取list数据 
            sheet.SetColumnWidth(0, 6000);
            sheet.SetColumnWidth(1, 3000);
            sheet.SetColumnWidth(2, 3000);
            sheet.SetColumnWidth(3, 6000);
            sheet.SetColumnWidth(4, 3000);
            sheet.SetColumnWidth(5, 3000);
            sheet.SetColumnWidth(6, 9500);
            IRow row = sheet.CreateRow(rowcount++);
            List<Student> students = GetAllStudent().Where(x => x.Code.StartsWith(name)).ToList();
            row.CreateCell(0).SetCellValue("学号");
            row.CreateCell(1).SetCellValue("姓名");
            row.CreateCell(2).SetCellValue("性别");
            row.CreateCell(3).SetCellValue("学院");
            row.CreateCell(4).SetCellValue("班级");
            row.CreateCell(5).SetCellValue("宿舍");
            row.CreateCell(6).SetCellValue("违纪情况");

            try
            {
                foreach (Student student in students)
                {
                    row = sheet.CreateRow(rowcount++);
                    row.CreateCell(0).SetCellValue(student.Code);
                    row.CreateCell(1).SetCellValue(student.Name);

                    row.CreateCell(2).SetCellValue(student.Sex.Text());

                    row.CreateCell(3).SetCellValue(student.Class.Academy.Name);

                    row.CreateCell(4).SetCellValue(student.Class.Name);

                    row.CreateCell(5).SetCellValue(student.Room.Dorm.Name + student.Room.Name);
                    row.CreateCell(6).SetCellValue(student.Discipline);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
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

        #region Drom

        public IDormCommand CreateDorm()
        {
            var dorm = new Dorm();
            _repository.Save(dorm);
            return new DormCommand(dorm, _repository);
        }


        public void DeleteDorm(string id)
        {
            _repository.Remove(new ById<Dorm>(id));
        }

        public IDormCommand EditDorm(string id)
        {
            Dorm dorm = GetDormById(id);
            _repository.Save(dorm);
            return new DormCommand(dorm, _repository);
        }

        public Dorm GetDormByName(string name)
        {
            if (!_repository.IsExisted(new Dorm.ByName(name))) throw new Exception("宿舍不存在");
            return _repository.GetOne(new Dorm.ByName(name));
        }

        public Dorm GetDormById(string id)
        {
            return _repository.GetById<Dorm>(new Guid(id));
        }

        public void InputStudents(string data)
        {
            Import<Student> import = new Import<Student>()
                .New(row => new Student(row["学号"].ToString()))
                .Map((obj, row) => obj.CreateTime = DateTime.Now)
                .Map((obj, row) => obj.Name = row["姓名"].ToString())
                .Map((obj, row) => obj.Sex = row["性别"].ToString()=="男"?Sex.Mam:Sex.Woman)
                .Map((obj, row) => obj.Class = GetClassByNameInImport(row["班级"].ToString(), row["学院"].ToString()))
                .Map(
                    (obj, row) =>
                        obj.Room =
                            GetRoomByNameAndDormName(row["房间"].ToString(), row["宿舍楼"].ToString(), row["学院"].ToString()))
                .Map((obj, row) => obj.Discipline = row["违纪情况"].ToString());
            IList<Student> result = import.MapTo(data).ToList();

            _repository.Save(result);
        }

        public void InputRoom(string data)
        {
            Import<Room> import = new Import<Room>()
                .New(row => new Room())
                .Map((obj, row) => obj.CreateTime = DateTime.Now)
                .Map((obj, row) => obj.EndTime =
                    string.IsNullOrEmpty(row["备注"].ToString())
                        ? DateTime.Now
                        : new DateTime(2222, 1, 1, 1, 1, 1))
                .Map((obj, row) => obj.Name = row["寝室号"].ToString())
                .Map((obj, row) => obj.MaxBedNum = int.Parse(row["寝室规格"].ToString()))
                .Map((obj, row) => obj.Academy = GetAcademyByName(row["学院"].ToString()))
                .Map((obj, row) => obj.Dorm = GetDormByName(row["寝室楼"].ToString() + "舍"));

            IList<Room> result = import.MapTo(data).ToList();
            _repository.Save<Room>(result);
        }

        public IList<Dorm> GetAllDorm(params Specification<Dorm>[] specifications)
        {
            return _repository.GetAll(specifications).OrderBy(x => x.Name.Length)
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
                dorms.Skip((index - 1) * size).Take(size).ToList());
        } 

        public Room GetRoomByNameAndDormName(string roomname, string dormname, string academyname)
        {
            Dorm drom = _repository.GetOne(new Dorm.ByName(dormname));
            if (_repository.IsExisted(new Room.ByDormName(dormname), new ByName<Room>(roomname)))
                return _repository.GetOne(new Room.ByDormName(dormname), new ByName<Room>(roomname));
            var id= CreateRoom()
                .Name(roomname)
                .Academy(GetAcademyByName(academyname).Id.ToString())
                .Dorm(drom.Id.ToString())
                .MaxBedNum("8").Id;
            return _repository.GetById<Room>(id);
        }

        private Class GetClassByNameInImport(string @class, string academy)
        {
            if (!_repository.IsExisted(new Class.ByName(@class)))
            {
               var id= CreateClass().Name(@class).Academy(GetAcademyByName(academy).Id.ToString()).Id;
                return _repository.GetById<Class>(id);
            }
            return _repository.GetOne(new Class.ByName(@class));
        }

        #endregion

        #region Union

        

         

        #endregion
    }
}