using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;

namespace DormitoryManagement.Web.Default
{
    [RoleAuth(Keys.Member)]
    public class MemberBaseContorller : BaseController 
    {
        protected IMemberService MemberService;
        public MemberBaseContorller(IMemberService menberService)
        {
            MemberService = menberService;
            MemberService.UserName = IoC.Get<IHttpService>().GetInlineAccountUserName();
        }
    }
}