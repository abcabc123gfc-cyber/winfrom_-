using EF框架_代码优先.Contexts;
using EF框架_代码优先.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EF框架_代码优先
{
    public partial class Form1 : Form
    {
        EFUserModel eFUserModel = new EFUserModel();
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RenderData<T>(List<T> ts)
        {
            dataGridView1.DataSource = ts;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            UserInfo userInfo = new UserInfo()
            {
                Account = "1234",
                Password = "Password",
                State = 0

            };
            eFUserModel.UserInfo.Add(userInfo);
            if (eFUserModel.SaveChanges()>0)
            {
                MessageBox.Show("添加成功");
                RenderData(eFUserModel.UserInfo.Where(m=>1==1).ToList());
            }
        }
    }
}
