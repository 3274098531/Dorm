using System;
using System.Linq.Expressions;
using LinqSpecs;
using MyFramework.Helper;

namespace DormitoryManagement.Domain
{
    partial class Check
    {
         

        

        public class ByInspection : Specification<Check>
        {
            public ByInspection(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }
            public Guid Id { get; set; }
            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.InspectionId == Id;
            }
        }
        public class ByRoom : Specification<Check>
        {
            public ByRoom(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.RoomId == Id;
            }
        }

        public class ByYearAndMounth : Specification<Check>
        {
            public ByYearAndMounth(int year, int mouth)
            {
                Year = year;
                Mouth = mouth;
            }

            public int Year { get; set; }
            public int Mouth { get; set; }

            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.Inspection.CheckTime.Year == Year && x.Inspection.CheckTime.Month == Mouth;
            }
        }

        public class ByDormName : Specification<Check>
        {
            public ByDormName(string dormname)
            {
                DormName = dormname == null ? null : dormname.Trim();
            }

            public string DormName { get; set; }

            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.Room.Dorm.Name.StartsWith(DormName)||(x.Room.Dorm.Name+x.Room.Name).StartsWith(DormName);
            }
        }

        public class ByAcademyName : Specification<Check>
        {
            public ByAcademyName(string academyname)
            {
                AcademyName = academyname == null ? null : academyname.Trim();
            }

            public string AcademyName { get; set; }

            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.Room.Academy.Name.StartsWith(AcademyName)||x.Room.Academy.ShortName.StartsWith(AcademyName);
            }
        }

        public class ByResult : Specification<Check>
        {
            public ByResult(string result)
            {
                Result = result.To<Grade>();
            }

            public Grade Result { get; set; }
            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.Result  == Result;
            }
        }

         

        public class ByInspectionName : Specification<Check>
        {
            public ByInspectionName(string name)
            {
                Name = name == null ? null : name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Check, bool>> IsSatisfiedBy()
            {
                return x => x.Inspection.Name == Name;
            }
        }
    }
}