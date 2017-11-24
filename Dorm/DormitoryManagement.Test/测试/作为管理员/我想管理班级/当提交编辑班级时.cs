using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理班级
{
    [Subject(typeof (AcademyController), "Edit")]
    public class 当提交编辑班级时 : 数据准备工作
    {
        private static string id;

        private Establish context = () =>
        {
            string 学院id = 创建学院("1234");
            id = 创建班级(Keys.Name);
            var formCollection = new FormCollection();
            formCollection.Add(Keys.Name, "123");
            formCollection.Add(Keys.Academy, 学院id);
            subject = Action<ClassController>(x => x.Edit(id, formCollection));
        };

        private Because of = () => subject.Invoke();

        private It 应该成功编辑班级 = () =>
        {
            Class academy = repository.GetOne(new Class.By(id));

            academy.Name.ShouldEqual("123");
        };
    }
}