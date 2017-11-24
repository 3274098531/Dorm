using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyFramework.Application.Interface;

namespace MyFramework.Attribute
{
     
        /// <summary>
        ///     权限验证
        /// </summary>
        public class AccountAuthAttribute : ActionFilterAttribute
        {
            public AccountAuthAttribute(object username)
            {
                UserName = username.ToString();
            }

            /// <summary>
            ///     角色名称
            /// </summary>
            public string UserName { get; set; }

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
                //如果存在身份信息
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var content = new ContentResult();
                    content.Content =
                        string.Format("<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>",
                            FormsAuthentication.LoginUrl);
                    filterContext.Result = content;
                } 
            }
        }
     
}
