using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace prictice
{
    public partial class Login1 : Form
    {
        public Login1()
        {
            InitializeComponent();
        }
        public static Dictionary<string, string> user = new Dictionary<string, string>();
        private void button3_Click(object sender, EventArgs e)
        {
            this.AutoSize = true;
            if (button3.Text.Trim() == "返回登录")
            {

                Login();
            }
            else
            {

                Register();
            }


        }
        public void Register()
        {
            Text = "注册界面";
            label1.Text = "注册账号";
            label2.Text = "注册密码";
            button2.Text = "完成注册";
            button3.Text = "返回登录";
            textBox1.Text = "";
            textBox2.Text = "";
            label3.Hide();
        }
        public void Login()
        {
            Text = "登录界面";
            label1.Text = "登录账号";
            label2.Text = "登录密码";
            button2.Text = "登录";
            button3.Text = "注册";
            label3.Show();
            textBox1.Text = "";
            textBox2.Text = "";
           
        }
        private void Form1_Load(object sender, EventArgs e)
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
                Form2 form2 = new Form2("登录成功界面");
                this.Hide();
                form2.ShowDialog();
            }
            else
            {
                register();
                Login();
            }

        }
        public bool login()
        {
            foreach (var item in user.Keys)
            {
                if (item == textBox1.Text && user[item] == textBox2.Text)
                {
                    return true;
                }
            }
            return false;


        }
        public void register()
        {
            
            foreach (var item in user.Keys)
            {
                if (item == textBox1.Text )
                {
                    MessageBox.Show("用户已存在");
                    return;
                }
            }
            MessageBox.Show("注册成功");
            user.Add(textBox1.Text, textBox2.Text);
         
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();
        }
    }
}
