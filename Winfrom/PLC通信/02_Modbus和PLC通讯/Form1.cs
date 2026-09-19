using Modbus.Device;
using System;
using System.Net.Sockets;
using System.Windows.Forms;
namespace _02_Modbus和PLC通讯
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient = null;
        ModbusMaster modbusMaster = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        #region 连接
        private void button1_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(textBox1.Text.Trim(), int.Parse(textBox2.Text.Trim()));
            modbusMaster = ModbusIpMaster.CreateIp(tcpClient);
        }
        #endregion
        #region 读数据 / 写数据
        private void button2_Click(object sender, EventArgs e)
        {
            modbusMaster.WriteSingleRegister(1, 0, 1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            modbusMaster.ReadHoldingRegisters(1, 0, 1);
        }
        #endregion
    }
}
