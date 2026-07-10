using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace day11
{
    internal class 常量与只读变量
    {
        // 类的字段可以天津爱一个readonly 修饰符 表示只读字段
        //不能被构造函数之外的地方修改
        public readonly int a = 10;
        //静态只读 
        public static readonly int b = 20;
        //静态只读字段,可以在静态构造函数中赋值
        //常量, 给字段加一个Const 关键字,表示这个字段是常量
        public const int c = 30;

        
        //属性的只读,往往需要搭配一个私有字段访问
        private int _d;
        public int D { get; }



        public void Demo()
        { 

        }
    }
    class T
    {
        readonly string name;
        readonly static string Value;
        public T(string S)
        {
            name = S;
        }
        static T()
        {
            Value = "静态只读字段";
        }   

    }
}
