using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为部长.我想管理部员
{
    [Subject(typeof (MemberController), "Edit")]
    public class 当编辑部员时 : 数据准备工作
    {
        Establish context = () =>
        { 
         
            id = 创建部员("2");
            FormCollection collection=new FormCollection();
            
            collection.Add(Keys.Phone,Phone);
            collection.Add(Keys.Email,Email);
            collection.Add(Keys.Position,Position);
            subject = Action<MemberController>(x => x.Edit(id,collection));
        };

        public static string Position = "Position";

        public static string Email = "Email";

        public static string Phone = "Phone";

        Because of=()=>subject.Invoke();

        It 应该编辑成功= () =>
        {
            var member = repository.GetOne(new ById<Member>(id));
            member.Phone.ShouldEqual(Phone);
            member.Position.ShouldEqual(Position.To<Position>());
            member.Email.ShouldEqual("Email");
        };

        public static string id;
    }
}
