using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;


namespace DormitoryManagement.Test.测试.作为管理员.我想管理学院
{
    [Subject(typeof(AcademyController),"Create")]
    public class 当提交添加学院时:数据准备工作 
    {
        Establish context = () =>
        {
            FormCollection form = new FormCollection();
            form.Add(Keys.Name,Keys.Name);
            form.Add(Keys.ShortName,Keys.ShortName);
            subject=Action<AcademyController>(x => x.Create(form));
        };
        Because of = () =>
        {
            subject.Invoke();
        };       
        It 应该成功添加 = () =>
        {
            var flag = repository.IsExisted(new Academy.ByName(Keys.Name));
             flag.ShouldBeTrue();
        };
    }
}
