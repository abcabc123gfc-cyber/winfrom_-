using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _07_反射
{
    internal class Program
    {
        static void Main(string[] args)
        {
          // typeof 获取类型
          Type  t1=  typeof(int);
          Type  t2=  typeof(string);
          Type  t3=  typeof(Student);
          Console.WriteLine(t1);
          Console.WriteLine(t2);
          Console.WriteLine(t3);


          //没有反射之前:如何查看或者使用程序集以及程序集中的对象?
          //1.添加引用 2.引用命名空间   3.实例化

         //有了反射之后,可以让查看或者使用程序集以及程序集中的对象,多了一种方式


         //反射就是通过元数据获取程序集或者程序集中对象的信息
         //信息: 类型  成员(字段 属性 方法....



            //没有反射之前 直接创建实例对象

            Student stu =new Student();

            Console.WriteLine(stu.myName);
            Console.WriteLine(stu.Name);
            stu.Test1("a", "b");


            Type stuType=  stu.GetType();

            Console.WriteLine( stuType);
            Console.WriteLine( stuType.Name);//类型名称
            Console.WriteLine( stuType.Namespace);//类所在的命名空间
            Console.WriteLine( stuType.FullName);//完整的名称  命名空间 +类型


            //---------------------通过反2345射创建实例 获取字段 属性 方法

            //Assembly.Load("07_反射");//加载程序集
            //如果加载的程序集在当前项目的bin.Debug中不存在, 会加载失败
            Assembly assembly = Assembly.Load("07_反射");
            //CreateInstance() 创建实例 在()中传入完整的对象命名,用记载的程序集,创建一个程序集中对象的实例
            //使用反射创建程序集中的某个实例
            //等同于Student stu =new Student();
            object tt1 =  assembly.CreateInstance("_07_反射.Student");

           
            Console.WriteLine("-----获取单个公开字段--------");


            Type type3= typeof(Student);

            //Field 字段 获取字段
            //FieldInfo fieldInfo= type3.GetField("_id");
            //SetValue() 给字段设置至 等同于 _id=123
            //报错 : 私有的无法访问
            //fieldInfo.SetValue(tt1, 123);

            FieldInfo fieldInfo = type3.GetField("myId");
            fieldInfo.SetValue(tt1, 123);

            //GetValue() 获取值  对象名.属性名
            int myId =(int) fieldInfo.GetValue(tt1);


            Console.WriteLine("-----获取所有的公开字段--------");
            FieldInfo[] fieldInfos=  type3.GetFields();

            //foreach (var item in fieldInfos)
            //{
            //    Console.WriteLine(item);
            //}


            Console.WriteLine("-----获取单个公开属性--------");

             var  propertyInfo=   type3.GetProperty("Name");
             propertyInfo.SetValue(tt1, "吴亦凡");
            Console.WriteLine(propertyInfo.GetValue(tt1));


            Console.WriteLine("-----获取所有的公开属性--------");

            var ps= type3.GetProperties();
            foreach (var item in ps)
            {
                Console.WriteLine(item.Name);
                if (item.Name=="Id")
                {
                    item.SetValue(tt1, 123);
                }
            }


            Console.WriteLine("-----获取单个方法--------");
            MethodInfo  methodInfo = type3.GetMethod("Test1");//获取单个方法


            //方法要通过Invoke() 调用  参数通过数组的形式传入
            methodInfo.Invoke(tt1,new object[] {"123","123"});

            Console.WriteLine("-----获取所有方法--------");
            type3.GetMethods();//获取所有方法

          Type s1=   type3.GetNestedType("SamllStudent");//获取Student类中的SamllStudent
          object ss2=  assembly.CreateInstance(s1.FullName);

            Console.WriteLine("--------------------");

            //利用反射 吧Libs文件夹夏的ClassLibrary1.dll中的class1读取出来

            //F:\C#软件开发14班\2026-07-31\0731_特性反射_加密解密\07_反射\libs\ClassLibrary1.dll


            //F:\C#软件开发14班\2026-07-31\0731_特性反射_加密解密\07_反射\bin\Debug

            //../../libs\ClassLibrary1.dll
            string filePath = Path.Combine(Environment.CurrentDirectory, "../../libs/ClassLibrary1.dll");

            Console.WriteLine(filePath);

            //1.通过路径加载程序集
            Assembly classAssembly= Assembly.LoadFile(filePath);

            //2.创建类的实例
          object class1=    classAssembly.CreateInstance("ClassLibrary1.Class1");
         Type classType=    class1.GetType();
         Console.WriteLine(classType.Name);
         Console.WriteLine(classType.Namespace);
         Console.WriteLine(classType.FullName);

            Console.WriteLine("--------------------");
            //获取类中所有公开的成员
            MemberInfo [] members=    classType.GetMembers();


            foreach (MemberInfo m in members)
            {

                if (m.MemberType==MemberTypes.Property)
                {
                    //说明是属性

                    PropertyInfo propertyInfo1= m as PropertyInfo;

                    Console.WriteLine(propertyInfo1.GetValue(class1));
                }
                if (m.MemberType == MemberTypes.Method)
                {
                    //说明是方法

                    MethodInfo methodInfo1= m as MethodInfo;

                   
                    if (methodInfo1.Name=="Test1")
                    {
                  
                        methodInfo1.Invoke(class1, null);
                    }

                }


                Console.WriteLine(m);
            }


        }
    }
}
