using System;
using System.Drawing;
using System.Windows.Forms;
using 示例.BLL;

namespace UI
{
    public partial class Login1 : Form
    {
        //实例化业务层对象 用户管理
        UserBLL userBLL = new UserBLL();

        public Login1()
        {
            InitializeComponent();
            Form1_Load();
            //导入测试数据
            Date_Test();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            register register = new register();
            register.Show();
            this.Hide();
        }

        private void Form1_Load()
        {
            label1.Text = "登录账号";
            label2.Text = "登录密码";
            label3.ForeColor = Color.Red;
            Text = "登录界面";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Trim() == "登录" && login())
            {
                //登录成功导入权限标识
                StudentList form2 = new StudentList(userBLL.Personalinfo().Grade);
                this.Hide();
                form2.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }


        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public bool login()
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请填写完整");
                return false;
            }
            try
            {
                if (userBLL.Login(int.Parse(textBox1.Text), int.Parse(textBox2.Text)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception    )
            {

                return false;

            }


        }
   
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
        #region 测试数据导入 用户 客户及客户地址
        DateTest dateTest = new DateTest();
        private void Date_Test()
        {
            dateTest.Date();
        }
        #endregion



        private void Login1_MouseLeave(object sender, EventArgs e)
        {



        }

        private void Login1_MouseEnter(object sender, EventArgs e)
        {


        }

        private void Login1_Load(object sender, EventArgs e)
        {

            textBox1.Text = "1";
            textBox2.Text = "1";


        }
    }
}
