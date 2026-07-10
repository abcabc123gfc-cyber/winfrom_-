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
    public partial class 事件驱动机制 : Form
    {
        public 事件驱动机制()
        {
            InitializeComponent();
        }
        public void button_1()
        {
            //  this.button1.Click += new System.EventHandler(this.button1_Click);

            //click 就是一个事件, 点击的时候会执行, 本质是一个特殊的多播委托
            //public event EventHandler Click  //事件
            //public delegate void EventHandler(object sender, EventArgs e); 委托

            //使用+=将事件和事件处理程序,绑定到一块,当找个事件触发的时候,就会调用这个事件处理程序

            //click 点击的时候会触发
            //Load 加载的时候会触发

            button1.Click += (object sender, EventArgs e) =>
            {
                MessageBox.Show("点击了");
            };
            button1.Click += (sender, e) =>
            {
                MessageBox.Show("点击了");
            };
        }
        //事件处理器
        private void Btn_Name(object sender, EventArgs e)
        {
            Console.WriteLine("执行了");
        }
    }
}
