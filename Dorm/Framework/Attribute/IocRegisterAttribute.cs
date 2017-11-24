using System;

namespace MyFramework.Attribute
{
    public class IocRegisterAttribute:System.Attribute
    {
        public Type RegisterType { get; private set; }

        public string Name { get; private set; }

        public IocRegisterAttribute() : this(null, null)
        {
        }

        public IocRegisterAttribute(Type register_type) : this(register_type, null)
        {
        }

        public IocRegisterAttribute(string name) : this(null, name)
        {
        }

        public IocRegisterAttribute(Type registerType, string name)
        {
            this.RegisterType = registerType;
            this.Name = name;
        }
    }
}