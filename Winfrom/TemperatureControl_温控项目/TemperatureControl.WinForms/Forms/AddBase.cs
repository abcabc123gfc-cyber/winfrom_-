using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemperatureControl.WinForms.Controls;

namespace TemperatureControl.WinForms.Froms
{
    public partial class AddBase : Form
    {
        bool isMax = false;
     
        public AddBase()
        {
            InitializeComponent();
        }

        private void AddBase_Load(object sender, EventArgs e)
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
               

                // 用 GraphicsPath 描述"要填充的形状"，这里是一个普通矩形
                var path = new GraphicsPath();
                path.AddRectangle(rect);
                // 线性渐变画刷
                // 参数1 rect：渐变作用的区域
                // 参数2 FColor：起始颜色
                // 参数3 TColor：终止颜色
                // 参数4 LinearGradientMode.Vertical：渐变方向，Vertical 表示从上到下
                var linearGradientBrush = new LinearGradientBrush(rect, FColor, TColor, LinearGradientMode.Vertical);
                
                //参数: 填充区域 、 填充
                g.FillPath(linearGradientBrush, path);

            };
            #endregion


          
          
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
            this.Close();
            this.Dispose();
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

        private void AddBase_Resize(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 0);
            panel1.Width = this.Width;
            panel1.Height = 100;
            #region 初始化 按钮位置
            button1.Location = new Point(panel1.Width - button1.Width - 10, panel1.Height / 3);
            button2.Location = new Point(panel1.Width - button2.Width - 90, panel1.Height / 3);
            button3.Location = new Point(panel1.Width - button3.Width - 170, panel1.Height / 3);
            #endregion
            panel2.Location = new Point((this.ClientSize.Width - panel2.Width) / 2, (this.ClientSize.Height - panel2.Height) / 2);
            panel2.BackColor = Color.Transparent;
            //panel2.BackColor = Color.Red;
        }
    }
}
