using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Group.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为小组.我想管理学生
{
    [Subject(typeof(StudentController),"Index")]
    public class 当浏览学生时 : 小组已经登陆
    {
        Establish context= () =>
        {
            创建学生("1");
            创建学生("2");
            subject = Action<StudentController>(x => x.Index(null, null));
        };

        Because of = () => result = subject.Invoke();
        
        It 应该成功浏览学生= () =>
        {
            var students = AsModels<Student>(result);
            students.Count().ShouldEqual(3);
             
        };
    }
}
