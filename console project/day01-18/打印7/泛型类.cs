using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 打印7
{
    internal class 泛型类
    {

    }
    class Person<T>
    {
       public int ID { get; set;}
        public T language {  get; set;}

        // 参数 T 类型 是泛型列的泛型,并不是一个泛型方法
        public void SayHellow(T content)
        {
            Console.WriteLine(content);
        }
        
        //泛型方法 
        public void SayHellow<T>(T content, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(content);
            }
        }

        
         
    }
}
