using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace DormitoryManagement.Domain
{
    partial class Dorm
    {
        public class By : Specification<Dorm>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Dorm, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }
        public class ByName:Specification<Dorm>
        {
            public ByName(string name)
            {
                Name =name==null?null: name.Trim();
            }

            public string Name { get; set; }
            public override Expression<Func<Dorm, bool>> IsSatisfiedBy()
            {
               
                return x => x.Name.StartsWith(Name);
            }
        }
    }
}