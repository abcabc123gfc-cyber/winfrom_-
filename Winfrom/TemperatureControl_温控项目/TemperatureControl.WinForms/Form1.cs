using Models;
using SqlSugar;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;
using TemperatureControl.WinForms.Model;

namespace TemperatureControl.WinForms
{
    public partial class Form1 : Form
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        bool isMax = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            #region 窗体最大化、最小化、关闭按钮
            button2.Image = Properties.Resources.max2;
            button3.Image = Properties.Resources.min2;
            button1.Image = Properties.Resources.close2;
            #endregion


            #region 窗体绘制渐变色 
            // 给 Panel（或任意 Control）挂上 Paint 事件：每次重绘时执行
            this.Paint += (ss, ee) =>
            {
                // ee.Graphics：GDI+ 绘画对象，所有绘制操作都通过它完成
                Graphics g = ee.Graphics;
                // 渐变的起始颜色（顶部）
                Color FColor = ColorTranslator.FromHtml("#9184EE");
                // 渐变的终止颜色（底部）
                Color TColor = ColorTranslator.FromHtml("#94F591");
                // 绘制区域：整个控件的矩形范围（从左上角 0,0 到右下角 Width,Height）
                var rect = new Rectangle(0, 0, this.Width, this.Height);
                //uipanel4
                uiPanel4.BackColor = ColorTranslator.FromHtml("#9184EE");

                // 用 GraphicsPath 描述"要填充的形状"，这里是一个普通矩形
                var path = new GraphicsPath();
                path.AddRectangle(rect);
                // 线性渐变画刷
                // 参数1 rect：渐变作用的区域
                // 参数2 FColor：起始颜色
                // 参数3 TColor：终止颜色
                // 参数4 LinearGradientMode.Vertical：渐变方向，Vertical 表示从上到下
                var linearGradientBrush = new LinearGradientBrush(rect, FColor, TColor, LinearGradientMode.Vertical);
                var linearGradientBrushpanel = new LinearGradientBrush(uiPanel4.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical);
                //参数: 填充区域 、 填充
                g.FillPath(linearGradientBrush, path);

            };
            #endregion


            #region 使用功能sugar 生成实体表
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\dbstore";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                sqlSugar.DbFirst.IsCreateAttribute().CreateClassFile(filePath);
            }

            #endregion
            this.Paint += (ss, ee) =>
            {
                #region 初始化 panel 边框
                uiPanel1.BorderColor = ColorTranslator.FromHtml("#9184EE");
                uiPanel1.BorderWidth = 7;
                uiPanel1.Radius = 15;
                panel1.Width = this.Width;


                uiPanel2.BorderWidth = 3;
                uiPanel3.BorderWidth = 3;
                uiPanel4.BorderWidth = 3;

                uiPanel4.FillColor = ColorTranslator.FromHtml("#9184EE");
                uiPanel4.BorderColor = ColorTranslator.FromHtml("#9184EE");
                uiPanel4.Radius = 10;



                #endregion
                #region 控制 图片框 输入框 标签 pane 位置

                var parent = pictureBox2.Parent;
                int x = (parent.ClientSize.Width - pictureBox2.Width) / 2;
                //int y = (parent.ClientSize.Height - pictureBox2.Height) / 2;
                pictureBox2.Location = new Point(Math.Max(0, x), Math.Max(0, 50));

                label2.Location = new Point(Math.Max(0, x - label2.Width / 2 + 20), Math.Max(0, 130));
                label3.Location = new Point(20, Math.Max(0, label3.Parent.Height / 2 - label3.Height / 2));
                label4.Location = new Point(20, Math.Max(0, label4.Parent.Height / 2 - label4.Height / 2));

                //var pointUIp2= uiPanel2.PointToScreen(uiPanel2.Location);
                //var pointUIP3 = uiPanel3.PointToScreen(uiPanel3.Location);
                //uiPanel2.Location = new Point(uiPanel1.Width/2, pointUIp2.Y);
                #endregion
                #region 初始化 按钮位置
                button1.Location = new Point(panel1.Width - button1.Width - 10, panel1.Height / 3);
                button2.Location = new Point(panel1.Width - button2.Width - 90, panel1.Height / 3);
                button3.Location = new Point(panel1.Width - button3.Width - 170, panel1.Height / 3);
                #endregion
                #region 初始化uipanel 1 位置
                uiPanel1.Location = new Point((this.ClientSize.Width - uiPanel1.Width) / 2, (this.ClientSize.Height - uiPanel1.Height) / 2);
                //uiPanel1.Anchor = AnchorStyles.None;

                #endregion
            };

        }


        #region 最大化 、 最小化 、 关闭 按钮
        /// <summary>
        /// 关闭 进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Image = Properties.Resources.close;
        }
        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.Image = Properties.Resources.max;
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            button3.Image = Properties.Resources.min;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Image = Properties.Resources.close2;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = Properties.Resources.max2;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Image = Properties.Resources.min2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isMax)
            {
                this.WindowState = FormWindowState.Normal;
                //强制重绘窗体，解决最大化后窗体不刷新问题
                this.Refresh();
                isMax = false;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.Refresh();
                isMax = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region 登录事件

        private void uiPanel4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入用户名密码");
                return;
            }

            VUser vu = sqlSugar.Queryable<VUser>().Where(it => it.Account == textBox1.Text && it.Password == textBox2.Text).Single();
            if (vu == null)
            {
                MessageBox.Show("用户名密码错误");
                return;
            }
            UserINfoState.UserInfo = vu;
            new MainFrm().Show();
            this.Hide();
            //sqlSugar.Dispose();
        }



        #endregion

        private void uiPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
