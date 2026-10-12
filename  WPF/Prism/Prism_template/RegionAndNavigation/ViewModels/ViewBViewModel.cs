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
    internal class ViewBViewModel : BindableBase, IConfirmNavigationRequest
    {
        //导航确认(导航守卫 导航拦截) 离开当前页面的时候执行
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (navigationContext.Uri.ToString() == "ViewA")
            {
                MessageBoxResult mbr = MessageBox.Show("是否允许进入ViewA页面", "询问", MessageBoxButton.YesNo);

                //if (mbr== MessageBoxResult.OK)
                //{
                //    continuationCallback(true);// 传入true 能离开 传入false不能离开
                //}
                continuationCallback(mbr == MessageBoxResult.Yes);

            }
            else
            {
                continuationCallback(true);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
