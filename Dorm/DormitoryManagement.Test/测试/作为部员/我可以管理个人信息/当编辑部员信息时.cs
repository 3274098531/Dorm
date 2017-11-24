using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为部员.我可以管理个人信息
{
    [Subject(typeof(InformationController), "Edit")]
    public class 当编辑部员信息时 : 部员已经登陆
    {
        Establish context = () =>
        {
            string 部门id = 创建部门("ss");
            id = 创建部员("2");
            FormCollection collection = new FormCollection();
            collection.Add(Keys.Union, 部门id);
            collection.Add(Keys.Phone, Phone);
            collection.Add(Keys.Email, Email);
            subject = Action<InformationController>(x => x.Edit(collection));
        };


        public static string Email = "Email";

        public static string Phone = "Phone";

        Because of = () => subject.Invoke();

        It 应该编辑成功 = () =>
        {
            var member = repository.GetOne(new ById<Member>(id));
            member.Phone.ShouldEqual(Phone);
            member.Email.ShouldEqual("Email");
        };

        public static string id;
    }
}
