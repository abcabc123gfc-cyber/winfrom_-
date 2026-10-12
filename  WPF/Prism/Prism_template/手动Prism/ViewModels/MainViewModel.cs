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
    internal class MainViewModel: BindableBase
    {
        //命令的类型
        //public DelegateCommand ClickCommand { get; }
        //public DelegateCommand<string> ParamCommand { get; } // 带参数命令
        //public DelegateCommand AsyncCommand { get; } // 异步命令
        public DelegateCommand OpenWindowCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    // MessageBox.Show("打开window1");
                    //Window win1 = new Window1();
                    //win1.ShowDialog();
                    //注意:引入Prism.Ioc;
                    App.Current.AppContainer.Resolve<Window1>().ShowDialog();
                });
            }

        }
    }
}
