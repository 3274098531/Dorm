using System.EnterpriseServices;
using System.Web.Mvc;

namespace MyFramework.Attribute
{

    [Authorize]
    [Transaction] 
    [Log]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")] 
    public abstract class BaseController : Controller 
    {
     
    }
}