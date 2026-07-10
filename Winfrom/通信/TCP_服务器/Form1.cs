using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_服务器
{
    public partial class Form1 : Form
    {
        Socket socket;
        public Form1()
        {
            InitializeComponent();
        }
        CancellationTokenSource CancellationTokenSource;
        private void button1_Click(object sender, EventArgs e)
        {
            //1. 实例化服务器
            //使用socket 这个类型来创建服务器对象, 引入using System.Net.Sockets; 

            //参数1: 寻址方式 AddressFamily 枚举类型 InterNetwork ipv4  InterNetworkV6 ipv6

            //参数2:传输方案  字节流传输适合TCP协议
            //参数3: 网络连接协议
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #region 端口号
            //2. 服务端绑定端口, 进行监听 (IP+ port)
            string ip = "192.168.1.5";
            //端口范围是 0-65535 系统保留 0-1023
            //sqlserver 1433 mysql:3306 ftp:21 http: 80 https: 443

            //Console.WriteLine(IPEndPoint.MaxPort);//端口的最大值 65535
            //Console.WriteLine(IPEndPoint.MinPort);//端口的最小值


            int port = 65000;
            //EndPoint 抽象类不能实例化, 需要使用IPEndPoint子类实例化
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            #endregion
            #region 获取ip地址的方式
            //192.169.221.8 局域网中的一个ip地址,此局域网中其他的客户端,都能访问此服务器,
            //127.0.0.1表示本机
            //locahost 表示本机 会被转成127.0.0.1 如果咱们使用127.0.0.1,那么只有本机的客户端才能访问此服务器,局域网中的其他客户端无法访问此服务器

            string ipStr = string.Empty;

            //获取本机的ip地址
            //主机名 字符串
            //MessageBox.Show(Dns.GetHostName());
            //把主机名 映射 对应ip地址, 返回一个IPHostEntry对象, 包含了主机和多个ip地址
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            //for (int i = 0; i < iPHostEntry.AddressList.Length; i++)
            //{
            //    richTextBox1.Invoke(new Action(() =>
            //    {
            //        richTextBox1.AppendText(iPHostEntry.AddressList[i].ToString());
            //    })) ;
            //}

            //dut.WriteRegister($"LUT_Compensate_DAC_MOD_CH{lane}", lutData, lutData.Length); 

            iPHostEntry.AddressList.ToList().ForEach(x =>
            {
                if (x.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipStr = x.ToString();
                }

            });
            richTextBox1.Text = ipStr;
            #endregion



            socket.Bind(endPoint);

            #region 3. 启动服务
            //让服务器处于监听状态,等待客户端连接
            //参数表示服务器可以同时接受的客户端连接数, 如果超过这个数量, 其他客户端连接请求会被拒绝(启动了100个线程,来处理客户端连接请求)
            socket.Listen(10);
            //客户端=> 服务端 请求
            //服务端=> 客户端 响应 
            #endregion
            #region  4. 接受数据
            //接收请求的时候,需要考虑如下问题
            //等待的过程不能阻塞主线程, 防止UI卡死
            //客户端不断发送消息, 需要循环接收 
            //有多个客户端, 循环接受每一个客户端, 每个客户端循环接受消息
            //4.等待客户端发送过来的消息(接收客户端的请求),只要服务器处于监听状态(服务器已经启动),立即可以接收客户端请求
            //socket.Accept();
            Task.Run(() =>
            {
                while (true)
                {
                    //把接收客户端请求单独封装一个函数
                    //AccepDataSin();
                    socket.Accept();
                    AccepDataSin(socket);

                }


            });
            #endregion



        }

        private void AccepDataSin(Socket socket)
        {
            CancellationTokenSource = new CancellationTokenSource();

            Task.Run(() =>
            {
                //设置超时等待时间
                socket.ReceiveTimeout = 1000;
                //判断是否连接到客户端
                if (!socket.Connected) return;
                byte[] buffer = new byte[1024];
             
                while (!CancellationTokenSource.IsCancellationRequested)
                {
                    int bytesRead = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    if (bytesRead == 0)
                    {
                        // 对方已正常关闭连接
                        MessageBox.Show("1");
                        break;
                    }


                    // 将接收到的字节转为字符串（此处假设是 UTF-8 文本）
                    string chunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke(new Action(() =>
                    {
                        richTextBox2.AppendText(chunk);

                    }));


                }
                
            }, CancellationTokenSource.Token);
        }

        #region 单个客户端的情况以及方法说明
        //单个客户端的情况
        private void AccepDataSin()
        {
            //cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                // 获取客户端实例,通过这个实例,可以获取到客户端的信息
                //Socket client = server.Accept();
                /*
                 client.RemoteEndPoint 远程终结点 
                 client.LocalEndPoint 本地终结点
                 client.Available  可供读取的字节数
                  client.Connected 连接状态
                 client.AddressFamily 寻址方案  ipv4 ipv6
                 client.ProtocolType 协议类型 TCP UDP
                
                 client.ReceiveBufferSize 能够接收的最大字节数
                 client.SendBufferSize  能够发送的的最大字节数

                  client.ReceiveTimeout // 接收数据时的超时时间,单位毫秒,如果在指定的时间内没有接收到数据,就会抛出异常
                client.SendTimeout // 发送数据时的超时时间,单位毫秒,如果在指定的时间内没有发送完数据,就会抛出异常
                 */
                //循环接收,一个客户端,  多个请求
                //while (!cts.IsCancellationRequested)
                {
                    //byte[] buffer = new byte[client.Available];

                    //读取并接收客户端请求的数据,放到缓冲区(buffer字节数组中)
                    //   len === client.Available
                    //int len = client.Receive(buffer);

                    Invoke(new Action(() =>
                    {
                        //richTextBox1.Text += Encoding.UTF8.GetString(buffer);
                        //richTextBox1.AppendText(Encoding.UTF8.GetString(buffer));
                    }));
                }
            });
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            #region 发送给客户端

            #endregion
        }
    }
}
