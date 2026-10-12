using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Prism_template.ViewModels.Regions
{
    class LeftMenuViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        public LeftMenuViewModel(IRegionManager regionManager)
        {

            this.regionManager = regionManager;


        }
        private int age;

        public int Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }

        public DelegateCommand<string> OpenCommand
        {
            get
            {
                return new DelegateCommand<string>(page =>
                {
                    //regionManager 获取区域管理器
                    //RegisterViewWithRegion 向某个区域放置对应的视图

                    //regionManager.RegisterViewWithRegion<Page1>("ContentRegion");
                    //regionManager.RegisterViewWithRegion("ContentRegion",typeof(Page1));
                    //regionManager.RegisterViewWithRegion("ContentRegion",page
                    //    );


                    // regionManager.Regions  获取一个区域集合  可以通过单个区域的名字,从区域集合中获取某个区域
                    //RequestNavigate 请求导航(跳转)
                    //导航的前提  必须把需要跳转的页面 注册成可以导航的页面

                    regionManager.Regions["ContentRegion"].RequestNavigate(page);
                });
            }
        }

    }
}
