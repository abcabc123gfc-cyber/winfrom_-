using _02_Project.Service;
using _02_Project.Share;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _02_Project.ViewModel
{
    /// <summary>
    /// 登录窗口 ViewModel
    /// 由 CommunityToolkit.Mvvm 源生成器自动生成：
    ///   属性 -> Account / Password / ShowPassword / ErrorMessage / IsBusy
    ///   命令 -> LoginCommand / GoToRegisterCommand
    /// </summary>
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IWindowService _windowService;

        public LoginViewModel(IAuthService authService, IWindowService windowService)
        {
            _authService = authService;
            _windowService = windowService;
           
        }

        /// <summary>用户名</summary>
        [ObservableProperty]
        private string _account = "A";

        /// <summary>密码（由 PasswordChanged 事件 / 可见 TextBox 双向绑定同步）</summary>
        [ObservableProperty]
        private string _password = "1";
        
        /// <summary>是否明文显示密码</summary>
        [ObservableProperty]
        private bool _showPassword;
        
        /// <summary>错误提示（为空时界面自动隐藏）</summary>
        [ObservableProperty]
        private string _errorMessage = string.Empty;

        /// <summary>是否正在请求（防止重复点击）</summary>
        [ObservableProperty]
        private bool _isBusy;

        /// <summary>登录命令：校验输入 -> 调服务 -> 成功则切主窗口</summary>
        [RelayCommand]
        private async Task LoginAsync()
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

            try
            {
                IsBusy = true;
                var result = await _authService.LoginAsync(Account, Password);
                if (!result.Success)
                {
                    ErrorMessage = result.Message;
                    return;
                }

                // 登录成功：记录当前会话（含用户类型，用于权限判断），打开主窗口，关闭登录窗口
                Session.CurrentUserId = result.UserId;
                Session.CurrentUserName = Account.Trim();
                Session.CurrentUserTypeId = result.UserTypeId;
                Session.CurrentUserTypeName = result.UserTypeName;
                _windowService.OpenMain();
                _windowService.CloseLogin();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"登录出错：{ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>跳转注册：打开注册窗口，关闭登录窗口</summary>
        [RelayCommand]
        private void GoToRegister()
        {
            _windowService.OpenRegister();
            _windowService.CloseLogin();
        }
    }
}
