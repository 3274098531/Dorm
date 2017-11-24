using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Room : Entity
    {
        public Guid DormId { get; set; }
        public new string Name { get; set; }
        public int MaxBedNum { get; set; }
        public virtual ICollection<Check> Checks { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        /// <summary>
        ///     不能检查开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     不能检查结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public string Remark { get; set; }
        public virtual Dorm Dorm { get; set; }
        public virtual Academy Academy { get; set; }


        public bool GetIsAbleCheck()
        {
            return !(DateTime.Now.CompareTo(StartTime) >= 0 && DateTime.Now.CompareTo(EndTime) <= 0);
        }
    }

    
}