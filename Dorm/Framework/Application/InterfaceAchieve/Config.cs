using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MyFramework.Application.Interface;
using MyFramework.Attribute;

namespace MyFramework.Application.InterfaceAchieve
{
    [IocRegister]
    public class Config:IConfig
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
