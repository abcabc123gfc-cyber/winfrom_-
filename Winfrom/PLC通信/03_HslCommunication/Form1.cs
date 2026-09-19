// 基础核心命名空间（必写）

// Modbus TCP/RTU 专用
using HslCommunication;
using HslCommunication.ModBus;
// 西门子S7 PLC（S7-200SMART/1200/1500/300）
using HslCommunication.Profinet.Siemens;
using System;
using System.Windows.Forms;

// 三菱MC协议（FX3U/Q/L/R系列以太网）

// 欧姆龙FINS

namespace _03_HslCommunication
{
    public partial class Form1 : Form
    {
        ModbusTcpNet modbusTcpNet;
        SiemensS7Net S7;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            modbusTcpNet = new ModbusTcpNet(textBox1.Text.Trim(), int.Parse(textBox2.Text.Trim()));
            modbusTcpNet.Station=1;
        }
        #region modbus
        private void button1_Click(object sender, EventArgs e)
        {
            OperateResult operate = modbusTcpNet.ConnectServer();
            if (!operate.IsSuccess)
            {
                MessageBox.Show("连接失败"+operate.Message);
            }
        }
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OperateResult operate = modbusTcpNet.Write("0", 12);
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //读取32位整数 0为起始地址
            OperateResult<int> operate = modbusTcpNet.ReadInt32("0");
            if (operate.IsSuccess)
            {
                MessageBox.Show("读取成功"+operate.Content.ToString());
            }
        }
        #endregion

        #region 西门子S7 PLC（S7-200SMART/1200/1500/300）
        private void button6_Click(object sender, EventArgs e)
        {
             S7 = new SiemensS7Net(SiemensPLCS.S1200,textBox1.Text.Trim());
            S7.ConnectTimeOut = 5000;
            var connect = S7.ConnectServer();
            if (!connect.IsSuccess)
            {
                MessageBox.Show("连接失败"+connect.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int value = S7.ReadInt32("DB50.DBD0").Content;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //注意方法重载 时的类型 
            int v = 1;
            S7.Write("DB50.DBD0", v);
        }
        #endregion


    }
}
