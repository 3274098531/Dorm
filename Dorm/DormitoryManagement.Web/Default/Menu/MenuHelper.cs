using System;
using System.Collections.Generic;
using System.Linq;
using DormitoryManagement.Domain;
using MyFramework.Domain;

namespace DormitoryManagement.Web.Default.Menu
{
    public class MenuHelper
    {
        public static IList<MenuGroup> GetGroup(params string[] rolenames)
        {
            var list = new List<MenuGroup>();
            var menu = new List<MenuGroup>();
            foreach (var rolename in rolenames)
            { 
            var role = (Roles) Enum.Parse(typeof (Roles), rolename);
            switch (role)
            {
                    #region Admin

                case Roles.Admin:
                    list.Add(new MenuGroup
                    {
                        Ico = "user",
                        Name = "用户管理",
                        Functions = new List<Function>
                        {
                            new Function {Action = "Index", Controller = "User", Area = "Admin", ActionInfo = "用户管理"}
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "cog",
                        Name = "基础数据管理",
                        Functions = new List<Function>
                        { 
                            new Function {ActionInfo = "学院管理", Action = "Index", Controller = "Academy", Area = "Admin"},
                            new Function {ActionInfo = "班级管理", Action = "Index", Controller = "Class", Area = "Admin"},
                            new Function {ActionInfo = "宿舍楼管理", Action = "Index", Controller = "Dorm", Area = "Admin"},
                            new Function {ActionInfo = "房间管理", Action = "Index", Controller = "Room", Area = "Admin"},
                            new Function {ActionInfo = "学生管理", Action = "Index", Controller = "Student", Area = "Admin"}
                        }
                    });
                    break;

                    #endregion

                    #region Minister

                case Roles.Minister:
                    list.Add(new MenuGroup
                    {
                        Ico = "user",
                        Name = "部门成员管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                Action = "Index",
                                Controller = "Member",
                                Area = "Minister",
                                ActionInfo = "成员管理"
                            }
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "cog",
                        Name = "基础数据管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "学院管理",
                                Action = "Index",
                                Controller = "Academy",
                                Area = "Minister"
                            },
                            new Function
                            {
                                ActionInfo = "班级管理",
                                Action = "Index",
                                Controller = "Class",
                                Area = "Minister"
                            },
                            new Function
                            {
                                ActionInfo = "宿舍楼管理",
                                Action = "Index",
                                Controller = "Dorm",
                                Area = "Minister"
                            },
                            new Function {ActionInfo = "房间管理", Action = "Index", Controller = "Room", Area = "Minister"},
                            new Function
                            {
                                ActionInfo = "学生管理",
                                Action = "Index",
                                Controller = "Student",
                                Area = "Minister"
                            }
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "tasks",
                        Name = "卫生管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "卫生检查管理",
                                Action = "Index",
                                Controller = "CheckDorm",
                                Area = "Minister"
                            },
                        }
                    });
                     
                    break;

                    #endregion

                    #region Member

                case Roles.Member:
                    list.Add(new MenuGroup
                    {
                        Ico = "user",
                        Name = "个人信息管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                Action = "Index",
                                Controller = "Information",
                                Area = "Member",
                                ActionInfo = "个人信息"
                            }
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "cog",
                        Name = "基础数据管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "学院管理",
                                Action = "Index",
                                Controller = "Academy",
                                Area = "Member"
                            },
                            new Function
                            {
                                ActionInfo = "班级管理",
                                Action = "Index",
                                Controller = "Class",
                                Area = "Member"
                            },
                            new Function
                            {
                                ActionInfo = "宿舍楼管理",
                                Action = "Index",
                                Controller = "Dorm",
                                Area = "Member"
                            },
                            new Function {ActionInfo = "房间管理", Action = "Index", Controller = "Room", Area = "Member"},
                            new Function
                            {
                                ActionInfo = "学生管理",
                                Action = "Index",
                                Controller = "Student",
                                Area = "Member"
                            }
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "tasks",
                        Name = "卫生管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "卫生检查管理",
                                Action = "Index",
                                Controller = "CheckDorm",
                                Area = "Member"
                            },
                        }
                    });
                    break;

                    #endregion

                    #region Group

                case Roles.Group:
                    list.Add(new MenuGroup
                    {
                        Ico = "user",
                        Name = "个人信息管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                Action = "Index",
                                Controller = "Information",
                                Area = "Group",
                                ActionInfo = "个人信息"
                            }
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "cog",
                        Name = "基础数据管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "学院管理",
                                Action = "Index",
                                Controller = "Academy",
                                Area = "Group"
                            },
                            new Function
                            {
                                ActionInfo = "班级管理",
                                Action = "Index",
                                Controller = "Class",
                                Area = "Group"
                            },
                            new Function
                            {
                                ActionInfo = "宿舍楼管理",
                                Action = "Index",
                                Controller = "Dorm",
                                Area = "Group"
                            },
                            new Function {ActionInfo = "房间管理", Action = "Index", Controller = "Room", Area = "Group"},
                            new Function
                            {
                                ActionInfo = "学生管理",
                                Action = "Index",
                                Controller = "Student",
                                Area = "Group"
                            }
                        }
                    });
                    list.Add(new MenuGroup
                    {
                        Ico = "tasks",
                        Name = "卫生管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "卫生检查管理",
                                Action = "Index",
                                Controller = "CheckDorm",
                                Area = "Group"
                            },
                        }
                    });
                    break;

                    #endregion

                    #region Root

                case Roles.Root: 
                    list.Add(new MenuGroup
                    {
                        Ico = "cog",
                        Name = "基础数据管理",
                        Functions = new List<Function>
                        {
                            new Function
                            {
                                ActionInfo = "操作日志管理",
                                Action = "Index",
                                Controller = "RequestLog",
                                Area = "Root"
                            },
                        }
                    });
                    break;

                    #endregion 
                
            }
            } 
            return list;
        }
    }
}