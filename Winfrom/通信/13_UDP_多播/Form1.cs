using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _13_UDP_多播
{
    public partial class Form1 : Form
    {
        UdpClient udpClient = null;
        IPEndPoint pEndPoint = null;
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
                   
                    //建立创建客户端实例
                    pEndPoint = new IPEndPoint(item, 62000);
                    udpClient = new UdpClient(pEndPoint);
                    textBox1.Text = item.ToString();
                    textBox2.Text = 60000.ToString();
                    break;
                }
            }
            Task.Run(() =>
            {
                while (true)
                {
                    //接收数据
                    var bytes = udpClient.Receive(ref pEndPoint);
                    //显示数据
                    richTextBox1.Text = Encoding.UTF8.GetString(bytes);
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pEndPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 60011);
            //连接
            udpClient.Connect(pEndPoint);
            // 务必开启广播选项
            udpClient.EnableBroadcast = true;
            byte[] bytes = Encoding.UTF8.GetBytes(richTextBox1.Text);
            udpClient.Send(bytes, bytes.Length);
        }
    }
}
