using System;

namespace day14
{
    internal class 可空类型
    {
        // 可空类型  :  ?
        //值类型中的int类型 能够存储的数据范围是: -2147483648~2147483647

        //bool能够存储的数据范围是: true false
        //引用类型的变量: 处理可以存储对应的类型之外, 还可以存储null
        //null是一个特殊值, 表示没有任何数据, 也就是空值
        //所有引用类型的默认值是null
        public void Demo()
        {
            string str = "1";
            int? i = null;
            if (i.Equals(null))
            {
                Console.WriteLine("int 类型是null");
            }
            // ?? : 运算符, 如果左边的值是null, 就返回右边的值
            //格式: 变量名 ?? 默认值
            int j = i ?? 0;
        }
    }
}
