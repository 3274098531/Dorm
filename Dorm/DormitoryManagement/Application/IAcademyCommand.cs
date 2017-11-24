using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Application.Imp;
using MyFramework.Application.Interface;

namespace DormitoryManagement.Application
{
    public interface IAcademyCommand:IBaseCommand
    {
        IAcademyCommand Name(string name);
        IAcademyCommand ShortName(string alais);
        IAcademyCommand Num(int num);
    }
}
