using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatchRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 绑定事件：给某个控件设置业务逻辑。
        // 如何绑定：1。双击，绑定的是默认事件  2。切换“闪电”图标，找到相应事件，双击即可。
        // 事件执行的时机？点击事件什么时候执行？用户点击控件执行
        // 方法btnFolder_Click，就是事件的业务逻辑。
        private void btnFolder_Click(object sender, EventArgs e)
        {
            // 实例化一个目录选择对话框
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            // 判断一下对话框的结果（打开，取消）
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // 把选择的目录路径SelectedPath赋值给输入框
                txtPath.Text = dialog.SelectedPath;

                ShowFiles(txtPath.Text);
            }
        }

        // 把某个路径下的所有文件显示到原文件列表中
        private void ShowFiles(string path)
        {
            // 实例化一个目录信息
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            // 从目录中拿目录下的所有文件
            FileInfo[] files = directoryInfo.GetFiles();
            // 循环所有文件
            foreach (FileInfo file in files)
            {
                // 向原文件列表中添加文件
                lbFileList.Items.Add(file.FullName);
            }
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            // 1。做校验
            // 判断输入框中是否已经存在路径
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("请先选择目录，再对目录下的文件进行重命名！");
                return;
            }
            // 判断原文件列表中是否存在文件
            if (lbFileList.Items.Count == 0)
            {
                MessageBox.Show("你选择目录没有可以重命名的文件，请切换目录！");
                return;
            }

            // 2。重命名
            // 实例化一个目录信息
            DirectoryInfo directoryInfo = new DirectoryInfo(txtPath.Text);
            // 从目录中拿目录下的所有文件
            FileInfo[] files = directoryInfo.GetFiles();
            string path = string.Empty;
            // 循环所有文件
            for (int i = 0; i < files.Length; i++)
            {
                // 1。拿到单个文件
                FileInfo file = files[i];

                // 2。重命名的目标文件
                string destFileName = "";
                // 2.1.获取路径中最后一个反斜杠所在的索引
                int index = file.FullName.LastIndexOf("\\");
                // 2.2.截取除文件名以外的路径
                path = file.FullName.Substring(0, index + 1);
                // 2.3.获取新的文件名
                // 2.3.1 先把原文件名拆分成数组，数组第一项是文件名，第二项是后缀名
                string[] arr = file.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                // 2.3.2.原来的文件名（不带后缀名的）
                string oldFileName = arr[0];
                // 2.3.3.拿原来的文件后缀名
                string ext = arr[1];
                // 2.3.4.新的文件名
                string newFileName = oldFileName + (i + 1).ToString() + "." + ext;
                // 2.3.5.再拼接
                destFileName = Path.Combine(path, newFileName);

                // 3. 重命名
                File.Move(file.FullName, destFileName);

            }

            // 3. 显示到右侧的列表中
            ShowRenameFiles(txtPath.Text);

            // 4. 更改状态栏
            tsslInfo.Text = $"目录：{path}下文件重命名完成。共计：{files.Length}个文件！";
            tsslInfo.ForeColor = Color.Red;

            // 5. 给个提示
            MessageBox.Show("批量重命名成功！");

        }

        // 显示重命名后的文件列表
        private void ShowRenameFiles(string path)
        {
            // 实例化一个目录信息
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            // 从目录中拿目录下的所有文件
            FileInfo[] files = directoryInfo.GetFiles();
            // 循环所有文件
            foreach (FileInfo file in files)
            {
                // 向重命名后的文件列表中添加文件
                lblReFileList.Items.Add(file.FullName);
            }
        }

        // 把原文件列表清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (lbFileList.Items.Count > 0)
            {
                lbFileList.Items.Clear();
            }
        }

        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("请先选择目录！");
                return;
            }

            // Process进程， Start()启动
            Process.Start(txtPath.Text);
        }

        private void btnOpenSoftDirectory_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory);
        }
    }
}
