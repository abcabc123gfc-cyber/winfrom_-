using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 字段，是否需要保存的标识
        /// </summary>
        bool isNeedSave = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void 新建文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* Form2 form2 = new Form2();
             // 给子窗体form2设置父窗体  this就是Form1的实例
             form2.MdiParent = this;
             // Show显示窗体， ShowDialog()以对话框的形式显示窗体
             form2.Show();*/

            if (isNeedSave)// 需要保存
                保存ToolStripMenuItem.PerformClick();// Perform执行
            richTextBox1.Text = string.Empty;
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 文件的完整路径
                string fileName = openFileDialog.FileName;

                // 把文件中的文本读取出来，赋值给richTextBox1
                richTextBox1.Text = File.ReadAllText(fileName);
                isNeedSave = false;
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件 (*.txt)|*.txt";
            saveFileDialog.Title = "保存";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 使用流进行文件保存(代码有瑕疵，阻塞，界面卡死)
                using (FileStream stream = File.Create(saveFileDialog.FileName))
                {
                    byte[] data = Encoding.UTF8.GetBytes(richTextBox1.Text);
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件 (*.txt)|*.txt";
            saveFileDialog.Title = "另存为";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 富文本控件的SaveFile()可以直接保存文件
                richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //richTextBox1.p
            //using (PageSetupDialog pageSetupDialog = new PageSetupDialog())
            //{
            //     if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            //     {
            //         // 获取打印参数
            //         PageSettings pageSettings = pageSetupDialog.PageSettings;
            //     }

            //}
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 关闭当前窗体
            //this.Close();

            // 退出应用程序
            Application.Exit();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();// 撤销
            //richTextBox1.Redo();// 重做
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
                richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = richTextBox1.SelectedText;
            int index = richTextBox1.Text.IndexOf(str);
            if (index != -1)
            {
                richTextBox1.Text = richTextBox1.Text.Remove(index, richTextBox1.SelectedText.Length);

                if (index >= 0 && index <= richTextBox1.Text.Length)
                {
                    richTextBox1.SelectionStart = index;
                    richTextBox1.ScrollToCaret(); // 滚动到当前光标位置
                }
            }
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelFocus(sender, e);
            //自带查找
            //richTextBox1.Find(textBox1.Text);

        }
        private void PanelFocus(object sender, EventArgs e)
        {
            panel1.Visible = true;
            int index = richTextBox1.SelectionStart;
            int indexEnd = richTextBox1.SelectionLength;
            textBox1.Text = richTextBox1.Text.Substring(index, indexEnd);
            //清空 富文本框背景颜色
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            if (index <= 0)
            {
                richTextBox1.Select(0, 0);
                return;
            }
            richTextBox1.Select(index, textBox1.Text.Length);
        }

        private void 查找上一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelFocus(sender, e);
        }

        private void 查找下一个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelFocus(sender, e);
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PanelFocus(sender, e);
        }

        private void 转到ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PanelFocus(sender, e);
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 时间日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Insert(index, DateTime.Now.ToString());
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Font = fontDialog.Font;
                }
            }

        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Size > 40)
            {
                return;
            }
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size + 1, richTextBox1.Font.Style);
            richTextBox1.Select(0, 0);
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font.Size < 7)
            {
                return;
            }
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size - 1, richTextBox1.Font.Style);
            richTextBox1.Select(0, 0);
        }

        private void 还原默认缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 12, richTextBox1.Font.Style);
            richTextBox1.Select(0, 0);
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1。写法1
            /*
            if (状态栏ToolStripMenuItem.Checked)
            {
                // Visible属性控制控件的显示和隐藏
                状态栏ToolStripMenuItem.Checked = false;
                statusStrip1.Visible = false;
            }
            else
            {
                状态栏ToolStripMenuItem.Checked = true;
                statusStrip1.Visible = true;
            }*/

            // 2。写法2
            // 连等执行的顺序：右到左
            //状态栏ToolStripMenuItem.Checked = statusStrip1.Visible = 状态栏ToolStripMenuItem.Checked ? false : true;

            // 3。写法3
            //状态栏ToolStripMenuItem.Checked = statusStrip1.Visible = !状态栏ToolStripMenuItem.Checked;

            // 4。写法4
            if (状态栏ToolStripMenuItem.Checked)
            {
                // Visible属性控制控件的显示和隐藏
                状态栏ToolStripMenuItem.Checked = false;
                statusStrip1.Visible = false;
                return;
            }

            状态栏ToolStripMenuItem.Checked = true;
            statusStrip1.Visible = true;

        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.WordWrap = !richTextBox1.WordWrap;
            自动换行ToolStripMenuItem.Checked = richTextBox1.WordWrap;


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // 文本发生变化时，需要保存。
            isNeedSave = true;
            撤销ToolStripMenuItem.Enabled = richTextBox1.CanUndo;
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                剪切ToolStripMenuItem.Enabled = true;
                复制ToolStripMenuItem.Enabled = true;
                删除ToolStripMenuItem.Enabled = true;
            }
            else
            {
                剪切ToolStripMenuItem.Enabled = false;
                复制ToolStripMenuItem.Enabled = false;
                删除ToolStripMenuItem.Enabled = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Hide();
            richTextBox1.Font = new System.Drawing.Font("宋体", 15);
            richTextBox2.Font = new System.Drawing.Font("微软雅黑", 15);

            richTextBox1.Text = "欢迎使用记事本,测试文本信息,www 文 文";
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            InitPanel();
        }

        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                InitPanel();

            }
        }
        //初始化pane控件
        private void InitPanel()
        {
            panel1.Hide();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
        }

        private void panel1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            (sender as Panel).Focus();
        }
        /// <summary>
        /// 查找上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入查找内容");
            }
            int index = richTextBox1.SelectionStart;
            int indexEnd = richTextBox1.SelectionLength;
            //MessageBox.Show(indexEnd.ToString());
            string text = richTextBox1.Text.Substring(0, index);

            int findIndex = text.LastIndexOf(textBox1.Text.Trim());
            if (findIndex == -1)
            {
                MessageBox.Show("没有找到");
                return;
            }
            if (index + indexEnd >= richTextBox1.Text.Length)
            {
                richTextBox1.Select(0, 0);
                return;
            }
            if (indexEnd <= 0)
            {
                indexEnd = textBox1.Text.Trim().Length;
            }
            richTextBox1.Focus();
            richTextBox1.Select(findIndex, indexEnd);
            richTextBox2.Text = text.ToString();
        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入查找内容");
            }

            string str = richTextBox1.Text;
            int tempIndex = 0;
            int index = richTextBox1.SelectionStart;
            tempIndex = str.IndexOf(textBox1.Text.Trim(), index);
            if (tempIndex == -1)
            {
                MessageBox.Show("没有找到");
                return;
            }
            richTextBox1.Text = richTextBox1.Text.Substring(0, tempIndex) + textBox2.Text.Trim() + richTextBox1.Text.Substring(tempIndex + textBox1.Text.Trim().Length);
            //richTextBox1.Text.Replace(textBox1.Text.Trim(), textBox2.Text.Trim());
            richTextBox1.Focus();
            richTextBox1.Select(tempIndex, textBox2.Text.Trim().Length);
            richTextBox1.SelectionStart = tempIndex + textBox2.Text.Trim().Length;



        }
        /// <summary>
        /// 查找下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入查找内容");
            }
            int index = richTextBox1.SelectionStart;
            int indexEnd = richTextBox1.SelectionLength;
            if (indexEnd <= 0)
            {
                indexEnd = textBox1.Text.Trim().Length;
            }
            string text = richTextBox1.Text.Substring(index + indexEnd);
            int findIndex = text.IndexOf(textBox1.Text.Trim()) + 1;
            //MessageBox.Show(index.ToString());
            if (findIndex == 0)
            {
                MessageBox.Show("没有找到");
                return;
            }

            if (index + indexEnd >= richTextBox1.Text.Length)
            {
                richTextBox1.Select(0, 0);
                return;
            }

            richTextBox1.Focus();
            richTextBox1.Select(index + findIndex, indexEnd);
            richTextBox2.Text = text.ToString();
        }
        /// <summary>
        /// 全部查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            int i = textBox1.Text.Trim().Length;
            //MessageBox.Show(i.ToString());
            //高亮 存储全部查找到的索引
            Dictionary<int, int> dHighlightsic = new Dictionary<int, int>();
            string str = richTextBox1.Text;
            int tempIndex = 0;
            while (str.IndexOf(textBox1.Text.Trim(), tempIndex) != -1)
            {
                tempIndex = str.IndexOf(textBox1.Text.Trim(), tempIndex);
                dHighlightsic.Add(tempIndex, textBox1.Text.Trim().Length);
                richTextBox1.Select(tempIndex++, textBox1.Text.Trim().Length);
                richTextBox1.SelectionBackColor = Color.Yellow;
            }


            //while ()
            //{

            //}
        }
        /// <summary>
        /// 替换全部
        /// </summary>
        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入查找内容");
            }

            string str = richTextBox1.Text;
            int tempIndex = 0;
            int index = richTextBox1.SelectionStart;
            tempIndex = str.IndexOf(textBox1.Text.Trim(), index);
            if (tempIndex == -1)
            {
                MessageBox.Show("没有找到");
                return;
            }
            richTextBox1.Text = richTextBox1.Text.Replace(textBox1.Text.Trim(), textBox2.Text.Trim());
        }
        /// <summary>
        /// 转到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入查找内容");
            }
            richTextBox1.Focus();
            int index = richTextBox1.Find(textBox1.Text.Trim());
            richTextBox1.SelectionStart = index;
            richTextBox1.Select(index, textBox1.Text.Trim().Length);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
