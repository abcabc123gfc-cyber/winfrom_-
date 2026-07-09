using System.Windows;
using System.Windows.Input;

namespace _01_core_WPF
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 登录按钮点击
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            // 简单验证
            if (string.IsNullOrEmpty(username))
            {
                txtError.Text = "请输入用户名";
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                txtError.Text = "请输入密码";
                txtPassword.Focus();
                return;
            }

            // 模拟登录验证（admin / 123456）
            if (username == "admin" && password == "123456")
            {
                txtError.Text = "";
                MessageBox.Show("登录成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);

                // 打开主窗口
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // 关闭登录窗口
                this.Close();
            }
            else
            {
                txtError.Text = "用户名或密码错误";
                txtPassword.Password = "";
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// 窗口拖动
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
