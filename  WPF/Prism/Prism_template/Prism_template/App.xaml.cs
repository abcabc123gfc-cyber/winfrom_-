using Prism.Ioc;
using Prism_template.Views;
using Prism_template.Views.pages;
using System.Windows;

namespace Prism_template
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        //protected override void InitializeShell(Window shell)
        //{
        //    Login loginWin = Container.Resolve<Login>();// 创建一个窗口对象

        //    if (loginWin == null || loginWin.ShowDialog() != true)// 以模态窗口的方式打开这个窗口对象
        //    {
        //        // 不需要App的属性ShutdownMode配合
        //        Application.Current.Shutdown();//退出应用程序
        //        // 如果没有强行退出的话，打开主窗口
        //    }
        //}

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Login>();
            //使用Prism框架 类的后缀是ViewModel 不用手动注册, 容器默认注册方式为瞬时模式
            containerRegistry.RegisterForNavigation<Page1>();
            containerRegistry.RegisterForNavigation<Page2>();
            //把视图和视图模型一并注册
            //containerRegistry.RegisterForNavigation<Page2, PViewModel>();

        }
    }
}
