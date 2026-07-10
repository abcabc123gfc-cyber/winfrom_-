using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 练习TCP客户端_modbus
{
    public partial class Form1 : Form
    {
        Socket server = null;
        Socket client = null;
        bool isConnect = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                client.Close();
                client.Dispose();
            }

            EndPoint remoteEP = new IPEndPoint(IPAddress.Parse(textBox3.Text.Trim()), Convert.ToInt32(textBox4.Text.Trim()));
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(remoteEP);
            isConnect = true;
            //将客户端添加到列表中
            comboBox1.Items.Add(remoteEP.ToString());
            Task.Run(() =>
            {
                byte[] buffer = new byte[4096]; // 固定大小缓冲区
                while (true)
                {
                    try
                    {
                        //server = client.Accept();
                        int len = client.Receive(buffer);
                        if (len == 0) // 对端关闭连接
                        {
                            continue;
                        }
                        string received = Encoding.UTF8.GetString(buffer, 0, len);
                        //Encoding.UTF8.get(buffer, 0, len);
                        //MessageBox.Show(received.GetType().ToString()); string
                        Invoke(new Action(() => textBox1.AppendText(received)));
                        //System.UInt16[]

                    }
                    catch (SocketException ex)
                    {
                        // 处理异常（如连接重置）
                        Invoke(new Action(() => textBox1.AppendText($"接收错误: {ex.Message}")));
                        break;
                    }
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                return;
            }
            if (client == null)
            {
                return;
            }
            client.Send(Encoding.UTF8.GetBytes(textBox2.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                return;
            }
           
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "支持的文件|*.txt;*.xlsx;*.jpg;*.bmp";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "请选择文件";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取文件名
                    string fileName = openFileDialog.SafeFileName;
                    //MessageBox.Show(fileName);
                    using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        byte[] buffer = new byte[4096];
                        byte[] buffer2 = new byte[4];
                        byte[] bufferLength = new byte[8];
                        StringBuilder stringBuilder = new StringBuilder();
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            //传递文件名字节数
                            buffer2 = BitConverter.GetBytes(Encoding.UTF8.GetBytes(fileName).Length);
                            client.Send(buffer2, 0, buffer2.Length, SocketFlags.None);
                            //传递文件名
                            byte[] bytes = new byte[buffer2.Length];
                            bytes = Encoding.UTF8.GetBytes(fileName);
                            textBox1.AppendText(Encoding.UTF8.GetString(bytes));
                            client.Send(bytes, 0, bytes.Length, SocketFlags.None);
                            //发送文件长度
                            bufferLength = BitConverter.GetBytes(fileStream.Length);
                            client.Send(bufferLength, 0, bufferLength.Length, SocketFlags.None);

                            while (true)
                            {
                                int len = reader.Read(buffer, 0, buffer.Length);

                                //  var bytes = reader.ReadBytes(buffer.Length);
                                if (len <= 0) break;
                                //for (int i = 0; i < len; i++)
                                //{
                                //    stringBuilder.Append(buffer[i].ToString("X2"));
                                //    stringBuilder.Append(" ");
                                //}
                                client.Send(buffer, 0, len, SocketFlags.None);
                            }

                            client.Shutdown(SocketShutdown.Send);
                            //Invoke(new Action(() => textBox1.AppendText($"{stringBuilder} ")));
                            Invoke(new Action(() => textBox1.AppendText($"发送完毕")));
                            stringBuilder.Clear();
                        }
                    }

                    //获取文件字节流, 后续使用 二进制读取, 用buffer[4096] 创建缓冲区 循环读取
                    //textBox2.Text = fileStream.Length.ToString();
                    //byte[] buffer = new byte[4096];
                    //while (fileStream.Read(buffer, 0, buffer.Length) > 0)
                    //{ 
                    //    client.Send(buffer);
                    //}
                }
         
            //client.SendFile(filePath);

        }
    }
}
