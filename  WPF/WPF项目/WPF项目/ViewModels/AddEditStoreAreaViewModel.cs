using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF项目.Common;
using WPF项目.Share;

namespace WPF项目.ViewModels
{
    internal class AddEditStoreAreaViewModel : ViewModelBase
    {
        SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;
        public AddEditStoreAreaViewModel()
        {
        }
        public AddEditStoreAreaViewModel(Window win, int edit)
        {
            _win = win;
            _isEdit = edit;

            if (_isEdit > -1)
            {
                userEdit = sqlSugar.Queryable<StoreArea>().Where(it => it.StoreAreaId == _isEdit).Single();
                StoreId = userEdit.StoreId;
                StoreAreaName = userEdit.StoreAreaName;
                StoreAreaNo = userEdit.StoreAreaNo;
                Temperature = userEdit.Temperature.ToString();
                MinTemperature = userEdit.MinTemperature.ToString();
                MaxTemperature = userEdit.MaxTemperature.ToString();
                TemperatureStatus = userEdit.TemperatureStatus;
               

                Status = userEdit.Status;


                Remark = userEdit.Remark;
                Title = "编辑仓库区域";
                Buttontext = "修改";
            }
            var v = sqlSugar.Queryable<Store>().ToDictionary(it => it.StoreName, it => it.StoreId);
            dicStorePlace = v.ToDictionary(it => it.Key.ToString(), it => Convert.ToInt32(it.Value));
            if (dicStorePlace != null && dicStorePlace.Count > 0)
            {
                // 关键：初始值必须是字典里真实存在的 Value
                StoreId = dicStorePlace.First().Value;
            }
        }
        private Window _win;
        private int _isEdit;
        /// <summary>
        /// 仓库地点 绑定Combox
        /// </summary>
        public Dictionary<string, int> dicStorePlace { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// 状态 绑定Combox
        /// </summary>
        public Dictionary<string, int> dicUserState { get; set; } = new Dictionary<string, int>() { { "启用", 0 }, { "禁用", 1 } };

        public Dictionary<string, int> dicTemperatureStatus { get; set; } = new Dictionary<string, int>() { { "正常", 0 }, { "高预警", 1 }, { "低预警", 2 } };

        /// <summary>
        /// 温度状态
        /// </summary>
        private int temperatureStatus;
        public int TemperatureStatus
        {
            get
            {
                return temperatureStatus;
            }
            set
            {
                temperatureStatus = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        StoreArea userEdit = null;
        private string title = "添加仓库区域";
        public string Title { get { return title; } set { title = value; OnPropertyChanged(); } }

        private string buttontext = "添加";
        public string Buttontext { get { return buttontext; } set { buttontext = value; OnPropertyChanged(); } }
        private string storeAreaNo;
        public string StoreAreaNo
        {
            get
            {
                return storeAreaNo;
            }
            set
            {
                storeAreaNo = value;
                OnPropertyChanged();
            }
        }
        private string storeAreaName;
        public string StoreAreaName
        {
            get
            {
                return storeAreaName;
            }
            set
            {
                storeAreaName = value;
                OnPropertyChanged();
            }
        }
        private int storeId;
        public int StoreId
        {
            get
            {
                return storeId;
            }
            set
            {
                storeId = value;
                OnPropertyChanged();
            }
        }
        private string temperature;
        public string Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                temperature = value;
                OnPropertyChanged();
            }
        }
        private string minTemperature;
        public string MinTemperature
        {
            get
            {
                return minTemperature;
            }
            set
            {
                minTemperature = value;
                OnPropertyChanged();
            }
        }
        private string maxTemperature;
        public string MaxTemperature
        {
            get
            {
                return maxTemperature;
            }
            set
            {
                maxTemperature = value;
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
        private int status;
        public int Status
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
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    //修改
                    if (_isEdit > -1)
                    {
                        try
                        {
                          
                            //_isEdit  是编号
                            int row = sqlSugar.Updateable(new StoreArea()
                            {
                                
                                StoreAreaName = StoreAreaName,
                                StoreAreaNo = StoreAreaNo,
                                StoreId = StoreId,
                                Temperature = Convert.ToDouble(Temperature),
                                MinTemperature = Convert.ToDouble(MinTemperature),
                                MaxTemperature = Convert.ToDouble(MaxTemperature),
                                TemperatureStatus = TemperatureStatus,
                                Status = (short)Status,
                                CreateUserId = UserInfoStatic<User>.user?.UserId ?? 0,
                                LastUpdateUserId = UserInfoStatic<User>.user?.UserId ?? 0,
                                CreateTime = DateTime.Now,
                                LastUpdateTime = DateTime.Now,
                                Remark = Remark
                            }).Where(it => it.StoreAreaId == _isEdit).ExecuteCommand();
                            if (row > 0)
                            {
                                MessageBox.Show("修改成功");
                                _win.DialogResult = true;
                                _win.Close();
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                       
                    }
                    else
                    {
                        //添加
                        int row = sqlSugar.Insertable(new StoreArea()
                        {
                            StoreAreaName = StoreAreaName,
                            StoreAreaNo = StoreAreaNo,
                            StoreId = StoreId,
                            Temperature = Convert.ToDouble(Temperature),
                            MinTemperature = Convert.ToDouble(MinTemperature),
                            MaxTemperature = Convert.ToDouble(MaxTemperature),
                            TemperatureStatus = TemperatureStatus,
                            Status = (short)Status,
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
