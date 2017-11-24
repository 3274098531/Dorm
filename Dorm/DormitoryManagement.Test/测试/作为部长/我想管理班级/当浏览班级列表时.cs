using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部长.我想管理班级
{   
    [Subject(typeof(ClassController),"Index")]
    internal class 当浏览班级列表时:数据准备工作
    {
        Establish context= () =>
        {
            创建班级("计算机");
            subject = Action<ClassController>(x => x.Index(null, null));
        };
        Because of=()=>  result = subject.Invoke(); 
        
        It 应该成功浏览班级列表= () =>
        {
            var academies = ((ViewResult)result).ViewData.Model as IEnumerable<Class>;
            academies.Count().ShouldEqual(1);
        };
    }
     
}
