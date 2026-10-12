using SqlSugar;
using System;
using System.IO;
using System.Windows;
using WPF项目.Share;
using WPF项目.ViewModels;

namespace WPF项目.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlSugarClient sqlSugar = SQLServerHerper.InstanceSqlSugar;
            try
            {
                #region 使用功能sugar 生成实体表
                string filePath = System.IO.Path.Combine(AppContext.BaseDirectory) + @"..\..\Entities";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    sqlSugar.DbFirst.IsCreateAttribute().CreateClassFile(filePath);
                }
                else
                {
                    sqlSugar.DbFirst.IsCreateAttribute().CreateClassFile(filePath);
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

