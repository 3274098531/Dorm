using System;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Helper;

namespace DormitoryManagement.Application.Imp
{
    public class MemberCommand : IMemberCommand
    {
        private readonly Member _menber;
        private readonly IRepository _repository;


        public MemberCommand(Member menber, IRepository repository)
        {
            _menber = menber;
            _repository = repository;
        }


         

        public IMemberCommand Phone(string name)
        {
            _menber.Phone = name;

            return this;
        }

        public IMemberCommand Email(string name)
        {
            _menber.Email = name;


            return this;
        }

        public IMemberCommand Position(string name)
        {
            _menber.Position = name.To<Position>();


            return this;
        }

         

        public Guid Id
        {
            get { return _menber.Id; }
        }
    }
}