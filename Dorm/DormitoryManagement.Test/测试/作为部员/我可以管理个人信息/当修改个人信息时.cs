using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部员.我可以管理个人信息
{
   public class 当修改个人信息时:部员已经登陆
    {
        Establish context = () =>
        {
            FormCollection form = new FormCollection();
            form.Add(Keys.Email, Email);
            form.Add(Keys.Phone, Phone);
            subject = Action<InformationController>(x => x.Edit(form));
        };

        public static string Email = "Emial";

        public static string Phone = "Phone";

        Because of = () => result = subject.Invoke();

        It 应该成功修改个人信息 = () =>
        {
            var member = MinisterService.GetMenberByCode(UserName);
            member.Email.ShouldEqual(Email);
            member.Phone.ShouldEqual(Phone);
        };
    }
}
