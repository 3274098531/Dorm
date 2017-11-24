using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理房间
{
    [Subject(typeof(RoomController),"Delete")]
    public class 当提交删除房间时:数据准备工作
    {
        private static string id;
           Establish context= () =>
           {
             id=创建房间(Keys.Name);
              subject= Action<RoomController>(x => x.Delete(id));
           };
         Because of = () => subject.Invoke();
          It 应该成功删除房间 = () =>
          {
              var flag = repository.IsExisted(new ById<Room>(id));
              flag.ShouldBeFalse();
          };
    }
}
