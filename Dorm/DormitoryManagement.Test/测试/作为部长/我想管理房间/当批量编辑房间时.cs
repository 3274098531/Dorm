using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为部长.我想管理房间
{
    public class 当批量编辑房间时:数据准备工作
    {
        private static string 宿舍id;
        private static string 房间id1;
        private static string 房间id2;
        private static string 房间id3;

        private Establish context = () =>
        {
           var 学院id = 创建学院("计算机");
            宿舍id = 创建宿舍楼("3舍");
            房间id1 = AdminService.CreateRoom().Name("532").Dorm(宿舍id).Academy(学院id).Id.ToString();
            房间id2 = AdminService.CreateRoom().Name("533").Dorm(宿舍id).Academy(学院id).Id.ToString();
            房间id3 = AdminService.CreateRoom().Name("534").Dorm(宿舍id).Academy(学院id).Id.ToString();
            var formCollection = new FormCollection();
            formCollection.Add(Keys.Room,"532,533,534");
            formCollection.Add(Keys.Dorm, 宿舍id);
            formCollection.Add(Keys.StartTime, StartTime.ToString());
            formCollection.Add(Keys.EndTime, EndTime.ToString());
            formCollection.Add(Keys.Remark, Remark);
            subject = Action<RoomController>(x => x.EditAll(formCollection));
        };

        public static DateTime StartTime=new DateTime(2016,1,1,1,1,1);
        public static DateTime EndTime=new DateTime(2016,1,5,1,1,1);

        public static string Remark = "实习寝室";

        Because of = () => subject.Invoke();

        It 应该成功编辑房间 = () =>
        {
            var rooms = repository.GetAll<Room>().ToList();
            rooms.Select(x=>x.StartTime).ShouldContainOnly(StartTime,StartTime,StartTime);
            rooms.Select(x=>x.EndTime).ShouldContainOnly(EndTime,EndTime,EndTime);
            rooms.Select(x=>x.Remark).ShouldContainOnly(Remark,Remark,Remark);
        };
    }
}
