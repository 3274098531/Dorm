using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyFramework.Application.Interface;
using MyFramework.Attribute;

namespace DormitoryManagement.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
      //          new {controller = "Account", action = "LogOn", id = UrlParameter.Optional} ,// 参数默认值
                 new {controller = "Default", action = "Index", id = UrlParameter.Optional}, // 参数默认值
                 new[] {"DormitoryManagement.Web.Controllers"}
                );

        }

        protected void Application_Start()
        {
            IoC.Get<IUnitOfWorks>().Prepare(); 
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
        }
         
        protected void Application_EndRequest(object sender, EventArgs s)
        {
            if (Request.CurrentExecutionFilePathExtension == "" || Request.CurrentExecutionFilePathExtension == ".aspx")
            {
                IoC.Get<IUnitOfWorks>().UnBindContext();
            }
        }
    }
}