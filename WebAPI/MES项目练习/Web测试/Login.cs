using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Web测试.Models;

namespace Web测试
{
    public partial class Login : Form
    {
        HttpClient client = single.GetInstance();
        public Login()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (client == null) return;
            if (!int.TryParse(textBox1.Text.Trim(), out int account) ||
        !int.TryParse(textBox2.Text.Trim(), out int password))
            {
                MessageBox.Show("账号和密码必须是纯数字！");
                return;
            }
            string url = $"User/GetLogin&Name{account}&Password/{password}";
            var v= await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            if (bool.TryParse(v, out bool result)&& result)
            {
                new Form1().Show();
            } 


        }
    }
}
