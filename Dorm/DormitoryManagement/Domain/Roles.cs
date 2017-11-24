using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public enum Roles
    {
        [EnumText("系统管理")] Root,
        [EnumText("管理员")] Admin,
        [EnumText("部长")] Minister,
        [EnumText("部员")] Member,
        [EnumText("小组")] Group 
    }
}