using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DormitoryManagement.Application;
using DormitoryManagement.Application.Imp;
using DormitoryManagement.Domain;
using MyFramework.Attribute;
using MyFramework.Domain;

namespace DormitoryManagement.Web.Default
{
    [RoleAuth(Keys.Admin)]
    public class AdminBaseContorller:BaseController 
    {
        public IAdminService AdminService;

        public AdminBaseContorller(IAdminService adminService)
        {
            AdminService = adminService;
        }
    }
}