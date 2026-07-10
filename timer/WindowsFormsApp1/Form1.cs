using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //会执图形需要 笔 颜色 纸张 两点 绘制的对象

            //Graphics graphics=new Graphics(this);

            //this 当前窗体对象 创建gdi对象
            Graphics graphics = this.CreateGraphics();
            //创建画笔对象
            Pen pen = new Pen(Brushes.Yellow, 1);
            //创建2个点
            Point point1 = new Point(30, 50);
            Point point2 = new Point(100, 100);
            //绘制线 单条
            graphics.DrawLine(pen, point1, point2);

            i++;
            label1.Text = i.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += button1_Click;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //绘制矩形
            //创建GDI
            Graphics graphics = this.CreateGraphics();
            Pen pen = new Pen((Color)Color.White, 1);
            Rectangle rectangle = new Rectangle(50, 50, 100, 100);
            graphics.DrawRectangle(pen, rectangle);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //绘制扇形
            //创建GDI
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen((Color)Color.Blue, 2);
            Rectangle rectangle = new Rectangle(50, 50, 100, 100);
            //参数3 开始角度,参数4结束角度
            g.DrawPie(pen, rectangle, 60, 60);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //绘制文字
            //创建gdi
            Graphics graphics = this.CreateGraphics();
            Pen pen = new Pen((Color)Color.Blue, 3);

            graphics.DrawString("绘制文字", new Font("宋体", 20, FontStyle.Underline), Brushes.Blue, new Point(100, 100));


        }
        /// <summary>
        /// 点击更换验证码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //需要先随机生成文字
            Random rnd = new Random();
            string str = null;
            for (int i = 0; i < 5; i++)
            {
                int rNumber = rnd.Next(0, 10);
                str += rNumber;

            }
            List<string> list = new List<string>();
            //MessageBox.Show(str);
            //创建 bmp图片对象,用于写入绘制的随机文字
            //创建GDI对象
            Bitmap bmp = new Bitmap(100, 20);
            Graphics g = Graphics.FromImage(bmp);
            string[] str1 = { "微软雅黑", "宋体", "黑体", "隶属", "仿宋" };
            Color[] cor = { Color.Blue, Color.Yellow, Color.Black, Color.Red };
            for (int i = 0; i < 5; i++)
            {
                //point的位置相对于image来说,(0,0)就是图片框左上角
                g.DrawString(str[i].ToString(), new Font(str1[rnd.Next(0, 5)], 15, FontStyle.Bold), new SolidBrush(cor[rnd.Next(0, 4)]), new Point(i * 15, 0));
            }

            //画线
            for (int i = 0; i < 20; i++)
            {
                Point p1 = new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height));
                Point p2 = new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height));
                g.DrawLine(new Pen(cor[rnd.Next(0, 4)], 1), p1, p2);
            }
            
            // 加入像素点
            for (int i = 0; i < 200; i++)
            {
                Point p1 = new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height));
                bmp.SetPixel(p1.X, p1.Y, cor[rnd.Next(0, 4)]);
            }
            //将图片镶嵌到图片框中
            pictureBox1.Image = bmp;



        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1_Click( sender,  e);
        }
    }
}
