using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public enum InspectionType
    {
        [EnumText("抽查")] RandCheck,
        [EnumText("例查")] Routine,

    }
}
