using System;

namespace DormitoryManagement.Application
{
    public interface IInspectionCommand
    {
        IInspectionCommand CheckTime(string checktime);
        IInspectionCommand Name(string name);
        IInspectionCommand Type(string type);
        IInspectionCommand Ratio(string ratio);
        Guid Id { get;  }
        
    }
}