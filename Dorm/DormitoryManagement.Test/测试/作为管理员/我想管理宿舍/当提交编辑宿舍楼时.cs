using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理宿舍
{
    [Subject(typeof(DormController),"Edit")]
    public class 当提交编辑宿舍楼时:数据准备工作
    {
        Establish context= () =>
        {
              id =创建宿舍楼(Keys.Name);
            FormCollection formCollection = new FormCollection();
            formCollection.Add(Keys.Name,"123");
            formCollection.Add(Keys.Type,Sex.Mam.ToString());
            subject = Action<DormController>(x => x.Edit(id, formCollection));
        };

        private Because of =()=> subject.Invoke();
        It 应该成功编辑学院 = () =>
        {
            var academy = repository.GetOne(new Dorm.By(id));
            academy.Type.ShouldEqual(Sex.Mam);
            academy.Name.ShouldEqual("123");
        };

        private static string id;
    }
}
