using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day01
{
    public partial class button_控件 : Form
    {
        public button_控件()
        {
            InitializeComponent();
        }
        public void button_1()
        {
            button1.Text = "点击";
            //设置大小
            button1.Size = new Size(100, 100);
            //设置对齐方式
            button1.TextAlign = ContentAlignment.MiddleCenter;

            //咱们现在是窗体应用程序,默认是没有控制台的,
            //如果需要控制台调试,在项目上,右键==>属性==>应用程序==>输入类型==>控制台应用程序


        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Console.WriteLine("鼠标移入了");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Console.WriteLine("鼠标离开了");
        }
    }
}
