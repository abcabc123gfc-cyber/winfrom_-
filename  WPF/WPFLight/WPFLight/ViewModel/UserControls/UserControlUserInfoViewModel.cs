using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFLight.Model;
using WPFLight.Service;
using WPFLight.View.UserControls;

namespace WPFLight.ViewModel.UserControls
{
    public class UserControlUserInfoViewModel:ViewModelBase
    {
        private int info;

        private Iservice<UserInfo> service;
        
        public UserControlUserInfoViewModel(Iservice<UserInfo> service)
        {
           
            this.service = service;
            dataInfo = service.GetAll();
          
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
