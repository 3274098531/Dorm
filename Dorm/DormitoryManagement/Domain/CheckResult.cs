using System;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class CheckResult : Entity
    {
        public Grade Grade { get; set; }
        public Guid RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime CheckTime { get; set; }
        public string InspecitionName { get; set; }
    }
}