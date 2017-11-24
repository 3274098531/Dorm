using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学生
{
    [Subject(typeof(StudentController),"Delete")]
    public class 当提交删除学生时:数据准备工作
    {
        private static string id;
           Establish context= () =>
           {
             id=创建学生(Keys.Code);
              subject= Action<StudentController>(x => x.Delete(id));
           };
         Because of = () => subject.Invoke();
          It 应该成功删除学生 = () =>
          {
              var flag = repository.IsExisted(new ById<Student>(id));
              flag.ShouldBeFalse();
          };
    }
}
