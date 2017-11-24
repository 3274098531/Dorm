using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Application.Imp;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface IDormCommand : IBaseCommand
    {
        IDormCommand Name(string name);
        IDormCommand Type(string type);
        
    }
}
