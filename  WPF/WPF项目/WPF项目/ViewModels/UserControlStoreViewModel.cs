using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF项目.Common;
using WPF项目.Share;
using WPF项目.Views;

namespace WPF项目.ViewModels
{
    internal class UserControlStoreViewModel:ViewModelBase
    {
        SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;
        // Students 改变 影响View
        private ObservableCollection<Store> userInfo;

        private bool isQuery = false;
        public ObservableCollection<Store> UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
                OnPropertyChanged();
            }
        }
        #region 分页
        private int pageIndex = 1;
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
            set
            {
                pageIndex = value;
                OnPropertyChanged();
            }
        }
        private int totalCount = 1;
        public int TotalCount
        {
            get
            {
                return totalCount;
            }
            set
            {
                totalCount = value;
                OnPropertyChanged();
            }
        }
        private int totalPage = 1;
        public int TotalPage
        {
            get
            {
                return totalPage;
            }
            set
            {
                totalPage = value;
                OnPropertyChanged();
            }
        }
        private int pageSize = 6;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand PageUpdatedCmd
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    if (!isQuery)
                    {
                        var v = sqlSugar.Queryable<Store>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<Store>(v);
                    }


                });
            }
        }
        #endregion
        #region crud
        /// <summary>
        /// 查询
        /// </summary>
        private string _keyword;
        public string Keyword
        {
            get => _keyword;
            set => _keyword = value;
        }
        public RelayCommand ModifyCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    if (!int.TryParse(o.ToString(), out int Query))
                    {
                        return;
                    }
                    AddEditStore addEditUser = new AddEditStore(Query);
                    if (addEditUser.ShowDialog() == true)
                    {
                        var v = sqlSugar.Queryable<Store>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<Store>(v);
                    }
                });
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    if (!int.TryParse(o.ToString(), out int Query))
                    {
                        return;
                    }
                    var v = MessageBox.Show("确定要删除记录?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (v == MessageBoxResult.OK)
                    {
                        if (sqlSugar.Deleteable<Store>().Where(x => x.StoreId == Query).ExecuteCommand() < 0) return;
                        var v1 = sqlSugar.Queryable<Store>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<Store>(v1);
                    }
                });
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {

                    if (int.TryParse(o.ToString(), out int Query))
                    {
                        if (Query == -1)
                        {
                            AddEditStore addEditUser = new AddEditStore(Query);
                            if (addEditUser.ShowDialog() == true)
                            {
                                var v = sqlSugar.Queryable<Store>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                                UserInfo = new ObservableCollection<Store>(v);
                            }
                        }
                    }
                });
            }
        }
        public RelayCommand QueryCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {

                    if (int.TryParse(o.ToString(), out int Query))
                    {
                        PageIndex = 1;
                        var v = sqlSugar.Queryable<Store>().Where(x => x.StoreId == Query).ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<Store>(v);
                    }
                });
            }
        }

        #endregion

        public UserControlStoreViewModel()
        {
            var v = sqlSugar.Queryable<Store>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
            UserInfo = new ObservableCollection<Store>(v);
        }
    }
}
