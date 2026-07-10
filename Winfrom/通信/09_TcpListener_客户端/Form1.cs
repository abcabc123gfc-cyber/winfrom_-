using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _09_TcpListener_客户端
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient;
        CancellationTokenSource cts = null;
        public Form1()
        {
            InitializeComponent();
        }
        //tcpClient.Client.LocalEndPoint 本地终结点 当前客户端的ip+port
        //tcpClient.Client.RemoteEndPoint 远程终结点 服务器的ip+port
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void AccepRequest()
        {
            cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    if (!tcpClient.Connected || tcpClient.Available == 0) continue;


                    NetworkStream stream = tcpClient.GetStream();

                    byte[] buffer = new byte[tcpClient.Available];

                    //同步读取
                    //int count = stream.Read(buffer,0,buffer.Length);
                    int count = await stream.ReadAsync(buffer, 0, buffer.Length);

                    //解析数据并显示

                    Invoke(new Action(() =>
                    {
                        string msg = Encoding.UTF8.GetString(buffer);
                        string ipPort = tcpClient.Client.RemoteEndPoint.ToString();
                        richTextBox1.Text += $"{ipPort}:::::{msg}{Environment.NewLine}";
                    }));
                }
            }, cts.Token);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(textBox1.Text);
            if (textBox1.Text == "" || tcpClient == null) return;
            if (!tcpClient.Connected) return;
            tcpClient.GetStream().Write(bytes, 0, bytes.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 创建客户端

            var v = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in v)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    //IPAddress ip = IPAddress.Parse("192.168.221.6");
                    IPAddress ip = IPAddress.Parse("192.168.1.5");
                    //int port = 9999;
                    //建立创建服务器实例

                    tcpClient = new TcpClient(new IPEndPoint(ip, Convert.ToInt32(61000)));
                    tcpClient.Connect(ip, Convert.ToInt32(60000));
                }
            }
            #endregion
            #region 接收数据
            AccepRequest();
            #endregion
        }
    }
}
