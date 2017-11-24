using MyFramework.Domain;

namespace DormitoryManagement.Domain
{
    public enum Position
    {
        [EnumText("部长")] Minister ,
        [EnumText("部员")] Member ,
        [EnumText("组员")] Group 
    }
}