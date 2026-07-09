using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace socket_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket socket;
        private void button1_Click(object sender, EventArgs e)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(textBox2.Text), int.Parse(textBox1.Text)));
            ShowMessage("连接服务端成功");
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
                textBox3.AppendText(message + "\r\n");
            }
        }
        void Listen(object obj)
        {
            Socket socket = (Socket)obj;
            while (true)
            {
                byte[] buffer = new byte[1024];
                int r = socket.Receive(buffer);
                if (r == 0)
                {
                    textBox3.AppendText(socket.LocalEndPoint + ":" + "_连接断开");
                    socket.Close();
                    return;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, r);
                ShowMessage(str);
            }
        }
        /// <summary>
        /// 发送消息给服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string str = textBox4.Text.Trim();
            if (str.Length > 0)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);
                socket.Send(buffer);
            
            }
        }
    }
}
