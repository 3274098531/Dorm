using DormitoryManagement.Application.Imp;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface IClassCommand : IBaseCommand
    {
        IClassCommand Name(string name);
       
        IClassCommand Academy(string academyid);
        
    }
}