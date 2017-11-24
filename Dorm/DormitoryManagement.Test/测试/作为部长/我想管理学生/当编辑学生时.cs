using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using MyFramework.Helper;
using StudentController = DormitoryManagement.Web.Areas.Minister.Controllers.StudentController;

namespace DormitoryManagement.Test.测试.作为部长.我想管理学生
{
    [Subject(typeof(StudentController),"Edit")]
    public class 当编辑学生时:数据准备工作
    {
        public static string id;
        Establish context= () =>
        {
            id=创建学生("1");
            string 房间id = 创建房间("11");
            FormCollection collection=new FormCollection();
            collection.Add(Keys.Code,Keys.Code);
            collection.Add(Keys.Name,Name);
            collection.Add(Keys.Sex,Sex.Value());
            collection.Add(Keys.Room,房间id);
            subject = Action<StudentController>(x => x.Edit(id,collection));
        };

        public static Sex Sex = Domain.Sex.Mam;

        public static string Name = "Name";

        Because of=()=>subject.Invoke();
        It 应该成功编辑学生列表= () =>
        {
            var student = repository.GetOne(new ById<Student>(id));
            student.Name.ShouldEqual(Name);
            student.Sex.ShouldEqual(Sex);
            student.Room.Name.ShouldEqual("11");
        };
    }
}
