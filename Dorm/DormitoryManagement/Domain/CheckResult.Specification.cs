using System;
using System.Linq.Expressions;
using LinqSpecs;

namespace DormitoryManagement.Domain
{
    partial class CheckResult
    {
        public class By : Specification<CheckResult>
        {
            public By(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            private Guid Id { get; set; }

            public override Expression<Func<CheckResult, bool>> IsSatisfiedBy()
            {
                return x => x.Id == Id;
            }
        }

        public class ByRoom : Specification<CheckResult>
        {
            public ByRoom(string id)
            {
                Id = id == null ? Guid.Empty : new Guid(id);
            }

            private Guid Id { get; set; }

            public override Expression<Func<CheckResult, bool>> IsSatisfiedBy()
            {
                return x => x.RoomId == Id;
            }
        }
    }
}