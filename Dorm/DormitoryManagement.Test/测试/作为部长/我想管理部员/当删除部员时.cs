using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部长.我想管理部员
{
    [Subject(typeof(MemberController), "Delete")]
    public class 当删除部员时 : 数据准备工作
    {
        private static string id;
        Establish context = () =>
        {
            id = 创建部员(Keys.Code);
            subject = Action<MemberController>(x => x.Delete(id));
        };
        Because of = () => subject.Invoke();
        It 应该成功部员 = () =>
        {
            var flag = repository.IsExisted(new Member.By(id));
            flag.ShouldBeFalse();
        };
    }
}
