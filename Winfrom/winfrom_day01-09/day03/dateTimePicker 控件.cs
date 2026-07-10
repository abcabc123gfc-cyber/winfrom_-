using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day03
{
    public partial class dateTimePicker_控件 : Form
    {
        public dateTimePicker_控件()
        {
            InitializeComponent();
            dateTimePicker111();
        }
        public void dateTimePicker111()
        {
            dateTimePicker2.Format=DateTimePickerFormat.Custom;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //value 事件发生时触发
            dateTimePicker1.ValueChanged += dateTimePicker_ValueChange;
            //指示在选定日期左侧,显示复选框
            dateTimePicker1.ShowCheckBox = true;

        }

        private void dateTimePicker_ValueChange(object sender, EventArgs e)
        {
            //MessageBox.Show("时间改变");
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            
        }
    }
}
