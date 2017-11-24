using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyFramework.Application.Interface;
using MyFramework.Domain;

namespace MyFramework.Attribute
{
    /// <summary>
    ///     权限验证
    /// </summary>
    public class RoleAuthAttribute : ActionFilterAttribute
    {
        public RoleAuthAttribute(object role)
        {
            Role = role.ToString();
        }

        /// <summary>
        ///     角色名称
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        ///     验证权限（action执行前会先执行这里）
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IoC.Get<IHttpService>().IsTimeout())
            {
                var content = new ContentResult();
                content.Content =
                    string.Format(
                        "<script type='text/javascript'>alert('登陆超时，请重新登陆');window.location.href='{0}';</script>",
                        "/Account/LogOff");
                filterContext.Result = content;
                return;
            }
            Account account = IoC.Get<IFrameworkService>().GetCurrentAccount();
         
            //如果不存在身份信息
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var content = new ContentResult();
                content.Content =
                    string.Format("<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>",
                        FormsAuthentication.LoginUrl);
                filterContext.Result = content;
            }
            else
            {
                List<string> role = account.Roles.Select(x => x.Name).ToList(); //获取所有角色
                if (role.Contains(Role)) return;
                //验证不通过
                var content = new ContentResult();
                content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
                filterContext.Result = content;
            }
        }
    }
}