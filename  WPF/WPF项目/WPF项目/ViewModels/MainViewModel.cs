using System;
using System.Windows.Controls;
using WPF项目.Common;
using WPF项目.Views;

namespace WPF项目.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region 绑定的用户控件
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }
        #endregion
        private string account;
        public string Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;

                OnPropertyChanged();
            }
        }
        public RelayCommand SelectCmd
        {
            get
            {
                return new RelayCommand((o) =>
             {
                 string str = o.ToString();
                 if (str.Equals("打开用户窗体"))
                 {
                     OpenUserFrm();
                 }
                 else if (str.Equals("关闭用户窗体"))
                 {
                     CloseUserFrm();
                 }
             });
            }
        }

        private void CloseUserFrm()
        {
            if (CurrentView != null)
            {
                CurrentView = null;
            }
        }

        private void OpenUserFrm()
        {
            UserControlUser userControlUser = new UserControlUser();
            CurrentView = userControlUser;
        }
        public RelayCommand Store
        {
            get
            {
                return new RelayCommand((o) =>
             {
                 string str = o.ToString();
                 if (str.Equals("打开仓库"))
                 {
                     OpenStoreFrm();
                 }
                 else if (str.Equals("关闭仓库"))
                 {
                     CloseStoreFrm();
                 }
             });
            }
        }

        private void CloseStoreFrm()
        {
            CurrentView = null;
        }

        private void OpenStoreFrm()
        {
            UserControlStore userControlStore = new UserControlStore();
            CurrentView = userControlStore;
        }
        public RelayCommand StoreArea
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    string str = o.ToString();
                    if (str.Equals("打开分区管理"))
                    {
                        OpenStoreAreaFrm();
                    }
                    else if (str.Equals("关闭分区管理"))
                    {
                        CloseStoreAreaFrm();
                    }
                });
            }
        }

        private void CloseStoreAreaFrm()
        {
           CurrentView = null;
        }

        private void OpenStoreAreaFrm()
        {
            UserControlStoreArea userControlStoreArea= new UserControlStoreArea();
            CurrentView = userControlStoreArea;
        }
    }
}
