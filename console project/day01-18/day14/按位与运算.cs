using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace day14
{
    internal class 按位与运算
    {
        //当双方都是布尔表达式时,&& || != 会变成逻辑运算符

        // && || !=  当双方都是十进制数据时变成按位运算
        
        //按位与运算: 对两个数   a

        public void Test( ref int a,int b)
        {
            unsafe
            {
                int* pa = (int*)Unsafe.AsPointer(ref a);
                int* pb = &b;
                Console.WriteLine($"ref a 地址：0x{(ulong)new UIntPtr(pa):X}");
                Console.WriteLine($"普通b 地址：0x{(ulong)new UIntPtr(pb):X}");
                Console.WriteLine($"a值={*pa}, b值={*pb}");
            }
        }
        
    }
}
