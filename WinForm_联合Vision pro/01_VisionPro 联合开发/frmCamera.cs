using Cognex.VisionPro;
using System;
using System.IO;
using System.Windows.Forms;

namespace _01_VisionPro_联合开发
{
    public partial class frmCamera : Form
    {
        /// <summary>
        /// 相机配置文件
        /// </summary>
        private string vppPath = Directory.GetCurrentDirectory() + @"\acq.vpp";
        /// <summary>
        /// 相机的配置文件
        /// </summary>
        private CogAcqFifoTool cogAcq;
        public frmCamera()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载vpp文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCamera_Load(object sender, EventArgs e)
        {
            //加载相机的配置文件
            cogAcq = CogSerializer.LoadObjectFromFile(vppPath) as CogAcqFifoTool;
            //赋值给相机
            cogAcqFifoEditV21.Subject = cogAcq;
        }

        private void 保存相机配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cogAcq = cogAcqFifoEditV21.Subject;
            CogSerializer.SaveObjectToFile(cogAcq, vppPath);
        }
    }
}
