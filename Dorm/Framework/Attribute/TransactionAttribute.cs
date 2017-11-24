using System;
using System.Web.Mvc;
using MyFramework.Application.Interface;
using MyFramework.Domain;
using MyFramework.Web;

namespace MyFramework.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string ControllerName = string.Format("{0}Controller",
                filterContext.RouteData.Values["controller"] as string);
            var ActionName = filterContext.RouteData.Values["action"] as string;
            Exception exception = filterContext.Exception;  
            filterContext.Controller.ViewData[FrameworkKeys.ErrorMessage] = exception.Message;
            filterContext.Controller.ViewData[FrameworkKeys.ErrorStackTrace] = exception.StackTrace;
            filterContext.ExceptionHandled = true;
            if (IoC.Get<IConfig>().GetValue("SoftIsRun")!="true")
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData
            };
            else
                filterContext.Result = new JavaScriptResult 
                {
                    Script = JavascriptHelper.Alert(exception.Message, null, AlertCategory.Error)
                }; 
           
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}