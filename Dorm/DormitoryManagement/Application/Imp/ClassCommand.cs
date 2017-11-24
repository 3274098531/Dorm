using System;
using DormitoryManagement.Domain;
using MyFramework.Application;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application.Imp
{
    public class ClassCommand : IClassCommand
    {
        public Class Class;
        public IRepository Repository;

        public ClassCommand(Class class1, IRepository repository)
        {
            Class = class1;
            Repository = repository;
        }

        public IClassCommand Name(string name)
        {
            Class.Name = name;

            return this;
        }

        public IClassCommand Academy(string academyid)
        { 
            Class.Academy = Repository.GetById<Academy>(new Guid(academyid));
            return this;
        }

        public Guid Id { get { return Class.Id; } }
    }
}