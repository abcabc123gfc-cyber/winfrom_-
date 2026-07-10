using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day09.自定义用户控件
{
    public partial class 自定义控件 : Form
    {
        public 自定义控件()
        {
            InitializeComponent();
        }

        private void userControl_自定义1_MyEvent(object sender, EventArgs e)
        {
            MessageBox.Show("自定义控件触发了事件"+ userControl_自定义1.ID);
        }
    }
}
