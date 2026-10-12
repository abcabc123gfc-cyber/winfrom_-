using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 反射_Core_01_主程序_SDK;

namespace 反射_Core_02_类库
{
    [Unfinished]
    public  class Cat
        :IAnimal
    {
        public void Voice(int index)
        {
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine("Meow");
            }
        }
    }
}
