using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyFramework.Domain
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumTextAttribute :System.Attribute
    {
        public EnumTextAttribute(string text)
        {
            EnumText = text;
        }

        public string EnumText { get; private set; } 
        public static string GetText(Type tp, string name)
        {
            MemberInfo[] mi = tp.GetMember(name);
            if (mi.Length > 0)
            {
                var attr =  GetCustomAttribute(mi[0], typeof(EnumTextAttribute)) as EnumTextAttribute;
                if (attr != null)
                {
                    return attr.EnumText;
                }
            }
            return name;
        }
        public static string GetText(object value)
        {
            if (value != null)
            {
                MemberInfo[] mi = value.GetType().GetMember(value.ToString());
                if (mi.Length > 0)
                {
                    var attr =  GetCustomAttribute(mi[0], typeof(EnumTextAttribute)) as EnumTextAttribute;
                    if (attr != null)
                    {
                        return attr.EnumText;
                    }
                }
                return value.ToString();
            }
            return null;
        }
    }
}
