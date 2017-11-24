using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理宿舍
{
    [Subject(typeof(DormController),"Create")]
    public class 当我提交添加宿舍时:数据准备工作
    {
        Establish context = () =>
        {
            FormCollection form = new FormCollection();
            form.Add(Keys.Name, Keys.Name);
            form.Add(Keys.Type, Keys.Type);
            subject = Action<DormController>(x => x.Create(form));
        };
        Because of = () =>
        {
            subject.Invoke();
        };
        It 应该成功添加 = () =>
        {
            var flag = repository.IsExisted(new Dorm.ByName(Keys.Name));
            flag.ShouldBeTrue();
        };
    }
}
