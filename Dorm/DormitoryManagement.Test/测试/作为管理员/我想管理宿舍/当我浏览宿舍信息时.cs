using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理宿舍
{
    [Subject(typeof(DormController),"Index")]
  public  class 当我浏览宿舍信息时:数据准备工作 
    {
         
        private Establish context =
            () =>
            {
                创建宿舍楼("计算机");
                subject = Action<DormController>(x => x.Index(null, null));
            };

        private Because of = () => result = subject.Invoke();

        private It 应该成功浏览了宿舍楼列表 =
            () =>
            {
                var academies = ((ViewResult)result).ViewData.Model as IEnumerable<Dorm>;
                academies.Count().ShouldEqual(1);
            };
    }
}
