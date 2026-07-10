using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{
    public partial class PartitionAdd : AddBase
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        Models.StoreArea editModel = null;
        public PartitionAdd(string strTitle = "添加", int id = -1)
        {
            InitializeComponent();
            if (strTitle == "编辑")
            {
                label1.Text = "编辑";
                btnAdd.Text = "保存";
                if (id < 0) return;
                editModel = sqlSugar.Queryable<Models.StoreArea>().Where(x => x.StoreAreaId == id).First();
                txtStoreAreaName.Text = editModel.StoreAreaName;
                cbStore.SelectedValue = editModel.StoreId;
                txtTemperature.Text = editModel.Temperature.ToString();
                txtMinTemperature.Text = editModel.MinTemperature.ToString();
                txtMaxTemperature.Text = editModel.MaxTemperature.ToString();
                rtbRemark.Text = editModel.Remark;
            }
            InitCom();
        }
        private void InitCom()
        {
            var list = sqlSugar.Queryable<Models.Store>().ToList();
            foreach (var item in list)
            {

                dic.Add(item.StoreName, item.StoreId);
            }
            cbStore.DataSource = dic.ToList();
            cbStore.DisplayMember = "Key";
            cbStore.ValueMember = "Value";
        }
        private void PartitionAdd_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存/添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            editModel.StoreAreaName = txtStoreAreaName.Text;
            editModel.StoreId = (int)cbStore.SelectedValue;
            editModel.Temperature = Convert.ToDouble(txtTemperature.Text);
            editModel.MinTemperature = Convert.ToDouble(txtMinTemperature.Text);
            editModel.MaxTemperature = Convert.ToDouble(txtMaxTemperature.Text);
            editModel.Remark = rtbRemark.Text;

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
            int temp = sqlSugar.Insertable<Models.StoreArea>(new Models.StoreArea
            {
                StoreAreaNo = "P" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                StoreAreaName = txtStoreAreaName.Text,
                StoreId = (int)cbStore.SelectedValue,
                Temperature = Convert.ToDouble(txtTemperature.Text),
                MinTemperature = Convert.ToDouble(txtMinTemperature.Text),
                MaxTemperature = Convert.ToDouble(txtMaxTemperature.Text),
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
