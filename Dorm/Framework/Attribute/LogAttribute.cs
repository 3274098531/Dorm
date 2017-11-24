using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MyFramework.Application.Interface;
using MyFramework.Domain;

namespace MyFramework.Attribute
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var service = IoC.Get<IFrameworkService>();
                service.CreateRequestLog(filterContext.HttpContext.Session.SessionID)
                       .Url(filterContext.HttpContext.Request.Url.ToString())
                       .Time(DateTime.Now)
                       .HttpMethod(filterContext.HttpContext.Request.HttpMethod)
                       .UserIp(filterContext.HttpContext.Request.UserHostAddress)
                       .UserName(filterContext.HttpContext.User.Identity.Name)
                       .ActionParameters(filterContext.ActionParameters.ToJson());
            }
        } 
        
    }
}
