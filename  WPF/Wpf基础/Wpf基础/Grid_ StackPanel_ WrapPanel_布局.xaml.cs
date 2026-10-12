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
using System.Windows.Shapes;

namespace Wpf基础
{
    /// <summary>
    /// Grid__StackPanel__WrapPanel_布局.xaml 的交互逻辑
    /// </summary>
    public partial class Grid__StackPanel__WrapPanel_布局 : Window
    {
        public Grid__StackPanel__WrapPanel_布局()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //改变按钮 填充颜色的两种方式
            //ellipes.Fill = new SolidColorBrush(Colors.Green);

            ellipes.Fill = new BrushConverter().ConvertFromString("#00FF00") as Brush;
            //MessageBox.Show("点击了按钮");
        }
    }
}
