using System;
using System.Text;

namespace day01
{
    internal class 控制台常用方法
    {
        // 控制台常用方法
        public void function()
        {
            //向控制台输出内容
            Console.WriteLine("hello world");
            //通过控制台扬声器播放声音
            //Console.Beep();
            //通过扬声器播放具有制定频率和持续时间的提示音
            //参数1：频率 37~32767 赫兹之间 2：持续时间 毫秒
            //Console.Beep(500, 500);
            //获取当前控制台的编码格式
            Console.WriteLine(Console.OutputEncoding.EncodingName); 
            Console.ReadKey();
            //设置当前控制台的编码格式
            Console.OutputEncoding = Encoding.UTF8;
            //获取用户输入的内容
            Console.WriteLine("请输入内容：");
            string input = Console.ReadLine();
            Console.WriteLine(input + "_输入的内容");
            //获取用户输入的键
            Console.WriteLine("请输入一个键：");
            char key = (char)Console.Read();
            Console.WriteLine(key + "_输入的键");


        }
        public void function2()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //输出一段内容不换行
            Console.Write("hello world");
            Console.WriteLine("_不换行");
            //输出一段内容并换行
            Console.Write("_换行+\\n\\r换行\n\r");
            Console.Write("hello world");
            //() 方法的调用 ():执行运算符
        }
        public void function3()
        {
            Console.OutputEncoding = Encoding.UTF8;
            /*属性: 记录信息
            通过属性名称获取属性值   属性名称.设置属性值 = 属性值 进行属性设置
            
            获取控制台背景色*/
            Console.WriteLine(Console.BackgroundColor + "_背景色");
            //设置控制台背景色
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("hello world");
            Console.ReadKey();
            Console.BackgroundColor = ConsoleColor.Black;
            //获取前景色
            var foregroundColor = Console.ForegroundColor;
            Console.WriteLine(foregroundColor + "_前景色");
            //设置前景色
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("hello world");
            Console.ReadKey();
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine("hello world");
            /*
              方法: 一个行为,使用()触发
                
             */
        }
        public void ProjectProperties()
        {
            //变量: 就是一个可以更改的量
            //常量: 值不能改变

            //变量: 是存储数据的容器,存储的数据类型可以改变
            //声明变量的格式 var 变量名 = 值;
            var name = "hello world";
            //常用的数据类型 string int float double bool char
            // 变量名 要做到见名知意

            //静态变量: 静态变量属于类,类中所有的对象都可以访问静态变量f
            //可以声明多个变量
            string name1 = "hello world", name2 = "hello world";

            //const: 常量
            const double PI = 3.14;

            //字符: 一个字符占两个字节
            //占位符输出
            Console.WriteLine("PI的值{0}", PI);

            //插值语法
            Console.WriteLine($"PI的值{PI}");

        }
        /// <summary>
        /// 值类型
        /// </summary>
        public void DerivedValue()
        {
            //值类型 : 基本数据类型
            //值类型的变量保存的是这个值本身


            #region 整数数据类型
            //   整数数据类型
            //8位无符号整数类型 范围0-255 字节类型 占用1字节
            byte a = 255;
            //8位有符号整数类型 范围-128-127 字节类型 占用1字节
            sbyte b = -128;

            //16位无符号整数类型 范围0-65535 字节类型 占用2字节
            ushort c = 65535;
            //16位有符号整数类型 范围-32768-32767 字节类型 占用2字节

            //32位无符号整数类型 范围0-4294967295 字节类型 占用4字节
            uint d = 4294967295;
            //32位有符号整数类型 范围-2147483648-2147483647 字节类型 占用4字节
            int e = -2147483648;

            //64位无符号整数类型 范围0-18446744073709551615 字节类型 占用8字节
            ulong f = 18446744073709551615;
            //64位有符号整数类型 范围-9223372036854775808-9223372036854775807 字节类型 占用8字节
            long g = -9223372036854775808;
            #endregion

            #region 浮点数数据类型
            //32位单精度类型 浮点数 占用4字节
            float h = 3.14f;
            //64位双精度类型 浮点数 占用8字节
            double i = 3.14;

            //128位精度类型 浮点数
            decimal j = 3.14m;

            #endregion

            #region 布尔数据类型
            //布尔数据类型
            bool k = true;
            #endregion

            #region 字符数据类型
            //字符数据类型
            //只能存储一个字符
            char l = 'a';
            char m = '\u4e00';
            char n = '\u4e01';
            char zh = '中';
            #endregion
            Console.WriteLine("{0},{2},{3},{3}",l,m,n,zh);
            



        }

        #region 枚举类型
        //限制一个值只能是固定的几种值
        //枚举的使用
        //1 定义一个枚举只能定义在类中
        //2 定义变量 ,设置变量的类型为这个枚举类型

        //定义一个个枚举 ,名字以大写开头
        /*
         枚举格式
        public enum Gender
        {
            Male,
            Female
        }
         */
        //枚举成员的默认值为0,枚举成员之间用逗号隔开
        //枚举成员值可以自定义
        public enum Gender
        {
            Male = 1,
            Female = 2
        }
        enum Color
        {
            Red,
            Green,
            Blue
        }
        #endregion

    }
}

