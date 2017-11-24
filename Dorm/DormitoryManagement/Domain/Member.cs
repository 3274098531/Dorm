using System;
using System.Web.Mvc;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public partial class Member : Entity
    {
        public new string Code { get; set; }

        public Member()
        {
        }

        public Member(string code) : this()
        {
            Code = code;
        }
        public Guid StudentId { get; set; }
        /// <summary>
        ///     部门
        /// </summary>
        public Guid UnionId { get; set; }

        
        public string Phone { get; set; }
        public string Email { get; set; }

        public Position Position { get; set; }
       
        public virtual Student Student { set; get; } 
    
}
}