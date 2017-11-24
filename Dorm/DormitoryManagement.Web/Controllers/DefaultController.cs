using System.Configuration;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;

namespace DormitoryManagement.Web.Controllers
{
    public class DefaultController : Controller
    {
        public DefaultController(IAdminService adminService, IMinisterService ministerService,
            IFrameworkService frameworkService )
        {
            FrameworkService = frameworkService;
            
            MinsService = ministerService;
            AdminService = adminService;
        }

         

        public IAdminService AdminService { get; set; }
        public IMinisterService MinsService { get; set; }

        public IFrameworkService FrameworkService { get; set; }

        public ActionResult Index(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
                ViewData[Keys.Message] = message;
            return View("Index");
        }


        public ActionResult Clear()
        {
            using (var dormitoryManagementEntities=new DormitoryManagementEntities())
            {
                dormitoryManagementEntities.InitDatebase();
            }
            return RedirectToAction("Index", new {message = "清空成功"});
        }


        public ActionResult InitData()
        { 

            AdminService.CreateAcademy().Name("文史学院").ShortName("文史学院").Num(1);
            AdminService.CreateAcademy().Name("数学与计算科学学院").ShortName("数计学院").Num(2);
            AdminService.CreateAcademy().Name("国际学院").ShortName("国际学院").Num(3);
            AdminService.CreateAcademy().Name("经济与管理学院").ShortName("经管学院").Num(4);
            AdminService.CreateAcademy().Name("生命与环境科学学院").ShortName("生科学院").Num(5);
             
            AdminService.CreateAcademy().Name("资源环境与旅游学院").ShortName("资旅学院").Num(6);
            AdminService.CreateAcademy().Name("法学院").ShortName("法学院").Num(7);
            AdminService.CreateAcademy().Name("机械工程学院").ShortName("机械学院").Num(8);
            AdminService.CreateAcademy().Name("外国语学院").ShortName("外国语学院").Num(9);
            AdminService.CreateAcademy().Name("计算机科学与技术学院").ShortName("计算机学院").Num(10);
            AdminService.CreateAcademy().Name("数学与计算科学学院").ShortName("数计学院").Num(2);
            AdminService.CreateAcademy().Name("物理与电子科学学院").ShortName("物电学院").Num(11);
            AdminService.CreateAcademy().Name("电气与信息工程学院").ShortName("电气学院").Num(12);
            AdminService.CreateAcademy().Name("化学化工学院").ShortName("化工学院").Num(13); 
            AdminService.CreateAcademy().Name("艺术表演与传媒学院").ShortName("艺传学院").Num(14);
            AdminService.CreateAcademy().Name("土木建筑工程学院").ShortName("土建学院").Num(15);
            
            AdminService.CreateAcademy().Name("体育学院").ShortName("体育学院").Num(16);
            AdminService.CreateAcademy().Name("美术学院").ShortName("美术学院").Num(17);

             
            AdminService.CreateDorm().Name("3舍").Type("男");
            AdminService.CreateDorm().Name("4舍").Type("女");
            AdminService.CreateDorm().Name("5舍").Type("男");
            AdminService.CreateDorm().Name("6舍").Type("女");
            AdminService.CreateDorm().Name("7舍").Type("男");
            AdminService.CreateDorm().Name("8舍").Type("女");
            AdminService.CreateDorm().Name("9舍").Type("男");
            AdminService.CreateDorm().Name("10舍").Type("女");
            AdminService.CreateDorm().Name("11舍").Type("女");
            AdminService.CreateDorm().Name("12舍").Type("男");
            AdminService.CreateDorm().Name("13舍").Type("女");
            AdminService.CreateDorm().Name("14舍").Type("女");
            AdminService.CreateDorm().Name("15舍").Type("男");
            AdminService.CreateDorm().Name("16舍").Type("女");
            AdminService.CreateDorm().Name("17舍").Type("女");
            AdminService.CreateDorm().Name("19舍").Type("女");
            AdminService.CreateDorm().Name("20舍").Type("女"); 
           //初始权限
            string adminsid =FrameworkService.CreateRole(1).Name(Roles.Admin.Value()).Discription(Roles.Admin.Text()).Id.ToString();
            string rootid =FrameworkService.CreateRole(0) .Name(FrameworkRole.Root.Value()) .Discription(FrameworkRole.Root.Text()) .Id.ToString();
            FrameworkService.CreateRole(3).Name(Roles.Member.Value()).Discription(Roles.Member.Text());
            FrameworkService.CreateRole(2).Name(Roles.Minister.Value()).Discription(Roles.Minister.Text());
            
            FrameworkService.CreateRole(4).Name(Roles.Group.Value()).Discription(Roles.Group.Text());
            //初始账户
            FrameworkService.CreateAccount("admin")
                .AddRole(adminsid)
                .PassWord("liuwei")
                .IsEable().Email("824359014@qq.com");
            FrameworkService.CreateAccount(FrameworkRole.Root.Value()).AddRole(rootid)
                .IsEable().Email("3274098531@qq.com");
            FrameworkService.CreateAccount("Administrator")
                .AddRole(adminsid)
                .PassWord("123456")
                .IsEable().Email("");
            

            return RedirectToAction("Index", new {message = "初始化成功"});
        }

