using modbus.helper;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApp1.Helper;
using modbus;

namespace modbus
{

    public partial class Form1 : Form
    {
        /// <summary>
        /// 关闭打开的标识
        /// </summary>
        private bool IsOpenClose;
        /// <summary>
        /// 是否发送邮件
        /// </summary>
        private bool IsSendMail;

        
        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            //区域
            ChartArea chartArea1 = new ChartArea()
            {
                AxisX = { Title = "(时间)小时" },
                AxisY = { Title = "测量值" }
            };
            chart1.ChartAreas.Add(chartArea1);
            string[] seriesName = { "含水率", "温度值", "电导率", "PH值" };

            Color[] seriesColor = { Color.Red, Color.Green, Color.Blue, Color.Orange };

            for (int i = 0; i < seriesName.Length; i++)
            {
                //序列
                Series series = new Series(seriesName[i]);
                //折线图
                series.ChartType = SeriesChartType.Line;
                series.Color = seriesColor[i];
                series.BorderWidth = 1;
                chart1.Series.Add(series);
            }

            Title title = new Title()
            {
                Text = "土壤检测数据",
                Font = new Font("微软雅黑", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };
            chart1.Titles.Add(title);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbPortName.DataSource = SerialPort.GetPortNames();
            cbbPortName.SelectedIndex = 2;
            cbbBaudRate.SelectedIndex = 1;
            cbbDataBit.SelectedIndex = 0;
            cbbParity.SelectedIndex = 0;
            cbbStopBit.SelectedIndex = 1;
        }

        /// <summary>
        /// 串口数据接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] bytes = new byte[serialPort1.BytesToRead];
            serialPort1.Read(bytes, 0, bytes.Length);
            //地址码 1
            //功能码 1
            //有效字节数 1
            //... 一个数据区, 占两个字节 读取了 4个 所有的数据区加一块是 8
            //校验码 2
            if (bytes.Length != 13)
            {
                return;
            }
            //解析相应帧
            //foreach (var item in bytes)
            //{


            //    richTextBox1.Invoke(new Action(() =>
            //    {
            //        richTextBox1.AppendText(item.ToString() + " ");
            //    }));


            //}
            //解析应答帧
            //                                          CRC校验码
            //应答帧 TX:  01 03 08     |04 57|  |00 DE| |0D 05|  |11 5C|       04 36
            //01 地址码 
            //03 功能码
            //08 有效字节数    读取到的数据 一个数据区占用两个字节 读取了四个数据  字节数位8
            // |04 57| 数据一区   含水率
            // |00 DE| 数据二区   温度值
            //.....
            //04 36 CRC 校验码

            //字节数组中 存储的是16进制的数据
            //也就是说 字节数组中数据的完整写法是 0x01 0x03 0x08 0x04 0x57 0x00 0xDE 0x0D......
            //一个字节是 8位 1byte =8bit
            //两个字节 是 16位 ==>  04 57  04 就是高8位   57低8位


            //公式:  10进制  ===== 16进制高8位 * 256 + 16进制低8位
            int hsl = bytes[3] * 256 + bytes[4];
            int wdz = bytes[5] * 256 + bytes[6];
            int ddl = bytes[7] * 256 + bytes[8];
            int ph = bytes[9] * 256 + bytes[10];
            //txtHSL.Invoke(new Action(() =>
            //{
            //    txtHSL.Text = hsl.ToString();
            //}));

            Invoke(new Action(() =>
            {
                txtHSL.Text = hsl.ToString();
                txtWDZ.Text = wdz.ToString();
                txtDDL.Text = ddl.ToString();
                txtPH.Text = ph.ToString();

                chart1.Series["含水率"].Points.AddXY(DateTime.Now.ToString("HH:mm:ss"),hsl); 
            }));


