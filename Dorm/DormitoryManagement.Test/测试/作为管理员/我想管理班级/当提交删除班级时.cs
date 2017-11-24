using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理班级
{
    [Subject(typeof(ClassController),"Delete")]
    public class 当提交删除班级时:数据准备工作
    {
           Establish context= () =>
           {
              var id=创建班级(Keys.Name);
              subject= Action<ClassController>(x => x.Delete(id));
           };
         Because of = () => subject.Invoke();
          It 应该成功删除班级 = () =>
          {
              var flag = repository.IsExisted(new Class.ByName(Keys.Name));
              flag.ShouldBeFalse();
          };
    }
}
