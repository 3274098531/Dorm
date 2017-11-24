using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace DormitoryManagement.Domain
{
    partial class Student
    {
        public class ByAcademy : Specification<Student>
        {
            public ByAcademy(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Class.AcademyId == Id;
            }
        }

        public class ByAcademyName : Specification<Student>
        {
            public ByAcademyName(string name)
            {
                Name =name==null?null: name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Class.Academy.Name.StartsWith(Name) || x.Class.Academy.ShortName.StartsWith(Name);
            }
        }

        public class ByClass : Specification<Student>
        {
            public ByClass(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.ClassId == Id;
            }
        }

        public class ByClassName : Specification<Student>
        {
            public ByClassName(string name)
            {
                Name = name==null?null:name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Class.Name.StartsWith(Name);
            }
        }


        public class ByDorm : Specification<Student>
        {
            public ByDorm(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Room.DormId == Id;
            }
        }

        public class ByDormName : Specification<Student>
        {
            public ByDormName(string name)
            {
                Name = name==null?null:name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Room.Dorm.Name.StartsWith(Name) || (x.Room.Dorm.Name + x.Room.Name).StartsWith(Name);
            }
        }

        public class ByGrade : Specification<Student>
        {
            public ByGrade(string code)
            {
                Code = code == null ? null:code.Trim();
            }

            public string Code { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Code.StartsWith(Code);
            }
        }

        public class ByRoom : Specification<Student>
        {
            public ByRoom(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.RoomId == Id;
            }
        }

        public class ByRoomName : Specification<Student>
        {
            public ByRoomName(string name)
            {
                Name = name == null ? null:name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Student, bool>> IsSatisfiedBy()
            {
                return x => x.Room.Name == Name;
            }
        }
    }
}