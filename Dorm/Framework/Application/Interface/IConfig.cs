using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.Application.Interface
{
    public interface IConfig
    {
        string GetValue(string key);
    }
}
