using S7.Net;

namespace _01_西门子_S7_NetPlus
{
    public partial class Form1 : Form
    {
        Plc plc = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listBox1.HorizontalScrollbar = false;
            listBox1.IntegralHeight = false;
            //初始化
            //参数1: 设备使用的Cpu 类型
            //参数2: 设备的Ip地址
            //参数3: 端口号
            //参数4: 机架 对于S71200 Cpu 来说，机架号是0
            //参数5: 插槽 对于S71200 Cpu 默认插槽是0
            try
            {
                plc = new Plc(CpuType.S71200, textBox1.Text, int.Parse(textBox3.Text.Trim()), short.Parse(textBox2.Text.Trim()), short.Parse(textBox4.Text.Trim()));

            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            if (plc != null && button1.Text == "连接")
            {
                try
                {
                    if (!plc.IsConnected)
                    {
                        await plc.OpenAsync();
                        button1.Text = "断开";
                        listBox1.Items.Add("连接成功");
                    }
                }
                catch (Exception ex)
                {

                    //MessageBox.Show(ex.Message);
                    listBox1.Items.Add(ex.Message);
                }
            }
            else
            {
                try
                {
                    if (plc != null && plc.IsConnected)
                    {
                        plc.Close();
                        button1.Text = "连接";
                        listBox1.Items.Add("断开成功");
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    listBox1.Items.Add(ex.Message);
                }
            }
        }
        #region 控制灯
        #region 灯1
        private void button2_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {
                    // DB(Data Block) 数据块 西门子plc中存储数据的内存区域  可以理解为之前咱们学习传感器学习的寄存器  存储数据的一小块内存
                    //DB50  表示第50号数据块
                    //DBD  数据类型  代表C#使用int   DBW=ushort DBX=bool
                    //0  字节偏移量  和  位偏移量
                    //写
                    //地址  值
                    plc.Write("DB50.DBD0", 1);
                    listBox1.Items.Add("灯1已打开");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    listBox1.Items.Add(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {
                    plc.Write("DB50.DBD0", 0);
                    listBox1.Items.Add("灯1已关闭");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    listBox1.Items.Add(ex.Message);
                }
            }
        }
        #endregion
        #region 灯2
        private void button4_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {
                    //写
                    //地址  值

                    //32位浮点数  单精度   float  1.1默认是64浮点数 double  双精度
                    //注意要加 f
                    plc.Write("DB50.DBD8", 1f);
                    listBox1.Items.Add("灯2已打开");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {

                    plc.Write("DB50.DBD8", 0f);
                    listBox1.Items.Add("灯2已关闭");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);

                }
            }
        }
        #endregion
        #region 灯3
        private void button6_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {

                    plc.Write("DB50.DBX12.0", true);
                    listBox1.Items.Add("灯3已打开");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);

                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {

                    plc.Write("DB50.DBX12.0", false);
                    listBox1.Items.Add("灯3已关闭");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);

                }
            }
        }
        #endregion
        #endregion
        #region 读取数据
        private void button8_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {

                    var v = plc.Read("I0.0");
                    listBox1.Items.Add($"读取成功: {v}");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (plc != null && plc.IsConnected)
            {
                try
                {

                    var v = plc.Read("DB50.DBD4");
                    listBox1.Items.Add($"读取成功: {v}");
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);

                }
            }
        }
        #endregion
    }
}
