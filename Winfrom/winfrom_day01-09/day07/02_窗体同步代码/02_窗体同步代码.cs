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

namespace day07
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //斐波那契数列
            //Fn1(20);
            label1.Text = Fn1(20).ToString();
        }
        private int Fn1(int v)
        {
            if (v <= 1)
            {
                return v;
            }
            else
            {
                return Fn1(v - 1) + Fn1(v - 2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Process.Start("https://www.baidu.com");
            
            Process.Start("D:\\Tencent\\QQNT\\QQ.exe");
        }
    }
}
