using _08_INi配置文件.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day05
{
    public partial class _08_INi配置文件 : Form
    {
        public _08_INi配置文件()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileIni.Write("用户信息", "ip", textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
           textBox2.Text= FileIni.Read("用户信息", "ip");
        }
    }
}
