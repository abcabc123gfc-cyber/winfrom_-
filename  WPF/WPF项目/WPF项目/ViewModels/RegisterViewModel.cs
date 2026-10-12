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

namespace WPF项目.ViewModels
{
    internal class RegisterViewModel:ViewModelBase
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
        private string title = "注册用户";
        public string Title { get { return title; } set { title = value; OnPropertyChanged(); } }

        private string buttontext = "注册";
        public string Buttontext { get { return buttontext; } set { buttontext = value; OnPropertyChanged(); } }
        private ObservableCollection<string> usersName;
        public ObservableCollection<string> UsersName
        {
            get { return usersName; }
            set { usersName = value; OnPropertyChanged(); }
        }
        public RegisterViewModel(Window win)
        {
            _win = win;
          
            var tempDict = sqlSugar.Queryable<UserType>()
     .ToDictionary(it => it.UserTypeName, it => it.UserTypeId);
            dicUserType = tempDict.ToDictionary(it => it.Key.ToString(), it => Convert.ToInt32(it.Value));
            
        }
        public RegisterViewModel()
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
                    var list = sqlSugar.Queryable<User>().Where(it => it.Account == Account).ToList();
                    if (list.Count > 0)
                    {
                        MessageBox.Show("用户已存在");
                        return;
                    }
                    if (_isEdit == 0)
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
                            MessageBox.Show("注册成功");
                            _win.DialogResult = true;
                            _win.Close();
                        }
                    }
                });
            }
        }

    }
}

