using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Socket_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //将监听到的socket保存在字典中
        Dictionary<string, Socket> socketDic = new Dictionary<string, Socket>();
        private void button1_Click(object sender, EventArgs e)
        {
            //参数1：地址族表明是否IPv4,参数2：套接字类型, 表明是流结构tcp,(TUDP是报结果，参数3：协议类型
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定IP和端口

            IPAddress ip = IPAddress.Parse(textBox2.Text);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(textBox1.Text));
            socket.Bind(point);
            ShowMessage("监听成功");
            //设置监听队列
            socket.Listen(10);
            Thread thread = new Thread(new ParameterizedThreadStart(Listen));
            thread.Start(socket);
        }
        void ShowMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(ShowMessage), new object[] { message });
            }
            else
            {
                //textBox3.Text += message + "\r\n";
                textBox3.AppendText(message + "\r\n");
            }
        }
        Socket socketSend;
        void Listen(object socket)
        {
            Socket socket1 = (Socket)socket;
            while (true)
            {
                //负责接受请求来自客户端,创建与客户端通信的socket
                socketSend = socket1.Accept();
                //Socket 添加到字典中
                socketDic.Add(socketSend.RemoteEndPoint.ToString(), socketSend);
                AddComboItem(socketSend.RemoteEndPoint.ToString());
                ShowMessage(socketSend.RemoteEndPoint.ToString() + "_连接成功");
                Thread th = new Thread(new ParameterizedThreadStart(Recive));
                th.Start(socketSend);
            }
        }
        //跨线程
        void AddComboItem(string item)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AddComboItem), new object[] { item });
            }
            else
            {
                comboBox1.Items.Add(item);
            }

        }
        /// <summary>
        /// 服务器接收数据
        /// </summary>
        /// <param name="socketSend1"></param>
        void Recive(object socketSend1)
        {
            Socket socketSend = (Socket)socketSend1;
            while (true)
            {

                //接受数据
                byte[] buffer = new byte[1024];
                //实际接收到的字节数
                int r = socketSend.Receive(buffer);
                if (r == 0)
                {
                    ShowMessage(socketSend.RemoteEndPoint.ToString() + "_连接断开");
                    socketSend.Close();
                    return;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, r);
                ShowMessage(socketSend.RemoteEndPoint.ToString() + ":" + str);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //发送数据
            //socketSend.Send(Encoding.UTF8.GetBytes(textBox4.Text));
            //获得用户在下拉框汇中选中的IP地址
            string ip = comboBox1.SelectedItem.ToString();
            socketDic[ip].Send(Encoding.UTF8.GetBytes(textBox4.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
    }
}
