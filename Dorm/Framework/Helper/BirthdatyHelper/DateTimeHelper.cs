using System;

namespace MyFramework.Helper.BirthdatyHelper
{
    public static class DateTimeHelper  
    {
        public static int GetDay(this DateTime time)
        {
            return time.Day;
        }

        public static int GetMouth(this DateTime time)
        {
            return time.Month;
        }

        public static int GetYear(this DateTime time)
        {
            return time.Year;
        }

        public static int GetAge(this DateTime birthdayTime)
        {
            DateTime now = DateTime.Now;
            int age = DateTime.Now.Year - birthdayTime.Year;
            if (now.Month == birthdayTime.Month)
                age = (now.Day < birthdayTime.Day) ? age - 1 : age;
            else if (now.Month < birthdayTime.Month)
                age = age - 1;
            return age;
        }

        public static DateTime InitTime(this DateTime time)
        {
            return new DateTime(1753, 1, 1, 12, 0, 0);
        }

        public static string ToPassWord(this DateTime time)
        {

            return time.Year + "" + time.Month + "" + time.Day + "" + time.Hour;
        }
    }
}