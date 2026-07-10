using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace day11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;

        }
        SqlConnection sqlConnection = null;
        private void BindDataGridView()
        {
            //C# 提供类 用来操作数据库

            try
            {
                //1. 数据库及案例连接
                //实例化一个数据库连接对象
                //参数是链接字符串 键值对
                //sever= . , 点 是默认的端口如果不是需要指定连接的那一个数据库服务 如 50736 格式: ip, 端口
                //. 点 表示本机, 端口号 1433 可以省略
                //database =db_name 指定连接数据库服务中的那一个数据库

                //uid =sa 账号
                //pwd =<PASSWORD> 密码
                sqlConnection = new SqlConnection("server=.;database=Test;uid=root;pwd=root");
                //2. 打开数据库连接
                sqlConnection.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (sqlConnection == null)
            {
                return;
            }
            string sql = "update Teacher set TeacherName='王三' where Id=1";
            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                int index = sqlCommand.ExecuteNonQuery();
            }

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlCommand sqlCommand = new SqlCommand("delete from Teacher where Id=2", sqlConnection))
            {
                int index = sqlCommand.ExecuteNonQuery();
                MessageBox.Show(index.ToString());
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (sqlConnection == null)
            {
                return;
            }
            using (SqlCommand sqlCommand = new SqlCommand("insert into Teacher(TeacherName,Age)  Values('王武',20)", sqlConnection))
            {
                int index = sqlCommand.ExecuteNonQuery();
                MessageBox.Show(index.ToString());
            }
        }

        /// <summary>
        /// 对数据进行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (sqlConnection == null)
            {
                return;
            }


            string sql = "select * from Teacher";
            //执行sql语句
            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {

                //查询数据库 获取结果
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    //SqlDataAdapter
                    //把查询数据库得到的数据 转换成DataTable 数据表
                    DataTable dataTable = new DataTable();

                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                }
            }

        }
    }
}
