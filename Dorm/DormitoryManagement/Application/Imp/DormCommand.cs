using System;
using DormitoryManagement.Domain;
using MyFramework.Application;
using MyFramework.Application.Interface;
using MyFramework.Helper;

namespace DormitoryManagement.Application.Imp
{
    public class DormCommand : IDormCommand
    {
        private readonly IRepository _repository;
        private readonly Dorm _dorm;


        public DormCommand(Dorm dorm, IRepository repository)
        {
            _dorm = dorm;
            _repository = repository;
        }

         

        public IDormCommand Name(string name)
        {
            _dorm.Name = name;

            return this;
        }

        public IDormCommand Type(string type)
        {
            _dorm.Type = type.To<Sex>();

            return this;
        }

        public Guid Id { get { return _dorm.Id; } }
    }
}