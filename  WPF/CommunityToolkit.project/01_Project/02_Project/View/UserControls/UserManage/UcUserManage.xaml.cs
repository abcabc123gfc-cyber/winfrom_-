using _02_Project.ViewModel.UserControls.UserManage;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _02_Project.View.UserControls.UserManage
{
    /// <summary>
    /// 用户管理页（MVVM 模式）：DataContext 为 UcUserManageViewModel（依赖注入）。
    /// PasswordBox 不支持绑定，本文件通过 PasswordChanged 事件把密码桥接到 ViewModel 的
    /// PasswordInput 属性（仅此一个 UI 通道例外，业务校验仍在 ViewModel）；其余交互全部走命令绑定。
    /// </summary>
    public partial class UcUserManage : UserControl
    {
        public UcUserManage(UcUserManageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // ViewModel 主动清空密码（新增/修改/取消时）→ 同步清空 PasswordBox
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(UcUserManageViewModel.PasswordInput)
                    && s is UcUserManageViewModel vm
                    && !string.Equals(PwdBox.Password, vm.PasswordInput, StringComparison.Ordinal))
                    PwdBox.Password = vm.PasswordInput;
            };
        }

        /// <summary>密码输入 → 桥接到 ViewModel（业务校验在 Save 时由 ViewModel 完成）</summary>
        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UcUserManageViewModel vm)
                vm.PasswordInput = PwdBox.Password;
        }

        /// <summary>查询框只允许输入数字（纯 UI 约束）</summary>
        private void QueryIdBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
