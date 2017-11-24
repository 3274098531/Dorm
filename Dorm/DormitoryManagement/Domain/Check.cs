using System;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Check : Entity
    {
        public Guid RoomId { get; set; }
        public Guid InspectionId { get; set; }
        public virtual Inspection Inspection { get; set; }
        public Grade Result { get; set; }
        public virtual Room Room { get; set; }
    }
}