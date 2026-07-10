using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04_Obsolete废弃特性
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Test1();
            Test2();
        }

        [Obsolete("这个方法已经被废弃了,建议使用Test2方法代替",true)]
        public static void Test1()
        {
            Console.WriteLine("Test1");
        }
        
        public static void Test2()
        {
            Console.WriteLine("Test2");
        }
    }
}
