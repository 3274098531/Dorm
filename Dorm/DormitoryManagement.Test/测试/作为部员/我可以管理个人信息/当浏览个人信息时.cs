using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部员.我可以管理个人信息
{
    [Subject(typeof(InformationController),"Index")]
    public class 当浏览个人信息时:部员已经登陆
    {
        Establish context = () =>
        { 
            subject = Action<InformationController>(x => x.Index());
        };
        Because of = () => result = subject.Invoke();
       
        It 应该成功浏览个人信息 = () =>
        {
            var member = AsModel<Member>(result);
            member.Code.ShouldEqual("201517020119");
        };
    }
}
