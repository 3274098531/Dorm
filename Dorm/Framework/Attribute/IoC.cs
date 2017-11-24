using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace MyFramework.Attribute
{

    #region Autofac

//    public class IoC
//    {
//        private static IContainer Container { get; set; }
//
//        public static void Resign()
//        {
//            ContainerBuilder builder = RegisterService();
//            Container = builder.Build();
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
//        }
//
//        private static ContainerBuilder RegisterService()
//        {
//            var builder = new ContainerBuilder();
          
//            builder.RegisterAssemblyTypes(assemblys.ToArray())
//                .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
//                .AsImplementedInterfaces().InstancePerLifetimeScope();
//            return builder;
//        }
//
//        public static T Get<T>()
//        {
//            return DependencyResolver.Current.GetService<T>();
//        }
//
//
//        public static IContainer Current()
//        {
//            return Container;
//        }
    //    }

    #endregion

    #region Unity

    public static class IoC
    {
        private static readonly IUnityContainer UnityContainer;

        static IoC()
        {
            UnityContainer = new UnityContainer();

            Register();
        }

        public static IUnityContainer Current
        {
            get { return UnityContainer; }
        }

        /// <summary>
        ///     遍历命名空间，自动注册
        /// </summary>
        public static void Register()
        {
            //Aop配置
            UnityContainer.AddNewExtension<Interception>();
            var interception = UnityContainer.Configure<Interception>();

            var types = new List<string>();
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            string[] appKeys = appSettings.AllKeys;
            foreach (string appKey in appKeys)
            {
                if (appKey.Substring(appKey.Length - 3, 3) == "IoC")
                {
                    types.Add(appKey);
                }
            }
            foreach (string type in types)
            {
                string value = ConfigurationManager.AppSettings[type];
                RegisterType(value, interception);
            } 
        }

        public static void RegisterType(string assemblyName, Interception interception)
        {
            Type[] types = Assembly.Load(assemblyName).GetTypes();
            foreach (Type type in types)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(type);
                foreach (System.Attribute attr in attrs)
                {
                    if (attr is IocRegisterAttribute)
                    {
                        var attribute = attr as IocRegisterAttribute;
                        var baseTypes = new List<Type>();
                        if (attribute.RegisterType != null)
                        {
                            baseTypes.Add(attribute.RegisterType);
                        }
                        else if (type.GetInterfaces().Any())
                        {
                            baseTypes.AddRange(type.GetInterfaces());
                        }
                        if (type.BaseType != null && type.BaseType != typeof (Object))
                        {
                            Type bt = type.BaseType;
                            while (bt.BaseType != null && bt.BaseType != typeof (Object))
                                bt = bt.BaseType;
                            baseTypes.Add(bt);
                        }
                        if (baseTypes.Count <= 0)
                            throw new Exception(string.Format("类型\"{0}\"没有基类型", type));
                        foreach (Type baseType in baseTypes)
                        {
                            if (string.IsNullOrEmpty(attribute.Name))
                                Current.RegisterType(baseType, type);
                            else
                                Current.RegisterType(baseType, type, attribute.Name);
                        }
                    }
                }
            }
        }

        public static T Get<T>()
        {
            try
            {
                return UnityContainer.Resolve<T>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "," + typeof (T));
            }
        }

        public static void AopRegister<TFrom, TTo>() where TTo : TFrom
        {
            Current.RegisterType<TFrom, TTo>()
                .Configure<Interception>()
                .SetInterceptorFor<TFrom>(new InterfaceInterceptor());
        }
    }

    #endregion
}