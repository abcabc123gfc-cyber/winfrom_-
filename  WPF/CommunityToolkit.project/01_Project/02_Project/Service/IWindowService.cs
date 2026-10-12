namespace _02_Project.Service
{
    /// <summary>
    /// 窗口导航服务接口：解耦 ViewModel 与具体 Window 类型
    /// 实现类位于 Service/lmp/WindowService.cs
    /// </summary>
    public interface IWindowService
    {
        /// <summary>打开登录窗口（新实例）</summary>
        void OpenLogin();

        /// <summary>打开注册窗口（新实例）</summary>
        void OpenRegister();

        /// <summary>打开主窗口</summary>
        void OpenMain();

        /// <summary>关闭当前已打开的登录窗口</summary>
        void CloseLogin();

        /// <summary>关闭当前已打开的注册窗口</summary>
        void CloseRegister();
    }
}
