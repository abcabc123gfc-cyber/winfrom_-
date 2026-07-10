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

namespace Nmodbus通信
{
    public partial class Form1 : Form
    {
        //使用modbus 通信
        //安装 Nmodbus.Serial 包 提供串口通信支持
        public Form1()
        {
            InitializeComponent();
            
            SerialPort serialPort = new SerialPort("COM1", 9600,Parity.None,8,StopBits.One);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
