using System;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;

namespace MyFramework.Application.InterfaceAchieve
{
    [IocRegister]
    public class RequestLogCommand : IRequestLogCommand
    {
        private readonly Log _log;

        public RequestLogCommand(Log log)
        {
            this._log = log;
        }

        public IRequestLogCommand Url(string url)
        {
            _log.Url = url;
            return this;
        }

        public IRequestLogCommand Time(DateTime time)
        {
            _log.Time = time;
            return this;
        }

        public IRequestLogCommand HttpMethod(string httpMethod)
        {
            _log.HttpMethod = httpMethod;
            return this;
        }

        public IRequestLogCommand UserIp(string userIp)
        {
            _log.UserIp = userIp;
            return this;
        }

        public IRequestLogCommand UserName(string userName)
        {
            _log.UserName = userName;
            return this;
        }

        public IRequestLogCommand ActionParameters(string parameters)
        {
            _log.ActionParameters = parameters;
            return this;
        } 
        public Guid Id { get { return _log.Id; } }
    }
}