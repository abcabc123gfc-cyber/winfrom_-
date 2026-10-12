using Prism.Commands;
using Prism.Mvvm;
using Prism_Dialog.Views;

namespace Prism_Dialog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
        public DelegateCommand<string> OpenCommand
        {
            get
            {
                return new DelegateCommand<string>(win =>
                {
                    if (win == "Window1")
                    {
                        new Window1().Show();

                    }
                    else
                    {
                        new Window2().Show();
                    }

                });
            }
        }
    }
}
