using System;
using System.Linq.Expressions;
using LinqSpecs;
using MyFramework.Helper;
using NPOI.SS.Formula.Functions;

namespace DormitoryManagement.Domain
{
    partial class Class
    {
        public class By : Specification<Class>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Class, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }

        public class ByAcademy : Specification<Class>
        {
            public ByAcademy(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Class, bool>> IsSatisfiedBy()
            {
                return x => x.AcademyId == Id;
            }
        }

        public class ByAcademyName : Specification<Class>
        {
            public ByAcademyName(string name)
            {
                Name = name == null ? null : name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Class, bool>> IsSatisfiedBy()
            {
                return x => x.Academy.Name.StartsWith( Name)|x.Academy.ShortName.StartsWith(Name);
            }
        }

        public class ByName : Specification<Class>
        {
            public ByName(string name)
            {
                Name = name == null ? null : name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Class, bool>> IsSatisfiedBy()
            {
                return x => x.Name.StartsWith(Name);
            }
        }

         
    }
}