            if (wdz > 40 && !IsSendMail)
            {
                //发邮件 温度过高
                IsSendMail = true;
                MailHelper.SendMail("3244331389@qq.com", "温度过高", "温度过高");
                //发短信




            }
        }


        /// <summary>
        /// 读取按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            //读取数据需要发送请求帧
            SendData();
            byte[] bytes = new byte[8];
            //从站地址对应了要访问的设备
            //从站的地址使用 16 进制 表示，0x01 表示从站地址为1; 10进制
            bytes[0] = 0x01;
            // 读取功能码 转成10进制为: 03
            bytes[1] = 0x03;
            //起始地址
            //起始地址的高位
            bytes[2] = 0x00;
            //起始地址的低位
            bytes[3] = 0x00;

            //读取的寄存器数量
            //寄存器长度的高位
            bytes[4] = 0x00;
            //寄存器长度的低位
            bytes[5] = 0x04;

            //校验位  crc 
            //使用请求帧  前六位 进行校验
            byte[] rc = CRCHelper.CRC16(bytes);


            bytes[6] = rc[0];
            bytes[7] = rc[1];
            serialPort1.Write(bytes, 0, bytes.Length);

           
            

        }

        private void SendData()
        {

        }

        private void btnConn_Click(object sender, EventArgs e)
        {

            try
            {

                if (serialPort1.IsOpen)
                {
                    btnConn.Text = "连接";
                    serialPort1.Close();
                }
                else
                {
                    serialPort1.PortName = cbbPortName.SelectedItem.ToString();
                    serialPort1.BaudRate = Convert.ToInt32(cbbBaudRate.SelectedItem);
                    serialPort1.DataBits = Convert.ToInt32(cbbDataBit.SelectedItem);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbbStopBit.SelectedItem.ToString());
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cbbParity.SelectedItem.ToString());
                    serialPort1.Open();
                    btnConn.Text = "断开";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 实时读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbRealTimeRead_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRealTimeRead.Checked)
            {
                if (serialPort1.IsOpen != true)
                {
                    MessageBox.Show("请先连接串口");
                    return;
                }
                timer1.Interval = 1000;
                timer1.Start();

            }
            else
            {
                timer1.Stop();
            }
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            btnRead_Click(null, null);
        }

        /// <summary>
        /// 修改波特率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[8];
            buffer[0] = 0x01;
            buffer[1] = 0x06;

            buffer[2] = 0x07;
            buffer[3] = 0xD1;

            int value = 2;
            //高8位
            buffer[4] = (byte)(value >> 8);
            //低8位
            buffer[5] = (byte)(value & 0xff);
            byte[] crc = CRCHelper.CRC16(buffer);
            buffer[6] = crc[0];
            buffer[7] = crc[1];
            serialPort1.Write(buffer, 0, buffer.Length);

        }
        /// <summary>
        /// 写入按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[8];
            //从站的地址
            buffer[0] = Convert.ToByte(txtSlaveID.Text.Trim());
            //功能码
            buffer[1] = 0x06;

            //需要写入的寄存器的地址
            // (byte)(address >> 8) 把地址转换为高8位
            int address = Convert.ToInt32(txtStartAddress.Text.Trim());
            buffer[2] = (byte)(address >> 8);//设置写入的起始地址的高位

            //(byte)(address & 0xff)把地址转换为低8位
            buffer[3] = (byte)(address & 0xff);//设置写入的起始地址的低位


            //需要写入的数据
            int data = Convert.ToInt32(txtData.Text.Trim());

            //buffer[4]=(byte)(data >> 8);//求高8位
            //buffer[5]=(byte)(data & 0xff);//求低8位

            buffer[4] = (byte)(data / 256);//求高8位
            buffer[5] = (byte)(data % 256);//求低8位

            byte[] crc = CRCHelper.CRC16(buffer);

            buffer[6] = crc[0];
            buffer[7] = crc[1];

            serialPort1.Write(buffer, 0, buffer.Length);

        }
    }
}
