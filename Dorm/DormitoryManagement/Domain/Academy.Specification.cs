using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace DormitoryManagement.Domain
{
    partial class Academy
    {
        public class By : Specification<Academy>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            private Guid Id { get; set; }

            public override Expression<Func<Academy, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }

        public class ByName : Specification<Academy>
        {
            public ByName(string name)
            {
                Name =name==null?null: name.Trim();
            }

            private string Name { get; set; }

            public override Expression<Func<Academy, bool>> IsSatisfiedBy()
            {
               
                return x => x.Name == Name || x.ShortName == Name;
            }
        }
    }
}