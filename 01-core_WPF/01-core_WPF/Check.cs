using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _01_core_WPF
{
    class Check
    {
        public void check(MainWindow main)
        {
            //main.Dispatcher.Invoke(() =>
            //{
            //    main.btn.Content = DateTime.Now.ToString();
            //});

            Application.Current.Dispatcher.Invoke(() =>
            {
                //Current 获取当前应用程序的实例 来调用Dispatcher
                main.btn.Content = DateTime.Now.ToString();
            });
        }
    }
}
