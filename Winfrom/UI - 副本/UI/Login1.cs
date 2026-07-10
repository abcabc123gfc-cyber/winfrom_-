using System;
using System.Windows.Forms;

namespace UI
{
    public partial class Login1 : Form
    {

        #region 初始化
        UserControl_Login UserControl_Login = new UserControl_Login();
        UserControl_register UserControl_register = new UserControl_register();
        public Login1()
        {
            InitializeComponent();
            this.Controls.Add(UserControl_Login);
            this.Controls.Add(UserControl_register);

            #region 订阅事件_事件的响应者
            UserControl_register.Hide();
            UserControl_Login.Login += UserControl_Login_Login_clicke;
            UserControl_Login.LoginSuccess +=(s,e)=>this.Hide();
            UserControl_register.Register += UserControl_Login_Register_clicke;
            #endregion
        }


        #endregion

        #region 订阅登录
        private void UserControl_Login_Login_clicke(object sender, EventArgs e)
        {
            UserControl_Login.Hide();
            UserControl_register.Show();
            
         
           
        }

        #endregion

        #region 订阅注册
        private void UserControl_Login_Register_clicke(object sender, EventArgs e)
        {
            UserControl_Login.Show();
            UserControl_register.Hide();
            this.Text = "注册";
        }
        #endregion



    }
}
