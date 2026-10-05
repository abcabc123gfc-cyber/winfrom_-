using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using Web测试.Models;

namespace Web测试
{
    public partial class Form2 : Form
    {
        HttpClient httpClient = single.GetInstance();
        public Form2()
        {
            InitializeComponent();
            richTextBox1.Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            #region 方法1
            //var v = await httpClient.GetStreamAsync($"Address/GetIndex{int.Parse(textBox1.Text)}");
            //var v1 = new StreamReader(v, Encoding.UTF8).ReadToEnd();
            //richTextBox1.Text=v1;
            //var v1 = JsonConvert.DeserializeObject<RespondResult<List<Address>>>(v);
            //dataGridView1.DataSource = v1.Data;
            #endregion
            var v = await httpClient.GetAsync($"Address/GetIndex{int.Parse(textBox1.Text)}").Result.Content.ReadAsStringAsync();
            var v1 = JsonConvert.DeserializeObject<RespondResult<List<Address>>>(v);
            dataGridView1.DataSource = v1.Data;
        }
        private async void button1_Click_1(object sender, EventArgs e)
        {
            var v = await httpClient.GetAsync($"Address/GetSelect").Result.Content.ReadAsStringAsync();
            var v1 = JsonConvert.DeserializeObject<RespondResult<List<Address>>>(v);
            dataGridView1.DataSource = v1.Data;
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0 || dataGridView1.CurrentCell == null)
            {
                return;
            }
            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            var response = await httpClient.DeleteAsync($"Address/Address/{id}");
            var v = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"状态码：{response.StatusCode} {response.StatusCode}\n内容：[{v}]");
            var v1 = JsonConvert.DeserializeObject<RespondResult<Address>>(v);
            if (v1.Code.Equals("200"))
            {
                MessageBox.Show(v1.Message);
                button1_Click_1(null, null);
            }
        }
    }
}
