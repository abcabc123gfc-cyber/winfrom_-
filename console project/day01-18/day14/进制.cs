using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace day14
{
    internal class 进制
    {
        //2进制数据 0-1 逢二进一
        int a = 0B0010101;
        //8进制数据 逢8进一
       
        //16进制 逢16进制1
        int b= 0X776601;

        public void Test()
        {
            string str = "1111";
            Convert.ToInt32(str, 8);
        }


        
    }
}
