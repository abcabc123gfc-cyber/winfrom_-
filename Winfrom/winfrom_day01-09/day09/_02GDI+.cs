using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace day09
{
    public partial class _02GDI_ : Form
    {
        public _02GDI_()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //获取绘制对象(画板)
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Red, 5f);

            // 包含直线与曲线图形路径 绘制一个闭合的区域
            GraphicsPath graphicsPath = new GraphicsPath();
            //路径开始
            graphicsPath.StartFigure();

            graphicsPath.AddLine(30, 30, 30, 30);
            graphicsPath.AddLine(30, 30, 100, 130);


            //路径结束
            graphicsPath.CloseFigure();
            g.DrawPath(pen, graphicsPath);


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Red, 5f);

            Point point1 = new Point(30, 30);
            Point point2 = new Point(100, 100);
            //绘制直线
            g.DrawLine(pen, point1, point2);


            //水平直线: 保证y轴坐标不变，x轴坐标递增
            g.DrawLine(pen, new Point(50, 0), new Point(100, 0));


        }
        /// <summary>
        /// 画弧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Red, 5f);

            g.DrawArc(pen, 30, 30, 100, 100, 0, 360);
        }



        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen pen = new Pen(Color.Red, 5f);

            Rectangle rectangle = new Rectangle(30, 30, 100, 100);

            //填充矩形
            //g.FillRectangle(Brushes.Red, rectangle);

            g.DrawArc(pen, rectangle, 0, 30);


        }
        /// <summary>
        /// 画图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            SetQuality(graphics);
            Image image = Image.FromFile(@"D:\Download_Microsoft\3_风景.bmp");

            //panel4.Size = new Size(image.Width, image.Height);

            //graphics.DrawImage(image, image.Width, image.Height);

            graphics.DrawImage(image,0,0, panel4.Width, panel4.Height);
            //graphics.DrawImage(image, 30, 30);


        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.DrawRectangle(new Pen(Color.Red, 7), 30, 30, 50, 50);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawPie(new Pen(Color.Red, 6), 30, 30, 100, 100, 0, 60);
        }

        private void _02GDI__Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawCurve(new Pen(Color.Yellow, 6), new Point[] { new Point(0, 30), new Point(30, 50), new Point(50, 70) });


        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawString("hell",new Font("宋体",14),Brushes.Yellow,40,40);
            //graphics.DrawString("hell", new Font("宋体", 14), Brushes.Yellow, 30, 30);
        }

        private void SetQuality(Graphics g)
        {
            //抗锯齿 让图像变得更加平滑
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //设置图像,呈现高质量,让绘制的图像更加清晰
            g.CompositingQuality = CompositingQuality.HighQuality;
            //设置插补模式为高质量双三插值法,让绘制图像更加清晰
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }
    }
}
