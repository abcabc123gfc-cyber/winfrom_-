using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism_template.Models;
using Prism_template.Views;
using System.Windows;

namespace Prism_template.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        //这样的方式获取App.xaml.cs中的App
        private readonly App app= Application.Current as App;

        public User user = new User() { Account = "1", Password = "1" };
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }
        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (User.Account == "1" && User.Password == "1")
                    {
                        //Login login = App.Current.Container.Resolve<Login>();
                        var login = app.Container.Resolve<Login>();
                        login.DialogResult = true;
                    }
                });
            }
        }

    }
}
