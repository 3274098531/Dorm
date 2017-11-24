using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理房间
{
    [Subject(typeof (RoomController), "Index")]
    internal class 当浏览房间列表时 : 数据准备工作
    {
       
        private Establish context =
            () =>
            {
                创建房间("532");
                subject = Action<RoomController>(x => x.Index(null,null));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了房间列表 =
            () =>
            {
                var rooms = AsModels<Room>(result);
                rooms.Count().ShouldEqual(1);
            };
    }
}