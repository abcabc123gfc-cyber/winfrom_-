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
    public partial class NumericUpDown控件 : Form
    {
        public NumericUpDown控件()
        {
            InitializeComponent();
            NumericUpDown控件_Load();
        }
        public void NumericUpDown控件_Load()
        {
            //最小数字
            numericUpDown1.Minimum = 0;
            //最大数字
            numericUpDown1.Maximum = 1000;
            //初始数字
            numericUpDown1.Value = 500;
            //步进 依次减去或加的数字
            numericUpDown1.Increment = 5;
            //小数位数
            numericUpDown1.DecimalPlaces = 2;
            //千分位
            numericUpDown1.ThousandsSeparator = true;
            //文本对齐
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            //设置前景颜色
            numericUpDown1.ForeColor = Color.Red;
            //设置背景颜色
            numericUpDown1.BackColor = Color.Yellow;
            //边框样式 
            numericUpDown1.BorderStyle = BorderStyle.FixedSingle;
            //valueChange 值发生改变的时候执行
            numericUpDown1.ValueChanged += action;
        }

        private void action(object sender, EventArgs e)
        {
          MessageBox.Show(numericUpDown1.Value.ToString());
        }
    }
}
