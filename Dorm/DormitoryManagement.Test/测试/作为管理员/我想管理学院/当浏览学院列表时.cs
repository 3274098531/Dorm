using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学院
{
    [Subject(typeof (AcademyController), "Index")]
    internal class 当浏览学院列表时 : 数据准备工作
    {
         

        private Establish context =
            () =>
            {
                创建学院("计算机");
                subject = Action<AcademyController>(x => x.Index(null, null));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了学院列表 =
            () =>
            {
                var academies = ((ViewResult) result).ViewData.Model as IEnumerable<Academy>;
                academies.Count().ShouldEqual(1);
            };
    }
}

 