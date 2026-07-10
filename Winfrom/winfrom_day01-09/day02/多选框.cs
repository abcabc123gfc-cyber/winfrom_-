using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day02
{
    public partial class 多选框 : Form
    {
        public 多选框()
        {
            InitializeComponent();
        }
        public void test()
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = "多选框";
            Controls .Add(checkBox);
            checkBox.Size = new Size(100, 20);
            checkBox.CheckedChanged += checked_change;
        }

        private void checked_change(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show("多选框被选中_{0}",checkBox1.Text);
            }
            else
            {
                MessageBox.Show("多选框取消选中");
            }
        }
    }
}
