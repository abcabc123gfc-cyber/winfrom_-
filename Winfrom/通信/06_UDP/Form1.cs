using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Helper;

namespace _06_UDP
{
    public partial class Form1 : Form
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        EndPoint endPoint;
        Dictionary<string, IPEndPoint> socketDic = new Dictionary<string, IPEndPoint>();
        SerialPort serialPort;
        bool isRead = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void InitUDP()
        {
            var v = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in v.AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    socket.Bind(new IPEndPoint(item, Convert.ToInt32(60000)));
                    break;
                }
            }

            Task.Run(() =>
            {
                byte[] buffer = new byte[1024];
                string strModbus;
                endPoint = new IPEndPoint(IPAddress.Any, 0);
                string str;
                while (true)
                {

                    int len = socket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPoint);
                    str = ($"来自：{endPoint}: {Encoding.UTF8.GetString(buffer, 0, len)}{Environment.NewLine}");
                    strModbus = (Encoding.UTF8.GetString(buffer, 0, len));

                    if (len == 0)
                    {
                        //MessageBox.Show("数据为空");
                        continue;
                    }
                    //moudbus 数据解析
                    ParseDataModbus(strModbus);
                    if (socketDic.ContainsKey(endPoint.ToString()))
                    {
                        socketDic[endPoint.ToString()] = (IPEndPoint)endPoint;
                    }
                    else
                    {
                        socketDic.Add(endPoint.ToString(), (IPEndPoint)endPoint);
                    }
                    Invoke(new Action(() =>
                    {

                        richTextBox1.AppendText($"{str}");
                        comboBox1.DataSource = socketDic.ToList();
                        comboBox1.DisplayMember = "Key";
                        comboBox1.ValueMember = "Value";
                    }));

                }

            });
        }
        /// <summary>
        /// 解析modbus数据
        /// </summary>
        /// <param name="strModbus"></param>
        private void ParseDataModbus(string strModbus)
        {
            var vArray = strModbus.Split(',');
            byte[] moudbusData;
            if (vArray.Length >= 3 && vArray[0] == "w")
            {
                isRead = false;
                moudbusData = new byte[8];

                moudbusData[0] = 0x01;
                //写入
                moudbusData[1] = 0x06;
                moudbusData[2] = (byte)(byte.Parse(vArray[1]) >> 8);
                moudbusData[3] = (byte)(byte.Parse(vArray[1]) & 0xFF);

                moudbusData[4] = (byte)(byte.Parse(vArray[2]) >> 8);
                moudbusData[5] = (byte)(byte.Parse(vArray[2]) & 0xFF);
                byte[] rc = CRCHelper.CRC16(moudbusData);
                moudbusData[6] = rc[0];
                moudbusData[7] = rc[1];
                serialPort.Write(moudbusData, 0, moudbusData.Length);
            }
            else if (vArray.Length >= 3 && vArray[0] == "r")
            {
                isRead = true;
                moudbusData = new byte[8];
                moudbusData[0] = 0x01;
                //MessageBox.Show("读数据");
                //读取
                moudbusData[1] = 0x03;
                moudbusData[2] = (byte)(byte.Parse(vArray[1]) >> 8);
                moudbusData[3] = (byte)(byte.Parse(vArray[1]) & 0xFF);

                moudbusData[4] = (byte)(byte.Parse(vArray[2]) >> 8);
                moudbusData[5] = (byte)(byte.Parse(vArray[2]) & 0xFF);

                byte[] rc = CRCHelper.CRC16(moudbusData);
                moudbusData[6] = rc[0];
                moudbusData[7] = rc[1];
                serialPort.Write(moudbusData, 0, moudbusData.Length);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;
            if (checkBox1.Checked)
            {
                foreach (var item in socketDic)
                {
                    endPoint = item.Value;
                    richTextBox1.Text += $"发送给：{endPoint}{Environment.NewLine}";
                    socket.SendTo(Encoding.UTF8.GetBytes(textBox1.Text), endPoint);
                }
            }
            else
            {
                if (socketDic.ContainsKey(((comboBox1.SelectedValue).ToString())))
                {
                    endPoint = socketDic[comboBox1.SelectedValue.ToString()];
                    richTextBox1.Text += $"发送给：{endPoint}{Environment.NewLine}";
                    socket.SendTo(Encoding.UTF8.GetBytes(textBox1.Text), endPoint);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort = new SerialPort("COM1");
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
                serialPort.DataReceived += SerialPort_WriteReceived;
            }
            InitUDP();



        }
        /// <summary>
        /// 串口接收数据    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_WriteReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!isRead) return;
            serialPort.ReadTimeout = 5000;

            //读取数据解析  需要判断
            MessageBox.Show("写入数据");
            byte[] bytes = new byte[serialPort.BytesToRead];
            //MessageBox.Show($"接收数据长度：{bytes.Length}");
            int len = serialPort.Read(bytes, 0, bytes.Length);
            int hsv = (bytes[3] << 8) + bytes[4];
            int wd = (bytes[3] << 8) + bytes[4];
            int dd = (bytes[5] << 8) + bytes[6];
            int ph = (bytes[7] << 8) + bytes[8];
            Invoke(new Action(() =>
            {

                richTextBox1.AppendText($"含水率：{hsv}  温度值：{wd}  电导率：{dd}  PH值：{ph}{Environment.NewLine}");
            }));


        }
    }
}
