using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Academy : Entity
    {
       
        public new string Name { get; set; }
        public int Num { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public string ShortName { get; set; }

        [NotMapped]
        public decimal Randgrade { get; set; }

        [NotMapped]
        public decimal Checkgrade { get; set; }
    }
}