using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 练习_TCP_modbus
{

    #region 思路
    //我发送给服务端的应该是一个" 触发字符,byte[] 字节" 通过切割来判断, 然后执行回调函数

    #endregion

    public partial class Form1 : Form
    {
        private Socket listeningSocket;
        //记录服务器已创建
        bool IsBound = false;
        //记录最新的客户端, 并于串口modbus通信
        private Socket clientSocket;
        public delegate ushort[] ModbusEventHandler(byte[] bytes);
        ModbusEventHandler Writemodbus = null;
        ModbusEventHandler Readmodbus = null;
        SerialPort serialPort = new SerialPort("COM1");
        ModbusMaster modbusMaster;
        //定义列表来存储连接
        List<Socket> clientSockets = new List<Socket>();
        //定义取消令牌
        CancellationTokenSource _cts;
        public Form1()
        {
            InitializeComponent();
            Writemodbus += WriteModbus;
            Readmodbus += ReadModbus;
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            //初始化modbus与串口 
            modbusMaster = ModbusSerialMaster.CreateRtu(serialPort);

        }
        /// <summary>
        /// 读取数据 回调方法
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private ushort[] ReadModbus(byte[] bytes)
        {
            MessageBox.Show("读取数据");
            return modbusMaster.ReadHoldingRegisters(bytes[0], bytes[1], bytes[2]);
        }

        /// <summary>
        /// 接收到数据 回调方法
        /// </summary>
        /// <param name="bytes"></param>
        private ushort[] WriteModbus(byte[] bytes)
        {
            //写入数据, 读取对应的数据 判断是否正确
            //写入的 设备 寄存器地址 值
            modbusMaster.WriteSingleRegister(bytes[0], bytes[1], bytes[2]);
            return modbusMaster.ReadHoldingRegisters(bytes[0], bytes[1], bytes[2]);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //绑定端口 判断
            if (IsBound)
            {
                _cts?.Cancel();
                //listeningSocket.Disconnect(false);
                listeningSocket.Close();
                listeningSocket.Dispose();
                IsBound = false;
                foreach (var item in clientSockets)
                {
                    item.Close();
                    item.Dispose();
                }
                comboBox1.Items.Clear();
                textBox1.Text = ("关闭服务端");
                //Thread.Sleep(200);
            }
            else
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(textBox3.Text.Trim()), Convert.ToInt32(textBox4.Text.Trim()));
                textBox1.Text = ("开启服务端");
                listeningSocket.Bind(endPoint);
                IsBound = true;
                listeningSocket.Listen(10); // 添加监听
                _cts = new CancellationTokenSource();
            }



            Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {

                    try
                    {
                        Socket clientSocket = await listeningSocket.AcceptAsync(); // 阻塞等待客户端
                                                                                   //判断是否已连接
                        int index = clientSockets.FindIndex(x => x.RemoteEndPoint == clientSocket.RemoteEndPoint);
                        //删除旧数据: (IP+端口）相同，并不意味着是“同一个客户端实例”，更不代表它对应的旧 Socket 对象还能用。
                        if (index != -1)
                        {
                            clientSockets[index].Close();
                            //clientSockets[index].Dispose();
                            clientSockets.RemoveAt(index);
                        }
                        clientSockets.Add(clientSocket);
                        comboBox1.Invoke(new Action(() =>
                        {
                            comboBox1.Items.Clear();
                            clientSockets.ForEach(x =>
                            {
                                comboBox1.Items.Add(x.RemoteEndPoint.ToString());
                            });
                        }));
                        //接受二进制文件
                        //MessageBox.Show(checkBox2.Checked.ToString());
                        bool isBin = checkBox2.Checked;
                        if (isBin)
                        {
                            await ReceiveBin(clientSocket, _cts);
                        }
                        else
                        {
                            //接受modbus与文本文件
                            await ReceiveData(clientSocket);
                        }

                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() => textBox1.AppendText($"接受连接失败: {ex.Message}")));
                    }
                }
            }, _cts.Token);
        }

        private async Task ReceiveBin(Socket client, CancellationTokenSource _cts)
        {

            await Task.Run(() =>
             {

                 Invoke(new Action(() => textBox1.AppendText("接收文件: ")));
                 StringBuilder stringBuilder = new StringBuilder();

                 //接收4个字节
                 byte[] bytes1 = new byte[4];
                 client.Receive(bytes1, 0, bytes1.Length, SocketFlags.None);
                 int length = BitConverter.ToInt32(bytes1, 0);
                 Invoke(new Action(() => textBox1.AppendText("接收文件长度: " + length)));

                 //获取文件名
                 byte[] bytes2 = new byte[length];
                 client.Receive(bytes2, 0, length, SocketFlags.None);
                 string fileName = Encoding.UTF8.GetString(bytes2);
                 Invoke(new Action(() => textBox1.AppendText("接收文件名字: " + fileName)));
                 //获取文件长度
                 byte[] bytesLength = new byte[8];
                 client.Receive(bytesLength, 0, bytesLength.Length, SocketFlags.None);
                 long fileByteLength = BitConverter.ToInt64(bytesLength, 0);
                 Invoke(new Action(() => textBox1.AppendText("接收文件总长度: " + fileByteLength)));
                 // 按长度收文件
                 byte[] buffer = new byte[4096];
                 long received = 0;
                 //所以我要先获取 前4个字节来判断文件的长度 之后使用,长度接受字节名.,重命名获取数据区
                 //写入数据
                 using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                 {
                     while (!_cts.IsCancellationRequested)
                     {
                         try
                         {
                             //for (int i = 0; i < len; i++)
                             //{
                             //    stringBuilder.Append(buffer[i]);
                             //    stringBuilder.Append(" ");
                             //}



                             #region 第一种文件接受写入方式 没有文件长度

                             int len = client.Receive(buffer);
                             if (len == 0) // 对端关闭连接
                             {
                                 break;
                             }
                             fileStream.Write(buffer, 0, len);
                             #endregion
                             #region 第二种文件接受写入方式 有文件长度
                             //if (received <= fileByteLength)
                             //{
                             //    int toRead = (int)Math.Min(buffer.Length, fileByteLength - received);
                             //    int len = client.Receive(buffer, 0, toRead, SocketFlags.None);
                             //    if (len == 0) throw new EndOfStreamException($"只收到 {received}/{fileByteLength}");
                             //    fileStream.Write(buffer, 0, len);
                             //    received += len;
                             //}
                             #endregion
                             //Invoke(new Action(() => textBox1.AppendText($"{stringBuilder}")));
                             //stringBuilder.Clear();

                         }
                         catch (Exception ex)
                         {
                             Invoke(new Action(() => textBox1.AppendText($"接受数据失败: {ex.Message}")));
                         }
                     }
                 }

             }, _cts.Token);
        }


        /// <summary>
        /// 接收数据 解析是否触发modbus
        /// </summary>
        /// <param name="client"></param>
        private async Task ReceiveData(Socket client)
        {
            clientSocket = client;
            await Task.Run(() =>
              {
                  byte[] buffer = new byte[4096]; // 固定大小缓冲区
                  while (!_cts.IsCancellationRequested)
                  {
                      try
                      {
                          int len = client.Receive(buffer);
                          if (len == 0) // 对端关闭连接
                          {
                              break;
                          }
                          string received = Encoding.UTF8.GetString(buffer, 0, len);
                          //解析数据   
                          var str = received.Split(',').ToList();
                          if (str.Count >= 4 && str[0] == "w")
                          {
                              //private ushort[] WriteModbus(byte[] bytes)
                              //触发方法
                              var bytes = str.Where(x => x != "w").Select(x => (byte)int.Parse(x)).ToArray();
                              var result = Writemodbus?.Invoke(bytes);

                              Invoke(new Action(() => textBox1.AppendText($"写入数据: {result[0]}\r\n")));
                              if (result != null && result[0] == int.Parse(str[3])) clientSocket.Send(Encoding.UTF8.GetBytes($"写入数据: {result[0]}正确\r\n"));
                              //w,1,0,3        
                          }
                          else if (str.Count >= 3 && str[0] == "r")
                          {
                              //触发方法
                              var bytes = str.Where(x => x != "r").Select(x => (byte)int.Parse(x)).ToArray();
                              var result = Readmodbus?.Invoke(bytes);
                              Invoke(new Action(() => textBox1.AppendText($"读取数据: {result.Length}\r\n")));
                              string temp = string.Empty;
                              //r,1,0,3  
                              foreach (var item in result)
                              {
                                  temp += item + " ";
                              }
                              if (result != null && result.Length == str.Count - 1) clientSocket.Send(Encoding.UTF8.GetBytes($"{temp}\r\n"));
                          }
                          else
                          {
                              Invoke(new Action(() => textBox1.AppendText($"{client.RemoteEndPoint}: {received}\r\n")));
                          }


                      }
                      catch (SocketException ex)
                      {
                          // 处理异常（如连接重置）
                          Invoke(new Action(() => textBox1.AppendText($"接收错误: {ex.Message}")));
                          break;
                      }
                  }
                  client.Close(); // 清理资源
              }, _cts.Token);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((comboBox1.SelectedItem==null).ToString()); 当全体广播的时候只要确保列表有值即可
            if ((string.IsNullOrWhiteSpace(textBox2.Text) || comboBox1.SelectedItem == null) || !checkBox1.Checked)
            {
                return;
            }
            if (clientSockets == null || clientSockets.Count == 0)
            {
                return;
            }
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(textBox2.Text);
                if (!checkBox1.Checked)
                {

                    Socket socket = clientSockets.Find(x => x.RemoteEndPoint.ToString() == comboBox1.SelectedItem.ToString());
                    socket.Send(data);
                    // 可选在接收框中显示发送内容
                    textBox1.AppendText($"发送:{socket.RemoteEndPoint}:  {textBox2.Text}\r\n");
                }
                else
                {
                    foreach (var item in clientSockets)
                    {
                        item.Send(data);
                        textBox1.AppendText($"全体发送: {textBox2.Text}\r\n");
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show($"发送失败: {ex.Message}");
                clientSocket = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
