using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace 雷赛运动控制卡
{
    public partial class Form1 : Form
    {
        //轴 0 左右 
        //轴 1 前后
        //轴 2 上下 使用前 去调试软件 Motion 清除错误信息
        string ip = ConfigurationManager.AppSettings["ip"];
        /// <summary>
        /// 控制器链接号
        /// </summary>
        ushort CardNo = ConfigurationManager.AppSettings["CardNo"] == null ? (ushort)8 : Convert.ToUInt16(ConfigurationManager.AppSettings["CardNo"]);
        /// <summary>
        /// 返回值
        /// </summary>
        short rtn = 0;
        /// <summary>
        /// 轴号
        /// </summary>
        ushort Axis { get { return Convert.ToUInt16(nud_AxisId.Value); } }

        private double equiv;
        /// <summary>
        /// 脉冲当量
        /// </summary>
        double Equiv { get { return equiv; } set { equiv = Convert.ToDouble(nud_PulseEquiv.Value); } }
        /// <summary>
        /// 目标位置
        /// </summary>
        public double Distance { get { return Convert.ToDouble(nud_Dist.Value); } }
        /// <summary>
        /// 运行模式 0：相对坐标模式，1：绝对坐标模式
        /// </summary>
        public ushort Myposi_mode = 0;
        /// <summary>
        /// 设置回零方式
        /// </summary>
        ushort Mymode = 28;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ushort state = 0;
                #region 初始化
                //初始化
                //参 数：CardNo 控制器链接号
                //Ipaddr 链接字符串，对应控制器的 IP 地
                //返回值：错误代码
                rtn = LTDMC.dmc_board_init_eth(CardNo, ip);
                if (rtn != 0)
                {
                  
                    throw new Exception("初始化失败" + rtn);
                }
                #endregion
                // 使能对应轴    
                #region 2 给轴使能 在移动回零之前要注意先配置使能
                rtn = LTDMC.nmc_set_axis_enable(CardNo, Axis);//设置指定轴使能,此处 Axis 即当前指定轴
               
                if (rtn != 0)
                {
                    throw new Exception("使能轴失败" + rtn);
                }
                else
                {
                    panel1.BackColor = Color.Green;
                }
                #endregion
                #region 2.1 配置轴的脉冲当量
                //获取
                //参数3: equiv  脉冲当量，单位：pulse / unit
                rtn = LTDMC.dmc_get_equiv(CardNo, Axis, ref equiv);
                //设置
                rtn = LTDMC.dmc_set_equiv(CardNo, Axis, equiv);
                if (rtn != 0)
                {
                    throw new Exception("设置脉冲当量失败" + rtn);
                }
                //nud_PulseEquiv.Value = (dynamic)equiv;
                #endregion
                #region 2.2 设置 单轴 轴的 运动速度曲线
                //设置单轴运动速度曲线
                //参数1 : 控制器链接号 指定轴号 起始速度 最大速度 加速时间 减速时间 停止速度

                LTDMC.dmc_set_profile_unit(CardNo, Axis, (double)nud_StartVel.Value, (double)nud_MaxVel.Value, (double)nud_Tacc.Value, (double)nud_Tdec.Value, (double)nud_StopVel.Value);
                #endregion
                #region 2.3设置轴回零
                //2.3设置轴回零
                /*
                 *  参 数：CardNo 控制器链接号
                    axis EtherCAT 总线轴轴号
                    home_mode EtherCAT 总线轴回零模式???

                    Low_Vel EtherCAT 总线轴回零低速
                    High_Vel EtherCAT 总线轴回零高速
                    Tacc EtherCAT 总线轴回零加速时间
                    Tdec EtherCAT 总线轴回零减速时间
                    Offsetpos EtherCAT 总线轴回零偏移
                 */
                rtn = LTDMC.nmc_set_home_profile(CardNo, Axis, Mymode, 10, 40, 0.1, 0.1, 0);
                if (rtn != 0)
                {
                    throw new Exception("设置回零参数失败！" + rtn);
                }
                rtn = LTDMC.nmc_home_move(CardNo, Axis);//执行回原点运动
                if (rtn != 0)
                {
                    throw new Exception("执行回零运动失败！" + rtn);
                }
                while (LTDMC.dmc_check_done(CardNo, Axis) == 0)// 判断轴运动状态，等待回零运动完成
                {
                    Application.DoEvents(); // 处理界面消息，防止假死
                }
                LTDMC.dmc_get_home_result(CardNo, Axis, ref state);//判断回零是否正常完成。
                if (state == 1) //回零正常完成
                {
                    MessageBox.Show("回零完成");
                }
                #endregion

                //信息显示
                timer2.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #region 定长运动
        /// <summary>
        /// 定长运动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                //参 数：CardNo
                // Axis: 指定指定轴号
                //Dist 目标位置，单位：unit
                //posi_mode 运动模式，0：相对坐标模式，1：绝对坐标模式
                rtn = LTDMC.dmc_pmove_unit(CardNo, Axis, Distance, Myposi_mode);//执行点位运动
                rtn = LTDMC.dmc_pmove_unit(CardNo, Axis, -4000, Myposi_mode);//执行点位运动
                if (rtn != 0)
                {
                    throw new Exception("执行点位运动失败！");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        #endregion
        private void btn_SetPulseEquiv_Click(object sender, EventArgs e)
        {
            equiv = Convert.ToDouble(nud_PulseEquiv.Value);
            rtn = LTDMC.dmc_set_equiv(CardNo, Axis, Equiv);
            if (rtn != 0)
            {
                throw new Exception("设置脉冲当量失败" + rtn);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //参 数：CardNo  控制器链接号
            //停止所有轴运动
            LTDMC.dmc_emg_stop(CardNo);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LTDMC.nmc_set_axis_disable(CardNo, Axis);//设置指定轴失能
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            double dCurrentVel = 0;     //速度
            double dCmdPos = 0;         //指令位置
            double dEnPos = 0;          //编码器反馈位置


            rtn = LTDMC.dmc_read_current_speed_unit(CardNo, Axis, ref dCurrentVel); // 读取轴当前速度
            tb_CurrentVel.Text = dCurrentVel.ToString();

            rtn = LTDMC.dmc_get_position_unit(CardNo, Axis, ref dCmdPos);           //读取指定轴指令位置值
            tb_CurrentPos.Text = dCmdPos.ToString();
            rtn = LTDMC.dmc_get_encoder_unit(CardNo, Axis, ref dEnPos);             //读取指定轴的编码器反馈值


            if (LTDMC.dmc_check_done(CardNo, Axis) == 0)                      //读取指定轴运动状态
            {
                tb_RunState.Text = "运行中";
            }
            else
            {
                tb_RunState.Text = "停止中";
            }

            // 读取指定轴状态机
            ushort usAxisStateMachine = 0;
            LTDMC.nmc_get_axis_state_machine(CardNo, Axis, ref usAxisStateMachine);
            switch (usAxisStateMachine)
            {
                case 0:
                    textBox_StateMachine.Text = "轴处于未启动状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 1:
                    textBox_StateMachine.Text = "轴处于启动禁止状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 2:
                    textBox_StateMachine.Text = "轴处于准备启动状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 3:
                    textBox_StateMachine.Text = "轴处于启动状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 4:
                    textBox_StateMachine.Text = "轴处于操作使能状态";
                    textBox_StateMachine.BackColor = Color.Green;
                    break;
                case 5:
                    textBox_StateMachine.Text = "轴处于停止状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 6:
                    textBox_StateMachine.Text = "轴处于错误触发状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
                case 7:
                    textBox_StateMachine.Text = "轴处于错误状态";
                    textBox_StateMachine.BackColor = Color.Red;
                    break;
            }
            ;

            //读取总线状态
            ushort usErrorCode = 0;
            short a = LTDMC.nmc_get_errcode(CardNo, 2, ref usErrorCode);
            if (usErrorCode == 0)
            {
                textBox_EthercatState.Text = "EtherCAT总线正常";
                textBox_EthercatState.BackColor = Color.Green;
            }
            else
            {
                textBox_EthercatState.Text = "EtherCAT总线出错";
                textBox_EthercatState.BackColor = Color.Red;
            }


            //读取限位原点状态
            uint unIoState = LTDMC.dmc_axis_io_status(CardNo, Axis);
            if ((unIoState & 2) == 2)//检测正限位信号
            {
                panel3.BackColor = Color.Green;
            }
            else
            {
                panel3.BackColor = Color.Red;
            }
            if ((unIoState & 4) == 4)//检测负限位信号
            {
                panel4.BackColor = Color.Green;
            }
            else
            {
                panel4.BackColor = Color.Red;
            }
            if ((unIoState & 16) == 16)//检测原点信号
            {
                panel2.BackColor = Color.Green;
            }
            else
            {
                panel2.BackColor = Color.Red;
            }
        }
        /// <summary>
        /// 连续运动 正向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            ushort MyCardNo, Myaxis, Mydir, Mystop_mode, statemachine;
            double MyMin_Vel, MyMax_Vel, MyTacc, MyTdec, MyStop_Vel, Mys_para;
            //控制器链接号
            MyCardNo = 0;
            //轴号
            Myaxis = 0;
            //起始速度200unit/s
            MyMin_Vel = 0;
            //最大速度5000unit/s
            MyMax_Vel = 10;
            //加速时间0.05s
            MyTacc = 0.05;
            //减速时间0.05s
            MyTdec = 0.05;
            //停止速度200unit/s
            MyStop_Vel = 0;
            //s 段时间
            Mys_para = 0.01;
            //运动方向为正向
            Mydir = 1;
            //停止方式为减速停止
            Mystop_mode = 0;
            //获取轴状态机
            statemachine = 0;
            rtn = LTDMC.nmc_get_axis_state_machine(CardNo, Axis, ref statemachine);//获取轴状态机

            if (statemachine == 4) //监控轴状态机的值，该值等于4表示轴状态机处于准备好状态
            {
                //设置单轴运动速度曲线
                rtn = LTDMC.dmc_set_s_profile(CardNo, Axis, 0, Mys_para);//速度曲线为 s 形
                if (rtn != 0)
                {
                    MessageBox.Show("设置速度曲线失败" + rtn);
                }
                rtn = LTDMC.dmc_set_profile_unit(CardNo, Axis, MyMin_Vel, MyMax_Vel, MyTacc, MyTdec, MyStop_Vel);
                if (rtn != 0)
                {
                    MessageBox.Show("点位失败" + rtn);
                }
                //执行连续运动
                rtn = LTDMC.dmc_vmove(CardNo, Axis, Mydir);

                if (rtn != 0)
                {
                    MessageBox.Show("运行失败，错误码=" + rtn);
                }
                //if (Mydir == 1)
                //{
                //    tb_RunState.Text = "运行中";
                //}
                //else
                //{
                //    tb_RunState.Text = "停止中";
                //}
            }
            else
            {
                MessageBox.Show("轴状态机错误，请检查轴状态机" + "返回值"+ rtn+"状态码"+ statemachine);
            }
        }

        /// <summary>
        /// 连续运动 负向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ushort MyCardNo, Myaxis, Mydir, Mystop_mode, statemachine;
            double MyMin_Vel, MyMax_Vel, MyTacc, MyTdec, MyStop_Vel, Mys_para;
            //控制器链接号
            MyCardNo = 0;
            //轴号
            Myaxis = 0;
            //起始速度200unit/s
            MyMin_Vel = 0;
            //最大速度5000unit/s
            MyMax_Vel = 10;
            //加速时间0.05s
            MyTacc = 0.05;
            //减速时间0.05s
            MyTdec = 0.05;
            //停止速度200unit/s
            MyStop_Vel = 0;
            //s 段时间
            Mys_para = 0.01;
            //运动方向为正向
            Mydir = 0;
            //停止方式为减速停止
            Mystop_mode = 0;
            //获取轴状态机
            statemachine = 0;
            rtn = LTDMC.nmc_get_axis_state_machine(CardNo, Axis, ref statemachine);//获取轴状态机
            MessageBox.Show("状态返回值" + rtn);
            if (statemachine == 4) //监控轴状态机的值，该值等于4表示轴状态机处于准备好状态
            {
                //设置单轴运动速度曲线
                rtn = LTDMC.dmc_set_s_profile(CardNo, Axis, 0, Mys_para);//速度曲线为 s 形
                if (rtn != 0)
                {
                    MessageBox.Show("设置速度曲线失败" + rtn);
                }
                rtn = LTDMC.dmc_set_profile_unit(CardNo, Axis, MyMin_Vel, MyMax_Vel, MyTacc, MyTdec, MyStop_Vel);
                if (rtn != 0)
                {
                    MessageBox.Show("点位失败" + rtn);
                }
                //执行连续运动
                rtn = LTDMC.dmc_vmove(CardNo, Axis, Mydir);
                if (rtn != 0)
                {
                    MessageBox.Show("运行失败");
                }
                //if (Mydir == 1)
                //{
                //    tb_RunState.Text = "运行中";
                //}
                //else
                //{
                //    tb_RunState.Text = "停止中";
                //}
            }
        }
        /// <summary>
        /// 硬件复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_HardwareReset_Click(object sender, EventArgs e)
        {

        }

        private void btn_ChangeVel_Click(object sender, EventArgs e)
        {
            //s 段时间
            double Mys_para = 0.01;
            ushort statemachine = 0;
            LTDMC.nmc_get_axis_state_machine(CardNo, Axis, ref statemachine);//获取轴状态机
            //监控轴状态机的值，该值等于4表示轴状态机处于准备好状态
            if (statemachine == 4)
            {
                LTDMC.dmc_set_profile_unit(CardNo, Axis, (double)nud_StartVel.Value, (double)nud_MaxVel.Value, (double)nud_Tacc.Value, (double)nud_Tdec.Value, (double)nud_StopVel.Value); //设置单轴运动速度曲线
                LTDMC.dmc_set_s_profile(CardNo, Axis, Myposi_mode, Mys_para);//设置 S 形速度曲线
                rtn = LTDMC.dmc_pmove_unit(CardNo, Axis, (double)nud_Dist.Value, Myposi_mode);//执行点位运动
                if (rtn == 0)
                {
                    LTDMC.dmc_change_speed_unit(CardNo, Axis, 20, 0);//执行在线，速度变为 unit/s
                }
                else
                {
                    MessageBox.Show("变速失败");
                }
            }
            else
            {
                MessageBox.Show("轴状态机处于未就绪状态");
            }
        }

        private void btnInit(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }
    }
}
