using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using DormitoryManagement.Web.Areas.Minister;
using MyFramework.Attribute;
using MyFramework.Domain;

namespace DormitoryManagement.Web.Default
{
    [RoleAuth(Keys.Minister)]
    public class MinisterBaseController:BaseController
    {
        protected IMinisterService MinisterService;

        public MinisterBaseController(IMinisterService ministerService)
        {
            MinisterService = ministerService;
        }
    }
}