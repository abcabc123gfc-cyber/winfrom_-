using Modbus.Device;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {


        /// <summary>
        /// 声明主站 modbus
        /// </summary>
        IModbusSerialMaster modbusMaster = null;
        private Dictionary<string, UILineSeries> seriesDict = new Dictionary<string, UILineSeries>();
        private Dictionary<string, UIBarSeries> uIBarSeries = new Dictionary<string, UIBarSeries>();
        private Dictionary<string, UIPieSeries> PieSeries = new Dictionary<string, UIPieSeries>();
        private Dictionary<string, UIDoughnutSeries> doughnutSeries = new Dictionary<string, UIDoughnutSeries>();
        string[] seriesName = { "含水率", "温度值", "电导率", "PH值" };
        Color[] seriesColor = { Color.Red, Color.Green, Color.Blue, Color.Orange };

        public Form1()
        {
            InitializeComponent();
        }
        //uiBarChart1
        private void Form1_Load(object sender, EventArgs e)
        {
            dataseriport();

            //初始化折线图
            InituILineOption();
            //初始化柱状图
            InituIBarOption();
            //初始化饼状图
            InituIPieOption();
            //初始化环形图
            InituIDoughnutOption();
        }

        private void InituIDoughnutOption()
        {
            //uiDoughnutChart1
            UIDoughnutOption uIDoughnutOption = new UIDoughnutOption();
            uIDoughnutOption.Title = new UITitle();
            uIDoughnutOption.Title.Text = "土壤检测数据";
            uIDoughnutOption.Title.SubText = "";
            uIDoughnutOption.Legend = new UILegend();
            uIDoughnutOption.Legend.Left = UILeftAlignment.Right;
            UIDoughnutSeries uIDoughnutSeries = new UIDoughnutSeries();
            uIDoughnutSeries.Name = "土壤参数";
            for (int i = 0; i < seriesName.Length; i++)
            {
                //添加数据
                uIDoughnutSeries.AddData(seriesName[i], 0, seriesColor[i]);
                //添加颜色代表的值名
                uIDoughnutOption.Legend.AddData(seriesName[i], seriesColor[i]);
                //保存到字典
                doughnutSeries[seriesName[i]] = uIDoughnutSeries;
            }
            uIDoughnutOption.AddSeries(uIDoughnutSeries);
            uiDoughnutChart1.SetOption(uIDoughnutOption);
        }

        private void InituIPieOption()
        {
            ////uiPieChart1
            //UIPieChart chart = new UIPieChart();
            //创建配置项 饼图
            UIPieOption uIPieOption = new UIPieOption();
            uIPieOption.Title = new UITitle();
            uIPieOption.Title.Text = "土壤检测数据";
            uIPieOption.Title.SubText = "";
           
            uIPieOption.Legend = new UILegend();
            uIPieOption.Legend.Left = UILeftAlignment.Right;
            //创建饼图 系列
            UIPieSeries uIPieSeries = new UIPieSeries();
            uIPieSeries.Name = "土壤参数"; // 系列名称（图例显示）
            for (int i = 0; i < seriesName.Length; i++)
            {
                //public void AddSeries(UIPieSeries series)
                //添加数据
                //public void AddData(string name, double value, Color color)
                //uIPieSeries.Name = seriesName[i];
                //添加数据 到集合中
                uIPieOption.Legend.AddData(seriesName[i], seriesColor[i]);
                uIPieSeries.AddData(seriesName[i], 0, seriesColor[i]);
                //保存到字典
                PieSeries[seriesName[i]] = uIPieSeries;
            }
            uIPieOption.AddSeries(uIPieSeries);
            uiPieChart1.SetOption(uIPieOption);

        }

        public void InituIBarOption()
        {
            //创建图表
            UIBarOption uIBarOption = new UIBarOption();
          
            uIBarOption.Title = new UITitle();
            uIBarOption.Title.Text = "土壤检测数据";
            uIBarOption.Title.SubText = "";
            //字体 全局字体
            //uiBarChart1.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            //uiBarChart1.ForeColor = Color.Yellow;
           
            uIBarOption.AutoSizeBarsCompact = true;
            //坐标轴
            uIBarOption.XAxis.Name = "采集次数";
            uIBarOption.YAxis.Name = "测量值";
            uIBarOption.Legend = new UILegend();
            //每种颜色对应的 值名  排列方式
            uIBarOption.Legend.Left = UILeftAlignment.Right;
            ////添加节点
            //uIBarOption.XAxis.Data.Add(seriesName[0]);
            ////添加数据
            //UIBarSeries series = new UIBarSeries();
            //series.Name = seriesName[0];
            //series.AddData(0);   // 数据点个数必须等于 XAxis.Data 的个数
            //uIBarOption.AddSeries(series);
            //
            UIBarSeries series1 = new UIBarSeries();
            //线条
            for (int i = 0; i < seriesName.Length; i++)
            {
                //添加X轴标签
                uIBarOption.XAxis.Data.Add(seriesName[i]);
                series1.Name = seriesName[i];
                // 关键：每个系列添加 1 个数据点，与 X 轴标签数量一致
                series1.AddData(seriesName[i], 0, seriesColor[i]);   // 初始名 值 颜色  
                //添加每种颜色代表的 值名 
                uIBarOption.Legend.AddData(seriesName[i], seriesColor[i]);
                //保存到字典
                uIBarSeries[seriesName[i]] = series1;
            }
            //添加 列表中 uIBarOption
            uIBarOption.AddSeries(series1);
            //添加的是列表
            uiBarChart1.SetOption(uIBarOption);
            uiBarChart1.Invalidate(); // 强制重绘

        }

        private void InituILineOption()
        {
            UILineOption uILineOption = new UILineOption();
            //配置图表标题 

            uILineOption.Title = new UITitle();
            uILineOption.Title.Text = "土壤检测数据";
            uILineOption.Title.SubText = "";
            uILineOption.Title.Top = UITopAlignment.Top;
            //配置坐标轴
            uILineOption.XAxis.Name = "(时间)小时";
            uILineOption.YAxis.Name = "测量值";
            uILineOption.Legend = new UILegend();
            uILineOption.Legend.Left = UILeftAlignment.Right;
            //线条
            for (int i = 0; i < seriesName.Length; i++)
            {
                // 创建一个折线图系列，名称为 seriesName[i]，颜色为 seriesColor[i]
                UILineSeries uILineSeries = new UILineSeries(seriesName[i], seriesColor[i]);
                //添加一个序列 列表
                var series1 = uILineOption.AddSeries(uILineSeries);
                series1.Color = seriesColor[i];
                series1.Width = 2;
                //设置最大数据点数
                series1.SetMaxCount(100);
                uILineOption.Legend.AddData(seriesName[i], seriesColor[i]);
                //保存到字典
                seriesDict[seriesName[i]] = series1;

            }
            uiLineChart1.SetOption(uILineOption);
            //x 设置x 时间轴为时间轴
            uILineOption.XAxisType = UIAxisType.DateTime;
            uILineOption.XAxis.AxisLabel.DateTimeFormat = "HH:mm:ss";

            // 设置Y轴显示范围从0到200
            uILineOption.YAxis.SetRange(0, 1000);

            //显示提示 鼠标悬停在折线图上时，显示提示信息
            uILineOption.ToolTip.Visible = true;
            //对鼠标事件不作出相应, 无法缩放图表
            uiLineChart1.MouseZoom = false;
        }
        private void dataseriport()
        {
            cbbPortName.DataSource = SerialPort.GetPortNames();
            cbbPortName.SelectedIndex = 2;
            cbbBaudRate.SelectedIndex = 1;
            cbbDataBit.SelectedIndex = 0;
            cbbParity.SelectedIndex = 0;
            cbbStopBit.SelectedIndex = 1;
        }



        /// <summary>
        /// 读取按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            //第三方包:  modbus4 
            modbusMaster = ModbusSerialMaster.CreateRtu(serialPort1);
            //读取数据 1 从第0个地址开始，读取4个寄存器 保持寄存器
            ushort[] ushorts = modbusMaster.ReadHoldingRegisters(1, 0, 4);
            this.Invoke(new Action(() =>
            {
                DateTime now = DateTime.Now;
                //通过名称从字典中获取序列，并添加数据
                for (int i = 0; i < seriesName.Length; i++)
                {
                    if (seriesDict.ContainsKey(seriesName[i]))
                        seriesDict[seriesName[i]].Add(now, ushorts[i]);
                }
                uiLineChart1.Refresh();
            }));


        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //byte[] bytes = new byte[serialPort1.BytesToRead];
            //serialPort1.Read(bytes, 0, bytes.Length);

            //if (bytes.Length != 13)
            //{
            //    return;
            //}

            ////int hsl = bytes[3] * 256 + bytes[4];
            ////int wdz = bytes[5] * 256 + bytes[6];
            ////int ddl = bytes[7] * 256 + bytes[8];
            ////int ph = bytes[9] * 256 + bytes[10];

            ////serialPort1.Write(bytes, 0, bytes.Length);
            //modbusMaster = ModbusSerialMaster.CreateRtu(serialPort1);



            //ushort[] ushorts = modbusMaster.ReadHoldingRegisters(1, 0, 4);


            //this.Invoke(new Action(() =>
            //{
            //    DateTime now = DateTime.Now;


            //    //通过名称从字典中获取序列，并添加数据
            //    for (int i = 0; i < seriesName.Length; i++)
            //    {
            //        if (seriesDict.ContainsKey(seriesName[i]))
            //            seriesDict[seriesName[i]].Add(now, ushorts[i]);
            //    }


            //    //if (seriesDict.ContainsKey("温度值"))
            //    //    seriesDict["温度值"].Add(now, ushorts[1]);

            //    //if (seriesDict.ContainsKey("电导率"))
            //    //    seriesDict["电导率"].Add(now, ushorts[2]);

            //    //if (seriesDict.ContainsKey("PH值"))
            //    //    seriesDict["PH值"].Add(now, ushorts[3]);

            //    uiLineChart1.Refresh();
            //}));




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

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnRead_Click(null, null);
        }

        /// <summary>
        /// 实时读取按钮
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 读取串口数据 柱状图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            modbusMaster = ModbusSerialMaster.CreateRtu(serialPort1);
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            ushort[] ushorts = modbusMaster.ReadHoldingRegisters(1, 0, 4);

            this.Invoke(new Action(() =>
            {
                //通过名称从字典中获取序列，并添加数据
                for (int i = 0; i < ushorts.Length; i++)
                {
                    if (uIBarSeries.TryGetValue(seriesName[i], out UIBarSeries series))
                    {
                        //series.AddData(seriesName[i],ushorts[i], seriesColor[i]);
                        series.Update(i, ushorts[i]);
                        richTextBox1.AppendText(ushorts[i].ToString() + "\r\n");
                    }
                }
                uiBarChart1.Refresh();
            }));
        }

        /// <summary>
        /// 读取串口数据 饼状图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            modbusMaster = ModbusSerialMaster.CreateRtu(serialPort1);
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            ushort[] ushorts = modbusMaster.ReadHoldingRegisters(1, 0, 4);
            this.Invoke(new Action(() =>
            {
                for (int i = 0; i < ushorts.Length; i++)
                {
                    if (PieSeries.TryGetValue(seriesName[i], out UIPieSeries series))
                    {
                        PieSeries[seriesName[i]].Update(seriesName[i], ushorts[i]);

                    }
                }
                uiPieChart1.Refresh();
            }));
        }
        /// <summary>
        /// 读取串口数据 环形图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            modbusMaster = ModbusSerialMaster.CreateRtu(serialPort1);
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            ushort[] ushorts = modbusMaster.ReadHoldingRegisters(1, 0, 4);
            this.Invoke(new Action(() =>
            {
                for (int i = 0; i < ushorts.Length; i++)
                {
                    if (doughnutSeries.TryGetValue(seriesName[i], out UIDoughnutSeries series))
                    {
                        series.Update(seriesName[i], ushorts[i]);
                    }
                }
                uiDoughnutChart1.Refresh();
            }));
        }
    }
}
