using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day04
{
    public partial class FIle_prictice : Form
    {
        public FIle_prictice()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //源文件路径点击
            textBox1.Text = GetPath();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //目标文件路径点击
            textBox2.Text = GetPath();
        }
        private string GetPath()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "文本文件|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(ofd.FileName);
                    return path;
                }
                return null;

            }
        }
        /// <summary>
        /// 写入文本文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //richTextBox1
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "保存位置";
            //只要txt
            ofd.Filter = "txt文件|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(ofd.FileName))
                {
                    File.Create(ofd.FileName);
                }
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Append))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(richTextBox1.Text);
                    fs.Write(bytes, 0, bytes.Length);
                    richTextBox1.Text = "";
                }
            }
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt文件|*.txt";
            ofd.Title = "请选择文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];

                    fs.Read(bytes, 0, bytes.Length);
                    richTextBox1.Text = "";
                    richTextBox1.Text = Encoding.UTF8.GetString(bytes);
                }
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                string path = Path.GetFullPath(textBox2.Text.Trim());
                if (!File.Exists(path))
                {
                    MessageBox.Show("文件不存在");
                    return;
                }
                File.Delete(path);
                //textBox2.Text = "";
            }
            catch (Exception)
            {

                MessageBox.Show("文件删除失败");
            }

        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            FileOperation("copy");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileOperation("move");
        }
        private void FileOperation(string Tag)
        {
            try
            {

                string sourcePath = Path.GetFullPath(textBox1.Text.Trim());
                string targetPath = Path.GetFullPath(textBox2.Text.Trim());
                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show("文件不存在");
                    return;
                }
                else if (!File.Exists(targetPath))
                {
                    using (File.Create(targetPath))
                    {

                    }

                }
                if (Tag == "move")
                {
                    if (File.Exists(targetPath))
                    {
                        File.Delete(targetPath);
                    }
                    File.Move(sourcePath, targetPath);
                    return;
                }
                else if (Tag == "copy")
                {
                    File.Copy(sourcePath, targetPath, true);
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("文件操作失败");
            }
        }

        private void FIle_prictice_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string folder = FolderDialog();
            if (folder == null)
            {
                MessageBox.Show("目录不存在");
                return;
            }
            richTextBox1.Text = "目录\t" + folder;
            foreach (string file in Directory.GetFiles(folder))
            {
                richTextBox1.Text += "\r\n文件\t" + file;
            }
        }
        /// <summary>
        /// 文件夹对话框 返回路径
        /// </summary>
        /// <returns></returns>
        private string FolderDialog()
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    return fb.SelectedPath;
                }
                else
                {
                    return null;
                }

            }
        }
        /// <summary>
        /// 获取目录下的所有目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button8_Click(object sender, EventArgs e)
        {
            string folder = FolderDialog();
            if (folder == null)
            {
                MessageBox.Show("目录不存在");
                return;
            }
            richTextBox1.Text = "根目录\t" + folder;
            FindDirectoryChild(folder);

        }

        private void FindDirectoryChild(string path)
        {
            try
            {

                foreach (var item in Directory.GetDirectories(path))
                {
                    //richbox 的跨线程支持委托
                    //richTextBox1.BeginInvok/e( new Action());
                    Task.Run(() =>
                    {
                        richTextBox1.Invoke(new Action(() =>
                        {
                            richTextBox1.AppendText("\r\n目录\t" + item);
                        }));
                        //richTextBox1.AppendText("\r\n目录\t" + item);
                    });
                    FindDirectoryChild
                        (item);
                }
                return;
            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            string folder = FolderDialog();
            if (folder == null)
            {
                MessageBox.Show("目录不存在");
                return;
            }
            richTextBox1.Text = "根目录\t" + folder;
            Directory.CreateDirectory(folder + "\\new");
            richTextBox1.Text += "\\new";
        }
        /// <summary>
        /// 删除目录以及子目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            string folder = FolderDialog();
            if (folder == null)
            {
                MessageBox.Show("目录不存在");
                return;
            }
            richTextBox1.Text = "根目录\t" + folder;
            DeleteDirectoryChild(folder);
        }
        private void DeleteDirectoryChild(string path)
        {
            try
            {

                foreach (var item in Directory.GetDirectories(path))
                {
                    FindDirectoryChild(item);
                    //richbox 的跨线程支持委托
                    richTextBox1.Invoke(new Action(() =>
                    {
                        Directory.Delete(item);

                    }));
                }
                return;
            }
            catch (Exception)
            {


            }
        }
    }
}
