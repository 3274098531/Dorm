using System;
using System.IO;
using System.Web;
using System.Web.Security;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;

namespace MyFramework.Application.InterfaceAchieve
{
    [IocRegister]
    public class HttpService : IHttpService
    {
        public HttpCookie LogOn(Account account)
        {
            //保存身份信息
            var ticket = new FormsAuthenticationTicket(1, account.UserName, DateTime.Now,
                DateTime.Now.AddMinutes(10), false, string.Join(",", account.Roles));
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(ticket)); //加密身份信息，保存至Cookie 
            return cookie;
        }

        public void LogOut()
        {
            
            FormsAuthentication.SignOut();
        }

        public byte[] ChangePhoto(HttpPostedFileBase file)
        {
            Stream stream = file.InputStream;
            var content = new byte[stream.Length];
            stream.Read(content, 0, content.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return content;
        }

        public bool IsTimeout()
        {
            return HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] == null;
        }

        public string GetInlineAccountUserName()
        { 
            if (!HttpContext.Current.Request.IsAuthenticated) return null;
            return HttpContext.Current.User.Identity.Name;
        }
    }
}