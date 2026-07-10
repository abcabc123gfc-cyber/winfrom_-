
#define Debug  //定义一个编译符号 类似定义一个变量  #define 声明 
//#undef Log  //取消
using System;
using System.Windows.Forms;

namespace _02__预处理器指令
{
    //这个类可以序列化
    [Serializable]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

#if Log
            Console.WriteLine("吴亦凡");
#elif Debug
            Console.WriteLine("吴亦凡");
#else

#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
