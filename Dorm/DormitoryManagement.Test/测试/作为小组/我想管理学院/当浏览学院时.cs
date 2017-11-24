using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Group.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为小组.我想管理学院
{
    [Subject(typeof(AcademyController),"Index")]
    public class 当浏览学院时 : 小组已经登陆
    {
        Establish context= () =>
        {
            创建学院("计算机");
            subject = Action<AcademyController>(x => x.Index(null, null));
        };
        Because of =()=>result=subject.Invoke();
        
        It 应该成功浏览学院= () =>
        {
            bool flag = repository.IsExisted(new Academy.ByName("计算机"));
            flag.ShouldBeTrue(); 
        };
    }
}
