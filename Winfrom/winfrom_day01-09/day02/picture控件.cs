using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day02
{
    public partial class picture控件 : Form
    {
        public picture控件()
        {
            InitializeComponent();
        }
        public void picture控件_Load()
        {
           //pictureBox1.Image = Image.FromFile("D:\\picture\\1.jpg");
           pictureBox1.Location=new Point(100,100);
            //使用 vs 的资源管理器添加图片
            //pic.ImageLocation = @"F:\C#软件开发14班\2026-07-21\Image\0012.PNG";

            //显示图片的格式 改变图片框的大小以适应图片
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
