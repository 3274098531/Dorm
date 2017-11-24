using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部长.我想管理部员
{
    [Subject(typeof(MemberController),"Index")]
    public class 当浏览部员时:数据准备工作
    {
        Establish context= () =>
        {
            创建部员("1");
            subject = Action<MemberController>(x => x.Index(null, null));
        };
         
        Because of=()=>result=subject.Invoke();
        It 应该成功浏览部员列表= () =>
        {
            var member = AsModels<Member>(result);
            member.Count().ShouldEqual(1);
        };
    }
}
