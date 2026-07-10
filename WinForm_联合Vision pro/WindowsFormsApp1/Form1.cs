using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += MyButton_Click;
        }

        private void MyButton_Click(object sender, EventArgs e)
        {
         this.button1.Text = "点击了按钮";
        }

        private void groupBox1_Layout(object sender, LayoutEventArgs e)
        {

        }
    }
}
