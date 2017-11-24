using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using MyFramework.Domain;
using MyFramework.Web.HtmlTags;
using MyFramework.Web.Page;

namespace MyFramework.Web
{
    public static class Html
    {
        public static MvcHtmlString Table<T>(this HtmlHelper htmlHelper, IEnumerable<T> dataSource,
            params Expression<Func<Table<T>, Column<T>>>[] columns)
        {
            Table<T> table = new Table<T>(htmlHelper)
                .Class("table table-striped table-bordered table-hover dataTable no-footer")
                .DataSource(dataSource);
            foreach (var column in columns)
            {
                column.Compile()(table);
            }
//            return MvcHtmlString.Create("<div class=\"table-responsive\">" + table+ "</div>");
            return MvcHtmlString.Create(table.ToString());
        }

        public static MvcHtmlString InputText(this HtmlHelper htmlHelper, string label, MvcHtmlString control1,
            string @clss = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<div class=\"form-group\" id = \"InputText\" name=\"{0}\">", label).AppendLine();
            sb.AppendLine("<label class=\"col-sm-2 control-label\">");
            sb.AppendFormat("{0}", label).AppendLine();
            sb.AppendLine("</label>");
            sb.AppendLine("<div class = \"col-sm-4\">");
            sb.AppendFormat("          {0}", control1).AppendLine();
            if (@clss != null)
            {
                sb.AppendFormat("<a><i class=\"{0}\"></i></a>", @clss).AppendLine();
            }
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");


            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ModelButton(this HtmlHelper html,string name,string id)
        {
            return
                MvcHtmlString.Create(
                    String.Format("<button id=\"ModelButton{0}\" class=\"btn btn-default\" style=\"display: inline\" onclick=\"document.getElementById('ModelForm{0}').style.display='block'\">{1}</button> ", id, name));
        }
        public static MvcHtmlString Model(this HtmlHelper html, string id, string Url,string buttonvalue, params MvcHtmlString[] mvc)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<form action={0} data-pjax=\"\" id=\"ModelForm{1}\" method=\"post\" style=\"display:none\">", Url, id);
            foreach (var mvcHtmlString in mvc)
            {
                sb.AppendFormat("{0}", mvcHtmlString).AppendLine();
            }
             sb.AppendLine("<div class=\"col-sm-offset-3 col-sm-9\">");
             sb.AppendFormat("<button type=\"submit\" name='Search' class=\"btn btn-success\">{0}</button>",buttonvalue);
             sb.AppendFormat(" <button type=\"button\" class=\"btn btn-default\" onclick=\"document.getElementById('ModelForm{0}').style.display ='none'; \">取消</button>",id);
            sb.AppendLine("</div></form>");
            return MvcHtmlString.Create(sb.ToString());
        }
        public static MvcHtmlString Backbutton(this HtmlHelper htmlHelper, string url = null)
        {
            if (url != null)
            {
                return htmlHelper.Button("返回", url);
            }
            var sb = new StringBuilder();
            sb.AppendLine(
                " <a onclick=\"history.go(-1)\" class=\"btn btn-default glyphicon glyphicon-share-alt\">返回</a>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ConfirmButton(this HtmlHelper html, string name, string url, string info)
        {
            return
                MvcHtmlString.Create(
                    string.Format("<a  data-post-pjax=\"\"   href={0} onclick=\"return confirm('{1}？')\">{2}</a>", url,
                        info, name));
        }

        public static MvcHtmlString CreateButton(this HtmlHelper html, string url)
        {
            return
                MvcHtmlString.Create(
                    string.Format(
                        "<a href={0} data-pjax = \"\" class=\"btn btn-default glyphicon glyphicon-plus\">添加</a>", url));
        }

        public static MvcHtmlString Button(this HtmlHelper html, string name, string url, string @class)
        {
             
            return MvcHtmlString.Create(
                string.Format(
                    "<a href={0} data-pjax = \"\" class=\"btn btn-{1}\" >{2}</a>", url, @class , name));
        }
        public static MvcHtmlString PostButton(this HtmlHelper html, string name, string url, string @class )
        {
             
            return MvcHtmlString.Create(
                string.Format(
                    "<a href={0} data-post-pjax = \"\" class=\"btn btn-{1}\" >{2}</a>", url, @class, name));
        }
        public static MvcHtmlString PostButton(this HtmlHelper html, string name, string url )
        {
            
            return MvcHtmlString.Create(
                string.Format(
                    "<a href={0} data-post-pjax = \"\"   >{1}</a>", url,  name));
        }
        public static MvcHtmlString Button(this HtmlHelper html, string name, string url)
        {
            return
                MvcHtmlString.Create(
                    string.Format(
                        "<a href={0} data-pjax = \"\"  >{1}</a>", url, name));
        }

        public static MvcHtmlString Submit(this HtmlHelper htmlHelper, MvcHtmlString button = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"col-sm-offset-3 col-sm-9\">");
            sb.AppendLine("<button type=\"submit\" class=\"btn btn-success\"><i class=\"fa fa-save\"></i>提交</button>");
            if (button == null)
            {
                sb.AppendLine(
                    "<input type=\"button\" value = \"返回\" class=\"btn btn-default glyphicon glyphicon-share-alt\" style=\"margin-left: 10px;\" onclick=\"history.go(-1)\"/>");
            }
            else
            {
                sb.AppendFormat("   {0}", button).AppendLine();
            }
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }


        public static MvcHtmlString File(this HtmlHelper html,string name, string url )
        {
            var id = Guid.NewGuid();
            var sb = new StringBuilder();
            sb.AppendFormat(
               "<button type=\"button\" class=\"btn btn-success\" onclick=\"document.getElementById('{1}').click();\">{0}</button>", name, id);
            var buttonsubmitid = Guid.NewGuid();
            sb.AppendFormat(
                "<form action={0} data-pjax  style=\"display: none\"  enctype=\"multipart/form-data\"  name={1}  id=\"FormId\" class=\"form-horizontal\" method=\"post\">",
                url,id).AppendLine();
            sb.AppendFormat(
               "<button type=\"submit\" id={0}></button>", buttonsubmitid);
            sb.AppendLine("<div class=\"col-sm-offset-3 col-sm-9\">");
            sb.AppendFormat("<input type=\"file\" name=\"file\" id=\"{0}\" style =\"display: none\"/>",id);
           
            sb.AppendLine("</div></form>");
            sb.AppendLine(string.Format("<script type=\"text/javascript\"> $('#{0}').change(function()",id) +"{"+
                          string.Format("$('#{0}').click();", buttonsubmitid) +
                           " });</script>");
            return MvcHtmlString.Create(sb.ToString());
        }

       

        public static MvcHtmlString Image(this HtmlHelper html, string id, string path)
        {
            return
                MvcHtmlString.Create(
                    new StringBuilder().AppendFormat(
                        "<img src={0} class=\"img-circle\" id={1} style=\"height:150px;width:150px;\" alt=\"\"/>", path, id)
                        .ToString());
        }


        public static MvcHtmlString Modal(this HtmlHelper mvcHtmlString, string id = null,
            string icon = null,
            string lable = null, string href = null,
            params MvcHtmlString[] mvc)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(
                "<button class=\"btn btn-primary glyphicon glyphicon-{0}\" data-toggle=\"modal\" data-target=\"#{1}\">{2}</button>",
                icon, id, lable);
            sb.AppendFormat(
                "<div class=\"modal fade\" id={0} tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\">",
                id);
            sb.AppendLine("<div class=\"modal-dialog\">");
            sb.AppendLine("<div class=\"modal-content\">");
            sb.AppendLine("<div class=\"modal-header\">");
            sb.AppendFormat(
                "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\"> &times;</button><p>{0}</p></div>",
                lable);
            sb.AppendFormat(
                "<form action={0} enctype=\"multipart/form-data\"    id=\"{1}Form\" class=\"form-horizontal\" method=\"post\">",
                href, id);
            if (mvc != null)
            {
                foreach (MvcHtmlString m in mvc)
                {
                    sb.AppendFormat("{0}", m).AppendLine();
                }
            }

            sb.AppendLine("<div class=\"modal-footer\">");
            sb.AppendFormat("<span id = \"{0}Result\" style=\"color: red; float: left; font-size: 20px;\"></span>", id);
            sb.AppendLine("<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">关闭 </button>");
            sb.AppendFormat(
                "<button type=\"submit\" class=\"btn btn-primary\" id=\"{0}Submit\"> 确定 </button> </div></form> ", id);
            sb.AppendLine("</div></div></div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString Span(this HtmlHelper mv, string lable)
        {
            return
                MvcHtmlString.Create("<span id = \"" + lable + "\" name=\"" + lable + "\" style=\"color:red;\"></span>");
        }

        public static MvcHtmlString Search(this HtmlHelper mv, string name, string hint)
        {
            return MvcHtmlString.Create(string.Format("<div class=\"col-lg-6\">" +
                                                      "<div class=\"input-group\">"
                                                      +
                                                      "<input type=\"text\" name={0} placeholder={1} class=\"form-control\">"
                                                      + "<span class=\"input-group-btn\">"
                                                      + "<button class=\"btn btn-default\" type=\"submit\">查找</button>"
                                                      + "</span>"
                                                      + "</div>"
                                                      + "</div>", name, hint));
        }

        public static MvcHtmlString CreatPageLiTag(this UrlHelper urlHelper,
            PageInfo pageinfo,
            int index,
            bool isCurrentIndex = false,
            bool isDisable = true,
            string content = "")
        {
            string url = urlHelper.Action(pageinfo.ActionName,
                new
                {
                    pagesize = pageinfo.PageSize,
                    index
                });
            string activeClass = !isCurrentIndex ? string.Empty : "class='active'";
            string disableClass = isDisable ? string.Empty : "class='disabled'";
            url = isDisable ? " href='" + url + "'" : string.Empty;
            string contentString = string.IsNullOrEmpty(content) ? index.ToString() : content;

            return
                new MvcHtmlString("<li " + activeClass + disableClass + "><a data-pjax  " + url + ">" + contentString +
                                  "</a></li>");
        }

        public static MvcHtmlString Alert(this HtmlHelper htmlHelper,
            string strongMessage, string message = "",
            AlertCategory alertCategory = AlertCategory.Default)
        {
            var tagBuilder = new TagBuilder("div");
            switch (alertCategory)
            {
                case AlertCategory.Error:
                    tagBuilder.MergeAttribute("class", "alert alert-danger");
                    break;
                case AlertCategory.Success:
                    tagBuilder.MergeAttribute("class", "alert alert-success");
                    break;
                case AlertCategory.Info:
                    tagBuilder.MergeAttribute("class", "alert alert-info");
                    break;
                default:
                    tagBuilder.MergeAttribute("class", "alert");
                    break;
            }
            var a = new TagBuilder("a");
            a.MergeAttribute("class", "close");
            a.MergeAttribute("data-dismiss", "alert");
            a.SetInnerText("×");
            var strong = new TagBuilder("strong");
            strong.SetInnerText(strongMessage);
            tagBuilder.InnerHtml = string.Format("{0}{1}{2}",
                a.ToString(TagRenderMode.Normal),
                strong.ToString(TagRenderMode.Normal),
                message);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString FormValidate(this HtmlHelper htmlHelper, string formName, params Rule[] rules)
        {
            string format = @"
<script type=""text/javascript"">
    $(document).ready(function(){{  
{3}     
        $(""#{0}"").validate({{
            onkeyup: false,
			errorClass: 'error',
			validClass: 'valid',
			highlight: function(element) {{
				$(element).parents('.form-group').addClass(""f_error"");
			}},
			unhighlight: function(element) {{
				$(element).parents('.form-group').removeClass(""f_error"");
			}},
            errorPlacement: function(error, element) {{
                $(element).parents('.form-group').children().last('div').append(error);
            }},  
            rules:{{
                {1}
            }},
            messages:{{
                {2}
            }}
        }}); 
    }});
</script>";
            var sbRules = new StringBuilder();
            for (int i = 0; i < rules.Length; i++)
            {
                sbRules.Append(rules[i].ToRules());
                if (i < rules.Length - 1)
                    sbRules.Append(",").AppendLine().Append("                ");
            }
            var sbMessages = new StringBuilder();
            for (int i = 0; i < rules.Length; i++)
            {
                sbMessages.Append(rules[i].ToMessages());
                if (i < rules.Length - 1)
                    sbMessages.Append(",").AppendLine().Append("                ");
            }

            //为必填的控件加上*号
            var sbRequires = new StringBuilder();
            foreach (Rule rule in rules)
            {
                foreach (var r in rule.Rules)
                {
                    if (r.Key == "required" && r.Value == "true")
                    {
                        sbRequires.Append("        ");
                        sbRequires.AppendFormat(
                            "$(\"[name='{0}']\").parents('.form-group').children('label.control-label').append('<span class=\"f_req\">*</span>');",
                            rule.TagName).AppendLine();
                        break;
                    }
                }
            }
            var sb = new StringBuilder();
            sb.AppendFormat(format, formName, sbRules, sbMessages, sbRequires);
            return MvcHtmlString.Create(sb.ToString());
        }
    }

    public enum AlertCategory
    {
        [EnumText("")] Default,
        [EnumText("error")] Error,
        [EnumText("success")] Success,
        [EnumText("info")] Info
    }

    public class ButtonClass
    {
        public const string Default = "default";
        public const string Primary = "primary";
        public const string Success = "success";
        public const string Info = "info";
        public const string Warning = "warning";
        public const string Danger = "danger";
        public const string Link = "link";
    }
}