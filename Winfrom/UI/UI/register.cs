using System;
using System.Windows.Forms;
using 示例;
using 示例.BLL;

namespace UI
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void register_Load(object sender, EventArgs e)
        {
            //初始化下拉列表
            comboBox1.Text = "请选择";
            comboBox1.Items.Add("管理员");
            comboBox1.Items.Add("操作员");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Load();
        }
        private void Form_Load()
        {


            Form target = Application.OpenForms["Login1"];
            if (target != null)
            {
                Login1 form = target as Login1;
                form.Show();

                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            register1();
        }
        public void register1()
        {
            try
            {

                UserBLL userBLL = new UserBLL();
                User user = new User();
                user.Id = Convert.ToInt32(textBox1.Text);
                user.Name = textBox1.Text;
                user.Account = Convert.ToInt32(textBox1.Text);
                user.Password = Convert.ToInt32(textBox1.Text);
                user.Grade = comboBox1.Text;

                if (!userBLL.register(user))
                {
                    MessageBox.Show("用户已存在");
                    return;
                }
                MessageBox.Show("注册成功");
                Form_Load();
            }
            catch (Exception)
            {

                MessageBox.Show("请输入正确的信息");
            }

        }
    }
}
