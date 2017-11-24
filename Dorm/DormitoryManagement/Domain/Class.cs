using System;
using System.Collections.Generic;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Class : Entity
    {
        public new string Name { get; set; }
        public Guid AcademyId { get; set; }  
        public virtual Academy Academy { set; get; }
        public virtual ICollection<Student> Students { get; set; }
    }
}