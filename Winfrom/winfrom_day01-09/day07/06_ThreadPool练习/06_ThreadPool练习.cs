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

namespace day07._06_ThreadPool练习
{
    public partial class _06_ThreadPool练习 : Form
    {
        public _06_ThreadPool练习()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(UpadtePropgreaaBar,0);
        }

        private void UpadtePropgreaaBar(object state)
        {
            while (true) { 
            
            Thread.Sleep(100);
                Invoke(new Action(() =>
                {
                    if (progressBar1.Value >= 100)
                    {
                        progressBar1.Value = 0;
                    }
                    progressBar1.Increment(1);
                }));
            }
        }
    }
}
