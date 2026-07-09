using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_联合Vision_pro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Click+=test;
        }
        // 定义事件 
        public event EventHandler Click;
        private void button1_Click(object sender, EventArgs e)
        {
            if (Click != null)
            {
                // 调用事件
                MessageBox.Show("点击了按钮");
                Click(this, e);
            }
        }
       

        private void test(object sender, EventArgs e)
        {
           MessageBox.Show("添加了一次方法");
        }
    }
   
}
