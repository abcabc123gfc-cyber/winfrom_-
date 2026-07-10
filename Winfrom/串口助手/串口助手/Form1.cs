using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 串口助手
{
    public partial class Form1 : Form
    {
        //实例化 串口对象
        SerialPort SerialPort = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string s=SerialPort.ReadExisting();
            richTextBox2.Invoke( new Action(() => richTextBox2.AppendText(s)) );
        }

        /// <summary>
        /// 串口初始化, 建立连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //窗口通信级建立连接, 需要以来 serialprot 对象

            //1. 实例化 Serial Prot 是 winform 中的一个组件
            //1.1 可以从工具箱中拖拽
            //1.2 可以new
            
            //2 配置串口参数 串口名称 , 波特率 校验位 数据位 停止位
            SerialPort.PortName = textBox1.Text;

            //3 打开串口
            if (SerialPort.IsOpen == false)
            {
                SerialPort.Open();
                richTextBox3.Text = "串口已打开";
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s=richTextBox1.Text;
            SerialPort.Write(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
                richTextBox3.Text = "串口已关闭";
            }
        }
    }
}
