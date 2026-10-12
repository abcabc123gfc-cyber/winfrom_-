using _02_Project.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _02_Project.ViewModel
{
    /// <summary>
    /// 注册窗口 ViewModel
    /// 由 CommunityToolkit.Mvvm 源生成器自动生成：
    ///   属性 -> Account / Password / ConfirmPassword / ErrorMessage / IsBusy
    ///   命令 -> RegisterCommand / BackToLoginCommand
    /// </summary>
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IWindowService _windowService;

        public RegisterViewModel(IAuthService authService, IWindowService windowService)
        {
            _authService = authService;
            _windowService = windowService;
        }

        /// <summary>用户名</summary>
        [ObservableProperty]
        private string _account = string.Empty;

        /// <summary>密码</summary>
        [ObservableProperty]
        private string _password = string.Empty;

        /// <summary>确认密码</summary>
        [ObservableProperty]
        private string _confirmPassword = string.Empty;

        /// <summary>错误提示</summary>
        [ObservableProperty]
        private string _errorMessage = string.Empty;

        /// <summary>是否正在请求</summary>
        [ObservableProperty]
        private bool _isBusy;

        /// <summary>注册命令：本地校验 -> 调服务（查重）-> 成功则回登录窗口</summary>
        [RelayCommand]
        private async Task RegisterAsync()
        {
            ErrorMessage = string.Empty;

            // 基本输入校验
            if (string.IsNullOrWhiteSpace(Account))
            {
                ErrorMessage = "用户名不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "密码不能为空";
                return;
            }
            if (!string.Equals(Password, ConfirmPassword, StringComparison.Ordinal))
            {
                ErrorMessage = "两次输入的密码不一致";
                return;
            }

            try
            {
                IsBusy = true;
                var result = await _authService.RegisterAsync(Account, Password);
                if (!result.Success)
                {
                    ErrorMessage = result.Message;
                    return;
                }

                // 注册成功：回到登录窗口
                _windowService.OpenLogin();
                _windowService.CloseRegister();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"注册出错：{ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>返回登录：关闭注册窗口，打开登录窗口</summary>
        [RelayCommand]
        private void BackToLogin()
        {
            _windowService.OpenLogin();
            _windowService.CloseRegister();
        }
    }
}
