using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            引用类型_权限关键字 YinYong = new 引用类型_权限关键字();
            //YinYong.Reference();
            //YinYong.ValueType();
            可空类型 KongLei = new 可空类型();
            //KongLei.ValueNull();
            获取控制台输入 input = new 获取控制台输入();
            //input.consoleInput();
            类型转换 typeConvert = new 类型转换();
            typeConvert.TypeConvert();
        }
    }
}
