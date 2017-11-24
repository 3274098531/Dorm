using System.Data.Common;
using System.Data.Entity;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.DataBase;

namespace DormitoryManagement.Domain
{
    [IocRegister]
    public class DormitoryManagementEntities
        : FrameworkDatabase<DormitoryManagementEntities> 
    {
        public DormitoryManagementEntities(){}
        public DormitoryManagementEntities(DbConnection connection)
            : base(connection, true)
        {
        }
        
        public DbSet<Academy> Academies { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Dorm> Dorms { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<CheckResult> CheckResults { get; set; }
        public DbSet<Member> Menbers { get; set; }
       
        
    }
}