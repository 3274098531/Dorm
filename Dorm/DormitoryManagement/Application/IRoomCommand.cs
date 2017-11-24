using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface IRoomCommand:IBaseCommand
    {
        IRoomCommand Name(string name);
        IRoomCommand MaxBedNum(string num);
        IRoomCommand Academy(string id);
        IRoomCommand Dorm(string id);
        IRoomCommand Discipline(string discipline);
      
        IRoomCommand StartTime(string starttime);
        IRoomCommand EndTime(string endtime);
       
    }
}