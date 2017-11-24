using System;
using DormitoryManagement.Domain;
using MyFramework.Helper;

namespace DormitoryManagement.Test.测试
{
    public class 数据准备工作 : 测试对象
    {
        public static string 创建学院(string name)
        {
            return AdminService.CreateAcademy().Name(name).ShortName(name).Id.ToString();
        }

        public static void 创建部员账号(string username, string password)
        {
            FrameworkService.CreateAccount(username).PassWord(password).AddRole(部员权限id);
        }

         

        public static void 创建寝室长账号(string username, string password)
        {
            FrameworkService.CreateAccount(username)
                .PassWord(password)
                .AddRole(寝室长权限id);
        }

        public static void 创建小组账号(string username, string password)
        {
            FrameworkService.CreateAccount(username).PassWord(password).AddRole(组员权限id);
        }
         

         

        public static string 创建部员(string code)
        {
            创建学生(code);
            repository.Submit();
            
            return MinisterService.CreateMenber(code) .Id.ToString();
        }

        public static string 创建班级(string name)
        {
            return AdminService.CreateClass().Name(name).Academy(创建学院(name)).Id.ToString();
        }

        public static string 创建宿舍楼(string name)
        {
            return AdminService.CreateDorm().Name(name).Id.ToString();
        }

        public static string 创建房间(string name)
        {
            string id = 创建宿舍楼(name + "dorm");
            string 学院 = 创建学院("Academy" + name);
            return AdminService.CreateRoom().Name(name).Dorm(id).Academy(学院).MaxBedNum("8").Id.ToString();
        }


        public static string 创建房间(string dormname, string roomname)
        {
            string id = 创建宿舍楼(dormname);
            string 学院 = 创建学院("Academy" + dormname);
            return AdminService.CreateRoom().Name(roomname)
                .Academy(学院).Dorm(id).MaxBedNum("8").Id.ToString();
        }

        public static string 创建学生(string code)
        {
            string 班级id = 创建班级(code + "class");
            string 房间id = 创建房间(code + "room");
            return AdminService.CreateStudent(code)
                .Class(班级id)
                .Room(房间id).Id.ToString();
        }

         

        public static string 创建卫生检查(string name)
        {
            return MinisterService.CreateInspection().Name(name).Id.ToString();
        }
    }
}