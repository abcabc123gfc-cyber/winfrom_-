using System.Windows;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 对话框服务实现：基于 WPF MessageBox（与 UI 框架解耦，便于替换/测试）。
    /// </summary>
    public class DialogService : IDialogService
    {
        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool Confirm(string message)
        {
            return MessageBox.Show(message, "确认", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes;
        }
    }
}
