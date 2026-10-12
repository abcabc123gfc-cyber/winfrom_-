using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
// 安装Microsoft.Extensions.DependencyInjection程序集
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace _01_基础使用
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //1.
        //IServiceProvider 服务器提供者接口 主要给ViewModel 提供各种服务(数据库服务区  日志服务....) 通过依赖注入的方式 注入到ViewModel中 方便Viewmodel获取各种服务
        public static IServiceProvider Services { get; private set; }
        public App()
        {
            Services = ConfigureServices();
        }

        private IServiceProvider ConfigureServices()
        {
            //定义一个服务器集合,服务集合编译之后,会生成一个服务器提供者(IServiceProvider),然后这个服务器提供者就可以通过GetService() 方法,获取对应的服务对象
            //注意: 需要安装Microsoft.Extensions.DependencyInjection; 程序包 才能使用ServiceCollection类,这个类是微软提供的一个轻量级的依赖注入容器,可以注册和解析服务
            //2.创建服务器集合
            var services = new ServiceCollection();

            //3.注册服务
            //注册服务 AddSingleton() 方法  注册一个单例服务,这个服务在整个应用程序生命周期内只会创建一个实例,每次请求整个服务都会返回同一个实例,适合用于需要共享状态的服务,比如 数据库上下文  日志记录器 配置管理器....
            services.AddSingleton<MainWindowViewModel>();
            //services.AddSingleton<EFUserModel>();


            //services.AddSingleton<IUser<User, ViewUser>, UserService>();

            //4.构建
            return services.BuildServiceProvider();
        }
    }
}
