using model;
using System;
using System.Drawing;
using System.Windows.Forms;
using 示例;


namespace UI
{
    public partial class UserControl_Login : UserControl
    {
        #region 初始化对象
        //实例化业务层对象 用户管理
        UserBLL userBLL = new UserBLL();

        Bitmap bmp = null;
        Color[] cor = { Color.Blue, Color.Yellow, Color.Black, Color.Red };

        Random rnd = new Random();
        string str = null;

        #endregion
        #region 定义事件
        public event EventHandler Login;
        public event EventHandler LoginSuccess;
        #endregion

        public UserControl_Login()
        {
            InitializeComponent();
           
        }

        #region 初始化控件
        private void UserControl_Login_Load(object sender, EventArgs e)
        {
            Login1_Load(sender, e);
        }
        private void Login1_Load(object sender, EventArgs e)
        {
            label3.ForeColor = cor[rnd.Next(0, 4)];
            textBox1.Text = "8";
            textBox2.Text = "57";
            pictureBox3.Image = img();
            uiTextBox1.Text = str;


            label1.Text = "登录账号";
            label2.Text = "登录密码";
           

        }
        #endregion

        #region 生成验证码位图
        private Bitmap img()
        {
            str = null;
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToString(rnd.Next(0, 9));
            }
            bmp = new Bitmap(100, 50);

            Graphics graphics = Graphics.FromImage(bmp);
            for (int i = 0; i < 5; i++)
            {

                graphics.DrawString(str[i].ToString(), new Font("Arial", rnd.Next(7, 15)), new SolidBrush(cor[rnd.Next(0, 4)]), (i) * 17, 0);
            }
            for (int i = 0; i < 10; i++)
            {
                graphics.DrawLine(new Pen(cor[rnd.Next(0, 4)]), new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height)), new Point(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height)));

            }
            for (int i = 0; i < 50; i++)
            {
                bmp.SetPixel(rnd.Next(0, bmp.Width), rnd.Next(0, bmp.Height), cor[rnd.Next(0, 4)]);
            }

            return bmp;
        }
        #endregion

        #region 重绘验证码
        /// <summary>
        /// 重绘验证码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = img();
            uiTextBox1.Text = str;
        }
        #endregion

        #region 登录逻辑
        /// <summary>
        /// 触发登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Trim() == "登录" && login())
            {
              
                //登录成功导入权限标识
                StudentList form2 = new StudentList(LoggedInUser.user.Grade);
                LoginSuccess?.Invoke(this, null);
                form2.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
        }



        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        private bool login()
        {
           
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                MessageBox.Show("请填写完整");
                return false;
            }
            try
            {
                if (userBLL.Login(int.Parse(textBox1.Text), int.Parse(textBox2.Text)) && uiTextBox1.Text.Equals(str))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;

            }


        }
        #endregion
        #region 注册逻辑
        private void button3_Click(object sender, EventArgs e)
        {
            Login?.Invoke(this, null);


        }
        #endregion



        #region 游客登录
        /// <summary>
        /// 游客登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            StudentList form2 = new StudentList("游客登录");
            this.Hide();
            form2.ShowDialog();
        }

        #endregion

      

    }
}

