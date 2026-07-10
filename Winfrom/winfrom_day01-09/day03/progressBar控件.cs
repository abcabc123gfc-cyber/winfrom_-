using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day03
{
    public partial class progressBar控件 : Form
    {
        public progressBar控件()
        {
            InitializeComponent();
            button_click();
        }
        private void button_click()
        {
            progressBar1.Size=new Size(150,50);
            //设置进度条范围 最小值
            progressBar1.Minimum=0;
            //设置进度条范围 最大值
            progressBar1.Maximum=100;
            //设置进度条当前进度
            progressBar1.Value=0;
            while (progressBar1.Value<progressBar1.Maximum)
            {
                progressBar1.Value++;
                //Thread.Sleep(1000);
            }

        }

    }
}
