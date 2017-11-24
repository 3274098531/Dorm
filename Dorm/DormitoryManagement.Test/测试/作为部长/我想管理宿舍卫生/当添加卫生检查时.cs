using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister.Controllers;
using Machine.Specifications;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试.作为部长.我想管理宿舍卫生
{
    public class 当添加卫生检查时:数据准备工作
    {
        Establish context = () =>
        {
            创建房间("532");
            创建房间("332");
            创建房间("533");
            创建房间("534");
            创建房间("535");
            创建房间("536");
            创建房间("537");
            创建房间("538");
            创建房间("539");
            创建房间("232");
            创建房间("332");
            创建房间("433");
            FormCollection form = new FormCollection();
            form.Add(Keys.CheckTime,CheckTime.ToString());
            form.Add(Keys.Ratio,Ratio.ToString());
            form.Add(Keys.Name,Name);
            form.Add(Keys.InspectionType,InspectionType.RandCheck.Value());
            subject = Action<CheckDormController>(x => x.Create(form));
        };

        public static int Ratio = 2;

        public static DateTime CheckTime = DateTime.Now;

        public static string Name = "name";

        Because of = () => result = subject.Invoke();
        It 应该成功添加卫生检查 = () =>
        {
            var inspects =repository.GetOne(new ByName<Inspection>(Name));
            inspects.Ratio.ShouldEqual(Ratio);
            inspects.CheckTime.ToString().ShouldEqual(CheckTime.ToString());
            inspects.Type.ShouldEqual(InspectionType.RandCheck);
            var checks = repository.GetAll(new Check.ByInspection(inspects.Id.ToString()));
            checks.Count().ShouldEqual(2);

        };

    }
}
