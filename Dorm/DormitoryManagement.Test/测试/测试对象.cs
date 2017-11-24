using System.Data.Entity;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using Machine.Specifications;
using Microsoft.Practices.Unity;
using MyFramework.Application.Interface;
using MyFramework.Application.InterfaceAchieve;
using MyFramework.Attribute;
using MyFramework.Helper;
using MyFramework.Web.测试;
using Rhino.Mocks;

namespace DormitoryManagement.Test.测试
{
    public class 测试对象 : BaseActionSpec
    {
        protected static string 部员权限id;
        protected static string 组员权限id;

        protected Establish WorkstageManageContext =
            () =>
            { 
                AdminService = A<IAdminService>();
                GroupService = A<IGroupService>(); 
                MinisterService = A<IMinisterService>();
                FrameworkService = A<IFrameworkService>();
                 HttpService = MockRepository.GenerateMock<IHttpService>();
                IoC.Current.RegisterInstance(typeof (IHttpService), HttpService); 
                FrameworkService.CreateRole(2).Name(Roles.Minister.Value()).Discription(Roles.Minister.Text());
                部员权限id =FrameworkService.CreateRole(3)
                        .Name(Roles.Member.Value())
                        .Discription(Roles.Member.Text())
                        .Id.ToString();
                 
                组员权限id =FrameworkService.CreateRole(4)
                        .Name(Roles.Group.Value())
                        .Discription(Roles.Group.Text())
                        .Id.ToString();
                FrameworkService.CreateRole(1).Name(Roles.Admin.Value()).Discription(Roles.Admin.Text());
            };

        public static IGroupService GroupService { get; set; } 
        public static string 寝室长权限id { get; set; } 
       
        public static void 账号登陆(string username,string password)
        {
            HttpService.Stub(x => x.GetInlineAccountUserName())
                .Return(username);
        }
        public static IFrameworkService FrameworkService { get; set; }
        public static IMinisterService MinisterService { get; set; }
        public static IAdminService AdminService { get; set; }
        public static IHttpService HttpService { get; set; }
    }
}