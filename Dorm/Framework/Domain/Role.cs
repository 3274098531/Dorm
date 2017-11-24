using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyFramework.Domain
{
    public partial class Role : Entity
    {
        public Role(){}
        public Role(int grade):this()
        {
            this.Grade = grade;
            Accounts = new Collection<Account>();
        }

        public new string Name { get; set; }
        [Required(ErrorMessage = "请输入名称")]
        public string Discription { get; set; } 
        public int Grade { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}