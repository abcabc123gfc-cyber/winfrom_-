using gts;
using System;
using System.Drawing;
using System.Windows.Forms;
using static gts.mc;
namespace 固高运行控制卡
{
    public partial class Form1 : Form
    {
        short _axis = 0;
        /// <summary>
        /// 当前轴号
        /// </summary>
        public short Axis
        {
            get => _axis;
            set
            {
                if (comboBox1.SelectedValue != null &&
                    short.TryParse(comboBox1.SelectedValue.ToString(), out short result))
                {
                    _axis = result;
                }
                else
                {
                    _axis = 1; // 或保留原值
                }
            }
        }

        /// <summary>
        /// 返回值
        /// </summary>
        short rtn = 0;
        /// <summary>
        /// 点位运动累加
        /// </summary>
        int pos;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打开运控控制卡
            //参数1:0正常打开运动控制器默认值为：0 1：内部调试方式，用户不能使用
            //参数2:当 channel=1 时，该参数有效
            //返回值:0:成功 1:失败 2:许可不支持 等:在返回值中定义的错误码-->编程手册--基本功能
            rtn = gts.mc.GT_Open(0, 1);

            if (rtn == 0)
            {
                MessageBox.Show("打开成功");
            }
            else if (rtn == 1)
            {
                MessageBox.Show("打开失败");
                return;
            }
            //控制卡复位
            //复位运动控制器。
            rtn = mc.GT_Reset();
            if (rtn != 0)
            {
                MessageBox.Show("复位失败" + rtn);
                return;
            }
            //加载配置文件
            //注意:配置文件需要换成自己配置的文件
            rtn = mc.GT_LoadConfig("GTS800.cfg");
            if (rtn != 0)
            {
                MessageBox.Show("加载配置文件失败" + rtn);
                return;
            }
            //清除状态
            //GT_LoadConfig()下载配置信息到运动控制器，调用该指令后需再调用GT_ClrSts 才能使该指令生效

            //清除驱动器报警标志、跟随误差越限标志、限位触发标志。
            //参数1:起始轴号。正整数。 参数2:读取的轴数，默认为1。正整数。
            rtn = mc.GT_ClrSts(1, 8);
            //开启伺服使能
            button3_Click_1(null, null);
            //开启定时器
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rtn = mc.GT_ClrSts(1, 8);
            if (rtn != 0)
            {
                MessageBox.Show("清除状态失败" + rtn);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mc.GT_AxisOn(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取位置和速度
            GetVelAndPos();
            //检测轴状态
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void GetVelAndPos()
        {

            //读取规划位置
            //参数1:起始规划轴号，正整数。
            //参数2:规划位置。单位：pulse。
            //参数3:读取的轴数，默认为1，正整数。 
            //参数4: 读取控制器时钟，默认值为：NULL，即不用读取控制器时钟。
            rtn = mc.GT_GetPrfPos(Axis, out double prfpos, 1, out uint clk);
            textBox2.Text = prfpos.ToString();
            //读取规划速度
            rtn = mc.GT_GetPrfVel(Axis, out double prfvel, 1, out clk);
            textBox4.Text = prfvel.ToString();
            //读取实际位置
            rtn = mc.GT_GetEncPos(Axis, out double encpos, 1, out clk);
            textBox3.Text = encpos.ToString();
            //读取实际速度
            rtn = mc.GT_GetEncVel(Axis, out double encvel, 1, out clk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox7.BackColor = Color.Red;
            textBox8.BackColor = Color.Red;
            textBox9.BackColor = Color.Red;
            textBox10.BackColor = Color.Red;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            short temp = 0;
            Axis = Convert.ToInt16(comboBox1.Text);
            //读取轴状态
            //参数1:起始轴号，正整数。 
            //参数2: 32位轴状态字，详细定义参见表5 - 2 轴状态定义。 
            //参数3: count 读取的轴数，默认为1，正整数。 
            //参数4: pClock 读取控制器时钟，默认值为：NULL，即不用读取控制器时钟。
            //轴状态 sts :0 保留 1: 驱动器报警 2:保留 3:保留 等5-2轴状态定义
            //注意轴状态返回的是 32位，所以要使用 & 运算符进行位运算
            rtn = mc.GT_GetSts(Axis, out int sts, 1, out uint clk);
            //驱动报警
            if ((sts & 0x2) != 0)
            {
                textBox7.BackColor = Color.Red;
            }
            else
            {
                textBox7.BackColor = Color.Green;
            }
            //正向限位
            if ((sts & 0x20) != 0)
            {
                textBox8.BackColor = Color.Red;
            }
            else
            {
                textBox8.BackColor = Color.Green;
            }
            //负向限位
            if ((sts & 0x40) != 0)
            {
                textBox9.BackColor = Color.Red;
            }
            else
            {
                textBox9.BackColor = Color.Green;
            }
            //使能
            if ((sts & 0x200) != 0)
            {
                textBox10.BackColor = Color.Green;
            }
            else
            {
                textBox10.BackColor = Color.Red;
            }

        }
        /// <summary>
        /// 伺服使能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (button3.Text == "伺服使能" || button3.Text == "伺服已关闭")
            {
                //打开驱动器使能
                //参数 打开伺服使能的轴的编号。正整数。 
                rtn = mc.GT_AxisOn(Axis);
                button3.Text = "伺服已开启";
            }
            else
            {
                //关闭驱动器使能
                //参数 关闭伺服使能的轴的编号。正整数。
                rtn = mc.GT_AxisOff(Axis);
                button3.Text = "伺服已关闭";
            }
        }
        #region 点位运动
        /// <summary>
        /// 开启点位运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            rtn = mc.GT_PrfTrap(Axis);
            MessageBox.Show(rtn.ToString());
        }
        /// <summary>
        /// 设置当前轴为零点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            //参数1: 轴编号。正整数。
            //参数2: 读取的轴数，默认为1，正整数。
            mc.GT_ZeroPos(Axis, 8);
        }

