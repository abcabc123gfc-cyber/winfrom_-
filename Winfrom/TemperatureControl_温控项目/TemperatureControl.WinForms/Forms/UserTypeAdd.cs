using SqlSugar;
using System;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{

    public partial class UserTypeAdd : AddBase
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        int id;
        Models.UserType v = null;
        public UserTypeAdd(String strTitle = "添加", int id = -1)
        {
            InitializeComponent();
            if (strTitle == "编辑")
            {
                label1.Text = "编辑";
                btnAdd.Text = "保存";
                this.id = id;
                if (id < -1) return;
                v = sqlSugar.Queryable<Models.UserType>().Where(x => x.UserTypeId == id).First();
                txtUserTypeName.Text = v.UserTypeName;
                rtbRemark.Text = v.Remark;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserTypeName.Text) || string.IsNullOrEmpty(rtbRemark.Text))
            {
                MessageBox.Show("请输入用户类型名称");
                return;
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

            if (v == null) return;
            v.UserTypeName = txtUserTypeName.Text;
            v.Remark = rtbRemark.Text;
            if (sqlSugar.Updateable(v).ExecuteCommand() > 0)
            {
                MessageBox.Show("保存成功");
                this.Close();
                this.Dispose();
                DialogResult = DialogResult.OK;
            }
        }

        private void Add()
        {
            int temp = sqlSugar.Insertable<Models.UserType>(new Models.UserType
            {
                UserTypeName = txtUserTypeName.Text,
                Remark = rtbRemark.Text,
                Status = 0,
                CreateTime = DateTime.Now,
                CreateUserId = 1,
                LastUpdateUserId = 1,
                LastUpdateTime = DateTime.Now,
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
