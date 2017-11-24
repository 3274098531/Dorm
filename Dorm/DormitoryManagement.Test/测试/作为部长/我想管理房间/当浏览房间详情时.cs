using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部长.我想管理房间
{
    [Subject(typeof(RoomController), "Details")]
    internal class 当浏览房间详情时 : 数据准备工作
    {
        
        private static string id;
        private Establish context =
            () =>
            {
                id=创建房间("532");
                subject = Action<RoomController>(x => x.Details(id));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了房间列表 =
            () =>
            {
                var rooms = AsModel<Room>(result);
                 rooms.ShouldNotBeNull();
                rooms.Name.ShouldEqual("532");
            };
    }
}
