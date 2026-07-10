using System;
using System.IO;
using System.Windows.Forms;

namespace day05_prictice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.ReadOnly=true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择文件";
            openFileDialog.Filter = "txt文件|*.txt";
            // 初始目录
            openFileDialog.InitialDirectory = "D:\\Microsoft Visual Studio\\XiangMu\\Winfrom\\读写文件区";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    textBox1.Text = openFileDialog.FileName;
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                    textBox1.Text=fileInfo.FullName;
                    textBox3.Text = fileInfo.Name;
                    textBox4.Text=fileInfo.CreationTime.ToString();

                    textBox5.Text = fileInfo.Length +"  kb";

                    textBox6.Text=fileInfo.Extension;

                    textBox7.Text=fileInfo.Attributes.ToString();

                    textBox8.Text=fileInfo.IsReadOnly.ToString();


                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path=Path.GetDirectoryName(textBox1.Text);
            path = path + "\\" + Path.GetFileNameWithoutExtension(textBox2.Text) + "_move" + Path.GetExtension(textBox1.Text);
            File.Move(textBox1.Text, path);    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(textBox1.Text);
           path=path+"\\"+Path.GetFileName("新建 文本文档.txt");
            File.Replace(textBox1.Text, path,null);
        }
    }

} 
