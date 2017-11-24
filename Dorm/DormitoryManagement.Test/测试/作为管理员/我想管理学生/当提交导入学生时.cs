using System.Linq;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Admin.Controllers;
using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为管理员.我想管理学生
{
    [Subject(typeof (StudentController), "Import")]
    public class 当批量导入学生时 : 数据准备工作
    {
        
         

        private Establish context =
            () =>
            {
                创建班级("信管15101班");
                创建班级("信管15102班");
                创建班级("信管15103班");
                创建房间("3舍", "532");
                subject = Action<StudentController>(x => x.InputFile(文件(content)));
            };

        private Because of = () => subject.Invoke();

        private It 应该成功导入了学生信息 =
            () =>
            {
                IQueryable<Student> students = repository.GetAll<Student>();
                students.Select(x => x.Code).ShouldContainOnly("201517020119", "201517020110", "201517020111");
            };
 static string content = @"学院,班级,姓名,学号,性别,宿舍楼,房间,违纪情况
计算机技术与科学学院,信管15101班,刘伟,201517020119,男,3舍,532,无
计算机技术与科学学院,信管15102班,张三,201517020110,男,3舍,532,无
计算机技术与科学学院,信管15103班,李四,201517020111,男,3舍,532,无";
    }
}