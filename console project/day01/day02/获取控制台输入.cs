using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day02
{
    internal class 获取控制台输入
    {
        /// <summary>
        /// 获取控制台输入
        /// </summary>
        public void consoleInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine(key.Key);
            
        }
    }
}
