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
    public partial class TextBox控件 : Form
    {
        public TextBox控件()
        {
            InitializeComponent();
        }
        public void ShowTextBox()
        {
            // 设置TextBox控件的文本
            textBox1.Text = "这是TextBox控件";
            //设置多行输入
            textBox1.Multiline = true;
            //显示位置
            textBox1.Location = new Point(10, 10);
            //multiLine 为true时, 高度才可以生效
            textBox1.Height = 100;
            //设置密码输入
            textBox1.PasswordChar = '*';
            //设置一行最大输入长度
            textBox1.MaxLength = 10;
            //设置只读
            textBox1.ReadOnly = true;

            TextBox textBox2 = new TextBox()
            {
                Text = "这是TextBox控件",
                Multiline = true,
                Location = new Point(10, 120),
                Height = 100,
                PasswordChar = '*',
                MaxLength = 10,
                ReadOnly = true
            };
            Controls.Add(textBox2);
        }
    }
}
