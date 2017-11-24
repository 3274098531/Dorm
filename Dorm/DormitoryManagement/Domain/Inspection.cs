using System;
using System.Collections.Generic;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public class Inspection : Entity
    {
          
        public DateTime CheckTime { get; set; }
         
        public InspectionType Type { get; set; }
        public new string Name { get; set; }
        public int Ratio { get; set; }
        public ICollection<Check> Checks { get; set; }
    }
}