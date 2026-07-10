using SqlSugar;
using System;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{
    public partial class WarehouseAdd : AddBase
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        Models.Store editModel = null;
        public WarehouseAdd(String strTitle = "添加", int id = -1)
        {
            InitializeComponent();
            if (strTitle == "编辑")
            {
                label1.Text = "编辑";
                btnAdd.Text = "保存";
                if (id < 0) return;
                editModel = sqlSugar.Queryable<Models.Store>().Where(x => x.StoreId == id).First();

                txtStoreName.Text = editModel.StoreName.ToString();
                txtStorePlace.Text = editModel.StorePlace;


                rtbRemark.Text = editModel.Remark;
            }
        }

        private void WarehouseAdd_Load(object sender, EventArgs e)
        {

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
            editModel.StoreName = txtStoreName.Text;
            editModel.Remark = rtbRemark.Text;
            editModel.StorePlace = txtStorePlace.Text;
            editModel.LastUpdateTime = DateTime.Now;
           
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
            int temp = sqlSugar.Insertable<Models.Store>(new Models.Store
            {
                StoreName = txtStoreName.Text,
                StorePlace = txtStorePlace.Text,

                AreaCount = 5,
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
