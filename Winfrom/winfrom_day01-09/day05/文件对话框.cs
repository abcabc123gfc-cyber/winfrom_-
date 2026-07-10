using System;
using System.IO;
using System.Windows.Forms;

namespace day05
{
    public partial class 文件对话框 : Form
    {
        public 文件对话框()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择文件";
            openFileDialog.Filter = "txt文件|*.txt";
            // 初始目录
            openFileDialog.InitialDirectory = "D:\\";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    richTextBox1.AppendText(openFileDialog.FileName);
                }
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "请选择保存文件";
            saveFileDialog.Filter = "txt文件|*.txt";
            saveFileDialog.InitialDirectory = "D:\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //创建文件
                File.Create(saveFileDialog.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择目录";
            // 初始目录 默认打开桌面,通过枚举设置打开的目录
            folderBrowserDialog.RootFolder= Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.AppendText(folderBrowserDialog.SelectedPath);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            // 允许选择 自定义颜色
            colorDialog.AllowFullOpen = true;
            // 显示 自定义颜色控件
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // 设置字体
                richTextBox1.Font = fontDialog.Font;
            }
        }
    }
}

