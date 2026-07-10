using System.Windows.Forms;

namespace day02   
{
    public partial class listImage控件 : Form
    {
        public listImage控件()
        {
            InitializeComponent();
            ListBox_Load();
        }
        //imageList1 添加图片

        public void ListBox_Load()
        {
            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources._1_风景);
            //设置图片大小 imagelist 控件
           imageList.ImageSize = new System.Drawing.Size(200, 200);

            pictureBox1.Image=imageList1.Images[0];
            imageList1.Images.Add(imageList.Images[0]);
            //经过imagelist1添加图片 过的图片变成位图,且大小发生改变
            //大小变成 16*16 默认
            //可以通过修改属性来改变大小
        }
    }
}
