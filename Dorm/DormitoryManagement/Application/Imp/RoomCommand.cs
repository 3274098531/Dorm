using System;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application.Imp
{
    public class RoomCommand : IRoomCommand
    {
        private readonly IRepository _repository;
        private readonly Room _room;


        public RoomCommand(Room room, IRepository repository)
        {
            _room = room;
            _repository = repository;
        }


        public IRoomCommand Name(string name)
        {
            _room.Name = name;
            return this;
        }

        public IRoomCommand MaxBedNum(string num)
        {
            int Num = int.Parse(num);
            _room.MaxBedNum = Num;

            return this;
        }


        public IRoomCommand StartTime(string starttime)
        {
            if (starttime != null)
                _room.StartTime = DateTime.Parse(starttime);
            return this;
        }

        public IRoomCommand EndTime(string endtime)
        {
            if (endtime != null)
                _room.EndTime = DateTime.Parse(endtime);
            return this;
        }

        public IRoomCommand Dorm(string id)
        {
            _room.Dorm = _repository.GetById<Dorm>(new Guid(id));

            return this;
        }

        public IRoomCommand Academy(string id)
        {
            _room.Academy = _repository.GetById<Academy>(new Guid(id));

            return this;
        }


        public IRoomCommand Discipline(string discipline)
        {
            _room.Remark = discipline;

            return this;
        }

        public Guid Id
        {
            get { return _room.Id; }
        }
    }
}