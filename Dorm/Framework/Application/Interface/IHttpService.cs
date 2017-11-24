using System.Web;
using MyFramework.Domain;

namespace MyFramework.Application.Interface
{
    public interface IHttpService
    {
        string GetInlineAccountUserName();
        HttpCookie LogOn(Account account);
        void LogOut();
        byte[] ChangePhoto(HttpPostedFileBase file);
        bool IsTimeout();
    }
}