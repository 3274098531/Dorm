using System;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Helper;

namespace DormitoryManagement.Application.Imp
{
    public class CheckCommand : ICheckCommand
    {
        private readonly Check _check;
        private readonly IRepository _repository;


        public CheckCommand(Check check, IRepository repository)
        {
            _check = check;
            _repository = repository;
        }


        public ICheckCommand Room(string id)
        {
            _check.Room = _repository.GetById<Room>(new Guid(id));

            return this;
        }

        public ICheckCommand Inspection(string id)
        {
            if (!_repository.IsExisted(new ById<Inspection>(id)))
                throw new Exception("检查不存在");
            _check.Inspection = _repository.GetOne(new ById<Inspection>(id));
            return this;
        }


        public ICheckCommand Result(string result)
        {
            _check.Result = result.To<Grade>();
            return this;
        }


        public Guid Id
        {
            get { return _check.Id; }
        }
    }
}