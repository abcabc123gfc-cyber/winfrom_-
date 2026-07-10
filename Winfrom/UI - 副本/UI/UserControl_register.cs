using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 示例;

namespace UI
{
    public partial class UserControl_register : UserControl
    {
        public UserControl_register()
        {
            InitializeComponent();
        }
        #region 定义事件
        public event EventHandler Register;
        #endregion
        #region 注册
        private void button1_Click(object sender, EventArgs e)
        {
            register1();
        }

        /// <summary>
        /// 注册验证
        /// </summary>
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
                user.state = 0;
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
        #endregion

        #region 取消
        private void button2_Click(object sender, EventArgs e)
        {
            Form_Load();
        }

        private void Form_Load()
        {
            Register?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region 初始化控件
        private void UserControl_register_Load(object sender, EventArgs e)
        {
            //初始化下拉列表
            comboBox1.Text = "请选择";
            comboBox1.Items.Add("管理员");
            comboBox1.Items.Add("操作员");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        #endregion
    }
}
