using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04_客户端
{
    public partial class Form1 : Form
    {
        //1.如何创建一个客户端实例
        //2.客户端如何和服务器建立连接
        //3.客户端如何向服务器发送数据
        //4.客户端如何接收服务器响应

        public Form1()
        {
            InitializeComponent();
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            await InitClinet();
        }
        private async Task InitClinet()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #region 创建客户端实例
            //2.客户端client和服务器server 建立连接(三次握手)
            //远程终结点  就是服务器的身份  Ip+Port
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32(textBox2.Text));

            await socket.ConnectAsync(endPoint);
            //异步连接,不会阻塞主线程
            //socket.Connect();
            //同步连接,会阻塞主线程,会造成页面卡顿
            socket.ReceiveTimeout = 5000;

            //接受数据
            ReceiveDate(socket);
            //socket.Receive(buffer);

            #endregion


        }

        private void button3_ClickAsync(object sender, EventArgs e)
        {

        }
        private async void ReceiveDate(Socket socket)
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                Invoke(new Action(() =>
                {
                    listBox1.Items.Add(Encoding.UTF8.GetString(buffer));
                    textBox3.Text += Encoding.UTF8.GetString(buffer);
                }));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
