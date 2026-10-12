using Models;
using SqlSugar;
using System;
using System.Collections;
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
    internal class UserControlStroreAreaViewModel:ViewModelBase
    {
        SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;
        // Students 改变 影响View
        private ObservableCollection<StoreArea> userInfo;

        private bool isQuery = false;
        public ObservableCollection<StoreArea> UserInfo
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
                        var v = sqlSugar.Queryable<StoreArea>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<StoreArea>(v);
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
                   
                    AddEditStoreArea addEditUser = new AddEditStoreArea(Query);
                    if (addEditUser.ShowDialog() == true)
                    {
                        var v = sqlSugar.Queryable<StoreArea>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<StoreArea>(v);
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
                    //MessageBox.Show(Query.ToString());
                    var v = MessageBox.Show("确定要删除记录?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (v == MessageBoxResult.OK)
                    {
                        if (sqlSugar.Deleteable<StoreArea>().Where(x => x.StoreAreaId == Query).ExecuteCommand() < 0) return;
                        var v1 = sqlSugar.Queryable<StoreArea>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<StoreArea>(v1);
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
                            AddEditStoreArea addEditUser = new AddEditStoreArea(Query);
                            if (addEditUser.ShowDialog() == true)
                            {
                                var v = sqlSugar.Queryable<StoreArea>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                                UserInfo = new ObservableCollection<StoreArea>(v);
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
                        var v = sqlSugar.Queryable<StoreArea>().Where(x => x.StoreAreaId == Query).ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
                        UserInfo = new ObservableCollection<StoreArea>(v);
                    }
                });
            }
        }

        #endregion

        public UserControlStroreAreaViewModel()
        {
            var v = sqlSugar.Queryable<StoreArea>().ToPageList(pageIndex, pageSize, ref totalCount, ref totalPage);
            UserInfo = new ObservableCollection<StoreArea>(v);
        }
    }
}

