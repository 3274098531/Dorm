using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using LinqSpecs;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理房间
{
    [Subject(typeof (RoomController), "Edit")]
    public class 当提交编辑房间时 : 数据准备工作
    {
        private static string 宿舍id;
        private static string 房间id;
        private Establish context = () =>
            {
                房间id = 创建房间("1234");
                宿舍id = 创建宿舍楼("2");
                string 学院id = 创建学院("23"); 
                var formCollection = new FormCollection();
                formCollection.Add(Keys.Name, "123");
                formCollection.Add(Keys.MaxBedNum,"6");
                formCollection.Add(Keys.Academy, 学院id);
                formCollection.Add(Keys.Dorm, 宿舍id);
                subject = Action<RoomController>(x => x.Edit(房间id, formCollection));
            };

        private Because of = () => subject.Invoke();

        private It 应该成功编辑房间 = () =>
            {
                var flag = repository.IsExisted(new ById<Room>(房间id));
                flag.ShouldBeTrue();
                var room = repository.GetOne(new ById<Room>(房间id));
                room.MaxBedNum.ShouldEqual(6);
            };
    }
}