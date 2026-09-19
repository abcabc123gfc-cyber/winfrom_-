using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinForm_联合Vision_pro
{
    public partial class Load__qb_job_tool : Form
    {
        public Load__qb_job_tool()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //问题: 当job 的项目中有脚本的时候会报错: 未将对象引用到实例
            //Environment.SpecialFolder.
            //Load__qb_job_tool
            CogJobManager cogJobManager = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory() + @"\Vpp\QuickBuild1.vpp") as CogJobManager;
            //cogJobManagerEdit1.Subject=cogJobManager;

            //获取第一个 job 中的项目
            CogJob cogJob = cogJobManager.Job(0);
            //获取job 中的 全部工具
            CogToolGroup cog = cogJob.VisionTool as CogToolGroup;
            //加载 工具块CogToolBlock1
            //CogToolBlock cogToolBlock = cog.Tools[1] as CogToolBlock;
            CogToolBlock cogToolBlock = cog.Tools["CogToolBlock1"] as CogToolBlock;
            //加载工具块中的工具
            CogImageConvertTool cogImageConvertTool = cogToolBlock.Tools["CogImageConvertTool1"] as CogImageConvertTool;
            //加载工具
            cogImageConvertEdit1.Subject = cogImageConvertTool;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //加载job
            CogJob cogJob = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory() + @"\Vpp\CogJob1.vpp") as CogJob;
            CogToolGroup cog = cogJob.VisionTool as CogToolGroup;
            cogJobEdit1.Subject = cog;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //加载 tool
            CogPMAlignMultiTool cog = CogSerializer.LoadObjectFromFile(Directory.GetCurrentDirectory() + @"\Vpp\CogPMAlignMultiTool1_tool.vpp") as CogPMAlignMultiTool;
            cogPMAlignMultiEditV21.Subject = cog;
        }
    }
}
