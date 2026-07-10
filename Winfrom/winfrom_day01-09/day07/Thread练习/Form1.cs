using System;
using System.Threading;
using System.Windows.Forms;

namespace day07.Thread练习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread thread;
        /// <summary>
        /// 启动线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(() =>
             {
                 while (true)
                 {
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


             });
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread.Suspend();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            thread.Resume();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            thread.Abort();
            progressBar1.Value = 0;
        }
    }
}
