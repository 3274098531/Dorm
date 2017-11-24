using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学院
{
    [Subject(typeof(AcademyController),"Edit")]
    public class 当提交编辑学院时:数据准备工作
    {
        Establish context= () =>
        {
              id = 创建学院(Keys.Name);
            FormCollection formCollection = new FormCollection();
            formCollection.Add(Keys.Name,"123");
            formCollection.Add(Keys.ShortName,"1234");
            subject = Action<AcademyController>(x => x.Edit(id, formCollection));
        };

        private Because of =()=> subject.Invoke();
        It 应该成功编辑学院 = () =>
        {
            var academy = repository.GetOne(new Academy.By(id));
            academy.ShortName.ShouldEqual("1234");
            academy.Name.ShouldEqual("123");
        };

        private static string id;
    }
}
