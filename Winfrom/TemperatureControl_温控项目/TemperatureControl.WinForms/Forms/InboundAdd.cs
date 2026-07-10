using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{
    public partial class InboundAdd : AddBase
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        Dictionary<string, int> dic1 = new Dictionary<string, int>();
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        Models.StorageRecord editModel = null;
        public InboundAdd(string strTitle = "添加", int id = -1)
        {

            InitializeComponent();
            if (strTitle == "编辑")
            {
                label1.Text = "编辑";
                btnAdd.Text = "保存";
                if (id < 0) return;
                editModel = sqlSugar.Queryable<Models.StorageRecord>().InSingle(id);
                dtpIncomeTime.Format = DateTimePickerFormat.Custom;
                dtpIncomeTime.CustomFormat = "yyyy-MM-dd HH:mm";

                cbStoreArea.SelectedValue = Convert.ToInt32(editModel.StoreAreaId);
                cbProduct.SelectedValue = Convert.ToInt32(editModel.ProductId);
                txtAmount.Text = editModel.Amount.ToString();
                dtpIncomeTime.Value = editModel.IncomeTime;
                rtbRemark.Text = editModel.Remark;


            }
            btnAdd.Click += btnAdd_Click;
            InitCom();
        }
        private void InitCom()
        {

            var list = sqlSugar.Queryable<Models.StoreArea>().ToList();
            foreach (var item in list)
            {

                dic.Add(item.StoreAreaName, item.StoreAreaId);
            }
            cbStoreArea.DataSource = dic.ToList();
            cbStoreArea.DisplayMember = "Key";
            cbStoreArea.ValueMember = "Value";
            var list1 = sqlSugar.Queryable<Models.Product>().ToList();
            foreach (var item in list1)
            {

                dic1.Add(item.ProductName, item.ProductId);
            }
            cbProduct.DataSource = dic1.ToList();
            cbProduct.DisplayMember = "Key";
            cbProduct.ValueMember = "Value";

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

           
            editModel.StoreAreaId = Convert.ToInt32(cbStoreArea.SelectedValue);
            editModel.ProductId = Convert.ToInt32(cbProduct.SelectedValue);
            editModel.Amount = Convert.ToInt32(txtAmount.Text);
            editModel.IncomeTime = dtpIncomeTime.Value;
            editModel.Remark = rtbRemark.Text;

            editModel.LastUpdateTime = DateTime.Now;

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
            int temp = sqlSugar.Insertable<Models.StorageRecord>(new Models.StorageRecord
            {

                

                StoreAreaId = Convert.ToInt32(cbStoreArea.SelectedValue),
                ProductId = Convert.ToInt32(cbProduct.SelectedValue),
                Amount = Convert.ToInt32(txtAmount.Text),
                IncomeTime = dtpIncomeTime.Value,
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
        #endregion

        private void InboundAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
