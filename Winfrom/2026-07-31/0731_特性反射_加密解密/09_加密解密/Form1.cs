using EncryptTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace _09_加密解密
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text= AESHelper.Encrypt(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = AESHelper.Decrypt(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = DESHelper.Encrypt(textBox1.Text);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = DESHelper.Decrypt(textBox2.Text);
        }
    }
}
