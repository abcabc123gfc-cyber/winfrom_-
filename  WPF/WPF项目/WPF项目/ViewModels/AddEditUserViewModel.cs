using Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPF项目.Common;
using WPF项目.Share;

namespace WPF项目.ViewModels
{
    internal class AddEditUserViewModel : ViewModelBase
    {
        SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;
        private Window _win;
        private int _isEdit;
        /// <summary>
        /// 用户类型 绑定Combox
        /// </summary>
        public Dictionary<string, int> dicUserType { get; set; }
        /// <summary>
        /// 用户状态 绑定Combox
        /// </summary>
        public Dictionary<string, int> dicUserState { get; set; } = new Dictionary<string, int>() { { "启用", 0 }, { "禁用", 1 } };
        /// <summary>
        /// 修改
        /// </summary>
        User userEdit = null;
        private string title = "添加用户";
        public string Title { get { return title; } set { title = value; OnPropertyChanged(); } }

        private string buttontext = "添加";
        public string Buttontext { get { return buttontext; } set { buttontext = value; OnPropertyChanged(); } }
        private ObservableCollection<string> usersName;
        public ObservableCollection<string> UsersName
        {
            get { return usersName; }
            set { usersName = value; OnPropertyChanged(); }
        }
        public AddEditUserViewModel(Window win, int edit)
        {
            _win = win;
            _isEdit = edit;
            var tempDict = sqlSugar.Queryable<UserType>()
     .ToDictionary(it => it.UserTypeName, it => it.UserTypeId);
            dicUserType = tempDict.ToDictionary(it => it.Key.ToString(), it => Convert.ToInt32(it.Value));
            if (_isEdit > -1)
            {
                userEdit = sqlSugar.Queryable<User>().Where(it => it.UserId == _isEdit).Single();
                Account = userEdit.Account;
                Password = userEdit.Password;
                UserTypeId = userEdit.UserTypeId.ToString();
                Status = userEdit.Status.ToString();
                Remark = userEdit.Remark;
                Title = "编辑用户";
                Buttontext = "修改";
            }
        }
        public AddEditUserViewModel()
        {

        }
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
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        private string userTypeId;
        public string UserTypeId
        {
            get
            {
                return userTypeId;
            }
            set
            {
                userTypeId = value;
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
                        int row = sqlSugar.Updateable(new User()
                        {
                            Account = Account,
                            Password = Password,
                            UserTypeId = Convert.ToInt32(UserTypeId),
                            Status = short.Parse(Status),
                            
                            LastUpdateUserId = UserInfoStatic<User>.user?.UserId ?? 0,
                            LastUpdateTime = DateTime.Now,
                            Remark = Remark
                        }).Where(it => it.UserId == _isEdit).ExecuteCommand();
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
                        int row = sqlSugar.Insertable(new User()
                        {
                            Account = Account,
                            Password = Password,
                            UserTypeId = Convert.ToInt32(UserTypeId),
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
