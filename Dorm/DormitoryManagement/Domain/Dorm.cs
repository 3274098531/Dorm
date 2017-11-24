using System.Collections.Generic;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Dorm : Entity
    {

        public new string Name { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public Sex Type { get; set; }
    }
}