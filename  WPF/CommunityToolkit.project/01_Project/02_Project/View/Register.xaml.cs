using _02_Project.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace _02_Project.View
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        private readonly RegisterViewModel _viewModel;

        public Register(RegisterViewModel registerViewModel)
        {
            InitializeComponent();
            _viewModel = registerViewModel;
            this.DataContext = registerViewModel;
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
                _viewModel.Password = pb.Password;
        }

        private void ConfirmPwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
                _viewModel.ConfirmPassword = pb.Password;
        }
    }
}
