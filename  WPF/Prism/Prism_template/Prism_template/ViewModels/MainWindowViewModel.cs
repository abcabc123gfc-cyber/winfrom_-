using Prism.Mvvm;
using Prism.Regions;
using Prism_template.Views.Regions;

namespace Prism_template.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        //IRegionManager 属于框架的核心内置服务，它会在应用程序启动时由 Bootstrapper（或 Prism 的初始化流程）自动注册到依赖注入容器中，并且其生命周期是单例（Singleton）。
        private readonly IRegionManager regionManager;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            //regionManager 区域管理器,负责管理区域 管理如何像此区域添加页面,如何移除页面,如果跳转 如何像页面传参

            //regionManager.RegisterViewWithRegion<TopHeader>("HeaderRegion"); 确实是为了把 TopHeader 这个视图“添加”到 HeaderRegion 区域中，但Prism将这种机制称为“视图发现”（View Discovery）
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion<TopHeader>("HeaderRegion");
            regionManager.RegisterViewWithRegion<LeftMenu>("LeftRegion");
        }
      

    }
}
