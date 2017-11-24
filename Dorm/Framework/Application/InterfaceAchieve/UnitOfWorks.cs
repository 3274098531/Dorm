using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Effort;
using MyFramework.Application.Interface;
using MyFramework.Attribute;

namespace MyFramework.Application.InterfaceAchieve
{
    [IocRegister]
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly object _sync = new object();

        private DbContext DbContext
        {
            get { return GetDbContext(); }
        }

        

        #region GetDbcontext

        public DbContext GetDbContext()
        {
            DbContext dbcontext;
            object efDbContext = CallContext.GetData("DbContext");
            lock (_sync)
            {
                if (efDbContext == null)
                {
                    string datatype = ConfigurationManager.AppSettings["DataAssemblyType"];
                    string dataPath = ConfigurationManager.AppSettings["DataAssemblyPath"];
                    Assembly assembly = Assembly.Load(dataPath);
                    if (assembly == null) throw new Exception("DataAssembly为空");
                    if (ConfigurationManager.AppSettings["DatabaseType"] == "Test")
                    {
                        DbConnection connection = DbConnectionFactory.CreateTransient();
                        efDbContext = assembly.CreateInstance(datatype, false, BindingFlags.Default, null,
                            new object[] {connection}, null, null);
                    }
                    else
                    {
                        efDbContext =
                            assembly.CreateInstance(datatype) as DbContext;
                    }

                    if (efDbContext == null) throw new Exception("没有数据库实例");
                    //存入到这个线程缓存中
                    CallContext.SetData("DbContext", efDbContext);
                    dbcontext = efDbContext as DbContext;
                    return dbcontext;
                }
            }
            return efDbContext as DbContext;
        }

        #endregion

      
        public void UnBindContext()
        {
            lock (_sync)
            {
                using (var transation = GetDbContext().Database.BeginTransaction())
                {
                    try
                    {
                        DbContext.SaveChanges();
                        transation.Commit();
                    }
                    catch (Exception)
                    {
                        transation.Rollback();
                    }
                }
            }
        }


        public void Prepare()
        {
            ObjectContext objectContext = ((IObjectContextAdapter) GetDbContext()).ObjectContext;
            var mappingcollection =
                (StorageMappingItemCollection) objectContext
                    .MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingcollection.GenerateViews(new List<EdmSchemaError>());
        }

         
    }
}