using BarcodeStandard;
using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace day09.练习
{
    public partial class 条形码 : Form
    {
        public 条形码()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //创建 Barcode 实例
                string str = textBox1.Text;
                var barcode = new Barcode();
                // 2. (可选) 设置在条码下方显示数据文本
                barcode.IncludeLabel = true;

                //生成条码图像
                //barcode.Encode 将文本数据转为条形码数据
                //参数:
                //条码说明,生成条码的数据,前景色条码颜色,背景颜色
                //条码长度 ,条码高度
                SKImage sKImage = barcode.Encode(BarcodeStandard.Type.Code128, str, SKColors.Blue, SKColors.White, 300, 100);
                // sKImage.Encode() 无参数  默认将图像编码为 PNG 格式 
                //SKData 对象 表明: 编码后的图像数据
                using (SKData date = sKImage.Encode())
                //SKData 对象（date）中的二进制数据复制到一个新的 MemoryStream 中 ate.ToArray() 
                // new MemoryStream 在内存中开辟一块缓冲区，把这些字节放进去
                //MemoryStream stream 不使用BufferedStream原因:
                //内存中的数据 Read 和 Write 操作本身没有 I/O 延迟，添加缓冲层只会增加额外的方法调用和内存复制。
                //byte[] bytes = data.ToArray();
                using (MemoryStream stream = new MemoryStream(date.ToArray()))
                {
                    //刚写入数据后，Position 指向末尾。

                    //如果此时直接调用 Image.FromStream，它可能会读不到数据（因为指针已在末尾）
                    //stream.Position = 0;  // 重置到开头
                    pictureBox1.Image = Image.FromStream(stream);


                    //保存
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string safeFileName = $"{DateTime.Now:yyyyy-MM-dd_HH-mm-ss}_条码.png";
                    string fullPath = Path.Combine(desktopPath, safeFileName);
                    Image image = Image.FromStream(stream);
                    image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("1");
            }
        }
    }
}
