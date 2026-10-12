using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using 手动Prism.Views;

namespace 手动Prism
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        //CreateShell()  创建外壳  创建应用程序启动的窗体
        //之前咱们是通过StartupUri="MainWindow.xaml"实现的
        //CreateShell() 执行的要比 StartupUri 执行的早
        protected override System.Windows.Window CreateShell()
        {
            //
            //耦合度比较高
            //Window win=  new Window1();
            //return win;

            //Container  就是ioc容器
            // 从 DI 容器解析主窗口（自动注入 DataContext）
            //Prism 并不是所有的场景,都会帮助咱们自动关联视图和视图模型,只有写到CreateShell()里面的窗体,才会触发自动关联的机制.没有写到CreateShell里面的窗体,需要手动开始自动关联 xmlns:prism="http://prismlibrary.com/" prism:ViewModelLocator.AutoWireViewModel="True"

            return Container.Resolve<Main>();
        }

        // new 隐藏基类  重写App 的 current属性 目的: 在vm中 通过 App.Current访问公开的对象
        //Prism/MAUI 里的「容器 + 当前 App 实例」封装成一个静态入口，让 ViewModel 不用构造函数注入，也能通过 App.Current.AppContainer.Resolve<T>() 拿到服务。
        public new static App Current => (App)Application.Current;
        public IContainerProvider AppContainer
        {
            get
            {
                return Container;
            }
        }


        //InitializeShell() 初始化的方法,会再CreateShell()执行
        protected override void InitializeShell(Window shell)
        {

            LoginWindow loginWin = Container.Resolve<LoginWindow>();// 创建一个窗口对象

            if (loginWin == null || loginWin.ShowDialog() != true)// 以模态窗口的方式打开这个窗口对象
            {
                // 不需要App的属性ShutdownMode配合
                Application.Current.Shutdown();//退出应用程序
                // 如果没有强行退出的话，打开主窗口
            }

        }

        //RegisterTypes()  注册类型, 启动就是将应用程序使用的对象放到Ioc容器中
        //应用程序需要使用的对象, 一般包括 视图(窗体  用户控件 页面) 服务(数据库操作服务  日志服务 视图服务....)
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //默认情况下Prism会帮助咱们注册,但是需要注意: 不是使用的单例模式注册,会导致全局有多个窗体 服务....
            containerRegistry.RegisterSingleton<LoginWindow>();
        }
    }
}
