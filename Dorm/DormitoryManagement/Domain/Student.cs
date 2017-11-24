using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Student : Entity
    {
        public Student()
        {
        }

        public Student(string code) : this()
        {
            Code = code;
        }

        public new string Name { get; set; }

        public Sex Sex { get; set; }
        public new string Code { get; set; }
        public Guid ClassId { get; set; }


        public virtual Class Class { get; set; }

        public Guid RoomId { get; set; }


        public virtual Room Room { get; set; }

        /// <summary>
        ///     违纪情况
        /// </summary>
        public string Discipline { get; set; }

         
    }

     
}