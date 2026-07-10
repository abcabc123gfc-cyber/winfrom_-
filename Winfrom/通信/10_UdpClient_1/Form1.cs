using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10_UdpClient_1
{
    public partial class Form1 : Form
    {
        UdpClient udpClient;
        IPEndPoint ipEndPoint;
        CancellationTokenSource cts = null;
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
                    udpClient = new UdpClient(new IPEndPoint(item, Convert.ToInt32(61000)));
                    //B 端 UDP
                    ipEndPoint = new IPEndPoint(item, Convert.ToInt32(61001));
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
                    byte[] bytes = udpClient.Receive(ref ipEndPoint);
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
            udpClient.Send(Encoding.UTF8.GetBytes(textBox1.Text), textBox1.Text.Length, ipEndPoint);
        }
    }
}
