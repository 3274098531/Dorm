using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;
using NPOI.SS.Formula.Functions;

namespace DormitoryManagement.Test.测试.作为部长.我想管理部员
{
    [Subject(typeof(MemberController),"Create")]
    public class 当创建部员时:数据准备工作
    {
        Establish context = () =>
        {
          
            创建学生("1");
            FormCollection collection=new FormCollection();
            collection.Add(Keys.Code,"1"); 
            collection.Add(Keys.Phone,Phone);
  
            collection.Add(Keys.Email,Email);
            collection.Add(Keys.Position,Position.ToString());
            subject = Action<MemberController>(x => x.Create(collection));
        };
         
        public static Position Position = Domain.Position.Group;

        public static string Email = "Email";

        public static string Union = "Union";

        public static string Phone = "Phone";

        Because of=()=>subject.Invoke();

        It 应该成功创建部员 = () =>
        {
            bool flag = repository.IsExisted(new Member.ByCode("1"));
            flag.ShouldBeTrue();
            var member = repository.GetOne(new Member.ByCode("1"));
            member.Position.ShouldEqual(Position);
            member.Email.ShouldEqual("Email");
           
            member.Phone.ShouldEqual("Phone");
            member.Email.ShouldEqual("Email");
        };
    }
}
