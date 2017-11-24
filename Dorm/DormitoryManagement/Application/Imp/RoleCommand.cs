using System;
using DormitoryManagement.Database;
using DormitoryManagement.Domain;
using MyFramework.Domain;

namespace DormitoryManagement.Application.Imp
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
    }
}