using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFLight.Model;
using WPFLight.Service;
using WPFLight.View.UserControls;

namespace WPFLight.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Iservice<UserInfo> service;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(Iservice<UserInfo> service)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
          
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                //方式一：
                //_name = value;
                //RaisePropertyChanged();
                //方式二：
                Set(ref _name, value);
            }
        }
        private UserControl uc;
        public UserControl UC
        {
            get
            {
                return uc;
            }
            set
            {
                //方式一：
                //_name = value;
                //RaisePropertyChanged();
                //方式二：
                Set(ref uc, value);
            }
        }
        void ChangeName(object o)
        {
            Thread.Sleep(7000);
            Name = "改名了";
        }
        public ICommand ChangeNameCmd
        {
            get
            {
                return new RelayCommand<object>(o =>
                {
                    Name = "命令改名";
                });
            }
        }
        public ICommand UserCommand
        {
            get
            {
                return new RelayCommand<object>(o =>
                {
                    string pageName = o.ToString();
                   
                    var type = Type.GetType($"WPFLight.View.UserControls.{pageName}");
                    UC = (UserControl)Activator.CreateInstance(type);
                   
                });
            }
        }
        public ICommand BlogCommand
        {
            get
            {
                return new RelayCommand<object>(o =>
                {
                    string pageName = o.ToString();
                    
                    var type = Type.GetType($"WPFLight.View.UserControls.{pageName}");
                    UC = (UserControl)Activator.CreateInstance(type);

                });
            }
        }
        private List<UserInfo> dataInfo;
        public List<UserInfo> DataInfo
        {
            get
            {
                return dataInfo;
            }
            set
            {
                dataInfo = value;
                RaisePropertyChanged();
            }
        }
        


    }
}