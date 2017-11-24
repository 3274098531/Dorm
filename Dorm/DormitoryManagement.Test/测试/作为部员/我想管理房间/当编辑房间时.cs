using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Member.Controllers;
using Machine.Specifications;
using MyFramework.Helper;
using RoomController = DormitoryManagement.Web.Areas.Minister.Controllers.RoomController;

namespace DormitoryManagement.Test.测试.作为部员.我想管理房间
{
    [Subject(typeof(Web.Areas.Minister.Controllers.RoomController), "Edit")]
    public class 当编辑房间时 : 数据准备工作
    {
        
        private static string 房间id;
        private Establish context = () =>
        {
            房间id = 创建房间("1234");
            var formCollection = new FormCollection();
            formCollection.Add(Keys.Name, "123");
            formCollection.Add(Keys.AbleCheck, "dfd");
            formCollection.Add(Keys.MaxBedNum, "6");
            formCollection.Add(Keys.Remark, "sdfsdfsd");
            subject = Action<RoomController>(x => x.Edit(房间id, formCollection));
        };

        Because of = () => subject.Invoke();

        It 应该成功编辑房间 = () =>
        {
            var flag = repository.IsExisted(new ById<Room>(房间id));
            flag.ShouldBeTrue();
            var room = repository.GetOne(new ById<Room>(房间id));
            room.Name.ShouldEqual("123");
            room.MaxBedNum.ShouldEqual(6);
        };
    }
}
