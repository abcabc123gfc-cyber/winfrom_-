using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    public class DataAccess
    {
        static readonly string str = ConfigurationManager.AppSettings["DAL"];
        #region CreateObject 
        /// <summary>
        /// 使用缓存创建DAL对象，主要为了提升创建DAL时性能。每一次创建DAL后，如果缓存中没有存储此DAL，会把DAL缓存一下，以便下次再次创建时，不用重新创建，而从缓存中取，从而提升性。
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="classNamespace">命令空间</param>
        /// <returns>DAL对象</returns>
        private static object CreateObject(string assemblyPath, string classNamespace)
        {
            // 先从缓存中取某个DAL实例，如果为null，说明缓存中还没有存储过此DAL实例。
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                //try
                //{
                //    // Assembly程序集，它的Load()方法专门用来加载程序集，必须保证UI层bin/debug下有此程序集，且还得有此程序的间接引用的其他程序，如DBUtility。
                //    // Assembly.Load()加载程序集。
                //    // CreateInstance()创建DAL实例。Instance实例， 相当于CustomerInfoDAL abc = new CustomerInfoDAL()




                //    //objType = Assembly.Load(assemblyPath).CreateInstance(classNamespace);
                //    //DataCache.SetCache(classNamespace, objType);// 写入缓存
                //    //throw new InvalidOperationException($"无法创建类型 {classNamespace}，请确认类有无无参构造函数且实现了目标接口。");
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception($"创建 DAL 实例失败 [程序集: {assemblyPath}, 类: {classNamespace}]", ex);
                //}

                try
                {
                    // 使用 Type.GetType + 程序集限定名，更可靠
                    string assemblyQualifiedName = $"{classNamespace}, {assemblyPath}";
                    Type type = Type.GetType(assemblyQualifiedName);
                    if (type == null)
                        throw new TypeLoadException($"找不到类型 {classNamespace}，请检查命名空间和类名是否正确。");

                    objType = Activator.CreateInstance(type); // 如果无参构造函数缺失会抛异常
                    DataCache.SetCache(classNamespace, objType);
                }
                catch (Exception ex)
                {
                    throw new Exception($"创建 DAL 实例失败 [程序集: {assemblyPath}, 类: {classNamespace}]", ex);
                }

            }
            return objType;
        }
        #endregion

        #region 泛型生成

        public static T CreateDAL<T>(string className)
        {
            //拿到完整的命名空间 程序集. 类名

            string classNamespace = string.Format("{0}.{1}", str, className);
            //创建Dall对象DAL实例
            // CreateObject()根据程序集名称和某个DAL完整的命名空间来创建DAL实例的。
            object obj = CreateObject(str, classNamespace);
            return (T)obj ;
           
        }
        #endregion
    }
}
