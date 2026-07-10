using PreviewDemo;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace _16_海康威视_监控
{
    public partial class Form1 : Form
    {
        private int UserID = -1;
        //存储用户信息的结构体
        CHCNetSDK.NET_DVR_USER_LOGIN_INFO pLogInfo;
        /// <summary>
        /// 设备信息
        /// </summary>
        private CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo;
        /// <summary>
        /// 错误码
        /// </summary>
        private uint lastError;
        /// <summary>
        /// 日志文件夹
        /// </summary>
        private string logDir = Path.Combine(Environment.CurrentDirectory, "../SdkLogs");
        /// <summary>
        /// 预览句柄 预览的标识
        /// </summary>
        private int lRealHandel = -1;
        /// <summary>
        /// 录像的文件夹
        /// </summary>
        private string videoDir = Path.Combine(Environment.CurrentDirectory, "../Videos");
        /// <summary>
        /// 图片的文件夹
        /// </summary>
        private string ImageDir = Path.Combine(Environment.CurrentDirectory, "../Images");
        /// <summary>
        /// 是否开始录像
        /// </summary>
        private bool IsOpen = false;
        public Form1()
        {
            InitializeComponent();
            //初始化SDK 调用其他函数之前必须调用此函数
            if (!CHCNetSDK.NET_DVR_Init())
            {
                lastError = CHCNetSDK.NET_DVR_GetLastError();
                //记录日志
                CHCNetSDK.NET_DVR_SetLogToFile(3, logDir, true);
                
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtIP.Text == "" || txtPort.Text == "" || txtAc.Text == "" || txtPw.Text == "")
            {
                MessageBox.Show("参数不能为空");
                return;
            }
            if (UserID < 0)
            {
                #region 初始化结构体
                pLogInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO()
                {
                    //sDeviceAddress = Encoding.UTF8.GetBytes(txtIP.Text),

                };
                //设备IP地址或者域名
                byte[] byIP = Encoding.Default.GetBytes(txtIP.Text);
                pLogInfo.sDeviceAddress = new byte[129];
                byIP.CopyTo(pLogInfo.sDeviceAddress, 0);

                //设备用户名
                byte[] byUserName = Encoding.Default.GetBytes(txtAc.Text);
                pLogInfo.sUserName = new byte[64];
                byUserName.CopyTo(pLogInfo.sUserName, 0);

                //设备密码
                byte[] byPassword = Encoding.Default.GetBytes(txtPw.Text);
                pLogInfo.sPassword = new byte[64];
                byPassword.CopyTo(pLogInfo.sPassword, 0);

                //设备服务端口号
                pLogInfo.wPort = ushort.Parse(txtPort.Text);
                #endregion
                //获取设备信息
                DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();
                // 登录设 备
                UserID = CHCNetSDK.NET_DVR_Login_V40(ref pLogInfo, ref DeviceInfo);
                //MessageBox.Show("登录结果：" + (UserID >= 0 ? "成功" : "失败"));
                if (UserID < 0)
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();
                    //记录日志
                    CHCNetSDK.NET_DVR_SetLogToFile(3, logDir, true);
                    MessageBox.Show("登录失败,错误码===" + lastError);
                    return;
                }
                else
                {
                    MessageBox.Show("登录成功");
                    Userinfo.Text = UserID.ToString();
                    btnLogin.Text = "退出";
                    return;
                }

            }
            else
            {
                btnLogin.Text = "登录";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CHCNetSDK.NET_DVR_Cleanup();
        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (UserID < 0)
            {
                MessageBox.Show("请先登录,再预览");
                return;
            }

            if (lRealHandel < 0)
            {


                //结构 保存预览参数
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();

                lpPreviewInfo.hPlayWnd = pictureBox1.Handle;//预览窗口
                lpPreviewInfo.lChannel = 1;//预览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;



                IntPtr pUser = new IntPtr();



                //开始预览 参数1,用户id 参数2 预览参数 参数3null 参数4 用户数据
                lRealHandel = CHCNetSDK.NET_DVR_RealPlay_V40(UserID, ref lpPreviewInfo, null, pUser);

                if (lRealHandel < 0)
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();
                    //记录日志
                    CHCNetSDK.NET_DVR_SetLogToFile(3, logDir, true);
                    MessageBox.Show("预览失败,错误码====" + lastError);
                    return;
                }
                else
                {
                    button1.Text = "停止预览";
                }
            }
            else
            {
                if (!CHCNetSDK.NET_DVR_StopRealPlay(lRealHandel))
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();

                    MessageBox.Show("停止预览失败,错误码===" + lastError);
                    return;
                }

                lRealHandel = -1;
                //停止预览
                button1.Text = "开始预览";
            }
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (lRealHandel < 0)
            {
                MessageBox.Show("请先开始预览,再开始录像");
                return;
            }

            //录像保存的路径
            string videosPath = videoDir + $"/{((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds()}.mp4";
            if(!Directory.Exists(videoDir))
            {
                Directory.CreateDirectory(videosPath);
            }
            if (!IsOpen)
            {
                //开始录像

                int lChannel = 1; //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(UserID, lChannel);

                if (!CHCNetSDK.NET_DVR_SaveRealData(lRealHandel, videosPath))
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();

                    MessageBox.Show("停止预览失败,错误码===" + lastError);
                    return;
                }
                else
                {
                    button2.Text = "停止录像";
                    IsOpen = !IsOpen;
                }

            }
            else
            {
                //停止录像
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(lRealHandel))
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();
                   
                    MessageBox.Show("保存失败,错误码===" + lastError);
                    return;
                }
                else
                {
                    button2.Text = "开始录像";
                    IsOpen = !IsOpen;
                }

            }
        }
        /// <summary>
        /// 抓图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (lRealHandel < 0)
            {
                MessageBox.Show("请先开始预览,再开始录像");
                return;
            }
            if (!Directory.Exists(ImageDir))
            {
                Directory.CreateDirectory(ImageDir);
            }

            if (radioButton1.Checked)
            {
                //JPEG
                string ImagePathJpeg = ImageDir + $"/{((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds()}.Jpeg";


                //结构体 设置图像信息
                CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
                lpJpegPara.wPicQuality = 0; //图像质量 Image quality
                lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
                int lChannel = 1;

                //JPEG抓图 Capture a JPEG picture
                if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(UserID, lChannel, ref lpJpegPara, ImagePathJpeg))
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();

                    MessageBox.Show("保存失败,错误码===" + lastError);
                    return;

                }
                else
                {
                    MessageBox.Show("保存成功");
                }



            }
            else
            {
                //BMP
                string ImagePathBMP = ImageDir + $"/{((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds()}.bmp";

                if (!CHCNetSDK.NET_DVR_CapturePicture(lRealHandel, ImagePathBMP))
                {
                    lastError = CHCNetSDK.NET_DVR_GetLastError();

                    MessageBox.Show("保存失败,错误码===" + lastError);
                    return;

                }
                else
                {
                    MessageBox.Show("保存成功");
                }

            }
        }

        #region 监控移动
        //鼠标按下 开始移动
        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {

            if (lRealHandel < 0)
            {
                MessageBox.Show("请先开始预览,在移动");
                return;
            }

            //参数1: 预览的句柄
            //参数2: 移动的方向
            //参数3:云台停止动作或开始动作：0－开始；1－停止 
            //参数4:移动的速度
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(lRealHandel, CHCNetSDK.PAN_LEFT, 0, 2);
        }

        //鼠标抬起  停止移动
        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(lRealHandel, CHCNetSDK.PAN_LEFT, 1, 2);
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(lRealHandel, CHCNetSDK.PAN_RIGHT, 0, 2);
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(lRealHandel, CHCNetSDK.PAN_RIGHT, 1, 2);
        }
        #endregion
    }
}