using System.Text;
using System.Web.Mvc;
using DormitoryManagement.Domain;

namespace DormitoryManagement.Web.Default.Helps
{
    public static class Helps
    {
        public static MvcHtmlString Dondown(this HtmlHelper htmlHelper )
        {
            var sb = new StringBuilder();
            
            sb.AppendLine(
                " <button type=\"button\" class=\"btn btn-success dropdown-toggle glyphicon glyphicon-save\" onclick = \"return alert(\'下载先要在右侧输入框输入需要下载的检查名称，默认为All\')\" id=\"dropdownMenu1\" data-toggle=\"dropdown\">下载");
            sb.AppendLine("<span class=\"caret\"></span>");
            sb.AppendLine("</button>");

            sb.AppendLine("<ul class=\"dropdown-menu\" role=\"menu\"  >");
            sb.AppendLine("<li  >");
            sb.AppendLine(
                "<a  class=\"btn btn-default glyphicon glyphicon-save\"  id=\"exporttoacademy\" \">学院反馈表</a>");
            sb.AppendLine("</li>");
            sb.AppendLine("<li >");
            sb.AppendLine(
                "<a    class=\"btn btn-default glyphicon glyphicon-save\" id=\"exporttocheck\" \">检查反馈表</a>");
            sb.AppendLine("</li>");

            sb.AppendLine("<li   >");
            sb.AppendLine(
                "<a   class=\"btn btn-default glyphicon glyphicon-save\" id=\"check\" \">卫生抽查表</a>");
            sb.AppendLine("</li>");

            sb.AppendLine("<li   >");
           
            sb.AppendLine(
                "<a  class=\"btn btn-default glyphicon glyphicon-save\" id=\"exporttochecksank\" \">检查排名表</a>");
            sb.AppendLine("</li>");
            sb.AppendLine("</ul>");
          
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}