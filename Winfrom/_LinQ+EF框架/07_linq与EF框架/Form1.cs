using _07_linq与EF框架.Context;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace _07_linq与EF框架
{
    public partial class Form1 : Form
    {

        #region 初始化分页
        int UserPageSize = 10;
        int UserPageIndex = 1;
        int UserTotalCount = 0;
        #endregion


        linqDbModel db = new linqDbModel();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //隐藏表头
            dataGridView1.ColumnHeadersVisible = true;
            //隐藏例头
            dataGridView1.RowHeadersVisible = false;
            //列宽自适应
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            InitData();




        }

        private void InitData()
        {
            IQueryable<UserInfo> a = db.UserInfos;
            //获取总分数数量
            UserTotalCount = (int)Math.Ceiling((double)a.Count() / (double)UserPageSize);
            //显示当前页码
            lblTotalPage.Text = $"{UserPageIndex}/{UserTotalCount}";
            a = a.OrderByDescending(x => x.Id).Skip((UserPageIndex - 1) * UserPageSize).Take(UserPageSize);
            dataGridView1.DataSource = a.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var a = db.UserInfos.Where(x => x.Account.Contains(textBox1.Text)).ToList();
                dataGridView1.DataSource = a;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (UserPageIndex < UserTotalCount)
            {
                UserPageIndex++;
                InitData();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (UserPageIndex > 1)
            {
                UserPageIndex--;
                InitData();
            }

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            UserPageIndex = 1;
            InitData();

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            UserPageIndex = UserTotalCount;
            InitData();

        }
    }
}
