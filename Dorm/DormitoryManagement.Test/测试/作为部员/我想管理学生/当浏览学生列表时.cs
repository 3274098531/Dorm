using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部员.我想管理学生
{
    [Subject(typeof(StudentController), "Index")]
    public class 当浏览学生列表时 : 数据准备工作
    {
        Establish context = () =>
        {
            创建学生("1");
            subject = Action<StudentController>(x => x.Index(null, null));
        };
        Because of = () => result = subject.Invoke();
        
        It 应该成功浏览学生列表 = () =>
        {
            IEnumerable<Student> students = AsModels<Student>(result);
            students.Count().ShouldEqual(1);
        };
    }
}
