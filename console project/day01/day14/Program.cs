using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //可空类型 k = new 可空类型();
            //k.Demo();
            按位与运算 an=new 按位与运算();
            unsafe
            {
                int a = 0, b = 0;
                int* pa = (int*)Unsafe.AsPointer(ref a);
                int* pb = &b;
                Console.WriteLine($"ref a 地址：0x{(ulong)new UIntPtr(pa):X}");
                Console.WriteLine($"普通b 地址：0x{(ulong)new UIntPtr(pb):X}");
                Console.WriteLine(a.GetHashCode() + " :a");
                Console.WriteLine(b.GetHashCode() + "  b");
                Console.WriteLine("方法调用");
                an.Test(ref a, b);

                ArrayList list = new ArrayList();   
                list.AddRange(new int[] { 1, 2, 3, 4, 5 });
                
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                //----------
                Console.Clear();
                
            }
        }
    }
}
