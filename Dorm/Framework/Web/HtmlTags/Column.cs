using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MyFramework.Web.HtmlTags
{
    public abstract class Column<T>
    {
        public  MvcHtmlString HeadExpression { get; protected set; }
        public  List<Expression<Func<T, MvcHtmlString>>> DataExpressions { get; protected set; }
        public  string FooterText { get; protected set; }
        public string HeaderRowStyle { get; protected set; }
        public string DataRowStyle { get; protected set; }
        public string FooterRowStyle { get; protected set; }
        public bool IsVisible { get; protected set; }
    }
}