using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.Domain
{
    public  class Function : Entity
    { 
        /// <summary>
        /// 子系统名称
        /// </summary>
        public   string Area { get; set; }

        /// <summary>
        /// Controller名称
        /// </summary>
        public   string Controller { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        public   string Action { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public   string ActionInfo { get; set; }

        
    }
}
