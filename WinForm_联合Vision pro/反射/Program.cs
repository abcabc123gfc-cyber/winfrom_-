using System;
using System.IO;
//using common;
// 反射 使用Assembly
using System.Reflection;
using common;
namespace 反射
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            //test.write();
            //test.SayHelllo();
            //test.SayHelllo("重载");
            //加载程序集文件
            //获取exe文件所在位置,与目录拼接
            string path = Path.Combine((AppDomain.CurrentDomain.BaseDirectory), "common.dll");
            //使用反射获取类型信息
            #region 使用 Type 获取程序集中的类型信息:方法 属性 类型
            //Type type= typeof(Test);
            //Console.WriteLine(type.Name+"___"+type.FullName);
            //获取所有程序
            /*foreach(var ass in type.GetProperties())
            {
                Console.WriteLine("属性"+"_"+ass.Name+"_类型_"+ass.PropertyType.Name);
            }*/
            //获取所有方法
            /*foreach(var ass in type.GetMethods())
            {
                Console.WriteLine("方法"+"_"+ass.Name+"_参数长度_"+ass.GetParameters().Length);
            }*/
            #endregion
            Assembly ass = Assembly.LoadFile(path);
            Console.WriteLine("加载程序成功");
            #region 获得程序集数据的三种方法
            //获取程序集中的所有类型,包括公开与私有
            //foreach (var item in ass.GetTypes())
            //{
            //    Console.WriteLine(item.Name);
            //    Console.WriteLine(item.FullName);
            //    Console.WriteLine(item.Namespace);
            //} 
            //获取程序集中公开的类型
            //foreach (var item in ass.GetExportedTypes())
            //{
            //    Console.WriteLine( item.Name);
            //    Console.WriteLine(item.FullName);
            //    Console.WriteLine(item.Namespace);
            //} 
            //获取指定的类型
            {
                Type type = ass.GetType("common.Test");
                //Console.WriteLine(type);
            }
            #endregion
            Subclass subclass = new Subclass();
            //CreateObject(ass);
           CommonFunctions(test,subclass);
        }
        static void CreateObject(Assembly ass)
        {

            //调用了 Test类中默认无参数的构造函数,
            //一旦创建对象,则必须提供无参构造函数,否则会报错
            //object o = ass.CreateInstance("common.Test");

            //调用有参构造函数
            object o = Activator.CreateInstance(ass.GetType("common.Test"), "张三", 18);
            Console.WriteLine(o.GetType());
            PropertyInfo[] propertyInfos = o.GetType().GetProperties(); 
            foreach (PropertyInfo obj in propertyInfos)
            {
                Console.WriteLine(obj .Name);
            }
            // 获取方法
            MethodInfo[] methodInfos = o.GetType().GetMethods();
            foreach (MethodInfo obj in methodInfos)
            {
                //obj.Invoke(o, null);
                Console.WriteLine(obj.Name);
            }
            //获取指定方法
            o.GetType().GetMethod("SayHelllo").Invoke(o, null);
        }

        /// <summary>
        /// 反射的常用方法
        /// </summary>
        static void CommonFunctions(Test test,Subclass SubClass)
        {
            // 判断test对象是否是Test类的实例
            bool b =typeof(Test ).IsAssignableFrom(test.GetType());
            Console.WriteLine(b);

            //判断SubClass对象是否是Test类的子类
            b=typeof(Test).IsAssignableFrom(typeof(Subclass));
            Console.WriteLine(b);

            //判断 test对象是否是Test类的实例,可判断接口
            b = test.GetType().IsInstanceOfType(test);
            typeof(I1).IsInstanceOfType(SubClass);
            Console.WriteLine(b);

            //判断test对象是否是Test类的实例,不能判断接口
            b = test.GetType().IsSubclassOf(typeof(Test));
            Console.WriteLine(b);
        }
    }
}
