using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{
    public partial class UserAdd : AddBase
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        Models.User editModel = null;
        Dictionary<string, int> userTypeDict = new Dictionary<string, int>
{
    { "管理员", 1 },
    { "员工", 2 },
    { "经理", 3 },
    { "总经理", 4 },
    { "总裁", 5 }
};
        public UserAdd(string strTitle = "添加", int id = -1)
        {
            InitializeComponent();
            if (strTitle == "编辑")
            {
                label1.Text = "编辑";
                btnAdd.Text = "保存";
                if (id < 0) return;
                editModel = sqlSugar.Queryable<Models.User>().Where(x => x.UserId == id).First();
              
                txtUsername.Text = editModel.Account.ToString();
                txtPassword.Text = editModel.Password;
                txtPassword.Enabled = false;
                cbUserType.SelectedValue = Convert.ToInt32(editModel.UserTypeId);
                rtbRemark.Text = editModel.Remark;
            }
        }

        private void UserTypeAdd_Load(object sender, EventArgs e)
        {
            cbUserType.DataSource = userTypeDict.ToList();
            cbUserType.DisplayMember = "Key";
            cbUserType.ValueMember = "Value";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (var v in panel2.Controls)
            {
                if (v is TextBox)
                {
                    var textBox = v as TextBox;
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        MessageBox.Show("请输入完整信息");
                        return;
                    }
                }
                else if (v is RichTextBox)
                {
                    var richTextBox = v as RichTextBox;
                    if (string.IsNullOrEmpty(richTextBox.Text))
                    {
                        MessageBox.Show("请输入完整信息");
                        return;
                    }
                }
                else if (v is ComboBox)
                {
                    var comboBox = v as ComboBox;
                    if (comboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择用户类型");
                        return;
                    }
                }
            }

            if (btnAdd.Text == "添加")
            {

                Add();
            }
            else if (btnAdd.Text == "保存")
            {
                Save();
            }
        }
        private void Save()
        {

            if (editModel == null) return;
            editModel.Account = txtUsername.Text;
            editModel.Remark = rtbRemark.Text;
            editModel.Password = txtPassword.Text;
            editModel.LastUpdateTime = DateTime.Now;
            editModel.UserTypeId = (int)cbUserType.SelectedValue;
            if (sqlSugar.Updateable(editModel).ExecuteCommand() > 0)
            {
                MessageBox.Show("保存成功");
                this.Close();
                this.Dispose();
                DialogResult = DialogResult.OK;
            }
        }

        private void Add()
        {
            int temp = sqlSugar.Insertable<Models.User>(new Models.User
            {
                Account = txtUsername.Text,
                Password = txtPassword.Text,

                UserTypeId = Convert.ToInt32(cbUserType.SelectedValue),
                Remark = rtbRemark.Text,
                Status = 0,
                CreateUserId = 1,
                CreateTime = DateTime.Now,
                LastUpdateUserId = 1,
                LastUpdateTime = DateTime.Now

            }).ExecuteCommand();
            if (temp > 0)
            {
                MessageBox.Show("添加成功");
                this.Close();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
    }
}