        /// <summary>
        /// 开始运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            //设置当前轴为点位运动模式
            //rtn=mc.GT_PrfTrap(Axis);
            mc.TTrapPrm trapPrm;
            //获取当前轴之前的点位运动信息
            mc.GT_GetTrapPrm(Axis, out trapPrm);
            //设置加速度
            trapPrm.acc = 0.1;
            //设置减速度
            trapPrm.dec = 0.1;
            //起跳速度 默认为0
            trapPrm.velStart = 0;
            //设置平滑时间
            trapPrm.smoothTime = 1;
            //设置点位运动
            mc.GT_SetTrapPrm(Axis, ref trapPrm);

            //设置AXIS轴的目标速度 
            mc.GT_SetVel(Axis, int.Parse(textBox12.Text.Trim()));
            //累加规划位置的偏移
            pos += int.Parse(textBox11.Text.Trim());
            //设置AXIS 轴的目标位置
            mc.GT_SetPos(Axis, pos);
            //开启点位运动
            //为什么要使用位运算1<<(Axis-1)呢？
            //Bit   3 2 1 0
            //对应轴 4 轴 3 轴 2 轴 1 轴
            mc.GT_Update(1 << (Axis - 1));

        }
        /// <summary>
        /// 停止点位运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            //停止一个或多个轴的规划运动，停止坐标系运动。
            //参数1:按位指示需要停止运动的轴号或者坐标系号。当bit位为1时表示停止对应的轴或  者坐标系。

            //参数2:当bit位为0时表示平滑停止对应的轴或坐标系，当bit位为1 时表示急停对应的轴或坐标系。 

