namespace _02_Project.Service
{
    /// <summary>
    /// 对话框服务抽象：ViewModel 与 UI 提示解耦（MVVM 模式下不允许 VM 直接弹 MessageBox）。
    /// </summary>
    public interface IDialogService
    {
        /// <summary>普通提示</summary>
        void ShowInfo(string message);

        /// <summary>警告提示</summary>
        void ShowWarning(string message);

        /// <summary>错误提示</summary>
        void ShowError(string message);

        /// <summary>确认框：true=用户点了“是”</summary>
        bool Confirm(string message);
    }
}
