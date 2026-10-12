//using System.Threading;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace 练习
{
    /// <summary>
    /// MI.xaml 的交互逻辑
    /// </summary>
    public partial class MI : Window
    {
        public MI()
        {
            InitializeComponent();
            //System.Timers.Timer 后台线程 访问UI需要跨线程
            timer.Enabled = true;
            timer.Interval = 2500;
            timer.Elapsed += PollImages;
        }
        int index = 1;
        private void PollImages(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (index > 8)
                {
                    index = 1;
                }

                image1.Source = new BitmapImage(new Uri($"pack://application:,,,/Image/{index}_风景.bmp", UriKind.Absolute));

                index++;

            });

        }

        Timer timer = new Timer();

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //不等于鼠标左键不执行
            if (e.ChangedButton != MouseButton.Left) return;
            image1.Source = new BitmapImage(new Uri($"pack://application:,,,/Image/3_风景.bmp", UriKind.Absolute));



        }
    }
}
