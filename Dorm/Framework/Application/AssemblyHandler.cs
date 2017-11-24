using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MyFramework.Application
{
    public class AssemblyHandler
    {
        private readonly string _path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/MyDLL/";

        /// <summary>
        ///     获取程序集名称列表
        /// </summary>
        public AssemblyResult GetAssemblyName()
        {
            var result = new AssemblyResult();
            string[] dicFileName = Directory.GetFileSystemEntries(_path);
            if (dicFileName != null)
            {
                var assemblyList = new List<string>();
                foreach (string name in dicFileName)
                {
                    assemblyList.Add(name.Substring(name.LastIndexOf('/') + 1));
                }
                result.AssemblyName = assemblyList;
            }
            return result;
        }

        /// <summary>
        ///     获取程序集中的类名称
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        public AssemblyResult GetClassName(string assemblyName)
        {
            var result = new AssemblyResult();
            if (!String.IsNullOrEmpty(assemblyName))
            {
                assemblyName = _path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type[] ts = assembly.GetTypes();
                var classList = new List<string>();
                foreach (Type t in ts)
                {
                    //classList.Add(t.Name);  
                    classList.Add(t.FullName);
                }
                result.ClassName = classList;
            }
            return result;
        }

        /// <summary>
        ///     获取类的属性、方法
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <param name="className">类名</param>
        public AssemblyResult GetClassInfo(string assemblyName, string className)
        {
            var result = new AssemblyResult();
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                assemblyName = _path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    //类的属性  
                    var propertieList = new List<string>();
                    PropertyInfo[] propertyinfo =
                        type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    foreach (PropertyInfo p in propertyinfo)
                    {
                        propertieList.Add(p.ToString());
                    }
                    result.Properties = propertieList;

                    //类的方法  
                    var methods = new List<string>();
                    MethodInfo[] methodInfos =
                        type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (MethodInfo mi in methodInfos)
                    {
                        methods.Add(mi.Name);
                        //方法的参数  
                        //foreach (ParameterInfo p in mi.GetParameters())  
                        //{  

                        //}  
                        //方法的返回值  
                        //string returnParameter = mi.ReturnParameter.ToString();  
                    }
                    result.Methods = methods;
                }
            }
            return result;
        }
    }

    public class AssemblyResult
    {
        /// <summary>
        ///     程序集名称
        /// </summary>
        public List<string> AssemblyName { get; set; }

        /// <summary>
        ///     类名
        /// </summary>
        public List<string> ClassName { get; set; }

        /// <summary>
        ///     类的属性
        /// </summary>
        public List<string> Properties { get; set; }

        /// <summary>
        ///     类的方法
        /// </summary>
        public List<string> Methods { get; set; }
    }
}