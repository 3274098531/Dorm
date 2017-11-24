using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace MyFramework.Domain
{
    public class Entity
    {
        public Entity()
        {
            InitPropertyOfDateTimeType();
        } 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; } 
        public DateTime UpdateTime { get; set; } 
        public string UpdateUserName { get; set; } 
        public string CreateUserName { get; set; } 
        public virtual string Code { get; private set; }

         
        public virtual string Name { get; private set; }

        private void InitPropertyOfDateTimeType()
        {
            Type type = GetType();
            PropertyInfo[] pies = type.GetProperties();
            foreach (PropertyInfo pi in pies.Where(pi => pi.PropertyType == typeof (DateTime)))
            {
                pi.SetValue(this, SqlServerMinValue(), null);
            }
        }

        public DateTime SqlServerMinValue()
        {
            return new DateTime(1753, 1, 1, 12, 0, 0);
        }
         
    }
}