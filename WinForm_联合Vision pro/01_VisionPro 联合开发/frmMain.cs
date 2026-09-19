using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace _01_VisionPro_联合开发
{
    public partial class frmMain : Form
    {
        #region 初始化
        /// <summary>
        /// 初始化工具快
        /// </summary>
        CogToolBlock CogToolBlock = null;
      
        /// <summary>
        /// 图片
        /// </summary>
        ICogImage cogImage;
        /// <summary>
        /// 图片文件集合
        /// </summary>
        List<FileInfo> imageFiles = null;
        //需要在 按钮生成解除后归零
        int btnX;
        int btnY = 20;
        int btnXIndex;
        int btnYIndex;
        CogImageFileTool CogImageFileTool = new CogImageFileTool();
        #endregion
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //加载 vpp文件
            string filePath = Directory.GetCurrentDirectory() + @"\VPP\TB_铁丝网_缺陷识别.vpp";
            if (File.Exists(filePath))
            {
                CogToolBlock = CogSerializer.LoadObjectFromFile(filePath) as CogToolBlock;
            }
            else
            {
                MessageBox.Show("加载VPP文件失败");
            }
                cogRecordDisplay1.Fit();
            cogRecordDisplay2.Fit();
        }

        #region 菜单栏
        private void 作业1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmBlock(CogToolBlock).Show();
        }
        private void 相机1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCamera().Show();
        }
        private void 通信设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCommunication().Show();
        }
        private void 选择TBVPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "VPP文件|*.vpp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CogToolBlock = CogSerializer.LoadObjectFromFile(openFileDialog.FileName) as CogToolBlock;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }


        #endregion
        #region 离线测试
        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                imageFiles = directoryInfo.GetFiles("*.jpg").Concat(directoryInfo.GetFiles("*.jpeg")).Concat(directoryInfo.GetFiles("*.png")).ToList();
                checkBox1.Checked = true;
            }
        }
        private void 单张图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片|*.jpg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CogImageFileTool.Operator.Open(openFileDialog.FileName, CogImageFileModeConstants.Read);
                CogImageFileTool.Run();
                cogImage = CogImageFileTool.OutputImage;
                cogRecordDisplay1.Image = cogImage;
            }
        }
        /// <summary>
        /// 离线测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && imageFiles != null)
            {
                batch_process_images(imageFiles);
                return;
            }
            else if (cogImage != null)
            {
                CogToolBlock.Inputs[0].Value = cogImage;
                CogToolBlock.Run();
                bool tempBool = (bool)CogToolBlock.Outputs["Output"].Value;

                cogRecordDisplay2.Record = CogToolBlock.CreateLastRunRecord().SubRecords[1];
            }
        }
        int imageIndex = 0;
        private void batch_process_images(List<FileInfo> imageFiles)
        {
            groupBox1.Controls.Clear();
            //总数
            labelZ.Text = imageFiles.Count.ToString();
            int OK = 0;
            int NG = 0;
            foreach (var item in imageFiles)
            {
                CogImageFileTool.Operator.Open(item.FullName, CogImageFileModeConstants.Read);
                CogImageFileTool.Run();
                cogImage = CogImageFileTool.OutputImage;
                CogToolBlock.Inputs[0].Value = cogImage;
                CogToolBlock.Run();
                bool tempBool = (bool)CogToolBlock.Outputs["Output"].Value;
                InitButton(imageIndex, tempBool, CogToolBlock.CreateLastRunRecord().SubRecords[1]);
                if (tempBool)
                {
                    OK++;
                }
                else
                {
                    NG++;
                }
                imageIndex++;
                //cogRecordDisplay2.Record = CogToolBlock.CreateLastRunRecord().SubRecords[1];
            }
            labelOK.Text = OK.ToString();
            labelLV.Text = ((double)OK / (double)imageFiles.Count).ToString("p");
            imageIndex = 0;
            btnX = 0;
            btnY = 20;
            btnXIndex = 0;
            btnYIndex = 0;
            imageFiles = null;
        }

        /// <summary>
        /// 动态绑定按钮
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cogRecord"></param>
        private void InitButton(int index, bool value, ICogRecord cogRecord)
        {
            if (groupBox1.Width - 50 <= btnX)
            {
                btnY += 90;
                btnX = 0;
            }
            Button button1 = new Button();
            button1.Text = index.ToString();
            button1.Tag = cogRecord;
            button1.Size = new Size(70, 70);
            button1.Location = new Point(btnX, btnY);
            button1.UseVisualStyleBackColor = false;   // 关键
            button1.BackColor = value ? Color.Transparent : ColorTranslator.FromHtml("#FFE4E1");     // 淡红色
            button1.ForeColor = Color.Black;           // 黑色字体
            // 可选：边框
            button1.FlatAppearance.BorderSize = 1;
            button1.FlatAppearance.BorderColor = Color.Silver;
            groupBox1.Controls.Add(button1);
            btnX += 200;
            button1.Click += ImageRecode;
        }
        private void ImageRecode(object sender, EventArgs e)
        {
            Button button = sender as Button;
            cogRecordDisplay2.Record = button.Tag as ICogRecord;
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
