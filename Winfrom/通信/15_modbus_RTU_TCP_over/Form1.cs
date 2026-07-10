using Modbus.Device;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15_modbus_RTU_TCP_over
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient = null;
        UdpClient udpClient = null;
        ModbusMaster master = null;
        CancellationTokenSource cts = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIP.Text = ConfigurationManager.AppSettings["ServerIP"];
            txtPort.Text = ConfigurationManager.AppSettings["ServerPort"];
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnConn.Text == "连接")
                {
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(txtIP.Text), Convert.ToInt32(txtPort.Text));
                    if (raTCP.Checked)
                    {
                        //TCP 
                        tcpClient = new TcpClient();
                        tcpClient.Connect(iPEndPoint);
                        master = ModbusIpMaster.CreateIp(tcpClient);
                    }
                    else if (raUDP.Checked)
                    {
                        //UDP
                        udpClient = new UdpClient();
                        udpClient.Connect(iPEndPoint);
                        master = ModbusIpMaster.CreateIp(udpClient);

                    }
                    else if (raRTUOT.Checked)
                    {
                        //RTU Over TCP
                        tcpClient = new TcpClient();
                        tcpClient.Connect(iPEndPoint);
                        master = ModbusSerialMaster.CreateRtu(tcpClient);
                    }
                    else
                    {
                        //RTU Over UDP
                        udpClient = new UdpClient();
                        udpClient.Connect(iPEndPoint);
                        master = ModbusSerialMaster.CreateRtu(udpClient);
                    }



                    btnConn.Text = "断开";
                    txtIP.Enabled = txtPort.Enabled = false;

                }
                else
                {
                    btnConn.Text = "连接";
                    txtIP.Enabled = txtPort.Enabled = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            master.WriteSingleRegister(1, 0, 50);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "实时读取"  )
            {
                if(btnConn.Text != "连接")
                {
                    btnStart.Text = "实时读取";
                    cts?.Cancel();

                    Invoke(new Action(() =>
                    {
                        txtData1.Text = "";
                        txtData2.Text = "";
                        txtData3.Text = "";
                        txtData4.Text = "";
                    }));
                }
                cts = new CancellationTokenSource();
                Task.Run(() =>
                {

                    while (!cts.IsCancellationRequested)
                    {

                        ushort[] data = master.ReadHoldingRegisters(1, 0, 4);

                        if (data.Length != 4) continue;

                        Invoke(new Action(() =>
                        {
                            txtData1.Text = data[0].ToString();
                            txtData2.Text = data[1].ToString();
                            txtData3.Text = data[2].ToString();
                            txtData4.Text = data[3].ToString();
                        }));

                    }
                }, cts.Token);
                btnStart.Text = "停止读取";
            }
            else
            {
                MessageBox.Show("请先连接服务器");
                return;
            }
        }
    }
}
