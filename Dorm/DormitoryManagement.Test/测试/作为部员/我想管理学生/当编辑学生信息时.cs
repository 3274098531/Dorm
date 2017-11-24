using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为部员.我想管理学生
{
    [Subject(typeof(StudentController), "Edit")]
    public class 当编辑学生信息时 : 数据准备工作
    {
        public static string id;
        Establish context = () =>
        {
            id = 创建学生("1");
            string 房间id = 创建房间("11");
            FormCollection collection = new FormCollection();
            collection.Add(Keys.Code, Keys.Code);
            collection.Add(Keys.Room, 房间id);
            subject = Action<StudentController>(x => x.Edit(id, collection));
        };

        Because of = () => subject.Invoke();
        It 应该成功编辑学生列表 = () =>
        {
            var student = repository.GetOne(new ById<Student>(id));
            student.Room.Name.ShouldEqual("11");
        };
    }
}
