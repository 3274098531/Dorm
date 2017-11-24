using System;
using MyFramework.Application.Interface;
using MyFramework.Domain;

namespace MyFramework.Application.InterfaceAchieve
{
    public class RoleCommand : IRoleCommand
    {
        public RoleCommand(Role role, IRepository dorm)
        {
            Role = role;

            Dorm = dorm;
        }

        public IRepository Dorm { get; set; }

        public Role Role { get; set; }

        public IRoleCommand Name(string name)
        {
            Role.Name = name;

            return this;
        }

        public IRoleCommand Discription(string discription)
        {
            Role.Discription = discription;
            return this;
        }

        public Guid Id { get { return Role.Id; } }
    }
}