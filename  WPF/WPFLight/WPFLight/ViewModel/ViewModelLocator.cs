/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPFLight"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Data.Entity;
using WPFLight.Model;
using WPFLight.Service;
using WPFLight.Share;
using WPFLight.ViewModel.UserControls;

namespace WPFLight.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            //注册到 IOC 容器中 默认单例模式
            SimpleIoc.Default.Register<MainViewModel>();

            // ---- 非单例：页面 VM、Service、DbContext ----
            SimpleIoc.Default.Register<UserControlUserInfoViewModel>(false);
            SimpleIoc.Default.Register<UserControlBlogViewModel>(false);
            // 关键：DbContext 不要单例
            //SimpleIoc.Default.Register<SQLHelper>(false);

            // Service 无状态，单例即可；DbContext 由 Func 每次新建
            SimpleIoc.Default.Register<Iservice<UserInfo>, WPFLight.Service.ServiceUserInfo>();
            SimpleIoc.Default.Register<Iservice<BlogInfo>, WPFLight.Service.ServiceBlogInfo>();

            #region 注意 
            //SimpleIoc.Default.Register<SQLHelper>(); 默认是单例。意味着：

            //后果 1：整个应用共享一个 DbContext，缓存无限增长
            //每个实体都留在内存里不释放 → 内存持续上涨，最终 OOM 或明显卡顿。

            //后果 2：DbContext 不是线程安全的 
            //WPF 里如果以后有 async 操作、Task.Run、BackgroundWorker、DispatcherTimer 等并发查库，单例 DbContext 会抛 InvalidOperationException: A second operation was started on this context，或者数据错乱。

            // 坑 2：ServiceBlogInfo 是 internal 修复：改成 public
            //  以后如果把 Service 拆到独立类库（WPFLight.Services.dll），internal 会直接导致 ActivationException，而且报错信息不直观。

            //  SimpleIoc 的泛型注册 Register<Iservice<BlogInfo>, ServiceBlogInfo>() 要求类型可访问，某些 IoC 实现会要求 public。

            #endregion
            #region 解决
            //将非必要界面改为 非单例模式 
            //对EF 框架 由service 注入 自己控制创建
            // 注册工厂
            SimpleIoc.Default.Register<Func<SQLHelper>>(() => () => new SQLHelper());

            //或者
            // 每次 GetInstance 都 new 一个新的 SQLHelper
            //SimpleIoc.Default.Register<SQLHelper>(() => new SQLHelper());
            #endregion
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public UserControlUserInfoViewModel UserInfo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserControlUserInfoViewModel>();
            }
        }
        public UserControlBlogViewModel BlogInfo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserControlBlogViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}