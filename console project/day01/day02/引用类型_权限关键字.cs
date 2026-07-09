using System;
using System.Runtime.InteropServices;

namespace day02
{
    internal class 引用类型_权限关键字
    {
        /// <summary>
        /// 引用类型
        /// </summary>
        public void Reference()
        {
            //应用：引用类型

            //1 字符串string 用于存储多个字符
            //需要使用 "" 包裹
            //创建字符串有三种方式 " "  @""  $""
            // \n 换行
            //\r 回车 \t 制表符
            //\t  前面的字符的个数+后面空格的个数 ,和刚好为8 要么就是8的倍数
            string name = "张\n三";

            //一些字符不能直接使用 \n \t  ,
            //可以使用 \ 进行转义 ,\称为转义字符
            string name1 = "张\\t三";

            //使用@ 创建的字符串不需要使用转义字符就可以实现特殊的字符
            //" :比较特殊 需要使用"" 表示一个"
            string name2 = @"C:\Program Files\Cognex\VisionPro\ReferencedAssemblies";

            //$"" 创建的字符串 可以使用{}进行变量的替换
            string name3 = $"{name}";

            //object 类型 对象类型
            //1 Object : 所有类4型的基类 可以存储任意类型的数据
            object obj = 123;
            object obje = "hello world";

            //2 可以定义类, 存储一系列的数据
            //对象和类的区别: 对象是类的实例化,对象可以访问类中的成员变量和方法
            //创建一Student实例化
            Student student = new Student("小王");

            student.Age = 18;
            Console.WriteLine($"名字{student.Name},年龄{student.Age}");

            //动态类型 dynamic 可以存储任意类型的数据
            dynamic name4 = "小王";







        }

        public void ValueType()
        {
            //值类型和引用类型的区别
            //1 存储的位置不一样 值类型是在栈中存储的保存的是值本身 引用类型是在堆中存储的,在在栈中保存的是引用地址
            //2 值类型有默认值 引用类型没有默认值
            //3 值类型不能为null 引用类型可以为null

            // = 运算符操作复制的时候,复制的是栈中保存的值, 赋值同理
            Student s1 = new Student("小王");
            Student S2 = s1;
            s1.Name = "张三";
            //Console.WriteLine(S2.Name);


            //引用类型赋值 使用 ref 确保 地址指向的一致性
            string s4 = "hello";
            ref string s3 = ref  s4;
            s3="world";
            //Console.WriteLine(s3);

            int i2 = 10;
            ref int i1 = ref i2;
            i1=20;
            //Console.WriteLine(i2);


            
            
        }
        //大驼峰命名法 : StudentObject
        //创建类使用大驼峰命名法,类成员
        //小驼峰命名法 : studentObject
        class Student
        {
            //在类中声明的内容:属性,方法,构造函数,属性,成员变量 统称为 类成员
            //属性:类成员变量的访问方式
            //方法:类成员函数的访问方式
            //构造函数:类对象创建时调用
            //成员变量:类成员变量的声明方式
            //类成员变量的声明方式:
            //访问修饰符 数据类型 成员变量名;
            //访问修饰符:public,private,protected,internal,protected internal,private protected

            //public：API接口、公共类库。

            //private：大多数类成员（默认），用于封装内部实现细节。

            //protected：为子类提供定制扩展点的方法。

            //internal：用于同一程序集内共享、但不对外公开的工具类或方法。

            //protected internal：需要同时支持继承和同程序集访问的基类方法。

            //private protected：非常严格，仅在极少数需要严控继承权限的场景下使用。
            public string Name;
            public int Age;
            public Student(string Name)
            {
                this.Name = Name;
            }
            public void Show()
            {
                Console.WriteLine($"{Name}---{Age}");
            }
        }




    }
}
