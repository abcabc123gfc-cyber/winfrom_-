using System;
using System.Collections;

namespace day10
{
    internal class 装箱与拆箱
    {
        // 装箱与拆箱
        // 装箱：把值类型的数据转换成对象类型
        // 拆箱：把对象类型转换成值类型

        public void BoxingDemo()
        {
            int a = 10;
            // 装箱
            object obj = a;
            //拆箱
            Console.WriteLine((int)obj);

            ArrayList list = new ArrayList();
            //装箱
            list.Add(a);
            //拆箱
            Console.WriteLine((int)list[0]);

            // 错误示范：不能直接把装箱的int转成string，会抛InvalidCastException
            // string str = (string)list[0];  // 这行会报错！

            // 正确做法：先拆箱回int，再用ToString()转字符串
            int num = (int)list[0];  // 先拆箱
            string str = num.ToString();  // 再转字符串
            Console.WriteLine("拆箱后转字符串：" + str);

        }
    }
}
