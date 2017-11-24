using System.Linq;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理房间
{
    [Subject(typeof(RoomController),"Create")]
    public class 当提交添加房间时:数据准备工作 
    {
        Establish context = () =>
        {
           var id= 创建学院("1324");
            var 宿舍id = 创建宿舍楼("三舍");
            FormCollection form = new FormCollection();
            form.Add(Keys.Name,Keys.Name);
            form.Add(Keys.Academy,id);
            form.Add(Keys.Dorm,宿舍id);
            form.Add(Keys.MaxBedNum,"5");
            subject=Action<RoomController>(x => x.Create(form));
        };
        Because of = () => subject.Invoke();       
        It 应该成功添加班级 = () =>
        {
            var room = repository.GetAll(new Room.ByDormName("三舍")).FirstOrDefault(x => x.Name == Keys.Name);

             room.ShouldNotBeNull();
            room.MaxBedNum.ShouldEqual(5);
            room.Name.ShouldEqual(Keys.Name);
            
        };
    }
}
