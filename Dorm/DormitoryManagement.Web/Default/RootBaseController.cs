using System.Web.Mvc;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;

namespace DormitoryManagement.Web.Default
{
    [AccountAuth(FrameworkRole.Root)]
    [Authorize]
    [Transaction] 
    public class RootBaseController : Controller
    {
        protected IFrameworkService GroupService;

        public RootBaseController(IFrameworkService groupService)
        {
            GroupService = groupService;
        }
    }
}