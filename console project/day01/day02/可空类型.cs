using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day02
{
    internal class 可空类型
    {
        public void ValueNull()
        {
            //可空数据类型 null 


            //就是可以让值类型可以存储一个null
            //比如: int? a = null;
            // 定义格式: 数据类型 变量= 值; 值只能取值范围之内的值 不可能还这个范围之外的值,也不能取到null
            // 定义格式 : 数据类型? 变量 = 值; 在原有范围之内额外增加了一个null

            int? a = 20;
           //?? : 运算符 会判断左边是否为null,如果为null则返回右边的值,否则返回左边的值
           //与可空类型进行计算时 需要增加判断
           int b =20;
            Console.WriteLine((a ??0)+b);

        }
    }
}
