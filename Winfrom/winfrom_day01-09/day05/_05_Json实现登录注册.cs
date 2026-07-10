using day05.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace day05
{
    public partial class _05_Json实现登录注册 : Form
    {
        public _05_Json实现登录注册()
        {
            InitializeComponent();
        }
        List<User> users;
        private void button2_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                UserName = textBox1.Text,
                PassWord = textBox2.Text
            };
            users = new List<User>();
            users.Add(user);
            string str = JsonConvert.SerializeObject(users);
            richTextBox1.AppendText(str);
            //全量写入
            File.WriteAllText(@"../../../../读写文件区/2.json", str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = File.ReadAllText(@"../../../../读写文件区/2.json");
            users = JsonConvert.DeserializeObject<List<User>>(str); 
            foreach (var item in users)
            {
                if (item.UserName == textBox1.Text && item.PassWord == textBox2.Text)
                {
                    richTextBox1.AppendText("登录成功");
                    return;
                }
            }
        }
    }
}
