using System;
using System.Linq;
using DormitoryManagement.Domain;
using MyFramework.Application;
using MyFramework.Application.Interface;
using MyFramework.Domain;
using MyFramework.Helper;

namespace DormitoryManagement.Application.Imp
{
    public class StudentCommand : IStudentCommand
    {
        private readonly IRepository _repository;
        private readonly Student _student;


        public StudentCommand(Student student, IRepository repository)
        {
            _student = student;
            _repository = repository;
        }


        public IStudentCommand Name(string name)
        {
            _student.Name = name;

            return this;
        }

        public IStudentCommand Sex(string sex)
        {
             
            _student.Sex = sex.To<Sex>();

            return this;
        }

        public IStudentCommand Class(string id)
        { 
            _student.Class = _repository.GetById<Class>(new Guid(id));

            return this;
        }

        public IStudentCommand Room(string id)
        { 
            _student.Room = _repository.GetById<Room>(new Guid(id));
            return this;
        }


        public IStudentCommand Discipline(string discipline)
        {
            _student.Discipline = discipline;


            return this;
        }


        public Guid Id
        {
            get { return _student.Id; }
        }
    }
}