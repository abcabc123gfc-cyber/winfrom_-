using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleB.ViewModels
{
    internal class ViewBViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        public ViewBViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

        }

        public DelegateCommand<string> OpenCommand
        {
            get
            {
                return new DelegateCommand<string>((page) =>
                {
                    if (page.Equals("ViewC"))
                    {
                        regionManager.RequestNavigate("ContentRegion", page);
                        //regionManager.RegisterViewWithRegion("ContentRegion", page);

                    }
                    else
                    {
                        regionManager.RequestNavigate("ContentRegion", page);
                    }
                });
            }
        }

    }
}
