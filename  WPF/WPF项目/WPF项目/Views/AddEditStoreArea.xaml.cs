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
using WPF项目.ViewModels;

namespace WPF项目.Views
{
    /// <summary>
    /// AddEditStoreArea.xaml 的交互逻辑
    /// </summary>
    public partial class AddEditStoreArea : Window
    {
        public AddEditStoreArea(int _edit)
        {
            InitializeComponent();
            //构造函数传参，判断是添加还是修改
            this.DataContext = new AddEditStoreAreaViewModel(this, _edit);
        }
    }
}
