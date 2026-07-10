using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day01
{
    public partial class 第一个窗体 : Form
    {
        //窗体的作用
        //1. 类的继承  2.接口的实现

        //默认情况下,同一个命名空间,只能有一个同名的类
        //partial 部分的 说明这两个类是同一个类,只不过分成了两部分
        //1. Form1.Designer.cs（自动生成部分） 2. Form1.cs（用户编写部分）
        //作用/; 1.方便维护 2.代码重用 3.方便分工
        public 第一个窗体()
        {
            InitializeComponent();
            lable_1();
        }
        public void lable_1()
        {
            label1.Image = Properties.Resources.屏幕截图_2026_06_07_160624;
            label1.AutoSize = false;
            //middle
            Button button = new Button()
            {
                Text = "点击",
            };
            this.Controls.Add(button);

            button.Click += action;
            label1.Click+=action;
        }

        private void action(object sender, EventArgs e)
        {
           MessageBox.Show("点击了");
        }
    }
}
