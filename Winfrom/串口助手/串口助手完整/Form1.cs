using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 串口助手完整.Helpers;

namespace 串口助手完整
{
    public partial class Form1 : Form
    {
        SerialPort serialPort = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
        }
        /// <summary>
        /// 串口数据接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //    rtbDislpayInfo.Invoke(new Action(() =>
            //    { 
            //        rtbDislpayInfo.AppendText(serialPort.ReadExisting());
            //    }));
            byte[] bytes = new Byte[serialPort.BytesToRead];
            serialPort.Read(bytes, 0, bytes.Length);
            byte[] filtered = bytes.Where(
                b => b >= 0x20 && b <= 0x7E
                 ||  b == 0x0D || b == 0x0A
                ).ToArray();
            rtbDislpayInfo.Invoke(new Action(() =>
            {
                rtbDislpayInfo.AppendText(Encoding.ASCII.GetString(filtered));
            }));
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            InitPort();
            //初始化图片
            InitPicture();

        }
        private void InitPicture()
        {
            //设置图片排序为左侧
            btnOpenOrClose.ImageAlign = ContentAlignment.MiddleLeft;
            //初始化按钮图片
            Image original = Properties.Resources.关闭.ToBitmap();

            btnOpenOrClose.Image = new Bitmap(original, new Size(40, 40));

        }
        private void InitPort()
        {

            #region 初始化端口列表
            var ports = SerialPort.GetPortNames();
            if (ports != null && ports.Length > 0)
            {
                cbbProt.Items.AddRange(ports);
                cbbProt.SelectedIndex = 2;
            }
            #endregion

            #region 波特率
            List<int> baudRateList = new List<int>() { 300, 1200, 4800, 9600, 19200, 57500, 115200 };
            cbbBaudRate.DataSource = baudRateList;
            cbbBaudRate.SelectedIndex = 3;
            #endregion

            #region 停止位
            ComItemsParity.SetStopBit(ref cbbStopBits);
            #endregion

            #region 奇偶校验
            ComItemsParity.SetParoty(ref cbbPartiy);
            #endregion

            #region 数据位
            List<int> dataBitsList = new List<int> { 8, 7, 6, 5 };
            cbbDataBits.DataSource = dataBitsList;
            #endregion
        }

        private void btnOpenOrClose_Click(object sender, EventArgs e)
        {

            if (!serialPort.IsOpen)
            {

                //Properties.Resources.open;
                //初始化按钮图片
                Image original = Properties.Resources.打开.ToBitmap();
                btnOpenOrClose.Image = new Bitmap(original, new Size(40, 40));
                btnOpenOrClose.Text = "关闭串口";

                //配置 端口打开之前
                serialPort.PortName = cbbProt.SelectedItem.ToString();
                //MessageBox.Show(cbbProt.SelectedItem.ToString());
                //配置波特率
                serialPort.BaudRate = (int)cbbBaudRate.SelectedValue;
                //配置停止位
                //(StopBits)cbbStopBits.SelectedValue; 直接转换会报错
                serialPort.StopBits = Enum.TryParse(cbbStopBits.SelectedItem.ToString(), out StopBits stopBits) ? stopBits : StopBits.One;
                //配置数据位
                serialPort.DataBits = int.TryParse(cbbDataBits.SelectedValue.ToString(), out int result) ? result : 8;
                //配置奇偶校验 (Parity)cbbPartiy.SelectedValue;
                serialPort.Parity = Enum.TryParse(cbbPartiy.SelectedItem.ToString(), out Parity parity) ? parity : Parity.None;
                serialPort.Open();


            }
            else
            {
                //Properties.Resources.close;
                //初始化按钮图片
                Image original = Properties.Resources.关闭.ToBitmap();
                btnOpenOrClose.Image = new Bitmap(original, new Size(40, 40));
                serialPort.Close();
                btnOpenOrClose.Text = "打开串口";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbDislpayInfo.Clear();
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            serialPort.Write(rtbSendInfo.Text.Trim());
        }

        /// <summary>
        /// 串口数据 以16进制触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbHexDislpay_CheckedChanged(object sender, EventArgs e)
        {
            //rtbDislpayInfo 富文本显示框 接受数据

        }
    }
}
