using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyFramework.Helper
{
    public class EmailHelper
    {
        /// <summary>
        ///     发送者邮箱（必填）
        /// </summary>
        public string MyEmail
        {
            get { return ConfigurationManager.AppSettings["FromEmailAddress"]; }
        }

        /// <summary>
        ///     发送者邮箱密码（必填）
        /// </summary>
        public string MyPwd
        {
            get { return ConfigurationManager.AppSettings["EmailPassword"]; }
        }

        /// <summary>
        ///     发送者昵称
        /// </summary>
        public string MyNickName
        {
            get { return "Admin"; }
        }

        /// <summary>
        ///     接收者邮箱（必填）
        /// </summary>
        public string[] ToEmail { get; set; }

        /// <summary>
        ///     邮件标题（必填）
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     邮件内容（必填）
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     附件信息
        /// </summary>
        public string[] Files { get; set; }

        /// <summary>
        ///     是否html格式
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        ///     地址（必填）
        /// </summary>
        public string Host
        {
            get { return ConfigurationManager.AppSettings["SmtpHost"]; }
        }

        /// <summary>
        ///     端口（必填）
        /// </summary>
        public int Port
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SmtpPort"]); }
        }

        /// <summary>
        ///     邮件发送（注意：1.QQ邮箱开启pop或者smtp 2.smtp端口是587 3.如果用手机发送短信获取了qq邮箱的授权密码，那么myPwd参数应该是授权密码）
        /// </summary>
        /// <returns></returns>
        public bool _Send()
        {
            
            try
            {
                var mail = new MailMessage();
                //发件人
                mail.From = new MailAddress(MyEmail, MyNickName, Encoding.UTF8);
                foreach (string item in ToEmail)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }
                    //收件人
                    mail.To.Add(new MailAddress(item, "", Encoding.UTF8));
                }

                //发送附件
                if (Files != null)
                {
                    foreach (string item in Files)
                    {
                        mail.Attachments.Add(new Attachment(item));
                    }
                }

                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = IsBodyHtml;

                var smtp = new SmtpClient(Host, Port);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(MyEmail, MyPwd);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}