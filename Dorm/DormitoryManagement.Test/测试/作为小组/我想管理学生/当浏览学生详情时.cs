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
    [Subject(typeof(StudentController),"Details")]
    public class 当浏览学生详情时 : 小组已经登陆
    {
        Establish context= () =>
        {
            id=创建学生("1");
            subject = Action<StudentController>(x => x.Details(id));
        };

        public static string id;

        Because of=()=>result=subject.Invoke();
        
        It 应该成功浏览学生信息= () =>
        {
            var student = AsModel<Student>(result);
            student.ShouldNotBeNull();
            student.Code.ShouldEqual("1");
        };
    }
}
