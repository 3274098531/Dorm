using System.Drawing.Imaging;

namespace MyFramework.Application.Interface
{
    public interface IAccountCommand:IBaseCommand
    {
        IAccountCommand PassWord(string password);
        IAccountCommand ResetPassWord(string password);
        IAccountCommand Email(string email);
        IAccountCommand AddRole(string id);
        IAccountCommand RemoveRole(string roleid);
        IAccountCommand Photo(byte[] photo);

        IAccountCommand IsEable();
        IAccountCommand RealName(string s); 
        IAccountCommand QQ(string qq);
        IAccountCommand Phone(string phone);

        IAccountCommand RemoveAllRole();
    }
}