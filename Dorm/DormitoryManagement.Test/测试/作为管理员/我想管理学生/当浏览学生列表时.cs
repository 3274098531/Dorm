﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学生
{
    [Subject(typeof (StudentController), "Index")]
    internal class 当浏览房间列表时 : 数据准备工作
    {
        

        private Establish context =
            () =>
            {
                创建学生("123");
                subject = Action<StudentController>(x => x.Index(null, null ));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了学生列表 =
            () =>
            {
                IEnumerable<Student> students = AsModels<Student>(result);
                students.Count().ShouldEqual(1);
            };
    }
}