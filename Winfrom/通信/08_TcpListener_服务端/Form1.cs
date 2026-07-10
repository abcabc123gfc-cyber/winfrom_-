using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _08_TcpListener_服务端
{
    public partial class Form1 : Form
    {
        TcpListener TcpListener = null;
        CancellationTokenSource cts = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 创建服务器

            var v = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (var item in v)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    //IPAddress ip = IPAddress.Parse("192.168.221.6");
                    //int port = 9999;
                    //建立创建服务器实例
                    TcpListener = new TcpListener(item, Convert.ToInt32(61000));

                }
            }
            //启动监听
            if (TcpListener != null) TcpListener.Start();
            //相当于 TcpListener.Listen(10);
            #endregion

            #region 接收数据
            //1.获取客户端的数据(循环获取多个客户端) 外层循环 线程
            //2. 接受数据(循环接受多个客户端的消息) 内层循环 线程
            cts = new CancellationTokenSource();
            //Task.Run(async () => { ... }) 就是在线程池上启动一个可取消的异步循环，用于持续接受 TCP 客户端连接，不会阻塞线程，并能响应取消信号。 这是实现高并发 TCP 服务器的常见模式之一。
            Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    //接收客户端
                    ////Pending()  判断是否有客户端连接  有 则返回true 
                    if (!TcpListener.Pending()) continue;
                    //AcceptTcpClient() 同步获取客户端
                    //  TcpClient tcpClient =  tcpListener.AcceptTcpClient();
                    //AcceptTcpClientAsync()  异步获取客户端
                    TcpClient tcpClient = await TcpListener.AcceptTcpClientAsync();
                    //解析数据
                    AccepData(tcpClient);

                }

            }, cts.Token);

            #endregion
        }
        //数据流
        NetworkStream networkStream = null;
        private void AccepData(TcpClient tcpClient)
        {
            Task.Run(async () =>
            {
                byte[] buffer = new byte[4096];
                while (!cts.IsCancellationRequested)
                {
                    if (!tcpClient.Connected || tcpClient.Available == 0) continue;

                    //读取或者写入数据有个前提: 获取流
                    //GetStream()  获取发送和接收数据的流对象

                     networkStream = tcpClient.GetStream();
                    {
                        //同步读取
                        //int count = stream.Read(buffer,0,buffer.Length);
                        int len = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                        Invoke(new Action(() =>
                        {
                            richTextBox1.AppendText($"来自：{tcpClient.Client.RemoteEndPoint}  {Encoding.UTF8.GetString(buffer, 0, len)}{Environment.NewLine}");
                        }));
                        //tcpClient.Client.LocalEndPoint 本地终结点 当前服务器的ip+port
                        //tcpClient.Client.RemoteEndPoint 远程终结点 客户端的ip+port
                        //if (len == 0)
                        //{

                        //    continue;
                        //}
                    }

                }
            }, cts.Token);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            networkStream.Close();
        }
    }
}
