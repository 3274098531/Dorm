using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyFramework.Application;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using Rhino.Mocks;

namespace MyFramework.Web.测试
{
    public class ActionSubject<TController> : IActionSubject
        where TController : Controller
    {
        private readonly Expression<Func<TController, ActionResult>> action;
        private readonly TController controller;

        public ActionSubject(TController controller, Expression<Func<TController, ActionResult>> action)
        {
            this.controller = controller;
            this.action = action;
        }

        public ActionResult Invoke()
        {
            ActionResult result = action.Compile().Invoke(controller);
            IoC.Get<IUnitOfWorks>().UnBindContext();
            return result;
        } 
    }
}