using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace MyFramework.Web
{
    public static class SelectHelper
    {
         

        public static IList<SelectListItem> ToSelectListItem<TSource, TValue, TText>(
            this IList<TSource> source,
            Func<TSource, TValue> value,
            Func<TSource, TText> text)
        {
            var list = new List<SelectListItem>();
            foreach (TSource item in source)
            {
                list.Add(new SelectListItem
                {
                    Text = text(item).ToString(),
                    Value = value(item).ToString()
                });
            }
            return list;
        }

        public static IList<SelectListItem> Selected(
            this IList<SelectListItem> list,
            string selectedValue)
        {
            foreach (SelectListItem item in list)
            {
                item.Selected = item.Value == selectedValue;
            }
            return list;
        }

        /// <summary>
        ///     将列表转成Json对象
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string ToJson(this IList<SelectListItem> items)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            if (items != null)
            {
                foreach (SelectListItem item in items)
                {
                    sb.AppendFormat("[\"{0}\",\"{1}\"]", item.Value, item.Text);
                    sb.Append(",");
                }
                if (sb.Length > 1)
                    sb.Remove(sb.Length - 1, 1); //去掉最后一个逗号
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}