using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理宿舍
{
    [Subject(typeof(DormController),"Delete")]
    public class 当提交删除宿舍楼时:数据准备工作
    {
           Establish context= () =>
           {
            var id=创建学院(Keys.Name);
              subject= Action<DormController>(x => x.Delete(id));
           };
         Because of = () => subject.Invoke();
          It 应该成功删除宿舍楼 = () =>
          {
              var flag = repository.IsExisted(new Dorm.ByName(Keys.Name));
              flag.ShouldBeFalse();
          };
    }
}
