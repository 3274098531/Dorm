﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyFramework.Domain;

namespace MyFramework.Helper
{
    public static class EnumExtensionMethods
    {
        public static string Text<TEnum>(this TEnum value) where TEnum : struct
        {
            return EnumTextAttribute.GetText(value);
        }

        public static string Value<TEnum>(this TEnum value) where TEnum : struct
        {
            return value.ToString();
        } 
        public static IList<SelectListItem> ToSelectListItems(this Type type)
        {
            var result = new List<SelectListItem>();
            foreach (var name in Enum.GetNames(type))
            {
                result.Add(new SelectListItem
                               {
                                   Value = name,
                                   Text = EnumTextAttribute.GetText(type, name)
                               });
            }
            return result;
        }


        public static IEnumerable<T> ToList<T>(this Type type)
        {
            var result = new List<T>();
            foreach (string name in Enum.GetNames(type))
            {
                result.Add((T) (Enum.Parse(type, name)));
            }
            return result;
        }

        public static T To<T>(this string value) where T : struct
        {
            foreach (var item in typeof (T).ToList<T>())
            {
                if (item.Value() == value||item.Text()==value)
                    return item;
            }
            return default(T);
        }
    }
}