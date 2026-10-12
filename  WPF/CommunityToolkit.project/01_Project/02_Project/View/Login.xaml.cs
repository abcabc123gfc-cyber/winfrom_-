using _02_Project.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace _02_Project.View
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        private readonly LoginViewModel _viewModel;

        public Login(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            _viewModel = loginViewModel;
            this.DataContext = loginViewModel;
        }

        /// <summary>
        /// PasswordBox 不支持直接绑定，这里在密码变化时同步到 ViewModel.Password。
        /// 明文 TextBox 已通过 TwoWay 绑定自动同步。
        /// </summary>
        private void PwdMasked_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
                _viewModel.Password = pb.Password;
        }
    }
}
