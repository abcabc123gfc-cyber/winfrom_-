using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;

// 三菱MC协议（FX3U/Q/L/R系列以太网）
using HslCommunication.Profinet.Melsec;
namespace _05_03_HslCommunication_三菱
{
    public partial class Form1 : Form
    {
        MelsecMcNet melsecMc;
        OperateResult operateResult;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            operateResult =melsecMc.ConnectServer();
            if (operateResult.IsSuccess)
            {
                listBox1.Items.Add("连接成功");
            }
            else
            {
                listBox1.Items.Add("连接失败：" + operateResult.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            melsecMc=new MelsecMcNet("192.168.1.10",2001);
        }
        /// <summary>
        /// 读取按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var v = melsecMc.ReadBool("X0").Content;
            listBox1.Items.Add(v);
        }
        /// <summary>
        /// 开灯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            melsecMc.Write("M0", true);
            if (true==melsecMc.ReadBool("M0").Content)
            {
                listBox1.Items.Add("开灯成功");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            melsecMc.Write("M0", false);
           var v= melsecMc.ReadBool("X0").Content;
            if (v)
            {
               listBox1.Items.Add(v+"关灯失败");
            }
            else
            {

                listBox1.Items.Add(v + "关灯成功");

            }
        }
    }
}
