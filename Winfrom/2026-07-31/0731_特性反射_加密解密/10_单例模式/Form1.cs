using _10_单例模式.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10_单例模式
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          textBox1.Text=  new Class1().Attach();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = new Class2().Attach();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText("a.txt", "吴亦凡");
            File.WriteAllText("a.txt", "罗志祥");
        }
    }
}
