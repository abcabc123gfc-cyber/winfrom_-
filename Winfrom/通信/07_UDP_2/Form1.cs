using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07_UDP_2
{
    public partial class Form1 : Form
    {
        Socket socket = null;
        EndPoint iPEndPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var v = Dns.GetHostEntry(Dns.GetHostName());
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            foreach (var item in v.AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    iPEndPoint = new IPEndPoint(item, Convert.ToInt32(60001));
                    try
                    {
                        socket.Bind(iPEndPoint);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("端口被占用");
                        return;
                    }

                    iPEndPoint = new IPEndPoint(item, Convert.ToInt32(60000));
                }
            }

            Task.Run(() =>
            {
                byte[] buffer = new byte[1024];

                StringBuilder stringBuilder = new StringBuilder();

                while (true)
                {

                    //0, buffer.Length, SocketFlags.None, 
                    int len = socket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref iPEndPoint);
                    if (len == 0) continue;
                    //comboBox1.Items.Add(iPEndPoint.ToString());

                    stringBuilder.AppendLine(Encoding.UTF8.GetString(buffer, 0, buffer.Length));
                    //MessageBox.Show(Encoding.UTF8.GetString(buffer, 0, buffer.Length));
                    Invoke(new Action(() => richTextBox1.AppendText($"来自：{iPEndPoint}:  {stringBuilder}{Environment.NewLine}")));
                    stringBuilder.Clear();
                }

            });

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (socket == null)
            {
                button1_Click(null, null);
            }
            socket.SendTo(Encoding.UTF8.GetBytes(textBox1.Text), iPEndPoint);
        }
    }
}
