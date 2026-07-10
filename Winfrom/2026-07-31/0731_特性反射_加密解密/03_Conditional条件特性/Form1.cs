
//#define Debug
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace _03_Conditional条件特性
{

    [SerializableAttribute]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Test1();
            Test2();
        }

        public static void Test1()
        {
            Console.WriteLine("Test1");
        }
        //ConditionalAttribute 条件特性  应用在方法上,让方法按照条件去编译
        //所有的特性 都是以Attribute 结尾  所以Attribute可以省略
        //Debug 就是一个编译符号 当定义了这个编译符号 才会编译Test2
        [ConditionalAttribute("Debug")]
        public static void Test2()
        {
            Console.WriteLine("Test2");
        }
    }

    
}
