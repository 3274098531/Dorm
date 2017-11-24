using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace DormitoryManagement.Domain
{
    partial class Member
    {
        public class By : Specification<Member>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Member, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }

        public class ByCode : Specification<Member>
        {
            public ByCode(string code)
            {
                Code = code==null?null:code.Trim();
            }

            public string Code { get; set; }

            public override Expression<Func<Member, bool>> IsSatisfiedBy()
            {
               
                return x => x.Code == Code;
            }
        }

         

        public class ByUnion : Specification<Member>
        {
            public ByUnion(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            public Guid Id { get; set; }

            public override Expression<Func<Member, bool>> IsSatisfiedBy()
            {
                return x => x.UnionId == Id;
            }
        }

        
    }
}