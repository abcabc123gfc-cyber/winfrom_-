using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Web测试.Models;

namespace Web测试
{
    public partial class Form1 : Form
    {
        HttpClient httpClient = single.GetInstance();

        public Form1()
        {
            InitializeComponent();

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var v = await httpClient.GetAsync("User/GetSelecte").Result.Content.ReadAsStringAsync();

            var v1 = JsonConvert.DeserializeObject<RespondResult<List<UserInfo>>>(v);
            if (v1.Code == "200")
            {
                List<UserInfo> userInfos = v1.Data as List<UserInfo>;
                dataGridView1.DataSource = userInfos;
            }
            else
            {
                MessageBox.Show(v1.Message);
            }

        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            var v = await httpClient.GetAsync($"User/GetName/{textBox1.Text}").Result.Content.ReadAsStringAsync();

            var v1 = JsonConvert.DeserializeObject<RespondResult<List<UserInfo>>>(v);
            if (v1.Code == "200")
            {
                dataGridView1.DataSource = v1.Data;
            }
            else
            {
                MessageBox.Show(v1.Message);
            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button4_ClickAsync(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) return;
            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            string str = JsonConvert.SerializeObject($"{id}");
            var conn = new StringContent(str, Encoding.UTF8, "application/json");
            var v = await httpClient.PostAsync($"User/Delete", conn).Result.Content.ReadAsStringAsync();
            var v1 = JsonConvert.DeserializeObject<RespondResult<int>>(v);

            if (v1.Data == 1)
            {
                MessageBox.Show("删除成功！");
                button1_Click(sender, e);
            }
            else
            {

            }
        }

        private async void button5_ClickAsync(object sender, EventArgs e)
        {
            string url = $"User/GetInsert&Name/{1}&Account/{222}&Password/{133}&Grade/{1}&State/{0}";
            try
            {
                var v = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
                var v1 = JsonConvert.DeserializeObject<RespondResult<int>>(v);
                if (v1.Code == "200")
                {
                    MessageBox.Show("添加成功！");
                    button1_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(v1.Message);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
            string url = $"User/GetUpdate&Id/{308}&Name/{1}&Account/{5522}&Password/{133}&Grade/{1}&State/{0}";
            try
            {
                var v = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
                var v1 = JsonConvert.DeserializeObject<RespondResult<int>>(v);
                if (v1.Code == "200")
                {
                    MessageBox.Show("修改成功！");
                    button1_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(v1.Message);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
