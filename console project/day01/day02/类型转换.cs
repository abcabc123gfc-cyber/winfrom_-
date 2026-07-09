using System;
using System.Text;

namespace day02
{
    internal class 类型转换
    {
        public void TypeConvert()
        {
            //类型转换: 不同类型之间进行相互转换
            //1. 强制转换: 强制转换会进行数据类型的转换,但是转换失败会报错
            //2. 隐式转换: 隐式转换会进行数据类型的转换,转换成功,转换失败不会报错

            //小范围转大范围
            //同级之间不能转换
            //无符号可以转换有符号



            Console.OutputEncoding = Encoding.UTF8;
            //int a = 10;
            //double b = a;

            int a = 232348493;
            uint b = Convert.ToUInt32(a);

            //b = a;

            //小数转换
            //float < double
            float c = 10.5f;
            double d = c;
            //整数直接转换小数
            d = a;
            char c1 = Convert.ToChar(3);
            //Console.WriteLine(c1);
            //Console.WriteLine((int)c1);

            #region 隐式类型转换
            //显式类型转换 : 大范围转小范围 
            //int > char

            //Console.WriteLine((int)c1);
            //int.parse();
            //Console.WriteLine(Convert.ToInt32(c1));
            #endregion
            Console.WriteLine("😄");


            //其他类型 转字符串
            // toString()
             string s2=a.ToString();
            //保留一位小数
            Console.WriteLine(a.ToString("F1"));
            //保留两位小数
            Console.WriteLine(a.ToString("F2"));
            //三位分割
            Console.WriteLine("三位分割");
            Console.WriteLine(a.ToString("N3"));
            Console.WriteLine(a.ToString("n"));
            Console.WriteLine("...");
            //加上￥
            Console.WriteLine(a.ToString("C"));

            //字符串转换为其他字符
            //int n1=int.Parse("123");

        }
    }
}
