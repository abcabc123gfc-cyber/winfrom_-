using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RegionAndNavigation.ViewModels
{
    //ViewA 要接收 MainWindow 传递过来的参数  如何接收?
    //要INavigationAware接口
    //INavigationAware 接口中 包含导航的生命周期(一系列的时间 比如 跳转的时候 从哪里来 到那里去) 导航的上下文(navigationContext)

    //生命周期: 在跳转的过程中 执行的一系列方法 执行这些方法 有先后的顺序
    internal class ViewAViewModel : BindableBase, INavigationAware
    {
        private string name = "11";
        public string  Name { get => name; set => SetProperty(ref name, value); }
        //IsNavigationTarget 在第一次节能如页面的时候不会执行,第二次进入之后才会执行
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Console.WriteLine("IsNavigationTarget");
            return true;//返回true和false 达到的效果是一样的,只不过性能不一样,如果返回true不会重新创建视图实例,如果返回false,就会重新创建视图实例
        }

        //OnNavigatedFrom以当前vm为参照物 离开ViewA的时候执行
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Console.WriteLine("OnNavigatedFrom");
        }

        //OnNavigatedTo 以当前vm为参照物 进入ViewA的时候执行
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //获取参数 
            Name = navigationContext.Parameters.GetValue<string>("name");
            

            //重定向:从MainWindow跳转到ViewA的时候,不在ViewA停留,直接跳转到ViewB
            //navigationContext.NavigationService.RequestNavigate("ViewB");

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

       
    }
}
