using Models;
using SqlSugar;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using WPF项目.Common;
using WPF项目.Share;
using WPF项目.Views;

namespace WPF项目.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;

        private ObservableCollection<string> usersName;
        public ObservableCollection<string> UsersName
        {
            get { return usersName; }
            set { usersName = value; OnPropertyChanged(); }
        }
        private readonly Window _loginWindow;

        public LoginViewModel(Window loginWindow)
        {
            _loginWindow = loginWindow;
            try
            {
                //db.Queryable<Student>().Select<int>("id").ToList();
                var v = sqlSugar.Queryable<User>().Select<string>("Account").ToList(); ;
                UsersName = new ObservableCollection<string>(v);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public LoginViewModel()
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
        private string labelText;
        public string LabeledText
        {
            get
            {
                return labelText;
            }
            set
            {
                labelText = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand LoginCommand
        {
            get
            {
                return new RelayCommand((o) =>
                 {
                     labelText = "账号不能为空";
                     //当 Account 没有输入时，它的值是 null，而你直接调用了 Account.Trim()，这就相当于对 null 调用方法，从而触发了“未将对象引用设置到对象的实例”异常。
                     if (string.IsNullOrEmpty(Account))
                     {
                         LabeledText = "账号不能为空";
                         return;
                     }
                     if (string.IsNullOrEmpty(password))
                     {
                         LabeledText = "密码不能为空";
                         return;
                     }
                     User u = sqlSugar.Queryable<User>().Where(x => x.Account == account && x.Password == password).Single();
                     if (u != null)
                     {
                        
                         UserInfoStatic<User>.user = u;
                         Main m= new Main();
                         m.Show();
                         _loginWindow.Close();
                     }
                     else
                     {
                         LabeledText = "密码错误";
                         return;
                     }
                 });
            }
        }
        public RelayCommand RegisterCommand
        {
            get
            {
                return new RelayCommand((o) =>
                 {
                     Register r = new Register();
                     if (r.ShowDialog()==true)
                     {
                         var v = sqlSugar.Queryable<User>().Select<string>("Account").ToList(); ;
                         UsersName = new ObservableCollection<string>(v);
                     }
                 });
            }
        }
    }
}
