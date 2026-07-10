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
using static System.Windows.Forms.AxHost;

namespace day07.线程池
{
    public partial class _04_线程池 : Form
    {
        public _04_线程池()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 创建线程池
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine(state);
            },"abc1");
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine(state);
            },"abc2");
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine(state);
            },"abc3");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //等待事件
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem(state =>
            {
                //分线程中代码是有序的 线程之间执行是无序的
                Dosomething(state.ToString());
                manualResetEvent.Set();
            },"abc");

            //阻塞主线程 等待Set() 执行 在执行主线程
            manualResetEvent.WaitOne();
            Console.WriteLine("主线程");

        }

        private static void Dosomething(object value)
        {
           Thread.Sleep(1000);
            Console.WriteLine(value);
        }
    }
}
