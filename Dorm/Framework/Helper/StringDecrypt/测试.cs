using Machine.Specifications;

namespace MyFramework.Helper.StringDecrypt 
{
    [Subject(typeof(StringDecrypt), "ToEncode")]
    public class 当对字符串加密时
    {
        private static string str ;
        private static string ToEncodestr;
        Establish context = () =>
            {
                str = "liuwei";
            };
        Because of = () => ToEncodestr = str.ToEncode();
        It 应该加密成功 = () =>
            {
                ToEncodestr.ShouldNotEqual(str); 
            };
    }
    [Subject(typeof(StringDecrypt), "ToDecode")]
    public class 当对字符串解密时
    {
        private static string str;
        private static string ToDecodestr;
        Establish context = () =>
        {
            str = "liuwei".ToEncode();
        };
        Because of = () => ToDecodestr = str.ToDecode();
        It 应该解密成功 = () =>
        {
            ToDecodestr.ShouldNotEqual(str);
            ToDecodestr.ShouldEqual("liuwei");
        };
    }
}
