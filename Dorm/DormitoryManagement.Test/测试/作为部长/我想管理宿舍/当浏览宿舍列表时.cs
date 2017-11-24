using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部长.我想管理宿舍
{
    [Subject(typeof(DormController), "Index")]
    public class 当浏览宿舍列表时 : 数据准备工作
    {
        Establish context = () =>
        {
            创建宿舍楼("3舍");
            创建宿舍楼("9舍");
            subject = Action<DormController>(x => x.Index(null, null));
        };
        Because of = () => result = subject.Invoke();
        It 应该成功浏览学院列表 = () =>
        {
            IEnumerable<Dorm> academies = AsModels<Dorm>(result);
            academies.Count().ShouldEqual(2);
        };

        
    }
}
