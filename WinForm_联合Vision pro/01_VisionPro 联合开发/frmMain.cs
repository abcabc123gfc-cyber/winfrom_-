using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_VisionPro_联合开发
{
    public partial class frmMain : Form
    {
        #region 初始化
        //初始化工具快
        CogToolBlock CogToolBlock = null;
        //初始化图片
        CogImage24PlanarColor CogImage24PlanarColor = null;
        //初始化
        #endregion
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //加载 vpp文件
            string filePath = @"C:\Users\Administrator\OneDrive\Desktop\VP_文件\CogToolBlock_答题卡.vpp";
        }
    }
}
