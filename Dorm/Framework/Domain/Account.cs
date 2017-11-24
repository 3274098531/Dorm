using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;

namespace MyFramework.Domain
{
    [Serializable]
    public partial class Account : Entity
    {
        public Account()
        {
            Roles = new Collection<Role>();
        }

        public Account(string username) : this()
        {
            UserName = username;
        }

        public string UserName { get; private set; }
        public string RealName { get; set; }
        public string PassWord { get; set; }
        public byte[] Photo { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public bool IsEnable { get; set; }
        public string Phone { get; set; }


        public void AddRole(params Role[] roles)
        {
            foreach (Role role in roles)
            {
                Roles.Add(role);
            }
        }

        public void RemoveRole(params Role[] roles)
        {
            foreach (Role role in roles)
            {
                if (Roles.Contains(role))
                {
                    Roles.Remove(role);
                }
            }
        }
    }
}