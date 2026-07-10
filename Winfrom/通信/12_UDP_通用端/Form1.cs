using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 通用端
{
    public partial class Form1 : Form
    {

        UdpClient udpClient = null;
        CancellationTokenSource cts = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (button1.Text == "启动")
                {

                    IPEndPoint locaEP = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));

                    udpClient = new UdpClient(locaEP);

                    this.Text = $"当前端的ip和port:[{locaEP}]";

                    button1.Text = "停止";



                    cts = new CancellationTokenSource();

                    Task.Run(() =>
                    {

                        while (!cts.IsCancellationRequested)
                        {

                            if (udpClient.Available == 0) continue;
                            {
                                IPEndPoint sendEndPoint = new IPEndPoint(IPAddress.Any, 0);
                                //返回值是另一端发送的数据
                                //输入参数 另一端的身份
                                byte[] buffer = udpClient.Receive(ref sendEndPoint);

                                Invoke(new Action(() =>
                                {
                                    string msg = Encoding.UTF8.GetString(buffer);
                                    richTextBox1.Text += $"{sendEndPoint}:::{msg}" + Environment.NewLine;
                                }));


                            }

                        }

                    },cts.Token);
                }


                else
                {
                    button1.Text = "启动";
                    cts.Cancel();
                    udpClient.Close();
                    udpClient = null;   
                  }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bytes  = Encoding.UTF8.GetBytes(textBox3.Text);


            IPAddress ip = IPAddress.Parse(textBox4.Text.Split(':')[0]);
            int port = int.Parse(textBox4.Text.Split(':')[1]);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            udpClient.Send(bytes, bytes.Length, endPoint);

        }
    }
}
