using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyFramework.Attribute
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController) IoC.Current.Resolve(controllerType, null);
        }
    }
}