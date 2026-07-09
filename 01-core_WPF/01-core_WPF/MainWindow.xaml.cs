using System.Windows;

namespace _01_core_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.Activated += (s, e) =>
            //{
            //    this.Title = "WPF";
            //};
            this.Loaded += MainWindow_Load;

        }
        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            //this.Title = "WPF";
            //Thread thread = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        this.Dispatcher.Invoke(() =>
            //        {
            //            this.btn.Content = DateTime.Now.ToString();
            //        });
            //    }
            //});
            //thread.IsBackground = true;
            //thread.Start();

            
            Check check = new Check();
            check.check(this);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //panel_Zindex zindex = new panel_Zindex();
            //zindex.Show();
            布局控件 zindex = new 布局控件();
            zindex.Show();
            //接受 用户点击的按钮
            MessageBoxResult result= MessageBox.Show("测试","标题栏",MessageBoxButton.OKCancel);
            //判断用户点击的是哪一个按钮
            if (result == MessageBoxResult.OK)
            {
                this.text.Text = "点击了OK";
            }
            else
            {
                this.text.Text = "点击了Cancel";
            }
            double height = this.ActualHeight;
            //MessageBox.Show(height.ToString());
            this.text.Text = height.ToString();
            
        }
    }
}