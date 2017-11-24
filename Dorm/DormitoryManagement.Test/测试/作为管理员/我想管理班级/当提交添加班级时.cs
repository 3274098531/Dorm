using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理班级
{
    [Subject(typeof(ClassController),"Create")]
    public class 当提交添加班级时:数据准备工作 
    {
        Establish context = () =>
        {
           var id= 创建学院("1324");
            FormCollection form = new FormCollection();
            form.Add(Keys.Name,Keys.Name);
            form.Add(Keys.Academy,id);
            subject=Action<ClassController>(x => x.Create(form));
        };
        Because of = () => subject.Invoke();       
        It 应该成功添加班级 = () =>
        {
            var flag = repository.IsExisted(new Class.ByName(Keys.Name));
             flag.ShouldBeTrue();
        };
    }
}
