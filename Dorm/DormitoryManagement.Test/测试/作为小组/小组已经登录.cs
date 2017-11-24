using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为小组
{
    public class 小组已经登陆 : 数据准备工作
    {
        Establish context = () =>
        {
            创建部员(UserName);
            创建小组账号(UserName, PassWord);
            账号登陆(UserName, PassWord);
        };

        protected static string UserName = "201517020119";
        protected static string PassWord = "1234";

    }
}
