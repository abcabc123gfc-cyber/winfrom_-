using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 示例;



namespace UI
{
    public partial class SelectStudent : Form
    {

        UserBLL userBLL = new UserBLL();
        CustomerBLL customerBLL = new CustomerBLL();
        /// <summary>
       /// 存储客户信息
        /// </summary>
        Customer customer1 = new Customer();
        List<User> bll;

        int userId;
        #region 初始化窗体
        public SelectStudent(string labText, string btnText, params User[] user1)
        {
            InitializeComponent();
            //初始化 bll列表
            //bll = userBLL.GetAllUser("1",1,out );

            label10.Text = labText;
            button1.Text = btnText;
            #region 初始化用户列表
            if (user1.Length > 0 && user1[0] != null)
            {
                userId = user1[0].Id;
                textBox1.Text = user1[0].Name;
                textBox5.Text = user1[0].Account.ToString();
                textBox4.Text = user1[0].Password.ToString();

                if (user1[0].Grade == "管理员")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }

            }
            #endregion
            if (labText == "用户详情")
            {
                button1.Hide();
                foreach (Control item in Controls)
                {
                    if (!(item is Label) && !(item is Button))
                    {

                        item.Enabled = false;
                    }
                }
            }
            if (labText == "添加客户")
            {
                InitCustomer();

            }
            if (labText == "编辑客户" || labText == "客户详情")
            {
                InitCustomer();
                if (StudentList.customer1 != null)
                {
                    customer1 = StudentList.customer1;
                    StudentList.customer1 = null;
                }
                else
                {
                    MessageBox.Show("请选择客户");
                }
                    textBox1.Text = customer1.Name;
                textBox5.Text = customer1.Phone;
                textBox4.Text = customer1.Balance.ToString();
            }
            if (labText == "客户详情")
            {
                button1.Hide();
                foreach (Control item in Controls)
                {
                    if (!(item is Label) && !(item is Button))
                    {

                        item.Enabled = false;
                    }
                }
            }

        }
        #endregion


        /// <summary>
        /// 关闭当前窗体打开 用户列表窗体
        /// </summary>
        private void Form_Close()
        {
            StudentList.Instance.Show();
            DialogResult = DialogResult.OK;
            this.Close();



        }
        #region 添加 修改 用户 客户 地址
        private void button1_Click(object sender, EventArgs e)
        {
            if (label10.Text == "添加用户" || label10.Text == "编辑用户")
            {
                if (button1.Text == "添加" && userBLL.AddUser(UserData("添加")))
                {

                    MessageBox.Show("添加成功");
                    Form_Close();
                }
                else if (button1.Text == "保存" && userBLL.UpdateUser(UserData("")))
                {

                   
                    MessageBox.Show("保存成功");

                    Form_Close();
                }
                else
                {
                    MessageBox.Show("请输入正确数据");
                }
            }
            else if (label10.Text == "添加客户" || label10.Text == "编辑客户")
            {

                if (button1.Text == "添加")
                {
                    Customer cu = CustomerData("添加");
                    if (cu != null && customerBLL.Adddal(cu))
                    {
                        MessageBox.Show("添加成功");
                        Form_Close();
                    }

                }
                else if (button1.Text == "保存")
                {
                    Customer cu = CustomerData("编辑");
                    if (cu != null && customerBLL.Updatedal(cu))
                    {
                        MessageBox.Show("修改成功");
                        Form_Close();
                    }

                }

            }
        }
        #endregion
        #region 用户表数据
        private User UserData(string str)
        {
            try
            {
                User user = new User();

                user.Id = userId;
                user.Name = textBox1.Text;
                user.Account = Convert.ToInt32(textBox5.Text);
                user.Password = Convert.ToInt32(textBox4.Text);
                user.Grade = radioButton1.Checked ? "管理员" : "操作员";
                user.state = 0;
                return user;
            }
            catch (Exception)
            {

                MessageBox.Show("请输入正确的用户数据");
                return null;
            }


        }
        #endregion

        #region 客户数据表
        private Customer CustomerData(string str)
        {

            try
            {
                Customer customer = new Customer();

                customer.Name = textBox1.Text;
                customer.Phone = textBox5.Text;
                customer.Balance = Convert.ToDouble(textBox4.Text);
                if (str == "添加")
                {

                  
                    return customer;
                }
                else if (str == "编辑")
                {
                    customer.Id = customer1.Id;
                    return customer;
                }
                return null;

            }
            catch (Exception)
            {

                MessageBox.Show("请输入正确数据客户表");
                return null;
            }
        }
        #endregion





        private void SelectStudent_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #region 初始化
        private void SelectStudent_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 初始化客户控件
        /// </summary>
        private void InitCustomer()
        {

            foreach (Control item in Controls)
            {
                if (!(item is TextBox) && !(item is Button))
                {
                    item.Hide();
                }

            }


            label1.Text = "客户名称";
            label2.Text = "手机号";
            label4.Text = "余额";

            label1.Show();
            label2.Show();
            label4.Show();



        }


        #endregion
        private void button2_Click_1(object sender, EventArgs e)
        {
            Form_Close();
        }
    }
}
