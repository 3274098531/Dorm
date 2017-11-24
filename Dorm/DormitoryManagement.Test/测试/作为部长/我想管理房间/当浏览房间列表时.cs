using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using RoomController = DormitoryManagement.Web.Areas.Minister.Controllers.RoomController;

namespace DormitoryManagement.Test.测试.作为部长.我想管理房间
{
    [Subject(typeof(RoomController), "Index")]
    internal class 当浏览房间列表时 : 数据准备工作
    {
         
        private Establish context =
            () =>
            {
                创建房间("532");
                创建房间("536");
                subject = Action<RoomController>(x => x.Index(null, null));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了房间列表 =
            () =>
            {
                var rooms = AsModels<Room>(result);
                rooms.Count().ShouldEqual(2);
            };
    }
}
