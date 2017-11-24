using System;
using System.Linq;
using DormitoryManagement.Domain;
using MyFramework.Application;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application.Imp
{
    public class AcademyCommand : IAcademyCommand
    {
        private readonly Academy _academy;
        private readonly IRepository _repository;

        public AcademyCommand(Academy academy, IRepository repository)
        {
            _academy = academy;
            _repository = repository ;
        }

        public IAcademyCommand Name(string name)
        {
            if (_academy.Name != name)
            { 
                if (_repository.IsExisted(new Academy.ByName(name)))
                    throw new Exception("学院已经存在");
            }
            _academy.Name = name;
            return this;
        }

        public IAcademyCommand ShortName(string shortname)
        {
            _academy.ShortName = shortname ;
            return this;
        }

        public IAcademyCommand Num(int num)
        {
            _academy.Num = num;
            return this;
        }

        public Guid Id { get { return _academy.Id; } }
    }
}