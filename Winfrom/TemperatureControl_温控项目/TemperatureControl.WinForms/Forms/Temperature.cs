using Modbus.Device;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms
{
    public partial class Temperature : Form
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        SqlSugarClient sqlSugar = SQLHerper.Connection();

        ModbusMaster modbus = null;
        public Temperature()
        {
            InitializeComponent();
        }

        private void Temperature_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM1";
            serialPort1.DataReceived += SerialPort1_DataReceived;
            var list = sqlSugar.Queryable<Models.StoreArea>().ToList();
            foreach (var item in list)
            {

                dic.Add(item.StoreAreaName, item.StoreAreaId);
            }
            cbStoreArea.DataSource = dic.ToList();
            cbStoreArea.DisplayMember = "Key";
            cbStoreArea.ValueMember = "Value";

            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
                modbus = ModbusSerialMaster.CreateRtu(serialPort1);
            }
            #region 图表初始化
            chart1.Titles.Add("温度");
            chart1.Series.Clear();
            //配置 xy  坐标轴
            chart1.ChartAreas[0].AxisX.Title = "时间";
            chart1.ChartAreas[0].AxisY.Title = "数量/温度";
            //配置 series 数据源 温度系列
            Series series = new Series("温度");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 5;
            series.Points.AddXY(DateTime.Now, 50);
           
            //配置 series 数据源 数量系列
            Series series1 = new Series("数量");
            series1.ChartType = SeriesChartType.Line;
            series1.BorderWidth = 5;
            series1.Points.AddXY(DateTime.Now, 50);

            chart1.Series.Add(series1);
            chart1.Series.Add(series);


            TextAnnotation annotation = new TextAnnotation
            {
                Text = "温度",
                X = 50,
                Y = 50,
                ForeColor = Color.Red,
                Font = new Font("微软雅黑", 10),
                Alignment = ContentAlignment.MiddleCenter
            };
            chart1.Annotations.Add(annotation);
            #endregion
        }

        private void SerialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                textBox5.Text = serialPort1.ReadExisting();
                listBox1.Items.Add(textBox5.Text);
            }));
            //textBox5.Text=serialPort1.ReadExisting();
         //textBox5.Text= serialPort1.ReadLine();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    ushort[] u = modbus.ReadHoldingRegisters(1, 0, 4);
                    Invoke(new Action(() =>
                    {
                        textBox1.Text = u[0].ToString();
                        var v = $"{temperatureGauge1.MinValue} ~ {temperatureGauge1.MaxValue}";
                        temperatureGauge1.Temperature = u[1];
                        textBox2.Text = v;
                        textBox3.Text = temperatureGauge1.Temperature > 60 ? "高" : "正常";
                        chart1.Series["温度"].Points.AddXY(DateTime.Now, u[1]);
                        chart1.Series["数量"].Points.AddXY(DateTime.Now, u[0]);
                    }));

                }
            });

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox4.Text);
        }
    }
}
