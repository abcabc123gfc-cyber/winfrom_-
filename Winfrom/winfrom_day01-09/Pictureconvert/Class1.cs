using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pictureconvert
{
    class ImageConverter
    {
        public static void ConvertJpegToBmp(string jpegPath, string bmpPath)
        {
            // 1. 用 WIC 解码 JPEG（自动处理 CMYK）
            JpegBitmapDecoder decoder;
            using (FileStream stream = new FileStream(jpegPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                decoder = new JpegBitmapDecoder(stream,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.OnLoad);
            }

            var frame = decoder.Frames[0];

            // 2. CMYK → RGB
            if (frame.Format == System.Windows.Media.PixelFormats.Cmyk32)
            {
                frame = new FormatConvertedBitmap(frame,
                    System.Windows.Media.PixelFormats.Bgr32, null, 0);
            }
            else if (frame.Format != System.Windows.Media.PixelFormats.Bgr32 &&
                     frame.Format != System.Windows.Media.PixelFormats.Bgra32)
            {
                frame = new FormatConvertedBitmap(frame,
                    System.Windows.Media.PixelFormats.Bgr32, null, 0);
            }

            // 3. 转为 GDI+ Bitmap 并保存为 BMP
            using (var bmp = new Bitmap(frame.PixelWidth, frame.PixelHeight, PixelFormat.Format32bppArgb))
            {
                var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.WriteOnly, bmp.PixelFormat);
                frame.CopyPixels(System.Windows.Int32Rect.Empty,
                    data.Scan0, data.Height * data.Stride, data.Stride);
                bmp.UnlockBits(data);

                bmp.Save(bmpPath, ImageFormat.Bmp);
            }
        }
    }
}