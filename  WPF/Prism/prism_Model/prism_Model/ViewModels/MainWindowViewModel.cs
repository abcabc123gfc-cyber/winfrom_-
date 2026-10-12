using ModuleB.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace prism_Model.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }
        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _regionManager.RegisterViewWithRegion("HeaderRegion", typeof(ModuleB.Views.ViewA));
                    //_regionManager.RegisterViewWithRegion("LeftRegion", typeof(ModuleB.Views.ViewB));
                    _regionManager.RegisterViewWithRegion<ViewB>("LeftRegion");
                });
            }
        }
    }
}
