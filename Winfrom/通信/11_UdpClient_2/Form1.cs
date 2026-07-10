using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11_UdpClient_2
{
    public partial class Form1 : Form
    {
        UdpClient udpClient;
        IPEndPoint iPEndPoint;
        CancellationTokenSource cts;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var v = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in v)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {

                    udpClient = new UdpClient(new IPEndPoint(item, Convert.ToInt32(60000)));
                    //A 端 UDP
                    iPEndPoint = new IPEndPoint(item, Convert.ToInt32(61000));
                    break;
                }
            }
            //创建task 取消令牌
            cts = new CancellationTokenSource();

            Task.Run(() =>
            {

                while (!cts.IsCancellationRequested)
                {
                    if (udpClient.Available == 0) continue;
                    byte[] bytes = udpClient.Receive(ref iPEndPoint);
                    textBox1.Invoke(new Action(() =>
                    {
                        richTextBox1.AppendText($"{Encoding.UTF8.GetString(bytes)}{Environment.NewLine}");
                    }));
                }
            }, cts.Token);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (udpClient == null) return;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) { return; }
            udpClient.Send(Encoding.UTF8.GetBytes(textBox1.Text), textBox1.Text.Length, iPEndPoint);
        }
    }
}

