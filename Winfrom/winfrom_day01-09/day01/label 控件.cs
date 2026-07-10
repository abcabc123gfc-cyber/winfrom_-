using System.Drawing;
using System.Windows.Forms;

namespace day01
{
    public partial class label_控件 : Form
    {
        public label_控件()
        {
            InitializeComponent();
        }

        public void ShowLable()
        {
            //Label 类
            Label label = new Label();
            //唯一标识
            label.Name = "label1";
            //设置显示文本
            label.Text = "这是一个标签";
            //设置位置 控件左上角为 (0,0) 以像素为单位 类型为point 
            label.Location = new Point(10, 10);
            //AtutoSize 默认值为 false 超出内容为不显示
            label.AutoSize = true;

            //设置颜色: 
            //1. Color 是一个结构, 其中包括各种颜色的英文别名, red 红色, green绿色
            label.BackColor = Color.White;

            //2. 颜色值
            //计算机三原色
            //Color.FromArgb(0-255, 0-255, 0-255)
            //r :red 红色 g: green 绿色 b: blue 蓝色

            //3. 颜色值 16进制
            //E5 红色系 3D 绿色系 3O 蓝色系
            //设置控件字体颜色
            label.ForeColor = ColorTranslator.FromHtml("#E53D3O");

            //设置字体样式和大小
            label.Font = new Font("微软雅黑", 12, FontStyle.Bold);

            //在创建字符串中, \ 表示转义字符 添加路径时 需要写成 \\
            //或者 使用 @符号:  @"C:\Users\Administrator\Desktop"
            //写的代码会被编译为字节码, 字节码会保存在 .exe 文件中
            label.Text = @"C:\Users\Administrator\Desktop";

            //资源文件
            //Properties.Resources.资源名称;
            //label.Image = Properties.Resources.屏幕截图_2026_06_07_160624;
           

            //对齐方式
            {
                //自定义枚举
                /*
                 TopLeft = 1,
                TopCenter = 2,
                TopRight = 4,
                MiddleLeft = 0x10,
                MiddleCenter = 0x20,
                MiddleRight = 0x40,
                BottomLeft = 0x100,
                BottomCenter = 0x200,
                BottomRight = 0x400
                 

                 Top 上
                Bottom 下
                Left 左
                Right 右
                Middle 垂直居中
                Center 水平居中
                 * 
                 */
            }

            label.TextAlign = ContentAlignment.MiddleCenter;
            label.ImageAlign = ContentAlignment.MiddleCenter;
            //设置控件大小,在AutoSize属性为false的时候才会生效
            label.Size = new Size(200, 200);
            //this From1实例,可以省略的
            //Control  控件 Controls控件集合, 也是form的一个属性,页面上所有的控件都在此集合中
            //吧new出来的控件,添加到此集合中,才能在页面上显示
            this.Controls.Add(label);
        }
    }
}
