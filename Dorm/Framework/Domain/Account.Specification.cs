using System;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;

namespace MyFramework.Domain
{
    partial class Account
    {
        public class By : Specification<Account>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }

        public class ByNotRole : Specification<Account>
        {
            public ByNotRole(string rolename)
            {
                RoleName = rolename == null ? null : rolename.Trim();
            }

            public string RoleName { get; set; }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.Roles.Select(y => y.Name).All(z => z != RoleName);
            }
        }

        public class ByRealName : Specification<Account>
        {
            public ByRealName(string name)
            {
                Name = name == null ? null : name.Trim();
            }

            public string Name { get; set; }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.RealName == Name;
            }
        }

        public class ByRole : Specification<Account>
        {
            public ByRole(string rolename)
            {
                RoleName = rolename == null ? null : rolename.Trim();
            }

            public string RoleName { get; set; }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.Roles.Select(y => y.Name).Any(z => z.Equals(RoleName));
            }
        }

        public class ByUserName : Specification<Account>
        {
            public ByUserName(string username)
            {
                UserNam = username.Trim();
            }

            public string UserNam { get; set; }

            public override Expression<Func<Account, bool>> IsSatisfiedBy()
            {
                return x => x.UserName == UserNam;
            }
        }
    }
}