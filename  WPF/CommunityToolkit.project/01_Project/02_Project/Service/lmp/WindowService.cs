using _02_Project.View;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace _02_Project.Service.lmp
{
    /// <summary>
    /// 窗口导航服务实现：从 DI 容器解析窗口并显示 / 关闭
    /// </summary>
    public class WindowService : IWindowService
    {
        public void OpenLogin()
        {
            App.ServiceProvider.GetRequiredService<Login>().Show();
        }

        public void OpenRegister()
        {
            App.ServiceProvider.GetRequiredService<Register>().Show();
        }

        public void OpenMain()
        {
            App.ServiceProvider.GetRequiredService<Main>().Show();
        }

        public void CloseLogin()
        {
            System.Windows.Application.Current.Windows.OfType<Login>().FirstOrDefault()?.Close();
        }

        public void CloseRegister()
        {
            System.Windows.Application.Current.Windows.OfType<Register>().FirstOrDefault()?.Close();
        }
    }
}
