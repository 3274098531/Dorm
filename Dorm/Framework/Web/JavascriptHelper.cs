using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFramework.Domain;

namespace MyFramework.Web
{
    public class JavascriptHelper
    {
        public static string Alert(string strongMessage, string message, AlertCategory alertCategory)
        {
            return string.Format("$('#{0}').prepend('{1}')",
                                FrameworkKeys.MainContent,
                                Html.Alert(null, strongMessage, message, alertCategory));
        }
    }
}
