using System;
using System.Linq.Expressions;

namespace MyFramework.Domain
{
    partial class Role
    {
        public class By : LinqSpecs.Specification<Role>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Role, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }


        public class ByName : LinqSpecs.Specification<Role>
        {
            public ByName(string name)
            {
                Name = name == null ? null : name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Role, bool>> IsSatisfiedBy()
            {
                return x => x.Name == Name;
            }
        }
    }
}