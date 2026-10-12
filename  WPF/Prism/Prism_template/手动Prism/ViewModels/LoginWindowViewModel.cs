using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 手动Prism.Views;

namespace 手动Prism.ViewModels
{
    internal class LoginWindowViewModel: BindableBase
    {

        private string title = "吴亦凡";
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                SetProperty(ref title, value);

            }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    //判断账号密码是否正确

                    //账号密码正确,给Login.DialogResult 赋值为true

                    //获取容器  再从容器中获取Login
                    MessageBox.Show("登录成功");
                    //App.Current 获取当前应用程序
                  var login=  App.Current.AppContainer.Resolve<LoginWindow>();
                    login.DialogResult = true;
                });
            }
        }
    }
}
