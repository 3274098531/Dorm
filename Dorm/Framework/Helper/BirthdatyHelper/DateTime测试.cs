using System;
using Machine.Specifications;

namespace MyFramework.Helper.BirthdatyHelper
{
    [Subject(typeof (DateTimeHelper), "GetAge")]
    public class DateTime测试
    {
        private static DateTime _time;
        private static DateTime _time1;
        private static DateTime _time2;
        
        private static int _age;
        private static int _age1;
        private static int _age2;

        private static readonly int Year = DateTime.Now.Year;
        private static readonly int Day = DateTime.Now.Day;
        private static readonly int Mouth = DateTime.Now.GetMouth();

        private Establish _context = () =>
        {
            _time = new DateTime(Year, Mouth, Day - 1);
            _time1 = new DateTime(Year - 2, Mouth, Day);
            _time2 = new DateTime(Year - 2, Mouth + 1, Day);
        };

        private Because _of = () =>
        {
            _age = _time.GetAge();
            _age1 = _time1.GetAge();
            _age2 = _time2.GetAge();
        };

        private It _应该成功返回年龄 = () =>
        {
            _age.ShouldEqual(0);
            _age1.ShouldEqual(2);
            _age2.ShouldEqual(1);
        };
    }
    [Subject(typeof(DateTimeHelper),"ToPassWord")]
    public class ToPassWordTest
    {
        private static DateTime Time;
        private static string password;
        Establish context = () =>
        {
            Time = new DateTime(2016,1,1,1,1,1,1);
        }; 
        Because of = () =>
        {
            password = Time.ToPassWord();
        };
        It 应该返回正确的密码 = () => password.ShouldEqual("2016111");
    }
}