using SqlSugar;
using System;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{
    public partial class ProductAdd : AddBase
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        Models.Product editModel = null;
        public ProductAdd(string strTitle = "添加", int id = -1)
        {
            InitializeComponent();
            if (strTitle == "编辑")
            {
                label1.Text = "编辑";
                btnAdd.Text = "保存";
                if (id < 0) return;
                editModel = sqlSugar.Queryable<Models.Product>().InSingle(id);
                txtProductName.Text = editModel.ProductName;
                txtMinTemperature.Text = editModel.MinTemperature.ToString();
                txtMaxTemperature.Text = editModel.MaxTemperature.ToString();
                rtbRemark.Text = editModel.Remark;
            }
            btnAdd.Click += btnAdd_Click;
        }
        #region 添加/保存
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

            editModel.ProductName = txtProductName.Text.Trim();
            editModel.MinTemperature = Convert.ToDecimal(txtMinTemperature.Text.Trim());
            editModel.MaxTemperature = Convert.ToDecimal(txtMaxTemperature.Text.Trim());
            editModel.Remark = rtbRemark.Text.Trim();
            editModel.LastUpdateUserId = 1;
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
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            int temp = sqlSugar.Insertable<Models.Product>(new Models.Product
            {
                ProductNo = timestamp.ToString(),
                ProductName = txtProductName.Text.Trim(),
                MinTemperature = Convert.ToDecimal(txtMinTemperature.Text.Trim()),
                MaxTemperature = Convert.ToDecimal(txtMaxTemperature.Text.Trim()),
                Remark = rtbRemark.Text.Trim(),
               
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
        #endregion

    }
}
