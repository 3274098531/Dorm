using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using MyFramework.Domain;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学生
{
    [Subject(typeof (StudentController), "Edit")]
    public class 当提交编辑学生时 : 数据准备工作
    {
        private static string id;
        private Establish context = () =>
        {
            string 班级id = 创建班级("信管15101");
            string 房间id = 创建房间("532");
            id = 创建学生("2012");
            var form = new FormCollection();
            form.Add(Keys.Code, Keys.Code);
            form.Add(Keys.Name, Keys.Name);
            form.Add(Keys.Room, 房间id);
            form.Add(Keys.Class, 班级id);

            subject = Action<StudentController>(x => x.Edit(id,form));
        };

        private Because of = () => subject.Invoke();

        private It 应该成功编辑学生 = () =>
        { 
            Student student = repository.GetOne(new ById<Student>(id));
            student.Name.ShouldEqual(Keys.Name);
            student.Class.Name.ShouldEqual("信管15101");
            student.Room.Name.ShouldEqual("532");
        };
    }
}