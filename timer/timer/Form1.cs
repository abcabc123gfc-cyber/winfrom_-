using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace timer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            label1.Text = label1.Text.Substring(1) + label1.Text.Substring(0, 1);
            label2.Text = DateTime.Now.ToString("F");
            //定时闹钟
            if (DateTime.Now.Minute == 7 || DateTime.Now.Minute == 8)
            {

                //SoundPlayer sp= new SoundPlayer("D:\\SteamLibrary\\steamapps\\workshop\\content\\431960\\3679122549\\music\\OnyXXX.mp3");
                //    sp.Play();
                //字体对话框
                FontDialog fd = new FontDialog();
                fd.ShowDialog();
                textBox1.Font = fd.Font;
                //颜色对话框
                ColorDialog cd = new ColorDialog();
                cd.ShowDialog();
                textBox1.ForeColor = cd.Color;
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if (p.ProcessName == "notepad11111")
                {
                    p.Kill();
                }
                listBox1.Items.Add(p.ProcessName);

            }

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            //通过进程打开一些程序
            //Process.Start("notepad11111");
            //Process.Start("iexplor", "http://www.baidu.com");

            //通过进程打开指定的文件
            //声明启动信息
            //ProcessStartInfo info = new ProcessStartInfo(@"D:\EchoMusic\EchoMusic.exe");
            //创建进程对象
            //Process p =new Process();
            //p.StartInfo = info;
            //p.Start();
            // 多线程
            Thread thread=new Thread(Test);
            //thread.Abort();
            //设置为后台线程
            thread.IsBackground=true;
            thread.Start();
            
        }
        void Test()
        {
            listBox1.Items.Add("线程");
        }
    }
}
