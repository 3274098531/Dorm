using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using LinqSpecs;

namespace MyFramework.Domain
{
    public class Log : Entity
    {
        protected Log()
        {
            Time = DateTime.Now;
        }

        public Log(string sessionId, int index) : this()
        {
            SessionId = sessionId;
            Index = index;
        }

        public string SessionId { get; set; }
        public int Index { get; set; }

        /// <summary>
        ///     Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     操作时间
        /// </summary>
        public DateTime Time { get; set; }

        public string Type { get; set; }
      
        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     请求类型
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        ///     用户Ip
        /// </summary>
        public string UserIp { get; set; }

        /// <summary>
        ///     参数数据
        /// </summary>
        public string ActionParameters { get; set; }

        public class BySessionId : Specification<Log>
        {
            private readonly string sessionId;

            public BySessionId(string sessionId)
            {
                this.sessionId = sessionId;
            }

            public override Expression<Func<Log, bool>> IsSatisfiedBy()
            {
                return x => x.SessionId == sessionId;
            }
        }
        
    }
    public static class LogHelp
    {
        public static string ToJson(this IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            foreach (var parameter in parameters)
            {
                if (sb[sb.Length - 1] != '{')
                    sb.Append(",");
                if (parameter.Value is FormCollection)
                {
                    var collection = (parameter.Value as FormCollection);
                    sb.AppendFormat("\"{0}\":[", parameter.Key);
                    foreach (var key in collection.AllKeys)
                    {
                        if (key != FrameworkKeys.Password)
                        {
                            if (sb[sb.Length - 1] != '[')
                                sb.Append(",");
                            sb.AppendFormat("\"{0}\":\"{1}\"", key, collection[key]);
                        }
                    }
                    sb.Append("]");
                }
                else
                    sb.AppendFormat("\"{0}\":\"{1}\"", parameter.Key, parameter.Value);

            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}