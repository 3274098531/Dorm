using System;
using System.Linq.Expressions;
using LinqSpecs;
using MyFramework.Helper;

namespace DormitoryManagement.Domain
{
    partial class Room
    {
         

        public class ByAcademy : Specification<Room>
        {
            public ByAcademy(string id)
            {
                Id =id==null?Guid.Empty: new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Room, bool>> IsSatisfiedBy()
            {
               
                return x => x.Academy.Id == Id;
            }
        }

        public class ByAcademyName : Specification<Room>
        {
            public ByAcademyName(string name)
            {
                Name = name==null?null:name.Trim()  ;
            }

            public string Name { get; set; }

            public override Expression<Func<Room, bool>> IsSatisfiedBy()
            {
               
                return x => x.Academy.Name.StartsWith(Name )|| (x.Academy.ShortName).StartsWith(Name);
            }
        }

        public class ByDorm : Specification<Room>
        {
            public ByDorm(string id)
            {
                Id  =id==null?Guid.Empty:new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Room, bool>> IsSatisfiedBy()
            {
                
                return x => x.DormId == Id;
            }
        }

        public class ByDormName : Specification<Room>
        {
            public ByDormName(string name)
            {
                Name = name==null?null:name.Trim()  ;
            }

            public string Name { get; set; }

            public override Expression<Func<Room, bool>> IsSatisfiedBy()
            {
                
                return  x =>x.Dorm.Name .StartsWith(Name) || (x.Dorm.Name + x.Name).StartsWith(Name);
            }
        }
         

        public class ByStudentCount : Specification<Room>
        {
            public ByStudentCount(string count)
            {
                Count =string.IsNullOrEmpty(count)?0:int.Parse(count) ;
            }

            public int Count { get; set; }

            public override Expression<Func<Room, bool>> IsSatisfiedBy()
            {
                
                return x => x.Students.Count == Count;
            }
        }

        public class ByCanCheck : Specification<Room>
        {
            public ByCanCheck(DateTime now)
            {
                Now = now;
            }
            public DateTime Now { get; set; }
            public override Expression<Func<Room, bool>> IsSatisfiedBy()
            {
                return x => x.StartTime.CompareTo(Now) < 0 || x.EndTime.CompareTo(Now) > 0;
            }
        }
    }
}