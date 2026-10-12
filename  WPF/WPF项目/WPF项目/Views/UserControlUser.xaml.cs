using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPF项目.ViewModels;

namespace WPF项目.Views
{
    /// <summary>
    /// UserControlUser.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlUser : UserControl
    {
        public UserControlUser()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DataContext = new UserControlUserViewModel();
            }
        }
    }
}
