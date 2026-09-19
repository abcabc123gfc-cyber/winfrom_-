using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinForm_联合Vision_pro
{
    public partial class Load_frmCamera_RunTB : Form
    {
        /// <summary>
        /// 相机集合
        /// </summary>
        private CogFrameGrabbers cogFrameGrabbers;
        /// <summary>
        /// 相机
        /// </summary>
        private ICogFrameGrabber cogFrameGrabber;
        /// <summary>
        /// 相机的接口
        /// </summary>
        private ICogAcqFifo cogAcqFifo;
        /// <summary>
        /// 图像
        /// </summary>
        private ICogImage cogImage;
        /// <summary>
        /// 工具块
        /// </summary>
        CogToolBlock toolBlock;
        /// <summary>
        /// 图片文件读取工具
        /// </summary>
        CogImageFileTool cogImageFileTool=new CogImageFileTool();
        public Load_frmCamera_RunTB()
        {
            InitializeComponent();
        }
        #region 初始化 相机取图
        private void Load_frmCamera_RunTB_Load(object sender, EventArgs e)
        {
            //comboBox1
            cogFrameGrabbers = new CogFrameGrabbers();
            if (cogFrameGrabbers.Count == 0)
            {
                MessageBox.Show("没有可用的相机");
                return;
            }
            foreach (ICogFrameGrabber item in cogFrameGrabbers)
            {
                //获取相机 类
                cogFrameGrabber = item;
                comboBox1.Items.Add(cogFrameGrabber.Name);
                //参数1 : 相机类型
                //参数2 : 像素格式
                //参数3 : 相机端口
                //参数4 : 自动准备
                cogAcqFifo = cogFrameGrabber.CreateAcqFifo("Generic GigEVision (Mono)", CogAcqFifoPixelFormatConstants.Format8Grey, 0, true);
                comboBox1.SelectedIndex = 0;
                //Complete 事件. 图像采集完成的时候触发
                cogAcqFifo.Complete += CogAcqFifo_Complete;
            }


        }

        private void CogAcqFifo_Complete(object sender, CogCompleteEventArgs e)
        {
            //事件处理函数执行, 说明图像采集完成
            //采集到的图像
            int numPending, numReady;
            bool busy;
            //参数1: 处于挂起状态的采集请求数（已请求但采集尚未开始）
            //参数2:已就绪、可供完成的采集请求数（已采集完成，可调用 CompleteAcquire 取走）
            //参数3: 最早的未完成采集是否正在等待触发信号或正在采集图像
            cogAcqFifo.GetFifoState(out numPending, out numReady, out busy);

            ICogAcqInfo info = new CogAcqInfo();
            if (numReady > 0)
            {
                //结束采集, 返回已经擦采集的图像
                cogImage = cogAcqFifo.CompleteAcquireEx(info);
                cogRecordDisplay1.Image = cogImage;
            }

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //调用相机拍照
            cogAcqFifo.StartAcquire();
        }
        /// <summary>
        /// 记载TB文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string str = null;
            if (checkBox1.Checked)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "TB文件|*.vpp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    str = ofd.FileName;
                }
            }
            else
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Vpp" + "\\CogToolBlock_答题卡_无脚本.vpp"))
                {
                    str = Directory.GetCurrentDirectory() + "\\Vpp" + "\\CogToolBlock_答题卡_无脚本.vpp";
                }
            }
            if (str != null)
            {
                try
                {
                    cogToolBlockEditV21.Subject = CogSerializer.LoadObjectFromFile(str) as CogToolBlock;
                    toolBlock = cogToolBlockEditV21.Subject;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (toolBlock == null) return;
            toolBlock.Run();
            CogBlobTool cogBlobTool = new CogBlobTool();
            CogBlobTool cogBlobTool1 = toolBlock.Tools["CogBlobTool1"] as CogBlobTool;
            label2.Text = "结果: " + cogBlobTool1.Results.GetBlobs().Count.ToString();
            label2.ForeColor = Color.Red;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (toolBlock == null) return;
            CogSerializer.SaveObjectToFile(toolBlock, Directory.GetCurrentDirectory() + "\\Vpp" + "\\CogToolBlock_答题卡_无脚本1.vpp");
        }
        #region 相机参数设置
        private void button2_Click(object sender, EventArgs e)
        {
            //设置曝光
            cogAcqFifo.OwnedExposureParams.Exposure = Convert.ToDouble(textBox1.Text.Trim());
        }
        #endregion
    }
}
