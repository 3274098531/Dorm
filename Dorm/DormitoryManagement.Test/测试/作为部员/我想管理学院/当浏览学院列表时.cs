using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部员.我想管理学院
{
    [Subject(typeof(AcademyController), "Index")]
    public class 当浏览学院列表时 : 数据准备工作
    {
        Establish context = () =>
        {
            创建学院("计院");
            创建学院("资料院");
            subject = Action<AcademyController>(x => x.Index(null, null));
        };
        Because of = () => result = subject.Invoke();
        It 应该成功浏览学院列表 = () =>
        {
            IEnumerable<Academy> academies = AsModels<Academy>(result);
            academies.Count().ShouldEqual(2);
        };

        
    }
}
