using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学生
{
    [Subject(typeof(StudentController), "Details")]
   public class 当浏览学生详情时:数据准备工作
    {
        private Establish context =
            () =>
            {
               id= 创建学生("123");
                subject = Action<StudentController>(x => x.Details(id));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了学生详情 =
            () =>
            {
                  Student  student = AsModel<Student>(result);
                      student.ShouldNotBeNull();
                student.Code.ShouldEqual("123");
            };

        
        private static string id;
    }
}
