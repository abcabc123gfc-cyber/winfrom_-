using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _14_UDP_多播_客户端
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
            int port = new Random().Next(60000, 64000);
            foreach (var item in v)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    //IPAddress ip = IPAddress.Parse("192.168.221.6");
                    //int port = 9999;
                    //建立创建客户端实例
                    udpClient = new UdpClient(new IPEndPoint(item, port));
                    pEndPoint=new IPEndPoint(item, 0);
                    textBox1.Text = item.ToString();
                    textBox2.Text = port.ToString();
                    break;
                }
            }
            Task.Run(() =>
            {
                while (true)
                {
                    //接收数据
                    var bytes = udpClient.ReceiveAsync().Result;
                    //显示数据
                    textBox1.Text = Encoding.UTF8.GetString(bytes.Buffer);
                }
            });

        }
    }
}
