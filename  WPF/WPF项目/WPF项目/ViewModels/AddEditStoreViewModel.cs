using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Windows;
using WPF项目.Common;
using WPF项目.Share;

namespace WPF项目.ViewModels
{
    internal class AddEditStoreViewModel : ViewModelBase
    {
        SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;
        public AddEditStoreViewModel()
        {
        }
        public AddEditStoreViewModel(Window win, int edit)
        {
            _win = win;
            _isEdit = edit;

            if (_isEdit > -1)
            {
                userEdit = sqlSugar.Queryable<Store>().Where(it => it.StoreId == _isEdit).Single();
                StoreName = userEdit.StoreName;
                StorePlace = userEdit.StorePlace;
                AreaCount = userEdit.AreaCount;
                Status =  userEdit.Status.ToString();


                Remark = userEdit.Remark;
                Title = "编辑用户";
                Buttontext = "修改";
            }
        }
        private Window _win;
        private int _isEdit;

        /// <summary>
        /// 状态 绑定Combox
        /// </summary>
        public Dictionary<string, int> dicUserState { get; set; } = new Dictionary<string, int>() { { "启用", 0 }, { "禁用", 1 } };
        /// <summary>
        /// 修改
        /// </summary>
        Store userEdit = null;
        private string title = "添加仓库";
        public string Title { get { return title; } set { title = value; OnPropertyChanged(); } }

        private string buttontext = "添加";
        public string Buttontext { get { return buttontext; } set { buttontext = value; OnPropertyChanged(); } }
        private string storeName;
        public string StoreName
        {
            get
            {
                return storeName;
            }
            set
            {
                storeName = value;
                OnPropertyChanged();
            }
        }
        private string storePlace;
        public string StorePlace
        {
            get
            {
                return storePlace;
            }
            set
            {
                storePlace = value;
                OnPropertyChanged();
            }
        }
        private int areaCount;
        public int AreaCount
        {
            get
            {
                return areaCount;
            }
            set
            {
                areaCount = value;
                OnPropertyChanged();
            }
        }
        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        private string remark;
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    //修改
                    if (_isEdit > -1)
                    {
                      
                        //_isEdit  是编号
                        int row = sqlSugar.Updateable(new Store()
                        {
                            StoreName = StoreName,
                            StorePlace = StorePlace,
                            AreaCount = AreaCount,
                            Status = short.Parse( Status),
                            CreateUserId = UserInfoStatic<User>.user?.UserId ?? 0,
                            LastUpdateUserId = UserInfoStatic<User>.user?.UserId ?? 0,
                            CreateTime = DateTime.Now,
                            LastUpdateTime = DateTime.Now,
                            Remark = Remark
                        }).Where(it => it.StoreId == _isEdit).ExecuteCommand();
                        if (row > 0)
                        {
                            MessageBox.Show("修改成功");
                            _win.DialogResult = true;
                            _win.Close();
                        }
                    }
                    else
                    {
                        //添加
                        int row = sqlSugar.Insertable(new Store()
                        {
                            StoreName = StoreName,
                            StorePlace = StorePlace,
                            AreaCount = AreaCount,
                            Status = short.Parse(Status),

                            CreateUserId = UserInfoStatic<User>.user?.UserId ?? 0,
                            CreateTime = DateTime.Now,
                            Remark = Remark
                        }).ExecuteCommand();
                        if (row > 0)
                        {
                            MessageBox.Show("添加成功");
                            _win.DialogResult = true;
                            _win.Close();
                        }
                    }
                });
            }
        }
    }
}
