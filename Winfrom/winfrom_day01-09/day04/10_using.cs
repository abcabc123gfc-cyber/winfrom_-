using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using aa=System.Threading.Tasks;

namespace day04
{
    internal class _10_using
    {
        public void UsingTest()
        {
            //using 语句作用:
            //1. 引用命名空间
            //2. 释放资源
            //3. 别名
            //using aa= System.Threading.Tasks;

            using (FileStream fs = new FileStream("D:\\1.txt", FileMode.OpenOrCreate))
            {
                
            }

        }
    }
}
