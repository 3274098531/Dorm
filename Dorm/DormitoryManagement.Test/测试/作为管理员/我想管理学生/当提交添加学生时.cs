using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学生
{
    [Subject(typeof (StudentController), "Create")]
    public class 当提交添加学生时 : 数据准备工作
    {
        private Establish context = () =>
        {
            string 班级id = 创建班级("信管15101");
            string 房间id = 创建房间("532");
            var form = new FormCollection();
            form.Add(Keys.Code, Keys.Code);
            form.Add(Keys.Name, Keys.Name);
            form.Add(Keys.Room, 房间id);
            form.Add(Keys.Class, 班级id);

            subject = Action<StudentController>(x => x.Create(form));
        };

        private Because of = () => subject.Invoke();

        private It 应该成功添加学生 = () =>
        {
            bool flag = repository.IsExisted(new ByCode<Student>(Keys.Code));
            flag.ShouldBeTrue();
            Student student = repository.GetOne(new ByCode<Student>(Keys.Code));
            student.Name.ShouldEqual(Keys.Name);
            student.Class.Name.ShouldEqual("信管15101");
            student.Room.Name.ShouldEqual("532");
        };
    }
}