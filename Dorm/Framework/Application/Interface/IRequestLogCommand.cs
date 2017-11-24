using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.Application.Interface
{
    public interface IRequestLogCommand:IBaseCommand
    {
        IRequestLogCommand Url(string url);
        IRequestLogCommand Time(DateTime time);
        IRequestLogCommand HttpMethod(string httpMethod);
        IRequestLogCommand UserIp(string userIp);
        IRequestLogCommand UserName(string userName);
        IRequestLogCommand ActionParameters(string parameters);
    }
}
