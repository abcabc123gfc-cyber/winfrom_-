using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Windows.Forms;

namespace _01_VisionPro_联合开发
{
    public partial class frmBlock : Form
    {
        //vpp路径
        //string path = @"C:\Users\Administrator\OneDrive\Desktop\VP_文件\ToolBlock_颜色识别.vpp";
        string path = @"C:\Users\Administrator\OneDrive\Desktop\ToolBlock.vpp";
        CogToolBlock cogToolBlock;
        CogImageFileTool cogImageFileTool = new CogImageFileTool();
        public frmBlock(CogToolBlock cogToolBlock)
        {
            InitializeComponent();
            if (cogToolBlock == null)
            {
                MessageBox.Show("加载失败");
                return;
            }
            if (cogToolBlockEditV21 == null)
            {
                MessageBox.Show("初始化失败");
                return;
            }
            try
            {
                cogToolBlockEditV21.Subject = cogToolBlock;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBlock_Load(object sender, EventArgs e)
        {
            cogToolBlock = CogSerializer.LoadObjectFromFile(path) as CogToolBlock;
            //cogImageFileTool.Operator.Open(@"C:\Users\Administrator\OneDrive\Desktop\密封条胶塞颜色识别\1.bmp", CogImageFileModeConstants.Read);
            //cogImageFileTool.Run();
            //cogToolBlock.Inputs[0].Value = cogImageFileTool.OutputImage;
            if (cogToolBlock == null)
            {
                MessageBox.Show("加载失败");
                return;
            }
            if (cogToolBlockEditV21 == null)
            {
                MessageBox.Show("初始化失败");
                return;
            }

            cogToolBlockEditV21.Subject = cogToolBlock;


        }

        private void 保存作业ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, path);
        }
    }
}
