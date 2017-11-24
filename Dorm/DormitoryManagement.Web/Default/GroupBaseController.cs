using DormitoryManagement.Application;
using MyFramework.Application.Interface;
using MyFramework.Attribute;

namespace DormitoryManagement.Web.Default
{
    public class GroupBaseController : BaseController
    {
        protected IGroupService GroupService;

        public GroupBaseController(IGroupService groupService)
        {
            GroupService = groupService;
            GroupService.UserName = IoC.Get<IHttpService>().GetInlineAccountUserName();
        }
    }
}