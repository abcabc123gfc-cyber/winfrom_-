using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// AddEditStore.xaml 的交互逻辑
    /// </summary>
    public partial class AddEditStore : Window
    {
        public AddEditStore(int _edit)
        {
            InitializeComponent();
            this.DataContext= new AddEditStoreViewModel(this,_edit);
        }
    }
}
