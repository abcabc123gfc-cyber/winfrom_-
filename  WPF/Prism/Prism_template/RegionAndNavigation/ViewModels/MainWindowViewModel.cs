using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace RegionAndNavigation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //IRegionManager 是 Prism 核心内置服务，由 Prism 启动流程自动注册到 DI 容器，通常是单例。 管理区域、注册视图、发起导航
        private readonly IRegionManager regionManager;
        //IRegionNavigationJournal 不是 Prism 默认让你直接构造函数注入的全局服务。它是某个区域导航服务对应的导航日志，用来实现“后退 / 前进”。
        private IRegionNavigationJournal journal;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public DelegateCommand<string> OpenCommand
        {
            get
            {
                return new DelegateCommand<string>((page) =>
                {
                    // MessageBox.Show(page);

                    //如何跳转? 跳转的功能在区域管理器,需要先拿到区域管理器
                    //如何拿到区域管理器:通过构造函数注入的方式(Prism帮助咱们注入的)
                    //regionManager.Regions 获取到所有的区域
                    //regionManager.Regions["区域名称"] 获取一个区域

                    //IRegion region=   regionManager.Regions["ContentRegion"];
                    //region.RequestNavigate(page);//直接跳转


                    NavigationParameters keyValuePairs = new NavigationParameters()
                    {
                        {"name","吴亦凡" }
                    };
                    
                    regionManager.RequestNavigate("ContentRegion", page, par =>
                    {
                        //Console.WriteLine(par);
                        // par.Error 错误信息
                        //参数3回调函数的参数,就是跳转成功之后的结果
                        //par.Context.NavigationService 导航服务
                        //par.Context.NavigationService.Journal 导航日志
                        //par.Context.Parameters 导航参数
                        //par.Context.Uri 页面信息
                        if (par.Result == true)
                        {
                            //page 对应的视图类型必须已经在容器里能解析出来，否则这里直接失败，par.Result 就是 false。
                            journal = par.Context.NavigationService.Journal;
                        }
                        //参数4 传递的参数
                    },keyValuePairs);


                });
            }
        }

        //使用button 文本作为页面名称跳转
        //public DelegateCommand<object> OpenCommand1
        //{
        //    get
        //    {
        //        return new DelegateCommand<object>(obj =>
        //        {

        //            Button btn= obj as Button;
        //            string  page= btn.Content.ToString().Trim();
        //            MessageBox.Show(page);
        //            IRegion region = regionManager.Regions["ContentRegion"];
        //            region.RequestNavigate(page);


        //        });
        //    }
        //}

        public DelegateCommand BackCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (journal != null && journal.CanGoBack)
                    {
                        journal.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("最后一步了,不能后退了");
                    }
                });
            }
        }
        public   DelegateCommand ForwardCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                  
                    if (journal != null && journal.CanGoForward)
                    {
                        journal.GoForward();
                    }
                    else
                    {
                        MessageBox.Show("已经是第一步了,不能再前进了");
                    }
                });
            }
        }
        public DelegateCommand ClearCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (journal != null)
                    {
                        journal.Clear();
                    }
                    else
                    {
                      
                    }
                });
            }
        }

        /*
        * 跳转的生命周期执行顺序:
        * 进入的时候
        * 第一次进入ViewA的时候 先执行OnNavigatedTo
        * 第二次进入的时候 先执行IsNavigationTarget 再执行OnNavigatedTo
        * 
        * 离开的时候
        * 从ViewA离开的时候 只执行OnNavigatedFrom
        * 
        *思考:
        *在哪里接收传递过来的参数? OnNavigatedTo
        */

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

    }
}
