using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Application.Imp;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface ICheckCommand:IBaseCommand
    {
         
        ICheckCommand Room(string id);
        ICheckCommand Inspection(string id); 
        ICheckCommand Result(string result);
    }
}