        public ActionResult InitTest()
        {
            string password = IoC.Get<IConfig>().GetValue(Keys.DefaultPassWord);
            string ministerid =
                FrameworkService.GetRoleByName(Roles.Minister.Value()) 
                    .Id.ToString(); 
            string memberid =
                FrameworkService.GetRoleByName(Roles.Member.Value()) .Id.ToString();

             
            string groupid =
                FrameworkService.GetRoleByName(Roles.Group.Value()) .Id.ToString();
            FrameworkService.CreateAccount("201501020139")
                .PassWord(password)
                .AddRole(ministerid)
                .Email("912398189@qq.com")
                .IsEable();
            FrameworkService.CreateAccount("201517020106")
                .PassWord(password)
                .AddRole(memberid)
                .Email("879052547@qq.com")
                .IsEable();
            FrameworkService.CreateAccount("201517010235")
                .PassWord(password)
                .AddRole(groupid)
                .Email("2232149342@qq.com")
                .IsEable();

            FrameworkService.CreateAccount("201514020209")
                .Email("2458568037@qq.com")
                .PassWord(password)
                .AddRole(ministerid)
                .IsEable();

            FrameworkService.CreateAccount("201501030211")
                .Email("591342914@qq.com")
                .PassWord(password)
                .AddRole(ministerid)
                .IsEable();
            

           
            FrameworkService.CreateAccount("201514040119")
                .Email("1769043742@qq.com").PassWord(password)
                .AddRole(ministerid).IsEable();
            
            string 经济与管理学院id = AdminService.GetAcademyByName("经济与管理学院").Id.ToString();
            string 计算机科学与技术学院id = AdminService.GetAcademyByName("计算机科学与技术学院").Id.ToString();
            string 资源环境与旅游学院id = AdminService.GetAcademyByName("资源环境与旅游学院").Id.ToString();
            string 三舍id = AdminService.GetDormByName("3舍").Id.ToString();
            string 十三舍id = AdminService.GetDormByName("13舍").Id.ToString();

            string 市场营销15101Id = AdminService.CreateClass().Name("市场营销15101班").Academy(经济与管理学院id).Id.ToString();
            string 信管15101Id = AdminService.CreateClass().Name("信管15101班").Academy(计算机科学与技术学院id).Id.ToString();
            string 计科15102Id = AdminService.CreateClass().Name("计科15102班").Academy(计算机科学与技术学院id).Id.ToString();
            string 地信15101Id = AdminService.CreateClass().Name("地信15101班").Academy(资源环境与旅游学院id).Id.ToString();
            string 旅管15102Id = AdminService.CreateClass().Name("旅管15102班").Academy(资源环境与旅游学院id).Id.ToString();
            string 会计15102Id = AdminService.CreateClass().Name("会计15102班").Academy(经济与管理学院id).Id.ToString();
            

            string 三舍532Id =
                AdminService.CreateRoom().Name("532").Dorm(三舍id).Academy(计算机科学与技术学院id).MaxBedNum("8").Id.ToString();
            
            string 三舍530Id =
                AdminService.CreateRoom().Name("530").Dorm(三舍id).Academy(计算机科学与技术学院id).MaxBedNum("8").Id.ToString();
            string 十三舍117Id =
                AdminService.CreateRoom().Name("117").Dorm(十三舍id).Academy(经济与管理学院id).MaxBedNum("4").Id.ToString();
            string 十三舍143Id =
                AdminService.CreateRoom().Name("143").Dorm(十三舍id).Academy(经济与管理学院id).MaxBedNum("4").Id.ToString();

            string 十三舍231Id =
                AdminService.CreateRoom().Name("231").Dorm(十三舍id).Academy(资源环境与旅游学院id).MaxBedNum("4").Id.ToString();
            string 十三舍303Id =
                AdminService.CreateRoom().Name("303").Dorm(十三舍id).Academy(资源环境与旅游学院id).MaxBedNum("4").Id.ToString();


            AdminService.CreateStudent("201501020139").Name("周归兰").Sex("女")
                .Class(市场营销15101Id).Room(十三舍117Id).Discipline("无");
            AdminService.CreateStudent("201517010235").Name("许伟聪").Sex("男")
                .Class(计科15102Id).Room(三舍530Id).Discipline("无");
            AdminService.CreateStudent("201514020209").Name("何青珍").Sex("女")
                .Class(旅管15102Id).Room(十三舍303Id).Discipline("无");
            AdminService.CreateStudent("201501030211").Name("李振红").Sex("女")
                .Class(会计15102Id).Room(十三舍143Id).Discipline("无");
            AdminService.CreateStudent("201514040119").Name("罗婷云").Sex("女")
                .Class(地信15101Id).Room(十三舍231Id).Discipline("无");
            AdminService.CreateStudent("201517020119").Name("刘伟").Sex("男")
                .Class(信管15101Id).Room(三舍532Id).Discipline("无");
            AdminService.CreateStudent("201517020106").Name("顾清煜").Sex("男")
                .Class(信管15101Id).Room(三舍532Id).Discipline("无");
            IoC.Get<IUnitOfWorks>().UnBindContext();
            MinsService.CreateMenber("201501020139")
                
                .Email("912398189@qq.com")
                .Position(Position.Minister.ToString());
            MinsService.CreateMenber("201517010235")
                 
                .Phone("")
                .Email("2232149342@qq.com")
                .Position(Position.Minister.ToString());
            MinsService.CreateMenber("201514020209")
                 
                .Email("2458568037@qq.com")
                .Position(Position.Minister.ToString());
            MinsService.CreateMenber("201517020106")
                
                .Email("879052547@qq.com")
                .Position(Position.Minister.ToString());
            MinsService.CreateMenber("201514040119")
                .Email("1769043742@qq.com")
                
                .Position(Position.Minister.ToString());
            MinsService.CreateMenber("201501030211")
               
                .Email("591342914@qq.com")
                .Position(Position.Minister.ToString());

            return RedirectToAction("Index", new {message = "初始化测试数据成功"});
        }
    }
}