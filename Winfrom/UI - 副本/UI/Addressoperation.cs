using System;

using System.Windows.Forms;
using 示例;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI
{
    public partial class Addressoperation : Form
    {
        int temp = -1;
        
        CustomerBLL customerBLL = new CustomerBLL();
        public Addressoperation(string str = "添加地址",string BtnStr = "添加")

        {
            InitializeComponent();
            if (str == "添加地址")
            {
                textBox1.Enabled = false;
                label3.Text = str;
                button1.Text = "添加";

            }
            else if (str == "修改地址")
            {
                label3.Text = str;
                button1.Text = "保存";
                label1.Hide();
                textBox1.Hide();
            }
           else if (str == "地址详情")
            {
               
                foreach (Control item in Controls)
                {
                    if (!(item is Label ))
                    {
                        item.Enabled = false;
                    }
                }
                button2.Enabled=true;
                button1.Hide();
            }
        }
        #region 添加 修改
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "添加")
            {
                if (customerBLL.AddAddress(AddressDate()))
                {

                    MessageBox.Show("添加成功");
                    Form_Close();
                }
            }
            else if (button1.Text == "保存" && customerBLL.UpdateAddress(AddressDate()))
            {
                MessageBox.Show("修改成功");
                Form_Close();
            }
        }

        private Address AddressDate()
        {
            try
            {

                return new Address()
                {
                    Id = temp,
                    Add = richTextBox1.Text
                };
            }
            catch (Exception)
            {

                MessageBox.Show("请输入地址");
                return null;
            }

        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Close();
        }
        // <summary>
        /// 关闭当前窗体打开 用户列表窗体
        /// </summary>
        private void Form_Close()
        {
            StudentList.Instance.Show();
            DialogResult = DialogResult.OK;
            this.Close();



        }

        private void Addressoperation_Load(object sender, EventArgs e)
        {
            if (StudentList.address1 != null)
            {
                temp = StudentList.address1.Id;
                textBox1.Text = StudentList.address1.Id.ToString();
                richTextBox1.Text = StudentList.address1.Add;
                StudentList.address1 = null;
            }
        }
    }
}
