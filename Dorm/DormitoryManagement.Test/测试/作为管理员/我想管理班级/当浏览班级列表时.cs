using System.Collections.Generic;
using System.Linq;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理班级
{
    [Subject(typeof (ClassController), "Index")]
    internal class 当浏览班级列表时 : 数据准备工作
    {
        private Establish context =
            () =>
            {
                创建班级("计算机");
                subject = Action<ClassController>(x => x.Index(null, null));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了班级列表 =
            () =>
            {
                IEnumerable<Class> academies = AsModels<Class>(result);
                academies.Count().ShouldEqual(1);
            };
    }
}