            mc.GT_Stop(1 << (Axis - 1), 0);
        }
        #endregion
        #region jog 运动
        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Axis.ToString());
            Axis = 1;
            //开启jog运动
            rtn = mc.GT_PrfJog(Axis);
            MessageBox.Show(rtn.ToString());
            gts.mc.TJogPrm jogPrm;
            //获取当前轴之前的点位运动信息
            rtn = gts.mc.GT_GetJogPrm(Axis, out jogPrm);
            if (rtn != 0)
            {
                MessageBox.Show("获取之前点位失败" + rtn);
            }

            //设置加速度
            jogPrm.acc = 0.1;
            //设置减速度
            jogPrm.dec = 0.1;
            //设置平滑时间, 错误原因: 平滑时间设置失败
            //jogPrm.smooth = 1;
            rtn = gts.mc.GT_SetJogPrm(Axis, ref jogPrm);
            if (rtn != 0)
            {
                MessageBox.Show("获取设置点位失败" + rtn + "轴号: " + Axis);
            }
        }
        #endregion
        /// <summary>
        /// 开始jog运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            //gts.mc.TJogPrm jogPrm;
            ////获取当前轴之前的点位运动信息
            //rtn = gts.mc.GT_PrfJog(Axis);//设置为jog模式
            //if (rtn != 0)
            //{
            //    MessageBox.Show("获取之前点位失败" + rtn);
            //}

            //rtn = gts.mc.GT_GetJogPrm(Axis, out jogPrm);
            ////设置加速度
            //jogPrm.acc = 0.1;
            ////设置减速度
            //jogPrm.dec = 0.1;
            ////设置平滑时间
            //jogPrm.smooth = 1;
            //if (rtn != 0)
            //{
            //    MessageBox.Show("获取设置点位失败" + rtn + "轴号: " + Axis);
            //}


        }
        /// <summary>
        /// 正向运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_MouseDown(object sender, MouseEventArgs e)
        {


            //设置AXIS轴的目标速度 
            rtn = gts.mc.GT_SetVel(Axis, -Convert.ToDouble(textBox14.Text));
            if (rtn != 0)
            {
                MessageBox.Show("正向运动失败" + rtn + "轴号: " + Axis);
            }
            rtn = gts.mc.GT_Update(1 << (Axis - 1));//更新轴运动
            if (rtn != 0)
            {
                MessageBox.Show("更新轴运动失败" + rtn + "轴号: " + Axis);
            }
        }
        /// <summary>
        /// 负向运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {


            //设置AXIS轴的目标速度 
            mc.GT_SetVel(Axis, Convert.ToDouble(textBox14.Text));
            if (rtn != 0)
            {
                MessageBox.Show("正向运动失败" + rtn + "轴号: " + Axis);
            }
            rtn = gts.mc.GT_Update(1 << (Axis - 1));//更新轴运动
            if (rtn != 0)
            {
                MessageBox.Show("更新轴运动失败" + rtn + "轴号: " + Axis);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 正向运动 结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            mc.GT_Stop(1 << (Axis - 1), 0);
        }
        /// <summary>
        /// 负向运动 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            mc.GT_Stop(1 << (Axis - 1), 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        #region 插补运动
        /// <summary>
        /// 建立坐标系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click_1(object sender, EventArgs e)
        {
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();
            rtn = mc.GT_GetCrdPrm(1, out crdPrm);

            //结构体赋值

            // 坐标系为二维坐标系 
            crdPrm.dimension = 2;
            // 最大合成速度：500pulse/ms 
            crdPrm.synVelMax = 100;
            // 最大加速度：1pulse/ms^2 
            crdPrm.synAccMax = 1;
            // 最小匀速时间：50ms 
            crdPrm.evenTime = 50;
            // 规划器1(即profile[0])对应到X轴（即1） 
            crdPrm.profile1 = 1;
            // 规划器2(即profile[1])对应到Y轴（即2）
            crdPrm.profile2 = 2;
            // 表示需要指定坐标系的原点坐标的规划位置 
            crdPrm.setOriginFlag = 1;
            // 坐标系的原点坐标的规划位置为（100, 100） 
            crdPrm.originPos1 = 100;
            crdPrm.originPos2 = 100;
            //或者=0，则坐标系原点为坐标系左下角与机械机坐标系重合
            //crdPrm.originPos1 = 0;
            //crdPrm.originPos2 = 0;
            // 建立1号坐标系，设置坐标系参数 
            //参数1: 坐标系号，取值范围：[1, 2]。
            //参数2: 结构体
            rtn = mc.GT_SetCrdPrm(1, ref crdPrm);


        }
        #endregion
        /// <summary>
        /// 直线插补运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            // 指令返回值变量 
            short sRtn;
            // 坐标系运动状态查询变量 
            short run;
            // 坐标系运动完成段查询变量 
            long segment;
            // 坐标系的缓存区剩余空间查询变量 
            int space;
            ushort MC_GPO = 10;

            // 即将把数据存入坐标系1的FIFO0中，所以要首先清除此缓存区中的数据 
            sRtn = GT_CrdClear(1, 0);

            // 向缓存区写入第一段插补数据 
            sRtn = GT_LnXY(
             1,    // 该插补段的坐标系是坐标系1 
              10000, 30000,  // 该插补段的终点坐标(200000, 0) 
             50,    // 该插补段的目标速度：100pulse/ms 
             0.1,    // 插补段的加速度：0.1pulse/ms^2 
             0,    // 终点速度为0 
             0);    // 向坐标系1的FIFO0缓存区传递该直线插补数据
                    // 向缓存区写入第二段插补数据 
            sRtn = GT_LnXY(1, 20000, 0, 50, 0.1, 0, 0);

            // 向缓存区写入第三段插补数据 
            sRtn = GT_LnXY(1, 30000, 30000, 50, 0.1, 0, 0);

            // 缓存区数字量输出 
            sRtn = GT_BufIO(
             1,    // 坐标系是坐标系1 
             MC_GPO,  // 数字量输出类型：通用输出 
             0xffff,   // bit0~bit15全部都输出 
             0x55,   // 输出的数值为0x55 
             0);    // 向坐标系1的FIFO0缓存区传递该数字量输出 


            // 缓存区延时指令 
            sRtn = GT_BufDelay(
             1,    // 坐标系是坐标系1 
             400,    // 延时400ms 
             0);    // 向坐标系1的FIFO0缓存区传递该延时

            // 缓存区数字量输出 
            //缓存区内数字量IO输出设置指令。

            //crd 坐标系号。正整数，取值范围：[1, 2]。 
            //doType
            //数字量输出的类型。 
            //MC_ENABLE(该宏定义为10)：输出驱动器使能。 
            //MC_CLEAR(该宏定义为11)：输出驱动器报警清除。 
            //MC_GPO(该宏定义为12)：输出通用输出。 
            //doMask 从bit0~bit15按位表示指定的数字量输出是否有操作。 
            //0：该路数字量输出无操作。1：该路数字量输出有操作。 
            //doValue 从bit0~bit15按位表示指定的数字量输出的值。 
            //fifo 插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0。 
            sRtn = GT_BufIO(1, MC_GPO, 0xffff, 0x55, 0);

            // 查询坐标系1的FIFO0所剩余的空间 
            sRtn = GT_CrdSpace(1, out space, 0);
            // 启动坐标系1的FIFO0的插补运动 
            //参数1: 启动的坐标系是坐标系 1: 启动坐标系, 0:,不启动
            //参数2: 从bit0~bit1按位表示坐标系需要启动的缓存区的编号。 
            sRtn = GT_CrdStart(1, 0);


            //!!!!GT_CrdStatus() 查询插补运动坐标系状态。

            short pRun;
            int pSegment;

            //参数1:坐标系号
            //参数2:输出参数 当前插补运动状态。0：停止，1：运行，
            //参数3:取当前已经完成的插补段数
            //参数4:所要查询运动状态的插补缓存区号。正整数，取值范围：[0, 1]，默认值为：0
            rtn = gts.mc.GT_CrdStatus(1, out pRun, out pSegment, 0);

            do
            {
                Console.WriteLine($"正在插补运动中,剩余段数: {pSegment}");
                rtn = gts.mc.GT_CrdStatus(1, out pRun, out pSegment, 0);
            }
            while (pRun == 1);

        }
        /// <summary>
        /// 圆弧插补运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            // 指令返回值变量 
            short sRtn;
            // 坐标系运动状态查询变量 
            short run;
            // 坐标系运动完成段查询变量 
            long segment;
            // 坐标系的缓存区剩余空间查询变量 
            int space;

            // 即将把数据存入坐标系1的FIFO0中，所以要首先清除此缓存区中的数据 
            sRtn = GT_CrdClear(1, 0);


            // 向缓存区写入第一段插补数据 
            sRtn = GT_LnXY(
             1,     // 该插补段的坐标系是坐标系1 
             20000, 0,   // 该插补段的终点坐标(200000, 0) 
             20,     // 该插补段的目标速度：100pulse/ms 
             0.1,     // 插补段的加速度：0.1pulse/ms^2 
             0,     // 终点速度为0 
             0);     // 向坐标系1的FIFO0缓存区传递该直线插补数据 

            //          // 向缓存区写入第二段插补数据，该段数据是以圆心描述方法描述了一个整圆 
            //          sRtn = GT_ArcXYC(
            //           1,     // 坐标系是坐标系1 
            //           2000, 0,   // 该圆弧的终点坐标(200000, 0) 
            //           -100000, 0,   // 圆弧插补的圆心相对于起点位置的偏移量(-100000, 0) 
            //           0,     // 该圆弧是顺时针圆弧 
            //           100,     // 该插补段的目标速度：100pulse/ms
            //           0.1,     // 该插补段的加速度：0.1pulse/ms^2 
            //0,     // 终点速度为0 
            //0);     // 向坐标系1的FIFO0缓存区传递该直线插补数据 


            // 向缓存区写入第三段插补数据，该段数据是以半径描述方法描述了一个1/4圆弧 
            sRtn = GT_ArcXYR(
             1,     // 坐标系是坐标系1 
             0, 20000,   // 该圆弧的终点坐标(0, 200000) 
             20000,    // 半径：200000pulse 
             1,     // 该圆弧是逆时针圆弧 
             20,     // 该插补段的目标速度：100pulse/ms 
             0.1,     // 该插补段的加速度：0.1pulse/ms^2 
             0,     // 终点速度为0 
             0);     // 向坐标系1的FIFO0缓存区传递该直线插补数据 


            // 向缓存区写入第四段插补数据，回到原点位置 
            sRtn = GT_LnXY(1, 0, 0, 20, 0.1, 0, 0);
            // 查询坐标系1的FIFO0所剩余的空间 
            sRtn = GT_CrdSpace(1, out space, 0);
            // 启动坐标系1的FIFO0的插补运动 
            sRtn = GT_CrdStart(1, 0);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // 指令返回值变量 
            short sRtn;
            // 坐标系运动状态查询变量 
            short run;
            // 坐标系运动完成段查询变量 
            long segment;
            // 坐标系的缓存区剩余空间查询变量 
            int space;

            // 即将把数据存入坐标系1的FIFO0中，所以要首先清除此缓存区中的数据 
            sRtn = GT_CrdClear(1, 0);

            // 向缓存区写入第一段插补数据 
            sRtn = GT_LnXY(
             1,     // 该插补段的坐标系是坐标系1 
             20000, 0,   // 该插补段的终点坐标(200000, 0) 
             30,     // 该插补段的目标速度：100pulse/ms 
             0.1,     // 插补段的加速度：0.1pulse/ms^2 
             0,     // 终点速度为0 
             0);     // 向坐标系1的FIFO0缓存区传递该直线插补数据 

            // 向缓存区写入第二段插补数据，该段数据是以圆心描述方法描述了一个整圆
            sRtn = GT_ArcXYC(
             1,     // 坐标系是坐标系1 
             20000, 0,   // 该圆弧的终点坐标(200000, 0) 
             -10000, 0,   // 圆弧插补的圆心相对于起点位置的偏移量(-100000, 0) 
             0,     // 该圆弧是顺时针圆弧 
             30,     // 该插补段的目标速度：100pulse/ms
             0.1,     // 该插补段的加速度：0.1pulse/ms^2 
  0,     // 终点速度为0 
  0);     // 向坐标系1的FIFO0缓存区传递该直线插补数据 

            // 向缓存区写入第三段插补数据，回到原点位置
            rtn = gts.mc.GT_LnXY(1, 0, 0, 100, 0.1, 0, 0);

            // 启动坐标系1的FIFO0的插补运动
            rtn = gts.mc.GT_CrdStart(1, 0);

        }
        /// <summary>
        /// 气缸上升
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            #region 控制气缸的 关系
            //GT_SetDoBit    本身并不直接驱动气缸，它控制的是运动控制器的通用数字量输出（DO）。气缸通常通过电磁阀控制，而电磁阀的线圈接在控制器的 DO 上。因此关系链是：

            //GT_SetDoBit → 控制器 DO → 电磁阀 → 气缸伸出 / 缩回

            //同时，气缸的到位状态通常由磁性开关反馈到控制器的数字量输入（DI），用 GT_GetDi 读取。
            #endregion
            //参数1:  doType
            //指定数字IO类型
            //MC_ENABLE(该宏定义为10)：驱动器使能。 
            //MC_CLEAR(该宏定义为11)：报警清除。 
            //MC_GPO(该宏定义为12)：通用输出。 
            //参数2:  doIndex
            //输出IO的索引。 
            //取值范围： 
            //doType = MC_ENABLE时：[1, 8]。 
            //doType = MC_CLEAR时：[1, 8]。 
            //doType = MC_GPO时：[1, 16]。 
            //参数3:   value 设置数字IO输出电平。 
            //默认情况下，1表示高电平，0表示低电平。
            gts.mc.GT_SetDoBit(gts.mc.MC_GPO, 1, 1);
            gts.mc.GT_SetDoBit(gts.mc.MC_GPO, 2, 0);
        }
        /// <summary>
        /// 气缸下降
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            gts.mc.GT_SetDoBit(gts.mc.MC_GPO, 1, 0);
            gts.mc.GT_SetDoBit(gts.mc.MC_GPO, 2, 1);
        }
        /// <summary>
        /// 回原点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            // 读取点位模式运动参数
            TTrapPrm trapPrm;

            // 启动Home捕获
            rtn = GT_SetCaptureMode(Axis, CAPTURE_HOME);
            if (rtn != 0)
            {
                MessageBox.Show("启动Home捕获失败" + rtn + "轴号: " + Axis);
            }
            // 切换到点位运动模式
            rtn = GT_PrfTrap(Axis);
            if (rtn != 0)
            {
                MessageBox.Show("切换到点位运动模式失败" + rtn + "轴号: " + Axis);
            }
           
            rtn = GT_GetTrapPrm(Axis, out trapPrm);
            if (rtn != 0)
            {
                MessageBox.Show("读取点位模式运动参数失败" + rtn + "轴号: " + Axis);
            }
            trapPrm.acc = 0.25;
            trapPrm.dec = 0.25;
            // 设置点位模式运动参数
            rtn = GT_SetTrapPrm(Axis, ref trapPrm);
            if (rtn != 0)
            {
                MessageBox.Show("设置点位模式运动参数失败" + rtn + "轴号: " + Axis);
            }
            // 设置点位模式目标速度，即回原点速度
            rtn = GT_SetVel(Axis, 10);
            if (rtn != 0)
            {
                MessageBox.Show("设置点位模式目标速度失败" + rtn + "轴号: " + Axis);
            }
            // 设置点位模式目标位置，即原点搜索距离
            rtn = GT_SetPos(Axis, -200000);
            if (rtn != 0)
            {
                MessageBox.Show("设置点位模式目标位置失败" + rtn + "轴号: " + Axis);
            }
            // 启动运动
            rtn = GT_Update(1 << (Axis - 1));
            if (rtn != 0)
            {
                MessageBox.Show("启动运动失败" + rtn + "轴号: " + Axis);
            }
            // 9. 等待捕获触发
          
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

            // 指令返回值变量 
            short sRtn;
            // 坐标系运动状态查询变量 
            short run;
            // 坐标系运动完成段查询变量 
            long segment;
            // 坐标系的缓存区剩余空间查询变量 
            int space;


            ushort MC_GPO = 10;
            // 即将把数据存入坐标系1的FIFO0中，所以要首先清除此缓存区中的数据 
            sRtn = GT_CrdClear(1, 0);

            // 向缓存区写入第一段插补数据 
            sRtn = GT_LnXY(
             1,     // 该插补段的坐标系是坐标系1 
             -2000, -2000,   // 该插补段的终点坐标(200000, 0) 
             30,     // 该插补段的目标速度：100pulse/ms 
             0.1,     // 插补段的加速度：0.1pulse/ms^2 
             0,     // 终点速度为0 
             0);     // 向坐标系1的FIFO0缓存区传递该直线插补数据 

            // 缓存区数字量输出 
            sRtn = GT_BufIO(
             1,    // 坐标系是坐标系1 
             MC_GPO,  // 数字量输出类型：通用输出 
             0xffff,   // bit0~bit15全部都输出 
             0x55,   // 输出的数值为0x55 
             0);    // 向坐标系1的FIFO0缓存区传递该数字量输出 

            // 启动坐标系1的FIFO0的插补运动 
            //参数1: 启动的坐标系是坐标系 1: 启动坐标系, 0:,不启动
            //参数2: 从bit0~bit1按位表示坐标系需要启动的缓存区的编号。 
            sRtn = GT_CrdStart(1, 0);
        }
    }
}