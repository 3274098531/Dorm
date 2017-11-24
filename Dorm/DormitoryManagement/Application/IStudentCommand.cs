using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface IStudentCommand:IBaseCommand
    {
         
        IStudentCommand Name(string name);
        IStudentCommand Sex(string sex);
        IStudentCommand Class(string id);
        IStudentCommand Room(string id);
        IStudentCommand Discipline(string discipline);

    }
}
