using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理房间
{
    [Subject(typeof(RoomController),"InputRoom")]
   public class 当导入房间时:数据准备工作
    {
        Establish context = () =>
        {
            创建宿舍楼("3舍");
            创建宿舍楼("4舍");
            创建学院("计算机科学与技术学院");
            创建学院("外国语学院");
            subject = Action<RoomController>(x => x.InputFile(文件(content)));
        };
        Because of = () => subject.Invoke();
        It 应该导入成功 = () =>
        {
            IQueryable<Room> students = repository.GetAll<Room>();
            students.Select(x => x.Name).ShouldContainOnly("532", "440");
        };
        private static string content = @"学院,寝室楼,寝室规格,寝室类型,寝室号,备注
计算机科学与技术学院,3,8,男,532,无 
外国语学院,4,8,男,440,无";
    }
}
