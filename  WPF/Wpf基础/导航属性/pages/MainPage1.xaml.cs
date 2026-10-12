using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 导航属性.pages
{
    /// <summary>
    /// MainPage1.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage1 : Page
    {
        public MainPage1()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //NavigationService 导航服务
            //Navigate() 跳转到指定的页面
            //Refresh() 刷新当前页面
            //GoBack() 返回上一个页面
            //GoForward() 前进到下一个页面
            this.NavigationService.Navigate(new pages.Page1());
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
           
            //pack://application:,,, 当前项目
            this.NavigationService.Navigate(new Uri("pack://application:,,,/Pages/Page1.xaml",UriKind.Absolute));
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("https://www.taobao.com/"));
        }
    }
}
