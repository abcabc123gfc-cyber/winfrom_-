using Cognex.VisionPro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_联合Vision_pro
{
    public partial class Load_frmCamera : Form
    {
        private string vppPath = Directory.GetCurrentDirectory() + @"\acq.vpp";
        CogAcqFifoTool cogAcqFifoTool;
        public Load_frmCamera()
        {
            InitializeComponent();
            
        }
        
        private void Load_frmCamera_Load(object sender, EventArgs e)
        {
            //加载vpp
            cogAcqFifoTool = CogSerializer.LoadObjectFromFile(vppPath) as CogAcqFifoTool;

            cogAcqFifoEditV21.Subject = cogAcqFifoTool;
        }
        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (cogAcqFifoTool.Operator != null)
            {
                cogAcqFifoTool.Run();
                cogRecordDisplay1.Image = cogAcqFifoTool.OutputImage;
            }
        }
        /// <summary>
        /// 实时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "实时显示")
            {
                button2.Text = "关闭实时";
                cogRecordDisplay1.StartLiveDisplay(cogAcqFifoTool.Operator, false);
            }
            else
            {
                button2.Text = "实时显示";
                cogRecordDisplay1.StopLiveDisplay();
            }
        }
        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (cogAcqFifoTool.Operator != null)
            {
            //Disconnect: 断开
            cogAcqFifoTool.Operator.FrameGrabber.Disconnect(false);
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string imgPath = Directory.GetCurrentDirectory() + @"\Image";
            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }


            //cogAcqFifoTool.OutputImage  需要保存的图片

            // 从文件加载图像
            Bitmap bitmap = cogAcqFifoTool.OutputImage.ToBitmap();

            // 保存为 JPEG，指定质量（可选）
            bitmap.Save($"{imgPath}\\{DateTime.Now.ToString("yyyyMMddHHmmsss")}.jpeg", ImageFormat.Jpeg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CogSerializer.SaveObjectToFile(cogAcqFifoTool, vppPath);
        }
    }
}
