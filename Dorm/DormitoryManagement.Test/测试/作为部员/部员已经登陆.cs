using Machine.Specifications;

namespace DormitoryManagement.Test.测试.作为部员
{

    public class 部员已经登陆:数据准备工作
    {
        Establish context = () =>
        {
            创建部员(UserName);
            创建部员账号(UserName,PassWord);
            账号登陆(UserName,PassWord);
        };

        protected static string UserName = "201517020119";
        protected static string PassWord = "1234";

    }
}
