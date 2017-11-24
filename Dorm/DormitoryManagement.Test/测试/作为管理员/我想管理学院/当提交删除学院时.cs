using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;
using Rhino.Mocks.Constraints;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学院
{
    [Subject(typeof(AcademyController),"Delete")]
    public class 当提交删除学院时:数据准备工作
    {
           Establish context= () =>
           {
            var id=创建学院(Keys.Name);
              subject= Action<AcademyController>(x => x.Delete(id));
           };
         Because of = () => subject.Invoke();
          It 应该成功删除学院 = () =>
          {
              var flag = repository.IsExisted(new Academy.ByName(Keys.Name));
              flag.ShouldBeFalse();
          };
    }
}
