using Cognex.VisionPro;
using Cognex.VisionPro.ColorMatch;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinForm_联合Vision_pro
{
    public partial class Form1 : Form
    {
        #region 初始化
        //vpp路径
        string path = @"C:\Users\Administrator\OneDrive\Desktop\VP_文件\ToolBlock_颜色识别.vpp";
        //视觉工具块, 用于组织和运行视觉工具块
        CogToolBlock toolBlock;
        //图像文件工具, 用于读取图像文件
        CogImageFileTool cogImageFileTool = new CogImageFileTool();
        //实例化颜色工具
        CogColorMatchTool cogColorMatchTool;

        //单张图片路径
        ICogImage imagePath;
        //图片列表集合
        List<FileInfo> imageList;
        //存储图片
        ICogImage cogImage;

        CogColorMatchTool cogColorMatchTool1;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.jpg;*.png;*.jpeg;*.bmp";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                cogImageFileTool.Operator.Open(openFileDialog.FileName, CogImageFileModeConstants.Read);
                cogImageFileTool.Run();
                imagePath = cogImageFileTool.OutputImage as CogImage24PlanarColor;


            }
        }
        /// <summary>
        /// 加载图片文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                string str = folder.SelectedPath;
                DirectoryInfo directoryInfo = new DirectoryInfo(str);
                imageList = directoryInfo.GetFiles("*.jpg")
     .Concat(directoryInfo.GetFiles("*.png"))
     .Concat(directoryInfo.GetFiles("*.bmp"))
     .ToList();
            }
            listBox1.DataSource = imageList;
        }
        int index = 0;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            bool isSelect = false;
            if (toolBlock == null)
            {
                return;
            }
            if (imageList != null)
            {
                isSelect = true;
            }
            else if (imagePath != null)
            {
                isSelect = false;
            }
            else
            {
                return;
            }
            if (!isSelect)
            {
                toolBlock.Inputs[0].Value = imagePath;
              
            }
            else
            {
                if (index < imageList.Count - 1)
                {
                    imagePath = null;
                    cogImageFileTool.Operator.Open(imageList[index++].FullName, CogImageFileModeConstants.Read);
                    cogImageFileTool.Run();
                    imagePath = cogImageFileTool.OutputImage;
                    toolBlock.Inputs[0].Value = imagePath;
                    listBox1.SelectedIndex = index;
                }
                else
                {
                    index = 0;
                    return;
                }
            }
            toolBlock.Run();
            cogColorMatchTool1 = toolBlock.Tools["CogColorMatchTool1"] as CogColorMatchTool;
            label2.Text = "识别结果：" + cogColorMatchTool1.Result.ResultOfBestMatch.Color.Name;
            cogRecordDisplay1.Record = toolBlock.CreateLastRunRecord().SubRecords[1];

            cogRecordDisplay2.Image = imagePath;
            cogRecordDisplay1.Fit();
            cogRecordDisplay2.Fit();
            sw.Stop();
            label1.Text = "运行时间：" + sw.ElapsedMilliseconds.ToString() + "ms";
        }
        /// <summary>
        /// 加载 vpp文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //toolBlock = new CogToolBlock();
            toolBlock = CogSerializer.LoadObjectFromFile(path) as CogToolBlock;
        }
    }

}
