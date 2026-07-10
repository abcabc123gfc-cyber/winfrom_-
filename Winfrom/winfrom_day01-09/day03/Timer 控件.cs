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
    public partial class Timer_控件 : Form
    {
        public Timer_控件()
        {
            InitializeComponent();
        }

        private void Timer_控件_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            //停止计时器
            timer.Stop();
            //启动
            timer .Start();
            //设置计时器间隔
            timer.Interval = 1000;
            //销毁
            timer.Dispose();
            //计时器事件
            timer.Tick += new EventHandler(timer_Tick);


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Label label1 = new Label();
            label1.Text=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
