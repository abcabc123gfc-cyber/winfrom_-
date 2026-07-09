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

namespace _01_core_WPF
{
    /// <summary>
    /// panel_Zindex.xaml 的交互逻辑
    /// </summary>
    public partial class panel_Zindex : Window
    {
        public panel_Zindex()
        {
            InitializeComponent();
        }

        private void Grid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox .Show("点击了");
        }
    }
}
