using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部员.我想管理卫生
{

    [Subject(typeof(CheckDormController), "Index")]
    public class 当浏览卫生检查表时 : 数据准备工作
    {
        Establish context = () =>
        {
            创建卫生检查(Name);
            subject = Action<CheckDormController>(x => x.Index(1, 10, null));
        };

        public static string Name = "name";

        Because of = () => result = subject.Invoke();
        It 应该成功浏览卫生检查表 = () =>
        {
            var inspects = AsModels<Inspection>(result);
            inspects.ShouldNotBeEmpty();
            inspects.Select(x => x.Name).ShouldContainOnly(Name);
        };


    }

}
