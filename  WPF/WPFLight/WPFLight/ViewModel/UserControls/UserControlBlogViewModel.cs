using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows;
using WPFLight.Model;
using WPFLight.Service;

namespace WPFLight.ViewModel.UserControls
{
    public class UserControlBlogViewModel : ViewModelBase
    {
        private int info;

        private Iservice<BlogInfo> service;

        public UserControlBlogViewModel(Iservice<BlogInfo> service)
        {

            this.service = service;
            dataInfo = service.GetAll();
           

        }
        private List<BlogInfo> dataInfo;
        public List<BlogInfo> DataInfo
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
