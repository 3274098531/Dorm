using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using MyFramework.Domain;
using Version = MyFramework.Domain.Version;

namespace MyFramework.DataBase
{
    public class FrameworkDatabase<T> : DbContext where T : DbContext
    {
        protected FrameworkDatabase(DbConnection connection, bool flag)
            :base(connection,flag)
        {
             
        }
        public FrameworkDatabase(){} 

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }

        public void InitDatebase()
        {
            string systemname = ConfigurationManager.AppSettings[FrameworkKeys.SystemName];
            Database.SetInitializer(new DropCreateDatabaseAlways<T>());
            Database.Initialize(true);
            Set<Version>().Add(new Version {SystemName = systemname, Num = 1.0, Id = Guid.NewGuid()});
            SaveChanges();
        }
         
    }

}