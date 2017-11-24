using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface IMemberCommand : IBaseCommand
    {
        
        IMemberCommand Phone(string name);
        IMemberCommand Email(string name);
        IMemberCommand Position(string name);
       
    }
}